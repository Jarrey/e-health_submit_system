// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmDownloadDoc.cs" company="Jarrey, jar_bob@163.com">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// <summary>
//   Defines the FrmDownloadDoc type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SubmitSys
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Windows.Forms;

    using CefSharp;
    using CefSharp.WinForms;

    using Newtonsoft.Json;

    using SubmitSys.DAL;
    using SubmitSys.Properties;

    public partial class FrmDownloadDoc : Form
    {
        #region Fields

        private readonly ChromiumWebBrowser webView;

        private readonly JsObj jsObj = new JsObj();

        private readonly Actions actions;

        private readonly DBDocTable dbDocTable;

        private AccountTypes account;

        private int currentIndex = -1;

        #endregion

        public FrmDownloadDoc()
        {
            this.Text = Resources.FormTitle;
            this.jsObj.OnLogin += OnLogin;
            this.jsObj.OnContinue += OnContinue;
            this.jsObj.OnException += OnException;
            this.webView = new ChromiumWebBrowser("about:blank");
            this.webView.RegisterJsObject("submitSys", this.jsObj);
            var actionsJson = File.ReadAllText("Scripts/DownloadActionDefinition.json");
            this.actions = JsonConvert.DeserializeObject<Actions>(actionsJson);
            this.InitializeComponent();
            this.webView.FrameLoadEnd += this.WebViewOnLoginFrameLoadEnd;
            this.webView.FrameLoadEnd += this.WebViewOnSetDefaultConditionsFrameLoadEnd;
            this.webView.Dock = DockStyle.Fill;
            this.pnlWebView.Controls.Add(this.webView);
            this.webView.Load(this.actions.LoginUrl);

            this.dbDocTable = new DBDocTable(File.ReadAllText("FieldMaps/DatabaseTable.map"));
        }

        #region Event Handlers

        private void OnLogin(object sender, MessageEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.Text = Resources.FormTitle + @" - " + e.Message;
                this.account = e.Account;

                if (this.account != AccountTypes.Admin)
                {
                    MessageBox.Show(Resources.UseAdminLoginMessage, Resources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.webView.FrameLoadEnd += this.WebViewOnLoginFrameLoadEnd;
                    this.webView.ExecuteScriptAsync("parent.logout();");
                }

                this.btnDownloadDoc.Enabled = this.account == AccountTypes.Admin;
            }));
        }

        private void WebViewOnSetDefaultConditionsFrameLoadEnd(object sender, FrameLoadEndEventArgs frameLoadEndEventArgs)
        {
            if (frameLoadEndEventArgs.Url.Contains("/entity/basic/prePregnancyService.action"))
            {
                // auto set the default conditions
                this.webView.ExecuteScriptAsync(Resources.RunTime);
                var step = this.actions.Steps["SetDefaultConsiftions"];
                var script = File.ReadAllText(Path.Combine("Scripts", step.Script));
                this.webView.EvaluateScriptAsync(script).Wait();
            }
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

        private void OnContinue(object sender, ContinueEventArgs e)
        {
            var fields =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    File.ReadAllText("FieldMaps/DBDocTable.map"));
            var docTable = new DataTable();
            foreach (var c in fields)
            {
                docTable.Columns.Add(c.Value);
            }

            var data = JsonConvert.DeserializeObject<dynamic>(e.Parameter);
            foreach (var r in data)
            {
                var row = docTable.NewRow();
                var cols = r.ToObject<IDictionary<string, string>>();
                foreach (var c in fields)
                {
                    row[c.Value] = cols[c.Value];
                }

                docTable.Rows.Add(row);
            }

            this.dbDocTable.UpdateTable(docTable);
            using (var view = new FrmGridDataView())
            {
                view.SetData(docTable);
                view.ShowDialog();
            }
        }

        private void OnException(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show(e.Ex.Message, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnDownloadDocClick(object sender, EventArgs e)
        {
            this.webView.ExecuteScriptAsync(Resources.RunTime);
            var step = this.actions.Steps["DownloadDocs"];
            var script = File.ReadAllText(Path.Combine("Scripts", step.Script));
            this.webView.EvaluateScriptAsync(script).Wait();
        }

        #endregion
    }
}
