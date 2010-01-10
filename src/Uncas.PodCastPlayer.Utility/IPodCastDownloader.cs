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
        /// Downloads the episode list.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <returns>A list of episodes.</returns>
        IList<Episode> DownloadEpisodeList(PodCast podCast);

        /// <summary>
        /// Downloads the pod cast info.
        /// </summary>
        /// <param name="podCastUrl">The pod cast URL.</param>
        /// <returns>Details about the pod cast.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        PodCast DownloadPodCastInfo(Uri podCastUrl);

        /// <summary>
        /// Gets the episode stream.
        /// </summary>
        /// <param name="mediaUrl">The media URL.</param>
        /// <returns>The episode stream.</returns>
        EpisodeMedia GetEpisodeStream(
            Uri mediaUrl);
    }
}