namespace SubmitSys
{
    using System;
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

        private DataTable datatable;

        private readonly Actions actions;

        private int currentStatus;

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
                        var mapper = new FieldMapper("FieldMaps/BasicInfo.map");
                        datatable = new DataTable();
                        var csvConfig = new CsvConfiguration { Delimiter = ",", TrimHeaders = true, TrimFields = true };
                        using (var textReader = new StreamReader(fileName))
                        {
                            var csv = new CsvReader(textReader, csvConfig);
                            var isHeaderRead = false;
                            var hasRecord = true;
                            while (hasRecord)
                            {
                                hasRecord = csv.Read();
                                if (!isHeaderRead)
                                {
                                    datatable.Columns.Add(Resources.SelectColumnName, typeof(bool));
                                    foreach (var column in csv.FieldHeaders)
                                    {
                                        datatable.Columns.Add(mapper.Map(column));
                                    }

                                    isHeaderRead = true;
                                }

                                if (hasRecord)
                                {
                                    var row = datatable.NewRow();
                                    row[Resources.SelectColumnName] = false;
                                    foreach (var columnName in csv.FieldHeaders)
                                    {
                                        var value = csv.GetField(columnName);
                                        row[mapper.Map(columnName)] = value;
                                    }

                                    datatable.Rows.Add(row);
                                }
                            }
                        }

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


            this.webView.ExecuteScriptAsync(Resources.RunTime);

            this.webView.FrameLoadEnd += WebViewOnFrameLoadEnd;
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
                                if (datatable != null)
                                {
                                    var row = datatable.Rows[0];
                                    foreach (DataColumn column in datatable.Columns)
                                    {
                                        script = script.Replace("{" + column.ColumnName + "}", row[column.ColumnName].ToString());
                                    }
                                }
                            }

                            this.webView.ExecuteScriptAsync(script);
                            this.currentStatus = step.Value.NextStatus;

                            if (this.currentStatus == 3)
                            {
                                this.webView.FrameLoadEnd -= WebViewOnFrameLoadEnd;
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
