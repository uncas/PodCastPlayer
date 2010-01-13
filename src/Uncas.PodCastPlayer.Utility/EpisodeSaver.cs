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
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        public long SaveStream(
            string filePath,
            long fileSize,
            Stream stream)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new UtilityException(
                    "File path is not defined.");
            }

            if (stream == null)
            {
                throw new UtilityException(
                    "Stream is not defined.");
            }

            // Creates the directory if it does not exist:
            var directory =
                new FileInfo(filePath).Directory;
            if (directory == null)
            {
                throw new UtilityException(
                    "Invalid directory.");
            }

            if (!directory.Exists)
            {
                try
                {
                    directory.Create();
                }
                catch (IOException ex)
                {
                    throw new UtilityException(
                        "Error while trying to create directory.",
                        ex);
                }
            }

            // A buffer for storing retrieved data:
            var downBuffer = new byte[2048];
            var bytesTotal = 0;
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
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        private static int DownloadBuffer(
            Stream responseStream,
            byte[] downBuffer,
            FileStream fileStream)
        {
            int bytesRead;
            try
            {
                bytesRead =
                    responseStream.Read(
                    downBuffer,
                    0,
                    downBuffer.Length);
            }
            catch (IOException ex)
            {
                throw new UtilityException(
                    "Error trying to download episode buffer.",
                    ex);
            }

            if (bytesRead == 0)
            {
                return 0;
            }

            // Writes to the local hard drive:
            try
            {
                fileStream.Write(
                    downBuffer,
                    0,
                    bytesRead);
            }
            catch (IOException ex)
            {
                throw new UtilityException(
                    "Error trying to save episode buffer.",
                    ex);
            }

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
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        private static int DownloadBuffers(
            Stream responseStream,
            byte[] downBuffer,
            int bytesTotal,
            FileStream fileStream)
        {
            while (true)
            {
                var bytesRead =
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