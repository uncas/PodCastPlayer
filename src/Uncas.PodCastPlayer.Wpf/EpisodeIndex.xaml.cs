//-------------
// <copyright file="EpisodeIndex.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System.Windows;
    using System.Windows.Controls;
    using AppServices;
    using Repository;
    using Utility;
    using ViewModel;

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
            try
            {
                this.service =
                    new EpisodeService(
                    App.Repositories,
                    App.Downloader,
                    App.EpisodeSaver);
            }
            catch (ServiceException ex)
            {
                App.HandleException(
                    "Service could not be loaded.",
                    ex);
                return;
            }

            this.podCastId = podCastId;
            this.Loaded +=
                this.EpisodeIndex_Loaded;
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
            var episodeId = episode.Id;

            try
            {
                this.service.AddEpisodeToDownloadList(
                    this.podCastId,
                    episodeId);
            }
            catch (ServiceException)
            {
                MessageBox.Show("Could not be added to download list.");
            }
            catch (RepositoryException)
            {
                MessageBox.Show("Could not be added to download list.");
            }

            this.LoadEpisodes();
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
                this.UpdateEpisodesButton_Click;
            this.LoadEpisodes();
        }

        /// <summary>
        /// Loads the episodes.
        /// </summary>
        private void LoadEpisodes()
        {
            try
            {
                var episodeIndex =
                    this.service.GetEpisodes(
                    this.podCastId);
                this.podCastNameTextBlock.Text =
                    episodeIndex.PodCastName;
                this.episodesListBox.ItemsSource =
                    episodeIndex.Episodes;
            }
            catch (RepositoryException)
            {
                MessageBox.Show(
                    "Episode index could not be retrieved from storage.");
                return;
            }
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
            try
            {
                // Updates from service:
                this.service.UpdateEpisodes(this.podCastId);
            }
            catch (UtilityException ex)
            {
                App.HandleException(
                    "Episode index could not be retrieved from the internet.",
                    ex);
            }
            catch (RepositoryException ex)
            {
                App.HandleException(
                    "Updated episode index could not be saved.",
                    ex);
            }

            // Updates the list of episodes:
            this.LoadEpisodes();
        }

        #endregion
    }
}