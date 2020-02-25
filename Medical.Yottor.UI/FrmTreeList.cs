using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace Medical.Yottor.UI
{
    public partial class FrmTreeList : DevExpress.XtraEditors.XtraForm
    {
        public FrmTreeList()
        {
            InitializeComponent();
        }

        private DataTable GetTestData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentID");
            dt.Columns.Add("ID");
            dt.Columns.Add("MC");

            dt.Rows.Add(new object[] { "", "A", "开发部" });
            dt.Rows.Add(new object[] { "A", 2, "张三" });
            dt.Rows.Add(new object[] { "A", 3, "李四" });
            dt.Rows.Add(new object[] { "A", 4, "王五" });
            dt.Rows.Add(new object[] { "A", 5, "赵六" });

            dt.Rows.Add(new object[] { "", "B", "商务部" });
            dt.Rows.Add(new object[] { "B", 8, "张三" });
            dt.Rows.Add(new object[] { "B", 9, "李四" });
            dt.Rows.Add(new object[] { "B", 10, "王五" });
            dt.Rows.Add(new object[] { "B", 11, "赵六" });

            dt.Rows.Add(new object[] { "", "C", "财务部" });
            dt.Rows.Add(new object[] { "C", 13, "张三" });
            dt.Rows.Add(new object[] { "C", 14, "李四" });
            dt.Rows.Add(new object[] { "C", 15, "王五" });
            dt.Rows.Add(new object[] { "C", 16, "赵六" });

            return dt;
        }

        private void FrmTreeList_Load(object sender, EventArgs e)
        {
            DataTable dt = GetTestData();
            if (dt != null)
            {
                myTreeList.KeyFieldName = "ID";
                myTreeList.ParentFieldName = "ParentID";
                myTreeList.DataSource = dt;

                SetImageIndex(myTreeList, null, 1, 0);

                myTreeList.ExpandAll();
            }
        }

        /// <summary>
        /// 递归设置TreeList图片
        /// </summary>
        private void SetImageIndex(TreeList t1,TreeListNode node,int nodeIndex,int parentIndex)
        {
            if (node == null)
            {
                foreach (TreeListNode N in t1.Nodes)
                    SetImageIndex(t1, N, nodeIndex, parentIndex);
            }
            else
            {
                if (node.HasChildren || node.ParentNode == null)
                {
                    node.StateImageIndex = parentIndex;
                    node.ImageIndex = parentIndex;
                }
                else
                {
                    node.StateImageIndex = nodeIndex;
                    node.ImageIndex = nodeIndex;
                }

                foreach (TreeListNode N in node.Nodes)
                {
                    SetImageIndex(t1, N, nodeIndex, parentIndex);
                }
            }
        }

        private void myTreeList_Click(object sender, EventArgs e)
        {
            if (myTreeList.FocusedNode == null)
                return;

            TreeListNode node = myTreeList.FocusedNode;
            string id = node.GetDisplayText("ID");
            string mc = node.GetDisplayText("MC");

            MsgBox.ShowInformation(string.Format("你选择的是 {0} - {1}.", id, mc));
        }
    }
}