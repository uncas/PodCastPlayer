//-------------
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
                TestApp.PodCastDownloader);

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
    }
}