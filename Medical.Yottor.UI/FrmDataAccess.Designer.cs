namespace Medical.Yottor.UI
{
    partial class FrmDataAccess
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
            this.btnGetDataTable = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetDataSet = new DevExpress.XtraEditors.SimpleButton();
            this.btnExecuteSQL = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnGetDataTable
            // 
            this.btnGetDataTable.Location = new System.Drawing.Point(107, 38);
            this.btnGetDataTable.Name = "btnGetDataTable";
            this.btnGetDataTable.Size = new System.Drawing.Size(282, 23);
            this.btnGetDataTable.TabIndex = 0;
            this.btnGetDataTable.Text = "获取数据表 ( GetDataTable )";
            this.btnGetDataTable.Click += new System.EventHandler(this.btnGetDataTable_Click);
            // 
            // btnGetDataSet
            // 
            this.btnGetDataSet.Location = new System.Drawing.Point(107, 83);
            this.btnGetDataSet.Name = "btnGetDataSet";
            this.btnGetDataSet.Size = new System.Drawing.Size(282, 23);
            this.btnGetDataSet.TabIndex = 1;
            this.btnGetDataSet.Text = "获取数据集 ( GetDataSet )";
            this.btnGetDataSet.Click += new System.EventHandler(this.btnGetDataSet_Click);
            // 
            // btnExecuteSQL
            // 
            this.btnExecuteSQL.Location = new System.Drawing.Point(107, 126);
            this.btnExecuteSQL.Name = "btnExecuteSQL";
            this.btnExecuteSQL.Size = new System.Drawing.Size(282, 23);
            this.btnExecuteSQL.TabIndex = 2;
            this.btnExecuteSQL.Text = "受影响行数 ( ExecuteSQL )";
            this.btnExecuteSQL.Click += new System.EventHandler(this.btnExecuteSQL_Click);
            // 
            // FrmDataAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 657);
            this.Controls.Add(this.btnExecuteSQL);
            this.Controls.Add(this.btnGetDataSet);
            this.Controls.Add(this.btnGetDataTable);
            this.Name = "FrmDataAccess";
            this.Text = "DataAccess 底层测试";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnGetDataTable;
        private DevExpress.XtraEditors.SimpleButton btnGetDataSet;
        private DevExpress.XtraEditors.SimpleButton btnExecuteSQL;
    }
}