namespace Medical.Yottor.UI
{
    partial class FrmFTP
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
            this.btnUpload = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtTest = new DevExpress.XtraEditors.MemoEdit();
            this.btnDownload = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtDownload = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDownload.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(45, 29);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(377, 23);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "上传";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(45, 77);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(143, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "本地路径：D:\\\\uploadTest";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(33, 113);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(175, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "服务器路径：ftp://171.16.0.21/";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtTest);
            this.panelControl1.Location = new System.Drawing.Point(33, 158);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(389, 476);
            this.panelControl1.TabIndex = 3;
            // 
            // txtTest
            // 
            this.txtTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTest.Location = new System.Drawing.Point(2, 2);
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(385, 472);
            this.txtTest.TabIndex = 0;
            this.txtTest.UseOptimizedRendering = true;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(469, 29);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(377, 23);
            this.btnDownload.TabIndex = 4;
            this.btnDownload.Text = "下载";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(469, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(160, 14);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "本地路径：D:\\\\downloadTest";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(454, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(175, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "服务器路径：ftp://171.16.0.21/";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.txtDownload);
            this.panelControl2.Location = new System.Drawing.Point(457, 156);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(389, 476);
            this.panelControl2.TabIndex = 7;
            // 
            // txtDownload
            // 
            this.txtDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDownload.Location = new System.Drawing.Point(2, 2);
            this.txtDownload.Name = "txtDownload";
            this.txtDownload.Size = new System.Drawing.Size(385, 472);
            this.txtDownload.TabIndex = 0;
            this.txtDownload.UseOptimizedRendering = true;
            // 
            // FrmFTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 646);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnUpload);
            this.Name = "FrmFTP";
            this.Text = "FTP 工具类";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDownload.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnUpload;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.MemoEdit txtTest;
        private DevExpress.XtraEditors.SimpleButton btnDownload;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.MemoEdit txtDownload;
    }
}