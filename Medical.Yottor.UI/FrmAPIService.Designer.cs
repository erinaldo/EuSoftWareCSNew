namespace Medical.Yottor.UI
{
    partial class FrmAPIService
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
            this.btnGet = new DevExpress.XtraEditors.SimpleButton();
            this.btnPost = new DevExpress.XtraEditors.SimpleButton();
            this.btnSoap = new DevExpress.XtraEditors.SimpleButton();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(60, 23);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(778, 23);
            this.btnGet.TabIndex = 0;
            this.btnGet.Text = "Get 方式调用";
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(60, 66);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(778, 23);
            this.btnPost.TabIndex = 1;
            this.btnPost.Text = "Post 方式调用";
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // btnSoap
            // 
            this.btnSoap.Location = new System.Drawing.Point(60, 109);
            this.btnSoap.Name = "btnSoap";
            this.btnSoap.Size = new System.Drawing.Size(778, 23);
            this.btnSoap.TabIndex = 2;
            this.btnSoap.Text = "Soap 方式调用";
            this.btnSoap.Click += new System.EventHandler(this.btnSoap_Click);
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(60, 157);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(778, 494);
            this.memoEdit1.TabIndex = 3;
            this.memoEdit1.UseOptimizedRendering = true;
            // 
            // FrmAPIService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 663);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.btnSoap);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.btnGet);
            this.MinimizeBox = false;
            this.Name = "FrmAPIService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "API 服务接口请求";
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnGet;
        private DevExpress.XtraEditors.SimpleButton btnPost;
        private DevExpress.XtraEditors.SimpleButton btnSoap;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
    }
}