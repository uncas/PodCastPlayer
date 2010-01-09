//-------------
// <copyright file="BaseRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Security;
    using SubSonic.DataProviders;
    using SubSonic.Repository;
    using Uncas.PodCastPlayer.Repository;

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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
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
        public SimpleRepository DB
        {
            get
            {
                return this.simpleRepository;
            }
        }

        /// <summary>
        /// Gets the repository folder exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>The repository exception.</returns>
        private static RepositoryException
            GetRepositoryFolderException(
            Exception ex)
        {
            return new RepositoryException(
                "Creation of folder for repository database failed",
                ex);
        }

        /// <summary>
        /// Initializes the simple repository.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        private void InitializeSimpleRepository(
            string databasePath)
        {
            if (string.IsNullOrEmpty(databasePath))
            {
                // TODO: EXCEPTION: Consider if something else should be done here?
                return;
            }

            try
            {
                FileInfo fi = new FileInfo(databasePath);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
            }
            catch (IOException ex)
            {
                throw GetRepositoryFolderException(ex);
            }
            catch (SecurityException ex)
            {
                throw GetRepositoryFolderException(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw GetRepositoryFolderException(ex);
            }
            catch (NotSupportedException ex)
            {
                throw GetRepositoryFolderException(ex);
            }

            string connectionString =
                string.Format(
                CultureInfo.InvariantCulture,
                "Data Source={0}",
                databasePath);
            try
            {
                var provider =
                    ProviderFactory.GetProvider(
                    connectionString,
                    "System.Data.SQLite");
                this.simpleRepository =
                    new SimpleRepository(
                        provider,
                        SimpleRepositoryOptions.RunMigrations);
            }
            catch (Exception ex)
            {
                // Unknown exceptions from third-party SubSonic...
                throw new RepositoryException(
                    "Error initializing SQLite repository",
                    ex);
            }
        }
    }
}