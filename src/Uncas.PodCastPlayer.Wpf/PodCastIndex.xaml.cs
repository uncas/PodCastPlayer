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
    using Uncas.PodCastPlayer.AppServices;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Interaction logic for PodCastIndex.xaml
    /// </summary>
    public sealed partial class PodCastIndex : UserControl
    {
        /// <summary>
        /// The pod cast service.
        /// </summary>
        private PodCastService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastIndex"/> class.
        /// </summary>
        public PodCastIndex()
        {
            this.InitializeComponent();
            this.service = new PodCastService(
                App.Repositories.PodCastRepository);
            this.Loaded +=
                new RoutedEventHandler(
                    this.PodCastIndex_Loaded);
        }

        /// <summary>
        /// Occurs when [pod cast selected].
        /// </summary>
        internal event EventHandler<PodCastSelectedEventArgs>
            PodCastSelected;

        /// <summary>
        /// Occurs when [episodes selected].
        /// </summary>
        internal event EventHandler<PodCastSelectedEventArgs>
            EpisodesSelected;

        /// <summary>
        /// Handles the Loaded event of the PodCastIndex control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PodCastIndex_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            podCastsListBox.ItemsSource =
                this.service.GetPodCasts();
            podCastsListBox.SelectionChanged +=
                new SelectionChangedEventHandler(
                    this.PodCastsListBox_SelectionChanged);
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
            if (eventHandler != null)
            {
                var podCastSelectedArgs =
                    new PodCastSelectedEventArgs
                    {
                        PodCast = selectedPodCast
                    };
                eventHandler(
                    this,
                    podCastSelectedArgs);
            }
        }

        /// <summary>
        /// Handles the Click event of the Episodes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Episodes_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var podCast = (PodCastIndexViewModel)button.DataContext;
            this.FireEvent(
                podCast,
                this.EpisodesSelected);
        }
    }
}