﻿//-------------
// <copyright file="EpisodeServiceTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.AppServiceTests
{
    using NUnit.Framework;
    using Uncas.PodCastPlayer.AppServices;

    /// <summary>
    /// Tests for the episode service.
    /// </summary>
    [TestFixture]
    public class EpisodeServiceTests
    {
        /// <summary>
        /// The service.
        /// </summary>
        private readonly EpisodeService service =
                new EpisodeService(
                TestApp.Repositories,
                TestApp.PodCastDownloader,
                TestApp.EpisodeSaver);

        /// <summary>
        /// Constructor_s the null_ OK.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ServiceException))]
        public void Constructor_000_OK()
        {
            var episodeService =
                new EpisodeService(
                    null,
                    null,
                    null);
        }

        /// <summary>
        /// Constructor_s the null_ OK.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ServiceException))]
        public void Constructor_110_OK()
        {
            var episodeService =
                new EpisodeService(
                    TestApp.Repositories,
                    TestApp.PodCastDownloader,
                    null);
        }

        /// <summary>
        /// Constructor_s the null_ OK.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ServiceException))]
        public void Constructor_101_OK()
        {
            var episodeService =
                new EpisodeService(
                    TestApp.Repositories,
                    null,
                    TestApp.EpisodeSaver);
        }

        /// <summary>
        /// Adds the episode to download list_1_ OK.
        /// </summary>
        [Test]
        public void AddEpisodeToDownloadList_1x_OK()
        {
            // Arrange:

            // Act:
            this.service.AddEpisodeToDownloadList(1, "x");

            // Assert:
        }

        /// <summary>
        /// Adds the episode to download list_1_ OK.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ServiceException))]
        public void AddEpisodeToDownloadList_1Null_OK()
        {
            // Arrange:

            // Act:
            this.service.AddEpisodeToDownloadList(1, null);

            // Assert:
        }

        /// <summary>
        /// Downloads the pending episodes_ fakes_ OK.
        /// </summary>
        [Test]
        public void DownloadPendingEpisodes_Fakes_OK()
        {
            // Arrange:

            // Act:
            this.service.DownloadPendingEpisodes();

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
            this.service.GetDownloadIndex();

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
            this.service.GetEpisodes(1);

            // Assert:
        }

        /// <summary>
        /// Updates the episodes_1_ OK.
        /// </summary>
        [Test]
        public void UpdateEpisodes_1_OK()
        {
            // Arrange:

            // Act:
            this.service.UpdateEpisodes(1);

            // Assert:
        }
    }
}