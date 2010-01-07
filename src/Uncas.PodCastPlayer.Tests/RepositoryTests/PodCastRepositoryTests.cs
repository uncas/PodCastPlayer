//-------------
// <copyright file="PodCastRepositoryTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.RepositoryTests
{
    using System;
    using System.Diagnostics;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Model;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Tests pod cast repository.
    /// </summary>
    [TestFixture]
    public class PodCastRepositoryTests : BaseTest
    {
        /// <summary>
        /// The pod cast repository.
        /// </summary>
        private readonly IPodCastRepository repository
            = TestApp.Repositories.PodCastRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastRepositoryTests"/> class.
        /// </summary>
        public PodCastRepositoryTests()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastRepositoryTests"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        public PodCastRepositoryTests(
            IRepositoryFactory repositories)
            : base(repositories)
        {
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
        /// Gets the pod casts.
        /// </summary>
        [Test]
        public void GetPodCasts_All_OK()
        {
            // Act:
            var podCasts
                = this.PodCastRepository.GetPodCasts();

            // Assert:
            foreach (var podCast in podCasts)
            {
                Trace.WriteLine(podCast.Name);
                Trace.WriteLine(podCast.Url);
                Assert.IsNotNull(podCast.Name);
                Assert.IsNotNull(podCast.Url);
            }
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