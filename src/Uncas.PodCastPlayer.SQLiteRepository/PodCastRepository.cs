//-------------
// <copyright file="PodCastRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System.Collections.Generic;
    using System.Linq;
    using SubSonic.DataProviders;
    using SubSonic.Repository;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Pod cast repository implemented with SQLite.
    /// </summary>
    internal class PodCastRepository : IPodCastRepository
    {
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
            var provider =
                ProviderFactory.GetProvider(
                @"Data Source=C:\PodCastPlayer.db",
                "System.Data.SQLite");
            var repo =
                new SimpleRepository(
                    provider,
                    SimpleRepositoryOptions.RunMigrations);

            var podCasts = repo.All<DBPodCast>()
                .Select(pc => new PodCastIndexViewModel(
                    pc.PodCastId,
                    pc.Name,
                    pc.Url));

            return podCasts.ToList();
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        public PodCast GetPodCast(int podCastId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        public PodCastDetailsViewModel GetPodCastDetails(int podCastId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public void SavePodCast(PodCast podCast)
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