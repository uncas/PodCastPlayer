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
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Tests for episode repository.
    /// </summary>
    [TestFixture]
    public class EpisodeRepositoryTests : BaseTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeRepositoryTests"/> class.
        /// </summary>
        public EpisodeRepositoryTests()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeRepositoryTests"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        public EpisodeRepositoryTests(
            IRepositoryFactory repositories)
            : base(repositories)
        {
        }

        #endregion

        #region Public methods (tests)

        /// <summary>
        /// Adds the episode to download list_1_ OK.
        /// </summary>
        [Test]
        public void AddEpisodeToDownloadList_1_OK()
        {
            // Arrange:
            var podCast = new PodCast(
                null,
                "x",
                new Uri("http://ww.ee"),
                "x",
                "x");
            this.PodCastRepository.SavePodCast(podCast);
            var episode = Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "x",
                "x",
                new Uri("http://uu.qq"),
                podCast,
                false);
            var episodes = new List<Episode>();
            episodes.Add(episode);
            this.EpisodeRepository.UpdateEpisodeList(
                podCast.Id.Value,
                episodes);

            // Act:
            this.EpisodeRepository.AddEpisodeToDownloadList(
                podCast.Id.Value,
                episode.Id);

            // Assert:
        }

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
        }

        /// <summary>
        /// Gets the episodes from an unsorted source 
        /// and checks that the result is properly sorted.
        /// </summary>
        [Test]
        public void GetEpisodes_UnsortedSource_CheckSortedResult()
        {
            // Arrange:
            var podCastIndexViewModel =
                this.PodCastRepository.GetPodCasts()
                 .FirstOrDefault();
            int podCastId = 1;
            PodCast podCast = null;
            if (podCastIndexViewModel == null)
            {
                podCast = new PodCast(
                    podCastId,
                    "x",
                    new Uri("http://www.xxx.ddd"),
                    "x",
                    "x");
                this.PodCastRepository.SavePodCast(podCast);
            }
            else
            {
                podCastId =
                    podCastIndexViewModel.Id.Value;
                podCast =
                    this.PodCastRepository.GetPodCast(
                    podCastId);
            }

            var episodes = new List<Episode>();
            var episodeToday =
                Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "x",
                "x",
                new Uri("http://www.xxxx.ddd"),
                podCast,
                false);
            var episodeOneDayAgo =
                Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now.AddDays(-1d),
                "x",
                "x",
                new Uri("http://www.xxxx.ddd"),
                podCast,
                false);
            var episodeOneWeekAgo =
                Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now.AddDays(-7d),
                "x",
                "x",
                new Uri("http://www.xxxx.ddd"),
                podCast,
                false);
            episodes.Add(episodeOneWeekAgo);
            episodes.Add(episodeToday);
            episodes.Add(episodeOneDayAgo);
            this.EpisodeRepository.UpdateEpisodeList(
                podCastId,
                episodes);

            // Act:
            var episodeList =
                this.EpisodeRepository.GetEpisodes(
                podCastId).Episodes.ToList();

            // Assert:
            // Check that the newest episode is first, etc:
            for (int episodeIndex = 0;
                episodeIndex <
                    episodeList.Count() - 1;
                episodeIndex++)
            {
                var thisEpisode =
                    episodeList[episodeIndex];
                var nextEpisode =
                    episodeList[episodeIndex + 1];
                Assert.IsTrue(
                    nextEpisode.Date <=
                    thisEpisode.Date);
            }
        }

        /// <summary>
        /// Gets the episodes to download_ all_ OK.
        /// </summary>
        [Test]
        public void GetEpisodesToDownload_All_OK()
        {
            // Arrange:
            var podCast = new PodCast(
                null,
                "x",
                new Uri("http://xx.dd"),
                "x",
                "x");
            this.PodCastRepository.SavePodCast(podCast);
            var episodes = new List<Episode>();
            episodes.Add(Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "x",
                "x",
                new Uri("http://xxx.ddd"),
                podCast,
                true));
            this.EpisodeRepository.UpdateEpisodeList(
                podCast.Id.Value,
                episodes);

            // Act:
            this.EpisodeRepository.GetEpisodesToDownload();

            // Assert:
        }

        /// <summary>
        /// Gets the download index_ all_ OK.
        /// </summary>
        [Test]
        public void GetDownloadIndex_All_OK()
        {
            // Arrange:

            // Act:
            this.EpisodeRepository.GetDownloadIndex();

            // Assert:
        }

        /// <summary>
        /// Updates the episode_1_ OK.
        /// </summary>
        [Test]
        public void UpdateEpisode_1_OK()
        {
            // Arrange:
            var podCast = new PodCast(
                1,
                "x",
                new Uri("http://xx.ss"),
                "x",
                "x");
            var episode =
                Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "x",
                "x",
                new Uri("http://xx.ss/sss.mp3"),
                podCast,
                false);

            // Act:
            this.EpisodeRepository.UpdateEpisode(episode);

            // Assert:
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
            var podCast = new PodCast(1, "x", new Uri("http://xx.dk"));
            var newEpisodes = new List<Episode>();
            newEpisodes.Add(Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "x",
                "x",
                new Uri("http://sss.ddd"),
                podCast,
                false));

            // Arrange, act and assert:
            this.UpdateEpisodeList(newEpisodes);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Updates the episode list.
        /// </summary>
        /// <param name="newEpisodes">The new episodes.</param>
        private void UpdateEpisodeList(
            List<Episode> newEpisodes)
        {
            // Arrange:
            var podCast =
                this.PodCastRepository.GetPodCasts()
                .FirstOrDefault();
            if (podCast == null)
            {
                return;
            }

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
            Assert.IsTrue(
                newEpisodesCount <=
                updatedEpisodesCount);
        }

        #endregion
    }
}