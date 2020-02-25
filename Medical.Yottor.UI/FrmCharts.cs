using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

namespace Medical.Yottor.UI
{
    public partial class FrmCharts : DevExpress.XtraEditors.XtraForm
    {
        public FrmCharts()
        {
            InitializeComponent();
        }

        private void FrmCharts_Load(object sender, EventArgs e)
        {
            DataTable dt = GetTestData();
            if (chartControl1.Series.Count > 0)
                chartControl1.Series.Clear();
            this.CreateSeries(chartControl1, "理论功率", ViewType.Spline, dt, "time", "Power");
            this.CreateSeries(chartControl1, "实际功率", ViewType.Spline, dt, "time", "ActulPower");

            if (chartControl2.Series.Count > 0)
                chartControl2.Series.Clear();
            this.CreateSeries(chartControl2, "理论功率", ViewType.Bar, dt, "time", "Power");
            this.CreateSeries(chartControl2, "实际功率", ViewType.Bar, dt, "time", "ActulPower");
        }

        /// <summary>
        /// 测试数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetTestData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("time",typeof(string)));
            dt.Columns.Add(new DataColumn("Power", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ActulPower", typeof(decimal)));

            Random _rm = new Random();
            for (int i = 0; i < 24; i++)
            {
                DataRow _drNew = dt.NewRow();
                _drNew["time"] = string.Format("{0}点", i);
                _drNew["Power"] = 220;
                _drNew["ActulPower"] = _rm.Next(150, 220);
                dt.Rows.Add(_drNew);
            }
            return dt;
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
 

    }
}