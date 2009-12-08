//-------------
// <copyright file="IPodCastRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Repository
{
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Handles storage of pod cast info.
    /// </summary>
    public interface IPodCastRepository
    {
        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        void SavePodCast(PodCast podCast);
    }
}