//-------------
// <copyright file="Program.cs" company="Uncas">
//     Copyright (c) Ole Lynge Sørensen. All rights reserved.
// </copyright>
//-------------

namespace Uncas.PodCastPlayer.UI
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Represents the main program for the windows application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
