// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadingDialog.cs" company="PerkinElmer Inc.">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SubmitSys
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// The loading dialog.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class LoadingDialog : Form
    {
        /// <summary>
        /// The owner window handle
        /// </summary>
        private readonly IntPtr ownerHandle;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingDialog"/> class.
        /// </summary>
        /// <param name="ownerHandle">The owner window handle</param>
        /// <param name="message">The message</param>
        public LoadingDialog(IntPtr ownerHandle, string message)
        {
            this.ownerHandle = ownerHandle;
            this.InitializeComponent();
            this.lbloading.Text = message;
            this.Closing += this.WindowClosing;
            EnableWindow(this.Handle, true);
            SetForegroundWindow(this.Handle);
        }

        /// <summary>
        /// P/Invoke system user32 library EnableWindow API
        /// </summary>
        /// <param name="wnd">The window handle</param>
        /// <param name="enable">The value to enable or disable</param>
        /// <returns>Returns the enabled state</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnableWindow(IntPtr wnd, bool enable);

        /// <summary>
        /// P/Invoke system user32 library SetForegroundWindow API
        /// </summary>
        /// <param name="wnd">The window handle</param>
        /// <returns>Returns the enabled state</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr wnd);

        /// <summary>
        /// Show dialog as modeless
        /// </summary>
        /// <param name="owner">The owner window</param>
        public void ShowModeless(IWin32Window owner)
        {
            EnableWindow(this.ownerHandle, false);
            EnableWindow(this.Handle, true);
            SetForegroundWindow(this.Handle);
            this.Show(owner);   
        }

        /// <summary>
        /// The event handler for current window closing
        /// </summary>
        /// <param name="sender">The event sender object</param>
        /// <param name="e">The event arguments</param>
        private void WindowClosing(object sender, CancelEventArgs e)
        {
            this.Closing -= this.WindowClosing;
            EnableWindow(this.Handle, false);
            EnableWindow(this.ownerHandle, true);
        }
    }
}
