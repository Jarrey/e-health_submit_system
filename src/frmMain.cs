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

        private AccountTypes account;

        private int currentIndex = -1;

        #endregion

        public FrmMain()
        {
            this.Text = Resources.FormTitle;
            this.jsObj.OnLogin += OnLogin;
            this.jsObj.OnContinue += OnContinue;
            this.jsObj.OnException += OnException;
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

        private void OnLogin(object sender, MessageEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.Text = Resources.FormTitle + @" - " + e.Message;
                this.account = e.Account;
                this.LstCategorySelectedIndexChanged(this, EventArgs.Empty);
            }));
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
                if (Settings.Default.AutoTextUserName)
                {
                    this.webView.ExecuteScriptAsync(File.ReadAllText(Path.Combine("Scripts", "Login.js")));
                }

                this.currentStatus = 0; // Initialize
            }
            else if (frameLoadEndEventArgs.Url.Contains(this.actions.Steps["Login"].FrameUrlKey))
            {
                this.webView.EvaluateScriptAsync(Resources.RunTime).Wait();
                var step = this.actions.Steps["Login"];
                var script = File.ReadAllText(Path.Combine("Scripts", step.Script));
                this.webView.EvaluateScriptAsync(script).Wait();
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
                    finally
                    {
                        this.loading.Close();
                        this.Enabled = true;
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

        private void WebViewOnFrameLoadEnd(object sender, FrameLoadEndEventArgs frameLoadEndEventArgs)
        {
            DataFile file = null;
            this.Invoke(new Action(() => { file = this.lstCategory.SelectedItem as DataFile; }));

            this.webView.ExecuteScriptAsync(Resources.RunTime);
            try
            {
                if (file == null)
                {
                    throw new NullReferenceException("Selected file is null");
                }

                foreach (var step in this.actions.Steps)
                {
                    if (frameLoadEndEventArgs.Url.Contains(step.Value.FrameUrlKey) && this.currentStatus == step.Value.PreStatus)
                    {
                        var scriptFile = step.Value.Script.Replace(@"{Parameter}", file.Parameter);
                        var script = File.ReadAllText(Path.Combine("Scripts", scriptFile));

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
                            script = script.Replace(@"{Status}", file.ModifyStep.ToString());
                            script = script.Replace(@"{Parameter}", file.Parameter);
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
                this.btnSubmit.Visible = false;
                this.btnModify.Visible = false;

                if (file.AccountSettings != null && file.AccountSettings[this.account.ToString()] != null)
                {
                    var btnNames = file.AccountSettings[this.account.ToString()].ToString().Split('|');
                    if (btnNames.Length > 1)
                    {
                        this.btnSubmit.Visible = file.CanNew;
                        this.btnModify.Visible = true;
                        this.btnSubmit.Text = btnNames[0];
                        this.btnModify.Text = btnNames[1];
                    }
                    else
                    {
                        this.btnSubmit.Visible = false;
                        this.btnModify.Visible = true;
                        this.btnModify.Text = btnNames[0];
                    }
                }
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

        private void BtnSubmitClick(object sender, EventArgs e)
        {
            if (!VerifyLoginPage()) return;

            var file = this.lstCategory.SelectedItem as DataFile;
            if (file != null)
            {
                if (!this.GetSelectedData(file.Table)) return;

                this.Enabled = false;
                this.loading = new LoadingDialog(this.Handle, Resources.Uploading);
                this.loading.ShowModeless(this);
                this.loading.Refresh();

                this.webView.ExecuteScriptAsync(Resources.RunTime);
                this.webView.FrameLoadEnd += WebViewOnFrameLoadEnd;
                this.OpenTab(file.NewStep);
            }
        }

        private void BtnModifyClick(object sender, EventArgs e)
        {
            if (!VerifyLoginPage()) return;

            var file = this.lstCategory.SelectedItem as DataFile;
            if (file != null)
            {
                if (!GetSelectedData(file.Table)) return;

                this.Enabled = false;
                this.loading = new LoadingDialog(this.Handle, Resources.Uploading);
                this.loading.ShowModeless(this);
                this.loading.Refresh();

                this.webView.ExecuteScriptAsync(Resources.RunTime);
                this.webView.FrameLoadEnd += WebViewOnFrameLoadEnd;
                this.OpenTab(file.ModifyStep);
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
                this.OpenTab(e.Step);
            }
        }

        private void OnException(object sender, ExceptionEventArgs e)
        {
            this.CloseLoading();
            MessageBox.Show(e.Ex.Message, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void OpenTab(StepStatus stepStatus)
        {
            if (stepStatus == StepStatus.Init) return;
            var openNewDocStep = this.actions.Steps["OpenDocumentTab"];
            var openClinicalStep = this.actions.Steps["OpenClinicalTab"];
            if (this.account == AccountTypes.Admin)
            {
                var script = File.ReadAllText(Path.Combine("Scripts", openNewDocStep.Script));
                this.webView.ExecuteScriptAsync(script);
            }

            if (this.account == AccountTypes.Clinical)
            {
                var script = File.ReadAllText(Path.Combine("Scripts", openClinicalStep.Script));
                this.webView.ExecuteScriptAsync(script);
            }

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
                this.jsObj.ShowMessages();
            }));
        }

        #endregion
    }
}
