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
using System.Diagnostics;
namespace Medical.Yottor.UI
{
    public partial class FrmPaymentInformation : DevExpress.XtraEditors.XtraForm
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
        EuSoft.BLL.MCEPaymentInfo bMCEPaymentInfo = new EuSoft.BLL.MCEPaymentInfo();
        EuSoft.Model.MCEPaymentInfo mMCEPaymentInfo = new EuSoft.Model.MCEPaymentInfo();
        DataTable dtOrderInfor = new DataTable();
        public FrmPaymentInformation()
        {
            InitializeComponent();
            butSearch_Click(null, null);
            //   getDate();
            //this.gridView2.UpdateCurrentRow();
            //gridControl2.DataSource = getEmpDataTable();
            //this.gridView2.PopulateColumns();

        }

        public void getDate()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo, BillCompany, PONumber, MCEOrderInfo.InvioceNo, InvoiceTotal,InvoiceDate, MCEOrderInfo.note,convert(varchar(20),ReceivedDate,101) as ReceivedDate,ReceivedAmount,BankingCost,Balance,Currency ,PayStatus,PayMethod,Terms,OrderStatus,MCEOrderInfo.ShipStatus,convert(varchar(20),b.ShipDate,101) as ShipDate,MCEOrderInfo.ID  from MCEOrderInfo   LEFT JOIN MCEPaymentInfo ON MCEPaymentInfo.InvioceNo = MCEOrderInfo.InvioceNo     LEFT JOIN (select OrderNo, note,shipdate ,ShipVia,TrackingNo ");
            sqlStr.Append("  from (select row_number()over(partition by OrderNo order by shipdate desc)rn, * from MCEShipmentInfo)t where t.rn=1  ) b ON b.OrderNo=MCEOrderInfo.OrderNo where PayStatus <>'Paid' ");


            sqlStr.Append(" order by MCEOrderInfo.orderno desc");



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
        }

        private void butSearch_Click(object sender, EventArgs e)
        {

            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append("select convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo, BillCompany, PONumber, MCEOrderInfo.InvioceNo, InvoiceTotal,InvoiceDate, MCEOrderInfo.note,convert(varchar(20),ReceivedDate,101) as ReceivedDate,ReceivedAmount,BankingCost,Balance,Currency ,PayStatus,PayMethod,Terms,OrderStatus,MCEOrderInfo.ShipStatus,convert(varchar(20),b.ShipDate,101) as ShipDate,MCEOrderInfo.ID  from MCEOrderInfo   LEFT JOIN MCEPaymentInfo ON MCEPaymentInfo.InvioceNo = MCEOrderInfo.InvioceNo    LEFT JOIN (select OrderNo, note,shipdate ,ShipVia,TrackingNo ");
            sqlStr.Append("  from (select row_number()over(partition by OrderNo order by shipdate desc)rn, * from MCEShipmentInfo)t where t.rn=1  ) b ON b.OrderNo=MCEOrderInfo.OrderNo where PayStatus ='Unpaid' and [OrderProcess] = 'Finalize' ");



            if (txtOrderNo.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtContactName.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and ( MCEOrderInfo.BillContactName  like '%{0}%' or MCEOrderInfo.ShipContactName  like '%{0}%' or MCEOrderInfo.SalesContactName  like '%{0}%' or MCEOrderInfo.[BilleMail] like '%{0}%' ) ", txtContactName.Text);
            }
            if (txtCompany.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.[BillCompany] like '%{0}%'  ", txtCompany.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr.AppendFormat(" and  PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }

            if (txtDate1.Text != "")
            {
                sqlStr.AppendFormat(" and  MCEOrderInfo.OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlStr.AppendFormat(" and  MCEOrderInfo.OrderDate<='{0}'", txtDate2.Text);
            }
            sqlStr.Append(" order by MCEOrderInfo.orderno desc");

            dtOrderInfor = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtOrderInfor;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtOrderInfor.Rows.Count.ToString();
            //   LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();

        }

        private void butAll_Click(object sender, EventArgs e)
        {
            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append("select convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo, BillCompany, PONumber, MCEOrderInfo.InvioceNo, InvoiceTotal,InvoiceDate, MCEOrderInfo.note,convert(varchar(20),ReceivedDate,101) as ReceivedDate,ReceivedAmount,BankingCost,Balance,Currency ,PayStatus,PayMethod,Terms,OrderStatus,MCEOrderInfo.ShipStatus,convert(varchar(20),b.ShipDate,101) as ShipDate,MCEOrderInfo.ID  from MCEOrderInfo   LEFT JOIN MCEPaymentInfo ON MCEPaymentInfo.InvioceNo = MCEOrderInfo.InvioceNo    LEFT JOIN (select OrderNo, note,shipdate ,ShipVia,TrackingNo ");
            sqlStr.Append("  from (select row_number()over(partition by OrderNo order by shipdate desc)rn, * from MCEShipmentInfo)t where t.rn=1  ) b ON b.OrderNo=MCEOrderInfo.OrderNo where 1=1  and [OrderProcess] = 'Finalize' ");



            if (txtOrderNo.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtContactName.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and ( MCEOrderInfo.BillContactName  like '%{0}%' or MCEOrderInfo.ShipContactName  like '%{0}%' or MCEOrderInfo.SalesContactName  like '%{0}%' or MCEOrderInfo.[BilleMail] like '%{0}%' ) ", txtContactName.Text);
            }
            if (txtCompany.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.[BillCompany] like '%{0}%'  ", txtCompany.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr.AppendFormat(" and  PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }



            if (txtDate1.Text != "")
            {
                sqlStr.AppendFormat(" and  MCEOrderInfo.OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlStr.AppendFormat(" and  MCEOrderInfo.OrderDate<='{0}'", txtDate2.Text);
            }
            sqlStr.Append(" order by MCEOrderInfo.orderno desc");

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
                string InvioceNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "InvioceNo").ToString();
                txtOrderDate.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "OrderDate").ToString();
                txtOrderNoV.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "OrderNo").ToString();

                txtBCompany.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "BillCompany").ToString();
                txtPoNumber.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "PONumber").ToString();
                txtInvioceV.Text = InvioceNo;
                txtInvoiceTotal.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "InvoiceTotal").ToString();

                txtOrderNote.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "note").ToString();
                txtPayMetod.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "PayMethod").ToString();
                txtPayStatus.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "PayStatus").ToString();
                this.gridControl2.DataSource = bMCEPaymentInfo.GetList("InvioceNo='" + InvioceNo + "'").Tables[0];
                this.gridView2.BestFitColumns();
            }
        }


        private void getShipDate(string orderNo)
        {
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
                if (gridView1.GetRowCellValue(e.RowHandle, "PayStatus").ToString() == "Paid")
                {
                    if (e.Column.ColumnHandle > 0)
                    {
                        e.Appearance.BackColor = Color.Lime;
                    }
                }

                if (gridView1.GetRowCellValue(e.RowHandle, "ReceivedDate").ToString() != "")
                {
                    if (e.Column.ColumnHandle == 0)
                    {
                        e.Appearance.BackColor = Color.SkyBlue;
                    }
                }

                if (gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString() == "Cancel")
                {
                    if (e.Column.ColumnHandle > 0)
                    {
                        e.Appearance.BackColor = Color.LightSlateGray;
                    }

                }

                if (gridView1.GetRowCellValue(e.RowHandle, "ShipDate").ToString() != "")
                {
                    if (gridView1.GetRowCellValue(e.RowHandle, "PayStatus").ToString() == "Unpaid" && gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString() != "Cancel" && gridView1.GetRowCellValue(e.RowHandle, "ShipStatus").ToString() == "Shipped")
                    {
                        if (DateTime.Now > Convert.ToDateTime(gridView1.GetRowCellValue(e.RowHandle, "ShipDate").ToString()).AddDays(Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "Terms"))))
                        {
                            if (e.Column.ColumnHandle > 0)
                            {
                                e.Appearance.BackColor = Color.Red;
                                e.Appearance.BackColor2 = Color.Brown;
                            }
                        }

                    }

                }

                if (gridView1.GetRowCellValue(e.RowHandle, "PayStatus").ToString() == "Cancel")
                {
                    if (e.Column.ColumnHandle > 0)
                    {
                        e.Appearance.BackColor = Color.LightSlateGray;
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

                string orderNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "OrderNo").ToString();
                string OrderStatus = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "PayStatus").ToString() == "Unpaid" ? "Paid" : "Unpaid";
                //  mMCEOrderInfo.OrderStatus = OrderStatus;
                string sql = "update  MCEOrderInfo set  PayStatus='" + OrderStatus + "' where orderNo='" + orderNo + "' ";
                if (DbHelperSQL.ExecuteSql(sql) > 0)
                {

                    DataRow[] selectedRows = dtOrderInfor.Select(" orderNo='" + orderNo + "'");
                    if (selectedRows != null && selectedRows.Length > 0)
                    {
                        for (int i = 0; i < selectedRows.Length; i++)
                        {
                            selectedRows[i]["PayStatus"] = OrderStatus;
                            txtPayStatus.Text = OrderStatus;
                        }

                        dtOrderInfor.AcceptChanges();

                    }

                    MessageDxUtil.ShowTips("success");
                }
            }
        }



        private void butOverdue_Click(object sender, EventArgs e)
        {
            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append("select convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo, BillCompany, PONumber, MCEOrderInfo.InvioceNo, InvoiceTotal,InvoiceDate, MCEOrderInfo.note,convert(varchar(20),ReceivedDate,101) as ReceivedDate,ReceivedAmount,BankingCost,Balance,Currency ,PayStatus,PayMethod,Terms,OrderStatus,MCEOrderInfo.ShipStatus,convert(varchar(20),b.ShipDate,101) as ShipDate,MCEOrderInfo.ID  from MCEOrderInfo   LEFT JOIN MCEPaymentInfo ON MCEPaymentInfo.InvioceNo = MCEOrderInfo.InvioceNo    LEFT JOIN (select OrderNo, note,shipdate ,ShipVia,TrackingNo ");
            sqlStr.Append(" from (select row_number()over(partition by OrderNo order by shipdate desc)rn, * from MCEShipmentInfo)t where t.rn=1  ) b ON b.OrderNo=MCEOrderInfo.OrderNo where   MCEOrderInfo.OrderStatus<>'Cancel' and PayStatus='Unpaid' and ShipDate is not null and  ShipStatus='Shipped' and getdate()>DateAdd(D,cast(Terms as int), cast(ShipDate as datetime)) and [OrderProcess] = 'Finalize' ");

            if (txtOrderNo.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtContactName.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and ( MCEOrderInfo.BillContactName  like '%{0}%' or MCEOrderInfo.ShipContactName  like '%{0}%' or MCEOrderInfo.SalesContactName  like '%{0}%' or MCEOrderInfo.[BilleMail] like '%{0}%' ) ", txtContactName.Text);
            }
            if (txtCompany.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.[BillCompany] like '%{0}%'  ", txtCompany.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr.AppendFormat(" and  PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr.AppendFormat(" and MCEOrderInfo.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }



            if (txtDate1.Text != "")
            {
                sqlStr.AppendFormat(" and  MCEOrderInfo.OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlStr.AppendFormat(" and  MCEOrderInfo.OrderDate<='{0}'", txtDate2.Text);
            }
            sqlStr.Append(" order by MCEOrderInfo.orderno desc");

            dtOrderInfor = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtOrderInfor;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtOrderInfor.Rows.Count.ToString();
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle > -1)
            {
                if (txtMReasons.Text == string.Empty)
                {
                    MessageDxUtil.ShowWarning("Please input the txtMReasons.");
                    txtMReasons.Focus();
                    return;
                }
                if (MessageDxUtil.ShowYesNoAndWarning("Delete?") == DialogResult.Yes)
                {
                    string sqlu = "insert into MCEPaymentInfoChangeRe([InvioceNo],[ReceivedDate],[ReceivedAmount],[BankingCost],[Balance],[Currency],[Note],[UpdateTime],[Person],[ChangeNote] ,[ChangePerson],[ChangeTime],[Reasons],[Cardinfo]) select  [InvioceNo],[ReceivedDate],[ReceivedAmount],[BankingCost],[Balance],[Currency],[Note],[UpdateTime],[Person],note ,'" + Properties.Settings.Default.LastUser + "',getdate(),'" + txtMReasons.Text + "',[Cardinfo] from MCEPaymentInfo where id=" + labelControl1.Text + "";
                    if (DbHelperSQL.ExecuteSql(sqlu) > 0)
                    {

                    }

                    if (bMCEPaymentInfo.Delete(Convert.ToInt32(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID").ToString())))
                    {
                        this.gridControl2.DataSource = bMCEPaymentInfo.GetList("InvioceNo='" + txtInvioceV.Text + "'").Tables[0];
                        this.gridView2.BestFitColumns();
                        MessageDxUtil.ShowTips("success");
                    }
                }

            }
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            if (txtRdate.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the received date.");
                txtRdate.Focus();
                return;
            }
            if (txtInvioceV.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the Invioce No.");
                txtInvioceV.Focus();
                return;
            }
            if (txtRAmount.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the received amount.");
                txtRAmount.Focus();
                return;
            }
            if (txtPayMetod.Text == "Credit Card" && txtCardInfo.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the Credit Card information(Ending 4 Numbers).");
                txtCardInfo.Focus();
                return;
            }
            if (txtBCost.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the banking cost.");
                txtBCost.Focus();
                return;
            }
            if (txtCurrency.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the currency.");
                txtCurrency.Focus();
                return;
            }
            if (MessageDxUtil.ShowYesNoAndWarning("Add PaymentInfo  to Invioce#" + txtInvioceV.Text + " ?") == DialogResult.Yes)
            {
                txtBalance.Text = Convert.ToString(Convert.ToDecimal(txtRAmount.Text) - Convert.ToDecimal(txtBCost.Text));
                mMCEPaymentInfo.Balance = txtBalance.Text;
                mMCEPaymentInfo.BankingCost = Convert.ToDecimal(txtBCost.Text);
                mMCEPaymentInfo.Currency = txtCurrency.Text;
                mMCEPaymentInfo.InvioceNo = txtInvioceV.Text;
                mMCEPaymentInfo.ReceivedDate = Convert.ToDateTime(txtRdate.Text);
                mMCEPaymentInfo.ReceivedAmount = Convert.ToDecimal(txtRAmount.Text);
                mMCEPaymentInfo.Note = txtNote.Text;
                mMCEPaymentInfo.Cardinfo = txtCardInfo.Text;
                mMCEPaymentInfo.UpdateTime = DateTime.Now;
                mMCEPaymentInfo.Person = Properties.Settings.Default.LastUser;
                if (bMCEPaymentInfo.Add(mMCEPaymentInfo) > 0)
                {
                    this.gridControl2.DataSource = bMCEPaymentInfo.GetList("InvioceNo='" + txtInvioceV.Text + "'").Tables[0];
                    this.gridView2.BestFitColumns();
                    MessageDxUtil.ShowTips("success");
                }

            }

        }

        private void butExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.FileName = "Order Payment.xlsx";
            saveFileDialog.Title = "Excel";
            saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                this.gridControl1.ExportToXlsx(saveFileDialog.FileName);
                if (MessageDxUtil.ShowYesNoAndTips("success,open？") == DialogResult.Yes)
                {
                    Process.Start(saveFileDialog.FileName);
                }
            }
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {
                int id = Convert.ToInt16(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID").ToString());
                labelControl1.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID").ToString();
                mMCEPaymentInfo = bMCEPaymentInfo.GetModel(id);
                if (mMCEPaymentInfo != null)
                {
                    txtBalance.Text = mMCEPaymentInfo.Balance;
                    txtBCost.Text = mMCEPaymentInfo.BankingCost.ToString();
                    txtCurrency.Text = mMCEPaymentInfo.Currency;
                    txtInvioceV.Text = mMCEPaymentInfo.InvioceNo;
                    txtRdate.Text = mMCEPaymentInfo.ReceivedDate.ToString();
                    txtRAmount.Text = mMCEPaymentInfo.ReceivedAmount.ToString();
                    txtNote.Text = mMCEPaymentInfo.Note;
                    txtCardInfo.Text = mMCEPaymentInfo.Cardinfo;
                    txtBalance.Text = Convert.ToString(Convert.ToDecimal(txtRAmount.Text) - Convert.ToDecimal(txtBCost.Text));
                }

            }
        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            if (txtMReasons.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the txtMReasons.");
                txtMReasons.Focus();
                return;
            }

            if (txtRdate.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the received date.");
                txtRdate.Focus();
                return;
            }
            if (txtInvioceV.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the Invioce No.");
                txtInvioceV.Focus();
                return;
            }
            if (txtRAmount.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the received amount.");
                txtRAmount.Focus();
                return;
            }
            if (txtPayMetod.Text == "Credit Card" && txtCardInfo.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the Credit Card information(Ending 4 Numbers).");
                txtCardInfo.Focus();
                return;
            }
            if (txtBCost.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the banking cost.");
                txtBCost.Focus();
                return;
            }
            if (txtCurrency.Text == string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the currency.");
                txtCurrency.Focus();
                return;
            }
            if (MessageDxUtil.ShowYesNoAndWarning("update PaymentInfo  to ID#" + labelControl1.Text + " ?") == DialogResult.Yes)
            {
                string sqlu = "insert into MCEPaymentInfoChangeRe([InvioceNo],[ReceivedDate],[ReceivedAmount],[BankingCost],[Balance],[Currency],[Note],[UpdateTime],[Person],[ChangeNote] ,[ChangePerson],[ChangeTime],[Reasons],[Cardinfo]) select  [InvioceNo],[ReceivedDate],[ReceivedAmount],[BankingCost],[Balance],[Currency],[Note],[UpdateTime],[Person],note ,'" + Properties.Settings.Default.LastUser + "',getdate(),'" + txtMReasons.Text + "',[Cardinfo] from MCEPaymentInfo where id=" + labelControl1.Text + "";
                if (DbHelperSQL.ExecuteSql(sqlu) > 0)
                {

                }

                txtBalance.Text = Convert.ToString(Convert.ToDecimal(txtRAmount.Text) - Convert.ToDecimal(txtBCost.Text));
                mMCEPaymentInfo.Balance = txtBalance.Text;
                mMCEPaymentInfo.BankingCost = Convert.ToDecimal(txtBCost.Text);
                mMCEPaymentInfo.Currency = txtCurrency.Text;
                mMCEPaymentInfo.InvioceNo = txtInvioceV.Text;
                mMCEPaymentInfo.ReceivedDate = Convert.ToDateTime(txtRdate.Text);
                mMCEPaymentInfo.ReceivedAmount = Convert.ToDecimal(txtRAmount.Text);
                mMCEPaymentInfo.Note = txtNote.Text;
                mMCEPaymentInfo.Cardinfo = txtCardInfo.Text;
                mMCEPaymentInfo.UpdateTime = DateTime.Now;
                mMCEPaymentInfo.Person = Properties.Settings.Default.LastUser;
                mMCEPaymentInfo.ID = Convert.ToInt16(labelControl1.Text);
                if (bMCEPaymentInfo.Update(mMCEPaymentInfo))
                {
                    this.gridControl2.DataSource = bMCEPaymentInfo.GetList("InvioceNo='" + txtInvioceV.Text + "'").Tables[0];
                    this.gridView2.BestFitColumns();
                    MessageDxUtil.ShowTips("success");
                }

            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndWarning(txtPayStatus.Text + " PaymentInfo  to Invioce#" + txtInvioceV.Text + " ?") == DialogResult.Yes)
            {
                if (butCancel.Text == "Cancel")
                {
                    var sql = @" INSERT INTO [dbo].[CancelPayCustomersInfo]
           ([OrderNo],[SalesCompany],[SalesContactName],[SalesStreet]
           ,[SalesCity],[SalesState],[SalesZip],[SalesCountry],[SalesTel]
           ,[SalesFax],[SaleseMail],[BillCompany],[BillContactName],[BillStreet]
           ,[BillCity],[BillState],[BillZip]
           ,[BillCountry],[BillTel],[BillFax],[BilleMail],[ShipCompany],[ShipContactName],[ShipStreet]
           ,[ShipCity],[ShipState],[ShipZip],[ShipCountry],[ShipTel],[ShipFax],[ShipeMail]
           ,[InvioceNo],[VendorCode],[Terms],[PayMethod],[OrderDate],[PONumber]
           ,[CustomerRefNo],[SH],[Comments],[Note],[OrderStatus],[OrderProcess],[CustomerID],[InvoiceDate],[InvoiceTotal],[PayStatus]
           ,[ShipStatus],[Carrier],[AccountNo],[UpdateTime],[SendConfirmDate],[SendNewDate],[Person])
           select [OrderNo],[SalesCompany],[SalesContactName],[SalesStreet]
           ,[SalesCity],[SalesState],[SalesZip],[SalesCountry],[SalesTel]
           ,[SalesFax],[SaleseMail],[BillCompany],[BillContactName],[BillStreet]
           ,[BillCity],[BillState],[BillZip]
           ,[BillCountry],[BillTel],[BillFax],[BilleMail],[ShipCompany],[ShipContactName],[ShipStreet]
           ,[ShipCity],[ShipState],[ShipZip],[ShipCountry],[ShipTel],[ShipFax],[ShipeMail]
           ,[InvioceNo],[VendorCode],[Terms],[PayMethod],[OrderDate],[PONumber]
           ,[CustomerRefNo],[SH],[Comments],[Note],[OrderStatus],[OrderProcess],[CustomerID],[InvoiceDate],[InvoiceTotal],[PayStatus]
           ,[ShipStatus],[Carrier],[AccountNo],[UpdateTime],[SendConfirmDate],[SendNewDate],'" + Properties.Settings.Default.LastUser + "' from MCEOrderInfo where [InvioceNo]='" + txtInvioceV.Text + "'";

                    var sql1 = "update  MCEOrderInfo set  PayStatus='Cancel' where orderNo='" + txtOrderNoV.Text + "' ";

                    var listSql = new List<string>();
                    listSql.Add(sql);
                    listSql.Add(sql1);

                    if (DbHelperSQL.ExecuteSqlTran(listSql) > 0)
                    {
                        var selectedRows = dtOrderInfor.Select(" orderNo='" + txtOrderNoV.Text + "'");
                        if (selectedRows != null && selectedRows.Length > 0)
                        {
                            for (var i = 0; i < selectedRows.Length; i++)
                            {
                                selectedRows[i]["PayStatus"] = "Cancel";
                                txtPayStatus.Text = "Cancel";
                                butCancel.Text = "Recovery";
                            }

                            dtOrderInfor.AcceptChanges();
                        }

                        MessageDxUtil.ShowTips("success");
                    }
                }
                else
                {
                    string sql = @"DELETE [CancelPayCustomersInfo] WHERE [OrderNo] ='" + txtOrderNoV.Text + "' ";
                    string sql1 = "UPDATE MCEOrderInfo SET PayStatus='Unpaid' WHERE orderNo='" + txtOrderNoV.Text + "' ";

                    var listSql = new List<string>();
                    listSql.Add(sql);
                    listSql.Add(sql1);

                    if (DbHelperSQL.ExecuteSqlTran(listSql) > 0)
                    {
                        var selectedRows = dtOrderInfor.Select(" orderNo='" + txtOrderNoV.Text + "'");
                        if (selectedRows != null && selectedRows.Length > 0)
                        {
                            for (var i = 0; i < selectedRows.Length; i++)
                            {
                                selectedRows[i]["PayStatus"] = "Unpaid";
                                txtPayStatus.Text = "Unpaid";
                                butCancel.Text = "Cancel";
                            }

                            dtOrderInfor.AcceptChanges();
                        }
                        txtPayStatus.Text = "Unpaid";
                        MessageDxUtil.ShowTips("success");
                    }
                }

            }


        }




    }
}