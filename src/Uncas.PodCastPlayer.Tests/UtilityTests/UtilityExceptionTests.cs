//-------------
// <copyright file="UtilityExceptionTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.UtilityTests
{
    using System;
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// Tests the utility exceptions.
    /// </summary>
    [TestFixture]
    public class UtilityExceptionTests
    {
        /// <summary>
        /// Tests the parameter-less constructor.
        /// </summary>
        [Test]
        public void Constructor_0_OK()
        {
            // Arrange:

            // Act:
            var ex0 = new UtilityException();

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
            var ex1 = new UtilityException("test");

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
                new UtilityException(
                "test",
                new Exception());

            // Assert:
        }
    }
}