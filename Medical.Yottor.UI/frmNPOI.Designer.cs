namespace Medical.Yottor.UI
{
    partial class frmNPOI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDatasetExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnDataTableExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnListExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnDataGridViewExport = new DevExpress.XtraEditors.SimpleButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Country = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDataTableImport = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnAge = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNation = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDatasetExport
            // 
            this.btnDatasetExport.Location = new System.Drawing.Point(50, 39);
            this.btnDatasetExport.Name = "btnDatasetExport";
            this.btnDatasetExport.Size = new System.Drawing.Size(207, 23);
            this.btnDatasetExport.TabIndex = 0;
            this.btnDatasetExport.Text = "由 DataSet 导出 Excel";
            this.btnDatasetExport.Click += new System.EventHandler(this.btnDatasetExport_Click);
            // 
            // btnDataTableExport
            // 
            this.btnDataTableExport.Location = new System.Drawing.Point(50, 90);
            this.btnDataTableExport.Name = "btnDataTableExport";
            this.btnDataTableExport.Size = new System.Drawing.Size(207, 23);
            this.btnDataTableExport.TabIndex = 1;
            this.btnDataTableExport.Text = "由 DataTable 导出 Excel";
            this.btnDataTableExport.Click += new System.EventHandler(this.btnDataTableExport_Click);
            // 
            // btnListExport
            // 
            this.btnListExport.Location = new System.Drawing.Point(50, 141);
            this.btnListExport.Name = "btnListExport";
            this.btnListExport.Size = new System.Drawing.Size(207, 23);
            this.btnListExport.TabIndex = 2;
            this.btnListExport.Text = "由 List 导出 Excel";
            this.btnListExport.Click += new System.EventHandler(this.btnListExport_Click);
            // 
            // btnDataGridViewExport
            // 
            this.btnDataGridViewExport.Location = new System.Drawing.Point(50, 186);
            this.btnDataGridViewExport.Name = "btnDataGridViewExport";
            this.btnDataGridViewExport.Size = new System.Drawing.Size(207, 23);
            this.btnDataGridViewExport.TabIndex = 3;
            this.btnDataGridViewExport.Text = "由 DataGridView 导出 Excel";
            this.btnDataGridViewExport.Click += new System.EventHandler(this.btnDataGridViewExport_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Country,
            this.UserName,
            this.Age,
            this.Address,
            this.Nation});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(331, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(651, 566);
            this.dataGridView1.TabIndex = 4;
            // 
            // No
            // 
            this.No.DataPropertyName = "No";
            this.No.HeaderText = "编号";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            // 
            // Country
            // 
            this.Country.DataPropertyName = "Country";
            this.Country.HeaderText = "国家";
            this.Country.Name = "Country";
            this.Country.ReadOnly = true;
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "Name";
            this.UserName.HeaderText = "姓名";
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            // 
            // Age
            // 
            this.Age.DataPropertyName = "Age";
            this.Age.HeaderText = "年龄";
            this.Age.Name = "Age";
            this.Age.ReadOnly = true;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "地址";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // Nation
            // 
            this.Nation.DataPropertyName = "Nation";
            this.Nation.HeaderText = "民族";
            this.Nation.Name = "Nation";
            this.Nation.ReadOnly = true;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnDataTableImport);
            this.panelControl1.Controls.Add(this.btnDataTableExport);
            this.panelControl1.Controls.Add(this.btnDatasetExport);
            this.panelControl1.Controls.Add(this.btnDataGridViewExport);
            this.panelControl1.Controls.Add(this.btnListExport);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(331, 566);
            this.panelControl1.TabIndex = 5;
            // 
            // btnDataTableImport
            // 
            this.btnDataTableImport.Location = new System.Drawing.Point(50, 235);
            this.btnDataTableImport.Name = "btnDataTableImport";
            this.btnDataTableImport.Size = new System.Drawing.Size(207, 23);
            this.btnDataTableImport.TabIndex = 4;
            this.btnDataTableImport.Text = "由 Excel 导入 DataTable";
            this.btnDataTableImport.Click += new System.EventHandler(this.btnDataTableImport_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(982, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(369, 566);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnNo,
            this.gridColumnCountry,
            this.gridColumnName,
            this.gridColumnAge,
            this.gridColumnAddress,
            this.gridColumnNation});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnNo
            // 
            this.gridColumnNo.Caption = "编号";
            this.gridColumnNo.FieldName = "No";
            this.gridColumnNo.Name = "gridColumnNo";
            this.gridColumnNo.Visible = true;
            this.gridColumnNo.VisibleIndex = 0;
            // 
            // gridColumnCountry
            // 
            this.gridColumnCountry.Caption = "国家";
            this.gridColumnCountry.FieldName = "Country";
            this.gridColumnCountry.Name = "gridColumnCountry";
            this.gridColumnCountry.Visible = true;
            this.gridColumnCountry.VisibleIndex = 1;
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "姓名";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 2;
            // 
            // gridColumnAge
            // 
            this.gridColumnAge.Caption = "年龄";
            this.gridColumnAge.FieldName = "Age";
            this.gridColumnAge.Name = "gridColumnAge";
            this.gridColumnAge.Visible = true;
            this.gridColumnAge.VisibleIndex = 3;
            // 
            // gridColumnAddress
            // 
            this.gridColumnAddress.Caption = "地址";
            this.gridColumnAddress.FieldName = "Address";
            this.gridColumnAddress.Name = "gridColumnAddress";
            this.gridColumnAddress.Visible = true;
            this.gridColumnAddress.VisibleIndex = 4;
            // 
            // gridColumnNation
            // 
            this.gridColumnNation.Caption = "民族";
            this.gridColumnNation.FieldName = "Nation";
            this.gridColumnNation.Name = "gridColumnNation";
            this.gridColumnNation.Visible = true;
            this.gridColumnNation.VisibleIndex = 5;
            // 
            // frmNPOI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 566);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmNPOI";
            this.Text = "NPOI 控件测试";
            this.Load += new System.EventHandler(this.frmNPOI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnDatasetExport;
        private DevExpress.XtraEditors.SimpleButton btnDataTableExport;
        private DevExpress.XtraEditors.SimpleButton btnListExport;
        private DevExpress.XtraEditors.SimpleButton btnDataGridViewExport;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Country;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Age;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nation;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCountry;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAge;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAddress;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNation;
        private DevExpress.XtraEditors.SimpleButton btnDataTableImport;
    }
}