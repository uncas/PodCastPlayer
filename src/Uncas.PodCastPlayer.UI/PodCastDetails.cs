//-------------
// <copyright file="PodCastDetails.cs" company="Uncas">
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
    /// Form for pod cast details.
    /// </summary>
    public partial class PodCastDetails : Form
    {
        /// <summary>
        /// The pod cast.
        /// </summary>
        private readonly PodCastIndexViewModel podCast;

        /// <summary>
        /// The pod cast service.
        /// </summary>
        private readonly PodCastService service =
            new PodCastService(
                App.Repositories.PodCastRepository);

        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastDetails"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="podCast">The pod cast.</param>
        public PodCastDetails(
            Form parent,
            PodCastIndexViewModel podCast)
        {
            this.MdiParent = parent;
            this.podCast = podCast;
            this.InitializeComponent();
            this.Load += new EventHandler(
                this.PodCastDetails_Load);
            this.saveAndExit.Click += new EventHandler(this.SaveAndExit_Click);
        }

        /// <summary>
        /// Handles the Click event of the SaveAndExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveAndExit_Click(
            object sender,
            EventArgs e)
        {
            this.podCast.Name = this.nameTextBox.Text;
            this.podCast.Url = new Uri(this.urlTextBox.Text);
            this.service.SavePodCast(this.podCast);
            this.Close();
        }

        /// <summary>
        /// Handles the Load event of the PodCastDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PodCastDetails_Load(
            object sender,
            EventArgs e)
        {
            // TODO: Localize strings:
            this.Text = "Pod cast details";
            this.saveAndExit.Text = "OK";
            this.nameTextBox.Text = this.podCast.Name;
            this.urlTextBox.Text = this.podCast.Url.ToString();
        }
    }
}