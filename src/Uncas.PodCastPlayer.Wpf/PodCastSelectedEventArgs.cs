//-------------
// <copyright file="PodCastSelectedEventArgs.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System;

    /// <summary>
    /// Holds info about the selected pod cast.
    /// </summary>
    internal class PodCastSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastSelectedEventArgs"/> class.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        public PodCastSelectedEventArgs(int? podCastId)
        {
            this.PodCastId = podCastId;
        }

        /// <summary>
        /// Gets or sets the pod cast id.
        /// </summary>
        /// <value>The pod cast id.</value>
        public int? PodCastId { get; set; }
    }
}