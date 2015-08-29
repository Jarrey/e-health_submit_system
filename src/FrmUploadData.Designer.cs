namespace SubmitSys
{
    using CefSharp.WinForms;

    partial class FrmUploadData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUploadData));
            this.pnlWebView = new System.Windows.Forms.Panel();
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.mainFrame = new System.Windows.Forms.SplitContainer();
            this.categoryDataFrame = new System.Windows.Forms.SplitContainer();
            this.grbCategory = new System.Windows.Forms.GroupBox();
            this.lstCategory = new System.Windows.Forms.ListBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.pnlTool = new System.Windows.Forms.Panel();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlWebView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainFrame)).BeginInit();
            this.mainFrame.Panel1.SuspendLayout();
            this.mainFrame.Panel2.SuspendLayout();
            this.mainFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoryDataFrame)).BeginInit();
            this.categoryDataFrame.Panel1.SuspendLayout();
            this.categoryDataFrame.Panel2.SuspendLayout();
            this.categoryDataFrame.SuspendLayout();
            this.grbCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pnlTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWebView
            // 
            this.pnlWebView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWebView.Controls.Add(this.cmbAccount);
            this.pnlWebView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWebView.Location = new System.Drawing.Point(0, 0);
            this.pnlWebView.Name = "pnlWebView";
            this.pnlWebView.Size = new System.Drawing.Size(1007, 394);
            this.pnlWebView.TabIndex = 5;
            // 
            // cmbAccount
            // 
            this.cmbAccount.DisplayMember = "Key";
            this.cmbAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Location = new System.Drawing.Point(3, 3);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(121, 21);
            this.cmbAccount.TabIndex = 0;
            this.cmbAccount.ValueMember = "Value";
            this.cmbAccount.SelectedIndexChanged += new System.EventHandler(this.CmbAccountSelectedIndexChanged);
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
            this.mainFrame.Panel1.Controls.Add(this.categoryDataFrame);
            this.mainFrame.Panel1MinSize = 150;
            // 
            // mainFrame.Panel2
            // 
            this.mainFrame.Panel2.Controls.Add(this.pnlWebView);
            this.mainFrame.Panel2MinSize = 50;
            this.mainFrame.Size = new System.Drawing.Size(1007, 662);
            this.mainFrame.SplitterDistance = 264;
            this.mainFrame.TabIndex = 6;
            // 
            // categoryDataFrame
            // 
            this.categoryDataFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryDataFrame.Location = new System.Drawing.Point(0, 0);
            this.categoryDataFrame.Name = "categoryDataFrame";
            // 
            // categoryDataFrame.Panel1
            // 
            this.categoryDataFrame.Panel1.Controls.Add(this.grbCategory);
            this.categoryDataFrame.Panel1MinSize = 100;
            // 
            // categoryDataFrame.Panel2
            // 
            this.categoryDataFrame.Panel2.Controls.Add(this.dgvData);
            this.categoryDataFrame.Panel2.Controls.Add(this.pnlTool);
            this.categoryDataFrame.Panel2MinSize = 300;
            this.categoryDataFrame.Size = new System.Drawing.Size(1007, 264);
            this.categoryDataFrame.SplitterDistance = 150;
            this.categoryDataFrame.TabIndex = 7;
            // 
            // grbCategory
            // 
            this.grbCategory.Controls.Add(this.lstCategory);
            this.grbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbCategory.Location = new System.Drawing.Point(0, 0);
            this.grbCategory.Name = "grbCategory";
            this.grbCategory.Size = new System.Drawing.Size(150, 264);
            this.grbCategory.TabIndex = 1;
            this.grbCategory.TabStop = false;
            this.grbCategory.Text = "数据类别";
            // 
            // lstCategory
            // 
            this.lstCategory.DisplayMember = "DisplayName";
            this.lstCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCategory.FormattingEnabled = true;
            this.lstCategory.ItemHeight = 20;
            this.lstCategory.Location = new System.Drawing.Point(3, 16);
            this.lstCategory.Name = "lstCategory";
            this.lstCategory.Size = new System.Drawing.Size(144, 245);
            this.lstCategory.TabIndex = 0;
            this.lstCategory.ValueMember = "Key";
            this.lstCategory.SelectedIndexChanged += new System.EventHandler(this.LstCategorySelectedIndexChanged);
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
            this.dgvData.Location = new System.Drawing.Point(0, 70);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(853, 194);
            this.dgvData.TabIndex = 2;
            this.dgvData.SelectionChanged += new System.EventHandler(this.DgvDataSelectionChanged);
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.lblTo);
            this.pnlTool.Controls.Add(this.lblDateFrom);
            this.pnlTool.Controls.Add(this.dateEnd);
            this.pnlTool.Controls.Add(this.dateStart);
            this.pnlTool.Controls.Add(this.btnModify);
            this.pnlTool.Controls.Add(this.btnSubmit);
            this.pnlTool.Controls.Add(this.chkSelectAll);
            this.pnlTool.Controls.Add(this.btnImport);
            this.pnlTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTool.Location = new System.Drawing.Point(0, 0);
            this.pnlTool.Name = "pnlTool";
            this.pnlTool.Size = new System.Drawing.Size(853, 70);
            this.pnlTool.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(202, 8);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(25, 20);
            this.lblTo.TabIndex = 8;
            this.lblTo.Text = "到";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFrom.Location = new System.Drawing.Point(4, 8);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(57, 20);
            this.lblDateFrom.TabIndex = 8;
            this.lblDateFrom.Text = "时间从";
            // 
            // dateEnd
            // 
            this.dateEnd.CustomFormat = "yyyy-MM-dd";
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateEnd.Location = new System.Drawing.Point(233, 8);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(129, 20);
            this.dateEnd.TabIndex = 7;
            this.dateEnd.ValueChanged += new System.EventHandler(this.DateEndValueChanged);
            // 
            // dateStart
            // 
            this.dateStart.CustomFormat = "yyyy-MM-dd";
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateStart.Location = new System.Drawing.Point(67, 8);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(129, 20);
            this.dateStart.TabIndex = 7;
            this.dateStart.ValueChanged += new System.EventHandler(this.DateStartValueChanged);
            // 
            // btnModify
            // 
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModify.Location = new System.Drawing.Point(675, 35);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(173, 30);
            this.btnModify.TabIndex = 6;
            this.btnModify.Text = "上传 && 修改档案";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Visible = false;
            this.btnModify.Click += new System.EventHandler(this.BtnModifyClick);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(675, 3);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(173, 30);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "上传 && 创建档案";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Visible = false;
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmitClick);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectAll.Location = new System.Drawing.Point(182, 38);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(60, 24);
            this.chkSelectAll.TabIndex = 5;
            this.chkSelectAll.Text = "全选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.ChkSelectAllCheckedChanged);
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(3, 34);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(173, 30);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "导入上传数据...";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.BtnImportClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmUploadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 662);
            this.Controls.Add(this.mainFrame);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 350);
            this.Name = "FrmUploadData";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlWebView.ResumeLayout(false);
            this.mainFrame.Panel1.ResumeLayout(false);
            this.mainFrame.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainFrame)).EndInit();
            this.mainFrame.ResumeLayout(false);
            this.categoryDataFrame.Panel1.ResumeLayout(false);
            this.categoryDataFrame.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.categoryDataFrame)).EndInit();
            this.categoryDataFrame.ResumeLayout(false);
            this.grbCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pnlTool.ResumeLayout(false);
            this.pnlTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWebView;
        private System.Windows.Forms.SplitContainer mainFrame;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel pnlTool;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.SplitContainer categoryDataFrame;
        private System.Windows.Forms.GroupBox grbCategory;
        private System.Windows.Forms.ListBox lstCategory;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.DateTimePicker dateStart;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.ComboBox cmbAccount;
    }
}

