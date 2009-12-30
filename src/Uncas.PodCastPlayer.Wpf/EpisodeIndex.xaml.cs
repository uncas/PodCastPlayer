//-------------
// <copyright file="EpisodeIndex.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System.Windows;
    using System.Windows.Controls;
    using Uncas.PodCastPlayer.AppServices;

    /// <summary>
    /// Interaction logic for EpisodeIndex.xaml
    /// </summary>
    public partial class EpisodeIndex : UserControl
    {
        /// <summary>
        /// The pod cast.
        /// </summary>
        private readonly int podCastId;

        /// <summary>
        /// The service.
        /// </summary>
        private readonly PodCastService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeIndex"/> class.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        public EpisodeIndex(
            int podCastId)
        {
            this.InitializeComponent();
            this.service = new PodCastService(
                App.Repositories.PodCastRepository);
            this.podCastId = podCastId;
            this.Loaded +=
                new RoutedEventHandler(
                    this.EpisodeIndex_Loaded);
        }

        /// <summary>
        /// Handles the Loaded event of the EpisodeIndex control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void EpisodeIndex_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            this.updateEpisodesButton.Click +=
                new RoutedEventHandler(
                    this.UpdateEpisodesButton_Click);
            this.LoadEpisodes();
        }

        /// <summary>
        /// Loads the episodes.
        /// </summary>
        private void LoadEpisodes()
        {
            var episodeIndex =
                this.service.GetEpisodes(
                this.podCastId);
            episodesListBox.ItemsSource =
                episodeIndex.Episodes;
        }

        /// <summary>
        /// Handles the Click event of the UpdateEpisodesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UpdateEpisodesButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            // TODO: FEATURE: Update from internet

            // Updates the list of episodes:
            this.LoadEpisodes();
        }
    }
}