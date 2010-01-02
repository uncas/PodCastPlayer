﻿//-------------
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
        #region Private fields

        /// <summary>
        /// The service.
        /// </summary>
        private readonly PodCastService service;

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
            this.service =
                new PodCastService(
                    App.Repositories,
                    App.Downloader);
            this.Loaded +=
                new RoutedEventHandler(
                    this.PodCastDetails_Loaded);
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
            if (this.podCastId != null)
            {
                PodCastDetailsViewModel podCast =
                    this.service.GetPodCast(this.podCastId);
                this.nameTextBox.Text = podCast.Name;
                this.urlTextBox.Text = podCast.Url.ToString();
            }

            this.saveButton.Click +=
                new RoutedEventHandler(
                    this.SaveButton_Click);
            this.deleteButton.Click +=
                new RoutedEventHandler(
                    this.DeleteButton_Click);
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
            var podCast =
                new PodCastDetailsViewModel(
                    this.podCastId,
                    new Uri(urlTextBox.Text));
            this.service.SavePodCast(podCast);
            this.BackToIndex();
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
            this.service.DeletePodCast(this.podCastId.Value);
            this.BackToIndex();
        }

        #endregion
    }
}