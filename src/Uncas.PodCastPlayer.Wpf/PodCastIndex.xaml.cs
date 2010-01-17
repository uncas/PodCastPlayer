//-------------
// <copyright file="PodCastIndex.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using AppServices;
    using Repository;
    using ViewModel;

    /// <summary>
    /// Interaction logic for PodCastIndex.xaml
    /// </summary>
    public sealed partial class PodCastIndex : UserControl
    {
        #region private fields

        /// <summary>
        /// The pod cast service.
        /// </summary>
        private PodCastService service;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastIndex"/> class.
        /// </summary>
        public PodCastIndex()
        {
            this.InitializeComponent();
            this.Loaded +=
                this.PodCastIndex_Loaded;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [pod cast selected].
        /// </summary>
        internal event EventHandler<PodCastSelectedEventArgs>
            PodCastSelected;

        /// <summary>
        /// Occurs when [new pod cast click].
        /// </summary>
        internal event EventHandler NewPodCastClick;

        /// <summary>
        /// Occurs when [episodes selected].
        /// </summary>
        internal event EventHandler<PodCastSelectedEventArgs>
            EpisodesSelected;

        #endregion

        #region Private methods

        /// <summary>
        /// Handles the Loaded event of the PodCastIndex control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PodCastIndex_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            try
            {
                this.service = new PodCastService(
                    App.Repositories,
                    App.Downloader);
            }
            catch (ServiceException ex)
            {
                App.HandleException(
                    "Error initializing service.",
                    ex);
                return;
            }

            try
            {
                podCastsListBox.ItemsSource =
                    this.service.GetPodCasts();
            }
            catch (RepositoryException ex)
            {
                App.HandleException(
                    "Error retrieving pod cast index.",
                    ex);
                return;
            }

            podCastsListBox.SelectionChanged +=
                this.PodCastsListBox_SelectionChanged;
            addPodCastButton.Click +=
                this.AddPodCastButton_Click;
        }

        /// <summary>
        /// Handles the Click event of the AddPodCastButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AddPodCastButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (this.NewPodCastClick != null)
            {
                this.NewPodCastClick(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the podCastsListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void PodCastsListBox_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            var selectedPodCast =
                (PodCastIndexViewModel)
                podCastsListBox.SelectedItem;
            this.FireEvent(
                selectedPodCast,
                this.PodCastSelected);
        }

        /// <summary>
        /// Fires the event.
        /// </summary>
        /// <param name="selectedPodCast">The selected pod cast.</param>
        /// <param name="eventHandler">The event handler.</param>
        private void FireEvent(
            PodCastIndexViewModel selectedPodCast,
            EventHandler<PodCastSelectedEventArgs> eventHandler)
        {
            if (eventHandler == null)
            {
                return;
            }

            var podCastId =
                selectedPodCast != null ?
                selectedPodCast.Id :
                null;
            var podCastSelectedArgs =
                new PodCastSelectedEventArgs(
                    podCastId);
            eventHandler(
                this,
                podCastSelectedArgs);
        }

        /// <summary>
        /// Handles the Click event of the Episodes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Episodes_Click(
            object sender,
            RoutedEventArgs e)
        {
            var button = (Button)sender;
            var podCast =
                (PodCastIndexViewModel)button.DataContext;
            this.FireEvent(
                podCast,
                this.EpisodesSelected);
        }

        #endregion
    }
}