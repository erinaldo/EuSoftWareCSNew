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
using Medical.Yottor.Domain;
using EuSoft.BLL;
namespace Medical.Yottor.UI
{
    public partial class FrmUser : DevExpress.XtraEditors.XtraForm
    {
        DataTable dt = new DataTable();
        MCEUser User = new MCEUser();
        public FrmUser()
        {
            InitializeComponent();
            dt = GetTestData();
        }


        private DataTable GetTestData()
        {
            DataTable dt = new DataTable();

            dt = User.GetAllList().Tables[0];

            return dt;
        }


        public void BindDataList()
        {
            dt = GetTestData();
            this.gridControl1.DataSource = dt;
        }
      

        private void FrmUser_Load(object sender, EventArgs e)
        {
           
            if (dt != null)
            {
                this.gridControl1.DataSource = dt;
            }
           
        
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FrmUserEdit xFrm = new FrmUserEdit();
            if (xFrm.ShowDialog() == DialogResult.OK)
            {
                this.BindDataList();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int mUserId = Convert.ToInt16( this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]).ToString());
            if (User.Delete(mUserId))
            {
                this.BindDataList();
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            string mUserId = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]).ToString();

            string mUserName = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[1]).ToString();

            string mUserPwd = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[2]).ToString();

            FrmUserEdit Xfrm = new FrmUserEdit(mUserId, mUserName, mUserPwd);
            if (Xfrm.ShowDialog() == DialogResult.OK)
            {
                this.BindDataList();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridControl1_DoubleClick(null, null);
        }

      
     


       
    }
}