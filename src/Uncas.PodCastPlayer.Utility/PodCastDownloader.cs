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
    using System.Xml.Serialization;

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

            int downloadedBytes =
                this.DownloadWithWebResponse(
                fileName,
                webRequest,
                fileSize);

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
            var result = new List<Episode>();
            using (WebClient client = new WebClient())
            {
                string rssFeed =
                    client.DownloadString(
                    podCast.Url);
            }
            return result;
        }

        #endregion

        #region Private methods

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
        /// Checks if the response supports partial content.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>True if it supports partial content.</returns>
        private static bool CheckIfResponseSupportsPartialContent(
            HttpWebResponse response)
        {
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

            return supportsPartialContent;
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <param name="webRequest">The web request.</param>
        /// <param name="desiredOffset">The desired offset.</param>
        /// <returns>The response.</returns>
        private static HttpWebResponse GetResponse(
            HttpWebRequest webRequest,
            int desiredOffset)
        {
            if (desiredOffset > 0)
            {
                webRequest.AddRange(desiredOffset);
            }

            // Retrieves the response from the server:
            var response =
                (HttpWebResponse)webRequest.GetResponse();
            return response;
        }

        /// <summary>
        /// Downloads the buffers.
        /// </summary>
        /// <param name="fileSize">Size of the file.</param>
        /// <param name="responseStream">The response stream.</param>
        /// <param name="downBuffer">Down buffer.</param>
        /// <param name="bytesTotal">The bytes total.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <returns>The total number of bytes downloaded.</returns>
        private int DownloadBuffers(
            long fileSize,
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
                this.NotifyOfDownloadStatus(
                    bytesRead,
                    fileSize,
                    bytesTotal);
            }

            return bytesTotal;
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
                bytesTotal =
                  this.DownloadBuffers(
                    fileSize,
                    responseStream,
                    downBuffer,
                    bytesTotal,
                    fileStream);
            }

            return bytesTotal;
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
            var response =
                GetResponse(
                    webRequest,
                    desiredOffset);
            bool supportsPartialContent =
                CheckIfResponseSupportsPartialContent(
                    response);
            var responseStream =
                response.GetResponseStream();
            return this.DownloadStream(
                fileName,
                fileSize,
                responseStream,
                supportsPartialContent);
        }

        /// <summary>
        /// Notifies the of download status.
        /// </summary>
        /// <param name="bytesRead">The bytes read.</param>
        /// <param name="fileSize">Size of the file.</param>
        /// <param name="bytesTotal">The bytes total.</param>
        private void NotifyOfDownloadStatus(
            int bytesRead,
            long fileSize,
            int bytesTotal)
        {
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

            Trace.WriteLine(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}: {1}/{2} = {3:P1}",
                    bytesRead,
                    bytesTotal,
                    fileSize,
                    (1D * bytesTotal) / (1D * fileSize)));
        }

        #endregion

    }

    /// <summary>
    /// Represents an rss feed.
    /// </summary>
    [XmlType("rss")]
    public class RssFeed
    {
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        [XmlElement("channel")]
        public RssChannel Channel { get; set; }

        /// <summary>
        /// Saves the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void Save(TextWriter stream)
        {
            XmlSerializer serializer =
                new XmlSerializer(
                    typeof(RssFeed));
            serializer.Serialize(
                stream,
                this);
        }
    }

    /// <summary>
    /// Represents an rss channel.
    /// </summary>
    public class RssChannel
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public List<RssItem> Items { get; set; }
    }

    /// <summary>
    /// Represents an rss item.
    /// </summary>
    [XmlType("item")]
    public class RssItem
    {
        /// <summary>
        /// Gets or sets the enclosure.
        /// </summary>
        /// <value>The enclosure.</value>
        public Enclosure Enclosure { get; set; }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        /// <value>The GUID.</value>
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the pub date.
        /// </summary>
        /// <value>The pub date.</value>
        public DateTime PubDate { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
    }

    /// <summary>
    /// Represents an encosure.
    /// </summary>
    public class Enclosure
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public int Length { get; set; }
    }
}