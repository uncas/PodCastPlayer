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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        void DeletePodCast(int podCastId);

        /// <summary>
        /// Gets the pod casts.
        /// </summary>
        /// <returns>The pod casts.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
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
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        PodCast GetPodCast(int podCastId);

        /// <summary>
        /// Gets the pod cast.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        /// <returns>The pod cast.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        PodCastDetailsViewModel GetPodCastDetails(int podCastId);

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        void SavePodCast(PodCast podCast);

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        void SavePodCast(PodCastDetailsViewModel podCast);
    }
}