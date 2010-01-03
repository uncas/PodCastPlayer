//-------------
// <copyright file="EpisodeIndexItemViewModel.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.ViewModel
{
    using System;

    /// <summary>
    /// Represents a view of an episode item.
    /// </summary>
    public class EpisodeIndexItemViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeIndexItemViewModel"/> class.
        /// </summary>
        /// <param name="date">The publish date.</param>
        /// <param name="id">The id of the episode.</param>
        /// <param name="title">The title of the episode.</param>
        /// <param name="pendingDownload">if set to <c>true</c> [pending download].</param>
        /// <param name="downloadCompleted">if set to <c>true</c> [download completed].</param>
        public EpisodeIndexItemViewModel(
            DateTime date,
            string id,
            string title,
            bool pendingDownload,
            bool downloadCompleted)
        {
            this.Date = date.Date;
            this.DownloadCompleted = downloadCompleted;
            this.Id = id;
            this.PendingDownload = pendingDownload;
            this.Title = title;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>The publish date.</value>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [download completed].
        /// </summary>
        /// <value><c>true</c> if [download completed]; otherwise, <c>false</c>.</value>
        public bool DownloadCompleted { get; private set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id of the episode.</value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [pending download].
        /// </summary>
        /// <value><c>true</c> if [pending download]; otherwise, <c>false</c>.</value>
        public bool PendingDownload { get; private set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title of the episode.</value>
        public string Title { get; private set; }

        #endregion
    }
}