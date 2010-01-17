//-------------
// <copyright file="Window1.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public sealed partial class Window1 : Window
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Window1"/> class.
        /// </summary>
        public Window1()
        {
            InitializeComponent();
            this.Loaded +=
                this.Window1_Loaded;
        }

        #endregion

        /// <summary>
        /// Handles the Loaded event of the Window1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Window1_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            this.podCastsButton.Click +=
                this.PodCastsButton_Click;
            this.downloadsButton.Click +=
                this.DownloadsButton_Click;
            this.ShowPodCastIndex();
        }

        /// <summary>
        /// Handles the Click event of the DownloadsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void DownloadsButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            var downloads = new DownloadIndex();
            this.contentControl.Content = downloads;
        }

        /// <summary>
        /// Shows the index of the pod cast.
        /// </summary>
        private void ShowPodCastIndex()
        {
            var podCastIndex = new PodCastIndex();
            podCastIndex.PodCastSelected +=
                this.PodCastIndex_PodCastSelected;
            podCastIndex.EpisodesSelected +=
                this.PodCastIndex_EpisodesSelected;
            podCastIndex.NewPodCastClick += this.PodCastIndex_NewPodCastClick;

            this.contentControl.Content = podCastIndex;
        }

        /// <summary>
        /// Handles the NewPodCastClick event of the PodCastIndex control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PodCastIndex_NewPodCastClick(
            object sender,
            EventArgs e)
        {
            var podCastNew = new PodCastCreate();
            podCastNew.PodCastCreated +=
                this.PodCastNew_PodCastCreated;
            this.contentControl.Content = podCastNew;
        }

        /// <summary>
        /// Handles the PodCastCreated event of the PodCastNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Uncas.PodCastPlayer.Wpf.PodCastSelectedEventArgs"/> instance containing the event data.</param>
        private void PodCastNew_PodCastCreated(
            object sender,
            PodCastSelectedEventArgs e)
        {
            this.ShowDetails(e);
        }

        /// <summary>
        /// Handles the EpisodesSelected event of the PodCastIndex control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Uncas.PodCastPlayer.Wpf.PodCastSelectedEventArgs"/> instance containing the event data.</param>
        private void PodCastIndex_EpisodesSelected(
            object sender,
            PodCastSelectedEventArgs e)
        {
            if (e.PodCastId.HasValue)
            {
                var index =
                    new EpisodeIndex(
                    e.PodCastId.Value);
                contentControl.Content = index;
            }
        }

        /// <summary>
        /// Handles the Click event of the PodCastsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PodCastsButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            this.ShowPodCastIndex();
        }

        /// <summary>
        /// Handles the PodCastSelected event of the PodCastIndex control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Uncas.PodCastPlayer.Wpf.PodCastSelectedEventArgs"/> instance containing the event data.</param>
        private void PodCastIndex_PodCastSelected(
            object sender,
            PodCastSelectedEventArgs e)
        {
            this.ShowDetails(e);
        }

        /// <summary>
        /// Shows the details.
        /// </summary>
        /// <param name="e">The <see cref="Uncas.PodCastPlayer.Wpf.PodCastSelectedEventArgs"/> instance containing the event data.</param>
        private void ShowDetails(PodCastSelectedEventArgs e)
        {
            var details =
                new PodCastDetails(
                    e.PodCastId);
            contentControl.Content = details;
            details.PodCastSaved +=
                this.Details_PodCastSaved;
        }

        /// <summary>
        /// Handles the PodCastSaved event of the Details control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Details_PodCastSaved(
            object sender,
            EventArgs e)
        {
            this.ShowPodCastIndex();
        }
    }
}