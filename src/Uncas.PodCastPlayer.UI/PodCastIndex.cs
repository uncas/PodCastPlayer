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
    using Uncas.PodCastPlayer.Fakes;

    /// <summary>
    /// The form with pod cast index.
    /// </summary>
    public partial class PodCastIndex : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PodCastIndex"/> class.
        /// </summary>
        public PodCastIndex()
        {
            this.InitializeComponent();
            this.Load +=
                new EventHandler(this.PodCastIndex_Load);
        }

        /// <summary>
        /// Handles the Load event of the PodCastIndex control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PodCastIndex_Load(object sender, EventArgs e)
        {
            PodCastService podCastService =
                new PodCastService(
                    new FakePodCastRepository());
            var podCasts = podCastService.GetPodCasts();
            this.podCastsListBox.DataSource = podCasts;
        }
    }
}