//-------------
// <copyright file="FakePodCastRepository.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.Fakes
{
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Fakes storage of pod cast info and media.
    /// </summary>
    internal class FakePodCastRepository : IPodCastRepository
    {
        #region IPodCastRepository Members

        /// <summary>
        /// Saves the pod cast.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public void SavePodCast(PodCast podCast)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}