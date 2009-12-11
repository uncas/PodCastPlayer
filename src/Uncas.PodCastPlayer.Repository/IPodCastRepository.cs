//-------------
// <copyright file="IPodCastRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Repository
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Handles storage of pod cast info.
    /// </summary>
    public interface IPodCastRepository
    {
        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        /// <returns>A list of pod casts.</returns>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Reads from repository; might be expensive.")]
        IList<PodCast> GetPodCasts();

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        void SavePodCast(PodCast podCast);
    }
}