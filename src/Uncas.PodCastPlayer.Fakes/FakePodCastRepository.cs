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
        private static List<PodCast> PodCasts
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

                    podCasts.Add(podCast);

                    var uri2 = new Uri(
                        "http://podcast.stackoverflow.com");
                    var podCast2
                        = new PodCast(
                            2,
                            "StackOverflow",
                            uri2,
                            4);
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

            var newPodCast =
                new PodCast(
                    null,
                    podCast.Name,
                    podCast.Url,
                    null);
            PodCasts.Add(newPodCast);
        }

        #endregion
    }
}