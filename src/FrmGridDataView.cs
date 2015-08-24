// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmGridDataView.cs" company="Jarrey, jar_bob@163.com">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// <summary>
//   Defines the FrmGridDataView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SubmitSys
{
    using System.Data;
    using System.Windows.Forms;

    public partial class FrmGridDataView : Form
    {
        public FrmGridDataView()
        {
            InitializeComponent();
        }

        public void SetData(DataTable table)
        {
            this.dgvDataView.DataSource = table;
        }
    }
}
