//-------------
// <copyright file="BaseService.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.AppServices
{
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// A base service class.
    /// </summary>
    public abstract class BaseService
    {
        #region Private fields

        /// <summary>
        /// The downloader.
        /// </summary>
        private readonly IPodCastDownloader downloader;

        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepositoryFactory repositories;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        /// <param name="downloader">The downloader.</param>
        /// <exception cref="Uncas.PodCastPlayer.AppServices.ServiceException"></exception>
        protected internal BaseService(
            IRepositoryFactory repositories,
            IPodCastDownloader downloader)
        {
            if (repositories == null)
            {
                throw new ServiceException(
                    "Repositories must be specified.");
            }

            if (downloader == null)
            {
                throw new ServiceException(
                    "Downloader must be specified.");
            }

            this.repositories = repositories;
            this.downloader = downloader;
        }

        #endregion

        #region Protected properties

        /// <summary>
        /// Gets the downloader.
        /// </summary>
        /// <value>The downloader.</value>
        protected IPodCastDownloader Downloader
        {
            get { return this.downloader; }
        }

        /// <summary>
        /// Gets the episode repository.
        /// </summary>
        /// <value>The episode repository.</value>
        protected IEpisodeRepository EpisodeRepository
        {
            get { return this.Repositories.EpisodeRepository; }
        }

        /// <summary>
        /// Gets the pod cast repository.
        /// </summary>
        /// <value>The pod cast repository.</value>
        protected IPodCastRepository PodCastRepository
        {
            get { return this.Repositories.PodCastRepository; }
        }

        /// <summary>
        /// Gets the repositories.
        /// </summary>
        /// <value>The repositories.</value>
        private IRepositoryFactory Repositories
        {
            get { return this.repositories; }
        }

        #endregion
    }
}