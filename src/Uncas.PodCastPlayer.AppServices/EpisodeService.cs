//-------------
// <copyright file="EpisodeService.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.AppServices
{
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.Utility;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Service for episodes.
    /// </summary>
    public class EpisodeService : BaseService
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeService"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        /// <param name="downloader">The downloader.</param>
        public EpisodeService(
            IRepositoryFactory repositories,
            IPodCastDownloader downloader)
            : base(repositories, downloader)
        {
        }

        #endregion

        /// <summary>
        /// Gets the episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>An index of episodes.</returns>
        public EpisodeIndexViewModel GetEpisodes(
            int podCastId)
        {
            return this.EpisodeRepository.GetEpisodes(
                podCastId);
        }

        /// <summary>
        /// Updates the episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        public void UpdateEpisodes(int podCastId)
        {
            // Gets pod cast info:
            var podCast =
                this.PodCastRepository.GetPodCast(
                podCastId);

            // Gets updated info from utility:
            var episodes =
                this.Downloader.DownloadEpisodeList(
                podCast);

            // Saves to repository:
            this.EpisodeRepository.UpdateEpisodeList(
                podCastId,
                episodes);
        }
    }
}