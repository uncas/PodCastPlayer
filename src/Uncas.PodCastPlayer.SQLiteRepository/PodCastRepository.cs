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
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;

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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public void DeletePodCast(int podCastId)
        {
            try
            {
                this.DB.Delete<DBPodCast>(podCastId);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown SubSonic exceptions
                throw new RepositoryException(
                    "Error trying to delete pod cast",
                    ex);
            }
        }

        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        /// <returns>The pod casts.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public IList<PodCastIndexViewModel> GetPodCasts()
        {
            IQueryable<DBPodCast> podCasts = null;
            try
            {
                podCasts =
                    this.DB.All<DBPodCast>();
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown SubSonic exceptions
                throw new RepositoryException(
                    "Error trying to get pod casts",
                    ex);
            }

            var result = podCasts.ToList()
                .Select(pc => pc.AsIndexViewModel());
            return result.ToList();
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public PodCast GetPodCast(int podCastId)
        {
            var podCast = this.GetDBPodCast(podCastId);
            if (podCast == null)
            {
                return null;
            }

            return podCast.AsModel();
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public PodCastDetailsViewModel GetPodCastDetails(
            int podCastId)
        {
            var podCast = this.GetDBPodCast(podCastId);
            if (podCast == null)
            {
                return null;
            }

            return podCast.AsDetailsViewModel();
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public void SavePodCast(
            PodCast podCast)
        {
            if (podCast == null
                || podCast.Url == null)
            {
                return;
            }

            DBPodCast pc = new DBPodCast
            {
                Author = podCast.Author,
                Description = podCast.Description,
                Name = podCast.Name,
                Url = podCast.Url.ToString()
            };

            try
            {
                this.DB.Add<DBPodCast>(pc);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown SubSonic exceptions
                throw new RepositoryException(
                    "Error trying to add pod cast to database",
                    ex);
            }

            podCast.Id = (int)pc.PodCastId;
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public void SavePodCast(
            PodCastDetailsViewModel podCast)
        {
            if (podCast == null
                || podCast.Url == null)
            {
                return;
            }

            var pc = this.GetDBPodCast(podCast.Id);
            if (pc == null)
            {
                return;
            }

            pc.Author = podCast.Author;
            pc.Description = podCast.Description;
            pc.PodCastId = (long)podCast.Id;
            pc.Name = podCast.Name;
            pc.Url = podCast.Url.ToString();

            try
            {
                this.DB.Update<DBPodCast>(pc);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown SubSonic exceptions
                throw new RepositoryException(
                    "Error trying to update pod cast in database",
                    ex);
            }
        }

        #endregion

        /// <summary>
        /// Gets the DB pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The DB pod cast.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        private DBPodCast GetDBPodCast(int? podCastId)
        {
            if (!podCastId.HasValue)
            {
                return null;
            }

            DBPodCast podCast = null;
            try
            {
                podCast =
                    this.DB.Single<DBPodCast>(
                    podCastId);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown SubSonic exceptions
                throw new RepositoryException(
                    "Error trying to get pod cast",
                    ex);
            }

            return podCast;
        }
    }
}