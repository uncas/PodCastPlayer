//-------------
// <copyright file="ModelExceptionTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.ModelTests
{
    using System;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Model;

    /// <summary>
    /// Tests the model exceptions.
    /// </summary>
    [TestFixture]
    public class ModelExceptionTests
    {
        /// <summary>
        /// Tests the parameter-less constructor.
        /// </summary>
        [Test]
        public void Constructor_0_OK()
        {
            // Arrange:

            // Act:
            var ex0 = new ModelException();

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
            var ex1 = new ModelException("test");

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
                new ModelException(
                "test",
                new Exception());

            // Assert:
        }
    }
}