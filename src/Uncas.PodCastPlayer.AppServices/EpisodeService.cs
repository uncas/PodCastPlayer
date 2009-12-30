//-------------
// <copyright file="EpisodeService.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.AppServices
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
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

        #region Public methods

        /// <summary>
        /// Downloads the pending episodes.
        /// </summary>
        public void DownloadPendingEpisodes()
        {
            while (true)
            {
                var episodesToDownload =
                    this.EpisodeRepository.GetEpisodesToDownload();
                if (episodesToDownload.Count == 0)
                {
                    break;
                }

                var episode = episodesToDownload.First();

                // TODO: FEATURE: Implement proper file path:
                string fileName =
                    string.Format(
                    CultureInfo.InvariantCulture,
                    "episode{0}.mp3",
                    episode.Id);
                string filePath =
                    Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.MyDocuments),
                        fileName);
                var mediaInfo =
                    this.Downloader.DownloadEpisode(
                    episode,
                    filePath);

                episode.MediaInfo = mediaInfo;
                episode.PendingDownload =
                    !mediaInfo.DownloadCompleted;

                // TODO: FEATURE: Only store relative path of file name:
                episode.FileName = filePath;
                this.EpisodeRepository.UpdateEpisode(episode);
            }
        }

        /// <summary>
        /// Gets an index of the episodes to download.
        /// </summary>
        /// <returns>An index of the episodes to download.</returns>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "This is an expensive read.")]
        public IEnumerable<DownloadIndexViewModel> GetDownloadIndex()
        {
            return this.EpisodeRepository.GetDownloadIndex();
        }

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

        #endregion
    }
}