using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SubmitSys
{
    using System.IO;

    using Newtonsoft.Json;

    public partial class FrmMessages : Form
    {
        public FrmMessages()
        {
            InitializeComponent();
            Messages= new List<string>();
            this.lstMsg.DataSource = Messages;
        }

        public List<string> Messages { get; set; }

        protected override void OnActivated(EventArgs e)
        {
            this.lstMsg.DataSource = null;
            this.lstMsg.DataSource = Messages;
            base.OnActivated(e);
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(
                        saveFileDialog.FileName,
                        JsonConvert.SerializeObject(Messages));
                }
            }
        }
    }
}
