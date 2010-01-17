//-------------
// <copyright file="App.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Threading;
    using Repository;
    using SQLiteRepository;
    using Utility;

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
        /// The episode saver.
        /// </summary>
        private static IEpisodeSaver episodeSaver;

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
            : this(Repositories, Downloader, EpisodeSaver)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        /// <param name="downloader">The downloader.</param>
        /// <param name="episodeSaver">The episode saver.</param>
        public App(
            IRepositoryFactory repositories,
            IPodCastDownloader downloader,
            IEpisodeSaver episodeSaver)
        {
            this.backgroundDownloader =
                new BackgroundDownloader(
                    repositories,
                    downloader,
                    episodeSaver);
            this.Startup +=
                this.App_Startup;
            this.DispatcherUnhandledException +=
                App_DispatcherUnhandledException;
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
                return downloader ??
                    (downloader = new PodCastDownloader());
            }
        }

        /// <summary>
        /// Gets the episode saver.
        /// </summary>
        /// <value>The episode saver.</value>
        internal static IEpisodeSaver EpisodeSaver
        {
            get
            {
                return episodeSaver ??
                    (episodeSaver = new EpisodeSaver());
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
                    var myMusicPath =
                        Environment.GetFolderPath(
                        Environment.SpecialFolder.MyMusic);
                    var podCastsPath =
                        Path.Combine(
                        myMusicPath,
                        "PodCasts");
                    var repositorypath =
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

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="exception">The exception.</param>
        public static void HandleException(
            string customMessage,
            Exception exception)
        {
            var messageToShow =
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}\n\n{1}",
                    customMessage,
                    exception);
            MessageBox.Show(messageToShow);

            // TODO: LOG EXCEPTION.
        }

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
        /// Handles the DispatcherUnhandledException event of the App control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void App_DispatcherUnhandledException(
            object sender,
            DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(
                "Unhandled exception.",
                e.Exception);
            e.Handled = true;
        }

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

        #endregion
    }
}