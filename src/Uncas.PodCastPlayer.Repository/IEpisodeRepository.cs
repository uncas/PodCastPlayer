//-------------
// <copyright file="IEpisodeRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Repository
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Interface for handling storage of episode info.
    /// </summary>
    public interface IEpisodeRepository
    {
        /// <summary>
        /// Adds the episode to the download list.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodeId">The episode id.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        void AddEpisodeToDownloadList(
            int podCastId,
            string episodeId);

        /// <summary>
        /// Gets the episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>An index of episodes.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        EpisodeIndexViewModel GetEpisodes(
            int podCastId);

        /// <summary>
        /// Gets the episodes to download.
        /// </summary>
        /// <returns>A list of episodes.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "This is an expensive read.")]
        IList<Episode> GetEpisodesToDownload();

        /// <summary>
        /// Gets the view of episodes to download.
        /// </summary>
        /// <returns>An index of the episodes to download.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "This is an expensive read.")]
        IEnumerable<DownloadIndexViewModel> GetDownloadIndex();

        /// <summary>
        /// Updates the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        void UpdateEpisode(Episode episode);

        /// <summary>
        /// Updates the episode list with the new list of episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodes">The updated list of episodes.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        void UpdateEpisodeList(
            int podCastId,
            IList<Episode> episodes);
    }
}