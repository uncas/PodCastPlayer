//-------------
// <copyright file="IEpisodeRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Repository
{
    using System.Collections.Generic;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Interface for handling storage of episode info.
    /// </summary>
    public interface IEpisodeRepository
    {
        /// <summary>
        /// Gets the episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>An index of episodes.</returns>
        EpisodeIndexViewModel GetEpisodes(
            int podCastId);

        /// <summary>
        /// Updates the episode list.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodes">The episodes.</param>
        void UpdateEpisodeList(
            int podCastId,
            IList<Episode> episodes);
    }
}