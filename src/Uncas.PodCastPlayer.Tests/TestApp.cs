//-------------
// <copyright file="TestApp.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests
{
    using Uncas.PodCastPlayer.Fakes;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// Statics for the test application.
    /// </summary>
    internal static class TestApp
    {
        /// <summary>
        /// The episode saver.
        /// </summary>
        private static IEpisodeSaver episodeSaver;

        /// <summary>
        /// The pod cast downloader.
        /// </summary>
        private static IPodCastDownloader podCastDownloader;

        /// <summary>
        /// The repositories.
        /// </summary>
        private static IRepositoryFactory repositories;

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
                        new FakeEpisodeSaver();
                }

                return episodeSaver;
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
                    podCastDownloader = new FakePodCastDownloader();
                }

                return podCastDownloader;
            }
        }

        /// <summary>
        /// Gets the repositories.
        /// </summary>
        /// <value>The repositories.</value>
        internal static IRepositoryFactory Repositories
        {
            get
            {
                if (repositories == null)
                {
                    repositories =
                        new FakeRepositoryFactory();
                }

                return repositories;
            }
        }
    }
}