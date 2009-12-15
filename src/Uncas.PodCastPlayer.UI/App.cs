//-------------
// <copyright file="App.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.UI
{
    using Uncas.PodCastPlayer.Fakes;
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Statics for the application.
    /// </summary>
    internal static class App
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