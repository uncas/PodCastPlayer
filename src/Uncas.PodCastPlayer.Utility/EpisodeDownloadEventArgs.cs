//-------------
// <copyright file="EpisodeDownloadEventArgs.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Utility
{
    using System;

    /// <summary>
    /// Stores information about episode download events.
    /// </summary>
    public class EpisodeDownloadEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the file size in bytes.
        /// </summary>
        /// <value>The file size in bytes.</value>
        public long FileSizeInBytes { get; set; }

        /// <summary>
        /// Gets or sets the bytes downloaded.
        /// </summary>
        /// <value>The bytes downloaded.</value>
        public int BytesDownloaded { get; set; }
    }
}