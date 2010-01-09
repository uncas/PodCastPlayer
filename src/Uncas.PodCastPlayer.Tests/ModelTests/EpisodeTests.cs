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
        /// Updates from other episode_ null media info_ OK.
        /// </summary>
        [Test]
        public void UpdateFromOtherEpisode_NullMediaInfo_OK()
        {
            // Arrange:
            var podCast = new PodCast(
                1,
                "x",
                new Uri("http://sss.dd"),
                "x",
                "x");
            var one = Episode.ConstructEpisode(
                "x",
                DateTime.Now,
                "x",
                "x",
                new Uri("http://www.xxx.dk"),
                podCast,
                false);
            one.MediaInfo = null;
            var other = Episode.ConstructEpisode(
                "x",
                DateTime.Now,
                "x",
                "y",
                new Uri("http://www.xxx.dk"),
                podCast,
                false);

            // Act:
            one.UpdateFromOtherEpisode(other);

            // Assert:
        }
    }
}