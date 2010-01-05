//-------------
// <copyright file="DBPodCast.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using SubSonic.SqlGeneration.Schema;

    /// <summary>
    /// Represents a pod cast in the database.
    /// </summary>
    [SubSonicTableNameOverride("PodCasts")]
    public class DBPodCast
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
    }
}