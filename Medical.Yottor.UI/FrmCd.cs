
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Medical.Yottor.UI
{
    public partial class FrmCd  : DevExpress.XtraEditors.XtraForm
    {

        EuSoft.BLL.SysZyb bSysZyb = new EuSoft.BLL.SysZyb();
        DataTable dt = new DataTable();
        public FrmCd()
        {
            InitializeComponent();
            LoadTree();
        }


        private DataTable GetTestData()
        {
            DataTable dt = new DataTable();

         //   dt = qx.GetXtZy();
            dt = bSysZyb.GetAllList().Tables[0];

            return dt;
        }

        private void LoadTree()
        {
            dt = GetTestData();
            // 开始更新控件，屏幕不刷新，提高效率。
            this.tvModule.BeginUpdate();
            this.tvModule.Nodes.Clear();
            this.LoadTreeModule();
            // 更新控件，屏幕一次性刷新，提高效率。
            this.tvModule.EndUpdate();
            tvModule.ExpandAll();
        }
        private void LoadTreeModule()
        {
            TreeNode treeNode = new TreeNode();
            this.LoadTreeModule(treeNode);
        }
        private void LoadTreeModule(TreeNode treeNode)
        {

            LoadTreeNodes(dt, "ID", "PID", "XsName", this.tvModule, treeNode, true, true);
        }

        #region public static void LoadTreeNodes(DataTable dataTable, string fieldId, string fieldParentId, string fieldFullName, TreeNode treeNode) 加载树型结构的主键
        /// <summary>
        /// 加载树型结构的主键
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="fieldId">主键</param>
        /// <param name="fieldParentId">上级字段</param>
        /// <param name="fieldFullName">全称</param>
        /// <param name="treeNode">当前树结点</param>
        public static void LoadTreeNodes(DataTable dataTable, string fieldId, string fieldParentId, string fieldFullName, TreeView treeView, TreeNode treeNode, bool loadTree, bool expandLevel0)
        {
            // 查找 ParentId 字段的值是否在 ID字段 里
            // 一般情况是简单的数据过滤，就没必要进行严格的检查了，进行了严格的检查，反而降低运行效率
            DataRow[] dataRows = null;
            if (treeNode.Tag == null)
            {
                if (dataTable.Columns[fieldId].DataType == typeof(int) || dataTable.Columns[fieldId].DataType == typeof(decimal))
                {
                    dataRows = dataTable.Select(fieldParentId + " IS NULL OR " + fieldParentId + " = 0");
                }
                else
                {
                    dataRows = dataTable.Select(fieldParentId + " IS NULL OR " + fieldParentId + " = ''");
                }
            }
            else
            {
                if (treeNode.Tag is string)
                {
                    dataRows = dataTable.Select(fieldParentId + "='" + treeNode.Tag.ToString() + "'", dataTable.DefaultView.Sort);
                }
                else
                {
                    dataRows = dataTable.Select(fieldParentId + "=" + treeNode.Tag.ToString() + "", dataTable.DefaultView.Sort);
                }
            }
            foreach (DataRow dataRow in dataRows)
            {
                // 判断不为空的当前节点的子节点
                if ((treeNode.Tag != null) && (treeNode.Tag.ToString().Length > 0) && (!treeNode.Tag.ToString().Equals(dataRow[fieldParentId].ToString())))
                {
                    continue;
                }

                // 当前节点的子节点, 加载根节点
                if (dataRow.IsNull(fieldParentId) || (dataRow[fieldParentId].ToString() == "0") || (dataRow[fieldParentId].ToString().Length == 0) || ((treeNode.Tag != null) && treeNode.Tag.Equals(dataRow[fieldParentId].ToString())))
                {
                    TreeNode newTreeNode = new TreeNode();
                    newTreeNode.Text = dataRow[fieldFullName].ToString();
                    newTreeNode.Tag = dataRow[fieldId].ToString();

                    if ((treeNode.Tag == null) || (treeNode.Tag.ToString().Length == 0))
                    {
                        // 树的根节点加载
                        newTreeNode.ImageIndex = 0;
                        newTreeNode.SelectedImageIndex = 0;
                        treeView.Nodes.Add(newTreeNode);
                        if (expandLevel0)
                        {
                            newTreeNode.Expand();
                        }
                    }
                    else
                    {
                        // 节点的子节点加载，第一层节点需要展开  
                        newTreeNode.ImageIndex = 1;
                        newTreeNode.SelectedImageIndex = 1;
                        treeNode.Nodes.Add(newTreeNode);
                        if (expandLevel0 && (treeNode.Level == 0))
                        {
                            treeNode.Expand();
                        }
                    }
                    if (loadTree)
                    {
                        // 递归调用本函数
                        LoadTreeNodes(dataTable, fieldId, fieldParentId, fieldFullName, treeView, newTreeNode, loadTree, expandLevel0);
                    }
                }
            }
        }
        #endregion

        private void tvModule_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvModule.SelectedNode!=null)
            {
                gridControl1.DataSource = bSysZyb.GetXTZyById(tvModule.SelectedNode.Tag.ToString());
                
            }
        }
        //#region private bool FileExist(string fileName) 检查文件是否存在
        ///// <summary>
        ///// 检查文件是否存在
        ///// </summary>
        ///// <param name="fileName">文件名</param>
        ///// <returns>是否存在</returns>
        //private bool FileExist(string fileName)
        //{
        //    if (System.IO.File.Exists(fileName))
        //    {
        //        string targetFileName = System.IO.Path.GetFileName(fileName);
        //        if (MessageBox.Show(AppMessage.Format(AppMessage.MSG0236, targetFileName), AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            System.IO.File.Delete(fileName);
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //#endregion


        //private void simpleButton1_Click(object sender, EventArgs e)
        //{

        //    // 开始忙了
        //    this.Cursor = Cursors.WaitCursor;
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.Title = "导出Excel";
        //    saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
        //    saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示   
        //    DialogResult dialogResult = saveFileDialog.ShowDialog(this);
        //    if (dialogResult == DialogResult.OK)
        //    {

        //      //  if (!this.FileExist(saveFileDialog.FileName))
        //      //  { 
        //        DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();

        //        //gridControl1.ExportToXls(saveFileDialog.FileName, options);  

        //        gridControl1.ExportToExcelOld(saveFileDialog.FileName);
        //        //Process.Start(saveFileDialog.FileName);
        //        DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //   // }
        //    }
        //    // 已经忙完了
        //    this.Cursor = Cursors.Default;
         
        //}
    }
}
