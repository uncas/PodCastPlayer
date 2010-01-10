//-------------
// <copyright file="PodCastDownloaderTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests.UtilityTests
{
    using System;
    using System.Diagnostics;
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
        /// Gets stream from a hanselminutes episode.
        /// </summary>
        [Test]
        public void GetEpisodeStream_Hanselminutes_OK()
        {
            // Arrange:
            Uri url =
                new Uri(
                    "http://perseus.franklins.net/hanselminutes_0079.mp3");

            // Act:
            var result =
                this.downloader.GetEpisodeStream(
                url);

            // Assert:
            Trace.WriteLine(result.Length);
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
                    GetUri());

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

        /// <summary>
        /// Downloads the pod cast info_ hanselminutes_ OK.
        /// </summary>
        [Test]
        public void DownloadPodCastInfo_Hanselminutes_OK()
        {
            // Arrange:

            // Act:
            PodCast podCast =
                this.downloader.DownloadPodCastInfo(
                GetUri());

            // Assert:
            Trace.WriteLine(podCast);
        }

        /// <summary>
        /// Downloads the pod cast info_ null uri_ OK.
        /// </summary>
        [Test]
        public void DownloadPodCastInfo_NullUri_OK()
        {
            // Arrange:

            // Act:
            this.downloader.DownloadPodCastInfo(null);

            // Assert:
        }

        /// <summary>
        /// Downloads the pod cast info_ false uri_ OK.
        /// </summary>
        [Test]
        [ExpectedException(typeof(UtilityException))]
        public void DownloadPodCastInfo_FalseUri_OK()
        {
            // Arrange:

            // Act:
            this.downloader.DownloadPodCastInfo(
                new Uri(
                    "http://www.xxxxx.dddddd"));

            // Assert:
        }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <returns>The uri of a test pod cast.</returns>
        private static Uri GetUri()
        {
            return new Uri(
                "http://feeds.feedburner.com/HanselminutesCompleteMP3");
        }
    }
}