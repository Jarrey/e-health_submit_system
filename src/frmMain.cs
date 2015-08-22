// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmMain.cs" company="Jarrey, jar_bob@163.com">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// <summary>
//   Defines the FrmMain type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SubmitSys
{
    using System.Windows.Forms;

    using SubmitSys.Properties;

    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.Text = Resources.FormTitle;
        }

        private void BtnDownloadDocClick(object sender, System.EventArgs e)
        {
            using (var form = new FrmDownloadDoc())
            {
                form.ShowDialog(this);
            }
        }

        private void BtnUploadDataClick(object sender, System.EventArgs e)
        {
            using (var form = new FrmUploadData())
            {
                form.ShowDialog(this);
            }
        }
    }
}
