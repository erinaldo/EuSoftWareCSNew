using System;
using System.Windows.Forms;
using System.Data;
using Medical.Yottor.Domain;

namespace Medical.Yottor.UI
{
    public partial class frmNPOI : Form
    {
        public frmNPOI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 由DataSet导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDatasetExport_Click(object sender, EventArgs e)
        {
            string filename = NPOIHelper.ExportToExcel(DataSource.GetTestDataSet());
            if (!string.IsNullOrEmpty(filename))
                MsgBox.ShowExclamation("导出成功！");
        }

        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataTableExport_Click(object sender, EventArgs e)
        {
            string filename = NPOIHelper.ExportToExcel(DataSource.GetTestDataTable(), "工作信息");
            if (!string.IsNullOrEmpty(filename))
                MsgBox.ShowExclamation("导出成功！");
        }

        /// <summary>
        /// 由List导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListExport_Click(object sender, EventArgs e)
        {
            string filename = NPOIHelper.ExportToExcel(DataSource.GetList(), DataSource.GetHeaderList(), "个人信息");
            if (!string.IsNullOrEmpty(filename))
                MsgBox.ShowExclamation("导出成功！");
        }

        /// <summary>
        /// 由DataGridView导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataGridViewExport_Click(object sender, EventArgs e)
        {
            string filename = NPOIHelper.ExportToExcel(this.dataGridView1, "个人信息");
            if (!string.IsNullOrEmpty(filename))
                MsgBox.ShowExclamation("导出成功！");
        }

        private void frmNPOI_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = DataSource.GetList();

            this.gridControl1.DataSource = DataSource.GetList();
        }

        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataTableImport_Click(object sender, EventArgs e)
        {
            DataTable dt = NPOIHelper.ImportFromExcel("", "工作信息", 0);
            if(dt != null && dt.Rows.Count > 0)
                MsgBox.ShowExclamation("导入成功！");
        }

       
    }
}
