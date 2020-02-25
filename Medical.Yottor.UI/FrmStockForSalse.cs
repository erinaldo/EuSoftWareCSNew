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
using Agile.Report;
namespace Medical.Yottor.UI
{
    public partial class FrmStockForSalse : DevExpress.XtraEditors.XtraForm
    {
        EuSoft.BLL.MCEShipmentInfo bMCEShipmentInfo = new EuSoft.BLL.MCEShipmentInfo();
        EuSoft.Model.MCEShipmentInfo mMCEShipmentInfo = new EuSoft.Model.MCEShipmentInfo();
        EuSoft.BLL.MCECarrierDefinition bMcu = new EuSoft.BLL.MCECarrierDefinition();
        EuSoft.BLL.MCEOrderInfo bMCEOrderInfo = new EuSoft.BLL.MCEOrderInfo();
        EuSoft.Model.MCEOrderInfo mMCEOrderInfo = new EuSoft.Model.MCEOrderInfo();
        EuSoft.BLL.MCEShipmentInfoChangeRe bMCEShipmentInfoChangeRe = new EuSoft.BLL.MCEShipmentInfoChangeRe();
        EuSoft.Model.MCEShipmentInfoChangeRe mMCEShipmentInfoChangeRe = new EuSoft.Model.MCEShipmentInfoChangeRe();

        EuSoft.BLL.MCEOrderProInfo bMCEOrderProInfo = new EuSoft.BLL.MCEOrderProInfo();
        EuSoft.BLL.MCEUnitDefinition bMCEUnitDefinition = new EuSoft.BLL.MCEUnitDefinition();
        EuSoft.BLL.MCEShipProInfo bMCEShipProInfo = new EuSoft.BLL.MCEShipProInfo();
        EuSoft.Model.MCEShipProInfo mMCEShipProInfo = new EuSoft.Model.MCEShipProInfo();

        EuSoft.Model.MCEStock mMCEStock = new EuSoft.Model.MCEStock();
        EuSoft.BLL.MCEStock bMCEStock = new EuSoft.BLL.MCEStock();
        EuSoft.BLL.MCEShipProInfoChangeRe bMCEShipProInfoChangeRe = new EuSoft.BLL.MCEShipProInfoChangeRe();
        EuSoft.Model.MCEShipProInfoChangeRe mMCEShipProInfoChangeRe = new EuSoft.Model.MCEShipProInfoChangeRe();

        DataTable dtMCEOrderProInfo = new DataTable();
        DataTable dt = new DataTable();
        StringBuilder sqlStr = new StringBuilder();
        public FrmStockForSalse()
        {
            InitializeComponent();
            getDate();
            butSearch_Click(null, null);
            //this.gridView2.UpdateCurrentRow();
            //gridControl2.DataSource = getEmpDataTable();
            //this.gridView2.PopulateColumns();
            groupControl7.Visible = false;
        }

        public void getDate()
        {
            //StringBuilder sqlStr = new StringBuilder();
            //sqlStr.Append("select top 30 convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo,InvioceNo,SalesCompany,PONumber,ShipStatus,convert(varchar(20),ShipDate,101) as ShipDate,ShipVia,TrackingNo,MCEShipmentInfo.Note, MCEOrderInfo.id    from MCEOrderInfo   LEFT JOIN MCEShipmentInfo ON MCEShipmentInfo.OrderNo=MCEOrderInfo.OrderNo where ( OrderDate >  dateadd(day,-1,getdate()) or ShipStatus = 'Unshipped')");
            //sqlStr.Append(" order by MCEOrderInfo.orderno desc");
            //DataTable dt = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            //gridControl1.DataSource = dt;
            ////gridView1.PopulateColumns();
            //this.gridView1.BestFitColumns();
            //LabRecords.Text = "Records:" + dt.Rows.Count.ToString();
            DataTable dtbMCEUnitDefinition = new DataTable();
            dtbMCEUnitDefinition = bMCEUnitDefinition.GetAllList().Tables[0];
            BindComboBoxEdit(txtProUnit, dtbMCEUnitDefinition, 1);



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
            sqlStr.Clear();

            sqlStr.Append("select convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo,InvioceNo,SalesCompany,PONumber,ShipStatus,convert(varchar(20),ShipDate,101) as ShipDate,ShipVia,TrackingNo,b.Note, MCEOrderInfo.id,MCEOrderInfo.StockStatus   from MCEOrderInfo   LEFT JOIN CSShipmentInfo b ON b.OrderNo=MCEOrderInfo.OrderNo where ( OrderDate >  dateadd(day,-1,getdate()) or ShipStatus = 'Unshipped')  and [OrderProcess] = 'Finalize' ");


            if (txtOrderNo.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtTracking.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and TrackingNo like '%{0}%' ", txtTracking.Text);
            }
            if (txtCompany.Text != "")
            {
                sqlStr.AppendFormat(" and [SalesCompany] like '%{0}%'  ", txtCompany.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr.AppendFormat(" and  PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr.AppendFormat(" and InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }

            if (txtCaNo.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and exists (select * from dbo.MCEOrderProInfo where  procatalogno like '%" + txtCaNo.Text + "%' and MCEOrderInfo.orderno=orderno)");

            }

            if (txtDate1.Text != "")
            {
                sqlStr.AppendFormat(" and  OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlStr.AppendFormat(" and  OrderDate<='{0}'", txtDate2.Text);
            }
            sqlStr.Append(" order by MCEOrderInfo.orderno desc");

            DataTable dt = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dt.Rows.Count.ToString();
            //   LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();

        }

        private void butAll_Click(object sender, EventArgs e)
        {
            sqlStr.Clear();
            sqlStr.Append("select convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo,InvioceNo,SalesCompany,PONumber,ShipStatus,convert(varchar(20),ShipDate,101) as ShipDate,ShipVia,TrackingNo,b.Note, MCEOrderInfo.id ,MCEOrderInfo.StockStatus    from MCEOrderInfo   LEFT JOIN CSShipmentInfo b ON b.OrderNo=MCEOrderInfo.OrderNo where 1=1 and [OrderProcess] = 'Finalize' ");


            if (txtOrderNo.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtTracking.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and TrackingNo like '%{0}%' ", txtTracking.Text);
            }
            if (txtCompany.Text != "")
            {
                sqlStr.AppendFormat(" and [SalesCompany] like '%{0}%'  ", txtCompany.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr.AppendFormat(" and  PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr.AppendFormat(" and InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }
            if (txtCaNo.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and exists (select * from dbo.MCEOrderProInfo where  procatalogno like '%" + txtCaNo.Text + "%' and MCEOrderInfo.orderno=orderno)");
            }


            if (txtDate1.Text != "")
            {
                sqlStr.AppendFormat(" and  OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlStr.AppendFormat(" and  OrderDate<='{0}'", txtDate2.Text);
            }
            sqlStr.Append(" order by MCEOrderInfo.orderno desc");

            DataTable dt = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dt.Rows.Count.ToString();
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            txtDate1.Text = "";
            txtDate2.Text = "";
            txtInvoiceNo.Text = "";
            txtCaNo.Text = "";
            txtPONo.Text = "";
            txtOrderNo.Text = "";
            txtCompany.Text = "";
            txtTracking.Text = "";
            txtCaNo.Text = "";
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                //int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[10]).ToString());
                string orderNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[1]).ToString();
                txtShipDate.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipDate").ToString();
                txtShipVia.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipVia").ToString();
                txtTrk.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "TrackingNo").ToString();
                txtNote.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "Note").ToString();
                txtTOrdeNo.Text = orderNo;
                dtMCEOrderProInfo = bMCEOrderProInfo.GetList(" orderno='" + orderNo + "'").Tables[0];
                gridControl2.DataSource = dtMCEOrderProInfo;
                // gridControl3.DataSource = bMCEShipProInfo.GetList(" TrackingNo='" + txtTrk.Text + "' and orderno='" + orderNo + "'").Tables[0];

                gridControl3.DataSource = DbHelperSQL.Query(" select * from dbo.CSShipProInfo where orderno='" + orderNo + "'").Tables[0];
                gridView2.BestFitColumns();
                gridView5.BestFitColumns();
                groupControl7.Visible = false;
            }
        }


        private void getShipDate(string orderNo)
        {
            string sqlWhere = string.Format(" orderno='{0}' ", orderNo);
            txtShipDate.Text = DateTime.Now.ToShortDateString();
            //this.gridView2.UpdateCurrentRow();
            gridControl2.DataSource = bMCEShipmentInfo.GetList(sqlWhere).Tables[0];
            this.gridView2.BestFitColumns();
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




        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {

                //if (MessageDxUtil.ShowYesNoAndWarning("Delete the product " + this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, this.gridView2.Columns[1]) + " from " + txtNoteOrderNo.Text + " ? ") == DialogResult.Yes)
                //{
                //    int id = Convert.ToInt32(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, this.gridView2.Columns[0]));


                //    StringBuilder sqlStr = new StringBuilder();
                //    sqlStr.Append("INSERT INTO [dbo].[MCEOrderProInfoChangeRe]([OrderNo],[ProCatalogNo],[ProDescription],[ProSize],[ProUnit],[ProQuantity],[ProAmount],[ProCurrency],");
                //    sqlStr.Append(" [ProDunOn],[ProNote],[ProLibraryID],[ProductStatus],[ProductProcess],[UpdateTime],[Person],[ChangeNote],[ChangePerson],[ChangeTime])");
                //    sqlStr.Append("select [OrderNo],[ProCatalogNo],[ProDescription],[ProSize],[ProUnit],[ProQuantity],[ProAmount],[ProCurrency],[ProDunOn],[ProNote],[ProLibraryID]");
                //    sqlStr.AppendFormat(",[ProductStatus],[ProductProcess],[UpdateTime],'{0}','{1}','{2}',getdate() FROM [dbo].[MCEOrderProInfo]", Properties.Settings.Default.LastUser, "ID " + id + " Deleted", Properties.Settings.Default.LastUser);
                //    sqlStr.AppendFormat("  where id={0}", id);
                //    if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(sqlStr.ToString()) > 0)
                //    {

                //    }



                //    if (bMCEOrderProInfo.Delete(id))
                //    {
                //        this.gridControl1_Click(null, null);
                //    }
                //}
            }
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridControl1_Click(null, null);
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
            string status;
            if (e.RowHandle > -1)
            {
                status = gridView1.GetRowCellValue(e.RowHandle, "ShipStatus").ToString();
                if (status == "Shipped")
                {
                    e.Appearance.BackColor = Color.Lime;
                }

            }
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

                if (gridView1.GetRowCellValue(e.RowHandle, "StockStatus") != null)
                {
                    if (gridView1.GetRowCellValue(e.RowHandle, "StockStatus").ToString() != string.Empty)
                    {
                        if (e.Column.ColumnHandle == 1)
                        {
                            e.Appearance.BackColor = Color.Aquamarine;
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


        private string PayType(string strType)
        {
            string typeStr = "";
            if (strType.ToLower().IndexOf("check or wire transfer") > 0)
                typeStr = "Check or Wire Transfer";
            else if (strType.ToLower().IndexOf("call") > 0)
                typeStr = "Call for CC information";
            else if (strType.ToLower().IndexOf("credit card") > 0)
                typeStr = "Credit Card";
            return typeStr;
        }




        private void ClearTxt()
        {

            txtTrk.Text = "";
        }



        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                var id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString());
                if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipStatus") != null)
                {
                    if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipStatus").ToString() != string.Empty)
                    {
                        var ShipmentStatus = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipStatus").ToString() == "Shipped" ? "Unshipped" : "Shipped";
                        if (DbHelperSQL.ExecuteSql("update MCEOrderInfo set ShipStatus='" + ShipmentStatus + "' where id=" + id + string.Empty) > 0)
                        {
                            dt.Rows.Cast<DataRow>().ToList<DataRow>().ForEach(s =>
                            {
                                var temp_id = s["id"].ToString();
                                if (temp_id == id.ToString())
                                {
                                    s["ShipStatus"] = ShipmentStatus;
                                }
                            });

                            gridControl1.DataSource = dt;
                            gridControl1.RefreshDataSource();
                            MessageDxUtil.ShowTips("success");
                        }
                    }
                }



            }
        }


        private void butClearTra_Click(object sender, EventArgs e)
        {
            txtShipDate.Text = "";
            txtShipVia.Text = "";
            txtTrk.Text = "";
            txtNote.Text = "";
            txtTOrdeNo.Text = "";
        }

        private void gridControl2_Click_1(object sender, EventArgs e)
        {
            if (this.gridView2.FocusedRowHandle > -1)
            {
                txtProductID.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ID").ToString();
                txtPCatlogNo.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProCatalogNo").ToString();
                txtProValCode.Focus();
            }
        }

        private void txtTrk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtTrk.Text != "" && e.KeyChar == 13)
            {
                DataTable dt = new DataTable();

                dt = bMCEShipmentInfo.GetList(" TrackingNo='" + txtTrk.Text + "' ").Tables[0];
                if (dt.Rows.Count > 0)
                {

                    txtShipDate.Text = dt.Rows[0]["ShipDate"].ToString();
                    txtShipVia.Text = dt.Rows[0]["ShipVia"].ToString();
                    txtTrk.Text = dt.Rows[0]["TrackingNo"].ToString();
                    txtNote.Text = dt.Rows[0]["Note"].ToString();
                    txtTOrdeNo.Text = dt.Rows[0]["OrderNo"].ToString();
                    LabID.Text = dt.Rows[0]["id"].ToString();
                    dtMCEOrderProInfo = bMCEOrderProInfo.GetList(" orderno='" + txtTOrdeNo.Text + "'").Tables[0];
                    gridControl2.DataSource = dtMCEOrderProInfo;
                    gridView2.BestFitColumns();

                    //  gridControl3.DataSource = bMCEShipProInfo.GetList(" orderno='" + txtTOrdeNo.Text + "'").Tables[0];
                }
                txtProValCode.Focus();
            }

        }

        private void txtProValCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtProValCode.Text != "" && e.KeyChar == 13)
            {
                if (txtProductID.Text != string.Empty)
                {

                    DataTable dt = new DataTable();
                    dt = bMCEStock.GetList("StockValCode =  '" + txtProValCode.Text + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  txtPCatlogNo.Text = dt.Rows[0]["StockCSNo"].ToString();

                        if (dt.Rows[0]["StockCSNo"].ToString() != txtPCatlogNo.Text)
                        {
                            MessageDxUtil.ShowWarning("Product error!");
                            return;
                        }

                        txtProSize.Text = dt.Rows[0]["StockSize"].ToString();
                        txtProUnit.Text = dt.Rows[0]["StockUnit"].ToString();
                        txtProBatchNo.Text = dt.Rows[0]["StockBatchNo"].ToString();
                        txtProNote.Text = dt.Rows[0]["StockNote"].ToString();
                        txtProLibraryID.Text = dt.Rows[0]["StockLibraryID"].ToString();
                        LabID.Text = dt.Rows[0]["ID"].ToString();
                        labrkrq.Text = dt.Rows[0]["rkrq"].ToString();
                        if (checkAuto.Checked)
                        {
                            butAdd_Click(null, null);
                            if (checkLable.Checked)
                            {


                                printsmall(dt, Application.StartupPath);
                            }
                        }
                    }
                    else
                    {
                        MessageDxUtil.ShowWarning("The Val Code can not find in the stock.");
                    }
                }
                else
                {
                    MessageDxUtil.ShowWarning("Please choose the product for shipping!");
                }
            }


        }

        private double getUnit(string LType)
        {
            double unitS = 0.00;
            switch (LType.ToLower())
            {
                case "ug":
                    unitS = 0.000001;
                    break;
                case "mg":
                    unitS = 0.001;
                    break;
                case "g":
                    unitS = 1;
                    break;
                case "kg":
                    unitS = 1000;
                    break;
            }
            return unitS;
        }

        public void printsmall(DataTable dt, string reportPath)
        {
            //StockCSNo	StockSize	StockUnit	StockValCode	StockBatchNo storage
            ReportEx report = new ReportEx();
            var dtRow = dt.AsEnumerable().Where<DataRow>(x => x["StockBatchNo"].ToString() == txtProBatchNo.Text);
            foreach (DataRow item in dtRow)
            {

                report.AddParameter("valcode", item["StockValCode"].ToString());
                report.AddParameter("CS_No", item["StockCSNo"].ToString());
                report.AddParameter("size", item["StockSize"].ToString());
                report.AddParameter("name", item["DrugNames"].ToString());
                report.AddParameter("cas", item["cas"].ToString());
                report.AddParameter("mwt", item["mwt"].ToString());
                report.AddParameter("lot", item["StockBatchNo"].ToString());
                report.AddParameter("pruity", item["pruity"].ToString());
                report.AddParameter("Storage", item["Storage"].ToString());
                report.LoadFrom(System.IO.Path.Combine(reportPath, "lysmall.frx"));
                report.Print(false, "");
            }


        }



        private void butAdd_Click(object sender, EventArgs e)
        {
            if (txtShipVia.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the shipment information fist! The carrier can not be empty.");
                return;
            }
            if (txtPCatlogNo.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please choose the product to input.Catalog No. can not be empty.");
                return;
            }
            if (txtProductID.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please choose the product to input.Prodcut ID can not be empty.");
                return;
            }
            if (txtProUnit.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please choose the Unite.");
                return;
            }
            if (txtProBatchNo.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the Batch No.");
                return;
            }
            if (txtProSize.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the Size.");
                return;
            }
            if (txtProBatchNo.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the batch No.");
                return;
            }
            double myAll, youAll;


            if (this.gridView2.FocusedRowHandle > -1)
            {
                myAll = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProSize").ToString()) * Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProQuantity").ToString()) * getUnit(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProUnit").ToString());
                youAll = getUnit(txtProUnit.Text) * Convert.ToDouble(txtProSize.Text);
                if (youAll > myAll * 1.2)
                {
                    MessageDxUtil.ShowWarning("The product weight is greater than 20% of the quantity of the order product!");
                    //    return;
                }

            }



            //report.AddParameter("valcode", dt.Rows[0]["valcode"].ToString());

            //report.AddParameter("CS_No", dt.Rows[0]["CS_No"].ToString());
            //report.AddParameter("size", dt.Rows[0]["size"].ToString());
            //report.AddParameter("name", dt.Rows[0]["name"].ToString());
            //report.AddParameter("cas", dt.Rows[0]["cas"].ToString());
            //report.AddParameter("mwt", dt.Rows[0]["mwt"].ToString());
            //report.AddParameter("lot", dt.Rows[0]["lot"].ToString());
            //report.AddParameter("pruity", dt.Rows[0]["pruity"].ToString());
            //report.AddParameter("Storage", dt.Rows[0]["Storage"].ToString());




            if (checkAuto.Checked)
            {
                mMCEShipProInfo.TrackingNo = txtTrk.Text;
                mMCEShipProInfo.ShipCatalogNo = txtPCatlogNo.Text;
                mMCEShipProInfo.ShipBatchNo = txtProBatchNo.Text;
                mMCEShipProInfo.OrderNo = txtTOrdeNo.Text;
                mMCEShipProInfo.ShipLibraryID = txtProLibraryID.Text;
                mMCEShipProInfo.ShipValCode = txtProValCode.Text;
                mMCEShipProInfo.ShipSize = Convert.ToDecimal(txtProSize.Text);
                mMCEShipProInfo.ShipUnit = txtProUnit.Text;
                mMCEShipProInfo.OrginalID = txtProductID.Text;
                mMCEShipProInfo.Person = Properties.Settings.Default.LastUser;
                mMCEShipProInfo.UpdateTime = DateTime.Now;
                mMCEShipProInfo.ShipNote = txtProNote.Text;
                mMCEShipProInfo.Rkrq = Convert.ToDateTime(labrkrq.Text);
                if (bMCEShipProInfo.Add(mMCEShipProInfo) > 0)
                {

                    gridControl3.DataSource = bMCEShipProInfo.GetList(" TrackingNo='" + txtTrk.Text + "' and orderno='" + txtTOrdeNo.Text + "'").Tables[0];
                    gridView5.BestFitColumns();
                    if (DbHelperSQL.ExecuteSqlMCE("delete from MCEStock where id=" + LabID.Text + "") > 0)
                    {
                        txtPCatlogNo.Text = "";
                        txtProSize.Text = "";
                        txtProUnit.Text = "";
                        txtProValCode.Text = "";
                        txtProBatchNo.Text = "";
                        txtProNote.Text = "";
                        txtProLibraryID.Text = "";
                        txtProductID.Text = "";

                    }

                }
            }
            else
            {
                if (MessageDxUtil.ShowYesNoAndWarning("Add product " + txtPCatlogNo.Text + " to shipment Tracking#" + txtTrk.Text + " ?") == DialogResult.Yes)
                {
                    mMCEShipProInfo.TrackingNo = txtTrk.Text;
                    mMCEShipProInfo.ShipCatalogNo = txtPCatlogNo.Text;
                    mMCEShipProInfo.ShipBatchNo = txtProBatchNo.Text;
                    mMCEShipProInfo.OrderNo = txtTOrdeNo.Text;
                    mMCEShipProInfo.ShipLibraryID = txtProLibraryID.Text;
                    mMCEShipProInfo.ShipValCode = txtProValCode.Text;
                    mMCEShipProInfo.ShipSize = Convert.ToDecimal(txtProSize.Text);
                    mMCEShipProInfo.ShipUnit = txtProUnit.Text;
                    mMCEShipProInfo.OrginalID = txtProductID.Text;
                    mMCEShipProInfo.Person = Properties.Settings.Default.LastUser;
                    mMCEShipProInfo.UpdateTime = DateTime.Now;
                    mMCEShipProInfo.ShipNote = txtProNote.Text;
                    if (bMCEShipProInfo.Add(mMCEShipProInfo) > 0)
                    {

                        gridControl3.DataSource = bMCEShipProInfo.GetList(" TrackingNo='" + txtTrk.Text + "' and orderno='" + txtTOrdeNo.Text + "'").Tables[0];
                        gridView5.BestFitColumns();
                        if (DbHelperSQL.ExecuteSqlMCE("delete from MCEStock where id=" + LabID.Text + "") > 0)
                        {
                            txtPCatlogNo.Text = "";
                            txtProSize.Text = "";
                            txtProUnit.Text = "";
                            txtProValCode.Text = "";
                            txtProBatchNo.Text = "";
                            txtProNote.Text = "";
                            txtProLibraryID.Text = "";
                            txtProductID.Text = "";
                        }

                    }
                }

            }

        }

        private void ButDel_Click(object sender, EventArgs e)
        {
            if (this.gridView5.FocusedRowHandle > -1)
            {
                if (gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "StockStatus").ToString() == "")
                {
                    if (gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "StockStatus").ToString() == "CA")
                    {
                        MessageDxUtil.ShowWarning("Cannot delete the Shipping record of CA!");
                        return;
                    }


                    if (bMCEStock.GetList(" StockValCode='" + gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "ShipValCode") + "'").Tables[0].Rows.Count > 0)
                    {
                        MessageDxUtil.ShowWarning("The ValCode deleted had not been existed in stock, please check again!");
                        return;
                    }
                    else
                    {
                        if (MessageDxUtil.ShowYesNoAndWarning("Delete the product ID " + gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "ID") + " ?") == DialogResult.Yes)
                        {
                            mMCEShipProInfo = bMCEShipProInfo.GetModel(Convert.ToInt32(gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "ID").ToString()));

                            mMCEStock.StockCSNo = mMCEShipProInfo.ShipCatalogNo;
                            mMCEStock.StockBatchNo = mMCEShipProInfo.ShipBatchNo;
                            mMCEStock.StockLibraryID = mMCEShipProInfo.ShipLibraryID;
                            mMCEStock.StockNote = mMCEShipProInfo.ShipNote;
                            mMCEStock.StockSize = mMCEShipProInfo.ShipSize.ToString();
                            mMCEStock.StockUnit = mMCEShipProInfo.ShipUnit;
                            mMCEStock.StockValCode = mMCEShipProInfo.ShipValCode;

                            mMCEStock.StockNote = "Restore from Shipping List:Prodcut ID " + Convert.ToInt32(gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "ID").ToString());
                            mMCEStock.Person = Properties.Settings.Default.LastUser;
                            mMCEStock.UpdateTime = DateTime.Now;
                            mMCEStock.Rkrq = mMCEShipProInfo.Rkrq;


                            string strSql = "INSERT INTO [dbo].[MCEStock]([StockCatalogNo] ,[StockCSNo],[StockSize],[StockUnit],[StockValCode],[StockBatchNo],[StockNote] ,[StockLibraryID],[SysNote],[UpdateTime],[Person],rkrq)";
                            strSql += " select CatalogNo,'" + mMCEStock.StockCSNo + "','" + mMCEStock.StockSize + "','" + mMCEStock.StockUnit + "','" + mMCEStock.StockValCode + "','" + mMCEStock.StockBatchNo + "','" + mMCEStock.StockNote + "','" + mMCEStock.StockLibraryID + "','" + mMCEStock.StockNote + "','" + mMCEStock.UpdateTime + "','" + mMCEStock.Person + "','" + mMCEStock.Rkrq + "' from  MCEProductsBasicinfo where CSNo='" + mMCEStock.StockCSNo + "'";

                            if (DbHelperSQL.ExecuteSqlMCE(strSql) > 0)
                            {

                            }
                            mMCEShipProInfoChangeRe.TrackingNo = mMCEShipProInfo.TrackingNo;
                            mMCEShipProInfoChangeRe.ShipCatalogNo = mMCEShipProInfo.ShipCatalogNo;
                            mMCEShipProInfoChangeRe.ShipSize = mMCEShipProInfo.ShipSize;
                            mMCEShipProInfoChangeRe.ShipUnit = mMCEShipProInfo.ShipUnit;
                            mMCEShipProInfoChangeRe.ShipValCode = mMCEShipProInfo.ShipValCode;
                            mMCEShipProInfoChangeRe.ShipBatchNo = mMCEShipProInfo.ShipBatchNo;
                            mMCEShipProInfoChangeRe.ShipNote = mMCEShipProInfo.ShipNote;
                            mMCEShipProInfoChangeRe.ShipLibraryID = mMCEShipProInfo.ShipLibraryID;
                            mMCEShipProInfoChangeRe.UpdateTime = mMCEShipProInfo.UpdateTime;
                            mMCEShipProInfoChangeRe.Person = mMCEShipProInfo.Person;
                            mMCEShipProInfoChangeRe.OrginalID = mMCEShipProInfo.OrginalID;
                            mMCEShipProInfoChangeRe.Flag = mMCEShipProInfo.Flag;
                            mMCEShipProInfoChangeRe.ChangeNote = "Deleted ID " + Convert.ToInt32(gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "ID").ToString());
                            mMCEShipProInfoChangeRe.ChangePerson = Properties.Settings.Default.LastUser;
                            mMCEShipProInfoChangeRe.ChangeTime = DateTime.Now;

                            if (bMCEShipProInfoChangeRe.Add(mMCEShipProInfoChangeRe) > 0)
                            {
                                if (bMCEShipProInfo.Delete(Convert.ToInt32(Convert.ToInt32(gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "ID").ToString()))))
                                {

                                }
                            }
                            gridControl3.DataSource = bMCEShipProInfo.GetList(" TrackingNo='" + txtTrk.Text + "' and orderno='" + txtTOrdeNo.Text + "'").Tables[0];
                            gridView5.BestFitColumns();
                        }
                    }
                }
            }
            else
            {
                MessageDxUtil.ShowWarning("Please choose the record to delete.");
            }
        }



        private void txtPCatlogNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPCatlogNo.Text != "" && e.KeyChar == 13)
                gridControl4.DataSource = bMCEStock.GetList(" StockCSNo='" + txtPCatlogNo.Text + "'").Tables[0];
            groupControl7.Visible = true;
            this.gridView7.BestFitColumns();
        }

        private void gridControl4_Click(object sender, EventArgs e)
        {
            if (this.gridView7.FocusedRowHandle >= 0)
            {
                txtPCatlogNo.Text = gridView7.GetRowCellValue(gridView7.FocusedRowHandle, "StockCSNo").ToString();
                txtProSize.Text = gridView7.GetRowCellValue(gridView7.FocusedRowHandle, "StockSize").ToString();
                txtProUnit.Text = gridView7.GetRowCellValue(gridView7.FocusedRowHandle, "StockUnit").ToString();
                txtProNote.Text = gridView7.GetRowCellValue(gridView7.FocusedRowHandle, "StockNote").ToString();
                txtProBatchNo.Text = gridView7.GetRowCellValue(gridView7.FocusedRowHandle, "StockBatchNo").ToString();
                txtProLibraryID.Text = gridView7.GetRowCellValue(gridView7.FocusedRowHandle, "StockLibraryID").ToString();
                txtProValCode.Text = gridView7.GetRowCellValue(gridView7.FocusedRowHandle, "StockValCode").ToString();
                LabID.Text = gridView7.GetRowCellValue(gridView7.FocusedRowHandle, "ID").ToString();
                butAdd_Click(null, null);
                groupControl7.Visible = false;
                //txtProductID.Text = gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "id").ToString();
            }
        }




        private void groupControl7_Click(object sender, EventArgs e)
        {
            groupControl7.Visible = false;
        }

        private void txtProValCode_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridControl3_Click(object sender, EventArgs e)
        {

        }

        private void butAuto_Click(object sender, EventArgs e)
        {
            string sqlwhere = " ";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    sqlwhere = sqlwhere + " and   orderno in ( '" + dt.Rows[i]["OrderNo"].ToString() + "',";
                }
                else if (i == dt.Rows.Count - 1)
                {
                    sqlwhere = sqlwhere + "'" + dt.Rows[i]["OrderNo"].ToString() + "')";
                }
                else
                {
                    sqlwhere = sqlwhere + "'" + dt.Rows[i]["OrderNo"].ToString() + "',";

                }
            }
            string sql = "  update   MCEOrderInfo set  ShipStatus='Shipped' where  not exists ( " +
                 "  select * from (select a.[ID],a.[OrderNo],a.[ProCatalogNo],a.[ProDescription],a.[ProSize], a.[ProUnit],case ProUnit when 'mg'  then isnull(c.shipsize,0)*1000 when 'g' then isnull(c.shipsize,0) when 'kg' then isnull(c.shipsize,0)*0.001  when 'μg' then isnull(c.shipsize,0)*1000000  else isnull(ShipSize,0) end [ShipSize],a.[ProQuantity],a.[ProAmount],a.[ProCurrency],a.[ProDunOn],a.[ProNote],a.[ProLibraryID] " +
      " ,a.[ProductStatus],a.[ProductProcess],a.[UpdateTime],a.[Person],a.[TaskTime] from MCEOrderProInfo  a   " +
       " left join (select OrginalID,isnull(sum(case shipunit when 'mg'  then ShipSize*0.001 when 'g' then ShipSize when 'kg' then ShipSize*1000  " +
       " when 'μg' then ShipSize*0.000001  else ShipSize end),0) shipsize from CSShipProInfo group by OrginalID)  c on a.ID=c.OrginalID  ) aa " +
      "  where ProSize>shipsize  and  MCEOrderInfo.orderno= aa.orderno " +
      "   )   and MCEOrderInfo.OrderProcess ='Finalize' and MCEOrderInfo.ShipStatus<>'Shipped' " + sqlwhere;



            if (DbHelperSQL.ExecuteSql(sql) > 0)
            {
                dt = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
                gridControl1.DataSource = dt;
                this.gridView1.BestFitColumns();
                LabRecords.Text = "Records:" + dt.Rows.Count.ToString();
                MessageDxUtil.ShowWarning("OK");
            }
        }

        private void gridView2_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                if (gridView2.GetRowCellValue(e.RowHandle, "StockStatus") != null)
                {
                    if (gridView2.GetRowCellValue(e.RowHandle, "StockStatus").ToString() != string.Empty)
                    {

                        e.Appearance.BackColor = Color.Aquamarine;

                    }

                }
            }

        }

        private void gridView5_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                if (gridView5.GetRowCellValue(e.RowHandle, "StockStatus") != null)
                {
                    if (gridView5.GetRowCellValue(e.RowHandle, "StockStatus").ToString() != string.Empty)
                    {

                        e.Appearance.BackColor = Color.Aquamarine;

                    }

                }
            }
        }


    }










}
