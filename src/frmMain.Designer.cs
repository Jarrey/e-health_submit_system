namespace SubmitSys
{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnDownloadDoc = new System.Windows.Forms.Button();
            this.btnUploadData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDownloadDoc
            // 
            this.btnDownloadDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadDoc.Location = new System.Drawing.Point(12, 12);
            this.btnDownloadDoc.Name = "btnDownloadDoc";
            this.btnDownloadDoc.Size = new System.Drawing.Size(175, 170);
            this.btnDownloadDoc.TabIndex = 0;
            this.btnDownloadDoc.Text = "下载档案数据...";
            this.btnDownloadDoc.UseVisualStyleBackColor = true;
            this.btnDownloadDoc.Click += new System.EventHandler(this.BtnDownloadDocClick);
            // 
            // btnUploadData
            // 
            this.btnUploadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadData.Location = new System.Drawing.Point(193, 12);
            this.btnUploadData.Name = "btnUploadData";
            this.btnUploadData.Size = new System.Drawing.Size(175, 170);
            this.btnUploadData.TabIndex = 0;
            this.btnUploadData.Text = "上传数据...";
            this.btnUploadData.UseVisualStyleBackColor = true;
            this.btnUploadData.Click += new System.EventHandler(this.BtnUploadDataClick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 193);
            this.Controls.Add(this.btnUploadData);
            this.Controls.Add(this.btnDownloadDoc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDownloadDoc;
        private System.Windows.Forms.Button btnUploadData;
    }
}