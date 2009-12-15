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
    public class PodCast : Entity
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCast"/> class.
        /// </summary>
        /// <param name="id">The id of the pod cast.</param>
        /// <param name="name">The name of the pod cast.</param>
        /// <param name="url">The URL of the pod cast.</param>
        /// <param name="numberToDownload">The number of episodes to download.</param>
        public PodCast(
            int? id,
            string name,
            Uri url,
            int? numberToDownload)
            : base(id)
        {
            this.Episodes = new List<Episode>();
            this.Name = name;
            this.NumberToDownload = numberToDownload;
            this.Url = url;
        }

        #endregion

        #region Public properties

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
        /// Gets or sets the number of episodes to download.
        /// </summary>
        /// <value>The number of episodes to download.</value>
        public int? NumberToDownload { get; set; }

        /// <summary>
        /// Gets or sets the URL to the pod cast.
        /// </summary>
        /// <value>The URL to the pod cast.</value>
        public Uri Url { get; set; }

        #endregion
    }
}