//-------------
// <copyright file="App.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System.Windows;
    using Uncas.PodCastPlayer.Fakes;
    using Uncas.PodCastPlayer.Repository;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
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