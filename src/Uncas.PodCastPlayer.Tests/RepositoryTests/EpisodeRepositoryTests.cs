//-------------
// <copyright file="EpisodeRepositoryTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.RepositoryTests
{
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Tests for episode repository.
    /// </summary>
    [TestFixture]
    public class EpisodeRepositoryTests
    {
        /// <summary>
        /// The episode repository.
        /// </summary>
        private readonly IEpisodeRepository repository
            = TestApp.Repositories.EpisodeRepository;

        /// <summary>
        /// Gets the episodes_1_ OK.
        /// </summary>
        [Test]
        public void GetEpisodes_1_OK()
        {
            // Arrange:

            // Act:
            var episodeIndex = this.repository.GetEpisodes(1);

            // Assert:
            Assert.IsNotNull(episodeIndex);
        }
    }
}
