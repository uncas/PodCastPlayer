﻿//-------------
// <copyright file="App.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Threading;
    using Uncas.PodCastPlayer.Repository;
    using Uncas.PodCastPlayer.SQLiteRepository;
    using Uncas.PodCastPlayer.Utility;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IDisposable
    {
        #region Private fields

        /// <summary>
        /// The background downloader.
        /// </summary>
        private readonly BackgroundDownloader backgroundDownloader;

        /// <summary>
        /// The downloader.
        /// </summary>
        private static IPodCastDownloader downloader;

        /// <summary>
        /// The repositories.
        /// </summary>
        private static IRepositoryFactory repositories;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">
        /// More than one instance of the <see cref="T:System.Windows.Application"/> class is created per <see cref="T:System.AppDomain"/>.
        /// </exception>
        public App()
            : this(Repositories, Downloader)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        /// <param name="downloader">The downloader.</param>
        public App(
            IRepositoryFactory repositories,
            IPodCastDownloader downloader)
        {
            this.backgroundDownloader =
                new BackgroundDownloader(
                    repositories,
                    downloader);
            this.Startup +=
                new StartupEventHandler(this.App_Startup);
            this.DispatcherUnhandledException +=
                new DispatcherUnhandledExceptionEventHandler(
                    this.App_DispatcherUnhandledException);
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the downloader.
        /// </summary>
        /// <value>The downloader.</value>
        internal static IPodCastDownloader Downloader
        {
            get
            {
                if (downloader == null)
                {
                    downloader =
                        new PodCastDownloader();
                }

                return downloader;
            }
        }

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
                    string myMusicPath =
                        Environment.GetFolderPath(
                        Environment.SpecialFolder.MyMusic);
                    string podCastsPath =
                        Path.Combine(
                        myMusicPath,
                        "PodCasts");
                    string repositorypath =
                        Path.Combine(
                        podCastsPath,
                        "PodCastPlayer.db");
                    repositories =
                        new SQLiteRepositoryFactory(
                            repositorypath);
                }

                return repositories;
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.backgroundDownloader.Dispose();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Handles the Startup event of the App control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void App_Startup(
            object sender,
            StartupEventArgs e)
        {
            // Starts background downloader:
            this.backgroundDownloader.Start();
        }

        /// <summary>
        /// Handles the DispatcherUnhandledException event of the App control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void App_DispatcherUnhandledException(
            object sender,
            DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
            e.Handled = true;
        }

        #endregion
    }
}