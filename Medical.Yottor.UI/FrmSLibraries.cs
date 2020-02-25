using Medical.Yottor.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Medical.Yottor.UI
{
    public partial class FrmSLibraries : DevExpress.XtraEditors.XtraForm
    {
        public FrmSLibraries()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void butSearch_Click(object sender, EventArgs e)
        {
            string sqlStr;
            sqlStr = "select MCEOrderProInfo.OrderNo,MCEScreeningLibraries.LibraryID,CatalogNO,SizeUnit,Note,convert(varchar(20), MCEScreeningLibraries.UpdateTime,101) UpdateTime,MCEScreeningLibraries.Person  from  MCEScreeningLibraries LEFT JOIN  MCEOrderProInfo ON  MCEScreeningLibraries.LibraryID=MCEOrderProInfo.ProLibraryID where 1 = 1 ";
            if (txtNote.Text != string.Empty)
            {
                sqlStr += string.Format(" and Note like '%{0}%'", txtNote.Text);
            }
            if (txtCatalogNo.Text != string.Empty)
            {
                sqlStr += string.Format(" and CatalogNo like '%{0}%'", txtCatalogNo.Text);
            }
            if (txtOrNo.Text != "")
            {
                sqlStr += string.Format(" and MCEOrderProInfo.OrderNo like '%{0}%' ", txtOrNo.Text);
            }
            if (txtSize.Text != "")
            {
                sqlStr += string.Format(" and  MCEScreeningLibraries.SizeUnit like '%{0}%' ", txtSize.Text);
            }
            if (txtLibraryID.Text != "")
            {
                sqlStr += string.Format(" and MCEScreeningLibraries.LibraryID  like '%{0}%'  ", txtLibraryID.Text);
            }

            sqlStr += "order by MCEScreeningLibraries.LibraryID  desc";

            dt = Maticsoft.DBUtility.DbHelperSQL.Query(sqlStr).Tables[0];
          

            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();

        }

  


        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
           
          
        }

  

     

        private void FrmOrderProductsInfor_Load(object sender, EventArgs e)
        {

        }

      

        private void butClear_Click(object sender, EventArgs e)
        {
            txtNote.Text = "";
            txtOrNo.Text = "";
            txtCatalogNo.Text = "";
            txtLibraryD.Text = "";
            txtSize.Text = "";
        
        }


    }
}
