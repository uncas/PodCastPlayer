//-------------
// <copyright file="AppTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.WpfTests
{
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Wpf;

    /// <summary>
    /// Tests of the App.
    /// </summary>
    [TestFixture]
    public class AppTests
    {
        /// <summary>
        /// The wpf app.
        /// </summary>
        private App app = 
            new App(
                TestApp.Repositories,
                TestApp.PodCastDownloader,
                TestApp.EpisodeSaver);

        /// <summary>
        /// App_s the fake_ OK.
        /// </summary>
        [Test]
        public void Constructor_Fake_OK()
        {
            // Arrange:

            // Act:

            // Assert:
        }

        /// <summary>
        /// Dispose_s the fake_ OK.
        /// </summary>
        [Test]
        public void Dispose_Fake_OK()
        {
            // Arrange:

            // Act:
            this.app.Dispose();

            // Assert:
        }
    }
}