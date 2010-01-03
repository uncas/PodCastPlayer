//-------------
// <copyright file="PodCastRepositoryTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests.RepositoryTests
{
    using NUnit.Framework;

    /// <summary>
    /// Testing pod cast repository.
    /// </summary>
    [TestFixture]
    public class PodCastRepositoryTests
    {
        /// <summary>
        /// Gets the pod casts_ all_ OK.
        /// </summary>
        [Test]
        public void GetPodCasts_All_OK()
        {
            // Arrange:

            // Act:
            TestApp.RealRepositories.PodCastRepository
                .GetPodCasts();

            // Assert:
        }
    }
}