//-------------
// <copyright file="FakePodCastRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.Fakes
{
    using System.Collections.Generic;
    using System.Linq;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Fakes storage of pod cast info and media.
    /// </summary>
    internal class FakePodCastRepository : IPodCastRepository
    {
        #region Private fields and properties

        /// <summary>
        /// The pod casts.
        /// </summary>
        private static List<PodCast> podCasts;

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

                    podCasts.Add(
                        new PodCast(
                            "Hanselminutes",
                            null,
                            3));
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
                new PodCastIndexViewModel());
            return result.ToList();
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public void SavePodCast(PodCast podCast)
        {
            PodCasts.Add(podCast);
        }

        #endregion
    }
}