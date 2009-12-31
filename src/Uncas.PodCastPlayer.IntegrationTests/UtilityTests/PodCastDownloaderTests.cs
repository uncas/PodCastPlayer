//-------------
// <copyright file="PodCastDownloaderTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests.UtilityTests
{
    using System;
    using System.Diagnostics;
    using System.IO;
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
        /// Downloads an episode from hanselminutes.
        /// </summary>
        [Test]
        public void DownloadEpisode_Hanselminutes_OK()
        {
            // Arrange:
            Uri url = new Uri("http://perseus.franklins.net/hanselminutes_0079.mp3");

            Episode episode = new Episode
            {
                MediaUrl = url
            };
            string fileName = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "test1.mp3");

            // Act:
            this.downloader.DownloadEpisode(
                episode,
                fileName);

            // Assert:
        }

        /// <summary>
        /// Downloads the episode list_ hanselminutes_ OK.
        /// </summary>
        [Test]
        public void DownloadEpisodeList_Hanselminutes_OK()
        {
            // Arrange:
            var podCast =
                new PodCast(
                    1,
                    "hanselminutes",
                    new Uri("http://feeds.feedburner.com/HanselminutesCompleteMP3"),
                    1);

            // Act:
            var episodes =
                this.downloader.DownloadEpisodeList(
                podCast);

            // Assert:
            Assert.IsTrue(0 < episodes.Count);
            foreach (var episode in episodes)
            {
                Trace.WriteLine(episode.ToString());
                Trace.WriteLine("---");
            }
        }
    }
}