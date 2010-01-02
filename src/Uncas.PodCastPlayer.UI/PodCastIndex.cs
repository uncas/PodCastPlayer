//-------------
// <copyright file="PodCastIndex.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.UI
{
    using System;
    using System.Windows.Forms;
    using Uncas.PodCastPlayer.AppServices;
    using Uncas.PodCastPlayer.ViewModel;

    /// <summary>
    /// The form with pod cast index.
    /// </summary>
    public partial class PodCastIndex : Form
    {
        #region Private fields

        /// <summary>
        /// Indicates if the button column has been added.
        /// </summary>
        private bool buttonColumnAdded;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastIndex"/> class.
        /// </summary>
        /// <param name="parent">The parent form.</param>
        public PodCastIndex(Form parent)
        {
            this.MdiParent = parent;
            this.InitializeComponent();
            this.Load +=
                new EventHandler(this.PodCastIndex_Load);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Shows the details.
        /// </summary>
        /// <param name="selectedPodCast">The selected pod cast.</param>
        private void ShowDetails(
            PodCastIndexViewModel selectedPodCast)
        {
            PodCastDetails details =
                new PodCastDetails(
                    this.MdiParent,
                    selectedPodCast);
            details.FormClosed +=
                new FormClosedEventHandler(
                    this.Details_FormClosed);
            details.Show();
        }

        /// <summary>
        /// Handles the FormClosed event of the Details control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        private void Details_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            this.LoadPodCasts();
        }

        /// <summary>
        /// Loads the pod casts.
        /// </summary>
        private void LoadPodCasts()
        {
            PodCastService podCastService =
                new PodCastService(
                   App.Repositories,
                   null);
            var podCasts = podCastService.GetPodCasts();

            this.podCastsGrid.DataSource = podCasts;

            if (!this.buttonColumnAdded)
            {
                DataGridViewButtonColumn buttonColumn =
                    new DataGridViewButtonColumn();
                this.podCastsGrid.Columns.Add(buttonColumn);
                this.buttonColumnAdded = true;
            }

            this.podCastsGrid.Columns["Id"].Visible = false;

            this.podCastsGrid.Columns["Name"].HeaderText = "Name";
            this.podCastsGrid.Columns["Url"].HeaderText = "Url";
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the PodCastsGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PodCastsGrid_CellDoubleClick(
              object sender,
              DataGridViewCellEventArgs e)
        {
            // The header:
            if (e.RowIndex == -1)
            {
                return;
            }

            int rowIndex = e.RowIndex;
            this.ShowDetails(rowIndex);
        }

        /// <summary>
        /// Shows the details.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void ShowDetails(int rowIndex)
        {
            var podCast =
                (PodCastIndexViewModel)this.podCastsGrid
                .Rows[rowIndex].DataBoundItem;
            this.ShowDetails(podCast);
        }

        /// <summary>
        /// Handles the Load event of the PodCastIndex control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PodCastIndex_Load(
            object sender,
            EventArgs e)
        {
            this.Text = "Pod casts";

            this.podCastsGrid.RowHeadersVisible = false;
            this.podCastsGrid.ReadOnly = true;
            this.podCastsGrid.CellDoubleClick +=
                new DataGridViewCellEventHandler(
                this.PodCastsGrid_CellDoubleClick);
            this.podCastsGrid.CellContentClick +=
                new DataGridViewCellEventHandler(
                    this.PodCastsGrid_CellContentClick);

            this.LoadPodCasts();
        }

        /// <summary>
        /// Handles the CellContentClick event of the PodCastsGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PodCastsGrid_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.ShowDetails(e.RowIndex);
            }
        }

        #endregion
    }
}