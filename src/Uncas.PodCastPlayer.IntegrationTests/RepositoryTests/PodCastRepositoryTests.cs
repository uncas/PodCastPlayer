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
    public class PodCastRepositoryTests :
        Tests.RepositoryTests.PodCastRepositoryTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastRepositoryTests"/> class.
        /// </summary>
        public PodCastRepositoryTests()
            : base(TestApp.RealRepositories)
        {
        }

        public override void GetPodCasts_All_OK()
        {
            base.GetPodCasts_All_OK();
        }
    }
}