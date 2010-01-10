//-------------
// <copyright file="Episode.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Model
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Represents an episode of a pod cast.
    /// </summary>
    public class Episode
    {
        #region Private fields

        /// <summary>
        /// The media url.
        /// </summary>
        private Uri mediaUrl;

        #endregion

        #region Private constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Episode"/> class.
        /// </summary>
        /// <param name="id">The id of the episode.</param>
        /// <param name="date">The publish date.</param>
        /// <param name="title">The title of the episode.</param>
        /// <param name="description">The description.</param>
        private Episode(
            string id,
            DateTime date,
            string title,
            string description)
        {
            this.Id = id;
            this.Date = date;
            this.Title = title;
            this.Description = description;
            this.MediaInfo = new EpisodeMediaInfo();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the date of the episode.
        /// </summary>
        /// <value>The date of the episode.</value>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the downloaded bytes.
        /// </summary>
        /// <value>The downloaded bytes.</value>
        public long DownloadedBytes
        {
            get
            {
                if (this.MediaInfo == null)
                {
                    return 0;
                }

                return this.MediaInfo.DownloadedBytes;
            }
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the file size in bytes.
        /// </summary>
        /// <value>The file size in bytes.</value>
        public long FileSizeInBytes
        {
            get
            {
                if (this.MediaInfo == null)
                {
                    return 0;
                }

                return this.MediaInfo.FileSizeInBytes;
            }
        }

        /// <summary>
        /// Gets the id of the episode.
        /// </summary>
        /// <value>The id of the episode.</value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets the media info.
        /// </summary>
        /// <value>The media info.</value>
        public EpisodeMediaInfo MediaInfo { get; set; }

        /// <summary>
        /// Gets the media URL.
        /// </summary>
        /// <value>The media URL.</value>
        public Uri MediaUrl
        {
            get
            {
                return this.mediaUrl;
            }

            private set
            {
                this.mediaUrl = value;
                if (value != null
                    && value.Segments != null
                    && value.Segments.Length > 0)
                {
                    this.FileName = value.Segments.Last();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the episode is pending download.
        /// </summary>
        /// <value><c>true</c> if pending download; otherwise, <c>false</c>.</value>
        public bool PendingDownload { get; set; }

        /// <summary>
        /// Gets the associated pod cast.
        /// </summary>
        /// <value>The pod cast.</value>
        public PodCast PodCast { get; private set; }

        /// <summary>
        /// Gets the title of the episode.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Constructs the episode.
        /// </summary>
        /// <param name="id">The id of the episode.</param>
        /// <param name="date">The date of the episode.</param>
        /// <param name="title">The title of the episode.</param>
        /// <param name="description">The description.</param>
        /// <param name="mediaUrl">The media URL.</param>
        /// <param name="podCast">The pod cast.</param>
        /// <param name="pendingDownload">if set to <c>true</c> [pending download].</param>
        /// <returns>The episode.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Model.ModelException"></exception>
        public static Episode ConstructEpisode(
            string id,
            DateTime date,
            string title,
            string description,
            Uri mediaUrl,
            PodCast podCast,
            bool pendingDownload)
        {
            if (mediaUrl == null)
            {
                throw new ModelException("MediaUrl is required");
            }

            var result =
                new Episode(
                id,
                date,
                title,
                description);
            result.PodCast = podCast;
            result.MediaUrl = mediaUrl;
            result.PendingDownload = pendingDownload;
            return result;
        }

        /// <summary>
        /// Updates from other episode.
        /// </summary>
        /// <param name="other">The other episode.</param>
        /// <exception cref="Uncas.PodCastPlayer.Model.ModelException"></exception>
        public void UpdateFromOtherEpisode(Episode other)
        {
            if (other == null)
            {
                return;
            }

            this.Date = other.Date;
            this.Description = other.Description;
            this.MediaUrl = other.MediaUrl;
            this.Title = other.Title;
            if (this.MediaInfo == null)
            {
                this.MediaInfo = new EpisodeMediaInfo();
            }

            if (other.MediaInfo != null)
            {
                this.MediaInfo.FileSizeInBytes =
                    other.MediaInfo.FileSizeInBytes;
            }
        }

        #endregion
    }
}