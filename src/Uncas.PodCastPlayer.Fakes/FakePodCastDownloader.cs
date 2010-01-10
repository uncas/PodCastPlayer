//-------------
// <copyright file="FakePodCastDownloader.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// Fakes download of pod casts.
    /// </summary>
    public class FakePodCastDownloader : IPodCastDownloader
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
            var result = new List<Episode>();
            result.AddRange(podCast.Episodes);
            result.Add(Episode.ConstructEpisode(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "x",
                "y",
                new Uri("http://www.xxx.ddd/asdasd.mp3"),
                podCast,
                false));
            return result;
        }

        /// <summary>
        /// Downloads the pod cast info.
        /// </summary>
        /// <param name="podCastUrl">The pod cast URL.</param>
        /// <returns>Details about the pod cast.</returns>
        public PodCast DownloadPodCastInfo(
            Uri podCastUrl)
        {
            return new PodCast(null, "X", podCastUrl);
        }

        /// <summary>
        /// Gets the episode stream.
        /// </summary>
        /// <param name="mediaUrl">The media URL.</param>
        /// <returns>The episode stream.</returns>
        public EpisodeMedia GetEpisodeStream(
            Uri mediaUrl)
        {
            Stream stream = new MemoryStream();
            string textContent = "abcdefg";
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(textContent);
            return new EpisodeMedia(
                stream,
                textContent.Length);
        }

        #endregion
    }
}