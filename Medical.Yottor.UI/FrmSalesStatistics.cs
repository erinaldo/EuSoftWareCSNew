using DevExpress.XtraCharts;
using Medical.Yottor.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Medical.Yottor.UI
{
    public partial class FrmSalesStatistics : DevExpress.XtraEditors.XtraForm
    {
        public FrmSalesStatistics()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void butSearch_Click(object sender, EventArgs e)
        {
            string sqlStr;
          
            if (txtDate1.Text==string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the date.");
                txtDate1.Focus();
                return;
            }
            if (txtDate2.Text==string.Empty)
            {
                MessageDxUtil.ShowWarning("Please input the date.");
                txtDate2.Focus();
                return;
            }
            sqlStr = "exec sp_SalesStatistics '" + txtContactName.Text + "','" + txtCountry.Text + "','"+txtContactName.Text+"','"+txtDate1.Text+"','"+txtDate2.Text+"'";
            dt = Maticsoft.DBUtility.DbHelperSQL.Query(sqlStr).Tables[0];
          

            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
            DevExpress.XtraGrid.Columns.GridColumn col_Profit = gridView1.Columns[0];
            gridView1.Columns[0].Width = 50;
            gridView1.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns[0].SummaryItem.DisplayFormat = dt.Rows.Count.ToString();

            DevExpress.XtraGrid.Columns.GridColumn col_Profit11 = gridView1.Columns[1];
            gridView1.Columns[1].Width = 50;
          //  gridView1.Columns[1].SummaryItem.SummaryType = "";
            gridView1.Columns[1].SummaryItem.DisplayFormat = "";


            DevExpress.XtraGrid.Columns.GridColumn col_Profit1 = gridView1.Columns[2];
            gridView1.Columns[2].Width = 200;
            gridView1.Columns[2].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns[2].SummaryItem.DisplayFormat = "Invoice Total:" + dt.Compute("sum(InvoiceTotal)", "TRUE").ToString();
            DevExpress.XtraGrid.Columns.GridColumn col_Profit2 = gridView1.Columns[3];
            gridView1.Columns[3].Width = 200;
            gridView1.Columns[3].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns[3].SummaryItem.DisplayFormat = "Orders Total:" + dt.Compute("sum(Orders)", "TRUE").ToString();
            DevExpress.XtraGrid.Columns.GridColumn col_Profit3 = gridView1.Columns[4];
            gridView1.Columns[4].Width = 200;
            gridView1.Columns[4].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns[4].SummaryItem.DisplayFormat = "Payment Total:" + dt.Compute("sum(Payment)", "TRUE").ToString();
            DevExpress.XtraGrid.Columns.GridColumn col_Profit4 = gridView1.Columns[5];
            gridView1.Columns[5].Width = 200;
            gridView1.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns[5].SummaryItem.DisplayFormat = "Products Total:" + dt.Compute("sum(Products)", "TRUE").ToString();

            DevExpress.XtraGrid.Columns.GridColumn col_Profit5 = gridView1.Columns[6];
            gridView1.Columns[6].Width = 200;
            gridView1.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns[6].SummaryItem.DisplayFormat = "Shipment Cost Total:" + dt.Compute("sum(ShipmentCost)", "TRUE").ToString();
            //DevExpress.XtraGrid.Columns.GridColumn col_Profit6 = gridView1.Columns[7];
            //gridView1.Columns[7].Width = 220;
            //gridView1.Columns[7].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //gridView1.Columns[7].SummaryItem.DisplayFormat = "Total sum except cancled amount.";
            if (chartControl2.Series.Count > 0)
                chartControl2.Series.Clear();
            this.CreateSeries(chartControl2, "Invoice Total", ViewType.Spline, dt, "Months", "InvoiceTotal");
            if (chartControl1.Series.Count > 0)
                chartControl1.Series.Clear();
            this.CreateSeries(chartControl1, "Orders Total", ViewType.Spline, dt, "Months", "Orders");
            this.CreateSeries(chartControl1, "Products Total", ViewType.Spline, dt, "Months", "Products");
        }


        /// <summary>
        /// 创建Series
        /// </summary>
        /// <param name="chat">图表控件</param>
        /// <param name="seriesName">Series名字【诸如：理论电量】</param>
        /// <param name="seriesType">SeriesType【枚举】</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="xBindName">图表控件的X轴绑定</param>
        /// <param name="yBindName">图表控件的Y轴绑定</param>
        public void CreateSeries(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName)
        {
            CreateSeries(chat, seriesName, seriesType, dataSource, xBindName, yBindName, null);
        }

        public void CreateSeries(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName, Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");

            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;

            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
           
          
        }

  

     

        private void FrmOrderProductsInfor_Load(object sender, EventArgs e)
        {

        }

      

        private void butClear_Click(object sender, EventArgs e)
        {
           
            txtContactName.Text = "";
            txtCompany.Text = "";
            txtDate2.Text = "";
            txtDate1.Text = "";
            txtCountry.Text = "";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string filename = NPOIHelper.ExportToExcel(dt, "Order SalesStatistics");
            if (!string.IsNullOrEmpty(filename))
                MessageDxUtil.ShowTips("success");
        }


    }
}
