//-------------
// <copyright file="Form1.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.UI
{
    using System.Windows.Forms;

    /// <summary>
    /// Represents the default form.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.Load += new System.EventHandler(
                this.Form1_Load);
        }

        /// <summary>
        /// Handles the Load event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Form1_Load(
            object sender,
            System.EventArgs e)
        {
            PodCastIndex podCastIndex =
                new PodCastIndex();
            podCastIndex.Show();
        }
    }
}
