//-------------
// <copyright file="Entity.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a basic entity.
    /// </summary>
    public abstract class Entity
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        protected Entity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        protected Entity(int? id)
        {
            this.Id = id;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id of the entity.</value>
        public int? Id { get; set; }

        #endregion
    }
}