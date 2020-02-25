namespace Medical.Yottor.UI
{
    partial class FrmHtml
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
            this.btnEmail = new DevExpress.XtraEditors.SimpleButton();
            this.btnPDF = new DevExpress.XtraEditors.SimpleButton();
            this.btnImage = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnEmail
            // 
            this.btnEmail.Location = new System.Drawing.Point(66, 49);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(376, 27);
            this.btnEmail.TabIndex = 0;
            this.btnEmail.Text = "根据 Html 模板， 发送邮件";
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.Location = new System.Drawing.Point(66, 106);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(376, 27);
            this.btnPDF.TabIndex = 1;
            this.btnPDF.Text = "根据 Html 模板，生成 PDF";
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // btnImage
            // 
            this.btnImage.Location = new System.Drawing.Point(66, 166);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(376, 27);
            this.btnImage.TabIndex = 2;
            this.btnImage.Text = "根据 Html 模板，生成 Image";
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // FrmHtml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 645);
            this.Controls.Add(this.btnImage);
            this.Controls.Add(this.btnPDF);
            this.Controls.Add(this.btnEmail);
            this.Name = "FrmHtml";
            this.Text = "Html/Pdf 操作";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnEmail;
        private DevExpress.XtraEditors.SimpleButton btnPDF;
        private DevExpress.XtraEditors.SimpleButton btnImage;
    }
}