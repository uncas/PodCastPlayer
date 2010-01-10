//-------------
// <copyright file="EpisodeSaver.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Utility
{
    using System.IO;

    /// <summary>
    /// Saves episode media.
    /// </summary>
    public class EpisodeSaver : IEpisodeSaver
    {
        #region IEpisodeSaver Members

        /// <summary>
        /// Saves the stream.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileSize">Size of the file.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>The number of bytes saved.</returns>
        public long SaveStream(
            string filePath,
            long fileSize,
            Stream stream)
        {
            // A buffer for storing retrieved data:
            byte[] downBuffer = new byte[2048];

            // Creates the directory if it does not exist:
            DirectoryInfo directory =
                new FileInfo(filePath).Directory;
            if (!directory.Exists)
            {
                directory.Create();
            }

            int bytesTotal = 0;
            using (var fileStream = new FileStream(
                 filePath,
                 FileMode.Create,
                 FileAccess.Write,
                 FileShare.None))
            {
                bytesTotal =
                  DownloadBuffers(
                    stream,
                    downBuffer,
                    bytesTotal,
                    fileStream);
            }

            return bytesTotal;
        }

        #endregion

        /// <summary>
        /// Downloads the buffer.
        /// </summary>
        /// <param name="responseStream">The response stream.</param>
        /// <param name="downBuffer">Down buffer.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <returns>The number of bytes read.</returns>
        private static int DownloadBuffer(
            Stream responseStream,
            byte[] downBuffer,
            FileStream fileStream)
        {
            int bytesRead =
                responseStream.Read(
                downBuffer,
                0,
                downBuffer.Length);
            if (bytesRead == 0)
            {
                return 0;
            }

            // Writes to the local hard drive:
            fileStream.Write(downBuffer, 0, bytesRead);
            return bytesRead;
        }

        /// <summary>
        /// Downloads the buffers.
        /// </summary>
        /// <param name="responseStream">The response stream.</param>
        /// <param name="downBuffer">Down buffer.</param>
        /// <param name="bytesTotal">The bytes total.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <returns>The total number of bytes downloaded.</returns>
        private static int DownloadBuffers(
            Stream responseStream,
            byte[] downBuffer,
            int bytesTotal,
            FileStream fileStream)
        {
            while (true)
            {
                int bytesRead =
                    DownloadBuffer(
                    responseStream,
                    downBuffer,
                    fileStream);
                if (bytesRead == 0)
                {
                    break;
                }

                bytesTotal += bytesRead;
            }

            return bytesTotal;
        }
    }
}