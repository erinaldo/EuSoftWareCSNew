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
using System.Net;
using System.Threading;
using System.IO;
namespace Medical.Yottor.UI
{

    public partial class FrmDateDownLoad : DevExpress.XtraEditors.XtraForm
    {
        EuSoft.BLL.ProductsCoaRecord bProductsCoaRecord = new EuSoft.BLL.ProductsCoaRecord();
        private clsFTP cf;
        DataTable dt = new DataTable();
        bool stop = false;
        delegate void AddItemDelegate(int item, string m);
        public FrmDateDownLoad()
        {
            InitializeComponent();

        }

        private void butSearch_Click_1(object sender, EventArgs e)
        {
            string strWhere = " 1=1 ";
            if (txtCaNo.Text != string.Empty)
            {
                strWhere += " and [CatalogNO] like '%" + txtCaNo.Text + "%' ";
            }
            if (txtTra.Text != string.Empty)
            {
                strWhere += " and [TrackingNO] like '%" + txtTra.Text + "%' ";
            }
            strWhere += " order by CatalogNO";
            dt = bProductsCoaRecord.GetList(strWhere).Tables[0];
            this.gridControl2.DataSource = dt;
            this.gridView2.BestFitColumns();
        }

     

        private void butSp2_Click(object sender, EventArgs e)
        {
            if (DbHelperSQL.ExecuteSqlByTimeLocal(" exec sp_DataUpdate2 ", 300) > 0)
            {
                txtSp2.BackColor = Color.LawnGreen;
            }
        }

        private void butSp3_Click(object sender, EventArgs e)
        {
            if (DbHelperSQL.ExecuteSqlByTimeLocal(" exec sp_DataUpdate3 ", 300) > 0)
            {
                txtSp3.BackColor = Color.LawnGreen;
            }
        }

        private void butSp4_Click(object sender, EventArgs e)
        {
            if (DbHelperSQL.ExecuteSqlByTimeLocal(" exec sp_DataUpdate4 ", 300) > 0)
            {
                txtSp4.BackColor = Color.LawnGreen;
            }
        }

      

        private void butSp6_Click(object sender, EventArgs e)
        {
            if (DbHelperSQL.ExecuteSqlByTimeLocal(" exec sp_DataUpdate6 ", 300) > 0)
            {
                txtSp6.BackColor = Color.LawnGreen;
            }
        }

        private void butSp7_Click(object sender, EventArgs e)
        {
            if (DbHelperSQL.ExecuteSqlByTimeLocal(" exec sp_DataUpdate7 ", 300) > 0)
            {
                txtSp7.BackColor = Color.LawnGreen;
            }
        }

        private void butSp8_Click(object sender, EventArgs e)
        {
            if (DbHelperSQL.ExecuteSqlByTimeLocal(" exec sp_DataUpdate8 ", 300) > 0)
            {
                txtSp8.BackColor = Color.LawnGreen;
            }
        }

        private void butSp9_Click(object sender, EventArgs e)
        {
            if (DbHelperSQL.ExecuteSqlByTimeLocal(" exec sp_DataUpdate9 ", 300) > 0)
            {
                txtSp9.BackColor = Color.LawnGreen;
            }
        }

        private void butCoa_Click(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle > -1)
            {
                FTPOperater ftp = new FTPOperater();
                ftp.Server = "34.207.148.81";
                ftp.User = "HaoYuanFTP43";
                ftp.Pass = "Lmh20160929";
                ftp.Port = 2143;
                if (ftp.GetFile("/PDF/CS/USA#Merge/", this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "MergePDFName").ToString(), "E:\\pdf\\CS\\EU#Merge\\", this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "MergePDFName").ToString()))
                {
                    if (File.Exists(@"E:\PDF\CS\EU#Merge\" + this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "MergePDFName").ToString()))
                        System.Diagnostics.Process.Start(@"E:\PDF\CS\EU#Merge\" + this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "MergePDFName").ToString());
                    else
                        MessageDxUtil.ShowWarning(@"E:\PDF\CS\EU#Merge\" + this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "MergePDFName").ToString() + " is not Exists! ");

                }

            }



        }



        private void butSpAll_Click(object sender, EventArgs e)
        {
           // txtAll.Text = "";
            //SingleThread();
            MultiThread();
        }
        private void MultiThread()
        {

            //int i = 1;
            //string[] sqlSplist = {  "sp_DataUpdate2", "sp_DataUpdate3", "sp_DataUpdate4", "sp_DataUpdate6", "sp_DataUpdate7", "sp_DataUpdate8", "sp_DataUpdate9" };
            //foreach (var sqlsp in sqlSplist)
            //{
            //    Thread thread = new Thread(new ParameterizedThreadStart(ExCmd));
            //    // tbUrl.Text = tbUrl.Text + "\r\n" + url;
            //    thread.Name = "Thread" + i.ToString();
            //    thread.Start(sqlsp);
            //    i++;
            //}

            string sql = "delete dbo.price";
            if (DbHelperSQL.ExecuteSql(sql) >= 0)
            {
                //  dt =  .QueryLocal("select catalog_no,package,price  from website_pro_package_price").Tables[0];
                dt = DbHelperMySQL.Query("select csno catalog_no,productSpecification package,commodityPrices price  from priceProductInfo", 300).Tables[0];
                DbHelperSQL.InsertO(dt, "price");
            }

            //RefreshData();
        }

        private void ExCmd(object state)
        {
            string sql = state as string;
            int retNum = DbHelperSQL.ExecuteSqlByTimeLocal(sql, 300);

            //Thread.Sleep(2000);
            this.Invoke(new AddItemDelegate(this.updateTBackColor), retNum, sql.Substring(13));
            ////  this.BeginInvoke(new AddItemDelegate(this.AddTable), dr, 1000);
            //cnn.Close();

        }
        private void updateTBackColor(int item, string m)
        {
            // txtAll.Text += item.ToString();
            // this.Controls["txtSp" + m].BackColor = Color.LawnGreen;
            // this.Controls["txtSp7"].BackColor = Color.LawnGreen;

            switch (m)
            {
             
                case "2":
                    txtSp2.BackColor = Color.LawnGreen;
                    break;
                case "3":
                    txtSp3.BackColor = Color.LawnGreen;
                    break;
                case "4":
                    txtSp4.BackColor = Color.LawnGreen;
                    break;
                case "6":
                    txtSp6.BackColor = Color.LawnGreen;
                    break;
                case "7":
                    txtSp7.BackColor = Color.LawnGreen;
                    break;
                case "8":
                    txtSp8.BackColor = Color.LawnGreen;
                    break;
                case "9":
                    txtSp9.BackColor = Color.LawnGreen;
                    break;

            }
        }

        private void butDownLoadAll_Click(object sender, EventArgs e)
        {


            stop = false;
            txtCaNo.Text = "";
            txtTra.Text = "";

            butSearch_Click_1(null, null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (stop)
                {
                    butDownLoadAll.Enabled = true;
                    Application.DoEvents();
                    this.txtCoa.Text = "Stop";
                    return;
                }
                butDownLoadAll.Enabled = false;
                if (checkOverlay.Checked)
                {
                    FTPOperater ftp = new FTPOperater();
                    ftp.Server = "34.207.148.81";
                    ftp.User = "HaoYuanFTP43";
                    ftp.Pass = "Lmh20160929";
                    ftp.Port = 2143;
                    ftp.GetFile("/PDF/CS/USA#Merge/", dt.Rows[i]["MergePDFName"].ToString(), "E:\\pdf\\CS\\EU#Merge\\", dt.Rows[i]["MergePDFName"].ToString());
                    Application.DoEvents();
                    this.txtCoa.Text = dt.Rows[i]["MergePDFName"].ToString() + "sucess!";
                }
                else
                {
                    if (!File.Exists("E:\\pdf\\CS\\EU#Merge\\" + dt.Rows[i]["MergePDFName"].ToString()))
                    {
                        FTPOperater ftp = new FTPOperater();
                        ftp.Server = "34.207.148.81";
                        ftp.User = "HaoYuanFTP43";
                        ftp.Pass = "Lmh20160929";
                        ftp.Port = 2143;
                        ftp.GetFile("/PDF/CS/USA#Merge/", dt.Rows[i]["MergePDFName"].ToString(), "E:\\pdf\\CS\\EU#Merge\\", dt.Rows[i]["MergePDFName"].ToString());
                        Application.DoEvents();
                        this.txtCoa.Text = dt.Rows[i]["MergePDFName"].ToString() + "sucess!";

                    }
                }





            }
        }

        private void butStopAll_Click(object sender, EventArgs e)
        {
            stop = true;
        }






        //private void ftp_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    string status = e.TotalBytesToReceive == -1 ? "下载中... ..." : "下载完成";
        //    string message = string.Format("\r\n文件大小:{0}KB,已经下载:{1}KB,下载进度:{2}", e.TotalBytesToReceive / 1024, e.BytesReceived / 1024, status);
        //    if (this.txtCoa.InvokeRequired)
        //    {
        //        Action<string> actionDelegate = (x) =>
        //        {
        //            this.txtCoa.Text += Environment.NewLine + x;
        //        };

        //        this.txtCoa.Invoke(actionDelegate, message);
        //    }
        //    else
        //    {
        //        this.txtCoa.Text += Environment.NewLine + message;
        //    }
        //}

        //private void ftp_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e)
        //{
        //    try
        //    {
        //        string message = Environment.NewLine + "文件下载成功！";
        //        if (this.txtCoa.InvokeRequired)
        //        {
        //            Action<string> actionDelegate = (x) =>
        //            {
        //                this.txtCoa.Text += Environment.NewLine + x;
        //            };

        //            this.txtCoa.Invoke(actionDelegate, message);
        //        }
        //        else
        //        {
        //            this.txtCoa.Text += Environment.NewLine + message;
        //        }

        //    }
        //    catch
        //    {
        //        this.txtCoa.Text += Environment.NewLine + "无法连接到服务器，或者用户登陆失败！";
        //    }
        //}



















    }










}
