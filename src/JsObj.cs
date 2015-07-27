using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubmitSys
{
    using System.Windows.Forms;

    using SubmitSys.Properties;

    internal class JsObj
    {
        public void PopupMsg(string msg)
        {
            MessageBox.Show(msg, Resources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
