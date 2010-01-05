//-------------
// <copyright file="PodCastRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;
    using Model = Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Pod cast repository implemented with SQLite.
    /// </summary>
    internal class PodCastRepository : BaseRepository,
        IPodCastRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastRepository"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        public PodCastRepository(string databasePath)
            : base(databasePath)
        {
        }

        #endregion

        #region IPodCastRepository Members

        /// <summary>
        /// Deletes the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        public void DeletePodCast(int podCastId)
        {
            this.SimpleRepository.Delete<DBPodCast>(podCastId);
        }

        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        /// <returns>The pod casts.</returns>
        public IList<PodCastIndexViewModel> GetPodCasts()
        {
            var podCasts =
                this.SimpleRepository.All<DBPodCast>();
            var result = podCasts.ToList()
                .Select(pc => new PodCastIndexViewModel(
                    (int)pc.PodCastId,
                    pc.Name,
                    new Uri(pc.Url)));
            return result.ToList();
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        public Model.PodCast GetPodCast(int podCastId)
        {
            var podCast =
                this.SimpleRepository.Single<DBPodCast>(podCastId);
            if (podCast == null)
            {
                return null;
            }

            return new Model.PodCast(
                podCastId,
                podCast.Name,
                new Uri(podCast.Url),
                podCast.Description,
                podCast.Author);
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        public PodCastDetailsViewModel GetPodCastDetails(
            int podCastId)
        {
            var podCast =
                this.SimpleRepository.Single<DBPodCast>(
                podCastId);
            if (podCast == null)
            {
                return null;
            }

            return new PodCastDetailsViewModel(
                podCastId,
                podCast.Name,
                new Uri(podCast.Url),
                podCast.Author,
                podCast.Description);
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public void SavePodCast(
            Model.PodCast podCast)
        {
            DBPodCast pc = new DBPodCast
            {
                Author = podCast.Author,
                Description = podCast.Description,
                Name = podCast.Name,
                Url = podCast.Url.ToString()
            };
            this.SimpleRepository.Add<DBPodCast>(pc);
            podCast.Id = (int)pc.PodCastId;
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public void SavePodCast(
            PodCastDetailsViewModel podCast)
        {
            DBPodCast pc =
                this.SimpleRepository.Single<DBPodCast>(
                podCast.Id);
            if (pc == null)
            {
                return;
            }

            pc.Author = podCast.Author;
            pc.Description = podCast.Description;
            pc.Name = podCast.Name;
            pc.Url = podCast.Url.ToString();

            this.SimpleRepository.Update<DBPodCast>(pc);
        }

        #endregion
    }
}