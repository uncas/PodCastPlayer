//-------------
// <copyright file="FakeEpisodeSaver.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Fakes
{
    using System.IO;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// Fake implementation of episode saving.
    /// </summary>
    public class FakeEpisodeSaver : IEpisodeSaver
    {
        #region IEpisodeSaver Members

        /// <summary>
        /// Saves the stream.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileSize">Size of the file.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>The number of bytes saved.</returns>
        public long SaveStream(
            string filePath,
            long fileSize,
            Stream stream)
        {
            return fileSize;
        }

        #endregion
    }
}