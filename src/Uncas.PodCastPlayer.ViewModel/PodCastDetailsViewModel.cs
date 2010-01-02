//-------------
// <copyright file="PodCastDetailsViewModel.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Holds info for a detailed view of a pod cast.
    /// </summary>
    public class PodCastDetailsViewModel : Entity
    {
        // TODO: REFACTOR: Use static constructors...

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastDetailsViewModel"/> class.
        /// </summary>
        /// <param name="id">The id of the pod cast.</param>
        /// <param name="url">The URL of the pod cast.</param>
        public PodCastDetailsViewModel(
            int? id,
            Uri url)
            : base(id)
        {
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
