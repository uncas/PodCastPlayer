//-------------
// <copyright file="PodCastRepositoryTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.RepositoryTests
{
    using System.Diagnostics;
    using System.Linq;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Repository;

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
            = TestApp.Repositories.PodCastRepository;

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
                Trace.WriteLine(podCast.Url);
                Assert.IsNotNull(podCast.Name);
                Assert.IsNotNull(podCast.Url);
            }
        }
    }
}