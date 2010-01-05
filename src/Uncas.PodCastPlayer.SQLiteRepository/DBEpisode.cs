//-------------
// <copyright file="DBEpisode.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System;
    using SubSonic.SqlGeneration.Schema;

    /// <summary>
    /// Represents the episode in the database.
    /// </summary>
    [SubSonicTableNameOverride("Episodes")]
    public class DBEpisode
    {
        /// <summary>
        /// Gets or sets the episode id.
        /// </summary>
        /// <value>The episode id.</value>
        [SubSonicPrimaryKey]
        public string EpisodeId { get; set; }

        /// <summary>
        /// Gets or sets the ref pod cast id.
        /// </summary>
        /// <value>The ref pod cast id.</value>
        public long RefPodCastId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The publish date.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [SubSonicLongString]
        [SubSonicNullString]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the media URL.
        /// </summary>
        /// <value>The media URL.</value>
        public string MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [pending download].
        /// </summary>
        /// <value><c>true</c> if [pending download]; otherwise, <c>false</c>.</value>
        public bool PendingDownload { get; set; }

        /// <summary>
        /// Gets or sets the file size in bytes.
        /// </summary>
        /// <value>The file size in bytes.</value>
        public long FileSizeInBytes { get; set; }

        /// <summary>
        /// Gets or sets the downloaded bytes.
        /// </summary>
        /// <value>The downloaded bytes.</value>
        public long DownloadedBytes { get; set; }
    }
}