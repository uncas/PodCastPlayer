//-------------
// <copyright file="PodCastDownloaderTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests.UtilityTests
{
    using System;
    using System.Collections.Generic;
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

        [Test]
        public void Rss()
        {
            var items = new List<RssItem>();
            items.Add(
                new RssItem
                {
                    Guid = "x",
                    PubDate = DateTime.Now,
                    Title = "test x",
                });
            RssFeed feed = new RssFeed
            {
                Channel = new RssChannel { Items = items }
            };

            StringWriter sw = new StringWriter();
            feed.Save(sw);
            Trace.WriteLine(sw);
        }
    }
}