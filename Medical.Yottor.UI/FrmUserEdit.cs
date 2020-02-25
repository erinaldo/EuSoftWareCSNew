using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EuSoft.BLL;
using EuSoft.Model;

namespace Medical.Yottor.UI
{
    public partial class FrmUserEdit : DevExpress.XtraEditors.XtraForm
    {
        private bool isUserClick = false;    // 是否是用户点击了复选框
        EuSoft.BLL.MCEUser bUser = new EuSoft.BLL.MCEUser();
        EuSoft.BLL.SysZyb bSysZyb = new EuSoft.BLL.SysZyb();
        EuSoft.Model.MCEUser mUser = new EuSoft.Model.MCEUser();
        EuSoft.BLL.SysQxB bSysQxB = new EuSoft.BLL.SysQxB();
        EuSoft.Model.SysQxB mSysQxB = new EuSoft.Model.SysQxB();
        DataTable dtQx = new DataTable();
        DataTable dtMyQx = new DataTable();
        public FrmUserEdit(string  nUserID,string nUsername,string  nPwd)
        {
            InitializeComponent();
            this.ButAdd.Text = "Add";
            this.Text = "Add";
            if (nUserID.Trim()!="")
            {
                this.ButAdd.Text = "Update";
                this.Text = "Update";
            }
            this.txtUserName.Text = nUsername;
            this.txtPwd.Text = nPwd;
            labId.Text = nUserID;

            LoadTree(nUserID);

        }

        private void LoadTree(string  nUserID)
        {
            dtMyQx = bSysZyb.GetQXList(" a.userid=" + nUserID + "");
            dtQx = bSysZyb.GetAllList().Tables[0];
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

            isUserClick = false;
            for (int i = 0; i < this.tvModule.Nodes.Count; i++)
            {

                this.DataTableToTree(this.tvModule.Nodes[i]);
            }
            isUserClick = true;
        }
        private void LoadTreeModule(TreeNode treeNode)
        {

            LoadTreeNodes(dtQx, "ID", "PID", "XsName", this.tvModule, treeNode, true, true);
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

        public FrmUserEdit()
        {
            InitializeComponent();
        }

       

        //private void butUpdate_Click(object sender, EventArgs e)
        //{
        //   
        //    mUser.UserName = this.txtUserName.Text;
        //    mUser.PassWord = this.txtPwd.Text;
        //    mUser.UpdateTime = DateTime.Now;
        //    mUser.Person = Properties.Settings.Default.LastUser;
        //    bool b = bUser.Update(mUser);
        //    if (b)
        //    {
        //        //this.Close();
        //        this.DialogResult = DialogResult.OK;
        //    }
        //}



        private void ButAdd_Click(object sender, EventArgs e)
        {
          
           
            mUser.UserName = this.txtUserName.Text;
            mUser.PassWord = this.txtPwd.Text;
            mUser.UpdateTime = DateTime.Now;
            mUser.Person = Properties.Settings.Default.LastUser;
            for (int i = 0; i < this.tvModule.Nodes.Count; i++)
            {

                this.EntityToDataTable(this.tvModule.Nodes[i]);
            }

            if (ButAdd.Text=="Update")
            {
                mUser.ID = Convert.ToInt32(this.labId.Text);
                bool b = bUser.Update(mUser);
                 if (b)
                 {
                     //this.Close();
                     this.DialogResult = DialogResult.OK;
                 }

            }
            else
            {
                int b = bUser.Add(mUser);
                if (b > 0)
                {
                    //this.Close();
                    this.DialogResult = DialogResult.OK;
                } 
            }
            
         
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void DataTableToTree(TreeNode treeNode)
        {
            // 提高运行速度
            DataRow[] dataRows = this.dtMyQx.Select(" ID='" + treeNode.Tag.ToString() + "' and IsQx=1 ");
            //foreach (DataRow dataRow in dataRows)
            //{
            //    //  dataRow["IsQx"] = treeNode.Checked ? 1 : 0;
            //    treeNode.Checked = Convert.ToInt16(dataRow["IsQx"]) == 1 ? true : false;


            //}
            if (dataRows.Length > 0)
            {
                treeNode.Checked = Convert.ToInt16(dataRows[0]["IsQx"]) == 1 ? true : false;
            }
            // BUBaseBusinessLogic.SetProperty(this.DSModule.Tables[BaseModuleTable.TableName], TreeNode.Tag.ToString(), BaseModuleTable.FieldEnabled, TreeNode.Checked ? 1 : 0);
            for (int i = 0; i < treeNode.Nodes.Count; i++)
            {
                // 这里进行递规操作
                this.DataTableToTree(treeNode.Nodes[i]);
            }
        }

        private void EntityToDataTable(TreeNode treeNode)
        {
            // 提高运行速度
            DataRow[] dataRows = this.dtMyQx.Select(" ID='" + treeNode.Tag.ToString() + "'");
          
           if (dataRows.Length > 0)
           {
                mSysQxB.IsQx = treeNode.Checked;
                mSysQxB.userid = labId.Text;
                mSysQxB.id = Convert.ToInt32(dataRows[0]["QxId"]);
                mSysQxB.ZyId = Convert.ToInt32(treeNode.Tag);
                bSysQxB.Update(mSysQxB);
           }
           else
           {
               mSysQxB.IsQx = treeNode.Checked;
               mSysQxB.userid = labId.Text;
               mSysQxB.ZyId =Convert.ToInt32(treeNode.Tag);
               bSysQxB.Add(mSysQxB);
           }
           
            // BUBaseBusinessLogic.SetProperty(this.DSModule.Tables[BaseModuleTable.TableName], TreeNode.Tag.ToString(), BaseModuleTable.FieldEnabled, TreeNode.Checked ? 1 : 0);
            for (int i = 0; i < treeNode.Nodes.Count; i++)
            {
                // 这里进行递规操作
                this.EntityToDataTable(treeNode.Nodes[i]);
            }
        }

        private void tvModule_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (isUserClick)
            {
                //for (int i = 0; i < e.Node.Nodes.Count; i++)
                //{
                //    e.Node.Nodes[i].Checked = e.Node.Checked;
                //}
                SetChildChecked(e.Node);  // 判断是否是根节点 
                if (e.Node.Parent != null)
                {
                    isUserClick = false;
                    SetParentChecked(e.Node);
                    isUserClick = true;
                }
            }
          
        }
        /// <summary> 
        /// 根据子节点状态设置父节点的状态 
        /// </summary> 
        /// <param name="childNode"></param> 
        private void SetParentChecked(TreeNode childNode)
        {
            TreeNode parentNode = childNode.Parent;
            if (!parentNode.Checked && childNode.Checked)
            {
                int ichecks = 0;
                for (int i = 0; i < parentNode.GetNodeCount(false); i++)
                {
                    TreeNode node = parentNode.Nodes[i];
                    if (node.Checked) { ichecks++; }
                }
                if (ichecks == parentNode.GetNodeCount(true))
                {
                    parentNode.Checked = true;
                    if (parentNode.Parent != null)
                    {
                        SetParentChecked(parentNode);
                    }
                }
                if (ichecks > 0 && !isUserClick)
                {
                    parentNode.Checked = true;
                }
            }
            else if (parentNode.Checked && !childNode.Checked)
            {
                int ichecks = 0;
                for (int i = 0; i < parentNode.GetNodeCount(false); i++)
                {
                    TreeNode node = parentNode.Nodes[i];
                    if (node.Checked) { ichecks++; }
                }
                if (ichecks == 0)
                    parentNode.Checked = false;
            }
        }
        /// <summary> 
        /// 根据父节点状态设置子节点的状态 
        /// </summary> 
        /// <param name="parentNode"></param> 
        private void SetChildChecked(TreeNode parentNode)
        {
            for (int i = 0; i < parentNode.GetNodeCount(false); i++)
            {
                TreeNode node = parentNode.Nodes[i];
                node.Checked = parentNode.Checked;
                if (node.GetNodeCount(false) > 0)
                {
                    SetChildChecked(node);
                }
            }
        }
      
    }
}
