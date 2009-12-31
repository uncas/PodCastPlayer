//-------------
// <copyright file="EpisodeMediaInfo.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Model
{
    /// <summary>
    /// Holds info about the episode media.
    /// </summary>
    public class EpisodeMediaInfo
    {
        /// <summary>
        /// Gets or sets the file size in bytes.
        /// </summary>
        /// <value>The file size in bytes.</value>
        public long FileSizeInBytes { get; set; }

        /// <summary>
        /// Gets or sets the downloaded bytes.
        /// </summary>
        /// <value>The downloaded bytes.</value>
        public int DownloadedBytes { get; set; }

        /// <summary>
        /// Gets a value indicating whether the download is completed.
        /// </summary>
        /// <value><c>true</c> if the download is completed; otherwise, <c>false</c>.</value>
        public bool DownloadCompleted
        {
            get
            {
                return this.FileSizeInBytes == 
                    this.DownloadedBytes;
            }
        }
    }
}