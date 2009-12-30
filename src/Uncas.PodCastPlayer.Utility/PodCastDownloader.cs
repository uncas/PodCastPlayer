//-------------
// <copyright file="PodCastDownloader.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Handles downloads of pod casts.
    /// </summary>
    public class PodCastDownloader : IPodCastDownloader
    {
        #region IPodCastDownloader Members

        /// <summary>
        /// Occurs when an episode buffer has been downloaded.
        /// </summary>
        public event EventHandler<EpisodeDownloadEventArgs>
            EpisodeBufferDownloaded;

        /// <summary>
        /// Downloads the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Info about the downloaded media.</returns>
        /// <remarks>
        /// http://www.geekpedia.com/tutorial179_Creating-a-download-manager-in-Csharp.html
        /// </remarks>
        public EpisodeMediaInfo DownloadEpisode(
            Episode episode,
            string fileName)
        {
            var webRequest =
                (HttpWebRequest)WebRequest.Create(
                episode.MediaUrl);

            // Set default authentication for retrieving the file
            webRequest.Credentials =
                            CredentialCache.DefaultCredentials;

            // Retrieve the response from the server
            var webResponse =
                (HttpWebResponse)webRequest.GetResponse();

            // Ask the server for the file size and store it
            long fileSize = webResponse.ContentLength;
            Trace.WriteLine(fileSize);

            /*DownloadWithWebclient(
                episode,
                fileName,
                fileSize);*/

            int downloadedBytes =
                this.DownloadWithWebResponse(
                fileName,
                webRequest,
                fileSize);

            // DownloadInOnePiece(episode, fileName);
            return new EpisodeMediaInfo
            {
                FileSizeInBytes = (int)fileSize,
                DownloadedBytes = downloadedBytes
            };
        }

        /// <summary>
        /// Downloads the episode list.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <returns>A list of episodes.</returns>
        public IList<Episode> DownloadEpisodeList(
            PodCast podCast)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Downloads the in one piece.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="fileName">Name of the file.</param>
        private static void DownloadInOnePiece(
            Episode episode,
            string fileName)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(
                    episode.MediaUrl.AbsoluteUri,
                    fileName);
            }
        }

        /// <summary>
        /// Downloads the stream.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileSize">Size of the file.</param>
        /// <param name="responseStream">The response stream.</param>
        private void DownloadStream(
            string fileName,
            long fileSize,
            Stream responseStream)
        {
            this.DownloadStream(
                fileName,
                fileSize,
                responseStream,
                false);
        }

        /// <summary>
        /// Downloads the stream.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileSize">Size of the file.</param>
        /// <param name="responseStream">The response stream.</param>
        /// <param name="appendData">if set to <c>true</c> [append data].</param>
        /// <returns>The number of bytes downloaded.</returns>
        private int DownloadStream(
            string fileName,
            long fileSize,
            Stream responseStream,
            bool appendData)
        {
            // A buffer for storing retrieved data:
            byte[] downBuffer = new byte[2048];

            int bytesTotal = 0;
            FileMode fileMode =
                appendData ?
                FileMode.Append :
                FileMode.Create;
            using (var fileStream = new FileStream(
                 fileName,
                 fileMode,
                 FileAccess.Write,
                 FileShare.None))
            {
                // Loop until no more data:
                while (true)
                {
                    int bytesRead =
                        responseStream.Read(
                        downBuffer,
                        0,
                        downBuffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    // Writes to the local hard drive:
                    fileStream.Write(downBuffer, 0, bytesRead);
                    bytesTotal += bytesRead;
                    Trace.WriteLine(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "{0}: {1}/{2} = {3:P1}",
                            bytesRead,
                            bytesTotal,
                            fileSize,
                            (1D * bytesTotal) / (1D * fileSize)));
                    if (this.EpisodeBufferDownloaded != null)
                    {
                        var eventArgs =
                            new EpisodeDownloadEventArgs
                            {
                                BytesDownloaded = bytesTotal,
                                FileSizeInBytes = fileSize
                            };
                        this.EpisodeBufferDownloaded(
                            this,
                            eventArgs);
                    }
                }
            }

            return bytesTotal;
        }

        /// <summary>
        /// Downloads the with web client.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileSize">Size of the file.</param>
        private void DownloadWithWebClient(
            Episode episode,
            string fileName,
            long fileSize)
        {
            // Opens the URL for download:
            using (var client = new WebClient())
            {
                using (var responseStream =
                      client.OpenRead(episode.MediaUrl))
                {
                    this.DownloadStream(
                        fileName,
                        fileSize,
                        responseStream);
                }
            }
        }

        /// <summary>
        /// Downloads the file with web response.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="webRequest">The web request.</param>
        /// <param name="fileSize">Size of the file.</param>
        /// <returns>The number of bytes downloaded.</returns>
        private int DownloadWithWebResponse(
            string fileName,
            HttpWebRequest webRequest,
            long fileSize)
        {
            // TODO: FEATURE: Proper offset!
            int desiredOffset = 0;
            if (desiredOffset > 0)
            {
                webRequest.AddRange(desiredOffset);
            }

            // Retrieves the response from the server:
            var response =
                (HttpWebResponse)webRequest.GetResponse();
            bool supportsPartialContent = false;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // The server ignored our range request.
                // The download must restart.
                // TODO: FEATURE: Save the whole response.
                supportsPartialContent = false;
            }
            else if (response.StatusCode ==
                HttpStatusCode.PartialContent)
            {
                // The server accepted our range request.
                // TODO: FEATURE: Append the partial response.
                supportsPartialContent = true;
            }

            var responseStream =
                response.GetResponseStream();
            return this.DownloadStream(
                fileName,
                fileSize,
                responseStream,
                supportsPartialContent);
        }

        #endregion
    }
}