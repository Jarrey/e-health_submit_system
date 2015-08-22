namespace SubmitSys
{
    partial class FrmDownloadDoc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDownloadDoc));
            this.pnlTool = new System.Windows.Forms.Panel();
            this.btnDownloadDoc = new System.Windows.Forms.Button();
            this.pnlWebView = new System.Windows.Forms.Panel();
            this.pnlTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnDownloadDoc);
            this.pnlTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTool.Location = new System.Drawing.Point(0, 0);
            this.pnlTool.Name = "pnlTool";
            this.pnlTool.Size = new System.Drawing.Size(1007, 36);
            this.pnlTool.TabIndex = 3;
            // 
            // btnDownloadDoc
            // 
            this.btnDownloadDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadDoc.Location = new System.Drawing.Point(829, 3);
            this.btnDownloadDoc.Name = "btnDownloadDoc";
            this.btnDownloadDoc.Size = new System.Drawing.Size(173, 30);
            this.btnDownloadDoc.TabIndex = 6;
            this.btnDownloadDoc.Text = "下载档案数据...";
            this.btnDownloadDoc.UseVisualStyleBackColor = true;
            // 
            // pnlWebView
            // 
            this.pnlWebView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWebView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWebView.Location = new System.Drawing.Point(0, 36);
            this.pnlWebView.Name = "pnlWebView";
            this.pnlWebView.Size = new System.Drawing.Size(1007, 626);
            this.pnlWebView.TabIndex = 5;
            // 
            // FrmDownloadDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 662);
            this.Controls.Add(this.pnlWebView);
            this.Controls.Add(this.pnlTool);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 350);
            this.Name = "FrmDownloadDoc";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlTool.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTool;
        private System.Windows.Forms.Button btnDownloadDoc;
        private System.Windows.Forms.Panel pnlWebView;
    }
}