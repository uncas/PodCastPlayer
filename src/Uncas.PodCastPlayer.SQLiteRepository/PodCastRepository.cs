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
        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastRepository"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        public PodCastRepository(string databasePath)
            : base(databasePath)
        {
        }

        #region IPodCastRepository Members

        /// <summary>
        /// Deletes the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        public void DeletePodCast(int podCastId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        /// <returns>The pod casts.</returns>
        public IList<PodCastIndexViewModel> GetPodCasts()
        {
            var podCasts =
                this.SimpleRepository.All<PodCast>()
                .Select(pc => new PodCastIndexViewModel(
                    pc.PodCastId,
                    pc.Name,
                    new Uri(pc.Url)));

            return podCasts.ToList();
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        public Model.PodCast GetPodCast(int podCastId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        public PodCastDetailsViewModel GetPodCastDetails(
            int podCastId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public void SavePodCast(Model.PodCast podCast)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public void SavePodCast(PodCastDetailsViewModel podCast)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}