//-------------
// <copyright file="Episode.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Model
{
    using System;

    /// <summary>
    /// Represents an episode of a pod cast.
    /// </summary>
    public class Episode
    {
        /// <summary>
        /// Gets or sets the date of the episode.
        /// </summary>
        /// <value>The date of the episode.</value>
        public DateTime Date { get; set; }
    }
}