//-------------
// <copyright file="DownloadIndexViewModel.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.ViewModel
{
    using System;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// View of download index.
    /// </summary>
    public class DownloadIndexViewModel
    {
        #region Public properties

        /// <summary>
        /// Gets or sets the episode date.
        /// </summary>
        /// <value>The episode date.</value>
        public DateTime EpisodeDate { get; set; }

        /// <summary>
        /// Gets or sets the episode id.
        /// </summary>
        /// <value>The episode id.</value>
        public string EpisodeId { get; set; }

        /// <summary>
        /// Gets or sets the episode title.
        /// </summary>
        /// <value>The episode title.</value>
        public string EpisodeTitle { get; set; }

        /// <summary>
        /// Gets or sets the pod cast id.
        /// </summary>
        /// <value>The pod cast id.</value>
        public int PodCastId { get; set; }

        /// <summary>
        /// Gets or sets the name of the pod cast.
        /// </summary>
        /// <value>The name of the pod cast.</value>
        public string PodCastName { get; set; }

        #endregion

        /// <summary>
        /// Gets from an episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>The download index view model.</returns>
        public static DownloadIndexViewModel FromEpisode(
            Episode episode)
        {
            if (episode == null
                || episode.PodCast == null
                || !episode.PodCast.Id.HasValue)
            {
                return null;
            }

            return new DownloadIndexViewModel
                {
                    EpisodeDate = episode.Date,
                    EpisodeId = episode.Id,
                    EpisodeTitle = episode.Title,
                    PodCastId = episode.PodCast.Id.Value,
                    PodCastName = episode.PodCast.Name
                };
        }
    }
}