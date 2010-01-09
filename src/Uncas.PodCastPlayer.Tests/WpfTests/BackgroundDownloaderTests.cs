//-------------
// <copyright file="BackgroundDownloaderTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.WpfTests
{
    using System.Threading;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Wpf;

    /// <summary>
    /// Tests for the background downloader.
    /// </summary>
    [TestFixture]
    public class BackgroundDownloaderTests
    {
        /// <summary>
        /// Dispose_s the fake_ OK.
        /// </summary>
        [Test]
        public void Dispose_Fake_OK()
        {
            // Arrange:
            BackgroundDownloader downloader =
                new BackgroundDownloader(
                    TestApp.Repositories,
                    TestApp.PodCastDownloader);

            // Act:
            downloader.Dispose();

            // Assert:
        }

        /// <summary>
        /// Start_s the normal_ OK.
        /// </summary>
        [Test]
        public void Start_Normal_OK()
        {
            // Arrange
            BackgroundDownloader downloader =
                new BackgroundDownloader(
                    TestApp.Repositories,
                    TestApp.PodCastDownloader);

            // Act
            downloader.Start();

            // Assert
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Stop_s the normal_ OK.
        /// </summary>
        [Test]
        public void Stop_Normal_OK()
        {
            // Arrange
            BackgroundDownloader downloader =
                new BackgroundDownloader(
                    TestApp.Repositories,
                    TestApp.PodCastDownloader);
            downloader.Start();
            Thread.Sleep(100);

            // Act
            downloader.Stop();

            // Assert
            Thread.Sleep(1000);
        }
    }
}