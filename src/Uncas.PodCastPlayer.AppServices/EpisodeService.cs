﻿//-------------
// <copyright file="EpisodeService.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.AppServices
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.Utility;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Service for episodes.
    /// </summary>
    public class EpisodeService : BaseService
    {
        /// <summary>
        /// The episode saver.
        /// </summary>
        private readonly IEpisodeSaver saver;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeService"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        /// <param name="downloader">The downloader.</param>
        /// <param name="episodeSaver">The episode saver.</param>
        public EpisodeService(
            IRepositoryFactory repositories,
            IPodCastDownloader downloader,
            IEpisodeSaver episodeSaver)
            : base(repositories, downloader)
        {
            this.saver = episodeSaver;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds the episode to the download list.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodeId">The episode id.</param>
        public void AddEpisodeToDownloadList(
            int podCastId,
            string episodeId)
        {
            this.EpisodeRepository.AddEpisodeToDownloadList(
                podCastId,
                episodeId);
        }

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

                foreach (var episode in episodesToDownload)
                {
                    this.DownloadEpisode(episode);
                }
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

        #region Private methods

        /// <summary>
        /// Downloads the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        private void DownloadEpisode(
           Episode episode)
        {
            string fileName = episode.FileName;
            string relativeFolderPath =
                Path.Combine(
                "PodCasts",
                episode.PodCast.Name);
            string absoluteFolderPath =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.MyMusic),
                    relativeFolderPath);
            string filePath =
                Path.Combine(
                    absoluteFolderPath,
                    fileName);

            // Gets stream:
            EpisodeMedia podCastStream =
                this.Downloader.GetEpisodeStream(
                episode.MediaUrl);

            // Saves stream:
            long downloadedBytes =
                this.saver.SaveStream(
                filePath,
                podCastStream.Length,
                podCastStream.Stream);

            // Returns info about how it all went:
            var mediaInfo =
                new EpisodeMediaInfo
                {
                    FileSizeInBytes = podCastStream.Length,
                    DownloadedBytes = downloadedBytes
                };

            episode.MediaInfo = mediaInfo;
            episode.PendingDownload =
                !mediaInfo.DownloadCompleted;

            this.EpisodeRepository.UpdateEpisode(episode);
        }

        #endregion
    }
}