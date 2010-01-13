//-------------
// <copyright file="PodCastDownloader.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Xml;
    using Model;

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
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        public IList<Episode> DownloadEpisodeList(
            PodCast podCast)
        {
            if (podCast == null)
            {
                return null;
            }

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
            if (feed == null)
            {
                return null;
            }

            string author = null;
            if (feed.Authors != null
                && feed.Authors.Count > 0)
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
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        public EpisodeMedia GetEpisodeStream(
            Uri mediaUrl)
        {
            if (mediaUrl == null)
            {
                return null;
            }

            var webRequest =
                (HttpWebRequest)WebRequest.Create(
                mediaUrl);

            // Set default authentication for retrieving the file
            webRequest.Credentials =
                CredentialCache.DefaultCredentials;

            // Retrieve the response from the server
            HttpWebResponse webResponse;
            try
            {
                webResponse =
                    (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException ex)
            {
                throw new UtilityException(
                    "Error retrieving response.",
                    ex);
            }

            // Ask the server for the file size and store it
            var fileSize = webResponse.ContentLength;

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
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        private static IList<Episode> FetchEpisodeList(
            PodCast podCast)
        {
            var result = new List<Episode>();

            // Loads the pod cast:
            var feed = GetFeed(podCast.Url);
            foreach (var item in feed.Items)
            {
                // Gets enclosure info:
                var enclosure =
                    item.Links.Where(
                    l => l.RelationshipType == "enclosure")
                    .SingleOrDefault();
                if (enclosure == null)
                {
                    continue;
                }

                // Gets episode info:
                var episode =
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
            // Loads the pod cast:
            try
            {
                using (var reader =
                    XmlReader.Create(podCastUrl.ToString()))
                {
                    return SyndicationFeed.Load(reader);
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
        }

        #endregion
    }
}