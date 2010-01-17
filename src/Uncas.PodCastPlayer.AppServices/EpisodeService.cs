//-------------
// <copyright file="EpisodeService.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.AppServices
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Model;
    using Repository;
    using Utility;
    using ViewModel;

    /// <summary>
    /// Service for episodes.
    /// </summary>
    public class EpisodeService : BaseService
    {
        #region Private fields

        /// <summary>
        /// The episode saver.
        /// </summary>
        private readonly IEpisodeSaver saver;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeService"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        /// <param name="downloader">The downloader.</param>
        /// <param name="episodeSaver">The episode saver.</param>
        /// <exception cref="Uncas.PodCastPlayer.AppServices.ServiceException"></exception>
        public EpisodeService(
            IRepositoryFactory repositories,
            IPodCastDownloader downloader,
            IEpisodeSaver episodeSaver)
            : base(repositories, downloader)
        {
            if (episodeSaver == null)
            {
                throw new ServiceException(
                    "Episode saver must be specified.");
            }

            this.saver = episodeSaver;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds the episode to the download list.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodeId">The episode id.</param>
        /// <exception cref="Uncas.PodCastPlayer.AppServices.ServiceException"></exception>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public void AddEpisodeToDownloadList(
            int podCastId,
            string episodeId)
        {
            if (string.IsNullOrEmpty(episodeId))
            {
                throw new ServiceException(
                    "Episode id must be specified.");
            }

            // Adds episode to download list
            // and passes exceptions through.
            this.EpisodeRepository.AddEpisodeToDownloadList(
                podCastId,
                episodeId);
        }

        /// <summary>
        /// Downloads the pending episodes.
        /// </summary>
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        /// <exception cref="Uncas.PodCastPlayer.AppServices.ServiceException"></exception>
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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
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
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
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
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        /// <exception cref="Uncas.PodCastPlayer.AppServices.ServiceException"></exception>
        private void DownloadEpisode(
           Episode episode)
        {
            Debug.Assert(
                episode != null,
                "A non-null episode is required internally.");
            var fileName = episode.FileName;
            var relativeFolderPath =
                Path.Combine(
                "PodCasts",
                episode.PodCast.Name);
            var absoluteFolderPath =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.MyMusic),
                    relativeFolderPath);
            var filePath =
                Path.Combine(
                    absoluteFolderPath,
                    fileName);

            // Gets stream:
            var podCastStream =
                this.Downloader.GetEpisodeStream(
                episode.MediaUrl);

            // Saves stream:
            var downloadedBytes =
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