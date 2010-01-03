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
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Interaction logic for EpisodeIndex.xaml
    /// </summary>
    public partial class EpisodeIndex : UserControl
    {
        #region Private fields

        /// <summary>
        /// The pod cast id.
        /// </summary>
        private readonly int podCastId;

        /// <summary>
        /// The episode service.
        /// </summary>
        private readonly EpisodeService service;

        #endregion

        #region Construtor

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeIndex"/> class.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        public EpisodeIndex(
            int podCastId)
        {
            this.InitializeComponent();
            this.service =
                new EpisodeService(
                App.Repositories,
                App.Downloader);
            this.podCastId = podCastId;
            this.Loaded +=
                new RoutedEventHandler(
                    this.EpisodeIndex_Loaded);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Handles the Click event of the DownloadButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void DownloadButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            var downloadButton =
                (Button)sender;
            var episode =
                (EpisodeIndexItemViewModel)
                downloadButton.DataContext;
            string episodeId = episode.Id;
            this.service.AddEpisodeToDownloadList(
                this.podCastId,
                episodeId);
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
            // Updates from service:
            this.service.UpdateEpisodes(this.podCastId);

            // Updates the list of episodes:
            this.LoadEpisodes();
        }

        #endregion
    }
}