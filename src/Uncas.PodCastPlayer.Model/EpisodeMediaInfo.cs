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
        public long DownloadedBytes { get; set; }

        /// <summary>
        /// Gets a value indicating whether the download is completed.
        /// </summary>
        /// <value><c>true</c> if the download is completed; otherwise, <c>false</c>.</value>
        public bool DownloadCompleted
        {
            get
            {
                return IsDownloadCompleted(
                    this.FileSizeInBytes,
                    this.DownloadedBytes);
            }
        }

        /// <summary>
        /// Determines whether [is download completed] [the specified file size in bytes].
        /// </summary>
        /// <param name="fileSizeInBytes">The file size in bytes.</param>
        /// <param name="downloadedBytes">The downloaded bytes.</param>
        /// <returns>
        /// <c>true</c> if [is download completed] [the specified file size in bytes]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDownloadCompleted(
            long fileSizeInBytes,
            long downloadedBytes)
        {
            return fileSizeInBytes ==
                downloadedBytes;
        }
    }
}