//-------------
// <copyright file="Episode.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Model
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents an episode of a pod cast.
    /// </summary>
    public class Episode
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Episode"/> class.
        /// </summary>
        public Episode()
        {
            this.Date = DateTime.Now;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the date of the episode.
        /// </summary>
        /// <value>The date of the episode.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id of the episode.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the media info.
        /// </summary>
        /// <value>The media info.</value>
        public EpisodeMediaInfo MediaInfo { get; set; }

        /// <summary>
        /// Gets or sets the media URL.
        /// </summary>
        /// <value>The media URL.</value>
        public Uri MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the episode is pending download.
        /// </summary>
        /// <value><c>true</c> if pending download; otherwise, <c>false</c>.</value>
        public bool PendingDownload { get; set; }

        /// <summary>
        /// Gets or sets the pod cast.
        /// </summary>
        /// <value>The pod cast.</value>
        public PodCast PodCast { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "Title: {0}\nDate: {1}\nId: {2}\nMediaUrl: {3}\nMediaLength: {4}\nDescription: {5}",
                /*0*/ this.Title,
                /*1*/ this.Date,
                /*2*/ this.Id,
                /*3*/ this.MediaUrl,
                /*4*/ this.MediaInfo.FileSizeInBytes,
                /*5*/ this.Description);
        }
    }
}