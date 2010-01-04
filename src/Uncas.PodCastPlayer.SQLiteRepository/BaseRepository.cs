//-------------
// <copyright file="BaseRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System.Globalization;
    using SubSonic.DataProviders;
    using SubSonic.Repository;

    /// <summary>
    /// Base repository class for SQLite.
    /// </summary>
    internal abstract class BaseRepository
    {
        /// <summary>
        /// The simple SubSonic repository.
        /// </summary>
        private SimpleRepository simpleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        public BaseRepository(
            string databasePath)
        {
            this.InitializeSimpleRepository(
                databasePath);
        }

        /// <summary>
        /// Gets the simple SubSonic repository.
        /// </summary>
        /// <value>The simple repository.</value>
        public SimpleRepository SimpleRepository
        {
            get
            {
                return this.simpleRepository;
            }
        }

        /// <summary>
        /// Initializes the simple repository.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        private void InitializeSimpleRepository(
            string databasePath)
        {
            string connectionString =
                string.Format(
                CultureInfo.InvariantCulture,
                "Data Source={0}",
                databasePath);
            var provider =
                ProviderFactory.GetProvider(
                connectionString,
                "System.Data.SQLite");
            this.simpleRepository =
                new SimpleRepository(
                    provider,
                    SimpleRepositoryOptions.RunMigrations);
        }
    }
}