//-------------
// <copyright file="PodCastDownloaderTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.UtilityTests
{
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// Tests for the pod cast downloader.
    /// </summary>
    [TestFixture]
    public class PodCastDownloaderTests
    {
        /// <summary>
        /// The downloader.
        /// </summary>
        private readonly IPodCastDownloader downloader =
            TestApp.PodCastDownloader;

        /// <summary>
        /// Downloads the episode list_ fake_ OK.
        /// </summary>
        [Test]
        public void DownloadEpisodeList_Fake_OK()
        {
            // Arrange:
            PodCast podCast =
                new PodCast(
                1,
                "test",
                new System.Uri("http://fake.uncas.dk"),
                1);

            // Act:
            var episodes =
                this.downloader.DownloadEpisodeList(
                podCast);

            // Assert:
            Assert.IsTrue(podCast.Episodes.Count <= episodes.Count);
        }
    }
}