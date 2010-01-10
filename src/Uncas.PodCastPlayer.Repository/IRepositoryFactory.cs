//-------------
// <copyright file="IRepositoryFactory.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Repository
{
    /// <summary>
    /// Interface for creating repositories.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets the episode repository.
        /// </summary>
        /// <value>The episode repository.</value>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        IEpisodeRepository EpisodeRepository { get; }

        /// <summary>
        /// Gets the pod cast repository.
        /// </summary>
        /// <value>The pod cast repository.</value>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        IPodCastRepository PodCastRepository { get; }
    }
}