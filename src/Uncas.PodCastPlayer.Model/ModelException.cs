﻿//-------------
// <copyright file="ModelException.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Model
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception from repository.
    /// </summary>
    [Serializable]
    public class ModelException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelException"/> class.
        /// </summary>
        public ModelException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ModelException(
            string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ModelException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected ModelException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}