//-------------
// <copyright file="EpisodeRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Model;
    using Repository;
    using ViewModel;

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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public void AddEpisodeToDownloadList(
            int podCastId,
            string episodeId)
        {
            DBEpisode episode;
            try
            {
                episode =
                    this.DB.Single<DBEpisode>(
                    episodeId);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown exceptions from SubSonic
                throw new RepositoryException(
                    "Failed to read episode from database",
                    ex);
            }

            if (episode == null)
            {
                return;
            }

            episode.PendingDownload = true;
            try
            {
                this.DB.Update<DBEpisode>(episode);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown exceptions from SubSonic
                throw new RepositoryException(
                    "Failed to update episode in database",
                    ex);
            }
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
            DBPodCast podCast;
            try
            {
                podCast =
                    this.DB.Single<DBPodCast>(
                    podCastId);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown exceptions from third-party SubSonic...
                throw new RepositoryException(
                    "Error fetching pod cast info from database",
                    ex);
            }

            if (podCast == null)
            {
                return null;
            }

            IEnumerable<DBEpisode> episodes;
            try
            {
                episodes =
                    this.DB.Find<DBEpisode>(
                        e => e.RefPodCastId == podCastId)
                    .ToList()
                    .OrderByDescending(e => e.Date);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown exceptions from third-party SubSonic...
                throw new RepositoryException(
                    "Error fetching episode info from database",
                    ex);
            }

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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public IList<Episode> GetEpisodesToDownload()
        {
            IList<DBEpisode> episodes;
            IList<DBPodCast> podCasts;
            try
            {
                episodes =
                    this.DB.All<DBEpisode>()
                    .Where(e => e.PendingDownload)
                    .ToList();
                podCasts =
                    this.DB.All<DBPodCast>().ToList();
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown exceptions from third-party SubSonic...
                throw new RepositoryException(
                    "Error fetching data from database",
                    ex);
            }

            if (episodes.Count == 0
                || podCasts.Count == 0)
            {
                return new List<Episode>();
            }

            return episodes.Select(
                e => e.AsModel(podCasts))
                .ToList();
        }

        /// <summary>
        /// Gets the view of episodes to download.
        /// </summary>
        /// <returns>An index of the episodes to download.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public IEnumerable<DownloadIndexViewModel>
            GetDownloadIndex()
        {
            return this.GetEpisodesToDownload()
                .Select(e =>
                    DownloadIndexViewModel.FromEpisode(e));
        }

        /// <summary>
        /// Updates the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public void UpdateEpisode(
            Episode episode)
        {
            DBEpisode e;
            try
            {
                e = this.DB.Single<DBEpisode>(
                    episode.Id);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown SubSonic exception
                throw new RepositoryException(
                    "Error retrieving episode",
                    ex);
            }

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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "SubSonic exceptions not known")]
        public void UpdateEpisodeList(
            int podCastId,
            IList<Episode> episodes)
        {
            IQueryable<DBEpisode> oldEpisodes;
            try
            {
                oldEpisodes =
                    this.DB.All<DBEpisode>()
                    .Where(e => e.RefPodCastId == podCastId);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown SubSonic exception
                throw new RepositoryException(
                    "Error reading existing episode list",
                    ex);
            }

            IList<Exception> exceptions = new List<Exception>();
            foreach (var episode in episodes)
            {
                var oldEpisode =
                    oldEpisodes.Where(
                    e => e.EpisodeId == episode.Id)
                    .SingleOrDefault();
                if (oldEpisode == null)
                {
                    try
                    {
                        // Inserts new episode:
                        this.DB.Add<DBEpisode>(
                            episode.AsDB());
                    }
                    catch (Exception ex)
                    {
                        // TODO: EXCEPTION: Unknown SubSonic exception
                        exceptions.Add(ex);
                    }
                }
                else
                {
                    try
                    {
                        // Updates old episode:
                        this.UpdateDBEpisode(episode, oldEpisode);
                    }
                    catch (RepositoryException ex)
                    {
                        exceptions.Add(ex);
                    }
                }
            }

            if (exceptions.Count > 0)
            {
                string message =
                    string.Format(
                    CultureInfo.CurrentCulture,
                    "Errors during update of episode list: See Data property for all {0} inner exceptions",
                    exceptions.Count);
                var exception =
                    new RepositoryException(
                    message,
                    exceptions.First());
                exception.Data.Add(
                    "Inner exceptions",
                    exceptions);
                throw exception;
            }
        }

        #endregion

        /// <summary>
        /// Updates the DB episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="e">The db episode.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        private void UpdateDBEpisode(
            Episode episode,
            DBEpisode e)
        {
            e.Date = episode.Date;
            e.Description = episode.Description;
            e.DownloadedBytes =
                episode.DownloadedBytes;
            e.FileName = episode.FileName;
            e.FileSizeInBytes =
                episode.FileSizeInBytes;
            e.MediaUrl = episode.MediaUrl.ToString();
            e.PendingDownload =
                episode.PendingDownload;
            e.Title = episode.Title;

            try
            {
                this.DB.Update<DBEpisode>(e);
            }
            catch (Exception ex)
            {
                // TODO: EXCEPTION: Unknown SubSonic exception
                throw new RepositoryException(
                    "Error updating episode",
                    ex);
            }
        }
    }
}