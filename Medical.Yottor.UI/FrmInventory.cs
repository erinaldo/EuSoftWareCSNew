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
    public partial class FrmInventory : DevExpress.XtraEditors.XtraForm
    {


        EuSoft.Model.MCEStock mMCEStock = new EuSoft.Model.MCEStock();
        EuSoft.BLL.MCEStock bMCEStock = new EuSoft.BLL.MCEStock();
        EuSoft.BLL.MCEStockonWay bMCEStockonWay = new EuSoft.BLL.MCEStockonWay();
        EuSoft.Model.MCEStockonWay mMCEStockonWay = new EuSoft.Model.MCEStockonWay();

        EuSoft.Model.MCEStockChangeRe mMCEStockChangeRe = new EuSoft.Model.MCEStockChangeRe();
        EuSoft.BLL.MCEStockChangeRe bMCEStockChangeRe = new EuSoft.BLL.MCEStockChangeRe();

        DataTable dtMCEOrderProInfo = new DataTable();
        public FrmInventory()
        {
            InitializeComponent();
            getDate();


        }

        public void getDate()
        {

            DataTable dt = bMCEStock.GetList(30, "", " ID desc").Tables[0];
            gridControl1.DataSource = dt;
            //gridView1.PopulateColumns();
            this.gridView1.BestFitColumns();
            // LabRecords.Text = "Records:" + dt.Rows.Count.ToString();

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
            sqlStr.Append(" 1=1");

            if (txtbatchNo.Text != "")
            {
                sqlStr.AppendFormat(" and StockBatchNo like '%{0}%' ", txtbatchNo.Text);
            }
            if (txtLocation.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and StockLocation like '%{0}%' ", txtLocation.Text);
            }
            if (txtValCode.Text != "")
            {
                sqlStr.AppendFormat(" and StockValCode like '%{0}%'  ", txtValCode.Text);
            }
            if (txtLibraryID.Text != "")
            {
                sqlStr.AppendFormat(" and  StockLibraryID like '%{0}%' ", txtLibraryID.Text);
            }
            if (txtNote.Text != "")
            {
                sqlStr.AppendFormat(" and StockNote  like '%{0}%'  ", txtNote.Text);
            }

            if (txtCaNo.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and StockCatalogNo like '%{0}%'", txtCaNo.Text);

            }


            sqlStr.Append(" order by ID desc");

            DataTable dt = bMCEStock.GetList(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
            //    LabRecords.Text = "Records:" + dt.Rows.Count.ToString();
            //   LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();

        }



        private void butClear_Click(object sender, EventArgs e)
        {

            txtNote.Text = "";
            txtCaNo.Text = "";
            txtLibraryID.Text = "";
            txtbatchNo.Text = "";
            txtValCode.Text = "";
            txtLocation.Text = "";
            txtCaNo.Text = "";
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

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            //GridControl grid = sender as GridControl;
            //if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && txtNoteOrderNo.Text != "")
            //{
            //    GridHitInfo hitInfo = gridView1.CalcHitInfo(e.Location);
            //    Point p = new Point(Cursor.Position.X, Cursor.Position.Y);

            //    popupMenu1.ShowPopup(p);

            //}
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







        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {

        }
        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //第一行  
            if (e.RowHandle > -1)
            {
                if (gridView1.GetRowCellValue(e.RowHandle, "TrackingNo") != null)
                {
                    if (gridView1.GetRowCellValue(e.RowHandle, "TrackingNo").ToString() != string.Empty)
                    {
                        if (e.Column.ColumnHandle == 0)
                        {
                            e.Appearance.BackColor = Color.MediumPurple;
                            e.Appearance.BackColor2 = Color.BlueViolet;
                        }
                    }

                }

            }
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

    

     



    }










}
