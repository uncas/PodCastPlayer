//-------------
// <copyright file="DownloadIndexViewModelTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.ViewModelTests
{
    using System;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Tests the download index view model.
    /// </summary>
    [TestFixture]
    public class DownloadIndexViewModelTests
    {
        /// <summary>
        /// Froms the episode_ x_ OK.
        /// </summary>
        [Test]
        public void FromEpisode_X_OK()
        {
            // Arrange:
            Episode episode =
                Episode.ConstructEpisode(
                "x",
                DateTime.Now,
                "x",
                "x",
                new Uri("http://xxx.ddd"),
                new PodCast(1, "x", null),
                true);

            // Act:
            DownloadIndexViewModel.FromEpisode(episode);

            // Assert:
        }
    }
}