//-------------
// <copyright file="PodCastDetails.xaml.cs" company="Uncas">
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
    /// Interaction logic for PodCastDetails.xaml
    /// </summary>
    public sealed partial class PodCastDetails : UserControl
    {
        /// <summary>
        /// The pod cast.
        /// </summary>
        private readonly PodCastIndexViewModel podCast;

        private readonly PodCastService service;

        // TODO: Consider using automatic updating in WPF

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastDetails"/> class.
        /// </summary>
        /// <param name="podCast">The pod cast.</param>
        public PodCastDetails(
            PodCastIndexViewModel podCast)
        {
            this.InitializeComponent();
            this.podCast = podCast;
            this.service =
                new PodCastService(
                    App.Repositories.PodCastRepository);
            this.Loaded +=
                new RoutedEventHandler(
                    this.PodCastDetails_Loaded);
        }

        /// <summary>
        /// Occurs when [pod cast saved].
        /// </summary>
        public event EventHandler PodCastSaved;

        /// <summary>
        /// Handles the Loaded event of the PodCastDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PodCastDetails_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            this.nameTextBox.Text = this.podCast.Name;
            this.urlTextBox.Text = this.podCast.Url.ToString();
            this.saveButton.Click +=
                new RoutedEventHandler(
                    this.SaveButton_Click);
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            this.podCast.Name = nameTextBox.Text;
            this.podCast.Url = new Uri(urlTextBox.Text);
            this.service.SavePodCast(this.podCast);
            if (this.PodCastSaved != null)
            {
                this.PodCastSaved(this, new EventArgs());
            }
        }
    }
}