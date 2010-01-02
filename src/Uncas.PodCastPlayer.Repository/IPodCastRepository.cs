//-------------
// <copyright file="IPodCastRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Handles storage of pod cast info.
    /// </summary>
    public interface IPodCastRepository
    {
        /// <summary>
        /// Deletes the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        void DeletePodCast(int podCastId);

        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        /// <returns>The pod casts.</returns>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Expensive read from repository.")]
        IList<PodCastIndexViewModel> GetPodCasts();

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        PodCast GetPodCast(int podCastId);

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        [Obsolete("Use overload with PodCastDetailsViewModel")]
        void SavePodCast(PodCastIndexViewModel podCast);

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        void SavePodCast(PodCastDetailsViewModel podCast);
    }
}