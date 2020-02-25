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
using EuSoft.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
namespace Medical.Yottor.UI
{
    public partial class FrmTracking : DevExpress.XtraEditors.XtraForm
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
        EuSoft.Common.FedexStr cFedexStr = new EuSoft.Common.FedexStr();
        bool bStop=true;
        DataTable dtProInfo = new DataTable();
        public FrmTracking()
        {
            InitializeComponent();
            butSearch_Click(null, null);
            //this.gridView2.UpdateCurrentRow();
            //gridControl2.DataSource = getEmpDataTable();
            //this.gridView2.PopulateColumns();
        }

        public void getDate()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select top 30 convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo,InvioceNo,SalesCompany,PONumber,convert(varchar(20),ShipDate,101) as ShipDate,ShipVia,TrackingNo,MCEShipmentInfo.Note,MCEShipmentInfo.ShipmentStatus,MCEShipmentInfo.ID    from MCEOrderInfo   LEFT JOIN MCEShipmentInfo ON MCEShipmentInfo.OrderNo=MCEOrderInfo.OrderNo where ShipmentStatus ='Undelivered' and (TrackingNo is not null or TrackingNo='' )");
            sqlStr.Append(" order by MCEOrderInfo.orderno desc");
            dtProInfo = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtProInfo;
            //gridView1.PopulateColumns();
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtProInfo.Rows.Count.ToString();
            DataTable dtbMCEUnitDefinition = new DataTable();
            dtbMCEUnitDefinition = bMCEUnitDefinition.GetAllList().Tables[0];

          
            
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
            sqlStr.Append("select convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo,InvioceNo,SalesCompany,PONumber,convert(varchar(20),ShipDate,101) as ShipDate,ShipVia,TrackingNo,dbo.CSShipmentInfo.Note,dbo.CSShipmentInfo.ShipmentStatus,dbo.CSShipmentInfo.ID,dbo.CSShipmentInfo.StockStatus   from MCEOrderInfo   LEFT JOIN dbo.CSShipmentInfo ON dbo.CSShipmentInfo.OrderNo=MCEOrderInfo.OrderNo where  ShipmentStatus ='Undelivered' and (TrackingNo is not null or TrackingNo='' )");


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
                sqlStr.AppendFormat(" and exists (select * from dbo.CSShipProInfo where  procatalogno like '%" + txtCaNo.Text + "%' and MCEOrderInfo.orderno=orderno)");

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

            dtProInfo = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtProInfo;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtProInfo.Rows.Count.ToString();
            //   LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();

        }

        private void butAll_Click(object sender, EventArgs e)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select convert(varchar(20),OrderDate,101) as OrderDate,MCEOrderInfo.OrderNo,InvioceNo,SalesCompany,PONumber,convert(varchar(20),ShipDate,101) as ShipDate,ShipVia,TrackingNo,dbo.CSShipmentInfo.Note,dbo.CSShipmentInfo.ShipmentStatus,dbo.CSShipmentInfo.ID,dbo.CSShipmentInfo.StockStatus   from MCEOrderInfo   LEFT JOIN dbo.CSShipmentInfo ON dbo.CSShipmentInfo.OrderNo=MCEOrderInfo.OrderNo where 1=1 ");


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
                sqlStr.AppendFormat(" and exists (select * from dbo.CSShipProInfo where  procatalogno like '%" + txtCaNo.Text + "%' and MCEOrderInfo.orderno=orderno)");
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

            dtProInfo = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtProInfo;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtProInfo.Rows.Count.ToString();
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
        public DataTable GetTrackUPSDetailInfomation(string TrackNumber)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("date");
            dt.Columns.Add("description");
            dt.Columns.Add("city");
            dt.Columns.Add("countrycode");
            try
            {

                string jsonStr = "";
                jsonStr = jsonStr + "";
                jsonStr = jsonStr + "{";
                jsonStr = jsonStr + "\"UPSSecurity\":{";
                jsonStr = jsonStr + "\"UsernameToken\":{";
                jsonStr = jsonStr + "\"Username\":\"medchemexpress\",";
                jsonStr = jsonStr + "\"Password\":\"52MedChem\"";
                jsonStr = jsonStr + "},";
                jsonStr = jsonStr + "\"ServiceAccessToken\":{";
                jsonStr = jsonStr + "\"AccessLicenseNumber\":\"9D2BF7D7AF74C9FD\"";
                jsonStr = jsonStr + "}";
                jsonStr = jsonStr + "},";
                jsonStr = jsonStr + "\"TrackRequest\":{";
                jsonStr = jsonStr + "\"Request\":{";
                jsonStr = jsonStr + "\"RequestOption\":\"1\",";
                jsonStr = jsonStr + "\"TransactionReference\":{";
                jsonStr = jsonStr + "\"CustomerContext\":\"Your Test Case Summary Description\"";
                jsonStr = jsonStr + "}";
                jsonStr = jsonStr + "},";
                jsonStr = jsonStr + "\"InquiryNumber\":\"" + TrackNumber + "\"";
                jsonStr = jsonStr + "}";
                jsonStr = jsonStr + "}";

                string retJson = sendPost("https://onlinetools.ups.com/rest/Track", jsonStr);
                JObject jo = (JObject)JsonConvert.DeserializeObject(retJson);

                if (retJson.IndexOf("error") > 0)
                {
                    result = jo["Error"]["Description"].ToString();

                }
                if (retJson.IndexOf("Fault") > 0)
                {
                    result = jo["Fault"]["detail"]["Errors"]["ErrorDetail"]["PrimaryErrorCode"]["Description"].ToString();

                }

                var retStatues = jo["TrackResponse"]["Shipment"]["Package"]["Activity"];

                foreach (var item in retStatues)
                {

                    DataRow _drNew = dt.NewRow();
                    _drNew["date"] = ((JObject)item)["Date"] + " " + ((JObject)item)["Time"];
                    _drNew["description"] = ((JObject)item)["Status"]["Description"];
                    _drNew["city"] = ((JObject)item)["ActivityLocation"]["Address"]["City"];
                    _drNew["countrycode"] = ((JObject)item)["ActivityLocation"]["Address"]["CountryCode"];
                    dt.Rows.Add(_drNew);

                }

            }
            catch (Exception ex)
            {
                result = String.Format("查询失败！ 错误信息：{0} ", ex.Message);

            }

            return dt;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                clearT();
                //int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[10]).ToString());
                string orderNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[1]).ToString();
                txtShipDate.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipDate").ToString();
                txtShipVia.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipVia").ToString();
                txtTrk.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "TrackingNo").ToString();
                txtNote.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "Note").ToString();
                txtTPoNo.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "PONumber").ToString();

                //  DataTable dt = cFedexStr.GetTrackDetailInfomation(txtTrk.Text);

                DataTable dt = new DataTable();
                if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipVia").ToString().ToUpper() == "FEDEX")
                {
                    dt = cFedexStr.GetTrackDetailInfomation(txtTrk.Text);
                }
                else
                {
                    dt = GetTrackUPSDetailInfomation(txtTrk.Text);
                }






                //dtProInfo = bMCEOrderProInfo.GetList(" orderno='" + orderNo + "'").Tables[0];
                gridControl2.DataSource = dt;
                gridView2.BestFitColumns();

                //gridControl3.DataSource = bMCEShipProInfo.GetList(" TrackingNo='" + txtTrk.Text + "' ").Tables[0];
                gridControl3.DataSource = DbHelperSQL.Query(" select * from dbo.CSShipProInfo where TrackingNo='" + txtTrk.Text + "'").Tables[0];
                gridView5.BestFitColumns();

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
            
        }
        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //第一行  
            if (e.RowHandle > -1)
            {

                if (gridView1.GetRowCellValue(e.RowHandle, "ShipmentStatus").ToString() == "Delivered")
                    {
                        if (e.Column.ColumnHandle > 0)
                        e.Appearance.BackColor = Color.Lime;
                    }

              

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

    
    

     
        private void ClearTxt()
        {

            txtTrk.Text = "";
        }

  

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ID").ToString());
                if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipmentStatus") != null)
                {

                    if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipmentStatus").ToString() != string.Empty)
                    {



                        string ShipmentStatus = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipmentStatus").ToString() == "Delivered" ? "Undelivered" : "Delivered";
                        string sql = "";
                        if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "StockStatus").ToString().ToUpper() == "CA")
                        {
                            sql = "update MCEShipmentInfo set ShipmentStatus='" + ShipmentStatus + "' where id=" + id + "";
                        }
                        else
                        {
                            sql = "update BBUSA.dbo.MCEShipmentInfo set ShipmentStatus='" + ShipmentStatus + "' where id=" + id + "";
                        }



                        if (DbHelperSQL.ExecuteSql(sql) > 0)
                        {
                            DataRow[] selectedRows = dtProInfo.Select(" id='" + id + "'");
                            if (selectedRows != null && selectedRows.Length > 0)
                            {

                                selectedRows[0]["ShipmentStatus"] = ShipmentStatus;
                                dtProInfo.AcceptChanges();

                            }
                            
                            MessageDxUtil.ShowTips("success");

                        }
                    }

                }



            }
        }

       
        private void clearT()
        {
            txtShipDate.Text = "";
            txtShipVia.Text = "";
            txtTrk.Text = "";
            txtNote.Text = "";
            txtTPoNo.Text = "";
        }

        private void gridControl2_Click_1(object sender, EventArgs e)
        {
            if (this.gridView2.FocusedRowHandle > -1)
            {
              
            }
        }
        public static string sendPost(string postUrl, string postDataStr)
        {

            //用来存放cookie
            CookieContainer cookie = null;
            HttpWebRequest request = null;
            Stream myRequestStream = null;
            HttpWebResponse response = null;
            Stream myResponseStream = null;
            StreamReader myStreamReader = null;
            try
            {
                //转化
                byte[] byteArray = Encoding.UTF8.GetBytes(postDataStr);
                cookie = new CookieContainer();
                //发送一个POST请求
                request = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                request.CookieContainer = cookie;
                //request.Timeout = 3000;
                request.Method = "POST";
                //application/x-www-form-urlencoded
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                myRequestStream = request.GetRequestStream();
                myRequestStream.Write(byteArray, 0, byteArray.Length);
                myRequestStream.Close();
                //获取返回的内容
                response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookie.GetCookies(response.ResponseUri);
                myResponseStream = response.GetResponseStream();
                myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                return myStreamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine("postUrl = " + postUrl + "  Exception" + ex);
            }
            finally
            {
                if (myStreamReader != null)
                {
                    myStreamReader.Close();
                }
                if (myResponseStream != null)
                {
                    myResponseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }

            return "";
        }
        private void butUpdateNote_Click(object sender, EventArgs e)
        {
            string sql = "update MCEShipmentInfo set note='" + txtNote.Text + "' where TrackingNo='" + txtTrk.Text + "' ";
            if (DbHelperSQL.ExecuteSql(sql)>0)
            {
                DataRow[] selectedRows = dtProInfo.Select(" TrackingNo='" + txtTrk.Text + "'");
                if (selectedRows != null && selectedRows.Length > 0)
                {

                    selectedRows[0]["Note"] = txtNote.Text;
                    dtProInfo.AcceptChanges();

                }
                MessageDxUtil.ShowTips("success");
            }
        }
          public string GetTrackUPSD(string TrackNumber)
        {
            string result = string.Empty;
            try
            {

                string jsonStr = "";
                jsonStr = jsonStr + "";
                jsonStr = jsonStr + "{";
                jsonStr = jsonStr + "\"UPSSecurity\":{";
                jsonStr = jsonStr + "\"UsernameToken\":{";
                jsonStr = jsonStr + "\"Username\":\"medchemexpress\",";
                jsonStr = jsonStr + "\"Password\":\"52MedChem\"";
                jsonStr = jsonStr + "},";
                jsonStr = jsonStr + "\"ServiceAccessToken\":{";
                jsonStr = jsonStr + "\"AccessLicenseNumber\":\"9D2BF7D7AF74C9FD\"";
                jsonStr = jsonStr + "}";
                jsonStr = jsonStr + "},";
                jsonStr = jsonStr + "\"TrackRequest\":{";
                jsonStr = jsonStr + "\"Request\":{";
                jsonStr = jsonStr + "\"RequestOption\":\"1\",";
                jsonStr = jsonStr + "\"TransactionReference\":{";
                jsonStr = jsonStr + "\"CustomerContext\":\"Your Test Case Summary Description\"";
                jsonStr = jsonStr + "}";
                jsonStr = jsonStr + "},";
                jsonStr = jsonStr + "\"InquiryNumber\":\"" + TrackNumber + "\"";
                jsonStr = jsonStr + "}";
                jsonStr = jsonStr + "}";

                string retJson = sendPost("https://onlinetools.ups.com/rest/Track", jsonStr);
                JObject jo = (JObject)JsonConvert.DeserializeObject(retJson);

                if (retJson.IndexOf("error") > 0)
                {
                    result = jo["Error"]["Description"].ToString();

                }
                if (retJson.IndexOf("Fault") > 0)
                {
                    result = jo["Fault"]["detail"]["Errors"]["ErrorDetail"]["PrimaryErrorCode"]["Description"].ToString();

                }

                var retStatues = jo["TrackResponse"]["Shipment"]["Package"]["Activity"];

                foreach (var item in retStatues)
                {
                    result = ((JObject)item)["Status"]["Type"].ToString();

                    break;
                }


            }
            catch (Exception ex)
            {
                result = String.Format("查询失败！ 错误信息：{0} ", ex.Message);

            }

            return result;
        }



        private void butFedex_Click(object sender, EventArgs e)
        {
            string statues;
            string sql = "";
            bStop = true;
            for (int i = 0; i < dtProInfo.Rows.Count; i++)
            {
                if (bStop)
                {
                    statues = "";
                    LabRecords.Text = (i + 1).ToString() + "/" + dtProInfo.Rows.Count.ToString();
                    if (dtProInfo.Rows[i]["ShipVia"].ToString().ToUpper() == "FEDEX")
                    {
                        statues = cFedexStr.GetTrackStatusInfomation(dtProInfo.Rows[i]["TrackingNo"].ToString());
                    }
                    else
                    {
                        statues = GetTrackUPSD(dtProInfo.Rows[i]["TrackingNo"].ToString());
                    }



                    //  statues = cFedexStr.GetTrackStatusInfomation(dtProInfo.Rows[i]["TrackingNo"].ToString());
                    if (statues == "DL" || statues == "D")
                    {






                        if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "StockStatus").ToString().ToUpper() == "CA")
                        {
                            sql = "update MCEShipmentInfo set ShipmentStatus='Delivered' where id='" + dtProInfo.Rows[i]["ID"].ToString() + "' ";
                        }
                        else
                        {
                            sql = "update BBUSA.dbo.MCEShipmentInfo set ShipmentStatus='Delivered' where id='" + dtProInfo.Rows[i]["ID"].ToString() + "' ";
                            //sql = "update BBUSA.dbo.MCEShipmentInfo set ShipmentStatus='" + ShipmentStatus + "' where id=" + id + "";
                        }
                       





                        if (DbHelperSQL.ExecuteSql(sql) > 0)
                        {
                            dtProInfo.Rows[i]["ShipmentStatus"] = "Delivered";
                        }
                        //mMCEShipmentInfo.ID = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ID").ToString());
                        //mMCEShipmentInfo.ShipmentStatus = "Delivered";
                        //if (bMCEShipmentInfo.Update(mMCEShipmentInfo))
                        //{
                        //    LabRecords.Text = i.ToString() + "/" + dtProInfo.Rows.Count.ToString();
                        //    Application.DoEvents();
                        //    dtProInfo.Rows[i]["ShipmentStatus"] = "Delivered";
                        //}
                    }
                }

            }
            dtProInfo.AcceptChanges();
            MessageDxUtil.ShowTips("success"); 
            
        }

        private void butStop_Click(object sender, EventArgs e)
        {
            bStop = false;
        }

    


    

    

      
      

      

       

      
        }

      

    






    }
