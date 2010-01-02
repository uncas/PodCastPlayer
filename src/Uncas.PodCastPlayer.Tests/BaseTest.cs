//-------------
// <copyright file="BaseTest.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests
{
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Base test class.
    /// </summary>
    public abstract class BaseTest
    {
        /// <summary>
        /// Gets the episode repository.
        /// </summary>
        /// <value>The episode repository.</value>
        public IEpisodeRepository EpisodeRepository
        {
            get
            {
                return TestApp.Repositories.EpisodeRepository;
            }
        }

        /// <summary>
        /// Gets the pod cast repository.
        /// </summary>
        /// <value>The pod cast repository.</value>
        public IPodCastRepository PodCastRepository
        {
            get
            {
                return TestApp.Repositories.PodCastRepository;
            }
        }
    }
}