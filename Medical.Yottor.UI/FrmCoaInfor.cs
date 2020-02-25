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
using System.IO;
using System.Diagnostics;
namespace Medical.Yottor.UI
{
    public partial class FrmCoaInfor : DevExpress.XtraEditors.XtraForm
    {
        EuSoft.BLL.ProductsCoaRecord bProductsCoaRecord = new EuSoft.BLL.ProductsCoaRecord();
        EuSoft.Model.ProductsCoaRecord mProductsCoaRecord = new EuSoft.Model.ProductsCoaRecord();
        EuSoft.BLL.MCEShipProInfo bMCEShipProInfo = new EuSoft.BLL.MCEShipProInfo();
        EuSoft.BLL.PDFDownloadRecord bPDFDownloadRecord = new EuSoft.BLL.PDFDownloadRecord();


        DataTable dtProInfo = new DataTable();
        public FrmCoaInfor()
        {
            InitializeComponent();
            butSearch_Click(null,null);
            //getDate();
            //this.gridView2.UpdateCurrentRow();
            //gridControl2.DataSource = getEmpDataTable();
            //this.gridView2.PopulateColumns();
        }

        public void getDate()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select top 30 B.OrderNo,B.TrackingNo,ShipCatalogNo,ShipValCode,ShipBatchNo,c.RetesDate,a.InvioceNo,c.MergePDFName from MCEShipProInfo b  left join MCEOrderInfo a on  a.OrderNo=b.OrderNo  LEFT JOIN ProductsCoaRecord c ON b.ShipBatchNo=c.TrackingNO    where (c.RetesDate <DateAdd(day,1,getdate()) or c.RetesDate is null) and  a.[OrderProcess] = 'Finalize'");
            sqlStr.Append(" order by b.orderno desc");
            dtProInfo = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtProInfo;
            //gridView1.PopulateColumns();
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtProInfo.Rows.Count.ToString();




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
            sqlStr.Append("select B.TrackingNo,ShipCatalogNo,ShipValCode,ShipBatchNo,c.RetesDate,a.InvioceNo,c.MergePDFName from csShipProInfo b  left join MCEOrderInfo a on  a.OrderNo=b.OrderNo  LEFT JOIN BBUSAtest..ProductsCoaRecord c ON RIGHT('00000000'+CAST(b.ShipBatchNo as varchar(10)),5)=c.TrackingNO  and ShipCatalogNo=CatalogNo     where 1=1 and b.stockstatus='' and (c.RetesDate <DateAdd(m,1,getdate()) or c.RetesDate is null) ");


            if (txtOrderNo.Text != "")
            {
                sqlStr.AppendFormat(" and b.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtTracking.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and b.TrackingNo like '%{0}%' ", txtTracking.Text);
            }

            if (txtInvoiceNo.Text != "")
            {
                sqlStr.AppendFormat(" and a.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }

            if (txtCaNo.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and  ShipCatalogNo like '%" + txtCaNo.Text + "%' ");

            }
            if (txtValCode.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and ShipValCode like '%{0}%'  ", txtValCode.Text);
            }

            if (txtDate1.Text != "")
            {
                sqlStr.AppendFormat(" and  OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlStr.AppendFormat(" and  OrderDate<='{0}'", txtDate2.Text);
            }
            sqlStr.Append(" order by b.orderno desc");

            dtProInfo = DbHelperSQL.Query(sqlStr.ToString()).Tables[0];
            gridControl1.DataSource = dtProInfo;
            this.gridView1.BestFitColumns();
            LabRecords.Text = "Records:" + dtProInfo.Rows.Count.ToString();
            //   LabInvoiceTotal.Text = "                Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "").ToString();

        }

        private void butAll_Click(object sender, EventArgs e)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select B.TrackingNo,ShipCatalogNo,ShipValCode,ShipBatchNo,c.RetesDate,a.InvioceNo,c.MergePDFName from csShipProInfo b  left join MCEOrderInfo a on  a.OrderNo=b.OrderNo  LEFT JOIN BBUSAtest..ProductsCoaRecord c ON RIGHT('00000000'+CAST(b.ShipBatchNo as varchar(10)),5)=c.TrackingNO  and ShipCatalogNo=CatalogNo    where 1=1 and b.stockstatus='' and a.[OrderProcess] = 'Finalize'");


            if (txtOrderNo.Text != "")
            {
                sqlStr.AppendFormat(" and b.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtTracking.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and b.TrackingNo like '%{0}%' ", txtTracking.Text);
            }

            if (txtInvoiceNo.Text != "")
            {
                sqlStr.AppendFormat(" and a.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }

            if (txtCaNo.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and  ShipCatalogNo like '%" + txtCaNo.Text + "%' ");

            }
            if (txtValCode.Text != string.Empty)
            {
                sqlStr.AppendFormat(" and ShipValCode like '%{0}%'  ", txtValCode.Text);
            }

            if (txtDate1.Text != "")
            {
                sqlStr.AppendFormat(" and  OrderDate>='{0}' ", txtDate1.Text);
            }

            if (txtDate2.Text != "")
            {
                sqlStr.AppendFormat(" and  OrderDate<='{0}'", txtDate2.Text);
            }
            sqlStr.Append(" order by b.orderno desc");

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

            txtOrderNo.Text = "";
            txtValCode.Text = "";
            txtTracking.Text = "";
            txtCaNo.Text = "";
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                clearT();

                labInvoice.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "InvioceNo").ToString();
                txtTra.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "TrackingNo").ToString();
                //int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[10]).ToString());
                //string orderNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[1]).ToString();
                //txtShipDate.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipDate").ToString();
                //txtShipVia.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ShipVia").ToString();
                //txtTrk.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "TrackingNo").ToString();
                //txtNote.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "Note").ToString();
                //txtTPoNo.Text = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "PONumber").ToString();

                //DataTable dt = cFedexStr.GetTrackDetailInfomation(txtTrk.Text);

                ////dtProInfo = bMCEOrderProInfo.GetList(" orderno='" + orderNo + "'").Tables[0];
                //gridControl2.DataSource = dt;
                //gridView2.BestFitColumns();

                //gridControl3.DataSource = bMCEShipProInfo.GetList(" TrackingNo='" + txtTrk.Text + "' ").Tables[0];
                //gridView5.BestFitColumns();

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


        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {


            //////第一行  
            //if (e.RowHandle > -1)
            //{
            //    if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "RetesDate").ToString() != "")
            //    {
            //        if (Convert.ToDateTime(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "RetesDate").ToString()) > DateTime.Now.AddDays(1))
            //        {
            //            if (e.Column.ColumnHandle>0)
            //            {
            //                e.Appearance.BackColor = Color.Lime;
            //            }

            //        }



            //        if (Convert.ToDateTime(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "RetesDate").ToString()) < DateTime.Now.AddDays(1))
            //        {
            //            if (e.Column.ColumnHandle > 0)
            //            {
            //                e.Appearance.BackColor = Color.LightSlateGray;
            //            }

            //        }
            //    }



            //}

         
        }





        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            ////第一行  
            if (e.RowHandle > -1)
            {
                if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "RetesDate").ToString() != "")
                {
                    if (Convert.ToDateTime(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "RetesDate").ToString()) > DateTime.Now.AddDays(1))
                    {

                        e.Appearance.BackColor = Color.Lime;
                        //e.Appearance.BackColor = Color.Aqua;
                    }



                    if (Convert.ToDateTime(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "RetesDate").ToString()) < DateTime.Now.AddDays(1))
                    {

                        //e.Appearance.BackColor2 = Color.LightSteelBlue;
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





        private void ClearTxt()
        {

            txtBNo.Text = "";
        }



     


        private void clearT()
        {

            txtBNo.Text = "";

            labInvoice.Text = "";
        }

        private void gridControl2_Click_1(object sender, EventArgs e)
        {
            if (this.gridView2.FocusedRowHandle > -1)
            {

            }
        }





       
        private void butS_Click(object sender, EventArgs e)
        {
            string sql = " 1=1";
            if (txtCa.Text != string.Empty)
            {
                sql += "  and CatalogNo like '%" + txtCa.Text + "%'";
            }
            if (txtBNo.Text != string.Empty)
            {
                sql += " and TrackingNo like '%" + txtBNo.Text + "%'";
            }
            this.gridControl2.DataSource = bProductsCoaRecord.GetList(sql).Tables[0];
            this.gridView2.BestFitColumns();
        }

        private void butVCoa_Click(object sender, EventArgs e)
        {

        


            if (gridView1.FocusedRowHandle > -1)
            {
                //if (File.Exists(Path + this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString()))
                //    System.Diagnostics.Process.Start(Path + this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString());
                //else
                //{
                var Path = @"c:\PDF\CS\EU#Merge\";
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);  

                    FTPOperater ftp = new FTPOperater();
                    ftp.Server = "34.207.148.81";
                    ftp.User = "HaoYuanFTP43";
                    ftp.Pass = "Lmh20160929";
                    ftp.Port = 2143;
                    if (ftp.GetFile("/PDF/CS/USA#Merge/", this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString(), Path, this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString()))
                    {
                        if (File.Exists(Path + this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString()))
                            System.Diagnostics.Process.Start(Path + this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString());
                        else
                            MessageDxUtil.ShowWarning(Path + this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString() + " is not Exists! ");

                    }


                //}
            }
        }

        private void butPCoa_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                var Path = @"c:\PDF\CS\EU#Merge\";
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);


                FTPOperater ftp = new FTPOperater();
                ftp.Server = "34.207.148.81";
                ftp.User = "HaoYuanFTP43";
                ftp.Pass = "Lmh20160929";
                ftp.Port = 2143;
                if (ftp.GetFile("/PDF/CS/USA#Merge/", this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString(), Path, this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString()))
                {
                   

                    Process pr = new Process();

                    pr.StartInfo.FileName = Path + this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString(); ;//文件全称-包括文件后缀

                    pr.StartInfo.CreateNoWindow = true;

                    pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    pr.StartInfo.Verb = "Print";

                    pr.Start();
                }
                else
                    MessageDxUtil.ShowWarning(Path + this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString() + " is not Exists! ");
            }

        }

        private void txtTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtAction.Text = "";
            if (txtTra.Text != string.Empty && e.KeyChar == 13)
            {
                if (!checkAuto.Checked)
                {
                    MessageDxUtil.ShowWarning("Please select the automatic print option!.");
                    return;
                }
                var Path = @"c:\PDF\CS\EU#Merge\";
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);  
                string tra = txtTra.Text.Length > 12 ? txtTra.Text.Substring(0, 12) : txtTra.Text;
                DataTable dt = new DataTable();
                DataTable dtPdf = new DataTable();
                dt = bMCEShipProInfo.GetList(" TrackingNo='" + tra + "' ").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dtPdf = bProductsCoaRecord.GetList(" [TrackingNO] = '" + dt.Rows[i]["ShipBatchNo"] + "' order by [ID] Desc ").Tables[0];
                        if (dtPdf.Rows.Count > 0)
                        {
                            FTPOperater ftp = new FTPOperater();
                            ftp.Server = "34.207.148.81";
                            ftp.User = "HaoYuanFTP43";
                            ftp.Pass = "Lmh20160929";
                            ftp.Port = 2143;
                            if (ftp.GetFile("/PDF/cs/USA#Merge/", dtPdf.Rows[0]["MergePDFName"].ToString(), Path, this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString()))
                            {

                                if (File.Exists(Path + dtPdf.Rows[0]["MergePDFName"].ToString()))
                                {
                                    Application.DoEvents();
                                    txtAction.Text = "Now COA (" + dtPdf.Rows[0]["MergePDFName"].ToString() + ") is found." + "\r\n" + txtAction.Text;
                                    System.Diagnostics.Process.Start(Path + dtPdf.Rows[0]["MergePDFName"].ToString());

                                    Process pr = new Process();

                                    pr.StartInfo.FileName = Path + dtPdf.Rows[0]["MergePDFName"].ToString();

                                    pr.StartInfo.CreateNoWindow = true;

                                    pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                                    pr.StartInfo.Verb = "Print";

                                    pr.Start();
                                }

                                else
                                {
                                    Application.DoEvents();
                                    txtAction.Text = "Now COA (" + dtPdf.Rows[0]["MergePDFName"].ToString() + ") is not found." + "\r\n" + txtAction.Text;

                                }
                            }
                        }
                        else
                        {
                            Application.DoEvents();
                            txtAction.Text = "Now COA  is found." + "\r\n" + txtAction.Text;

                        }
                    }
                }
            }
        }

        private void txtVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtAction.Text = "";
            if (txtVal.Text != string.Empty && e.KeyChar == 13)
            {
                if (!checkAuto.Checked)
                {
                    MessageDxUtil.ShowWarning("Please select the automatic print option!.");
                    return;
                }
                  var Path = @"c:\PDF\cs\EU#Merge\";
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);
                DataTable dt = new DataTable();
                DataTable dtPdf = new DataTable();
                dt = bMCEShipProInfo.GetList(" ShipValCode='" + txtVal.Text + "' ").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dtPdf = bProductsCoaRecord.GetList(" [TrackingNO] = '" + dt.Rows[i]["ShipBatchNo"] + "' order by [ID] Desc ").Tables[0];
                        if (dtPdf.Rows.Count > 0)
                        {
                            FTPOperater ftp = new FTPOperater();
                            ftp.Server = "34.207.148.81";
                            ftp.User = "HaoYuanFTP43";
                            ftp.Pass = "Lmh20160929";
                            ftp.Port = 2143;
                            if (ftp.GetFile("/PDF/cs/USA#Merge/", dtPdf.Rows[0]["MergePDFName"].ToString(), Path, this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "MergePDFName").ToString()))
                            {
                                if (File.Exists(Path + dtPdf.Rows[0]["MergePDFName"].ToString()))
                                {
                                    Application.DoEvents();
                                    txtAction.Text = "Now COA (" + dtPdf.Rows[0]["MergePDFName"].ToString() + ") is found." + "\r\n" + txtAction.Text;
                                    System.Diagnostics.Process.Start(Path + dtPdf.Rows[0]["MergePDFName"].ToString());

                                    Process pr = new Process();

                                    pr.StartInfo.FileName = Path + dtPdf.Rows[0]["MergePDFName"].ToString();

                                    pr.StartInfo.CreateNoWindow = true;

                                    pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                                    pr.StartInfo.Verb = "Print";

                                    pr.Start();
                                }

                                else
                                {
                                    Application.DoEvents();
                                    txtAction.Text = "Now COA (" + dtPdf.Rows[0]["MergePDFName"].ToString() + ") is not found." + "\r\n" + txtAction.Text;

                                }
                            }
                        }
                        else
                        {
                            Application.DoEvents();
                            txtAction.Text = "Now COA is  not found." + "\r\n" + txtAction.Text;

                        }
                    }
                }
            }
        }

        private void butBPL_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //  FrmCellPropertiesViewer xFrmV = new FrmCellPropertiesViewer(Application.StartupPath + @"\EU装箱单模板5.xlsx", txtTra.Text, "", 1, "", "", true);
            FrmCellPropertiesViewer xFrmV = new FrmCellPropertiesViewer(Application.StartupPath + @"\PackingListTemplate.xlsx", txtTra.Text, "", 1, "");

            this.Cursor = Cursors.Default;
            if (xFrmV.ShowDialog() == DialogResult.OK)
            {
                // txtAtta.Text = xFrmV.savePath;
            }
    
        }

        private void butVPLable_Click(object sender, EventArgs e)
        {
          

        }

        private void butVPro_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridView1_RowCellStyle_1(object sender, RowCellStyleEventArgs e)
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

       















    }










}
