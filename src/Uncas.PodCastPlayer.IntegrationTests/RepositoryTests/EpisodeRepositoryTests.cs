//-------------
// <copyright file="EpisodeRepositoryTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests.RepositoryTests
{
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Testing pod cast repository.
    /// </summary>
    [TestFixture]
    public class EpisodeRepositoryTests
    {
        /// <summary>
        /// Gets the episode repository.
        /// </summary>
        /// <value>The episode repository.</value>
        private IEpisodeRepository EpisodeRepository
        {
            get
            {
                return TestApp.RealRepositories.EpisodeRepository;
            }
        }

        /// <summary>
        /// Gets the episodes_1_ OK.
        /// </summary>
        [Test]
        public void GetEpisodes_1_OK()
        {
            // Arrange:

            // Act:
            var result =
                this.EpisodeRepository.GetEpisodes(1);

            // Assert:
        }
    }
}