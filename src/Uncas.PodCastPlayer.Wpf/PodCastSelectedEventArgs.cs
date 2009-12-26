//-------------
// <copyright file="PodCastSelectedEventArgs.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Holds info about the selected pod cast.
    /// </summary>
    internal class PodCastSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the pod cast.
        /// </summary>
        /// <value>The pod cast.</value>
        public PodCastIndexViewModel PodCast { get; set; }
    }
}