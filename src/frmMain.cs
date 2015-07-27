// --------------------------------------------------------------------------------------------------------------------
// <copyright file="frmMain.cs" company="Jarrey, jar_bob@163.com">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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

    using Newtonsoft.Json;

    using SubmitSys.Properties;

    /// <summary> The main form. </summary>
    public partial class FrmMain : Form
    {
        #region Fields

        private readonly ChromiumWebBrowser webView;

        private readonly JsObj jsObj = new JsObj();

        private readonly List<DataRow> selectedRows = new List<DataRow>();

        private readonly List<string> selectedColumns = new List<string>();

        private List<DataFile> dataFiles;

        private readonly Actions actions;

        private StepStatus currentStatus;

        private int currentIndex = -1;

        #endregion

        public FrmMain()
        {
            this.webView = new ChromiumWebBrowser("about:blank");
            this.webView.RegisterJsObject("submitSys", this.jsObj);
            var actionsJson = File.ReadAllText("Scripts/ActionDefinition.js");
            this.actions = JsonConvert.DeserializeObject<Actions>(actionsJson);
            this.InitializeComponent();
            this.webView.FrameLoadEnd += this.WebViewOnLoginFrameLoadEnd;
            this.webView.Dock = DockStyle.Fill;
            this.pnlWebView.Controls.Add(this.webView);
            this.webView.Load(this.actions.LoginUrl);
            this.dataFiles = new List<DataFile>();
        }

        #region Event Handlers

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
                        this.lstCategory.DataSource = null;
                        var file = new DataFile(fileName);
                        if (this.dataFiles.Any(f => f.Key == file.Key))
                        {
                            this.dataFiles.Remove(this.dataFiles.First(f => f.Key == file.Key));
                        }
                        this.dataFiles.Add(file);
                        this.lstCategory.DataSource = this.dataFiles;
                        this.lstCategory.SelectedItem = file;
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
            if (!VerifyLoginPage()) return;

            var file = this.lstCategory.SelectedItem as DataFile;
            if (file != null)
            {
                if (!this.GetSelectedData(file.Table)) return;

                this.webView.ExecuteScriptAsync(Resources.RunTime);
                this.webView.FrameLoadEnd += WebViewOnFrameLoadEnd;
                this.OpenDocumentTab(StepStatus.OpenDocTabForNew);
            }
        }

        private void BtnModifyClick(object sender, EventArgs e)
        {
            if (!VerifyLoginPage()) return;

            var file = this.lstCategory.SelectedItem as DataFile;
            if (file != null)
            {
                if (!GetSelectedData(file.Table)) return;

                this.webView.ExecuteScriptAsync(Resources.RunTime);
                this.webView.FrameLoadEnd += WebViewOnFrameLoadEnd;
                this.OpenDocumentTab(StepStatus.OpenDocTabForModify);
            }
        }

        private void WebViewOnFrameLoadEnd(object sender, FrameLoadEndEventArgs frameLoadEndEventArgs)
        {
            this.webView.ExecuteScriptAsync(Resources.RunTime);
            try
            {
                foreach (var step in this.actions.Steps)
                {
                    if (frameLoadEndEventArgs.Url.Contains(step.Value.FrameUrlKey) && this.currentStatus == step.Value.PreStatus)
                    {
                        var script = File.ReadAllText(Path.Combine("Scripts", step.Value.Script));

                        if (step.Value.HasData)
                        {
                            if (step.Value.DataIncrease) currentIndex++;
                            if (currentIndex >= selectedRows.Count)
                            {
                                this.webView.FrameLoadEnd -= WebViewOnFrameLoadEnd;
                                return;
                            }

                            var row = selectedRows[currentIndex];
                            script = this.selectedColumns.Aggregate(script, (c, column) => c.Replace("{" + column + "}", row[column].ToString()));
                        }

                        this.webView.EvaluateScriptAsync(script).Wait();
                        this.currentStatus = step.Value.NextStatus;

                        if (step.Value.NextStatus == StepStatus.Init && currentIndex < selectedRows.Count - 1)
                        {
                            if (step.Value.PreStatus == StepStatus.ClickNewDoc)
                                this.OpenDocumentTab(StepStatus.OpenDocTabForNew);
                            if (step.Value.PreStatus == StepStatus.ClickModifyDoc)
                                this.OpenDocumentTab(StepStatus.OpenDocTabForModify);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Private Methods

        private bool GetSelectedData(DataTable table)
        {
            selectedRows.Clear();
            selectedColumns.Clear();
            currentIndex = -1;
            foreach (DataRow row in table.Rows)
            {
                if ((bool)row[Resources.SelectColumnName])
                {
                    selectedRows.Add(row);
                }
            }

            foreach (DataColumn column in table.Columns)
            {
                selectedColumns.Add(column.ColumnName);
            }

            if (selectedRows.Count == 0 || selectedColumns.Count == 0)
            {
                MessageBox.Show(Resources.SelectRecordsMessage, Resources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void OpenDocumentTab(StepStatus stepStatus)
        {
            var openNewDocStep = this.actions.Steps["OpenDocumentTab"];
            var script = File.ReadAllText(Path.Combine("Scripts", openNewDocStep.Script));
            this.webView.ExecuteScriptAsync(script);
            this.currentStatus = stepStatus;
        }

        private bool VerifyLoginPage()
        {
            if (this.webView.Address.Contains("login/ssoLogin_doctor.action"))
            {
                MessageBox.Show(Resources.LoginInfoMessage, Resources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        #endregion

        private void LstCategorySelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstCategory.SelectedItem is DataFile)
            {
                var file = this.lstCategory.SelectedItem as DataFile;
                this.dgvData.DataSource = file.Table;
            }
        }
    }
}
