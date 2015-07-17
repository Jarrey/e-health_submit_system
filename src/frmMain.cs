namespace SubmitSys
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    using CefSharp;
    using CefSharp.WinForms;

    using Newtonsoft.Json;

    using SubmitSys.Properties;

    public partial class frmMain : Form
    {
        private readonly ChromiumWebBrowser webView = new ChromiumWebBrowser("about:blank");

        private readonly Actions actions;

        private Status currentStatus;

        public frmMain()
        {
            this.InitializeComponent();

            var actionsJson = File.ReadAllText("Scripts/ActionDefinition.js");
            actions = JsonConvert.DeserializeObject<Actions>(actionsJson);

            this.webView.Dock = DockStyle.Fill;
            this.pnlWebView.Controls.Add(webView);
            this.webView.FrameLoadEnd += WebViewOnFrameLoadEnd;
            this.webView.Load(actions.LoginUrl);
        }

        private void WebViewOnFrameLoadEnd(object sender, FrameLoadEndEventArgs frameLoadEndEventArgs)
        {
#if DEBUG
            this.webView.ShowDevTools();
#endif
            // Only for development, auto enter user name and password
            if (frameLoadEndEventArgs.Url.Contains("login/ssoLogin_doctor.action"))
            {
                this.webView.ExecuteScriptAsync("document.getElementById('form').scrollIntoView(false);");
#if DEBUG
                this.webView.ExecuteScriptAsync(Resources.Login);
#endif
                this.currentStatus = Status.Initialize;
            }
            else
            {
                this.webView.ExecuteScriptAsync(Resources.RunTime);

                try
                {
                    foreach (var step in this.actions.Steps)
                    {
                        if (frameLoadEndEventArgs.Url.Contains(step.FrameUrlKey))
                        {
                            if (this.currentStatus == step.PreStatus)
                            {
                                var script = File.ReadAllText(Path.Combine("Scripts", step.Script));

                                if (step.HasData)
                                {
                                    
                                }

                                this.webView.ExecuteScriptAsync(script);
                                this.currentStatus = step.NextStatus;
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
}
