//-------------
// <copyright file="PodCastTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.ModelTests
{
    using System;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Tests pod cast object.
    /// </summary>
    [TestFixture]
    public class PodCastTests
    {
        /// <summary>
        /// Pods the cast0.
        /// </summary>
        [Test]
        public void PodCast0()
        {
            string name = "Hanselminutes";
            Uri url
                = new Uri(
                    "http://feeds.feedburner.com/HanselminutesCompleteMP3");
            int? numberToDownload = 3;

            // Testing:
            var podcast
                = new PodCast(
                    1,
                    name,
                    url,
                    numberToDownload);

            // Asserting:
            Assert.AreEqual(name, podcast.Name);
            Assert.AreEqual(url, podcast.Url);
            Assert.AreEqual(
                numberToDownload,
                podcast.NumberToDownload);
        }
    }
}