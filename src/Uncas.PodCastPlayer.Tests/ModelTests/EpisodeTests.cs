//-------------
// <copyright file="EpisodeTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.ModelTests
{
    using System;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Tests the episode class.
    /// </summary>
    [TestFixture]
    public class EpisodeTests
    {
        /// <summary>
        /// Constructor_s the null media url_ exception.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ModelException))]
        public void Constructor_NullMediaUrl_Exception()
        {
            // Arrange:

            // Act:
            var episode = Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "x",
                "x",
                null,
                null,
                false);

            // Assert:
        }

        /// <summary>
        /// Files the size in bytes_ null media info_ OK.
        /// </summary>
        [Test]
        public void FileSizeInBytes_NullMediaInfo_OK()
        {
            // Arrange:
            var episode = GetEpisode();
            episode.MediaInfo = null;

            // Act:
            var fileSizeInBytes = episode.FileSizeInBytes;
            var downloadedBytes = episode.DownloadedBytes;

            // Assert:
            Assert.AreEqual(0, fileSizeInBytes);
            Assert.AreEqual(0, downloadedBytes);
        }

        /// <summary>
        /// Updates from other episode_ null media info_ OK.
        /// </summary>
        [Test]
        public void UpdateFromOtherEpisode_NullMediaInfo_OK()
        {
            // Arrange:
            var podCast = GetPodCast();
            var one = GetEpisode(podCast);
            one.MediaInfo = null;
            var other = GetEpisode(podCast);

            // Act:
            one.UpdateFromOtherEpisode(other);

            // Assert:
        }

        /// <summary>
        /// Updates from other episode_ null_ OK.
        /// </summary>
        [Test]
        public void UpdateFromOtherEpisode_Null_OK()
        {
            // Arrange:
            var one = GetEpisode();

            // Act:
            one.UpdateFromOtherEpisode(null);

            // Assert:
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <returns>The pod cast.</returns>
        private static PodCast GetPodCast()
        {
            var podCast = new PodCast(
                1,
                "x",
                new Uri("http://sss.dd"),
                "x",
                "x");
            return podCast;
        }

        /// <summary>
        /// Gets the episode.
        /// </summary>
        /// <returns>The episode.</returns>
        private static Episode GetEpisode()
        {
            return GetEpisode(GetPodCast());
        }

        /// <summary>
        /// Gets the episode.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <returns>The episode.</returns>
        private static Episode GetEpisode(PodCast podCast)
        {
            var one = Episode.ConstructEpisode(
                "x",
                DateTime.Now,
                "x",
                "x",
                new Uri("http://www.xxx.dk"),
                podCast,
                false);
            return one;
        }
    }
}