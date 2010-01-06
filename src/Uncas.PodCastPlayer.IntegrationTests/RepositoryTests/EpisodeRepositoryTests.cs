//-------------
// <copyright file="EpisodeRepositoryTests.cs" company="Uncas">
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
    public class EpisodeRepositoryTests :
        Tests.RepositoryTests.EpisodeRepositoryTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeRepositoryTests"/> class.
        /// </summary>
        public EpisodeRepositoryTests()
            : base(TestApp.RealRepositories)
        {
        }
    }
}