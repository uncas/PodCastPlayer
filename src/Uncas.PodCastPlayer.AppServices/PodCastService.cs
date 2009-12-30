//-------------
// <copyright file="PodCastService.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.AppServices
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Service for pod casts.
    /// </summary>
    public class PodCastService
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IPodCastRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public PodCastService(IPodCastRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        /// <returns>A list of pod casts.</returns>
        [SuppressMessage(
           "Microsoft.Design",
           "CA1024:UsePropertiesWhereAppropriate",
           Justification = "Reads from repository; might be expensive.")]
        public IList<PodCastIndexViewModel> GetPodCasts()
        {
            return this.repository.GetPodCasts();
        }

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public void SavePodCast(
            PodCastIndexViewModel podCast)
        {
            this.repository.SavePodCast(podCast);
        }

        /// <summary>
        /// Deletes the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        public void DeletePodCast(int podCastId)
        {
            this.repository.DeletePodCast(podCastId);
        }
    }
}