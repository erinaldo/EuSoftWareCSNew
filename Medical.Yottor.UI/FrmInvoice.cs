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

using System.IO;

using EuSoft.Common;


namespace Medical.Yottor.UI
{
    public partial class FrmInvoice : DevExpress.XtraEditors.XtraForm
    {
        EuSoft.BLL.MCEShipmentInfo bMCEShipmentInfo = new EuSoft.BLL.MCEShipmentInfo();
        EuSoft.Model.MCEShipmentInfo mMCEShipmentInfo = new EuSoft.Model.MCEShipmentInfo();
        EuSoft.BLL.MCECarrierDefinition bMcu = new EuSoft.BLL.MCECarrierDefinition();
        EuSoft.BLL.MCEOrderInfo bMCEOrderInfo = new EuSoft.BLL.MCEOrderInfo();
        EuSoft.Model.MCEOrderInfo mMCEOrderInfo = new EuSoft.Model.MCEOrderInfo();
        EuSoft.BLL.MCEShipmentInfoChangeRe bMCEShipmentInfoChangeRe = new EuSoft.BLL.MCEShipmentInfoChangeRe();
        EuSoft.Model.MCEShipmentInfoChangeRe mMCEShipmentInfoChangeRe = new EuSoft.Model.MCEShipmentInfoChangeRe();
        EuSoft.BLL.MCEPaymentInfo bMCEPaymentInfo = new EuSoft.BLL.MCEPaymentInfo();
        EuSoft.BLL.MCEOrderProInfo bMCEOrderProInfo = new EuSoft.BLL.MCEOrderProInfo();

        EuSoft.BLL.MCEShipProInfo bMCEShipProInfo = new EuSoft.BLL.MCEShipProInfo();
        EuSoft.Model.MCEShipProInfo mMCEShipProInfo = new EuSoft.Model.MCEShipProInfo();
        DataTable dtOrderInfor = new DataTable();
        bool Shipments = false;
        public FrmInvoice()
        {
            InitializeComponent();
            butSearch_Click(null, null);
            // getDate();
            //this.gridView2.UpdateCurrentRow();
            //gridControl2.DataSource = getEmpDataTable();
            //this.gridView2.PopulateColumns();

        }

        public void getDate()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select top 30 convert(varchar(20),a.OrderDate,101) as OrderDate,a.OrderNo,a.InvioceNo, a.SalesCompany ,a.SalesContactName,a.SalesCountry,a.PONumber,a.Comments,a.Note,a.InvoiceDate,a.PayStatus,a.Terms,a.OrderStatus,a.ShipStatus,convert(varchar(20),b.ShipDate,101) as ShipDate,a.ID  from MCEOrderInfo a  LEFT JOIN (select OrderNo, note,shipdate ,ShipVia,TrackingNo ");
            sqlStr.Append("  from (select row_number()over(partition by OrderNo order by shipdate desc)rn, * from MCEShipmentInfo)t where t.rn=1  ) b ON b.OrderNo=a.OrderNo  where (a.OrderDate >  DateAdd(day,-1,getdate()) or a.InvoiceDate is null)  and a.OrderStatus<>'Cancel'");


            sqlStr.Append(" order by a.orderno desc");



            dtOrderInfor = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtOrderInfor;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtOrderInfor.Rows.Count.ToString();
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
            splitContainerControl5.SplitterPosition = splitContainerControl4.Width / 2;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select convert(varchar(20),a.OrderDate,101) as OrderDate,a.OrderNo,a.InvioceNo, a.SalesCompany ,a.BillCompany,a.SalesContactName,a.SalesCountry,a.PONumber,a.Comments,a.Note,a.InvoiceDate,a.PayStatus,a.Terms,a.OrderStatus,a.ShipStatus,convert(varchar(20),b.ShipDate,101) as ShipDate,a.ID,a.StockStatus,a.BillCompany  from MCEOrderInfo a  LEFT JOIN (select OrderNo, note,shipdate ,ShipVia,TrackingNo ");
            sqlStr.Append("  from (select row_number()over(partition by OrderNo order by shipdate desc)rn, * from MCEShipmentInfo)t where t.rn=1  ) b ON b.OrderNo=a.OrderNo  where (a.OrderDate >  DateAdd(day,-1,getdate()) or a.InvoiceDate is null)  and a.OrderStatus<>'Cancel' and a.[OrderProcess] = 'Finalize' ");


            if (txtOrderNo.Text != "")
            {
                sqlStr.AppendFormat(" and a.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtContactName.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and ( a.BillContactName  like '%{0}%' or a.ShipContactName  like '%{0}%' or a.SalesContactName  like '%{0}%' or [BilleMail] like '%{0}%' ) ", txtContactName.Text);
            }
            if (txtCompany.Text != "")
            {
                sqlStr.AppendFormat(" and a.[SalesCompany] like '%{0}%'  ", txtCompany.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr.AppendFormat(" and  PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr.AppendFormat(" and a.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }



            if (txtDate1.Text != "")
            {
                sqlStr.AppendFormat(" and  a.OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlStr.AppendFormat(" and  a.OrderDate<='{0}'", txtDate2.Text);
            }
            sqlStr.Append(" order by a.orderno desc");

            dtOrderInfor = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtOrderInfor;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtOrderInfor.Rows.Count.ToString();
            //   LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();

        }

        private void butAll_Click(object sender, EventArgs e)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select convert(varchar(20),a.OrderDate,101) as OrderDate,a.OrderNo,a.InvioceNo, a.SalesCompany ,a.SalesContactName,a.SalesCountry,a.PONumber,a.Comments,a.Note,a.InvoiceDate,a.PayStatus,a.Terms,a.OrderStatus,a.ShipStatus,convert(varchar(20),b.ShipDate,101) as ShipDate,a.ID,a.StockStatus ,a.BillCompany from MCEOrderInfo a  LEFT JOIN (select OrderNo, note,shipdate ,ShipVia,TrackingNo ");
            sqlStr.Append("  from (select row_number()over(partition by OrderNo order by shipdate desc)rn, * from MCEShipmentInfo)t where t.rn=1  ) b ON b.OrderNo=a.OrderNo  where 1=1 and a.[OrderProcess] = 'Finalize' ");


            if (txtOrderNo.Text != "")
            {
                sqlStr.AppendFormat(" and a.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtContactName.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and ( a.BillContactName  like '%{0}%' or a.ShipContactName  like '%{0}%' or a.SalesContactName  like '%{0}%' or [BilleMail] like '%{0}%' ) ", txtContactName.Text);
            }
            if (txtCompany.Text != "")
            {
                sqlStr.AppendFormat(" and a.[SalesCompany] like '%{0}%'  ", txtCompany.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr.AppendFormat(" and  PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr.AppendFormat(" and a.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }



            if (txtDate1.Text != "")
            {
                sqlStr.AppendFormat(" and  a.OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlStr.AppendFormat(" and  a.OrderDate<='{0}'", txtDate2.Text);
            }
            sqlStr.Append(" order by a.orderno desc");

            dtOrderInfor = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtOrderInfor;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtOrderInfor.Rows.Count.ToString();
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            txtDate1.Text = "";
            txtDate2.Text = "";
            txtInvoiceNo.Text = "";

            txtPONo.Text = "";
            txtOrderNo.Text = "";
            txtCompany.Text = "";
            txtContactName.Text = "";

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ID").ToString());
                string orderNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "OrderNo").ToString();
                mMCEOrderInfo = bMCEOrderInfo.GetModel(id);

                //  txtSalesCompany.EditValue = mMCEOrderInfo.SalesCompany;

                txtAtta.Text = "";


                txtBillCompany.Text = mMCEOrderInfo.BillCompany;
                txtBillContactname.EditValue = mMCEOrderInfo.BillContactName;
                txtBillContactname.Text = mMCEOrderInfo.BillContactName;
                txtBillEmail.Text = mMCEOrderInfo.BilleMail;
                txtBillFax.Text = mMCEOrderInfo.BillFax;
                txtBillStreet.Text = mMCEOrderInfo.BillStreet;
                txtBillTel.Text = mMCEOrderInfo.BillTel;
                cobBillCountry.Text = mMCEOrderInfo.BillCountry;
                cobBillState.Text = mMCEOrderInfo.BillState;
                txtBillZip.Text = mMCEOrderInfo.BillZip;
                txtBillCity.Text = mMCEOrderInfo.BillCity;



                txtShipCompany.Text = mMCEOrderInfo.ShipCompany;
                txtShipContactName.EditValue = mMCEOrderInfo.ShipContactName;
                txtShipContactName.Text = mMCEOrderInfo.ShipContactName;
                txtShipEmail.Text = mMCEOrderInfo.ShipeMail;
                txtShipFax.Text = mMCEOrderInfo.ShipFax;
                txtShipStreet.Text = mMCEOrderInfo.ShipStreet;
                txtShipTel.Text = mMCEOrderInfo.ShipTel;
                txtShipCity.Text = mMCEOrderInfo.ShipCity;
                cobShipCountry.Text = mMCEOrderInfo.ShipCountry;
                cobShipState.Text = mMCEOrderInfo.ShipState;
                txtShipZip.Text = mMCEOrderInfo.ShipZip;

                txtNote.Text = mMCEOrderInfo.Note;
                txtNoteAccountNo.Text = mMCEOrderInfo.AccountNo;
                txtNoteTrems.Text = mMCEOrderInfo.Terms;
                txtNoteInvoiceNo.Text = mMCEOrderInfo.InvioceNo;
                txtNoteCuNo.Text = mMCEOrderInfo.CustomerRefNo;
                txtNoteOrderNo.Text = mMCEOrderInfo.OrderNo;
                cobNoteOrderDate.Text = string.Format("{0:d}", mMCEOrderInfo.OrderDate);
                txtNotePONumber.Text = mMCEOrderInfo.PONumber;
                txtNoteTotal.Text = mMCEOrderInfo.InvoiceTotal.ToString();
                txtNoteSH.Text = mMCEOrderInfo.SH.ToString();
                cobNoteCarrier.Text = mMCEOrderInfo.Carrier.ToString();
                txtNoteVendorCode.Text = mMCEOrderInfo.VendorCode;
                cobNoteCarrier.Text = mMCEOrderInfo.Carrier;
                txtNoteC.Text = mMCEOrderInfo.Comments;
                txtNote.Text = mMCEOrderInfo.Note;
                txtInvoiceDate.Text = string.Format("{0:d}", mMCEOrderInfo.InvoiceDate);
                txtInvoiceFax.Text = "1" + mMCEOrderInfo.BillFax + "@myfax.com";
                // cobNoteMailPromotion.Text = mMCEOrderInfo.MailPromotion;

                cobPayMethod.Text = mMCEOrderInfo.PayMethod;
                getShipDate(orderNo);
                //this.gridView2.PopulateColumns();


            }
        }


        private void getShipDate(string orderNo)
        {
            Shipments = false;
            string sqlWhere = string.Format(" orderno='{0}' ", orderNo);
            sqlWhere = sqlWhere + " order by id desc";
            //  cobShipDate.Text = DateTime.Now.ToShortDateString();
            //this.gridView2.UpdateCurrentRow();
            //  gridControl2.DataSource = DbHelperSQL.Query("select * from  CSShipmentInfo where orderno='"+orderNo+"'").Tables[0];
            //    DataTable dtMCEShipmentInfo = bMCEShipmentInfo.GetList(sqlWhere).Tables[0];
            DataTable dtMCEShipmentInfo = DbHelperSQL.Query("select * from  CSShipmentInfo where orderno='" + orderNo + "'").Tables[0];
            gridControl3.DataSource = dtMCEShipmentInfo;
            this.gridView5.BestFitColumns();
            if (dtMCEShipmentInfo.Rows.Count > 0)
                Shipments = true;
            gridControl2.DataSource = bMCEOrderProInfo.GetList(sqlWhere).Tables[0];
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

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FrmEditOrder xFrm = new FrmEditOrder("", txtNoteOrderNo.Text);

            if (xFrm.ShowDialog() == DialogResult.OK)
            {
                this.gridControl1_Click(null, null);
            }
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {
                string id = Convert.ToString(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, this.gridView2.Columns[0]));
                FrmEditOrder xFrm = new FrmEditOrder(id, txtNoteOrderNo.Text);

                if (xFrm.ShowDialog() == DialogResult.OK)
                {
                    this.gridControl1_Click(null, null);
                }
            }
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

            //if (e.RowHandle > -1)
            //{
            //    if (gridView1.GetRowCellValue(e.RowHandle, "InvoiceDate").ToString() != "")
            //    {
            //        e.Appearance.BackColor = Color.Lime;
            //    }

            //    if (gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString() == "Cancel")
            //    {
            //         e.Appearance.BackColor = Color.LightSlateGray;
            //    }


            //}





        }
        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //第一行  
            if (e.RowHandle > -1)
            {
                if (gridView1.GetRowCellValue(e.RowHandle, "InvoiceDate").ToString() != "")
                {
                    if (e.Column.ColumnHandle > 0)
                    {
                        e.Appearance.BackColor = Color.Lime;
                    }
                }

                if (gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString() == "Cancel")
                {
                    if (e.Column.ColumnHandle > 0)
                    {
                        e.Appearance.BackColor = Color.LightSlateGray;
                    }

                }
                string status = gridView1.GetRowCellValue(e.RowHandle, "StockStatus").ToString();
                if (status!="")
                {
                    if (e.Column.ColumnHandle == 1)
                    {
                        e.Appearance.BackColor = Color.Aquamarine;
                    }
                }
               

                if (gridView1.GetRowCellValue(e.RowHandle, "ShipDate").ToString() != "")
                {
                    if (gridView1.GetRowCellValue(e.RowHandle, "PayStatus").ToString() == "Unpaid" && gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString() != "Cancel" && gridView1.GetRowCellValue(e.RowHandle, "ShipStatus").ToString() == "Shipped")
                    {
                        if (DateTime.Now > Convert.ToDateTime(gridView1.GetRowCellValue(e.RowHandle, "ShipDate").ToString()).AddDays(Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "Terms"))))
                        {
                            if (e.Column.ColumnHandle == 0)
                            {
                                e.Appearance.BackColor = Color.Red;
                                e.Appearance.BackColor2 = Color.Brown;
                            }
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









        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "InvoiceDate").ToString() != "")
                {
                    int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ID"));

                    if (DbHelperSQL.ExecuteSql("update MCEOrderInfo set InvoiceDate='' where id=" + id + "") > 0)
                    {
                        DataRow[] selectedRows = dtOrderInfor.Select("ID=" + id + "");
                        if (selectedRows != null && selectedRows.Length > 0)
                        {

                            selectedRows[0]["InvoiceDate"] = DBNull.Value;
                            dtOrderInfor.AcceptChanges();

                        }
                        MessageDxUtil.ShowTips("success");

                    }

                }
                else
                {
                    MessageDxUtil.ShowError("Error, the invoice data does not exist!");
                }



            }
        }



        private void butOverdue_Click(object sender, EventArgs e)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select  convert(varchar(20),a.OrderDate,101) as OrderDate,a.OrderNo,a.InvioceNo, a.SalesCompany ,a.SalesContactName,a.SalesCountry,a.PONumber,a.Comments,a.Note,");
            sqlStr.Append(" a.InvoiceDate,a.PayStatus,a.Terms,a.OrderStatus,a.ShipStatus,convert(varchar(20),b.ShipDate,101) as ShipDate,a.ID,a.StockStatus,a.BillCompany   from MCEOrderInfo a ");
            sqlStr.Append("  LEFT JOIN (select OrderNo, note,shipdate ,ShipVia,TrackingNo   from (select row_number()over(partition by OrderNo order by shipdate desc)rn, ");
            sqlStr.Append(" * from MCEShipmentInfo)t where t.rn=1  ) b ON b.OrderNo=a.OrderNo  where  a.OrderStatus<>'Cancel'  and a.[OrderProcess] = 'Finalize' and PayStatus='Unpaid' and ShipDate is not null and  ShipStatus='Shipped' and getdate()>DateAdd(D,cast(Terms as int), cast(ShipDate as datetime))  ");


            if (txtOrderNo.Text != "")
            {
                sqlStr.AppendFormat(" and a.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtContactName.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and ( a.BillContactName  like '%{0}%' or a.ShipContactName  like '%{0}%' or a.SalesContactName  like '%{0}%' or [BilleMail] like '%{0}%' ) ", txtContactName.Text);
            }
            if (txtCompany.Text != "")
            {
                sqlStr.AppendFormat(" and a.[SalesCompany] like '%{0}%'  ", txtCompany.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr.AppendFormat(" and  PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr.AppendFormat(" and a.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }



            if (txtDate1.Text != "")
            {
                sqlStr.AppendFormat(" and  a.OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlStr.AppendFormat(" and  a.OrderDate<='{0}'", txtDate2.Text);
            }
            sqlStr.Append(" order by a.orderno desc");

            dtOrderInfor = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtOrderInfor;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtOrderInfor.Rows.Count.ToString();
        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "OrderStatus").ToString() == "Cancel")
            {
                MessageDxUtil.ShowWarning("Invalid Operation! The order has been revoked.");
                return;
            }
            if (txtInvoiceDate.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please fill the invoice date.");
                return;
            }
            int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ID"));

            if (DbHelperSQL.ExecuteSql("update MCEOrderInfo set InvoiceDate=getdate() where id=" + id + " and  isnull(InvoiceDate,'')='' ") > 0)
            {
                var selectedRows = dtOrderInfor.Select("ID=" + id + string.Empty);
                if (selectedRows != null && selectedRows.Length > 0)
                {
                    selectedRows[0]["InvoiceDate"] = txtInvoiceDate.Text;
                    dtOrderInfor.AcceptChanges();
                    txtInvoiceDate.Text = DateTime.Now.ToString();
                }
                MessageDxUtil.ShowTips("success");
            }

            //if (DbHelperSQL.ExecuteSql("update MCEOrderInfo set InvoiceDate='" + txtInvoiceDate.Text + "' where id=" + id + "") > 0)
            //{
            //    // getShipDate(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "OrderNo").ToString());
            //    //this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);
            //    //  this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowStyle);



            //    DataRow[] selectedRows = dtOrderInfor.Select("ID=" + id + "");
            //    if (selectedRows != null && selectedRows.Length > 0)
            //    {

            //        selectedRows[0]["InvoiceDate"] = txtInvoiceDate.Text;
            //        dtOrderInfor.AcceptChanges();

            //    }
            //    MessageDxUtil.ShowTips("success");

            //}

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (txtInvoiceDate.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please fill the invoice date.");
                return;
            }
            string sql = "select ISNULL(TrackingNo,'') as TrackingNo from  MCEShipmentInfo  where orderno='" + txtNoteOrderNo.Text + "'";

            DataTable dbShip = DbHelperSQL.Query(sql).Tables[0];
            if (dbShip.Rows.Count <= 0)
            {
                if (MessageDxUtil.ShowYesNoAndWarning("this is Orders " + txtNoteOrderNo.Text + "   not exists TrackingNo.Continue?") != DialogResult.Yes)
                {
                    return;
                }
            }
            if (!Shipments)
            {
                if (MessageDxUtil.ShowYesNoAndWarning("this is Orders " + txtNoteOrderNo.Text + "   not exists Shipments.Continue?") != DialogResult.Yes)
                {
                    return;
                }
            }

            this.Cursor = Cursors.WaitCursor;
            FrmCellPropertiesViewer xFrmV = new FrmCellPropertiesViewer(Application.StartupPath + @"\InvoiceTemplate.xlsx", txtNoteOrderNo.Text, "", 0, txtNoteInvoiceNo.Text);
            this.Cursor = Cursors.Default;
            if (xFrmV.ShowDialog() == DialogResult.OK)
            {
                txtAtta.Text = xFrmV.savePath;
            }
        }



        private void SaveFile(int Year, int Month, string departName)
        {
            string tempPath = "";

            tempPath = Application.StartupPath + @"\InvoiceTemplateT.xlsx";

            if (tempPath.Length > 0)
            {
                int ID = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ID").ToString());

                DataTable dt = bMCEOrderInfo.GetList(" Id=" + ID + "").Tables[0];

                DataTable dtShip = this.gridControl3.DataSource as DataTable;

                DataTable dtPr = this.gridControl2.DataSource as DataTable;
                DataTable dtPay = bMCEPaymentInfo.GetList(" invioceno='" + txtNoteInvoiceNo.Text + "' order by id desc ").Tables[0];




                if (dt != null && dt.Rows.Count > 0)
                {
                    string bakPath = Application.StartupPath + @"\aaaa.xlsx";
                    // string Title = string.Format("{0}{1}年{2}月份主要经营情况快报表", departName, Year, Month);
                    ExcelHelper.ExportExcelForDtByNPOI(dt, dtShip, dtPr, dtPay, bakPath, tempPath);
                    MessageDxUtil.ShowTips("生成本月报表成功！");
                }
                else
                {

                    return;
                }
            }
        }

        private void butExcel_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.FileName = "Order Invoice.xlsx";
            fileDialog.Title = "Excel";
            fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";
            var dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                gridControl1.ExportToXlsx(fileDialog.FileName);
                if (MessageDxUtil.ShowYesNoAndTips("success,open？") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(fileDialog.FileName);
                }
            }
        }

        private void butPdf_Click(object sender, EventArgs e)
        {
            if (txtInvoiceDate.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please fill the invoice date.");
                return;
            }
            string sql = "select ISNULL(TrackingNo,'') as TrackingNo from  MCEShipmentInfo  where orderno='" + txtNoteOrderNo.Text + "'";

            DataTable dbShip = DbHelperSQL.Query(sql).Tables[0];
            if (dbShip.Rows.Count <= 0)
            {
                if (MessageDxUtil.ShowYesNoAndWarning("this is Orders " + txtNoteOrderNo.Text + "   not exists TrackingNo.Continue?") != DialogResult.Yes)
                {
                    return;
                }
            }
            if (!Shipments)
            {
                if (MessageDxUtil.ShowYesNoAndWarning("this is Orders " + txtNoteOrderNo.Text + "   not exists Shipments.Continue?") != DialogResult.Yes)
                {
                    return;
                }
            }

            this.Cursor = Cursors.WaitCursor;
            FrmCellPropertiesViewer xFrmV = new FrmCellPropertiesViewer(Application.StartupPath + @"\InvoiceTemplate.xlsx", txtNoteOrderNo.Text, "", 0, txtNoteInvoiceNo.Text);
            this.Cursor = Cursors.Default;
            if (xFrmV.ShowDialog() == DialogResult.OK)
            {
                txtAtta.Text = xFrmV.savePath;
            }
        }

        ///<summary>
        /// 把Excel文件转换成PDF格式文件
        ///</summary>
        ///<param name="sourcePath">源文件路径</param>
        ///<param name="targetPath">目标文件路径</param> 
        ///<returns>true=转换成功</returns>
        //private static bool XLSConvertToPDF(string sourcePath, string targetPath)
        //{
        //    bool result = false;
        //    Microsoft.Office.Interop.Excel.XlFixedFormatType targetType = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF;
        //    object missing = Type.Missing;
        //    Microsoft.Office.Interop.Excel.Application application = null;
        //    Microsoft.Office.Interop.Excel.Workbook workBook = null;
        //    try
        //    {
        //        application = new Microsoft.Office.Interop.Excel.Application();
        //        object target = targetPath;
        //        object type = targetType;
        //        workBook = application.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing,
        //        missing, missing, missing, missing, missing, missing, missing, missing, missing);

        //        workBook.ExportAsFixedFormat(targetType, target, Microsoft.Office.Interop.Excel.XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);
        //        result = true;
        //    }
        //    catch
        //    {
        //        result = false;
        //    }
        //    finally
        //    {
        //        if (workBook != null)
        //        {
        //            workBook.Close(true, missing, missing);
        //            workBook = null;
        //        }
        //        if (application != null)
        //        {
        //            application.Quit();
        //            application = null;
        //        }
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //    return result;
        //}

        private void butEmial_Click(object sender, EventArgs e)
        {

            //原模板内容
            string html = GenerateHtmlHelper.ReadHtml(Application.StartupPath + @"\CSinvoice.html");




            //需要替换的内容


            string imgPath = Application.StartupPath + @"\usa_email_logo.png";
            html = html.Replace("{#CName}", txtBillContactname.Text);


            //写入操作
            string savepath = Application.StartupPath + @"\CSinvoiceT.html";
            bool success = GenerateHtmlHelper.WriteHtml(html, savepath);
            if (success)
            {
                string content = GenerateHtmlHelper.ReadHtml(savepath);
                //List<string> mailAddress = new List<string>();
                //mailAddress.Add(txtBillEmail.Text);
                List<string> mailAddress = new List<string>(txtBillEmail.Text.Split(';'));
                bool bOk;
                string Subject = "Invoice from ChemScene (PO#" + txtNotePONumber.Text + ", INV#" + txtNoteInvoiceNo.Text + ")";

                if (txtNotePONumber.Text.Length == 0)
                    Subject = "Invoice from ChemScene (INV#" + txtNoteInvoiceNo.Text + ")";
                string sfile = @txtAtta.Text;
                string test = EuSoft.Common.MailSender.sendMail(Subject, content, "CS Customer Service <customer_service@medchemexpress.com>", mailAddress, "smtp.gmail.com", 587, "customer_service@chemscene.com", "haoyuan20160601", true, "system_letter@chemscene.com", imgPath, sfile, out bOk);
                if (bOk)
                {
                    MessageDxUtil.ShowTips("success");
                }
                else
                {
                    MessageDxUtil.ShowTips("error");
                }
            }
        }

        private void butFax_Click(object sender, EventArgs e)
        {
            //原模板内容
            string html = GenerateHtmlHelper.ReadHtml(Application.StartupPath + @"\CSinvoice.html");




            //需要替换的内容


            string imgPath = Application.StartupPath + @"\usa_email_logo.png";
            html = html.Replace("{#CName}", txtBillContactname.Text);


            //写入操作
            string savepath = Application.StartupPath + @"\CSinvoiceT.html";
            bool success = GenerateHtmlHelper.WriteHtml(html, savepath);
            if (success)
            {
                string content = GenerateHtmlHelper.ReadHtml(savepath);
                //List<string> mailAddress = new List<string>();
                //mailAddress.Add(txtInvoiceFax.Text);
                List<string> mailAddress = new List<string>(txtBillEmail.Text.Split(';'));
                bool bOk;
                string Subject = "Invoice from ChemScene (PO#" + txtNotePONumber.Text + ", INV#" + txtNoteInvoiceNo.Text + ")";

                if (txtNotePONumber.Text.Length == 0)
                    Subject = "Invoice from ChemScene (INV#" + txtNoteInvoiceNo.Text + ")";
                string sfile = @txtAtta.Text;
                string test = EuSoft.Common.MailSender.sendMail(Subject, content, "CS Customer Service <customer_service@medchemexpress.com>", mailAddress, "smtp.gmail.com", 587, "customer_service@chemscene.com", "haoyuan20160601", true, "system_letter@chemscene.com", imgPath, sfile, out bOk);
                if (bOk)
                {
                    MessageDxUtil.ShowTips("success");
                }
                else
                {
                    MessageDxUtil.ShowTips("error");
                }
            }
        }
        private static string DateConvert(DateTime dTime)
        {
            string retTime = "";
            switch (dTime.Month)
            {
                case 1:
                    retTime = "Jan-" + dTime.ToString("dd-yyyy");
                    break;
                case 2:
                    retTime = "Feb-" + dTime.ToString("dd-yyyy");
                    break;
                case 3:
                    retTime = "Mar-" + dTime.ToString("dd-yyyy");
                    break;
                case 4:
                    retTime = "Apr-" + dTime.ToString("dd-yyyy");
                    break;
                case 5:
                    retTime = "May-" + dTime.ToString("dd-yyyy");
                    break;
                case 6:
                    retTime = "June-" + dTime.ToString("dd-yyyy");
                    break;
                case 7:
                    retTime = "July-" + dTime.ToString("dd-yyyy");
                    break;
                case 8:
                    retTime = "Aug-" + dTime.ToString("dd-yyyy");
                    break;
                case 9:
                    retTime = "Sept-" + dTime.ToString("dd-yyyy");
                    break;
                case 10:
                    retTime = "Oct-" + dTime.ToString("dd-yyyy");
                    break;
                case 11:
                    retTime = "Nov-" + dTime.ToString("dd-yyyy");
                    break;
                case 12:
                    retTime = "Dec-" + dTime.ToString("dd-yyyy");
                    break;

            }
            return retTime;
        }
        private void butOverDueE_Click(object sender, EventArgs e)
        {
            //原模板内容
            string html = GenerateHtmlHelper.ReadHtml(Application.StartupPath + @"\CSOverDueInvoice.html");




            //需要替换的内容


            string imgPath = Application.StartupPath + @"\usa_email_logo.png";
            html = html.Replace("{#CName}", txtBillContactname.Text);
            html = html.Replace("{#PoNo}", txtNotePONumber.Text);
            html = html.Replace("{#Invoice}", txtNoteInvoiceNo.Text);
            html = html.Replace("{#Je}", Convert.ToDouble(txtNoteTotal.Text).ToString("0.00"));
            html = html.Replace("{#Date}", DateConvert(Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipDate").ToString()).AddDays(Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Terms")))));
            html = html.Replace("{#Zje}", Convert.ToDouble(txtNoteTotal.Text).ToString("0.00"));


            //写入操作
            string savepath = Application.StartupPath + @"\CSOverDueInvoiceT.html";
            bool success = GenerateHtmlHelper.WriteHtml(html, savepath);
            if (success)
            {
                string content = GenerateHtmlHelper.ReadHtml(savepath);
                //List<string> mailAddress = new List<string>();
                //mailAddress.Add(txtBillEmail.Text);
                List<string> mailAddress = new List<string>(txtBillEmail.Text.Split(';'));
                bool bOk;
                string Subject = "Payment Reminder for your order from CS";
                string sfile = @txtAtta.Text;
                string test = EuSoft.Common.MailSender.sendMail(Subject, content, "CS Customer Service <customer_service@medchemexpress.com>", mailAddress, "smtp.gmail.com", 587, "customer_service@chemscene.com", "haoyuan20160601", true, "system_letter@chemscene.com", imgPath, sfile, out bOk);
                if (bOk)
                {
                    MessageDxUtil.ShowTips("success");
                }
                else
                {
                    MessageDxUtil.ShowTips("error");
                }
            }
        }

        private void txtAtta_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string filePath = WHC.OrderWater.Commons.FileDialogHelper.OpenPDF();
            if (!string.IsNullOrEmpty(filePath))
            {
                txtAtta.Text = filePath;
            }
        }

        private void txtInvoiceDate_Click(object sender, EventArgs e)
        {
            txtInvoiceDate.Text = DateTime.Now.ToShortDateString();
        }

        private void gridView5_RowStyle(object sender, RowStyleEventArgs e)
        {
            string status;
            if (e.RowHandle > -1)
            {
                status = gridView5.GetRowCellValue(e.RowHandle, "StockStatus").ToString();
                if (status != "")
                {
                    e.Appearance.BackColor = Color.Aquamarine;
                }

            }
        }







    }
}