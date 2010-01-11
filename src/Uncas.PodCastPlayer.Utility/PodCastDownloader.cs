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
            if (podCastUrl == null)
            {
                return null;
            }

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

        /// <summary>
        /// Gets the episode stream.
        /// </summary>
        /// <param name="mediaUrl">The media URL.</param>
        /// <returns>The episode stream.</returns>
        public EpisodeMedia GetEpisodeStream(
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
            return new EpisodeMedia(
                responseStream,
                fileSize);
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
            catch (XmlException ex)
            {
                throw GetException(ex);
            }
            catch (WebException ex)
            {
                throw GetException(ex);
            }

            return feed;
        }

        #endregion
    }
}