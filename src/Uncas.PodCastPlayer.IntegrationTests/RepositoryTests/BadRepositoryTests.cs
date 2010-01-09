//-------------
// <copyright file="BadRepositoryTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests.RepositoryTests
{
    using NUnit.Framework;
    using Uncas.PodCastPlayer.SQLiteRepository;

    /// <summary>
    /// Testing pod cast repository with bad repository path.
    /// </summary>
    [TestFixture]
    public class BadRepositoryTests :
        Tests.RepositoryTests.EpisodeRepositoryTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRepositoryTests"/> class.
        /// </summary>
        public BadRepositoryTests()
            : base(new SQLiteRepositoryFactory(string.Empty))
        {
        }
    }
}