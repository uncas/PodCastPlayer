//-------------
// <copyright file="PodCastIndexViewModel.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.ViewModel
{
    using System;

    /// <summary>
    /// Represents data for the index of pod casts.
    /// </summary>
    public class PodCastIndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastIndexViewModel"/> class.
        /// </summary>
        /// <param name="name">The name of the pod cast.</param>
        /// <param name="url">The URL of the pod cast.</param>
        public PodCastIndexViewModel(
            string name,
            Uri url)
        {
            this.Name = name;
            this.Url = url;
        }

        /// <summary>
        /// Gets or sets the name of the pod cast.
        /// </summary>
        /// <value>The name of the pod cast.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL of the pod cast.</value>
        public Uri Url { get; set; }
    }
}