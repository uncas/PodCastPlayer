//-------------
// <copyright file="DBPodCast.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using SubSonic.SqlGeneration.Schema;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Represents a pod cast in the database.
    /// </summary>
    [SubSonicTableNameOverride("PodCasts")]
    internal class DBPodCast
    {
        #region Public properties

        /// <summary>
        /// Gets or sets the pod cast id.
        /// </summary>
        /// <value>The pod cast id.</value>
        [SubSonicPrimaryKey]
        public long PodCastId { get; set; }

        /// <summary>
        /// Gets or sets the URL to the pod cast.
        /// </summary>
        /// <value>The URL to the pod cast.</value>
        [SuppressMessage("Microsoft.Design",
            "CA1056:UriPropertiesShouldNotBeStrings",
            Justification = "Uri property as string for database.")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the pod cast.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        [SubSonicNullString]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [SubSonicLongString]
        [SubSonicNullString]
        public string Description { get; set; }

        #endregion

        /// <summary>
        /// Gets the URL as URI.
        /// </summary>
        /// <value>The URL as URI.</value>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        [SubSonicIgnore]
        private Uri UrlAsUri
        {
            get
            {
                Uri url = null;
                try
                {
                    url = new Uri(this.Url);
                }
                catch (ArgumentNullException ex)
                {
                    throw GetInvalidPodCastUrlException(ex);
                }
                catch (UriFormatException ex)
                {
                    throw GetInvalidPodCastUrlException(ex);
                }

                return url;
            }
        }

        /// <summary>
        /// Gets as a details view model.
        /// </summary>
        /// <returns>The details view model.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public PodCastDetailsViewModel AsDetailsViewModel()
        {
            return new PodCastDetailsViewModel(
                (int)this.PodCastId,
                this.Name,
                this.UrlAsUri,
                this.Author,
                this.Description);
        }

        /// <summary>
        /// Gets as an index view model.
        /// </summary>
        /// <returns>The index view model.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public PodCastIndexViewModel AsIndexViewModel()
        {
            return new PodCastIndexViewModel(
                (int)this.PodCastId,
                this.Name,
                this.UrlAsUri);
        }

        /// <summary>
        /// Gets as a model pod cast.
        /// </summary>
        /// <returns>The model pod cast.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public PodCast AsModel()
        {
            return new PodCast(
                (int)this.PodCastId,
                this.Name,
                this.UrlAsUri,
                this.Description,
                this.Author);
        }

        /// <summary>
        /// Gets the invalid pod cast URL exception.
        /// </summary>
        /// <param name="ex">The original exception.</param>
        /// <returns>The repository exception.</returns>
        private static RepositoryException
            GetInvalidPodCastUrlException(Exception ex)
        {
            return new RepositoryException(
                "Invalid pod cast url in database",
                ex);
        }
    }
}