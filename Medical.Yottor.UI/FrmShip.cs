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
    public partial class FrmShip : DevExpress.XtraEditors.XtraForm
    {
        EuSoft.BLL.MCEShipmentInfo bMCEShipmentInfo = new EuSoft.BLL.MCEShipmentInfo();
        EuSoft.Model.MCEShipmentInfo mMCEShipmentInfo = new EuSoft.Model.MCEShipmentInfo();
        EuSoft.BLL.MCECarrierDefinition bMcu = new EuSoft.BLL.MCECarrierDefinition();
        EuSoft.BLL.MCEOrderInfo bMCEOrderInfo = new EuSoft.BLL.MCEOrderInfo();
        EuSoft.Model.MCEOrderInfo mMCEOrderInfo = new EuSoft.Model.MCEOrderInfo();
        EuSoft.BLL.MCEShipmentInfoChangeRe bMCEShipmentInfoChangeRe = new EuSoft.BLL.MCEShipmentInfoChangeRe();
        EuSoft.Model.MCEShipmentInfoChangeRe mMCEShipmentInfoChangeRe = new EuSoft.Model.MCEShipmentInfoChangeRe();

        EuSoft.BLL.MCEOrderProInfo bMCEOrderProInfo = new EuSoft.BLL.MCEOrderProInfo();

        EuSoft.BLL.MCEShipProInfo bMCEShipProInfo = new EuSoft.BLL.MCEShipProInfo();
        EuSoft.Model.MCEShipProInfo mMCEShipProInfo = new EuSoft.Model.MCEShipProInfo();
        DataTable dt = new DataTable();
        public FrmShip()
        {
            InitializeComponent();
            getDate();
            butSearch_Click(null,null);
            cobShipVia.Text = "FedEx";
            //this.gridView2.UpdateCurrentRow();
            //gridControl2.DataSource = getEmpDataTable();
            //this.gridView2.PopulateColumns();

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

            DataTable dtCarrier = new DataTable();
            dtCarrier = bMcu.GetAllList().Tables[0];
            BindComboBoxEdit(cobShipVia, dtCarrier, 1);
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
            sqlStr.Append("select convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo,InvioceNo,SalesCompany,PONumber,ShipStatus,convert(varchar(20),ShipDate,101) as ShipDate,ShipVia,TrackingNo,MCEShipmentInfo.Note, MCEOrderInfo.id,MCEOrderInfo.StockStatus   from MCEOrderInfo   LEFT JOIN MCEShipmentInfo ON MCEShipmentInfo.OrderNo=MCEOrderInfo.OrderNo where ( OrderDate >  dateadd(day,-1,getdate()) or ShipStatus = 'Unshipped')  and [OrderProcess] = 'Finalize' ");


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

            dt = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dt.Rows.Count.ToString();
            //   LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();

        }

        private void butAll_Click(object sender, EventArgs e)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo,InvioceNo,SalesCompany,PONumber,ShipStatus,convert(varchar(20),ShipDate,101) as ShipDate,ShipVia,TrackingNo,MCEShipmentInfo.Note, MCEOrderInfo.id,MCEOrderInfo.StockStatus    from MCEOrderInfo   LEFT JOIN MCEShipmentInfo ON MCEShipmentInfo.OrderNo=MCEOrderInfo.OrderNo where 1=1  and [OrderProcess] = 'Finalize' ");


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

            dt = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
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
                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "id").ToString());
                string orderNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[1]).ToString();
                mMCEOrderInfo = bMCEOrderInfo.GetModel(id);

                textEdit19.EditValue = mMCEOrderInfo.SalesCompany;
                textEdit19.Text = mMCEOrderInfo.SalesCompany;
                txtSalesContactName.Text = mMCEOrderInfo.SalesContactName;
                txtSalesEmail.Text = mMCEOrderInfo.SaleseMail;
                txtSalesFax.Text = mMCEOrderInfo.SalesFax;
                txtSalesStreet.Text = mMCEOrderInfo.SalesStreet;
                txtSalesTel.Text = mMCEOrderInfo.SalesTel;
                txtSalesCity.Text = mMCEOrderInfo.SalesCity;
                cobSalesCountry.Text = mMCEOrderInfo.SalesCountry;
                cobSalesState.Text = mMCEOrderInfo.SalesState;
                txtSalesZip.Text = mMCEOrderInfo.SalesZip;




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
                // cobNoteMailPromotion.Text = mMCEOrderInfo.MailPromotion;
                BtuDel.Text = "Del";
                cobPayMethod.Text = mMCEOrderInfo.PayMethod;
                getShipDate(orderNo);
                //this.gridView2.PopulateColumns();


            }
        }


        private void getShipDate(string orderNo)
        {
            string sqlWhere = string.Format(" orderno='{0}' ", orderNo);
            cobShipDate.Text = DateTime.Now.ToShortDateString();
            //this.gridView2.UpdateCurrentRow();

           // gridControl2.DataSource = bMCEShipmentInfo.GetList(sqlWhere).Tables[0];
            gridControl2.DataSource = DbHelperSQL.Query("select * from  CSShipmentInfo where orderno='"+orderNo+"'").Tables[0];
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

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            btnUpdate_ItemClick(null, null);
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

        private void BtuAdd_Click(object sender, EventArgs e)
        {
            if (cobShipDate.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the date of shipment.");
                cobShipDate.Focus();
                return;
            }
            if (txtTrk.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the tracking#.");
                txtTrk.Focus();
                return;
            }
            if (txtShipCost.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please choose the carrier.");
                txtShipCost.Focus();
                return;
            }
            DataTable dt = bMCEShipmentInfo.GetList("TrackingNo='" + txtTrk.Text + "' and orderno='" + txtNoteOrderNo.Text + "' ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                MessageDxUtil.ShowWarning("Same tracking found in system,Please check!");
                return;
            }
            if (MessageDxUtil.ShowYesNoAndWarning("Add the shipment " + txtTrk.Text + " to " + txtNoteOrderNo.Text + " ? ") == DialogResult.Yes)
            {
                mMCEShipmentInfo.Cost = Convert.ToDecimal(txtShipCost.Text);
                mMCEShipmentInfo.ShipDate = Convert.ToDateTime(cobShipDate.Text);
                mMCEShipmentInfo.ShipVia = cobShipVia.Text;
                mMCEShipmentInfo.Note = txtShipNote.Text;
                mMCEShipmentInfo.Currency = cobShipC.Text;
                mMCEShipmentInfo.OrderNo = txtNoteOrderNo.Text;
                mMCEShipmentInfo.TrackingNo = txtTrk.Text;
                mMCEShipmentInfo.Person = Properties.Settings.Default.LastUser;
                mMCEShipmentInfo.ShipmentStatus = "Undelivered";
                mMCEShipmentInfo.UpdateTime = DateTime.Now;

                mMCEShipmentInfo.SendOrderCOADate = null;
                mMCEShipmentInfo.SendOrderReceiptDate = null;
                mMCEShipmentInfo.SendOrderShipDate = null;

                if (bMCEShipmentInfo.Add(mMCEShipmentInfo) > 0)
                {
                    getShipDate(txtNoteOrderNo.Text);
                    ClearTxt();
                    MessageDxUtil.ShowTips("success");
                }
            }

        }

        private void BtuDel_Click(object sender, EventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {

                if (gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "StockStatus").ToString() == "CA")
                {
                    MessageDxUtil.ShowWarning("Cannot change the Shipping record of CA!");
                    return;
                }

                if (BtuDel.Text == "Del")
                {
                    int id = Convert.ToInt32(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, this.gridView2.Columns[0]));
                    if (MessageDxUtil.ShowYesNoAndWarning("Delete the shipment " + txtTrk.Text + " from " + txtNoteOrderNo.Text + " ? ") == DialogResult.Yes)
                    {

                        string sql = string.Format("INSERT INTO [dbo].[MCEShipmentInfoChangeRe]([OrderNo],[ShipDate],[ShipVia],[TrackingNo],[Note],[Cost],[Currency],[ShipmentStatus],[UpdateTime],[Person],[ChangeNote],[ChangePerson],[ChangeTime])");
                        sql += string.Format("SELECT [OrderNo],[ShipDate],[ShipVia],[TrackingNo],[Note],[Cost],[Currency],[ShipmentStatus],UpdateTime,[Person],'{0}','{1}',GETDATE() FROM [dbo].[MCEShipmentInfo]", "ID " + id + " Deleted", Properties.Settings.Default.LastUser);

                        if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(sql.ToString()) > 0)
                        {

                        }

                        if (bMCEShipmentInfo.Delete(id))
                        {
                            getShipDate(txtNoteOrderNo.Text);
                            ClearTxt();
                            MessageDxUtil.ShowTips("success");
                        }
                    }
                }
                else
                {

                    if (MessageDxUtil.ShowYesNoAndWarning("Update the shipment " + txtTrk.Text + " from " + txtNoteOrderNo.Text + " ? ") == DialogResult.Yes)
                    {
                        string trkNo = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, this.gridView2.Columns[3]).ToString();
                        int id = Convert.ToInt32(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, this.gridView2.Columns[0]));

                        if (bMCEShipmentInfo.Update(mMCEShipmentInfo))
                        {
                            string sqlstr1 = string.Format("INSERT INTO [dbo].[MCEShipmentInfoChangeRe]([OrderNo],[ShipDate],[ShipVia],[TrackingNo],[Note],[Cost],[Currency],[ShipmentStatus],[UpdateTime],[Person],[ChangeNote],[ChangePerson],[ChangeTime])");
                            sqlstr1 += string.Format("SELECT [OrderNo],[ShipDate],[ShipVia],[TrackingNo],[Note],[Cost],[Currency],[ShipmentStatus],UpdateTime,[Person],'{0}','{1}',GETDATE() FROM [dbo].[MCEShipmentInfo]", "ID " + id + " Update", Properties.Settings.Default.LastUser);
                            string sqlstr2 = "update  MCEShipProInfo set trackingno='" + txtTrk.Text + "',UpdateTime=getdate(),Person='" + Properties.Settings.Default.LastUser + "' where  trackingno='" + trkNo + "'";
                            string sqlstr3 = "update  MCEShipmentInfo set ShipDate='" + cobShipDate.Text + "',trackingno='" + txtTrk.Text + "',ShipVia='" + cobShipVia.Text + "',Note='" + txtShipNote.Text + "',Cost=" + txtShipCost.Text + ",Currency='" + cobShipC.Text + "',ShipmentStatus='Undelivered',UpdateTime=getdate(),Person='" + Properties.Settings.Default.LastUser + "' where  id=" + id + "";


                            List<string> strList = new List<string>();
                            strList.Add(sqlstr1);
                            strList.Add(sqlstr2);
                            strList.Add(sqlstr3);
                            if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSqlTran(strList) > 0)
                            {
                                getShipDate(txtNoteOrderNo.Text);
                                ClearTxt();
                                MessageDxUtil.ShowTips("success");
                            }



                        }
                    }
                }


            }
        }

        private void ClearTxt()
        {
            txtShipCost.Text = "";
            txtShipNote.Text = "";
            txtTrk.Text = "";
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {
                int id = Convert.ToInt32(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, this.gridView2.Columns[0]));
                mMCEShipmentInfo = bMCEShipmentInfo.GetModel(id);
                txtShipCost.Text = mMCEShipmentInfo.Cost.ToString();
                cobShipDate.Text = mMCEShipmentInfo.ShipDate.ToString();
                cobShipVia.Text = mMCEShipmentInfo.ShipVia;
                txtShipNote.Text = mMCEShipmentInfo.Note;
                cobShipC.Text = mMCEShipmentInfo.Currency;
                txtTrk.Text = mMCEShipmentInfo.TrackingNo;

                string strSql = "select * from MCEShipProInfo where trackingno='" + txtTrk.Text + "'";


                DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
                if (dt.Rows.Count > 0)
                    BtuDel.Text = "Update";
                else
                    BtuDel.Text = "Del";


            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "id").ToString());
                if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipStatus") != null)
                {

                    if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipStatus").ToString() != string.Empty)
                    {



                        string ShipmentStatus = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipStatus").ToString() == "Shipped" ? "Unshipped" : "Shipped";
                        if (DbHelperSQL.ExecuteSql("update MCEOrderInfo set ShipStatus='" + ShipmentStatus + "' where id=" + id + "") > 0)
                        {
                            //  getDate();

                            DataRow[] selectedRows = dt.Select("id=" + id + "");
                            if (selectedRows != null && selectedRows.Length > 0)
                            {

                                selectedRows[0]["ShipStatus"] = ShipmentStatus;
                                dt.AcceptChanges();

                            }

                            MessageDxUtil.ShowTips("success");

                        }
                    }

                }



            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtNoteOrderNo.Text != string.Empty)
            {
                string reference = "";
                string rowCount;
                DataTable dt = bMCEOrderProInfo.GetList(" orderno='" + txtNoteOrderNo.Text + "' and stockstatus='' ").Tables[0];
                rowCount = dt.Rows.Count.ToString();
                if (dt.Rows.Count==0)
                {
                    if (MessageDxUtil.ShowYesNoAndTips("There is no NJ shipment for this order!,Continue?")== DialogResult.No)
                    {
                        return;
                    }
                }  
                if (dt.Rows.Count < 4 && dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        reference += dt.Rows[i]["ProCatalogNo"].ToString() + "/" + dt.Rows[i]["prosize"].ToString() + dt.Rows[i]["ProUnit"].ToString();
                    }

                }
                else
                    reference = dt.Rows.Count + " items";


                string sumInTotal = dt.Compute("sum(proamount)", "true").ToString();

                FrmFedex xFrm = new FrmFedex(txtNoteOrderNo.Text, rowCount, txtShipCompany.Text, txtShipContactName.Text, txtShipStreet.Text, txtShipCity.Text, cobShipState.Text, txtShipZip.Text, cobShipCountry.Text, txtShipTel.Text, txtShipEmail.Text, txtNoteInvoiceNo.Text, txtNotePONumber.Text, txtNoteAccountNo.Text, reference, Convert.ToDecimal(txtNoteTotal.Text) - Convert.ToDecimal(txtNoteSH.Text));
                if (xFrm.ShowDialog() == DialogResult.OK)
                {
                    txtTrk.Text = xFrm.traNo;
                    txtShipCost.Text = xFrm.Yf;
                }
            }

        }

        private void gridView2_RowStyle(object sender, RowStyleEventArgs e)
        {
            string status;
            if (e.RowHandle > -1)
            {
                status = gridView2.GetRowCellValue(e.RowHandle, "StockStatus").ToString();
                if (status != "")
                {
                    e.Appearance.BackColor = Color.Aquamarine;
                }

            }
        }










    }
}