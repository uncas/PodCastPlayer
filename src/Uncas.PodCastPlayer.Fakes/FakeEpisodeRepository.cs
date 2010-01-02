//-------------
// <copyright file="FakeEpisodeRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Fakes
{
    using System.Collections.Generic;
    using System.Linq;
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
            var podCast =
                FakePodCastRepository.PodCasts.Where(
                pc => pc.Id == podCastId)
                .SingleOrDefault();
            if (podCast == null)
            {
                return;
            }

            foreach (var episode in episodes)
            {
                var existingEpisode =
                    podCast.Episodes.Where(
                        e => e.Id.Equals(episode.Id))
                    .SingleOrDefault();
                if (existingEpisode != null)
                {
                    existingEpisode.UpdateFromOtherEpisode(
                        episode);
                }
                else
                {
                    podCast.Episodes.Add(
                        episode);
                }
            }
        }

        /// <summary>
        /// Gets the episodes to download.
        /// </summary>
        /// <returns>A list of episodes.</returns>
        public IList<Episode> GetEpisodesToDownload()
        {
            var result = new List<Episode>();
            foreach (var podCast in FakePodCastRepository.PodCasts)
            {
                result.AddRange(
                    podCast.Episodes.Where(
                        e => e.PendingDownload));
            }

            return result;
        }

        /// <summary>
        /// Updates the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        public void UpdateEpisode(Episode episode)
        {
        }

        /// <summary>
        /// Gets the view of episodes to download.
        /// </summary>
        /// <returns>An index of the episodes to download.</returns>
        public IEnumerable<DownloadIndexViewModel> GetDownloadIndex()
        {
            return this.GetEpisodesToDownload()
                .Select(e => new DownloadIndexViewModel
                {
                    EpisodeDate = e.Date,
                    EpisodeId = e.Id,
                    PodCastName = e.PodCast.Name
                });
        }

        #endregion
    }
}