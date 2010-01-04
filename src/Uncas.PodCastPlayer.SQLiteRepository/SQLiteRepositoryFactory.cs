//-------------
// <copyright file="SQLiteRepositoryFactory.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Factory for repositories implemented in SQLite.
    /// </summary>
    public class SQLiteRepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        /// The database path.
        /// </summary>
        private readonly string databasePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteRepositoryFactory"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        public SQLiteRepositoryFactory(
            string databasePath)
        {
            this.databasePath = databasePath;
        }

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
            get
            {
                return new PodCastRepository(
                    this.databasePath);
            }
        }

        #endregion
    }
}