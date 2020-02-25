using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Medical.Yottor.SQLDAL;
namespace Medical.Yottor.UI
{
    public partial class FrmZzJg : Form
    {
        DataTable dt = new DataTable();
        QxDAO qx = new QxDAO();
        public FrmZzJg()
        {
            InitializeComponent();
          
            LoadTree();
        

            this.panelControl1.Controls.Clear();
            this.panelControl1.Controls.Add(ZzJg.ItSelf);
            ZzJg.ItSelf.Dock = System.Windows.Forms.DockStyle.Fill;
        }

    

        private DataTable GetTestData()
        {
            DataTable dt = new DataTable();

            dt = qx.GetJg();

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
            LoadTreeNodes(dt, "ID", "ParentID", "MC", this.tvModule, treeNode, true, true);
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


        //#region private void GetOrganizeList() 获得子部门列表
        ///// <summary>
        ///// 获得子部门列表
        ///// </summary>
        //private void GetOrganizeList()
        //{
        //    // 设置鼠标繁忙状态
        //    this.Cursor = Cursors.WaitCursor;
        //    DataTable DTOrganizeList = qx.GetJgInFoByUID(tvModule.SelectedNode.Tag.ToString());
        //    this.grdOrganize.DataSource = DTOrganizeList.DefaultView;
         
        //    // 设置鼠标默认状态
        //    this.Cursor = Cursors.Default;
        //}
        //#endregion

        private void tvModule_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvModule.SelectedNode!=null)
            {
                //this.GetOrganizeList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // 添加
            this.Add(false);
        }

        private void btnBatchDelete_Click(object sender, EventArgs e)
        {
            int UId = Convert.ToInt16(tvModule.SelectedNode.Tag);
            if (qx.DeleteJg(UId))
                LoadTree();
            else
                MsgBox.ShowExclamation("删除错误,请联系管理员");
        }

       

        #region public string Add(bool root) 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns>主键</returns>
        public string Add(bool root)
        {
            string returnValue = string.Empty;
            //FrmZzJgEdIt frmZzJgEdIt;
            //if (root || (this.ParentEntityId.Length == 0) || (this.tvModule.SelectedNode == null))
            //{
            //    frmZzJgEdIt = new FrmZzJgEdIt();
            //}
            //else
            //{
            //    frmZzJgEdIt = new FrmZzJgEdIt(this.ParentEntityId, this.tvModule.SelectedNode.Text);
            //}
            //if (frmZzJgEdIt.ShowDialog(this) == DialogResult.OK)
            //{
            //    returnValue = frmZzJgEdIt.EntityId;
            //    this.FormLoaded = false;
            //    // 重新加载窗体
            //    this.FormOnLoad();
            //    this.FormLoaded = true;
            //}
            return returnValue;
        }
        #endregion

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //if (BaseInterfaceLogic.CheckInputSelect(this.DTModuleList, BaseBusinessLogic.SelectedColumn, false))
            //{
            //    string id = BaseInterfaceLogic.GetDataGridViewEntityID(this.grdModule, BaseModuleTable.FieldId);
            FrmZzJgEdIt frmZzJgEdIt = new FrmZzJgEdIt();
           frmZzJgEdIt.Cateid = this.tvModule.SelectedNode.Tag.ToString();
            if (frmZzJgEdIt.ShowDialog(this) == DialogResult.OK)
                {
                    //this.FormLoaded = false;
                    // //重新加载窗体
                    //this.FormOnLoad();
                    //this.FormLoaded = true;
                }
            //}
        }
        



    }
}
