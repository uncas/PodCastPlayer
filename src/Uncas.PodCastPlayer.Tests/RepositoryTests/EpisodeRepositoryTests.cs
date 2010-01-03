//-------------
// <copyright file="EpisodeRepositoryTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.RepositoryTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Tests for episode repository.
    /// </summary>
    [TestFixture]
    public class EpisodeRepositoryTests : BaseTest
    {
        /// <summary>
        /// Gets the episodes_1_ OK.
        /// </summary>
        [Test]
        public void GetEpisodes_1_OK()
        {
            // Arrange:

            // Act:
            var episodeIndex =
                this.EpisodeRepository.GetEpisodes(1);

            // Assert:
            Assert.IsNotNull(episodeIndex);
        }

        /// <summary>
        /// Tests the method to update the episode list.
        /// </summary>
        [Test]
        public void UpdateEpisodeList_0New_OK()
        {
            // Arrange:
            var newEpisodes = new List<Episode>();

            // Arrange, act and assert:
            this.UpdateEpisodeList(newEpisodes);
        }

        /// <summary>
        /// Tests the method to update the episode list.
        /// </summary>
        [Test]
        public void UpdateEpisodeList_1New_OK()
        {
            // Arrange:
            var podCast = new PodCast(1, "x", new Uri("http://xx.dk"), 3);
            var newEpisodes = new List<Episode>();
            newEpisodes.Add(Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "x",
                "x",
                podCast));

            // Arrange, act and assert:
            this.UpdateEpisodeList(newEpisodes);
        }

        /// <summary>
        /// Updates the episode list.
        /// </summary>
        /// <param name="newEpisodes">The new episodes.</param>
        private void UpdateEpisodeList(List<Episode> newEpisodes)
        {
            // Arrange:
            var podCast =
                this.PodCastRepository.GetPodCasts()
                .FirstOrDefault();
            int podCastId = podCast.Id.Value;
            var originalEpisodes =
                this.EpisodeRepository.GetEpisodes(
                podCastId);
            int originalEpisodesCount =
                originalEpisodes.Episodes.Count();
            int newEpisodesCount =
                newEpisodes.Count();

            // Act:
            this.EpisodeRepository.UpdateEpisodeList(
                podCastId,
                newEpisodes);

            // Assert:
            var updatedEpisodes =
                this.EpisodeRepository.GetEpisodes(
                podCastId);
            int updatedEpisodesCount =
                updatedEpisodes.Episodes.Count();
            Trace.WriteLine(
               "Original episodes: " +
               originalEpisodesCount);
            Trace.WriteLine(
                "New episodes: " +
                newEpisodesCount);
            Trace.WriteLine(
                "Updated episodes: " +
                updatedEpisodesCount);
            Assert.IsTrue(
                originalEpisodesCount <=
                updatedEpisodesCount);
        }
    }
}