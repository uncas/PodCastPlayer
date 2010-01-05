//-------------
// <copyright file="EpisodeRepository.cs" company="Uncas">
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
    internal class EpisodeRepository : BaseRepository,
        IEpisodeRepository
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeRepository"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        public EpisodeRepository(string databasePath)
            : base(databasePath)
        {
        }

        #endregion

        #region IEpisodeRepository Members

        /// <summary>
        /// Adds the episode to the download list.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodeId">The episode id.</param>
        public void AddEpisodeToDownloadList(
            int podCastId,
            string episodeId)
        {
            var episode =
                this.SimpleRepository.Single<DBEpisode>(
                episodeId);
            if (episode == null)
            {
                return;
            }

            episode.PendingDownload = true;
            this.SimpleRepository.Update<DBEpisode>(episode);
        }

        /// <summary>
        /// Gets the episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>An index of episodes.</returns>
        public EpisodeIndexViewModel GetEpisodes(
            int podCastId)
        {
            var podCast =
                this.SimpleRepository.Single<DBPodCast>(
                podCastId);
            if (podCast == null)
            {
                return null;
            }

            var episodes =
                this.SimpleRepository.Find<DBEpisode>(
                e => e.RefPodCastId == podCastId)
                .ToList();
            var episodeIndexItems =
                episodes.Select(
                e => GetViewModelFromDb(e));
            return new EpisodeIndexViewModel
            {
                Episodes = episodeIndexItems,
                PodCastName = podCast.Name
            };
        }

        /// <summary>
        /// Gets the episodes to download.
        /// </summary>
        /// <returns>A list of episodes.</returns>
        public IList<Model.Episode> GetEpisodesToDownload()
        {
            // TODO: Make this work
            var query = from episode in
                            this.SimpleRepository.
                            Find<DBEpisode>(
                            e => e.PendingDownload)
                        join podCast in this.SimpleRepository.All<DBPodCast>()
                        on episode.RefPodCastId equals podCast.PodCastId
                        select new
                        {
                            Episode = episode,
                            PodCast = podCast
                        };
            return query.Select(
                e => GetModelFromDB(e.Episode, e.PodCast))
                .ToList();
        }

        /// <summary>
        /// Gets the view of episodes to download.
        /// </summary>
        /// <returns>An index of the episodes to download.</returns>
        public IEnumerable<DownloadIndexViewModel>
            GetDownloadIndex()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        public void UpdateEpisode(
            Model.Episode episode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the episode list with the new list of episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodes">The updated list of episodes.</param>
        public void UpdateEpisodeList(
            int podCastId,
            IList<Model.Episode> episodes)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Gets the model from DB.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="databasePodCast">The database pod cast.</param>
        /// <returns>The model episode.</returns>
        private static Episode GetModelFromDB(
            DBEpisode episode,
            DBPodCast databasePodCast)
        {
            var podCast = new PodCast(
                (int)databasePodCast.PodCastId,
                databasePodCast.Name,
                new Uri(databasePodCast.Url),
                databasePodCast.Description,
                databasePodCast.Author);
            return Episode.ConstructEpisode(
                episode.EpisodeId,
                episode.Date,
                episode.Title,
                episode.Description,
                podCast);
        }

        /// <summary>
        /// Gets the view model from db.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>The episode index item view model.</returns>
        private static EpisodeIndexItemViewModel
            GetViewModelFromDb(
            DBEpisode episode)
        {
            bool downloadCompleted =
                Model.EpisodeMediaInfo.IsDownloadCompleted(
                episode.FileSizeInBytes,
                episode.DownloadedBytes);
            return new EpisodeIndexItemViewModel(
                episode.Date,
                episode.EpisodeId,
                episode.Title,
                episode.PendingDownload,
                downloadCompleted);
        }
    }
}