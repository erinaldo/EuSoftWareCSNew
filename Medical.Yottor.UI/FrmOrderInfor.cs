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
    public partial class FrmOrderInfor : DevExpress.XtraEditors.XtraForm
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
        EuSoft.BLL.MCEShipProInfo bMCEShipProInfo = new EuSoft.BLL.MCEShipProInfo();
        EuSoft.BLL.MCEShipmentInfo bMCEShipmentInfo = new EuSoft.BLL.MCEShipmentInfo();
        EuSoft.BLL.MCEPaymentInfo bMCEPaymentInfo = new EuSoft.BLL.MCEPaymentInfo();
        public FrmOrderInfor()
        {
            InitializeComponent();
            getDate();
            //this.gridView2.UpdateCurrentRow();
            //gridControl2.DataSource = getEmpDataTable();
            //this.gridView2.PopulateColumns();

        }

        public void getDate()
        {
          

            DataTable dt = bMCEOrderInfo.GetList(30, "( OrderDate >   DateAdd(day,-1,getdate()) or [OrderProcess] = 'In progress') and [OrderStatus] = 'Ok'", "id desc").Tables[0];
            gridControl1.DataSource = dt;
            LabRecords.Text = "Records:" + dt.Rows.Count.ToString();
            LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();
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
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_Click);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {

            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.Append("  1=1 and ( OrderDate >   DateAdd(day,-1,getdate()) or [OrderProcess] = 'In progress') and [OrderStatus] = 'Ok' ");
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
            if (txtCaNo.Text != string.Empty)
            {
                sqlWhere.AppendFormat(" and exists (select * from dbo.MCEOrderProInfo where  procatalogno like '%" + txtCaNo.Text + "%' and MCEOrderInfo.orderno=orderno)");
                //var ftp = new FTPOperater();
                //ftp.Server = "34.207.148.81";
                //ftp.User = "HaoYuanFTP43";
                //ftp.Pass = "Lmh20160929";
                //ftp.Port = 2143;
                //string[] ftpname = ftp.GetList("/PDF/MCE/USA#Merge/");
                //List<ListModel> MergePDFName = new List<ListModel>();
                //foreach (string names in ftpname)
                //{
                //    ListModel lmodel = new ListModel();
                //    lmodel.listmodel = names.Replace("\r", "").Replace("\n", "");
                //    if (lmodel.listmodel.ToUpper().IndexOf(txtCaNo.Text.ToUpper()) > 0)
                //    {
                //        MergePDFName.Add(lmodel);
                //    }
                //}

                //gridControl6.DataSource = MergePDFName;
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

            DataTable dt = bMCEOrderInfo.GetList(sqlWhere.ToString()).Tables[0];
            gridControl1.DataSource = dt;
            LabRecords.Text = "Records:" + dt.Rows.Count.ToString();
            LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();

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
            if (txtCaNo.Text != string.Empty)
            {
                sqlWhere.AppendFormat(" and exists (select * from dbo.MCEOrderProInfo where  procatalogno like '%" + txtCaNo.Text + "%' and MCEOrderInfo.orderno=orderno)");
                //var ftp = new FTPOperater();
                //ftp.Server = "34.207.148.81";
                //ftp.User = "HaoYuanFTP43";
                //ftp.Pass = "Lmh20160929";
                //ftp.Port = 2143;
                //string[] ftpname = ftp.GetList("/PDF/MCE/USA#Merge/");
                //List<ListModel> MergePDFName = new List<ListModel>();
                //foreach (string names in ftpname)
                //{
                //    ListModel lmodel = new ListModel();
                //    lmodel.listmodel = names.Replace("\r", "").Replace("\n", "");
                //    if (lmodel.listmodel.ToUpper().IndexOf(txtCaNo.Text.ToUpper()) > 0)
                //    {
                //        MergePDFName.Add(lmodel);
                //    }
                //}

                //gridControl6.DataSource = MergePDFName;
            }

            if (txtDate1.Text != "")
            {
                sqlWhere.AppendFormat(" and  OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlWhere.AppendFormat(" and  OrderDate<='{0}'", txtDate2.Text);
            }

            DataTable dt = bMCEOrderInfo.GetList(sqlWhere.ToString()).Tables[0];
            gridControl1.DataSource = dt;

            LabRecords.Text = "Records:" + dt.Rows.Count.ToString();
            LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();
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
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]));
                string orderNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[2]).ToString();
                mMCEOrderInfo = bMCEOrderInfo.GetModel(id);

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
                txtStockStatus.Text = mMCEOrderInfo.StockStatus;
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
                cobPayMethod.Text = mMCEOrderInfo.PayMethod;
                xtraTabControl1_SelectedPageChanged(null, null);
               // xtraTabControl1_Selected(null, null);
                //    this.txtCardGrade.SelectedValueChanged += new EventHandler(txtCardGrade_SelectedValueChanged);

              //  this.xtraTabControl1.SelectedPageChanging += xtraTabControl1_SelectedPageChanging;
                //   xtraTabControl1_SelectedPageChanging += new xtraTabControlEventHandler(xtraTabControl1_SelectedPageChanging);
                // xtraTabControl1_SelectedPageChanging(null, xtraTabControl1.SelectedPageChanging);
                //  string sqlWhere = string.Format(" orderno='{0}' ", orderNo);

                ////this.gridView2.UpdateCurrentRow();
                // gridControl2.DataSource = bMCEOrderProInfo.GetList(sqlWhere).Tables[0];
                //  this.gridView2.PopulateColumns();


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
            //if (txtId.Text.Trim() != "")
            //{
            //    DataTable dt = new DataTable();
            //    string sqlWhere = string.Format(" CustomerID={0} ", txtId.Text);
            //    dt = bMCEOrderInfo.GetAllList().Tables[0];
            //    txtBillContactname.Properties.DataSource = dt;
            //    txtBillContactname.Properties.ValueMember = "BillContactName";
            //    txtBillContactname.Properties.DisplayMember = "BillContactName";
            //}
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

        private void gridView2_RowStyle(object sender, RowStyleEventArgs e)
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
            //第一行  
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

        private void gridView6_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                string status = gridView6.GetRowCellValue(e.RowHandle, "StockStatus").ToString();
                if (status == "CA")
                {
                    e.Appearance.BackColor = Color.Aquamarine;
                }

            }
        }

        private void gridView9_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                string status = gridView9.GetRowCellValue(e.RowHandle, "StockStatus").ToString();
                if (status == "CA")
                {
                    e.Appearance.BackColor = Color.Aquamarine;
                }

            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                string orderNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[2]).ToString();
                string InvoiceNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[3]).ToString();
                string sqlWhere;

                if (this.xtraTabControl1.SelectedTabPage.Text == "OrderPro Information")
                {
                    sqlWhere = string.Format(" orderno='{0}' ", orderNo);
                    gridControl2.DataSource = bMCEOrderProInfo.GetList(sqlWhere).Tables[0];
                    this.gridView2.BestFitColumns();
                }
                else if (this.xtraTabControl1.SelectedTabPage.Text == "ShipmentPro Information")
                {
                    sqlWhere = string.Format("select ShipCatalogNo,ShipValCode,ShipSize,ShipUnit,ShipBatchNo,ShipNote,ShipLibraryID,CSShipmentInfo.StockStatus from dbo.CSShipProInfo,dbo.CSShipmentInfo where CSShipProInfo.TrackingNo = CSShipmentInfo.TrackingNo  and  CSShipmentInfo.OrderNo ='{0}' ", orderNo);
                    gridControl3.DataSource = DbHelperSQL.Query(sqlWhere).Tables[0];
                    this.gridView6.BestFitColumns();
                }
                else if (this.xtraTabControl1.SelectedTabPage.Text == "Shipment Information")
                {
                    sqlWhere = string.Format(" orderno='{0}' ", orderNo);
                    // gridControl4.DataSource = bMCEShipmentInfo.GetList(sqlWhere).Tables[0];
                    gridControl4.DataSource = DbHelperSQL.Query("select * from  CSShipmentInfo where orderno='" + orderNo + "'").Tables[0];
                    this.gridView9.BestFitColumns();
                }
                else if (this.xtraTabControl1.SelectedTabPage.Text == "Payment Information")
                {
                    sqlWhere = string.Format(" InvioceNo='{0}' ", InvoiceNo);
                    gridControl5.DataSource = bMCEPaymentInfo.GetList(sqlWhere).Tables[0];
                    this.gridView5.BestFitColumns();
                }
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var Path = @"c:\PDF\MCE\EU#Merge\";
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            var ftp = new FTPOperater();
            ftp.Server = "198.1.131.74";
            ftp.User = "HaoYuanFTP43";
            ftp.Pass = "Lmh20160929";
            ftp.Port = 21;
            if (e.Button.Caption == "下载")
            {
                if (ftp.GetFile("/PDF/MCE/USA#Merge/", gridView8.GetRowCellValue(gridView8.FocusedRowHandle, "listmodel").ToString(), Path, gridView8.GetRowCellValue(gridView8.FocusedRowHandle, "listmodel").ToString()))
                {
                    if (File.Exists(Path + gridView8.GetRowCellValue(gridView8.FocusedRowHandle, "listmodel").ToString()))
                    {
                        System.Diagnostics.Process.Start(Path + gridView8.GetRowCellValue(gridView8.FocusedRowHandle, "listmodel").ToString());
                    }
                    else
                    {
                        MessageDxUtil.ShowWarning(Path + gridView8.GetRowCellValue(gridView8.FocusedRowHandle, "listmodel").ToString() + " is not Exists! ");
                    }
                }
            }
        }

        public class ListModel
        {
            public string listmodel { get; set; }

        }
    }


}