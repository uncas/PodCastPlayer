//-------------
// <copyright file="ServiceExceptionTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests.AppServiceTests
{
    using System;
    using AppServices;
    using NUnit.Framework;

    /// <summary>
    /// Tests the service exception class.
    /// </summary>
    [TestFixture]
    public class ServiceExceptionTests
    {
        /// <summary>
        /// Constructor_0_s the OK.
        /// </summary>
        [Test]
        public void Constructor_0_OK()
        {
            var ex = new ServiceException();
        }

        /// <summary>
        /// Constructor_0_s the OK.
        /// </summary>
        [Test]
        public void Constructor_2_OK()
        {
            var ex = 
                new ServiceException(
                "x",
                new Exception());
        }
    }
}