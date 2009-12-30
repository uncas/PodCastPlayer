//-------------
// <copyright file="EpisodeIndexViewModel.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.ViewModel
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a view of an episode index.
    /// </summary>
    public class EpisodeIndexViewModel
    {
        /// <summary>
        /// Gets or sets the name of the pod cast.
        /// </summary>
        /// <value>The name of the pod cast.</value>
        public string PodCastName { get; set; }

        /// <summary>
        /// Gets or sets the episodes.
        /// </summary>
        /// <value>The episodes.</value>
        public IEnumerable<EpisodeIndexItemViewModel> Episodes
        {
            get;
            set;
        }
    }
}