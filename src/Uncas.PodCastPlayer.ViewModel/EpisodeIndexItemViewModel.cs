//-------------
// <copyright file="EpisodeIndexItemViewModel.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.ViewModel
{
    using System;

    /// <summary>
    /// Represents a view of an episode item.
    /// </summary>
    public class EpisodeIndexItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeIndexItemViewModel"/> class.
        /// </summary>
        public EpisodeIndexItemViewModel()
        {
            this.Date = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date of the episode.</value>
        public DateTime Date { get; set; }
    }
}