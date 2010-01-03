//-------------
// <copyright file="SQLiteRepositoryFactory.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System;
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Factory for repositories implemented in SQLite.
    /// </summary>
    public class SQLiteRepositoryFactory : IRepositoryFactory
    {
        #region IRepositoryFactory Members

        /// <summary>
        /// Gets the episode repository.
        /// </summary>
        /// <value>The episode repository.</value>
        public IEpisodeRepository EpisodeRepository
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the pod cast repository.
        /// </summary>
        /// <value>The pod cast repository.</value>
        public IPodCastRepository PodCastRepository
        {
            get { return new PodCastRepository(); }
        }

        #endregion
    }
}