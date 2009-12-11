//-------------
// <copyright file="PodCastRepositoryTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.RepositoryTests
{
    using System.Diagnostics;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.Tests.Fakes;

    /// <summary>
    /// Tests pod cast repository.
    /// </summary>
    [TestFixture]
    public class PodCastRepositoryTests
    {
        /// <summary>
        /// The pod cast repository.
        /// </summary>
        private readonly IPodCastRepository repository
            = new FakePodCastRepository();

        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        [Test]
        public void GetPodCasts_All_OK()
        {
            // Testing:
            var podCasts
                = this.repository.GetPodCasts();

            foreach (var podCast in podCasts)
            {
                Trace.WriteLine(podCast.Name);
            }
        }

        /// <summary>
        /// Saves the pod cast_ one_ success.
        /// </summary>
        [Test]
        public void SavePodCast_One_Success()
        {
            // Setting up:
            var podCast = new PodCast(
                "A",
                null,
                null);
            int numberOfPodCasts =
                this.repository.GetPodCasts().Count;

            // Testing:
            this.repository.SavePodCast(podCast);

            // Asserting:
            Assert.AreEqual(
                numberOfPodCasts + 1,
                this.repository.GetPodCasts().Count);
        }
    }
}