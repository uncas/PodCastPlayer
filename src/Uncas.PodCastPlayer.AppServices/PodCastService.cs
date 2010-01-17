//-------------
// <copyright file="PodCastService.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.AppServices
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Repository;
    using Utility;
    using ViewModel;

    /// <summary>
    /// Service for pod casts.
    /// </summary>
    public class PodCastService : BaseService
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastService"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        /// <param name="downloader">The downloader.</param>
        /// <exception cref="Uncas.PodCastPlayer.AppServices.ServiceException"></exception>
        public PodCastService(
            IRepositoryFactory repositories,
            IPodCastDownloader downloader)
            : base(
                repositories,
                downloader)
        {
        }

        #endregion

        /// <summary>
        /// Deletes the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public void DeletePodCast(int podCastId)
        {
            this.PodCastRepository.DeletePodCast(podCastId);
        }

        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        /// <returns>A list of pod casts.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        [SuppressMessage(
           "Microsoft.Design",
           "CA1024:UsePropertiesWhereAppropriate",
           Justification = "Reads from repository; might be expensive.")]
        public IList<PodCastIndexViewModel> GetPodCasts()
        {
            return this.PodCastRepository.GetPodCasts();
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public void SavePodCast(
            PodCastDetailsViewModel podCast)
        {
            this.PodCastRepository.SavePodCast(podCast);
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>Details of the pod cast.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public PodCastDetailsViewModel GetPodCast(int? podCastId)
        {
            if (!podCastId.HasValue)
            {
                return null;
            }

            return this.PodCastRepository.GetPodCastDetails(
                podCastId.Value);
        }

        /// <summary>
        /// Creates the pod cast.
        /// </summary>
        /// <param name="podCastUrl">The pod cast URL.</param>
        /// <returns>A view of the new pod cast.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public PodCastNewViewModel CreatePodCast(
            Uri podCastUrl)
        {
            // Gets pod cast info from utility:
            var podCast =
                this.Downloader.DownloadPodCastInfo(
                podCastUrl);

            // If OK, saves to repository:
            int? podCastId = null;
            if (podCast != null)
            {
                this.PodCastRepository.SavePodCast(podCast);
                podCastId = podCast.Id;
            }

            // Returns info:
            return new PodCastNewViewModel
            {
                PodCastId = podCastId,
                PodCastUrl = podCastUrl
            };
        }
    }
}