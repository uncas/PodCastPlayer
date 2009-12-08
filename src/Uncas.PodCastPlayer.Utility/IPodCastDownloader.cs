//-------------
// <copyright file="IPodCastDownloader.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Utility
{
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Handles download of pod cast.
    /// </summary>
    public interface IPodCastDownloader
    {
        /// <summary>
        /// Downloads the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        void DownloadEpisode(Episode episode);
    }
}