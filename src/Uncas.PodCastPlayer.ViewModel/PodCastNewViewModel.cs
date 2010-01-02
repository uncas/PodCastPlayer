//-------------
// <copyright file="PodCastNewViewModel.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.ViewModel
{
    using System;

    /// <summary>
    /// Represents a view of a new pod cast.
    /// </summary>
    public class PodCastNewViewModel
    {
        /// <summary>
        /// Gets or sets the pod cast id.
        /// </summary>
        /// <value>The pod cast id.</value>
        public int? PodCastId { get; set; }

        /// <summary>
        /// Gets or sets the pod cast URL.
        /// </summary>
        /// <value>The pod cast URL.</value>
        public Uri PodCastUrl { get; set; }
    }
}