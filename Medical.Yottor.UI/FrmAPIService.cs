using System;
using System.Collections;
//using Domain;
using System.Xml;

namespace Medical.Yottor.UI
{
    public partial class FrmAPIService : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        private string url = "http://www.webxml.com.cn/WebServices/WeatherWebService.asmx";

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAPIService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGet_Click(object sender, EventArgs e)
        {
          /*  Hashtable ht = new Hashtable();
            ht["byProvinceName"] = "湖北";
            XmlDocument doc = WebServiceCaller.QueryGetWebService(url, "getSupportCity", ht);

            this.memoEdit1.Text = doc.InnerXml;*/
        }

        /// <summary>
        /// Post调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost_Click(object sender, EventArgs e)
        {
           /* Hashtable ht = new Hashtable();
            ht["byProvinceName"] = "广东";
            XmlDocument doc = WebServiceCaller.QueryPostWebService(url, "getSupportCity", ht);
            this.memoEdit1.Text = doc.InnerXml; */
        }

        /// <summary>
        /// Soap调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSoap_Click(object sender, EventArgs e)
        {
          /*  Hashtable ht = new Hashtable();
            ht["byProvinceName"] = "黑龙江";
            XmlDocument doc = WebServiceCaller.QuerySoapWebService(url, "getSupportCity", ht);
            this.memoEdit1.Text = doc.InnerXml; */
        }
    }
}
