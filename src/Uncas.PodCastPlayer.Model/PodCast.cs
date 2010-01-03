//-------------
// <copyright file="PodCast.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents a pod cast.
    /// </summary>
    public class PodCast : Entity
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCast"/> class.
        /// </summary>
        /// <param name="id">The id of the pod cast.</param>
        /// <param name="name">The name of the pod cast.</param>
        /// <param name="url">The URL of the pod cast.</param>
        public PodCast(
            int? id,
            string name,
            Uri url)
            : base(id)
        {
            this.Episodes = new List<Episode>();
            this.Name = name;
            this.Url = url;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCast"/> class.
        /// </summary>
        /// <param name="id">The id of the pod cast.</param>
        /// <param name="name">The name of the pod cast.</param>
        /// <param name="url">The URL of the pod cast.</param>
        /// <param name="description">The description.</param>
        /// <param name="author">The author.</param>
        public PodCast(
            int? id,
            string name,
            Uri url,
            string description,
            string author)
            : base(id)
        {
            this.Author = author;
            this.Description = description;
            this.Episodes = new List<Episode>();
            this.Name = name;
            this.Url = url;
        }

        #endregion

        #region Public properties

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

        #endregion

        #region Public methods

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "Id={0}, Name={1}, Url={2}, Episodes={3}",
                this.Id,
                this.Name,
                this.Url,
                this.Episodes.Count);
        }

        #endregion
    }
}