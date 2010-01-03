//-------------
// <copyright file="DownloadIndexViewModel.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.ViewModel
{
    using System;

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
    }
}