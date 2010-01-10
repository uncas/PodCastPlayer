//-------------
// <copyright file="EpisodeStream.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Utility
{
    using System.IO;

    /// <summary>
    /// Represents an episode stream.
    /// </summary>
    public class EpisodeStream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeStream"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="length">The length.</param>
        public EpisodeStream(
            Stream stream,
            long length)
        {
            this.Stream = stream;
            this.Length = length;
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>The stream.</value>
        public Stream Stream { get; private set; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>The length.</value>
        public long Length { get; private set; }
    }
}