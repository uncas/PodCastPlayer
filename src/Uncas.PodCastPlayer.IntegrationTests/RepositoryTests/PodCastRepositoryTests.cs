//-------------
// <copyright file="PodCastRepositoryTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests.RepositoryTests
{
    using System;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Testing pod cast repository.
    /// </summary>
    [TestFixture]
    public class PodCastRepositoryTests
    {
        /// <summary>
        /// Gets the pod cast repository.
        /// </summary>
        /// <value>The pod cast repository.</value>
        private IPodCastRepository PodCastRepository
        {
            get
            {
                return TestApp.RealRepositories.PodCastRepository;
            }
        }

        /// <summary>
        /// Deletes the pod cast_0_ OK.
        /// </summary>
        [Test]
        public void DeletePodCast_0_OK()
        {
            // Arrange:

            // Act:
            this.PodCastRepository.DeletePodCast(0);

            // Assert:
        }

        /// <summary>
        /// Gets the pod cast_1_ OK.
        /// </summary>
        [Test]
        public void GetPodCast_1_OK()
        {
            // Arrange:

            // Act:
            this.PodCastRepository.GetPodCast(1);

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
            this.PodCastRepository.GetPodCasts();

            // Assert:
        }

        /// <summary>
        /// Gets the pod cast details_1_ OK.
        /// </summary>
        [Test]
        public void GetPodCastDetails_1_OK()
        {
            // Arrange:

            // Act:
            this.PodCastRepository.GetPodCastDetails(1);

            // Assert:
        }

        /// <summary>
        /// Saves the pod cast_ non existing_ OK.
        /// </summary>
        [Test]
        public void SavePodCast_NonExisting_OK()
        {
            // Arrange:
            var podCast = new PodCast(
                null,
                "x",
                new Uri("http://test.dxxxxx"),
                "x",
                "x");

            // Act:
            this.PodCastRepository.SavePodCast(podCast);

            // Assert:
            Assert.IsNotNull(podCast.Id);
        }

        /// <summary>
        /// Saves the pod cast_ existing_ OK.
        /// </summary>
        [Test]
        public void SavePodCast_Existing_OK()
        {
            // Arrange:
            var podCast = new PodCastDetailsViewModel(
                1,
                "X",
                new Uri("http://test.asdweqwe"),
                "x",
                "x");

            // Act:
            this.PodCastRepository.SavePodCast(podCast);

            // Assert:
        }
    }
}