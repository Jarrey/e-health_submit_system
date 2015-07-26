namespace SubmitSys
{
    using CefSharp.WinForms;

    partial class FrmMain
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
            this.pnlWebView = new System.Windows.Forms.Panel();
            this.mainFrame = new System.Windows.Forms.SplitContainer();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.pnlTool = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tabActions = new System.Windows.Forms.TabControl();
            this.tabNewDoc = new System.Windows.Forms.TabPage();
            this.tabModifyDoc = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.mainFrame)).BeginInit();
            this.mainFrame.Panel1.SuspendLayout();
            this.mainFrame.Panel2.SuspendLayout();
            this.mainFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pnlTool.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.tabActions.SuspendLayout();
            this.tabNewDoc.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWebView
            // 
            this.pnlWebView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWebView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWebView.Location = new System.Drawing.Point(0, 0);
            this.pnlWebView.Name = "pnlWebView";
            this.pnlWebView.Size = new System.Drawing.Size(1090, 450);
            this.pnlWebView.TabIndex = 5;
            // 
            // mainFrame
            // 
            this.mainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainFrame.Location = new System.Drawing.Point(0, 0);
            this.mainFrame.Name = "mainFrame";
            this.mainFrame.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainFrame.Panel1
            // 
            this.mainFrame.Panel1.Controls.Add(this.tabActions);
            this.mainFrame.Panel1.Controls.Add(this.pnlLogo);
            this.mainFrame.Panel1MinSize = 150;
            // 
            // mainFrame.Panel2
            // 
            this.mainFrame.Panel2.Controls.Add(this.pnlWebView);
            this.mainFrame.Panel2MinSize = 50;
            this.mainFrame.Size = new System.Drawing.Size(1090, 756);
            this.mainFrame.SplitterDistance = 302;
            this.mainFrame.TabIndex = 6;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToOrderColumns = true;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 39);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.Size = new System.Drawing.Size(1076, 164);
            this.dgvData.TabIndex = 2;
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnSubmit);
            this.pnlTool.Controls.Add(this.chkSelectAll);
            this.pnlTool.Controls.Add(this.btnImport);
            this.pnlTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTool.Location = new System.Drawing.Point(3, 3);
            this.pnlTool.Name = "pnlTool";
            this.pnlTool.Size = new System.Drawing.Size(1076, 36);
            this.pnlTool.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(898, 6);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(173, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "上传创建档案";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmitClick);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(192, 9);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(50, 17);
            this.chkSelectAll.TabIndex = 5;
            this.chkSelectAll.Text = "全选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.ChkSelectAllCheckedChanged);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 6);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(173, 23);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "导入上传数据文件...";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.BtnImportClick);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLogo.Controls.Add(this.picLogo);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(1090, 70);
            this.pnlLogo.TabIndex = 0;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.White;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(1086, 66);
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tabActions
            // 
            this.tabActions.Controls.Add(this.tabNewDoc);
            this.tabActions.Controls.Add(this.tabModifyDoc);
            this.tabActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabActions.Location = new System.Drawing.Point(0, 70);
            this.tabActions.Name = "tabActions";
            this.tabActions.SelectedIndex = 0;
            this.tabActions.Size = new System.Drawing.Size(1090, 232);
            this.tabActions.TabIndex = 7;
            // 
            // tabNewDoc
            // 
            this.tabNewDoc.Controls.Add(this.dgvData);
            this.tabNewDoc.Controls.Add(this.pnlTool);
            this.tabNewDoc.Location = new System.Drawing.Point(4, 22);
            this.tabNewDoc.Name = "tabNewDoc";
            this.tabNewDoc.Padding = new System.Windows.Forms.Padding(3);
            this.tabNewDoc.Size = new System.Drawing.Size(1082, 206);
            this.tabNewDoc.TabIndex = 0;
            this.tabNewDoc.Text = "新建档案";
            this.tabNewDoc.UseVisualStyleBackColor = true;
            // 
            // tabModifyDoc
            // 
            this.tabModifyDoc.Location = new System.Drawing.Point(4, 22);
            this.tabModifyDoc.Name = "tabModifyDoc";
            this.tabModifyDoc.Padding = new System.Windows.Forms.Padding(3);
            this.tabModifyDoc.Size = new System.Drawing.Size(992, 206);
            this.tabModifyDoc.TabIndex = 1;
            this.tabModifyDoc.Text = "完善档案";
            this.tabModifyDoc.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 756);
            this.Controls.Add(this.mainFrame);
            this.MinimumSize = new System.Drawing.Size(400, 350);
            this.Name = "FrmMain";
            this.Text = "国家免费孕前优生健康检查项目信息上传系统";
            this.mainFrame.Panel1.ResumeLayout(false);
            this.mainFrame.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainFrame)).EndInit();
            this.mainFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pnlTool.ResumeLayout(false);
            this.pnlTool.PerformLayout();
            this.pnlLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.tabActions.ResumeLayout(false);
            this.tabNewDoc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWebView;
        private System.Windows.Forms.SplitContainer mainFrame;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel pnlTool;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TabControl tabActions;
        private System.Windows.Forms.TabPage tabNewDoc;
        private System.Windows.Forms.TabPage tabModifyDoc;
    }
}

