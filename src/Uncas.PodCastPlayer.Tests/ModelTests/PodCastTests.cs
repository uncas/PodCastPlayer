//-------------
// <copyright file="PodCastTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.ModelTests
{
    using System;
    using System.Diagnostics;
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

            // Testing:
            var podcast
                = new PodCast(
                    1,
                    name,
                    url);

            // Asserting:
            Assert.AreEqual(name, podcast.Name);
            Assert.AreEqual(url, podcast.Url);
            Trace.Write(podcast.ToString());
        }
    }
}