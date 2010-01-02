//-------------
// <copyright file="PodCastCreate.xaml.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.Wpf
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Uncas.PodCastPlayer.AppServices;
    using Uncas.PodCastPlayer.Utility;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// Interaction logic for PodCastCreate.xaml
    /// </summary>
    public partial class PodCastCreate : UserControl
    {
        /// <summary>
        /// The pod cast service.
        /// </summary>
        private PodCastService service =
            new PodCastService(
                App.Repositories,
                App.Downloader);

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastCreate"/> class.
        /// </summary>
        public PodCastCreate()
        {
            this.InitializeComponent();
            this.createButton.Click +=
                new RoutedEventHandler(
                    this.CreateButton_Click);
        }

        /// <summary>
        /// Occurs when the pod cast has been created.
        /// </summary>
        internal event EventHandler<PodCastSelectedEventArgs>
            PodCastCreated;

        /// <summary>
        /// Displays the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        private static void DisplayErrorMessage(
            string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Handles the Click event of the CreateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CreateButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(urlTextBox.Text))
            {
                DisplayErrorMessage("No url provided");
                return;
            }

            Uri podCastUrl = null;
            try
            {
                podCastUrl = new Uri(urlTextBox.Text);
            }
            catch (ArgumentNullException)
            {
                DisplayErrorMessage("No url provided");
                return;
            }
            catch (UriFormatException)
            {
                DisplayErrorMessage("Invalid url");
                return;
            }

            PodCastNewViewModel result = null;
            try
            {
                result =
                    this.service.CreatePodCast(
                    podCastUrl);
            }
            catch (UtilityException)
            {
                DisplayErrorMessage("Invalid pod cast");
                return;
            }

            if (result.PodCastId.HasValue)
            {
                if (this.PodCastCreated != null)
                {
                    var eventArgs =
                        new PodCastSelectedEventArgs(
                            result.PodCastId);
                    this.PodCastCreated(
                        this,
                        eventArgs);
                }
            }
            else
            {
                DisplayErrorMessage("Error adding pod cast");
            }
        }
    }
}