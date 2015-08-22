using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SubmitSys
{
    using System.IO;

    using CefSharp;
    using CefSharp.WinForms;

    using Newtonsoft.Json;

    using SubmitSys.Properties;

    public partial class FrmDownloadDoc : Form
    {
        #region Fields

        private readonly ChromiumWebBrowser webView;

        private readonly JsObj jsObj = new JsObj();

        private LoadingDialog loading = null;

        private readonly Actions actions;

        private StepStatus currentStatus;

        private AccountTypes account;

        private int currentIndex = -1;

        #endregion

        public FrmDownloadDoc()
        {
            this.Text = Resources.FormTitle;
            this.jsObj.OnLogin += OnLogin;
            //this.jsObj.OnContinue += OnContinue;
            this.jsObj.OnException += OnException;
            this.webView = new ChromiumWebBrowser("about:blank");
            this.webView.RegisterJsObject("submitSys", this.jsObj);
            var actionsJson = File.ReadAllText("Scripts/DownloadActionDefinition.json");
            this.actions = JsonConvert.DeserializeObject<Actions>(actionsJson);
            this.InitializeComponent();
            this.webView.FrameLoadEnd += this.WebViewOnLoginFrameLoadEnd;
            this.webView.Dock = DockStyle.Fill;
            this.pnlWebView.Controls.Add(this.webView);
            this.webView.Load(this.actions.LoginUrl);
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

        private void OnException(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show(e.Ex.Message, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
