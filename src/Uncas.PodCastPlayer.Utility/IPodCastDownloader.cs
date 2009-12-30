//-------------
// <copyright file="IPodCastDownloader.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Utility
{
    using System;
    using System.Collections.Generic;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Handles download of pod cast.
    /// </summary>
    public interface IPodCastDownloader
    {
        /// <summary>
        /// Occurs when an episode buffer has been downloaded.
        /// </summary>
        event EventHandler<EpisodeDownloadEventArgs>
            EpisodeBufferDownloaded;

        /// <summary>
        /// Downloads the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Info about the downloaded media.</returns>
        EpisodeMediaInfo DownloadEpisode(
            Episode episode,
            string fileName);

        /// <summary>
        /// Downloads the episode list.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <returns>A list of episodes.</returns>
        IList<Episode> DownloadEpisodeList(PodCast podCast);
    }
}