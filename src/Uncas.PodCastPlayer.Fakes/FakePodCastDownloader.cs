//-------------
// <copyright file="FakePodCastDownloader.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Fakes
{
    using System;
    using System.Collections.Generic;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// Fakes download of pod casts.
    /// </summary>
    public class FakePodCastDownloader : IPodCastDownloader
    {
        #region IPodCastDownloader Members

        /// <summary>
        /// Occurs when an episode buffer has been downloaded.
        /// </summary>
        public event EventHandler<EpisodeDownloadEventArgs>
            EpisodeBufferDownloaded;

        /// <summary>
        /// Downloads the episode list.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <returns>A list of episodes.</returns>
        public IList<Episode> DownloadEpisodeList(
            PodCast podCast)
        {
            var result = new List<Episode>();
            result.AddRange(podCast.Episodes);
            result.Add(Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "x",
                "y"));
            return result;
        }

        /// <summary>
        /// Downloads the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Info about the downloaded media.</returns>
        public EpisodeMediaInfo DownloadEpisode(
            Episode episode,
            string fileName)
        {
            if (this.EpisodeBufferDownloaded != null)
            {
                var eventArgs =
                    new EpisodeDownloadEventArgs
                    {
                        BytesDownloaded = 1,
                        FileSizeInBytes = 1
                    };
                this.EpisodeBufferDownloaded(
                    this,
                    eventArgs);
            }

            return new EpisodeMediaInfo
            {
                DownloadedBytes = 1,
                FileSizeInBytes = 1
            };
        }

        #endregion
    }
}