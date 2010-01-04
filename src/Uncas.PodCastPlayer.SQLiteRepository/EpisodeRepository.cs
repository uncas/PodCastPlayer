//-------------
// <copyright file="EpisodeRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System;
    using System.Collections.Generic;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;
    using Model = Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Pod cast repository implemented with SQLite.
    /// </summary>
    internal class EpisodeRepository : BaseRepository,
        IEpisodeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeRepository"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        public EpisodeRepository(string databasePath)
            : base(databasePath)
        {
        }

        #region IEpisodeRepository Members

        /// <summary>
        /// Adds the episode to the download list.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodeId">The episode id.</param>
        public void AddEpisodeToDownloadList(int podCastId, string episodeId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>An index of episodes.</returns>
        public EpisodeIndexViewModel GetEpisodes(int podCastId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the episodes to download.
        /// </summary>
        /// <returns>A list of episodes.</returns>
        public IList<Model.Episode> GetEpisodesToDownload()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the view of episodes to download.
        /// </summary>
        /// <returns>An index of the episodes to download.</returns>
        public IEnumerable<DownloadIndexViewModel> GetDownloadIndex()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        public void UpdateEpisode(Model.Episode episode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the episode list with the new list of episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodes">The updated list of episodes.</param>
        public void UpdateEpisodeList(int podCastId, IList<Model.Episode> episodes)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}