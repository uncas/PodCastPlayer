//-------------
// <copyright file="RepositoryExceptionTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.RepositoryTests
{
    using System;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Tests the repository exceptions.
    /// </summary>
    [TestFixture]
    public class RepositoryExceptionTests
    {
        /// <summary>
        /// Tests the parameter-less constructor.
        /// </summary>
        [Test]
        public void Constructor_0_OK()
        {
            // Arrange:

            // Act:
            var ex0 = new RepositoryException();

            // Assert:
        }

        /// <summary>
        /// Tests the constructor with one parameter.
        /// </summary>
        [Test]
        public void Constructor_1_OK()
        {
            // Arrange:

            // Act:
            var ex1 = new RepositoryException("test");

            // Assert:
        }

        /// <summary>
        /// Tests the constructor with two parameters.
        /// </summary>
        [Test]
        public void Constructor_2_OK()
        {
            // Arrange:

            // Act:
            var ex2 =
                new RepositoryException(
                "test",
                new Exception());

            // Assert:
        }
    }
}