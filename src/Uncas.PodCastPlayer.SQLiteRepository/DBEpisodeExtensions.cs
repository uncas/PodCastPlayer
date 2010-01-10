//-------------
// <copyright file="DBEpisodeExtensions.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Extensions for the db episode.
    /// </summary>
    internal static class DBEpisodeExtensions
    {
        /// <summary>
        /// The invalid data text.
        /// </summary>
        private const string InvalidDataText =
            "Invalid data in database";

        /// <summary>
        /// Gets as model.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="podCasts">The pod casts.</param>
        /// <returns>The model episode.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public static Episode AsModel(
            this DBEpisode episode,
            IList<DBPodCast> podCasts)
        {
            Debug.Assert(
                podCasts != null,
                "Internal code assumes that there are pod casts!");
            Debug.Assert(
                podCasts.Count > 0,
                "Internal code assumes that there are pod casts!");
            if (podCasts == null
                || episode.MediaUrl == null)
            {
                throw new RepositoryException(
                    InvalidDataText);
            }

            var podCast =
                podCasts.Where(
                pc => pc.PodCastId == episode.RefPodCastId)
                .SingleOrDefault();
            if (podCast == null)
            {
                throw new RepositoryException(
                    InvalidDataText);
            }

            Uri mediaUrl = null;

            try
            {
                mediaUrl = new Uri(episode.MediaUrl);
            }
            catch (UriFormatException ex)
            {
                throw new RepositoryException(
                    InvalidDataText,
                    ex);
            }

            return Episode.ConstructEpisode(
                episode.EpisodeId,
                episode.Date,
                episode.Title,
                episode.Description,
                mediaUrl,
                podCast.AsModel(),
                true);
        }

        /// <summary>
        /// Gets as index item.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>The episode index item view model.</returns>
        public static EpisodeIndexItemViewModel AsIndexItem(
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

        /// <summary>
        /// Gets db episode from the model episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>The db episode.</returns>
        public static DBEpisode AsDB(
            this Episode episode)
        {
            return new DBEpisode
            {
                Date = episode.Date,
                Description = episode.Description,
                DownloadedBytes = episode.DownloadedBytes,
                EpisodeId = episode.Id,
                FileName = episode.FileName,
                FileSizeInBytes = episode.FileSizeInBytes,
                MediaUrl = episode.MediaUrl.ToString(),
                PendingDownload =
                    episode.PendingDownload,
                RefPodCastId = (long)episode.PodCast.Id,
                Title = episode.Title
            };
        }
    }
}