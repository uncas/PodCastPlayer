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
    using AppServices;
    using Repository;
    using Utility;
    using ViewModel;

    /// <summary>
    /// Interaction logic for PodCastCreate.xaml
    /// </summary>
    public partial class PodCastCreate : UserControl
    {
        #region Private fields

        /// <summary>
        /// The pod cast service.
        /// </summary>
        private PodCastService service;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastCreate"/> class.
        /// </summary>
        public PodCastCreate()
        {
            this.InitializeComponent();
            this.createButton.Click +=
                this.CreateButton_Click;
            this.Loaded += this.PodCastCreate_Loaded;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the pod cast has been created.
        /// </summary>
        internal event EventHandler<PodCastSelectedEventArgs>
            PodCastCreated;

        #endregion

        #region Private methods

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
        /// Handles the Loaded event of the PodCastCreate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PodCastCreate_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            try
            {
                this.service =
                    new PodCastService(
                        App.Repositories,
                        App.Downloader);
            }
            catch (ServiceException ex)
            {
                App.HandleException(
                    "Service did not initialize.",
                    ex);
            }
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

            Uri podCastUrl;
            try
            {
                podCastUrl = new Uri(urlTextBox.Text);
            }
            catch (UriFormatException)
            {
                DisplayErrorMessage("Invalid url");
                return;
            }

            PodCastNewViewModel result;
            try
            {
                result =
                    this.service.CreatePodCast(
                    podCastUrl);
            }
            catch (UtilityException ex)
            {
                // TODO: EXCEPTION: Resolve ambiguity: This can mean either 1) the uri is not a valid feed/pod cast, 2) the utility had problems retrieving the pod cast info.
                App.HandleException(
                    "Invalid pod cast or problems retrieving pod cast info.",
                    ex);
                return;
            }
            catch (RepositoryException ex)
            {
                App.HandleException(
                    "Unable to save new pod cast info.",
                    ex);
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
                DisplayErrorMessage("Pod cast not created properly.");
            }
        }

        #endregion
    }
}