//-------------
// <copyright file="IPodCastDownloader.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Utility
{
    using System.Collections.Generic;
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

        /// <summary>
        /// Downloads the episode list.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <returns>A list of episodes.</returns>
        IList<Episode> DownloadEpisodeList(PodCast podCast);
    }
}