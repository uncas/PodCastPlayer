//-------------
// <copyright file="EpisodeSaverTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests.UtilityTests
{
    using System;
    using System.IO;
    using System.Text;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// Tests the episode saver.
    /// </summary>
    [TestFixture]
    public class EpisodeSaverTests
    {
        /// <summary>
        /// The episode saver.
        /// </summary>
        private readonly IEpisodeSaver saver = new EpisodeSaver();

        /// <summary>
        /// Tests saving the stream from memory.
        /// </summary>
        [Test]
        public void SaveStream_Memory_OK()
        {
            // Arrange:
            string textContent = "abcdefg";
            Stream stream = GetStream(textContent);
            string filePath =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "test.txt");

            // Act:
            this.saver.SaveStream(
                filePath,
                textContent.Length,
                stream);

            // Assert:
            // TODO: TEST: Make sure this is OK, by checking file?!
        }

        /// <summary>
        /// Tests saving the stream from memory.
        /// </summary>
        [Test]
        public void SaveStream_NewFolder_OK()
        {
            // Arrange:
            string textContent = "abcdefg";
            Stream stream = GetStream(textContent);
            string folderPath =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    Guid.NewGuid().ToString());
            string filePath =
                Path.Combine(
                    folderPath,
                    "test.txt");

            // Act:
            this.saver.SaveStream(
                filePath,
                textContent.Length,
                stream);

            // Assert:
            // TODO: TEST: Make sure this is OK, by checking file?!

            // Clean up:
            Directory.Delete(folderPath, true);
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <param name="textContent">Content of the text.</param>
        /// <returns>The stream.</returns>
        private static Stream GetStream(
            string textContent)
        {
            Stream stream =
                new MemoryStream(
                    ASCIIEncoding.Default.GetBytes(
                        textContent));
            return stream;
        }
    }
}