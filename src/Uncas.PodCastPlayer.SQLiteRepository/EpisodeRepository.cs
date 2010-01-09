//-------------
// <copyright file="EpisodeRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
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
                this.DB.Single<DBEpisode>(
                episodeId);
            if (episode == null)
            {
                return;
            }

            episode.PendingDownload = true;
            this.DB.Update<DBEpisode>(episode);
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
                this.DB.Single<DBPodCast>(
                podCastId);
            if (podCast == null)
            {
                return null;
            }

            var episodes =
                this.DB.Find<DBEpisode>(
                e => e.RefPodCastId == podCastId)
                .ToList();
            var episodeIndexItems =
                episodes.Select(
                e => e.AsIndexItem());
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
        public IList<Episode> GetEpisodesToDownload()
        {
            var episodes =
                this.DB.All<DBEpisode>()
                .Where(e => e.PendingDownload)
                .ToList();
            var podCasts =
                this.DB.All<DBPodCast>().ToList();
            return episodes.Select(
                e => e.AsModel(podCasts))
                .ToList();
        }

        /// <summary>
        /// Gets the view of episodes to download.
        /// </summary>
        /// <returns>An index of the episodes to download.</returns>
        public IEnumerable<DownloadIndexViewModel>
            GetDownloadIndex()
        {
            return this.GetEpisodesToDownload()
                .Select(e => new DownloadIndexViewModel
                {
                    EpisodeDate = e.Date,
                    EpisodeId = e.Id,
                    EpisodeTitle = e.Title,
                    PodCastId = e.PodCast.Id.Value,
                    PodCastName = e.PodCast.Name
                });
        }

        /// <summary>
        /// Updates the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        public void UpdateEpisode(
            Episode episode)
        {
            DBEpisode e =
                this.DB.Single<DBEpisode>(
                episode.Id);
            if (e == null)
            {
                return;
            }

            this.UpdateDBEpisode(episode, e);
        }

        /// <summary>
        /// Updates the episode list with the new list of episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodes">The updated list of episodes.</param>
        public void UpdateEpisodeList(
            int podCastId,
            IList<Episode> episodes)
        {
            var oldEpisodes = this.DB.All<DBEpisode>()
                .Where(e => e.RefPodCastId == podCastId);

            foreach (var episode in episodes)
            {
                var oldEpisode =
                    oldEpisodes.Where(
                    e => e.EpisodeId == episode.Id)
                    .SingleOrDefault();
                if (oldEpisode == null)
                {
                    // Inserts new episode:
                    this.DB.Add<DBEpisode>(
                        episode.AsDB());
                }
                else
                {
                    // Updates old episode:
                    this.UpdateDBEpisode(episode, oldEpisode);
                }
            }
        }

        #endregion

        /// <summary>
        /// Updates the DB episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="e">The db episode.</param>
        private void UpdateDBEpisode(
            Episode episode,
            DBEpisode e)
        {
            e.Date = episode.Date;
            e.Description = episode.Description;
            e.DownloadedBytes =
                episode.MediaInfo.DownloadedBytes;
            e.FileName = episode.FileName;
            e.FileSizeInBytes =
                episode.MediaInfo.FileSizeInBytes;
            e.MediaUrl = episode.MediaUrl.ToString();
            e.PendingDownload =
                episode.PendingDownload;
            e.Title = episode.Title;

            this.DB.Update<DBEpisode>(e);
        }
    }
}