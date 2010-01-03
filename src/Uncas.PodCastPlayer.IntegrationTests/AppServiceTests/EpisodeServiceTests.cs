//-------------
// <copyright file="EpisodeServiceTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests.AppServiceTests
{
    using NUnit.Framework;
    using Uncas.PodCastPlayer.AppServices;

    /// <summary>
    /// Tests the episode service with the real implementations.
    /// </summary>
    [TestFixture]
    public class EpisodeServiceTests
    {
        /// <summary>
        /// The episode service.
        /// </summary>
        private readonly EpisodeService service =
            new EpisodeService(
                TestApp.FakeRepositories,
                TestApp.PodCastDownloader);

        /// <summary>
        /// Downloads the pending episodes.
        /// </summary>
        [Test]
        [Ignore]
        public void DownloadPendingEpisodes_All_OK()
        {
            // Arrange:

            // Act:
            this.service.DownloadPendingEpisodes();

            // Assert:
        }
    }
}