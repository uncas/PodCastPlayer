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
    using System.Linq;
    using System.Net;
    using System.Security;
    using System.ServiceModel.Syndication;
    using System.Xml;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Handles downloads of pod casts.
    /// </summary>
    /// <remarks>See http://www.codeproject.com/KB/IP/MyDownloader.aspx for more deails.</remarks>
    public class PodCastDownloader : IPodCastDownloader
    {
        #region IPodCastDownloader Members

        /// TODO: OBSOLETE: Make AppService use IPodCastDownloader and IEpisodeSaver
        /// <summary>
        /// Downloads and saves the episode media in a file.
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
            // Gets stream:
            EpisodeStream podCastStream =
                GetEpisodeStream(episode.MediaUrl);

            // Saves stream:
            EpisodeSaver saver = new EpisodeSaver();
            int downloadedBytes =
                saver.SaveStream(
                fileName,
                podCastStream.Length,
                podCastStream.Stream);

            // Returns info about how it all went:
            return new EpisodeMediaInfo
            {
                FileSizeInBytes = podCastStream.Length,
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
            return FetchEpisodeList(
                podCast);
        }

        /// <summary>
        /// Downloads the pod cast info.
        /// </summary>
        /// <param name="podCastUrl">The pod cast URL.</param>
        /// <returns>Details about the pod cast.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        public PodCast DownloadPodCastInfo(
            Uri podCastUrl)
        {
            var feed = GetFeed(podCastUrl);

            string author = null;
            if (feed.Authors.Count > 0)
            {
                author = feed.Authors.First().Name;
            }

            return new PodCast(
                null,
                feed.Title.Text,
                podCastUrl,
                feed.Description.Text,
                author);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Reads the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <returns>A list of episodes.</returns>
        private static IList<Episode> FetchEpisodeList(
            PodCast podCast)
        {
            Uri podCastUrl = podCast.Url;

            var result = new List<Episode>();

            // Loads the pod cast:
            SyndicationFeed feed = GetFeed(podCastUrl);
            foreach (SyndicationItem item in feed.Items)
            {
                // Gets enclosure info:
                var enclosure = item.Links.Where(
                    l => l.RelationshipType == "enclosure")
                    .SingleOrDefault();
                if (enclosure == null)
                {
                    continue;
                }

                // Gets episode info:
                Episode episode =
                    Episode.ConstructEpisode(
                    item.Id,
                    item.PublishDate.Date,
                    item.Title.Text,
                    item.Summary.Text,
                    enclosure.Uri,
                    podCast,
                    false);

                episode.MediaInfo =
                    new EpisodeMediaInfo
                    {
                        FileSizeInBytes = enclosure.Length
                    };

                result.Add(episode);
            }

            return result;
        }

        /// <summary>
        /// Gets the episode stream.
        /// </summary>
        /// <param name="mediaUrl">The media URL.</param>
        /// <returns>The episode stream.</returns>
        private static EpisodeStream GetEpisodeStream(
            Uri mediaUrl)
        {
            var webRequest =
                (HttpWebRequest)WebRequest.Create(
                mediaUrl);

            // Set default authentication for retrieving the file
            webRequest.Credentials =
                CredentialCache.DefaultCredentials;

            // Retrieve the response from the server
            var webResponse =
                (HttpWebResponse)webRequest.GetResponse();

            // Ask the server for the file size and store it
            long fileSize = webResponse.ContentLength;

            var responseStream = webResponse.GetResponseStream();
            EpisodeStream podCastStream =
                new EpisodeStream(
                responseStream,
                fileSize);
            return podCastStream;
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        /// <returns>The utility exception.</returns>
        private static UtilityException GetException(
            Exception innerException)
        {
            return new UtilityException(
                "Exception in PodCastDownloader",
                innerException);
        }

        /// <summary>
        /// Gets the feed.
        /// </summary>
        /// <param name="podCastUrl">The pod cast URL.</param>
        /// <returns>The retrieved feed.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        private static SyndicationFeed GetFeed(
           Uri podCastUrl)
        {
            SyndicationFeed feed = null;

            // Loads the pod cast:
            try
            {
                using (XmlReader reader =
                    XmlReader.Create(podCastUrl.ToString()))
                {
                    feed = SyndicationFeed.Load(reader);
                }
            }
            catch (ArgumentNullException ex)
            {
                throw GetException(ex);
            }
            catch (SecurityException ex)
            {
                throw GetException(ex);
            }
            catch (FileNotFoundException ex)
            {
                throw GetException(ex);
            }
            catch (UriFormatException ex)
            {
                throw GetException(ex);
            }

            return feed;
        }
        
        #endregion
    }
}