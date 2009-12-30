//-------------
// <copyright file="TestApp.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests
{
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// Statics for the test application.
    /// </summary>
    internal static class TestApp
    {
        /// <summary>
        /// The pod cast downloader.
        /// </summary>
        private static IPodCastDownloader podCastDownloader;

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
    }
}