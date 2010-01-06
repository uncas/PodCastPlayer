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
        /// The repositories.
        /// </summary>
        private readonly IRepositoryFactory repositories;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        public BaseTest()
            : this(TestApp.Repositories)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        public BaseTest(IRepositoryFactory repositories)
        {
            this.repositories = repositories;
        }

        /// <summary>
        /// Gets the episode repository.
        /// </summary>
        /// <value>The episode repository.</value>
        public IEpisodeRepository EpisodeRepository
        {
            get
            {
                return this.repositories.EpisodeRepository;
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
                return this.repositories.PodCastRepository;
            }
        }
    }
}