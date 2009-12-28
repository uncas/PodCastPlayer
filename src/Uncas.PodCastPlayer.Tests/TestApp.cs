//-------------
// <copyright file="TestApp.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Tests
{
    using Uncas.PodCastPlayer.Fakes;
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Statics for the test application.
    /// </summary>
    internal static class TestApp
    {
        /// <summary>
        /// The repositories.
        /// </summary>
        private static IRepositoryFactory repositories;

        /// <summary>
        /// Gets the repositories.
        /// </summary>
        /// <value>The repositories.</value>
        internal static IRepositoryFactory Repositories
        {
            get
            {
                if (repositories == null)
                {
                    repositories =
                        new FakeRepositoryFactory();
                }
                
                return repositories;
            }
        }
    }
}