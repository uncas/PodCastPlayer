//-------------
// <copyright file="FakeRepositoryFactory.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Fakes
{
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Constructs fake repositories.
    /// </summary>
    public class FakeRepositoryFactory : IRepositoryFactory
    {
        #region IRepositoryFactory Members

        /// <summary>
        /// Gets the episode repository.
        /// </summary>
        /// <value>The episode repository.</value>
        public IEpisodeRepository EpisodeRepository
        {
            get { return new FakeEpisodeRepository(); }
        }

        /// <summary>
        /// Gets the pod cast repository.
        /// </summary>
        /// <value>The pod cast repository.</value>
        public IPodCastRepository PodCastRepository
        {
            get { return new FakePodCastRepository(); }
        }

        #endregion
    }
}