//-------------
// <copyright file="PodCastIndex.Designer.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.UI
{
    /// <summary>
    /// Form with pod cast index.
    /// </summary>
    public partial class PodCastIndex
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Grid with pod casts.
        /// </summary>
        private System.Windows.Forms.DataGridView podCastsGrid;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.podCastsGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.podCastsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // podCastsGrid
            // 
            this.podCastsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.podCastsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.podCastsGrid.Location = new System.Drawing.Point(0, 0);
            this.podCastsGrid.Name = "podCastsGrid";
            this.podCastsGrid.Size = new System.Drawing.Size(292, 273);
            this.podCastsGrid.TabIndex = 1;
            // 
            // PodCastIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.podCastsGrid);
            this.Name = "PodCastIndex";
            this.Text = "PodCastIndex";
            ((System.ComponentModel.ISupportInitialize)(this.podCastsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}