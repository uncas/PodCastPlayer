//-------------
// <copyright file="EpisodeIndex.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System.Windows;
    using System.Windows.Controls;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Interaction logic for EpisodeIndex.xaml
    /// </summary>
    public partial class EpisodeIndex : UserControl
    {
        /// <summary>
        /// The pod cast.
        /// </summary>
        private readonly PodCastIndexViewModel podCast;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeIndex"/> class.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public EpisodeIndex(
            PodCastIndexViewModel podCast)
        {
            this.InitializeComponent();
            this.podCast = podCast;
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
            // TODO: Fetch episodes for pod cast.
            // HACK: The following is wrong:
            episodesListBox.DataContext = this.podCast;
        }
    }
}