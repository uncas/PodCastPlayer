//-------------
// <copyright file="PodCastDetailsViewModel.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.ViewModel
{
    using System;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Holds info for a detailed view of a pod cast.
    /// </summary>
    public class PodCastDetailsViewModel : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastDetailsViewModel"/> class.
        /// </summary>
        /// <param name="id">The id of the pod cast.</param>
        /// <param name="name">The name of the pod cast.</param>
        /// <param name="url">The URL of the pod cast.</param>
        /// <param name="author">The author.</param>
        /// <param name="description">The description.</param>
        public PodCastDetailsViewModel(
            int? id,
            string name,
            Uri url,
            string author,
            string description)
            : base(id)
        {
            this.Author = author;
            this.Description = description;
            this.Name = name;
            this.Url = url;
        }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

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