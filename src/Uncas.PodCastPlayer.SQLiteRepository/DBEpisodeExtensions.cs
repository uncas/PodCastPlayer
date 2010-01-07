//-------------
// <copyright file="DBEpisodeExtensions.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Extensions for the db episode.
    /// </summary>
    public static class DBEpisodeExtensions
    {
        /// <summary>
        /// Gets as model.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="podCasts">The pod casts.</param>
        /// <returns>The model episode.</returns>
        public static Episode AsModel(
            this DBEpisode episode,
            IList<DBPodCast> podCasts)
        {
            var podCast =
                podCasts.Where(
                pc => pc.PodCastId == episode.RefPodCastId)
                .SingleOrDefault();
            return Episode.ConstructEpisode(
                episode.EpisodeId,
                episode.Date,
                episode.Title,
                episode.Description,
                new Uri(episode.MediaUrl),
                podCast.AsModelPodCast(),
                true);
        }

        /// <summary>
        /// Gets as index item.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>The episode index item view model.</returns>
        public static EpisodeIndexItemViewModel
            AsIndexItem(
            this DBEpisode episode)
        {
            bool downloadCompleted =
                EpisodeMediaInfo.IsDownloadCompleted(
                episode.FileSizeInBytes,
                episode.DownloadedBytes);
            return new EpisodeIndexItemViewModel(
                episode.Date,
                episode.EpisodeId,
                episode.Title,
                episode.PendingDownload,
                downloadCompleted);
        }
    }
}