//-------------
// <copyright file="BackgroundDownloaderTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.UITests
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
        /// Start_s the normal_ OK.
        /// </summary>
        [Test]
        public void Start_Normal_OK()
        {
            // Arrange
            BackgroundDownloader downloader =
                new BackgroundDownloader();

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
                new BackgroundDownloader();
            downloader.Start();
            Thread.Sleep(100);

            // Act
            downloader.Stop();

            // Assert
            Thread.Sleep(1000);
        }
    }
}