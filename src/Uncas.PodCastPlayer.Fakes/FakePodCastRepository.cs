//-------------
// <copyright file="FakePodCastRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Fakes storage of pod cast info and media.
    /// </summary>
    public class FakePodCastRepository : IPodCastRepository
    {
        #region Private fields

        /// <summary>
        /// The pod casts.
        /// </summary>
        private static List<PodCast> podCasts;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FakePodCastRepository"/> class.
        /// </summary>
        internal FakePodCastRepository()
        {
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        /// <value>The pod casts.</value>
        internal static List<PodCast> PodCasts
        {
            get
            {
                if (podCasts == null)
                {
                    podCasts = new List<PodCast>();

                    var uri = new Uri(
                        "http://www.hanselminutes.com");
                    var podCast
                        = new PodCast(
                            1,
                            "Hanselminutes",
                            uri,
                            3);

                    podCast.Episodes.Add(
                        new Episode
                        {
                            PendingDownload = true,
                            PodCast = podCast,
                            Id = "01",
                            MediaUrl = new Uri(
                                "http://perseus.franklins.net/hanselminutes_0079.mp3")
                        });
                    podCast.Episodes.Add(
                        new Episode
                        {
                            PodCast = podCast,
                            Id = "02"
                        });

                    podCasts.Add(podCast);

                    var uri2 = new Uri(
                        "http://podcast.stackoverflow.com");
                    var podCast2
                        = new PodCast(
                            2,
                            "StackOverflow",
                            uri2,
                            4);

                    podCast2.Episodes.Add(
                        new Episode
                        {
                            PodCast = podCast,
                            Id = "11"
                        });
                    podCast2.Episodes.Add(
                        new Episode
                        {
                            PodCast = podCast,
                            Id = "12"
                        });
                    podCast2.Episodes.Add(
                        new Episode
                        {
                            PodCast = podCast,
                            Id = "13"
                        });

                    podCasts.Add(podCast2);
                }

                return podCasts;
            }
        }

        #endregion

        #region IPodCastRepository Members

        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        /// <returns>A list of pod casts.</returns>
        public IList<PodCastIndexViewModel> GetPodCasts()
        {
            var result
                = PodCasts
                .Select(pc =>
                new PodCastIndexViewModel(
                    pc.Id,
                    pc.Name,
                    pc.Url));
            return result.ToList();
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public void SavePodCast(PodCastIndexViewModel podCast)
        {
            if (podCast.Id.HasValue)
            {
                var existingPodCast =
                    PodCasts.Where(pc =>
                        pc.Id.Value == podCast.Id.Value)
                    .SingleOrDefault();
                if (existingPodCast != null)
                {
                    existingPodCast.Name = podCast.Name;
                    existingPodCast.Url = podCast.Url;
                    return;
                }
            }

            int newId =
                (PodCasts.Max(pc => pc.Id) ?? 0)
                + 1;

            var newPodCast =
                new PodCast(
                    newId,
                    podCast.Name,
                    podCast.Url,
                    null);
            PodCasts.Add(newPodCast);
        }

        /// <summary>
        /// Deletes the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        public void DeletePodCast(int podCastId)
        {
            var podCast =
                PodCasts.Where(
                pc => pc.Id == podCastId)
                .SingleOrDefault();
            if (podCast == null)
            {
                return;
            }

            PodCasts.Remove(podCast);
        }

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        public PodCast GetPodCast(int podCastId)
        {
            return PodCasts.Where(
                pc => pc.Id == podCastId)
                .SingleOrDefault();
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Gets the episodes by pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>An index of episodes.</returns>
        internal static EpisodeIndexViewModel GetEpisodesByPodCast(
            int podCastId)
        {
            var podCast =
                PodCasts.Where(
                pc => pc.Id == podCastId)
                .SingleOrDefault();
            if (podCast == null)
            {
                return null;
            }

            var episodes = podCast.Episodes
                .Select(e => new EpisodeIndexItemViewModel
                {
                    Date = e.Date
                });
            return new EpisodeIndexViewModel
            {
                PodCastName = podCast.Name,
                Episodes = episodes
            };
        }

        /// <summary>
        /// Updates the episode list.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <param name="episodes">The episodes.</param>
        internal static void UpdateEpisodeList(
            int podCastId,
            IList<Episode> episodes)
        {
            var podCast =
                PodCasts.Where(
                pc => pc.Id == podCastId)
                .SingleOrDefault();
            if (podCast == null)
            {
                return;
            }

            podCast.Episodes.Clear();
            foreach (var episode in episodes)
            {
                podCast.Episodes.Add(episode);
            }
        }

        #endregion
    }
}