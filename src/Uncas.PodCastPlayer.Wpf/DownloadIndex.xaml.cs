//-------------
// <copyright file="DownloadIndex.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System.Windows;
    using System.Windows.Controls;
    using Uncas.PodCastPlayer.AppServices;

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
                new RoutedEventHandler(
                    this.DownloadIndex_Loaded);
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
            var service =
                new EpisodeService(
                App.Repositories,
                App.Downloader);
            this.episodesListBox.ItemsSource =
                service.GetDownloadIndex();
        }

        #endregion
    }
}