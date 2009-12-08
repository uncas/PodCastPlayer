//-------------
// <copyright file="PodCast.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a pod cast.
    /// </summary>
    public class PodCast
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PodCast"/> class.
        /// </summary>
        public PodCast()
        {
            this.Episodes = new List<Episode>();
        }

        /// <summary>
        /// Gets the episodes.
        /// </summary>
        /// <value>The episodes.</value>
        public IList<Episode> Episodes { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the pod cast.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL to the pod cast.
        /// </summary>
        /// <value>The URL to the pod cast.</value>
        public Uri Url { get; set; }
    }
}