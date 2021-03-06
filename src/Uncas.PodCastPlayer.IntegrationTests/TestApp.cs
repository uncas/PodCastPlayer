﻿//-------------
// <copyright file="TestApp.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests
{
    using System.IO;
    using Uncas.PodCastPlayer.Fakes;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.SQLiteRepository;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// Statics for the test application.
    /// </summary>
    internal static class TestApp
    {
        #region Private fields

        /// <summary>
        /// The pod cast downloader.
        /// </summary>
        private static IPodCastDownloader podCastDownloader;

        /// <summary>
        /// The episode saver.
        /// </summary>
        private static IEpisodeSaver episodeSaver;

        /// <summary>
        /// The fake repositories.
        /// </summary>
        private static IRepositoryFactory fakeRepositories;

        /// <summary>
        /// The reeal repositories.
        /// </summary>
        private static IRepositoryFactory realRepositories;

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the episode saver.
        /// </summary>
        /// <value>The episode saver.</value>
        internal static IEpisodeSaver EpisodeSaver
        {
            get
            {
                if (episodeSaver == null)
                {
                    episodeSaver =
                        new EpisodeSaver();
                }

                return episodeSaver;
            }
        }

        /// <summary>
        /// Gets the fake repositories.
        /// </summary>
        /// <value>The fake repositories.</value>
        internal static IRepositoryFactory FakeRepositories
        {
            get
            {
                if (fakeRepositories == null)
                {
                    fakeRepositories =
                        new FakeRepositoryFactory();
                }

                return fakeRepositories;
            }
        }

        /// <summary>
        /// Gets the pod cast downloader.
        /// </summary>
        /// <value>The pod cast downloader.</value>
        internal static IPodCastDownloader PodCastDownloader
        {
            get
            {
                if (podCastDownloader == null)
                {
                    podCastDownloader = new PodCastDownloader();
                }

                return podCastDownloader;
            }
        }

        /// <summary>
        /// Gets the real repositories.
        /// </summary>
        /// <value>The real repositories.</value>
        internal static IRepositoryFactory RealRepositories
        {
            get
            {
                if (realRepositories == null)
                {
                    string currentDir =
                        Directory.GetCurrentDirectory();
                    string repositoryPath =
                        Path.Combine(
                        currentDir,
                        "PodCastPlayerTests.db");
                    realRepositories =
                        new SQLiteRepositoryFactory(
                            repositoryPath);
                }

                return realRepositories;
            }
        }

        #endregion
    }
}