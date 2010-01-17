//-------------
// <copyright file="BackgroundDownloader.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System;
    using System.ComponentModel;
    using System.Timers;
    using AppServices;
    using Repository;
    using Utility;

    /// <summary>
    /// Handles background downloads.
    /// </summary>
    public sealed class BackgroundDownloader : IDisposable
    {
        #region Private fields

        /// <summary>
        /// The service.
        /// </summary>
        private readonly EpisodeService service;

        /// <summary>
        /// The timer.
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// The background worker.
        /// </summary>
        private readonly BackgroundWorker worker;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundDownloader"/> class.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        /// <param name="downloader">The downloader.</param>
        /// <param name="episodeSaver">The episode saver.</param>
        public BackgroundDownloader(
            IRepositoryFactory repositories,
            IPodCastDownloader downloader,
            IEpisodeSaver episodeSaver)
        {
            try
            {
                this.service = new EpisodeService(
                        repositories,
                        downloader,
                        episodeSaver);
            }
            catch (ServiceException)
            {
                // TODO: EXCEPTION: Pass info along with event handler:
                // Background downloader does not work properly. You can continue work, but no pod casts will be downloaded.
            }

            this.worker = new BackgroundWorker
                              {
                                  WorkerSupportsCancellation = true
                              };
            this.worker.DoWork +=
                this.Worker_DoWork;
            this.worker.RunWorkerCompleted +=
                this.Worker_RunWorkerCompleted;

            this.timer = new Timer(1000d);
            this.timer.Elapsed +=
                this.Timer_Elapsed;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Starts downloads of episodes in a background thread.
        /// </summary>
        public void Start()
        {
            this.worker.RunWorkerAsync();
        }

        /// <summary>
        /// Stops downloads of episodes in a background thread.
        /// </summary>
        public void Stop()
        {
            this.worker.CancelAsync();
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
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.worker.Dispose();
                this.timer.Dispose();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Handles the DoWork event of the Worker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void Worker_DoWork(
            object sender,
            DoWorkEventArgs e)
        {
            this.service.DownloadPendingEpisodes();
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the Worker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void Worker_RunWorkerCompleted(
            object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                this.timer.Start();
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void Timer_Elapsed(
            object sender,
            ElapsedEventArgs e)
        {
            this.timer.Stop();
            this.worker.RunWorkerAsync();
        }

        #endregion
    }
}