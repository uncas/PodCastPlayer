//-------------
// <copyright file="Episode.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.SQLiteRepository
{
    using SubSonic.SqlGeneration.Schema;

    /// <summary>
    /// Represents the episode in the database.
    /// </summary>
    public class Episode
    {
        /// <summary>
        /// Gets or sets the episode id.
        /// </summary>
        /// <value>The episode id.</value>
        [SubSonicPrimaryKey]
        public string EpisodeId { get; set; }

        /// <summary>
        /// Gets or sets the ref pod cast id.
        /// </summary>
        /// <value>The ref pod cast id.</value>
        public int RefPodCastId { get; set; }
    }
}