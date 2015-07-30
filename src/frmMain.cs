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
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Windows.Forms;

    using CefSharp;
    using CefSharp.WinForms;

    using ICSharpCode.SharpZipLib.Zip;

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

        private LoadingDialog loading = null;

        private List<DataFile> dataFiles;

        private readonly Actions actions;

        private StepStatus currentStatus;

        private int currentIndex = -1;

        #endregion

        public FrmMain()
        {
            this.jsObj.OnContinue += OnContinue;
            this.webView = new ChromiumWebBrowser("about:blank");
            this.webView.RegisterJsObject("submitSys", this.jsObj);
            var actionsJson = File.ReadAllText("Scripts/ActionDefinition.json");
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

                    this.Enabled = false;
                    this.loading = new LoadingDialog(this.Handle, "正在读取文件数据, 请稍等...");
                    this.loading.ShowModeless(this);
                    this.loading.Refresh();
                    try
                    {
                        if (string.Compare(Path.GetExtension(fileName), ".zip", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            using (var fs = File.OpenRead(fileName))
                            {
                                var zf = new ZipFile(fs);
                                foreach (ZipEntry zipEntry in zf)
                                {
                                    if (!zipEntry.IsFile)
                                    {
                                        continue; // Ignore directories
                                    }

                                    var zipStream = zf.GetInputStream(zipEntry);
                                    this.AddDataFile(new DataFile(zipStream, zipEntry.Name));
                                }
                            }
                        }
                        else if (string.Compare(Path.GetExtension(fileName), ".csv", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            this.AddDataFile(new DataFile(fileName));
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show(string.Format(Resources.ReadFileError, fileName), Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    this.loading.Close();
                    this.Enabled = true;
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

            this.Enabled = false;
            this.loading = new LoadingDialog(this.Handle, "正在上传数据, 请稍等...");
            this.loading.ShowModeless(this);
            this.loading.Refresh();
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

            this.Enabled = false;
            this.loading = new LoadingDialog(this.Handle, "正在上传数据, 请稍等...");
            this.loading.ShowModeless(this);
            this.loading.Refresh();
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
                                this.CloseLoading();
                                return;
                            }

                            var row = selectedRows[currentIndex];
                            script = this.selectedColumns.Aggregate(script, (c, column) => c.Replace("{" + column + "}", row[column].ToString()));
                        }

                        this.webView.EvaluateScriptAsync(script).Wait();
                        this.currentStatus = step.Value.NextStatus;
                    }
                }
            }
            catch (Exception)
            {
                this.CloseLoading();
                throw;
            }
        }

        private void LstCategorySelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstCategory.SelectedItem is DataFile)
            {
                var file = this.lstCategory.SelectedItem as DataFile;
                this.dgvData.DataSource = file.Table;
                this.btnSubmit.Enabled = file.CanNew;
            }
        }

        private void DgvDataSelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvData.Rows)
            {
                var checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null)
                {
                    checkBoxCell.Value = this.dgvData.SelectedRows.Contains(row);
                }
            }
        }

        private void OnContinue(object sender, ContinueEventArgs e)
        {
            if (currentIndex >= selectedRows.Count - 1)
            {
                this.CloseLoading();
                return;
            }

            if (currentIndex < selectedRows.Count - 1)
            {
                this.OpenDocumentTab(e.Step);
            }
        }

        #endregion

        #region Private Methods

        private void AddDataFile(DataFile dataFile)
        {
            this.lstCategory.DataSource = null;
            if (this.dataFiles.Any(f => f.Key == dataFile.Key))
            {
                this.dataFiles.Remove(this.dataFiles.First(f => f.Key == dataFile.Key));
            }
            this.dataFiles.Add(dataFile);
            this.lstCategory.DataSource = this.dataFiles;
            this.lstCategory.ValueMember = "Key";
            this.lstCategory.DisplayMember = "DisplayName";
            this.lstCategory.SelectedItem = dataFile;
        }

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

        private void CloseLoading()
        {
            this.Invoke(new Action(() =>
            {
                this.webView.FrameLoadEnd -= WebViewOnFrameLoadEnd;
                this.loading.Close();
                this.Enabled = true;
            }));
        }

        #endregion
    }
}
