//-------------
// <copyright file="BackgroundDownloader.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;

    /// <summary>
    /// Handles background downloads.
    /// </summary>
    public class BackgroundDownloader : IDisposable
    {
        #region Private fields

        /// <summary>
        /// The background worker.
        /// </summary>
        private readonly BackgroundWorker worker;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundDownloader"/> class.
        /// </summary>
        public BackgroundDownloader()
        {
            this.worker = new BackgroundWorker();
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork +=
                new DoWorkEventHandler(
                    this.Worker_DoWork);
            this.worker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
                    this.Worker_RunWorkerCompleted);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            WriteTrace("Start Begin");
            this.worker.RunWorkerAsync();
            WriteTrace("Start End");
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            WriteTrace("Stop Begin");
            this.worker.CancelAsync();
            WriteTrace("Stop End");
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
                this.worker.Dispose();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Writes the trace.
        /// </summary>
        /// <param name="info">The info to write.</param>
        private static void WriteTrace(string info)
        {
            Trace.WriteLine(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}: {1}",
                    DateTime.Now.Millisecond,
                    info));
        }

        /// <summary>
        /// Handles the DoWork event of the Worker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void Worker_DoWork(
            object sender,
            DoWorkEventArgs e)
        {
            WriteTrace("Worker_DoWork Begin");
            Thread.Sleep(500);
            WriteTrace("Worker_DoWork End");
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
            WriteTrace("Worker_RunWorkerCompleted Begin");
            if (!e.Cancelled)
            {
                WriteTrace("Cancelled");
                this.worker.RunWorkerAsync();
            }

            WriteTrace("Worker_RunWorkerCompleted End");
        }

        #endregion
    }
}