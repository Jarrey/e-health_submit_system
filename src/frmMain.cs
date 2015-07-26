namespace SubmitSys
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using CefSharp;
    using CefSharp.WinForms;

    using CsvHelper;
    using CsvHelper.Configuration;

    using Newtonsoft.Json;

    using SubmitSys.Properties;

    public partial class FrmMain : Form
    {
        private readonly ChromiumWebBrowser webView = new ChromiumWebBrowser("about:blank");

        private readonly List<DataRow> selectedRows = new List<DataRow>();

        private DataTable datatable;

        private readonly Actions actions;

        private int currentStatus;

        private int currentIndex = -1;

        public FrmMain()
        {
            this.InitializeComponent();

            var actionsJson = File.ReadAllText("Scripts/ActionDefinition.js");
            actions = JsonConvert.DeserializeObject<Actions>(actionsJson);
            this.webView.FrameLoadEnd += WebViewOnLoginFrameLoadEnd;
            this.webView.Dock = DockStyle.Fill;
            this.pnlWebView.Controls.Add(webView);
            this.webView.Load(actions.LoginUrl);
        }

        private void WebViewOnLoginFrameLoadEnd(object sender, FrameLoadEndEventArgs frameLoadEndEventArgs)
        {
            if (Settings.Default.DebugTool)
            {
                this.webView.ShowDevTools();
            }

            // Only for development, auto enter user name and password
            if (frameLoadEndEventArgs.Url.Contains("login/ssoLogin"))
            {
                this.webView.ExecuteScriptAsync("document.getElementById('form').scrollIntoView(false);");
#if DEBUG
                this.webView.ExecuteScriptAsync(Resources.Login);
#endif
                this.currentStatus = 0; // Initialize
            }
            else
            {
                this.webView.FrameLoadEnd -= WebViewOnLoginFrameLoadEnd;
            }
        }

        private void BtnImportClick(object sender, EventArgs e)
        {
            using (var openFile = new OpenFileDialog()
                                {
                                    CheckFileExists = true,
                                    CheckPathExists = true,
                                    Multiselect = false,
                                    Title = Resources.OpenFileTitle,
                                    Filter = Resources.SubmitFileFilter
                                })
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    var fileName = openFile.FileName;
                    if (!File.Exists(fileName))
                    {
                        MessageBox.Show(string.Format(Resources.FileNotExistError, fileName), Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        datatable = Utility.ReadCsvToDataTable(fileName, "FieldMaps/BasicInfo.map");
                        this.dgvData.DataSource = datatable;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(string.Format(Resources.ReadFileError, fileName), Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void ChkSelectAllCheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvData.Rows)
            {
                var checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null)
                {
                    checkBoxCell.Value = this.chkSelectAll.Checked;
                }
            }
        }

        private void BtnSubmitClick(object sender, EventArgs e)
        {
            if (this.webView.Address.Contains("login/ssoLogin_doctor.action"))
            {
                MessageBox.Show(Resources.LoginInfoMessage, Resources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            selectedRows.Clear();
            currentIndex = -1;
            foreach (DataRow row in this.datatable.Rows)
            {
                if ((bool)row[Resources.SelectColumnName])
                {
                    selectedRows.Add(row);
                }
            }

            if (selectedRows.Count == 0)
            {
                MessageBox.Show(Resources.SelectRecordsMessage, Resources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.webView.ExecuteScriptAsync(Resources.RunTime);

            this.webView.FrameLoadEnd += WebViewOnFrameLoadEnd;
            this.CreateNewDoc();
        }


        private void CreateNewDoc()
        {
            var openNewDocStep = this.actions.Steps["OpenNewDocumentTab"];
            var script = File.ReadAllText(Path.Combine("Scripts", openNewDocStep.Script));
            this.webView.ExecuteScriptAsync(script);
            this.currentStatus = openNewDocStep.NextStatus;
        }

        private void WebViewOnFrameLoadEnd(object sender, FrameLoadEndEventArgs frameLoadEndEventArgs)
        {
            this.webView.ExecuteScriptAsync(Resources.RunTime);
            try
            {
                foreach (var step in this.actions.Steps)
                {
                    if (frameLoadEndEventArgs.Url.Contains(step.Value.FrameUrlKey))
                    {
                        if (this.currentStatus == step.Value.PreStatus)
                        {
                            var script = File.ReadAllText(Path.Combine("Scripts", step.Value.Script));

                            if (step.Value.HasData)
                            {
                                currentIndex++;
                                if (currentIndex >= selectedRows.Count)
                                {
                                    this.webView.FrameLoadEnd -= WebViewOnFrameLoadEnd;
                                    return;
                                }

                                var row = selectedRows[currentIndex];
                                foreach (DataColumn column in datatable.Columns)
                                {
                                    script = script.Replace("{" + column.ColumnName + "}", row[column.ColumnName].ToString());
                                }
                            }

                            this.webView.ExecuteScriptAsync(script);
                            this.currentStatus = step.Value.NextStatus;

                            if (this.currentStatus == 3 && currentIndex < selectedRows.Count - 1)
                            {
                                this.CreateNewDoc();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
