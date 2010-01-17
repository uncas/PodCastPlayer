//-------------
// <copyright file="DownloadIndex.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System.Windows;
    using System.Windows.Controls;
    using AppServices;
    using Repository;

    /// <summary>
    /// Interaction logic for DownloadIndex.xaml
    /// </summary>
    public partial class DownloadIndex : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadIndex"/> class.
        /// </summary>
        public DownloadIndex()
        {
            this.InitializeComponent();
            this.Loaded +=
                this.DownloadIndex_Loaded;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Handles the Loaded event of the DownloadIndex control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void DownloadIndex_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            EpisodeService service;
            try
            {
                service =
                    new EpisodeService(
                    App.Repositories,
                    App.Downloader,
                    App.EpisodeSaver);
            }
            catch (ServiceException)
            {
                MessageBox.Show("Download index cannot be displayed.");
                
                // TODO: LOG exception.
                return;
            }

            try
            {
                this.episodesListBox.ItemsSource =
                    service.GetDownloadIndex();
            }
            catch (RepositoryException)
            {
                MessageBox.Show("Download index data could not be retrieved.");
                
                // TODO: LOG exception.
                throw;
            }
        }

        #endregion
    }
}