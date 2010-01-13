//-------------
// <copyright file="IEpisodeSaver.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Utility
{
    using System.IO;

    /// <summary>
    /// Handles saving episode media.
    /// </summary>
    public interface IEpisodeSaver
    {
        /// <summary>
        /// Saves the stream.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileSize">Size of the file.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>The number of bytes saved.</returns>
        /// <exception cref="Uncas.PodCastPlayer.Utility.UtilityException"></exception>
        long SaveStream(
            string filePath,
            long fileSize,
            Stream stream);
    }
}