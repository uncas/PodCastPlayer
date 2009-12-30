//-------------
// <copyright file="FakeEpisodeRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Fakes
{
    using System.Collections.Generic;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Fake episode repository.
    /// </summary>
    internal class FakeEpisodeRepository : IEpisodeRepository
    {
        #region IEpisodeRepository Members

        /// <summary>
        /// Gets the episodes.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>An index of episodes.</returns>
        public EpisodeIndexViewModel GetEpisodes(
            int podCastId)
        {
            return FakePodCastRepository.GetEpisodesByPodCast(
                podCastId);
        }

        /// <summary>
        /// Updates the episode list.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodes">The episodes.</param>
        public void UpdateEpisodeList(
            int podCastId,
            IList<Episode> episodes)
        {
            FakePodCastRepository.UpdateEpisodeList(
                podCastId,
                episodes);
        }

        #endregion
    }
}