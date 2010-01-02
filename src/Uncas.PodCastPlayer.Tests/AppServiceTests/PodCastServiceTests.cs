﻿//-------------
// <copyright file="PodCastServiceTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.AppServiceTests
{
    using System;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.AppServices;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Tests the pod cast service.
    /// </summary>
    [TestFixture]
    public class PodCastServiceTests : BaseTest
    {
        /// <summary>
        /// The service.
        /// </summary>
        private readonly PodCastService service =
            new PodCastService(
                TestApp.Repositories,
                TestApp.PodCastDownloader);

        /// <summary>
        /// Deletes the pod cast_1_ OK.
        /// </summary>
        [Test]
        public void DeletePodCast_1_OK()
        {
            // Arrange:

            // Act:
            this.service.DeletePodCast(1);

            // Assert:
        }

        /// <summary>
        /// Gets the pod casts_ all_ OK.
        /// </summary>
        [Test]
        public void GetPodCasts_All_OK()
        {
            // Arrange:

            // Act:
            this.service.GetPodCasts();

            // Assert:
        }

        /// <summary>
        /// Retrieves the pod cast info.
        /// </summary>
        [Test]
        public void RetrievePodCastInfo()
        {
            // Arrange:
            var url = new Uri("http://rss.conversationsnetwork.org/series/stackoverflow.xml");

            // Act:
            var info = this.service.RetrievePodCastInfo(url);

            // Assert:
            Assert.IsNotNull(info);
        }

        /// <summary>
        /// Saves the existing pod cast.
        /// </summary>
        [Test]
        public void SavePodCast_Existing_OK()
        {
            // Arrange:
            var podCast =
                new PodCastDetailsViewModel(
                    1,
                    null);

            // Act:
            this.service.SavePodCast(podCast);

            // Assert:
        }

        /// <summary>
        /// Saves the new pod cast.
        /// </summary>
        [Test]
        public void SavePodCast_New_OK()
        {
            // Arrange:
            Uri url = new Uri("http://xxx.ddd.wweerr");
            var podCast =
                new PodCastDetailsViewModel(
                    null,
                    url);

            // Act:
            this.service.SavePodCast(podCast);

            // Assert:
        }
    }
}