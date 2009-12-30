//-------------
// <copyright file="PodCastDownloader.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Utility
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using Uncas.PodCastPlayer.Model;
    using System.Diagnostics;

    /// <summary>
    /// Handles downloads of pod casts.
    /// </summary>
    public class PodCastDownloader : IPodCastDownloader
    {
        #region IPodCastDownloader Members

        // The stream of data retrieved from the web server
        private Stream strResponse;
        // The stream of data that we write to the harddrive
        private Stream strLocal;
        // The request to the web server for file information
        private HttpWebRequest webRequest;
        // The response from the web server containing information about the file
        private HttpWebResponse webResponse;
        // The progress of the download in percentage
        //private static int PercentProgress;

        /// <summary>
        /// Downloads the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        public void DownloadEpisode(Episode episode)
        {
            // TODO: FEATURE: Implement proper file path:
            string fileName = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "test1.mp3");

            this.webRequest =
                (HttpWebRequest)WebRequest.Create(
                episode.MediaUrl);
            // Set default authentication for retrieving the file
            webRequest.Credentials =
                CredentialCache.DefaultCredentials;
            // Retrieve the response from the server
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            // Ask the server for the file size and store it
            Int64 fileSize = webResponse.ContentLength;
            Trace.WriteLine(fileSize);

            // Open the URL for download
            using (WebClient wcDownload = new WebClient())
            {
                strResponse = wcDownload.OpenRead(episode.MediaUrl);
                // Create a new file stream where we will be saving the data (local drive)
                strLocal = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            }

            // It will store the current number of bytes we retrieved from the server
            int bytesSize = 0;
            // A buffer for storing and writing the data retrieved from the server
            byte[] downBuffer = new byte[2048];

            int bytesTotal = 0;

            // Loop through the buffer until the buffer is empty
            while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
            {
                // Write the data from the buffer to the local hard drive
                strLocal.Write(downBuffer, 0, bytesSize);
                bytesTotal += bytesSize;
                Trace.WriteLine(
                    string.Format(
                        "{0}: {1}/{2} = {3:P1}",
                        bytesSize,
                        bytesTotal,
                        fileSize,
                        (1D * bytesTotal) / (1D * fileSize)));
            }

            // TODO: OPTIMIZE: Download async + in pieces.
            /*using (WebClient client = new WebClient())
            {
                client.DownloadFile(
                    episode.MediaUrl.AbsoluteUri,
                    fileName);
            }*/
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
    }
}