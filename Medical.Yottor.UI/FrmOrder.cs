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
using Medical.Yottor.UI.ServiceReference1;

namespace Medical.Yottor.UI
{
    public partial class FrmOrder : DevExpress.XtraEditors.XtraForm
    {
        EuSoft.BLL.MCEOrderInfo bMCEOrderInfo = new EuSoft.BLL.MCEOrderInfo();
        EuSoft.Model.MCEOrderInfo mMCEOrderInfo = new EuSoft.Model.MCEOrderInfo();

        EuSoft.BLL.MCEOrderInfoChangeRe bMCEOrderInfoChangeRe = new EuSoft.BLL.MCEOrderInfoChangeRe();
        EuSoft.Model.MCEOrderInfoChangeRe mMCEOrderInfoChangeRe = new EuSoft.Model.MCEOrderInfoChangeRe();
        EuSoft.BLL.MCECustomersInfo bMcus = new EuSoft.BLL.MCECustomersInfo();
        EuSoft.Model.MCECustomersInfo mMcus = new EuSoft.Model.MCECustomersInfo();
        EuSoft.BLL.MCEEmailPromotion bMeep = new EuSoft.BLL.MCEEmailPromotion();
        EuSoft.BLL.MCECountryDefinition bMCountry = new EuSoft.BLL.MCECountryDefinition();
        EuSoft.BLL.MCEPayMethodDefinition bMPay = new EuSoft.BLL.MCEPayMethodDefinition();
        EuSoft.BLL.MCEStateDefinition bMsa = new EuSoft.BLL.MCEStateDefinition();
        EuSoft.BLL.MCECarrierDefinition bMcu = new EuSoft.BLL.MCECarrierDefinition();


        EuSoft.BLL.MCEOrderProInfo bMCEOrderProInfo = new EuSoft.BLL.MCEOrderProInfo();
        EuSoft.Model.MCEOrderProInfo mMCEOrderProInfo = new EuSoft.Model.MCEOrderProInfo();
        DataTable dtbMCEOrderInfo = new DataTable();
        private bool IsWeb = false;
          DataTable dtState = new DataTable();
          

        CARateRequest cag = new CARateRequest();
        CATaxRateAPIClient cat = new CATaxRateAPIClient();
        CARateResponseCollection car = new CARateResponseCollection();
        public FrmOrder()
        {
            InitializeComponent();
            butClear_Click(null, null);
            getDate();
            butSearch_Click(null, null);
            if (Properties.Settings.Default.SalesCompany != string.Empty)
            {
                txtSalesCompany.Text = Properties.Settings.Default.SalesCompany;
                txtSalesCompany_Leave(null, null);
            }
            groupControl10.Visible = false;
            groupControl9.Visible = false;
            groupControl8.Visible = false;
            cobNoteCarrier.Text = "FedEx";
            dataGridView1.Visible = false;
            txtTax.Text = "0";
            txtTaxation.Text = "0";
        }

        public void getDate()
        {
            DataTable dtCountry = new DataTable();
            dtCountry = bMCountry.GetAllList().Tables[0];
            BindComboBoxEdit(cobSalesCountry, dtCountry, 1);
            BindComboBoxEdit(cobBillCountry, dtCountry, 1);
            BindComboBoxEdit(cobShipCountry, dtCountry, 1);

            dtState = bMsa.GetAllList().Tables[0];
            //BindComboBoxEdit(cobShipState, dtState, 2);
            //BindComboBoxEdit(cobBillState, dtState, 2);
            //BindComboBoxEdit(cobSalesState, dtState, 2);

            DataTable dtPayMethod = new DataTable();
            dtPayMethod = bMPay.GetAllList().Tables[0];
            BindComboBoxEdit(cobPayMethod, dtPayMethod, 1);

            DataTable dtCarrier = new DataTable();
            dtCarrier = bMcu.GetAllList().Tables[0];
            BindComboBoxEdit(cobNoteCarrier, dtCarrier, 1);



            //DataTable dtMCECustomersInfo = new DataTable();
            //dtMCECustomersInfo = bMcus.GetAllList().Tables[0];

            //BindComboBoxEdit(txtSalesCompany, dtMCECustomersInfo, 1);
            //txtSalesCompany.Properties.DataSource = dtMCECustomersInfo;
            //// txtSalesCompany.Properties.DataSource = bMcus.GetModelList("1=1");
            //txtSalesCompany.Properties.DisplayMember = "SalesCompany";
            //txtSalesCompany.Properties.ValueMember = "SalesCompany";
            //if (Properties.Settings.Default.SalesCompany == "")
            //    txtSalesCompany.EditValue = dtMCECustomersInfo.Rows[0]["SalesCompany"].ToString();
            //else
            //    txtSalesCompany.EditValue = Properties.Settings.Default.SalesCompany;


            //    txtSalesCompany.Properties.ImmediatePopup = true; //显示下拉
            //    txtSalesCompany.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains; 
            //DataTable dtEmail = new DataTable();
            //dtEmail = bMeep.GetAllList().Tables[0];
            //BindComboBoxEdit(cobNoteMailPromotion, dtEmail, 1);

            //  cobNoteOrderDate.Text = DateTime.Now.Date.ToShortDateString();

            //DataTable dt = bMCEOrderInfo.GetList(30, "( OrderDate >   DateAdd(day,-1,getdate()) or [OrderProcess] = 'In progress') and [OrderStatus] = 'Ok'", "id desc").Tables[0];
            //gridControl1.DataSource = dt;
            //LabRecords.Text = "Records:" + dt.Rows.Count.ToString();
            //LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();
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
                cmb.SelectedIndex = 0;
        }
        private void FrmOrder_Load(object sender, EventArgs e)
        {
            //  GridViewCreateNewCellItem.CreateClearCellItem(gridView2);
            dxValidationProvider1.ValidationMode = ValidationMode.Manual;
            dxValidationProvider1.Validate();
            splitContainerControl5.SplitterPosition = splitContainerControl2.Width / 2;
            splitContainerControl6.SplitterPosition = splitContainerControl2.Width / 2;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {

            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.Append("  1=1 and ( OrderDate >   DateAdd(day,-1,getdate()) or [OrderProcess] = 'In progress') and [OrderStatus] = 'Ok' ");
            if (txtOrderNo.Text != "")
            {
                sqlWhere.AppendFormat(" and orderno like '%{0}%'  ", txtOrderNo.Text);
            }

            if (txtCompany.Text != "")
            {
                sqlWhere.AppendFormat(" and [SalesCompany] like '%{0}%'  ", txtCompany.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlWhere.AppendFormat(" and  PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlWhere.AppendFormat(" and InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }
            if (txtContactName.Text != "")
            {
                sqlWhere.AppendFormat("and ([BillContactName] like '%{0}%' or [ShipContactName] like '%{0}%' or [SalesContactName] like '%{0}%')", txtContactName.Text);
            }

            if (txtDate1.Text != "")
            {
                sqlWhere.AppendFormat(" and  OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlWhere.AppendFormat(" and  OrderDate<='{0}'", txtDate2.Text);
            }
            sqlWhere.Append(" order by orderno desc");

            dtbMCEOrderInfo = bMCEOrderInfo.GetList(sqlWhere.ToString()).Tables[0];
            gridControl1.DataSource = dtbMCEOrderInfo;
            LabRecords.Text = "Records:" + dtbMCEOrderInfo.Rows.Count.ToString();
            LabInvoiceTotal.Text = "                Invoice Total:" + dtbMCEOrderInfo.Compute("sum(InvoiceTotal)", "").ToString();

        }

        private void butAll_Click(object sender, EventArgs e)
        {
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.Append("  1=1  ");
            if (txtOrderNo.Text != "")
            {
                sqlWhere.AppendFormat(" and orderno like '%{0}%' ", txtOrderNo.Text);
            }

            if (txtCompany.Text != "")
            {
                sqlWhere.AppendFormat(" and [SalesCompany] like '%{0}%'  ", txtCompany.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlWhere.AppendFormat(" and  PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlWhere.AppendFormat(" and InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }
            if (txtContactName.Text != "")
            {
                sqlWhere.AppendFormat("and ([BillContactName] like '%{0}%' or [ShipContactName] like '%{0}%' or [SalesContactName] like '%{0}%')", txtContactName.Text);
            }

            if (txtDate1.Text != "")
            {
                sqlWhere.AppendFormat(" and  OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlWhere.AppendFormat(" and  OrderDate<='{0}'", txtDate2.Text);
            }

            dtbMCEOrderInfo = bMCEOrderInfo.GetList(sqlWhere.ToString()).Tables[0];
            gridControl1.DataSource = dtbMCEOrderInfo;

            LabRecords.Text = "Records:" + dtbMCEOrderInfo.Rows.Count.ToString();
            LabInvoiceTotal.Text = "                Invoice Total:" + dtbMCEOrderInfo.Compute("sum(InvoiceTotal)", "").ToString();
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            txtDate1.Text = "";
            txtDate2.Text = "";
            txtInvoiceNo.Text = "";
            txtContactName.Text = "";
            txtPONo.Text = "";
            txtOrderNo.Text = "";
            txtCompany.Text = "";
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                btuClear_Click(null, null);
                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]));
                string orderNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[2]).ToString();
                mMCEOrderInfo = bMCEOrderInfo.GetModel(id);
                txtId.Text = mMCEOrderInfo.CustomerID;
                txtSalesCompany.EditValue = mMCEOrderInfo.SalesCompany;
                txtSalesCompany.Text = mMCEOrderInfo.SalesCompany;
                txtSalesContactName.Text = mMCEOrderInfo.SalesContactName;
                txtSalesEmail.Text = mMCEOrderInfo.SaleseMail;
                txtSalesFax.Text = mMCEOrderInfo.SalesFax;
                txtSalesStreet.Text = mMCEOrderInfo.SalesStreet;
                txtSalesTel.Text = mMCEOrderInfo.SalesTel;
                txtSalesCity.Text = mMCEOrderInfo.SalesCity;
                cobSalesCountry.Text = mMCEOrderInfo.SalesCountry;
                cobSalesState.Text = mMCEOrderInfo.SalesState;
                txtSalesZip.Text = mMCEOrderInfo.SalesZip;

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
                txtStockStatus.Text = mMCEOrderInfo.StockStatus;
                // cobNoteMailPromotion.Text = mMCEOrderInfo.MailPromotion;
                cobPayMethod.Text = mMCEOrderInfo.PayMethod;
                txtTax.Text = mMCEOrderInfo.Tax.ToString();
                txtTaxation.Text = mMCEOrderInfo.Taxation.ToString();
                    DataRow[] selectedRows = dtbMCEOrderInfo.Select("id=" + id + "");
                    if (selectedRows != null && selectedRows.Length > 0)
                    {

                        selectedRows[0]["StockStatus"] = txtStockStatus.Text;
                        dtbMCEOrderInfo.AcceptChanges();

                    }
               

                string sqlWhere = string.Format(" orderno='{0}' ", orderNo);

                //this.gridView2.UpdateCurrentRow();
                gridControl2.DataSource = bMCEOrderProInfo.GetList(sqlWhere).Tables[0];
                //this.gridView2.PopulateColumns();


            }
        }


        public DataTable getEmpDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Catalog No."));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("Size"));
            dt.Columns.Add(new DataColumn("Unit"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("Currency"));
            dt.Columns.Add(new DataColumn("Dun on"));
            dt.Columns.Add(new DataColumn("Note"));
            dt.Columns.Add(new DataColumn("Library ID"));
            dt.Columns.Add(new DataColumn("ID"));
            for (int i = 0; i < 20; i++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            return dt;
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
            GridControl grid = sender as GridControl;
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && txtNoteOrderNo.Text != "")
            {
                GridHitInfo hitInfo = gridView1.CalcHitInfo(e.Location);
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);

                popupMenu1.ShowPopup(p);

            }
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

                if (MessageDxUtil.ShowYesNoAndWarning("Delete the product " + this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, this.gridView2.Columns[1]) + " from " + txtNoteOrderNo.Text + " ? ") == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, this.gridView2.Columns[0]));


                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("INSERT INTO [dbo].[MCEOrderProInfoChangeRe]([OrderNo],[ProCatalogNo],[ProDescription],[ProSize],[ProUnit],[ProQuantity],[ProAmount],[ProCurrency],");
                    sqlStr.Append(" [ProDunOn],[ProNote],[ProLibraryID],[ProductStatus],[ProductProcess],[UpdateTime],[Person],[ChangeNote],[ChangePerson],[ChangeTime])");
                    sqlStr.Append("select [OrderNo],[ProCatalogNo],[ProDescription],[ProSize],[ProUnit],[ProQuantity],[ProAmount],[ProCurrency],[ProDunOn],[ProNote],[ProLibraryID]");
                    sqlStr.AppendFormat(",[ProductStatus],[ProductProcess],[UpdateTime],'{0}','{1}','{2}',getdate() FROM [dbo].[MCEOrderProInfo]", Properties.Settings.Default.LastUser, "ID " + id + " Deleted", Properties.Settings.Default.LastUser);
                    sqlStr.AppendFormat("  where id={0}", id);
                    if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(sqlStr.ToString()) > 0)
                    {

                    }



                    if (bMCEOrderProInfo.Delete(id))
                    {
                        this.gridControl1_Click(null, null);
                    }
                }
            }
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridControl1_Click(null, null);
        }

        private void txtBillContactname_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim() != "")
            {
                string sql = "select distinct [BillContactName],[BillStreet],[BillCity],[BillState],[BillZip],[BillCountry],[BillTel],[BillFax],[BilleMail]  from [MCEOrderInfo] where [CustomerID] = " + txtId.Text + " order by [BilleMail]";
                DataTable dt = DbHelperSQL.Query(sql).Tables[0];
                gridControl4.DataSource = dt;
                gridView7.BestFitColumns();
                groupControl8.Visible = true;
            }
        }

        private void txtSalesCompany_Click(object sender, EventArgs e)
        {
            //txtId.Text = txtSalesCompany.EditValue.ToString();
            //if (txtId.Text.Trim() == "")
            //{
            //    return;
            //}
            //mMcus = bMcus.GetModel(Convert.ToInt32(txtId.Text));
            ////   txtSalesCompany.Text = mMcus.SalesCompany;
            //txtSalesContactName.Text = mMcus.SalesContactName;
            //txtSalesEmail.Text = mMcus.SaleseMail;
            //txtSalesFax.Text = mMcus.SalesFax;
            //txtSalesStreet.Text = mMcus.SalesStreet;
            //txtSalesTel.Text = mMcus.SalesTel;
            //txtSalesCity.Text = mMcus.SalesCity;
            //cobSalesCountry.Text = mMcus.SalesCountry;
            //cobSalesState.Text = mMcus.SalesState;
            //txtSalesZip.Text = mMcus.SalesZip;

            //txtBillCompany.Text = mMcus.BillCompany;
            //txtBillContactname.Text = mMcus.BillContactName;
            //txtBillEmail.Text = mMcus.BilleMail;
            //txtBillFax.Text = mMcus.BillFax;
            //txtBillStreet.Text = mMcus.BillStreet;
            //txtBillTel.Text = mMcus.BillTel;
            //cobBillCountry.Text = mMcus.BillCountry;
            //cobBillState.Text = mMcus.BillState;
            //txtBillZip.Text = mMcus.BillZip;
            //txtBillCity.Text = mMcus.BillCity;


            //txtShipCompany.Text = mMcus.ShipCompany;
            //txtShipContactName.Text = mMcus.ShipContactName;
            //txtShipEmail.Text = mMcus.ShipeMail;
            //txtShipFax.Text = mMcus.ShipFax;
            //txtShipStreet.Text = mMcus.ShipStreet;
            //txtShipTel.Text = mMcus.ShipTel;
            //txtShipCity.Text = mMcus.ShipCity;
            //cobShipCountry.Text = mMcus.ShipCountry;
            //cobShipState.Text = mMcus.ShipState;
            //txtShipZip.Text = mMcus.ShipZip;

            //txtNote.Text = mMcus.Note;
            //txtNoteAccountNo.Text = mMcus.AccountNo;
            //txtNoteTrems.Text = mMcus.Terms;

            //txtNoteVendorCode.Text = mMcus.VendorCode;
            //cobNoteCarrier.Text = mMcus.Carrier;

            //cobPayMethod.Text = mMcus.PayMethod;
        }



        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

        }





        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }



        private void btuAdd_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {

                if (txtNotePONumber.Text.Trim().Length > 0)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select count(1) from MCEOrderInfo");
                    strSql.Append(" where  PONumber=@PONumber");
                    SqlParameter[] parameters = {
				
                    new SqlParameter("@PONumber", SqlDbType.VarChar,50)
		              	};
                    parameters[0].Value = txtNotePONumber.Text;

                    if (Maticsoft.DBUtility.DbHelperSQL.Exists(strSql.ToString(), parameters))
                    {
                        if (MessageDxUtil.ShowYesNoAndWarning("The PO number does exist..Continue?") == DialogResult.No)
                        {
                            return;
                        }
                    }
                }

                string sql = @" select count(*) from(
select  convert(varchar(20),a.OrderDate,101) as OrderDate,a.OrderNo,a.InvioceNo, a.SalesCompany ,a.BillCompany,a.SalesContactName,a.SalesCountry,a.PONumber,a.Comments,a.Note,
          a.InvoiceDate,a.PayStatus,a.Terms,a.OrderStatus,a.ShipStatus,convert(varchar(20),b.ShipDate,101) as ShipDate,a.ID  from MCEOrderInfo a 
        LEFT JOIN (select OrderNo, note,shipdate ,ShipVia,TrackingNo   from (select row_number()over(partition by OrderNo order by shipdate desc)rn, 
     * from MCEShipmentInfo)t where t.rn=1  ) b ON b.OrderNo=a.OrderNo  where  a.OrderStatus<>'Cancel' and PayStatus='Unpaid' 
     and ShipDate is not null and  ShipStatus='Shipped' and getdate()>DateAdd(D,cast(Terms as int), cast(ShipDate as datetime)) and  a.[OrderProcess] = 'Finalize'
     )   aaaa where SalesCompany='" + txtSalesCompany.Text + "' ";

                DataTable dtUnPaid = Maticsoft.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dtUnPaid.Rows.Count > 0)
                {
                    if (Convert.ToInt16(dtUnPaid.Rows[0][0].ToString()) > 0)
                    {
                        if (MessageDxUtil.ShowYesNoAndWarning("The customer has " + dtUnPaid.Rows[0][0].ToString() + " overdue unpaid order..Continue?") == DialogResult.No)
                        {
                            return;
                        }
                    }
                }

                sql = @"select count(*) from CancelPayCustomersInfo  where SalesCompany='" + txtSalesCompany.Text + "' ";

                dtUnPaid = Maticsoft.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dtUnPaid.Rows.Count > 0)
                {
                    if (Convert.ToInt16(dtUnPaid.Rows[0][0].ToString()) > 0)
                    {
                        if (MessageDxUtil.ShowYesNoAndWarning("The customer  has joined the blacklist..Continue?") == DialogResult.No)
                        {
                            return;
                        }
                    }
                }




                StringBuilder sqlWhere = new StringBuilder();
                sqlWhere.AppendFormat(" SalesCompany='{0}'  ", txtSalesCompany.Text);
                DataTable dt = new DataTable();
                dt = bMcus.GetList(sqlWhere.ToString()).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    mMcus.SalesCompany = txtSalesCompany.Text;
                    mMcus.SalesContactName = txtSalesContactName.Text;
                    mMcus.SaleseMail = txtSalesEmail.Text;
                    mMcus.SalesFax = txtSalesFax.Text;
                    mMcus.SalesStreet = txtSalesStreet.Text;
                    mMcus.SalesTel = txtSalesTel.Text;
                    mMcus.SalesCity = txtSalesCity.Text;
                    mMcus.SalesCountry = cobSalesCountry.Text;
                    mMcus.SalesState = cobSalesState.Text;
                    mMcus.SalesZip = txtSalesZip.Text;

                    mMcus.BillCompany = txtBillCompany.Text;
                    mMcus.BillContactName = txtBillContactname.Text;
                    mMcus.BilleMail = txtBillEmail.Text;
                    mMcus.BillFax = txtBillFax.Text;
                    mMcus.BillStreet = txtBillStreet.Text;
                    mMcus.BillTel = txtBillTel.Text;
                    mMcus.BillCountry = cobBillCountry.Text;
                    mMcus.BillState = cobBillState.Text;
                    mMcus.BillZip = txtBillZip.Text;
                    mMcus.BillCity = txtBillCity.Text;


                    mMcus.ShipCompany = txtShipCompany.Text;
                    mMcus.ShipContactName = txtShipContactName.Text;
                    mMcus.ShipeMail = txtShipEmail.Text;
                    mMcus.ShipFax = txtShipFax.Text;
                    mMcus.ShipStreet = txtShipStreet.Text;
                    mMcus.ShipTel = txtShipTel.Text;
                    mMcus.ShipCity = txtShipCity.Text;
                    mMcus.ShipCountry = cobShipCountry.Text;
                    mMcus.ShipState = cobShipState.Text;
                    mMcus.ShipZip = txtShipZip.Text;

                    mMcus.Note = txtNote.Text;
                    mMcus.AccountNo = txtNoteAccountNo.Text;
                    mMcus.Terms = txtNoteTrems.Text;

                    mMcus.VendorCode = txtNoteVendorCode.Text;
                    mMcus.Carrier = cobNoteCarrier.Text;
                    mMcus.PayMethod = cobPayMethod.Text;
                    mMcus.Person = Properties.Settings.Default.LastUser;
                    mMcus.UpdateTime = DateTime.Now;
                    mMcus.Reasons = txtNoteM.Text;
                    if (bMcus.Add(mMcus) > 0)
                    {

                    }
                }
                mMCEOrderInfo.OrderNo = "1";
                mMCEOrderInfo.InvioceNo = "1";
                mMCEOrderInfo.SalesCompany = txtSalesCompany.EditValue.ToString();
                mMCEOrderInfo.SalesContactName = txtSalesContactName.Text;
                mMCEOrderInfo.SaleseMail = txtSalesEmail.Text;
                mMCEOrderInfo.SalesFax = txtSalesFax.Text;
                mMCEOrderInfo.SalesStreet = txtSalesStreet.Text;
                mMCEOrderInfo.SalesTel = txtSalesTel.Text;
                mMCEOrderInfo.SalesCity = txtSalesCity.Text;
                mMCEOrderInfo.SalesCountry = cobSalesCountry.Text;
                mMCEOrderInfo.SalesState = cobSalesState.Text;
                mMCEOrderInfo.SalesZip = txtSalesZip.Text;
                mMCEOrderInfo.CustomerID = txtId.Text;
                mMCEOrderInfo.BillCompany = txtBillCompany.Text;
                mMCEOrderInfo.BillContactName = txtBillContactname.EditValue.ToString();
                mMCEOrderInfo.BilleMail = txtBillEmail.Text;
                mMCEOrderInfo.BillFax = txtBillFax.Text;
                mMCEOrderInfo.BillStreet = txtBillStreet.Text;
                mMCEOrderInfo.BillTel = txtBillTel.Text;
                mMCEOrderInfo.BillCountry = cobBillCountry.Text;
                mMCEOrderInfo.BillState = cobBillState.Text;
                mMCEOrderInfo.BillZip = txtBillZip.Text;
                mMCEOrderInfo.BillCity = txtBillCity.Text;
                mMCEOrderInfo.SendConfirmDate = null;

                mMCEOrderInfo.ShipCompany = txtShipCompany.Text;
                mMCEOrderInfo.ShipContactName = txtShipContactName.EditValue.ToString();
                mMCEOrderInfo.ShipeMail = txtShipEmail.Text;
                mMCEOrderInfo.ShipFax = txtShipFax.Text;
                mMCEOrderInfo.ShipStreet = txtShipStreet.Text;
                mMCEOrderInfo.ShipTel = txtShipTel.Text;
                mMCEOrderInfo.ShipCity = txtShipCity.Text;
                mMCEOrderInfo.ShipCountry = cobShipCountry.Text;
                mMCEOrderInfo.ShipState = cobShipState.Text;
                mMCEOrderInfo.ShipZip = txtShipZip.Text;
                mMCEOrderInfo.StockStatus = txtStockStatus.Text;
                mMCEOrderInfo.Note = txtNote.Text;
                mMCEOrderInfo.AccountNo = txtNoteAccountNo.Text;
                mMCEOrderInfo.Terms = txtNoteTrems.Text;
                mMCEOrderInfo.InvioceNo = txtNoteInvoiceNo.Text;
                mMCEOrderInfo.CustomerRefNo = txtNoteCuNo.Text;
                mMCEOrderInfo.OrderNo = txtNoteOrderNo.Text;
                mMCEOrderInfo.OrderDate = Convert.ToDateTime(cobNoteOrderDate.Text);
                mMCEOrderInfo.PONumber = txtNotePONumber.Text;
                mMCEOrderInfo.InvoiceTotal = Convert.ToDecimal(txtNoteTotal.Text);
                mMCEOrderInfo.SH = txtNoteSH.Text == "" ? 0 : Convert.ToInt32(txtNoteSH.Text);
                mMCEOrderInfo.Carrier = cobNoteCarrier.Text;
                mMCEOrderInfo.VendorCode = txtNoteVendorCode.Text;
                mMCEOrderInfo.Carrier = cobNoteCarrier.Text;
                mMCEOrderInfo.Comments = txtNoteC.Text;
                mMCEOrderInfo.Note = txtNote.Text;
                //  mMCEOrderInfo.MailPromotion = cobNoteMailPromotion.Text;
                mMCEOrderInfo.PayMethod = cobPayMethod.Text;
                //  mMCEOrderInfo.OrderProcess = "OrderProcess";
                //  mMCEOrderInfo.UpdateTime = DateTime.Now;
                mMCEOrderInfo.Person = Properties.Settings.Default.LastUser;
                mMCEOrderInfo.Tax = Convert.ToDecimal(txtTax.Text == "" ? "0" : txtTax.Text);
                if (bMCEOrderInfo.AddProc_CreateDD(mMCEOrderInfo) > 0)
                {
                    butSearch_Click(null, null);
                    sqlWhere.Length = 0;
                    if (txtWebOrderNo.Text != "")
                    {
                        sqlWhere.AppendFormat(" Note like '%{0}%'  order by id desc ", txtWebOrderNo.Text);

                        DataTable dtzj = new DataTable();

                        dtzj = bMCEOrderInfo.GetList(sqlWhere.ToString()).Tables[0];
                        if (dtzj.Rows.Count > 0)
                        {
                            txtNoteOrderNo.Text = dtzj.Rows[0]["OrderNo"].ToString();
                            txtNoteInvoiceNo.Text = dtzj.Rows[0]["InvioceNo"].ToString();
                            sqlWhere.Clear();
                            sqlWhere.AppendFormat("update MCEOrderProInfo set orderno='{0}' where orderno='{1}' ", txtNoteOrderNo.Text, txtWebOrderNo.Text);
                            if (DbHelperSQL.ExecuteSql(sqlWhere.ToString()) > 0)
                            {
                                butSearch_Click(null, null);
                                sqlWhere.Clear();

                                sqlWhere.AppendFormat(" orderno='{0}' ", txtNoteOrderNo.Text);

                                gridControl2.DataSource = bMCEOrderProInfo.GetList(sqlWhere.ToString()).Tables[0];
                            }

                        }
                    }
                    txtWebOrderNo.Text = "";

                    MessageDxUtil.ShowTips("success");
                }
            }

        }



        /// <summary>
        /// 实现控件输入检查的函数
        /// </summary>
        /// <returns></returns>
        public bool CheckInput()
        {
            bool result = true;//默认是可以通过

            #region MyRegion

            if (this.txtSalesCompany.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the SalesCompany name.");
                this.txtSalesCompany.Focus();
                result = false;
            }
            else if (this.txtShipCompany.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the ShipCompany name.");
                this.txtShipCompany.Focus();
                result = false;
            }
            else if (this.txtNoteTotal.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the Total.");
                this.txtNoteTotal.Focus();
                result = false;
            }
            else if (this.txtBillCompany.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the BillCompany name.");
                this.txtBillCompany.Focus();
                result = false;
            }
            //else if (this.cobSalesState.Text.Trim().Length == 0)
            //{
            //    MessageDxUtil.ShowTips("Please fill the SalesState.");
            //    this.cobSalesState.Focus();
            //    result = false;
            //}
            else if (this.txtBillCompany.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the BillCompany.");
                this.txtBillCompany.Focus();
                result = false;
            }
            else if (this.cobShipCountry.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the ShipCountry.");
                this.cobShipCountry.Focus();
                result = false;
            }
            else if (this.txtShipStreet.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the ShipStreet.");
                this.txtShipStreet.Focus();
                result = false;
            }
            else if (this.cobNoteOrderDate.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the eOrderDate.");
                this.cobNoteOrderDate.Focus();
                result = false;
            }
            else if (this.txtShipContactName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the ShipContactName.");
                this.txtShipContactName.Focus();
                result = false;
            }
            //else if (this.txtShipTel.Text.Trim().Length == 0)
            //{
            //    MessageDxUtil.ShowTips("Please fill the txtShipTel.");
            //    this.txtShipTel.Focus();
            //    result = false;
            //}

            if (cobSalesCountry.Text != "")
            {
                if (!bMCountry.ExistscountryName(cobSalesCountry.Text))
                {
                    cobSalesCountry.Focus();
                    MessageDxUtil.ShowError("SalesCountry error!");
                    result = false;
                }
            }

            if (cobBillCountry.Text != "")
            {
                if (!bMCountry.ExistscountryName(cobBillCountry.Text))
                {
                    cobBillCountry.Focus();
                    MessageDxUtil.ShowError("BillCountry error!");
                    result = false;
                }
            }

            if (cobShipCountry.Text != "")
            {
                if (!bMCountry.ExistscountryName(cobShipCountry.Text))
                {
                    cobShipCountry.Focus();
                    MessageDxUtil.ShowError("ShipCountry error!");
                    result = false;
                }
            }

  

            if (cobSalesCountry.Text == "United States")
            {
                if (cobSalesState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobSalesState.Text))
                    {
                        cobSalesState.Focus();
                        MessageDxUtil.ShowError("SalesState error!");
                        result = false;
                    }
                }

            }

            if (cobSalesCountry.Text == "United States")
            {
                if (cobBillState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobBillState.Text))
                    {
                        cobBillState.Focus();
                        MessageDxUtil.ShowError("BillState error!");
                        result = false;
                    }
                }

            }

            if (cobSalesCountry.Text == "United States")
            {
                if (cobShipState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobShipState.Text))
                    {
                        cobShipState.Focus();
                        MessageDxUtil.ShowError("ShipState error!");
                        result = false;
                    }
                }

            }

            #endregion

            return result;
        }


        /// <summary>  
        /// 验证是否为空  
        /// </summary>  
        private void ValidatEmptyUser()
        {
            dxValidationProvider1.ValidationMode = ValidationMode.Manual;
            dxValidationProvider1.Validate();

            Validate(txtSalesCompany, "该字段不能为空！");
            Validate(txtShipCompany, "该字段不能为空！");
            Validate(txtNoteTotal, "该字段不能为空！");
            Validate(txtBillCompany, "该字段不能为空！");
            Validate(cobSalesState, "该字段不能为空！");
            Validate(cobBillCountry, "该字段不能为空！");
            Validate(cobShipCountry, "该字段不能为空！");
            Validate(txtShipStreet, "该字段不能为空！");
            Validate(cobNoteOrderDate, "该字段不能为空！");
            Validate(txtShipContactName, "该字段不能为空！");
        }


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

        private void btuModify_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]));
                if (CheckInput())
                {


                    if (this.txtNoteM.Text.Trim().Length == 0)
                    {
                        MessageDxUtil.ShowTips("Please fill the Modification Reasons.");
                        this.txtNoteM.Focus();
                        return;
                    }
                    if (txtNotePONumber.Text.Trim().Length > 0)
                    {

                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("select count(1) from MCEOrderInfo");
                        strSql.Append(" where ID<>@ID");
                        strSql.Append(" and  PONumber=@PONumber");
                        SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@PONumber", SqlDbType.VarChar,50)
		              	};
                        parameters[0].Value = id;
                        parameters[1].Value = txtNotePONumber.Text;

                        if (Maticsoft.DBUtility.DbHelperSQL.Exists(strSql.ToString(), parameters))
                        {
                            if (MessageDxUtil.ShowYesNoAndWarning("The PO number does exist..Continue?") == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("INSERT INTO [dbo].[MCEOrderInfoChangeRe]([OrderNo],[SalesCompany],[SalesContactName],[SalesStreet],[SalesCity],[SalesState],[SalesZip],[SalesCountry],[SalesTel],[SalesFax],[SaleseMail]");
                    sqlStr.Append(",[BillCompany],[BillContactName],[BillStreet],[BillCity],[BillState],[BillZip],[BillCountry],[BillTel],[BillFax] ,[BilleMail],[ShipCompany]");
                    sqlStr.Append(",[ShipContactName],[ShipStreet],[ShipCity],[ShipState],[ShipZip],[ShipCountry],[ShipTel] ,[ShipFax],[ShipeMail],[InvioceNo],[VendorCode],[Terms]");
                    sqlStr.Append(",[PayMethod],[OrderDate] ,[PONumber],[CustomerRefNo],[SH],[Comments],[Note],[OrderStatus],[OrderProcess],[CustomerID]");
                    sqlStr.Append(",[InvoiceTotal] ,[InvoiceDate] ,[PayStatus],[Carrier] ,[AccountNo] ,[UpdateTime],[Person],[ChangeNote],[ChangePerson],[ChangeTime],[Reasons])");
                    sqlStr.Append("SELECT [OrderNo] ,[SalesCompany],[SalesContactName] ,[SalesStreet],[SalesCity],[SalesState],[SalesZip],[SalesCountry],[SalesTel],[SalesFax],[SaleseMail]");
                    sqlStr.Append(",[BillCompany],[BillContactName],[BillStreet],[BillCity],[BillState],[BillZip],[BillCountry] ,[BillTel],[BillFax],[BilleMail],[ShipCompany]");
                    sqlStr.Append(",[ShipContactName],[ShipStreet],[ShipCity],[ShipState],[ShipZip],[ShipCountry],[ShipTel],[ShipFax],[ShipeMail],[InvioceNo],[VendorCode],[Terms]");
                    sqlStr.Append(",[PayMethod],[OrderDate],[PONumber],[CustomerRefNo],[SH],[Comments],Note,[OrderStatus],[OrderProcess],[CustomerID]");
                    sqlStr.AppendFormat(",[InvoiceTotal],[InvoiceDate],[PayStatus],[Carrier] ,[AccountNo] ,getdate() ,'{0}','{1}','{2}',getdate(),'{3}'", Properties.Settings.Default.LastUser, "Order No " + txtNoteOrderNo.Text + " before modification", Properties.Settings.Default.LastUser, txtNoteM.Text);
                    sqlStr.AppendFormat(" FROM [dbo].[MCEOrderInfo] where id={0}", id);
                    if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(sqlStr.ToString()) > 0)
                    {

                    }

                    mMCEOrderInfo.ID = id;
                    mMCEOrderInfo.OrderNo = txtNoteOrderNo.Text;
                    mMCEOrderInfo.InvioceNo = txtNoteInvoiceNo.Text;
                    mMCEOrderInfo.SalesCompany = txtSalesCompany.EditValue.ToString();
                    mMCEOrderInfo.SalesContactName = txtSalesContactName.Text;
                    mMCEOrderInfo.SaleseMail = txtSalesEmail.Text;
                    mMCEOrderInfo.SalesFax = txtSalesFax.Text;
                    mMCEOrderInfo.SalesStreet = txtSalesStreet.Text;
                    mMCEOrderInfo.SalesTel = txtSalesTel.Text;
                    mMCEOrderInfo.SalesCity = txtSalesCity.Text;
                    mMCEOrderInfo.SalesCountry = cobSalesCountry.Text;
                    mMCEOrderInfo.SalesState = cobSalesState.Text;
                    mMCEOrderInfo.SalesZip = txtSalesZip.Text;
                    mMCEOrderInfo.CustomerID = txtId.Text;
                    mMCEOrderInfo.BillCompany = txtBillCompany.Text;
                    mMCEOrderInfo.BillContactName = txtBillContactname.EditValue.ToString();
                    mMCEOrderInfo.BilleMail = txtBillEmail.Text;
                    mMCEOrderInfo.BillFax = txtBillFax.Text;
                    mMCEOrderInfo.BillStreet = txtBillStreet.Text;
                    mMCEOrderInfo.BillTel = txtBillTel.Text;
                    mMCEOrderInfo.BillCountry = cobBillCountry.Text;
                    mMCEOrderInfo.BillState = cobBillState.Text;
                    mMCEOrderInfo.BillZip = txtBillZip.Text;
                    mMCEOrderInfo.BillCity = txtBillCity.Text;
                //    mMCEOrderInfo.SendConfirmDate = null;

                    mMCEOrderInfo.StockStatus = txtStockStatus.Text;
                    mMCEOrderInfo.ShipCompany = txtShipCompany.Text;
                    mMCEOrderInfo.ShipContactName = txtShipContactName.EditValue.ToString();
                    mMCEOrderInfo.ShipeMail = txtShipEmail.Text;
                    mMCEOrderInfo.ShipFax = txtShipFax.Text;
                    mMCEOrderInfo.ShipStreet = txtShipStreet.Text;
                    mMCEOrderInfo.ShipTel = txtShipTel.Text;
                    mMCEOrderInfo.ShipCity = txtShipCity.Text;
                    mMCEOrderInfo.ShipCountry = cobShipCountry.Text;
                    mMCEOrderInfo.ShipState = cobShipState.Text;
                    mMCEOrderInfo.ShipZip = txtShipZip.Text;

                    mMCEOrderInfo.Note = txtNote.Text;
                    mMCEOrderInfo.AccountNo = txtNoteAccountNo.Text;
                    mMCEOrderInfo.Terms = txtNoteTrems.Text;
                    mMCEOrderInfo.InvioceNo = txtNoteInvoiceNo.Text;
                    mMCEOrderInfo.CustomerRefNo = txtNoteCuNo.Text;
                    mMCEOrderInfo.OrderNo = txtNoteOrderNo.Text;
                    mMCEOrderInfo.OrderDate = Convert.ToDateTime(cobNoteOrderDate.Text);
                    mMCEOrderInfo.PONumber = txtNotePONumber.Text;
                    mMCEOrderInfo.InvoiceTotal = Convert.ToDecimal(txtNoteTotal.Text);
                    mMCEOrderInfo.SH = txtNoteSH.Text == "" ? 0 : Convert.ToInt32(txtNoteSH.Text);
                    mMCEOrderInfo.Carrier = cobNoteCarrier.Text;
                    mMCEOrderInfo.VendorCode = txtNoteVendorCode.Text;
                    mMCEOrderInfo.Carrier = cobNoteCarrier.Text;
                    mMCEOrderInfo.Comments = txtNoteC.Text;
                    mMCEOrderInfo.Note = txtNote.Text;
                    //  mMCEOrderInfo.MailPromotion = cobNoteMailPromotion.Text;
                    mMCEOrderInfo.PayMethod = cobPayMethod.Text;
                    mMCEOrderInfo.Person = Properties.Settings.Default.LastUser;

                    this.mMCEOrderInfo.Tax = Convert.ToDecimal(txtTax.Text == "" ? "0" : txtTax.Text);
                    this.mMCEOrderInfo.Taxation = Convert.ToDecimal(this.txtTaxation.Text == "" ? "0" : this.txtTaxation.Text);

                    if (bMCEOrderInfo.Update(mMCEOrderInfo))
                    {
                        sqlStr.Clear();
                        sqlStr.Append("INSERT INTO [dbo].[MCEOrderInfoChangeRe]([OrderNo],[SalesCompany],[SalesContactName],[SalesStreet],[SalesCity],[SalesState],[SalesZip],[SalesCountry],[SalesTel],[SalesFax],[SaleseMail]");
                        sqlStr.Append(",[BillCompany],[BillContactName],[BillStreet],[BillCity],[BillState],[BillZip],[BillCountry],[BillTel],[BillFax] ,[BilleMail],[ShipCompany]");
                        sqlStr.Append(",[ShipContactName],[ShipStreet],[ShipCity],[ShipState],[ShipZip],[ShipCountry],[ShipTel] ,[ShipFax],[ShipeMail],[InvioceNo],[VendorCode],[Terms]");
                        sqlStr.Append(",[PayMethod],[OrderDate] ,[PONumber],[CustomerRefNo],[SH],[Comments],[Note],[OrderStatus],[OrderProcess],[CustomerID]");
                        sqlStr.Append(",[InvoiceTotal] ,[InvoiceDate] ,[PayStatus],[Carrier] ,[AccountNo] ,[UpdateTime],[Person],[ChangeNote],[ChangePerson],[ChangeTime],[Reasons])");
                        sqlStr.Append("SELECT [OrderNo] ,[SalesCompany],[SalesContactName] ,[SalesStreet],[SalesCity],[SalesState],[SalesZip],[SalesCountry],[SalesTel],[SalesFax],[SaleseMail]");
                        sqlStr.Append(",[BillCompany],[BillContactName],[BillStreet],[BillCity],[BillState],[BillZip],[BillCountry] ,[BillTel],[BillFax],[BilleMail],[ShipCompany]");
                        sqlStr.Append(",[ShipContactName],[ShipStreet],[ShipCity],[ShipState],[ShipZip],[ShipCountry],[ShipTel],[ShipFax],[ShipeMail],[InvioceNo],[VendorCode],[Terms]");
                        sqlStr.Append(",[PayMethod],[OrderDate],[PONumber],[CustomerRefNo],[SH],[Comments],Note,[OrderStatus],[OrderProcess],[CustomerID]");
                        sqlStr.AppendFormat(",[InvoiceTotal],[InvoiceDate],[PayStatus],[Carrier] ,[AccountNo] ,getdate() ,'{0}','{1}','{2}',getdate(),'{3}'", Properties.Settings.Default.LastUser, "Order No " + txtNoteOrderNo.Text + " before modification", Properties.Settings.Default.LastUser, txtNoteM.Text);
                        sqlStr.AppendFormat(" FROM [dbo].[MCEOrderInfo] where id={0}", id);
                        if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(sqlStr.ToString()) > 0)
                        {

                        }

                        //   butSearch_Click(null, null);
                        MessageDxUtil.ShowTips("success");
                    }
                }
            }
        }
        private void btuFinalize_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]));
                mMCEOrderInfo = bMCEOrderInfo.GetModel(id);
               // mMCEOrderInfo.SendConfirmDate = null;
                Double sumSpje;
                DataTable dt = new DataTable();
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select isnull(sum(ProAmount),0) as [sumSpje] from [MCEOrderProInfo] where [OrderNo] = '{0}'", txtNoteOrderNo.Text);

                sumSpje = Convert.ToDouble(DbHelperSQL.GetSingle(strSql.ToString()));
                txtTaxation.Text = Convert.ToDouble(sumSpje * Convert.ToDouble(txtTax.Text == "" ? "0" : txtTax.Text)).ToString();
                txtNoteTotal.Text = (sumSpje + Convert.ToDouble(txtNoteSH.Text) + Convert.ToDouble(txtTaxation.Text)).ToString("0.00");

                //if (Convert.ToDouble(txtNoteTotal.Text) != sumSpje + Convert.ToDouble(txtNoteSH.Text))
                //{
                //    MessageDxUtil.ShowTips("Error. Amount of products and S&H fee is not equal to invoice total");
                //    return;
                //}
                mMCEOrderInfo.ID = id;
                mMCEOrderInfo.OrderNo = txtNoteOrderNo.Text;
                mMCEOrderInfo.OrderProcess = "Finalize";
                mMCEOrderInfo.UpdateTime = DateTime.Now;
                mMCEOrderInfo.Person = Properties.Settings.Default.LastUser;
                mMCEOrderInfo.InvoiceTotal = Convert.ToDecimal(txtNoteTotal.Text);
                mMCEOrderInfo.Taxation = Convert.ToDecimal(txtTaxation.Text);
                mMCEOrderInfoChangeRe.OrderNo = txtNoteOrderNo.Text;
                mMCEOrderInfoChangeRe.OrderProcess = "Finalize";
                mMCEOrderInfoChangeRe.UpdateTime = DateTime.Now;
                mMCEOrderInfoChangeRe.Person = Properties.Settings.Default.LastUser;
                mMCEOrderInfoChangeRe.Reasons = "Finalize";





                if (bMCEOrderInfo.Update(mMCEOrderInfo))
                {
                    if (bMCEOrderInfoChangeRe.Add(mMCEOrderInfoChangeRe) > 0)
                    {
                        DataRow[] selectedRows = dtbMCEOrderInfo.Select("id=" + id + "");
                        if (selectedRows != null && selectedRows.Length > 0)
                        {

                            selectedRows[0]["OrderProcess"] = "Finalize";
                            dtbMCEOrderInfo.AcceptChanges();

                        }


                        MessageDxUtil.ShowTips("success");
                    }

                }



            }
        }
        private void btuCancel_Click(object sender, EventArgs e)
        {
            if (this.txtNoteM.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the Modification Reasons.");
                this.txtNoteM.Focus();
                return;
            }
            if (MessageDxUtil.ShowYesNoAndWarning("Are you sure to change this order status is Cancel") == DialogResult.Yes)
            {

                if (this.gridView1.FocusedRowHandle >= 0)
                {
                    int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]));
                    mMCEOrderInfo = bMCEOrderInfo.GetModel(id);
                    mMCEOrderInfo.ID = id;
                    mMCEOrderInfo.OrderProcess = "Cancel";
                    mMCEOrderInfo.UpdateTime = DateTime.Now;
                    mMCEOrderInfo.Person = Properties.Settings.Default.LastUser;
                    //mMCEOrderInfo.SendConfirmDate = null;


                    mMCEOrderInfoChangeRe.OrderNo = txtNoteOrderNo.Text;
                    mMCEOrderInfoChangeRe.OrderProcess = "Cancel";
                    mMCEOrderInfoChangeRe.UpdateTime = DateTime.Now;
                    mMCEOrderInfoChangeRe.Person = Properties.Settings.Default.LastUser;
                    mMCEOrderInfoChangeRe.Reasons = txtNoteM.Text;
                    string sql = "update MCEOrderProInfo set ProductStatus='Cancel'  where [OrderNo] = '" + txtNoteOrderNo.Text + "'";
                    if (bMCEOrderInfo.Update(mMCEOrderInfo))
                    {
                        if (DbHelperSQL.ExecuteSql(sql) > 0)
                        {
                            if (bMCEOrderInfoChangeRe.Add(mMCEOrderInfoChangeRe) > 0)
                            {
                                DataRow[] selectedRows = dtbMCEOrderInfo.Select("id=" + id + "");
                                if (selectedRows != null && selectedRows.Length > 0)
                                {

                                    selectedRows[0]["OrderProcess"] = "Cancel";
                                    dtbMCEOrderInfo.AcceptChanges();

                                }
                                MessageDxUtil.ShowTips("success");
                            }
                        }
                    }
                }
            }
        }

        private void btuClear_Click(object sender, EventArgs e)
        {
            //txtSalesCompany.EditValue = "";
            //txtSalesCompany.Text = "";
            txtId.Text = "1";
            txtSalesContactName.Text = "";
            txtSalesEmail.Text = "";
            txtSalesFax.Text = "";
            txtSalesStreet.Text = "";
            txtSalesTel.Text = "";
            txtSalesCity.Text = "";
            cobSalesCountry.Text = "";
            cobSalesState.Text = "";
            txtSalesZip.Text = "";

            txtBillCompany.Text = "";
            txtBillContactname.EditValue = "";
            txtBillContactname.Text = "";
            txtBillEmail.Text = "";
            txtBillFax.Text = "";
            txtBillStreet.Text = "";
            txtBillTel.Text = "";
            cobBillCountry.Text = "";
            cobBillState.Text = "";
            txtBillZip.Text = "";
            txtBillCity.Text = "";


            txtShipCompany.Text = "";
            txtShipContactName.EditValue = "";
            txtShipContactName.Text = "";
            txtShipEmail.Text = "";
            txtShipFax.Text = "";
            txtShipStreet.Text = "";
            txtShipTel.Text = "";
            txtShipCity.Text = "";
            cobShipCountry.Text = "";
            cobShipState.Text = "";
            txtShipZip.Text = "";

            txtNote.Text = "";
            txtNoteAccountNo.Text = "";

            txtNoteInvoiceNo.Text = "";
            txtNoteCuNo.Text = "";
            txtNoteOrderNo.Text = "";

            txtNotePONumber.Text = "";
            txtNoteTotal.Text = "";
            txtNoteSH.Text = "";
            cobNoteCarrier.Text = "";
            txtNoteVendorCode.Text = "";
            cobNoteCarrier.Text = "";
            txtNoteC.Text = "";
            txtNote.Text = "";
            // cobNoteMailPromotion.Text = mMCEOrderInfo.MailPromotion;
            cobPayMethod.Text = "";
            txtNoteM.Text = "";
        }



        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {

            if (e.RowHandle > -1)
            {
                string status = gridView1.GetRowCellValue(e.RowHandle, "OrderProcess").ToString();
                if (status == "Finalize")
                {
                    e.Appearance.BackColor = Color.Lime;
                }
                else if (status == "Cancel")
                {
                    e.Appearance.BackColor = Color.LightSlateGray;
                }

                //if (gridView1.GetRowCellValue(e.RowHandle, "StockStatus") != null)
                //{
                //    if (gridView1.GetRowCellValue(e.RowHandle, "StockStatus").ToString() != string.Empty)
                //    {
                //        e.Appearance.ForeColor = Color.Blue;
                //    }

                //}

            }




        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

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

        private void ButWeb_Click(object sender, EventArgs e)
        {
            ButWeb.Enabled = false;
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.Append(" 1=1 ");
            sqlWhere.AppendFormat(" and Note like '%{0}%' ", txtWebOrderNo.Text);

            DataTable dtzj = new DataTable();

            dtzj = bMCEOrderInfo.GetList(sqlWhere.ToString()).Tables[0];
            if (dtzj.Rows.Count > 0)
            {
                ButWeb.Enabled = true;
                MessageDxUtil.ShowWarning("This order already exis! please check again!.");
                return;
            }

            string sqlDel = "delete from  MCEOrderProInfo  where OrderNo = '" + txtWebOrderNo.Text + "' ";
            if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(sqlDel) > 0)
            {

            }
            DataTable dt = new DataTable();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from orderInfoV  where orderid='{0}'", txtWebOrderNo.Text);
            dt = Maticsoft.DBUtility.DbHelperMySQL.Query(strSql.ToString()).Tables[0];
            IsWeb = true;
            if (dt.Rows.Count > 0)
            {


                txtSalesCompany.EditValue = dt.Rows[0]["shoppingOrganizationName"].ToString();
                txtSalesCompany.Text = dt.Rows[0]["shoppingOrganizationName"].ToString();
                txtSalesContactName.Text = dt.Rows[0]["applicationName"].ToString();
                txtSalesEmail.Text = dt.Rows[0]["shoppingemail"].ToString();
                txtSalesStreet.Text = dt.Rows[0]["shoppingStreet"].ToString();
                txtSalesTel.Text = dt.Rows[0]["shoppingp"].ToString();
                txtSalesCity.Text = dt.Rows[0]["shoppingCity"].ToString();
                cobSalesCountry.Text = dt.Rows[0]["shoppingc"].ToString();
                cobSalesState.Text = dt.Rows[0]["shoppingState"].ToString();
                txtSalesZip.Text = dt.Rows[0]["shoppingZIP"].ToString();


                txtShipCompany.Text = dt.Rows[0]["shoppingOrganizationName"].ToString();
                txtShipContactName.Text = dt.Rows[0]["applicationName"].ToString();
                txtShipEmail.Text = dt.Rows[0]["shoppingemail"].ToString();
                txtShipStreet.Text = dt.Rows[0]["shoppingStreet"].ToString();
                txtShipTel.Text = dt.Rows[0]["shoppingp"].ToString();
                txtShipCity.Text = dt.Rows[0]["shoppingCity"].ToString();
                cobShipCountry.Text = dt.Rows[0]["shoppingc"].ToString();
                cobShipState.Text = dt.Rows[0]["shoppingState"].ToString();
                txtShipZip.Text = dt.Rows[0]["shoppingZIP"].ToString();




                txtBillCompany.Text = dt.Rows[0]["billingOrganizationName"].ToString();
                txtBillContactname.EditValue = dt.Rows[0]["billingInvoiceContactName"].ToString();
                txtBillContactname.Text = dt.Rows[0]["billingInvoiceContactName"].ToString();
                txtBillEmail.Text = dt.Rows[0]["billingEmailAddress"].ToString();
                txtBillStreet.Text = dt.Rows[0]["billingStreet"].ToString();
                txtBillTel.Text = dt.Rows[0]["billingPhoneNumber"].ToString();
                cobBillCountry.Text = dt.Rows[0]["billingc"].ToString();
                cobBillState.Text = dt.Rows[0]["billingstate"].ToString();
                txtBillZip.Text = dt.Rows[0]["billingZIP"].ToString();
                txtBillCity.Text = dt.Rows[0]["billingCity"].ToString();



                txtNoteAccountNo.Text = dt.Rows[0]["fedExAccount"].ToString();
                cobNoteOrderDate.Text = DateTime.Now.ToShortDateString();
                txtNotePONumber.Text = dt.Rows[0]["po"].ToString();
                txtNoteTotal.Text = dt.Rows[0]["totalPrice"].ToString().Replace("$", "");
                txtNoteSH.Text = dt.Rows[0]["freight"].ToString().Replace("$", ""); ;



                txtNote.Text = "Order from web # " + txtWebOrderNo.Text;
                // cobNoteMailPromotion.Text = mMCEOrderInfo.MailPromotion;
                cobPayMethod.Text = dt.Rows[0]["paymentMethod"].ToString();

                this.txtTax.Text = dt.Rows[0]["tax"].ToString();   //税率
                this.txtTaxation.Text = dt.Rows[0]["tax_amount"].ToString();   //税费

                string strBefor = "";
                string strAfter = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBefor = "";
                    strAfter = "";
                    mMCEOrderProInfo.OrderNo = txtWebOrderNo.Text;
                    mMCEOrderProInfo.ProCatalogNo = dt.Rows[i]["csno"].ToString();
                    mMCEOrderProInfo.ProDescription = dt.Rows[i]["productName"].ToString();
                    cutStr(dt.Rows[i]["size"].ToString(), out strBefor, out strAfter);
                    mMCEOrderProInfo.ProSize = Convert.ToDecimal(strBefor);
                    mMCEOrderProInfo.ProUnit = strAfter;
                    mMCEOrderProInfo.ProQuantity = Convert.ToInt32(dt.Rows[i]["quantity"].ToString());
                    mMCEOrderProInfo.ProAmount = Convert.ToDecimal(dt.Rows[i]["amount"].ToString());
                    mMCEOrderProInfo.ProCurrency = "USD";
                    mMCEOrderProInfo.ProductStatus = "OK";
                    mMCEOrderProInfo.Person = Properties.Settings.Default.LastUser;
                    mMCEOrderProInfo.UpdateTime = DateTime.Now;
                    if (bMCEOrderProInfo.Add(mMCEOrderProInfo) > 0)
                    {


                    }


                }


            }
            else
                MessageDxUtil.ShowWarning("The orderNo  is wrong! please check again!.");
            IsWeb = false;
            // btuAdd_Click(null, null);
            ButWeb.Enabled = true;
        }
        private void cutStr(string str, out string strBefor, out string strAfter)
        {
            strBefor = "";
            strAfter = "";
            str = str.ToLower().Replace("in", " ").Replace("water", "").Replace("ethanol", "").Replace("dmso", "").Replace("dmf", "").Replace(" ", "").Replace("\t", "");


            if (str.ToLower().IndexOf("sample") >= 0)
            {
                strBefor = "1";
                strAfter = "mg";
            }
            else if (str.ToLower().IndexOf("mg") > 0)
            {
                strBefor = str.Substring(0, str.ToLower().IndexOf("mg"));
                strAfter = str.Substring(str.ToLower().IndexOf("mg"));
            }
            else if (str.ToLower().IndexOf("ug") > 0)
            {
                strBefor = str.Substring(0, str.ToLower().IndexOf("ug"));
                strAfter = str.Substring(str.ToLower().IndexOf("ug"));
            }
            else if (str.ToLower().IndexOf("kg") > 0)
            {
                strBefor = str.Substring(0, str.ToLower().IndexOf("kg"));
                strAfter = str.Substring(str.ToLower().IndexOf("kg"));
            }
            else if (str.ToLower().IndexOf("g") > 0)
            {
                strBefor = str.Substring(0, str.ToLower().IndexOf("g"));
                strAfter = str.Substring(str.ToLower().IndexOf("g"));
            }
            else if (str.ToLower().IndexOf("10mm*1ml") >= 0)
            {
                strBefor = "10";
                strAfter = "mM*1mL";
            }
            //else if (str.ToLower().IndexOf("freesample") >= 0)
            //{
            //    strBefor = "0";
            //    strAfter = "mM*1mL";
            //}


        }

        private void btuEmail_Click(object sender, EventArgs e)
        {

            DbHelperSQL.ExecuteSql(" update  mceorderinfo set SendConfirmDate=null,note=replace(note,'Order from web','') where orderno='" + txtNoteOrderNo + "' ");

            DbHelperSQL.ExecuteSql(" delete  from  dbo.MCEMailSendRecord where type='Order Confirmation' and orderno='" + txtNoteOrderNo + "' ");

            MessageDxUtil.ShowTips(" Mail will be sent in 30 minutes ");
            ////原模板内容
            //string html = GenerateHtmlHelper.ReadHtml(Application.StartupPath + @"\chemenseT.html");



            ////需要替换的内容


            //string imgPath = Application.StartupPath + @"\usa_email_logo.png";
            //html = html.Replace("{#CName}", txtSalesContactName.Text);
            //html = html.Replace("{#ShipNAme}", txtShipContactName.Text);
            //html = html.Replace("{#ShipEmail}", txtShipEmail.Text);
            //html = html.Replace("{#ShipDate}", cobNoteOrderDate.Text);
            //html = html.Replace("{#ShipPNo}", txtShipTel.Text);
            //html = html.Replace("{#ShipPo}", txtNotePONumber.Text);
            //html = html.Replace("{#ShipINNo}", txtNoteInvoiceNo.Text);
            //html = html.Replace("{#ShipAddress}", EuSoft.Common.StringPlus.ConStr(txtShipCompany.Text) + EuSoft.Common.StringPlus.ConStr(txtShipStreet.Text) + EuSoft.Common.StringPlus.ConStr(txtShipCity.Text) + EuSoft.Common.StringPlus.ConStr(cobShipState.Text) + EuSoft.Common.StringPlus.ConStr(txtShipZip.Text) + EuSoft.Common.StringPlus.ConStr(cobShipCountry.Text, false));
            //html = html.Replace("{#BillName}", txtBillContactname.Text);
            //html = html.Replace("{#BillPNo}", txtBillTel.Text);

            //html = html.Replace("{#BillEmail}", txtBillEmail.Text);
            //html = html.Replace("{#BillAddress}", EuSoft.Common.StringPlus.ConStr(txtBillCompany.Text) + EuSoft.Common.StringPlus.ConStr(txtBillStreet.Text) + EuSoft.Common.StringPlus.ConStr(txtBillCity.Text) + EuSoft.Common.StringPlus.ConStr(cobBillState.Text) + EuSoft.Common.StringPlus.ConStr(txtBillZip.Text) + EuSoft.Common.StringPlus.ConStr(cobBillCountry.Text, false));
            //html = html.Replace("{#Note}", txtNoteC.Text);


            //if (txtNoteAccountNo.Text != string.Empty)
            //    html = html.Replace("{#FedexAc}", "FedEx account: ");
            //else
            //    html = html.Replace("{#FedexAc}", " &nbsp;");

            //html = html.Replace("{#FedexAccount}", txtNoteAccountNo.Text);
            //html = html.Replace("{#Sh}", txtNoteSH.Text);

            //string pMethod = "";
            //if (cobPayMethod.Text != string.Empty)
            //{
            //    pMethod += "<tr>";
            //    pMethod += @"<td width=""700"" style=""background:#FFFFFF; padding-left:20px; padding-right:20px; border-top:20px solid #FFF;"">";
            //    pMethod += @"<fieldset style=""width:670px; border:1px solid #CCC; padding:0; padding-left:15px; padding-right:15px;"">";
            //    pMethod += @"<legend><strong style=""color:#6a4b92; font-weight:bold; font-size:14px; font-family:Arial, Helvetica, sans-serif;"">Payment Method</strong></legend>";
            //    pMethod += @"<table width=""670"" cellpadding=""0"" cellspacing=""0"" style=""margin:0; padding:0; border-top:10px solid #FFF; border-bottom:5px solid #FFF; "">";
            //    pMethod += @"<tr>";
            //    pMethod += @"<td valign=""top"" height=""25"" style=""font-family:Arial, Helvetica, sans-serif; font-weight:bold; font-size:12px; color:#333;"">";
            //    pMethod += string.Format("{0}", cobPayMethod.Text);
            //    pMethod += @"</td>";
            //    pMethod += @"</tr>";
            //    pMethod += @"</table>";
            //    pMethod += @"</fieldset>";
            //    pMethod += @"</td>";
            //    pMethod += @"</tr>";
            //}

            //html = html.Replace("{#PaymentMethod}", pMethod);

            //string newText = string.Empty;
            //const string top = "top";
            //const string height = "25";
            //const string style = "font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333;";

            //string sqlWhere = string.Format(" orderno='{0}' ", txtNoteOrderNo.Text);
            //decimal SumToTal;
            //SumToTal = 0;
            //DataTable dt = bMCEOrderProInfo.GetList(sqlWhere).Tables[0];
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    newText += "<tr>";
            //    newText += string.Format(@"<td valign=""{0}""  width=""{1}"" height=""{2}"" style=""{3}"">" + "{4}" + "</td>", top, 100, height, style, dt.Rows[i]["ProCatalogNo"].ToString());
            //    newText += string.Format(@"<td valign=""{0}""  width=""{1}"" height=""{2}"" style=""{3}"">" + "{4}" + "</td>", top, 320, height, style, dt.Rows[i]["ProDescription"].ToString());
            //    newText += string.Format(@"<td valign=""{0}""  width=""{1}"" height=""{2}"" style=""{3}"">" + "{4}" + "</td>", top, 90, height, style,  dt.Rows[i]["ProSize"].ToString()+" "+dt.Rows[i]["ProUnit"].ToString());
            //    newText += string.Format(@"<td valign=""{0}"" align=""center""  width=""{1}"" height=""{2}"" style=""{3}"">" + "{4}" + "</td>", top, 85, height, style, dt.Rows[i]["ProQuantity"].ToString());
            //    newText += string.Format(@"<td valign=""{0}""  width=""{1}"" height=""{2}"" style=""{3}"">" + "${4}" + "</td>", top, 75, height, style, dt.Rows[i]["ProAmount"].ToString());
            //    newText += "</tr>";
            //    SumToTal = SumToTal + Convert.ToDecimal(dt.Rows[i]["ProAmount"].ToString());
            //}
            //SumToTal = SumToTal + Convert.ToDecimal(txtNoteSH.Text);
            //html = html.Replace("{#Total}", SumToTal.ToString());
            ////替换操作
            //html = html.Replace("{#OrderInfo}", newText);

            ////写入操作
            //string savepath = Application.StartupPath + @"\chemenseTest.html";
            //bool success = GenerateHtmlHelper.WriteHtml(html, savepath);
            //if (success)
            //{
            //    string content = GenerateHtmlHelper.ReadHtml(savepath);
            //    //   List<string> mailAddress = new List<string>();

            //    List<string> mailAddress = new List<string>(txtSalesEmail.Text.Split(';'));
            //    // mailAddress.Add(txtSalesEmail.Text);
            //    //mailAddress.Add("yutao@chemexpress.co");
            //    //mailAddress.Add("727492819@qq.com");
            //    bool bOk;
            //    string Subject = "Order Confirmation from ChemScene (PO#" + txtNotePONumber.Text + ", INV#" + txtNoteInvoiceNo.Text + ")";

            //    if (txtNotePONumber.Text.Length == 0)
            //        Subject = "Order Confirmation from ChemScene (INV#" + txtNoteInvoiceNo.Text + ")";


            //    string test = EuSoft.Common.MailSender.sendMail(Subject, content, "CS Customer Service <customer_service@chemscene.com>", mailAddress, "smtp.gmail.com", 587, "customer_service@chemscene.com", "haoyuan20160601", true, "system_letter@chemscene.com", imgPath, "", out bOk);
            //    if (bOk)
            //    {
            //        MessageDxUtil.ShowTips("success");
            //    }
            //    else
            //    {
            //        MessageDxUtil.ShowTips("error");
            //    }
            //}
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            FrmMoreEmail xFrm = new FrmMoreEmail(txtNoteOrderNo.Text);
            xFrm.Show();

        }

        private void txtSalesCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSalesCompany_Leave(object sender, EventArgs e)
        {
            if (txtSalesCompany.Text.Trim() != "")
            {
                dataGridView1.Visible = false;
                string sqlWhere = string.Format("SalesCompany='{0}'", txtSalesCompany.Text);
                DataTable dt = new DataTable(); ;
                dt = bMcus.GetList(sqlWhere).Tables[0];
                if (dt.Rows.Count <= 0) return;

                txtId.Text = dt.Rows[0]["ID"].ToString();
                int id = Convert.ToInt32(txtId.Text);

                mMcus = bMcus.GetModel(id);
                if (mMcus != null && !IsWeb)
                {
                    txtSalesCompany.Text = mMcus.SalesCompany;
                    txtSalesContactName.Text = mMcus.SalesContactName;
                    txtSalesEmail.Text = mMcus.SaleseMail;
                    txtSalesFax.Text = mMcus.SalesFax;
                    txtSalesStreet.Text = mMcus.SalesStreet;
                    txtSalesTel.Text = mMcus.SalesTel;
                    txtSalesCity.Text = mMcus.SalesCity;
                    cobSalesCountry.Text = mMcus.SalesCountry;
                    cobSalesState.Text = mMcus.SalesState;
                    txtSalesZip.Text = mMcus.SalesZip;

                    txtBillCompany.Text = mMcus.BillCompany;
                    txtBillContactname.Text = mMcus.BillContactName;
                    txtBillEmail.Text = mMcus.BilleMail;
                    txtBillFax.Text = mMcus.BillFax;
                    txtBillStreet.Text = mMcus.BillStreet;
                    txtBillTel.Text = mMcus.BillTel;
                    cobBillCountry.Text = mMcus.BillCountry;
                    cobBillState.Text = mMcus.BillState;
                    txtBillZip.Text = mMcus.BillZip;
                    txtBillCity.Text = mMcus.BillCity;


                    txtShipCompany.Text = mMcus.ShipCompany;
                    txtShipContactName.Text = mMcus.ShipContactName;
                    txtShipEmail.Text = mMcus.ShipeMail;
                    txtShipFax.Text = mMcus.ShipFax;
                    txtShipStreet.Text = mMcus.ShipStreet;
                    txtShipTel.Text = mMcus.ShipTel;
                    txtShipCity.Text = mMcus.ShipCity;
                    cobShipCountry.Text = mMcus.ShipCountry;
                    cobShipState.Text = mMcus.ShipState;
                    txtShipZip.Text = mMcus.ShipZip;

                    txtNote.Text = mMcus.Note;
                    txtNoteAccountNo.Text = mMcus.AccountNo;
                    txtNoteTrems.Text = mMcus.Terms;
                    // txtNoteVatIDNo.Text = mMcus.VATIDNo;
                    txtNoteVendorCode.Text = mMcus.VendorCode;
                    cobNoteCarrier.Text = mMcus.Carrier;
                    //  cobNoteMailPromotion.Text = mMcus.MailPromotion;
                    cobPayMethod.Text = mMcus.PayMethod;
                }

            }
        }

        private void txtSalesContactName_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSalesContactName_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim() != "")
            {
                string sql = "select distinct [SalesContactName],[SalesStreet],[SalesCity],[SalesState],[SalesZip],[SalesCountry],[SalesTel],[SalesFax],[SaleseMail] from [MCEOrderInfo] where [CustomerID] = " + txtId.Text + " order by [SaleseMail]";
                DataTable dt = DbHelperSQL.Query(sql).Tables[0];
                gridControl5.DataSource = dt;
                gridView9.BestFitColumns();
                groupControl10.Visible = true;
            }
        }

        private void gridControl5_Click(object sender, EventArgs e)
        {
            if (gridView9.FocusedRowHandle > -1)
            {

                txtSalesContactName.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "SalesContactName").ToString();
                txtSalesEmail.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "SaleseMail").ToString();
                txtSalesFax.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "SalesFax").ToString();
                txtSalesStreet.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "SalesStreet").ToString();
                txtSalesTel.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "SalesTel").ToString();
                txtSalesCity.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "SalesCity").ToString();
                cobSalesCountry.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "SalesCountry").ToString();
                cobSalesState.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "SalesState").ToString();
                txtSalesZip.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "SalesZip").ToString();
            }
            groupControl10.Visible = false;
        }

        private void gridControl4_Click(object sender, EventArgs e)
        {
            if (gridView7.FocusedRowHandle > -1)
            {


                txtBillContactname.Text = gridView7.GetRowCellValue(this.gridView7.FocusedRowHandle, "BillContactName").ToString();
                txtBillEmail.Text = gridView7.GetRowCellValue(this.gridView7.FocusedRowHandle, "BilleMail").ToString();
                txtBillFax.Text = gridView7.GetRowCellValue(this.gridView7.FocusedRowHandle, "BillFax").ToString();
                txtBillStreet.Text = gridView7.GetRowCellValue(this.gridView7.FocusedRowHandle, "BillStreet").ToString();
                txtBillTel.Text = gridView7.GetRowCellValue(this.gridView7.FocusedRowHandle, "BillTel").ToString();
                cobBillCountry.Text = gridView7.GetRowCellValue(this.gridView7.FocusedRowHandle, "BillCountry").ToString();
                cobBillState.Text = gridView7.GetRowCellValue(this.gridView7.FocusedRowHandle, "BillState").ToString();
                txtBillZip.Text = gridView7.GetRowCellValue(this.gridView7.FocusedRowHandle, "BillZip").ToString();
                txtBillCity.Text = gridView7.GetRowCellValue(this.gridView7.FocusedRowHandle, "BillCity").ToString();
            }
            groupControl8.Visible = false;
        }

        private void txtShipContactName_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim() != "")
            {
                //  DataTable dt = bMCEOrderInfo.GetList(" CustomerID=" + txtId.Text + "").Tables[0];
                string sql = "select distinct [shipContactName],[ShipStreet],[ShipCity],[ShipState],[ShipZip],[ShipCountry],[ShipTel],[ShipFax],[ShipeMail] from [MCEOrderInfo] where [CustomerID] = " + txtId.Text + " order by [ShipeMail]";
                DataTable dt = DbHelperSQL.Query(sql).Tables[0];
                gridControl3.DataSource = dt;
                gridView5.BestFitColumns();
                groupControl9.Visible = true;
            }
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            if (gridView5.FocusedRowHandle > -1)
            {

                txtShipContactName.Text = gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, "shipContactName").ToString();
                txtShipEmail.Text = gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, "ShipeMail").ToString();
                txtShipFax.Text = gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, "ShipFax").ToString();
                txtShipStreet.Text = gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, "ShipStreet").ToString();
                txtShipTel.Text = gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, "ShipTel").ToString();
                txtShipCity.Text = gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, "ShipCity").ToString();
                cobShipCountry.Text = gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, "ShipCountry").ToString();
                cobShipState.Text = gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, "ShipState").ToString();
                txtShipZip.Text = gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, "ShipZip").ToString();

            }
            groupControl9.Visible = false;
        }

        private void groupControl10_Click(object sender, EventArgs e)
        {
            groupControl10.Visible = !groupControl10.Visible;

        }

        private void groupControl8_Click(object sender, EventArgs e)
        {
            groupControl8.Visible = !groupControl8.Visible;

        }

        private void groupControl9_Click(object sender, EventArgs e)
        {
            groupControl9.Visible = !groupControl9.Visible;

        }

        private void FrmOrder_Resize(object sender, EventArgs e)
        {
            groupControl10.Width = txtSalesContactName.Width;
            groupControl8.Width = txtBillContactname.Width;
            groupControl9.Width = txtSalesContactName.Width;
        }

        private void cobNoteOrderDate_Click(object sender, EventArgs e)
        {
            cobNoteOrderDate.Text = DateTime.Now.ToShortDateString();
        }

        private void FrmOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {


                txtBillCompany.Text = txtSalesCompany.Text;
                txtBillContactname.Text = txtSalesContactName.Text;
                txtBillEmail.Text = txtSalesEmail.Text;
                txtBillFax.Text = txtSalesFax.Text;
                txtBillStreet.Text = txtSalesStreet.Text;
                txtBillTel.Text = txtSalesTel.Text;
                cobBillCountry.Text = cobSalesCountry.Text;
                cobBillState.Text = cobSalesState.Text;
                txtBillZip.Text = txtSalesZip.Text;
                txtBillCity.Text = txtSalesCity.Text;


                txtShipCompany.Text = txtSalesCompany.Text;
                txtShipContactName.Text = txtSalesContactName.Text;
                txtShipEmail.Text = txtSalesEmail.Text;
                txtShipFax.Text = txtSalesFax.Text;
                txtShipStreet.Text = txtSalesStreet.Text;
                txtShipTel.Text = txtSalesTel.Text;
                txtShipCity.Text = txtSalesCity.Text;
                cobShipCountry.Text = cobSalesCountry.Text;
                cobShipState.Text = cobSalesState.Text;
                txtShipZip.Text = txtSalesZip.Text;

            }
        }

        private void txtSalesCompany_KeyUp(object sender, KeyEventArgs e)
        {
            dataGridView1.Visible = true;

            string key = this.txtSalesCompany.Text.Trim();
            dataGridView1.DataSource = null;
            if (string.IsNullOrEmpty(key))
            {
                dataGridView1.Visible = false;
                return;
            }
            string sqlSales = "select top 10 id, SalesCompany FROM MCECustomersInfo where SalesCompany like '%" + key + "%' ";
            DataTable dt = DbHelperSQL.Query(sqlSales).Tables[0];
            int swCount = dt.Rows.Count;
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Width = txtSalesCompany.Width - 20;
            this.dataGridView1.Height = swCount * 50;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.BorderStyle = BorderStyle.None;
            this.dataGridView1.CurrentCell = null;
            if (swCount > 0)
            {
                //  this.dataGridView1.Height = swCount * 25;
                //  this.dataGridView1.Location = new Point(0, 0);

                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[1].Width = txtSalesCompany.Width - 20;

            }
            else
            {
                dataGridView1.DataSource = null;
            }

        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1) { this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White; this.dataGridView1.CurrentCell = null; }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1) { this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue; this.dataGridView1.CurrentCell = null; }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSalesCompany.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(); //获得当前;
            dataGridView1.Visible = false;
            txtSalesCompany_Leave(null, null);
        }

        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                string status = gridView2.GetRowCellValue(e.RowHandle, "StockStatus").ToString();
                if (status == "CA")
                {
                    e.Appearance.BackColor = Color.Aquamarine;
                }
               
            }

        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            ////第一行  
            if (e.RowHandle > -1)
            {
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

        private void txtShipStreet_EditValueChanged(object sender, EventArgs e)
        {
            txtTax.Text = string.Empty;
            txtTaxation.Text = string.Empty;
            if (txtShipCity.Text.ToString() != "" && cobShipState.Text.ToString() != "" && txtShipStreet.Text.ToString() != "" && txtShipZip.Text.ToString() != "")
            {
                string Tax = ObtainMethod(txtShipCity.Text, cobShipState.Text, txtShipZip.Text, txtShipStreet.Text);
                txtTax.Text = Tax;
            }
        }

        private void txtShipCity_EditValueChanged(object sender, EventArgs e)
        {
            txtTax.Text = string.Empty;
            txtTaxation.Text = string.Empty;
            if (txtShipCity.Text.ToString() != "" && cobShipState.Text.ToString() != "" && txtShipStreet.Text.ToString() != "" && txtShipZip.Text.ToString() != "")
            {
                string Tax = ObtainMethod(txtShipCity.Text, cobShipState.Text, txtShipZip.Text, txtShipStreet.Text);
                txtTax.Text = Tax;
            }
        }

        private void cobShipState_EditValueChanged(object sender, EventArgs e)
        {
            txtTax.Text = string.Empty;
            txtTaxation.Text = string.Empty;
            if (txtShipCity.Text.ToString() != "" && cobShipState.Text.ToString() != "" && txtShipStreet.Text.ToString() != "" && txtShipZip.Text.ToString() != "")
            {
                string Tax = ObtainMethod(txtShipCity.Text, cobShipState.Text, txtShipZip.Text, txtShipStreet.Text);
                txtTax.Text = Tax;
            }
        }

        private void txtShipZip_EditValueChanged(object sender, EventArgs e)
        {
            txtTax.Text = string.Empty;
            txtTaxation.Text = string.Empty;
            if (txtShipCity.Text.ToString() != "" && cobShipState.Text.ToString() != "" && txtShipStreet.Text.ToString() != "" && txtShipZip.Text.ToString() != "")
            {
                string Tax = ObtainMethod(txtShipCity.Text, cobShipState.Text, txtShipZip.Text, txtShipStreet.Text);
                txtTax.Text = Tax;
            }
        }

        public string ObtainMethod(string city, string state, string zipcode, string streeaddress)
        {
            try {
            CATaxRateAPIClient client = new CATaxRateAPIClient();
            cag.City = city;
            cag.State = state;
            cag.ZipCode = Convert.ToInt32(zipcode);
            cag.StreetAddress = streeaddress;

            car = client.GetRate(cag);
            
            }catch(Exception e){
                
            }
            if (car.CARateResponses == null)
            {
                return "0";

            }
            if (car.CARateResponses[0].Errors.Length > 0)
            {
                return "0";
            }
            else
                return car.CARateResponses[0].Responses[0].Rate.ToString();
        }

        private void cobBillCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cobBillCountry.Text != "United States")
            {
                cobBillState.Text = "";
                cobBillState.Properties.Items.Clear();
            }
            else
            {

                BindComboBoxEdit(cobBillState, dtState, 2);
                cobBillState.Text = "";

            }
        }

        private void cobShipCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cobShipCountry.Text != "United States")
            {

                cobShipState.Text = "";
                cobShipState.Properties.Items.Clear();
            }
            else
            {

                BindComboBoxEdit(cobShipState, dtState, 2);
                cobShipState.Text = "";

            }
        }

        private void cobSalesCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cobSalesCountry.Text != "United States")
            {
                cobSalesState.Text = "";
                cobSalesState.Properties.Items.Clear();
            }
            else
            {

                BindComboBoxEdit(cobSalesState, dtState, 2);
                cobSalesState.Text = "";
            }
        }










    }
}