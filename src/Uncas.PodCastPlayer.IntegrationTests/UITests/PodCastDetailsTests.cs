//-------------
// <copyright file="PodCastDetailsTests.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.IntegrationTests.UITests
{
    using NUnit.Framework;
    using Uncas.PodCastPlayer.Wpf;

    /// <summary>
    /// Tests for the user control with pod cast details.
    /// </summary>
    [TestFixture]
    public class PodCastDetailsTests
    {
        /// <summary>
        /// Pods the cast details_ null_ OK.
        /// </summary>
        [Test]
        public void PodCastDetails_Null_OK()
        {
            // Arrange:

            // Act:
            PodCastDetails details = new PodCastDetails(null);

            // Assert:
        }

        /// <summary>
        /// Pods the cast details_ null_ OK.
        /// </summary>
        [Test]
        public void PodCastDetails_1_OK()
        {
            // Arrange:

            // Act:
            PodCastDetails details = new PodCastDetails(1);

            // Assert:
        }
    }
}