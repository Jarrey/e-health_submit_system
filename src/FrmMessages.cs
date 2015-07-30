using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SubmitSys
{
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
    }
}
