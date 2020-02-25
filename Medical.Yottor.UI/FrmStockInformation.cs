using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using FastReport.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maticsoft.DBUtility;
using DevExpress.Utils;
using System.Data.SqlClient;
using System.Text.RegularExpressions;//Please add references
using MySql.Data.MySqlClient;
using Medical.Yottor.Domain;
namespace Medical.Yottor.UI
{
    public partial class FrmStockInformation : DevExpress.XtraEditors.XtraForm
    {
        EuSoft.BLL.MCEProductsBasicinfo bMCEProductsBasicinfo = new EuSoft.BLL.MCEProductsBasicinfo();
        EuSoft.BLL.MCEStock bMCEStock = new EuSoft.BLL.MCEStock();
        EuSoft.BLL.MCEStockChina bMCEStockChina = new EuSoft.BLL.MCEStockChina();
        EuSoft.BLL.MCEStockSweden bMCEStockSweden = new EuSoft.BLL.MCEStockSweden();

        DataTable dtMCEOrderProInfo = new DataTable();
        public FrmStockInformation()
        {
            InitializeComponent();
            getDate();
          
        }

        public void getDate()
        {

            DataTable dt = bMCEProductsBasicinfo.GetList(30, "", "ID").Tables[0];
            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
        }

        /// <summary>
        /// 绑定ComBoBoxEdit下拉框数据  
        /// </summary>
        /// <param name="cmb">ComBoBoxEdit控件</param>
        /// <param name="dt">数据源</param>
        /// <param name="key">显示名称</param>
        /// <param name="val">对应EditValue值</param>
        public static void BindComboBoxEdit(ComboBoxEdit cmb, DataTable dt, int val)
        {
            cmb.Properties.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmb.Properties.Items.Add(dr[val].ToString());
            }
            if (cmb.Properties.Items.Count > 0)
                cmb.SelectedIndex = 1;
        }





        private void FrmOrder_Load(object sender, EventArgs e)
        {
            //  GridViewCreateNewCellItem.CreateClearCellItem(gridView2);
            dxValidationProvider1.ValidationMode = ValidationMode.Manual;
            dxValidationProvider1.Validate();

          
        }

        private void butSearch_Click(object sender, EventArgs e)
        {

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(" 1=1 ");
            if (txtCsNo.Text != "")
            {
                sqlStr.AppendFormat(" and CSNo like '%{0}%' ", txtCsNo.Text);
            }
        
            if (txtName.Text != "")
            {
                sqlStr.AppendFormat(" and DrugNames like '%{0}%'  ", txtName.Text);
            }
            if (txtCAS.Text != "")
            {
                sqlStr.AppendFormat(" and  CAS like '%{0}%' ", txtCAS.Text);
            }
            if (txtAName.Text != "")
            {
                sqlStr.AppendFormat(" and AlternativeNames  like '%{0}%'  ", txtAName.Text);
            }

            if (txtCaNo.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and CatalogNo  like '%{0}%'  ", txtCaNo.Text);

            }



            DataTable dt = bMCEProductsBasicinfo.GetList(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();

        }

    

        private void butClear_Click(object sender, EventArgs e)
        {
            txtAName.Text = "";
            txtCaNo.Text = "";
            txtCAS.Text = "";
            txtCsNo.Text = "";
            txtName.Text = "";
            txtCaNo.Text = "";
        }

    
        private void getDate(string CatalogNo,string vCode,string bNo)
        {
            string sqlwhere = " 1=1 ";
            if (CatalogNo!=string.Empty)
            {
                sqlwhere += " and StockCatalogNo = '" + CatalogNo + "'";
            }
            if (vCode!=string.Empty)
            {
                sqlwhere += " and StockValCode = '" + CatalogNo + "'";
            }
            if (bNo!=string.Empty)
            {
                sqlwhere += " and StockBatchNo = '" + CatalogNo + "'";
            }

            gridControl2.DataSource = bMCEStock.GetList(sqlwhere).Tables[0];
            gridControl3.DataSource = bMCEStockChina.GetList(sqlwhere).Tables[0];
            gridControl4.DataSource = bMCEStockSweden.GetList(sqlwhere).Tables[0];
            this.gridView2.BestFitColumns();
            this.gridView5.BestFitColumns();
            this.gridView7.BestFitColumns();
        }

       



        public class GridViewAddPopupMenuBase
        {

            EventHandler OnClearCellClick;
            string MenuName;



            public static void CreateNewCellItem(GridView View, string cMenuName, EventHandler DoClearCellClick)
            {
                GridViewAddPopupMenuBase gb = new GridViewAddPopupMenuBase();
                gb.OnClearCellClick = DoClearCellClick;
                gb.MenuName = cMenuName;


                View.PopupMenuShowing += new PopupMenuShowingEventHandler(gb.Create_NewCellItem);
            }
            void Create_NewCellItem(object sender, PopupMenuShowingEventArgs e)
            {
                //if (e.MenuType   == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                //{
                //if (((GridView)sender).OptionsBehavior.Editable == true)
                //{
                if (e.HitInfo.InRowCell && e.HitInfo.Column.OptionsColumn.AllowEdit == true)
                {

                    e.Menu.Items.Add(CreateNewMenuItem((GridView)sender));


                }
                //}
                //}
            }

            DXMenuItem CreateNewMenuItem(GridView view)
            {
                DXMenuItem copyitem = new DXMenuItem(MenuName, new EventHandler(OnClearCellClick), null);

                return copyitem;

            }




        }
        public class GridViewCreateNewCellItem : GridViewAddPopupMenuBase
        {
            #region 添加复制Cell菜单
            public static void CreateClearCellItem(GridView View)
            {
                CreateNewCellItem(View, "Add", DoAdd);
                CreateNewCellItem(View, "Update", DoUpdate);
                CreateNewCellItem(View, "Delete", DoDel);

            }

            private static void DoAdd(object sender, EventArgs e)
            {
                //GridColumn col = (GridColumn)((DXMenuItem)sender).Tag;
                //col.View.SetRowCellValue(col.View.FocusedRowHandle, col, DBNull.Value);
            }
            private static void DoUpdate(object sender, EventArgs e)
            {
                //GridColumn col = (GridColumn)((DXMenuItem)sender).Tag;
                //col.View.SetRowCellValue(col.View.FocusedRowHandle, col, DBNull.Value);
            }
            private static void DoDel(object sender, EventArgs e)
            {
                //GridColumn col = (GridColumn)((DXMenuItem)sender).Tag;
                //col.View.SetRowCellValue(col.View.FocusedRowHandle, col, DBNull.Value);
            }


            #endregion
        }

     





        /// <summary>



        /// <summary>  
        /// 验证控件样式  
        /// </summary>  
        /// <param name="c"></param>  
        /// <param name="ErrorText"></param>  
        private void Validate(Control c, string ErrorText)
        {
            ConditionValidationRule notEmpty = new ConditionValidationRule();
            notEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            notEmpty.ErrorText = ErrorText;
            dxValidationProvider1.SetValidationRule(c, notEmpty);
            dxValidationProvider1.SetIconAlignment(c, ErrorIconAlignment.MiddleRight);

        }







 


        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridControl1) return;

            ToolTipControlInfo info = null;
            //Get the view at the current mouse position
            GridView view = gridControl1.GetViewAt(e.ControlMousePosition) as GridView;
            if (view == null) return;

            //Get the view's element information that resides at the current position
            GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
            //Display a hint for row indicator cells
            if (hi.HitTest == GridHitTest.RowIndicator)
            {
                //An object that uniquely identifies a row indicator cell
                object o = hi.HitTest.ToString() + hi.RowHandle.ToString();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Row  basic information：");
                foreach (GridColumn gridCol in view.Columns)
                {
                    if (gridCol.Visible)
                    {
                        sb.AppendFormat("    {0}：{1}\r\n", gridCol.Caption, view.GetRowCellDisplayText(hi.RowHandle, gridCol.FieldName));
                    }
                }
                info = new ToolTipControlInfo(o, sb.ToString());
            }

            //Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
            if (info != null)
            {
                e.Info = info;
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {

            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }

        }

    
     
      

     
        private void ClearTxt()
        {

            txtValCode.Text = "";
        }

  

       
        private void butClearTra_Click(object sender, EventArgs e)
        {
            txtCaTaNo.Text = "";
            txtValCode.Text = "";
            txtBatch.Text = "";
          
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                getDate(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "CatalogNo").ToString(),"","");
            }
        }

        private void butSearchStock_Click(object sender, EventArgs e)
        {
  
            getDate(txtCaTaNo.Text, txtValCode.Text, txtBatch.Text);
        }

       

     

      
        }

      

    






    }
