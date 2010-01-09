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
        /// Gets as a model pod cast.
        /// </summary>
        /// <returns>The model pod cast.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Repository.RepositoryException"></exception>
        public PodCast AsModelPodCast()
        {
            try
            {
                return new PodCast(
                    (int)this.PodCastId,
                    this.Name,
                    new Uri(this.Url),
                    this.Description,
                    this.Author);
            }
            catch (ArgumentNullException ex)
            {
                throw GetInvalidPodCastUrlException(ex);
            }
            catch (UriFormatException ex)
            {
                throw GetInvalidPodCastUrlException(ex);
            }
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