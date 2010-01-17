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
    using AppServices;
    using Repository;
    using ViewModel;

    /// <summary>
    /// Interaction logic for PodCastDetails.xaml
    /// </summary>
    public sealed partial class PodCastDetails : UserControl
    {
        #region Private fields

        /// <summary>
        /// The service.
        /// </summary>
        private PodCastService service;

        /// <summary>
        /// The id of the pod cast.
        /// </summary>
        private int? podCastId;

        #endregion

        #region Constructor

        // TODO: REFACTOR: Consider using automatic updating in WPF

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastDetails"/> class.
        /// </summary>
        /// <param name="podCastId">The pod cast id.</param>
        public PodCastDetails(
            int? podCastId)
        {
            this.InitializeComponent();
            this.podCastId = podCastId;
            this.Loaded +=
                this.PodCastDetails_Loaded;
        }

        #endregion

        #region Public event

        /// <summary>
        /// Occurs when pod cast saved.
        /// </summary>
        public event EventHandler PodCastSaved;

        #endregion

        #region Private methods

        /// <summary>
        /// Handles the Loaded event of the PodCastDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PodCastDetails_Loaded(
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
                    "Service not initialized.",
                    ex);
                return;
            }

            if (this.podCastId.HasValue)
            {
                PodCastDetailsViewModel podCast;
                try
                {
                    podCast = 
                        this.service.GetPodCast(
                        this.podCastId.Value);
                }
                catch (RepositoryException ex)
                {
                    App.HandleException(
                        "Error retrieving pod cast details",
                        ex);
                    return;
                }

                this.nameTextBox.Text = podCast.Name;
                this.urlTextBox.Text = podCast.Url.ToString();
                this.descriptionTextBlock.Text = podCast.Description;
                this.authorTextBlock.Text = podCast.Author;
            }

            this.deleteButton.Click +=
                this.DeleteButton_Click;
        }

        /// <summary>
        /// Goes back to index.
        /// </summary>
        private void BackToIndex()
        {
            if (this.PodCastSaved != null)
            {
                this.PodCastSaved(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void DeleteButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (!this.podCastId.HasValue)
            {
                return;
            }

            try
            {
                this.service.DeletePodCast(this.podCastId.Value);
            }
            catch (RepositoryException ex)
            {
                App.HandleException(
                    "Error deleting pod cast info.",
                    ex);
                return;
            }

            this.BackToIndex();
        }

        #endregion
    }
}