namespace Medical.Yottor.UI
{
    partial class FrmTreeList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTreeList));
            this.myTreeList = new DevExpress.XtraTreeList.TreeList();
            this.ColumnMC = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ColumnID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.myTreeList)).BeginInit();
            this.SuspendLayout();
            // 
            // myTreeList
            // 
            this.myTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.ColumnMC,
            this.ColumnID});
            this.myTreeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTreeList.Location = new System.Drawing.Point(0, 0);
            this.myTreeList.Name = "myTreeList";
            this.myTreeList.OptionsBehavior.Editable = false;
            this.myTreeList.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowAlways;
            this.myTreeList.Size = new System.Drawing.Size(586, 482);
            this.myTreeList.StateImageList = this.imageList1;
            this.myTreeList.TabIndex = 0;
            this.myTreeList.Click += new System.EventHandler(this.myTreeList_Click);
            // 
            // ColumnMC
            // 
            this.ColumnMC.Caption = "组织机构";
            this.ColumnMC.FieldName = "MC";
            this.ColumnMC.MinWidth = 33;
            this.ColumnMC.Name = "ColumnMC";
            this.ColumnMC.Visible = true;
            this.ColumnMC.VisibleIndex = 0;
            this.ColumnMC.Width = 92;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "gif_45_053.gif");
            this.imageList1.Images.SetKeyName(1, "gif_45_102.gif");
            // 
            // ColumnID
            // 
            this.ColumnID.Caption = "编号";
            this.ColumnID.FieldName = "ID";
            this.ColumnID.Name = "ColumnID";
            // 
            // FrmTreeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 482);
            this.Controls.Add(this.myTreeList);
            this.Name = "FrmTreeList";
            this.Text = "TreeList控件";
            this.Load += new System.EventHandler(this.FrmTreeList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myTreeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList myTreeList;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ColumnMC;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ColumnID;
    }
}