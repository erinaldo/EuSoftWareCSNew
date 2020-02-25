using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Domain;
using System.Xml;

namespace Medical.Yottor.UI
{
    /// <summary>
    /// 
    ///  XPath 使用语法
    ///  
    ///  1、(/)     : 表示从根节点开始选择                       eg:/root                              表示选择根节点root
    ///             表示节点与节点之间的间隔符                   eg:/root/Temple                       表示根节点root下的Temple节点
    ///          
    ///  2、(//)    : 表示从整个xml文档中查找,不考虑节点位置     eg://Temple_Title                     表示xml中所有Temple_Title节点
    ///  
    ///  3、(.)     : 表示选择当前节点                           eg:/root/.                            表示选择root节点
    ///  
    ///  4、(..)    : 表示父节点                                 eg:/root/Temple[0]/..                 表示选择Temple[0]的父节点，也就是root节点
    ///  
    ///  5、(@)     : 表示属性                                   eg://Temple_Name/@Temple_Value        表示Temple_Name节点下所有的Temple_Value属性
    ///     ([...]) : 表示属性条件                               eg://Temple_Name[@Temple_Value = '0'] 表示Temple_Name节点下所有的Temple_Value值为0的节点
    ///     (|)     : 表示合并节点                               eg://Temple_Value[@Temple_Value = '1'] | Temple_Name[@Temple_Value = '1']
    ///                                                                                                表示Temple_Name下所有Temple_Value值为0和1的节点
    ///     (*)     : 表示任何名字的节点和属性                   eg://Temle_Content/*                  表示xml中Temle_Content下所有子节点
    ///                                                          eg://Temle_Content/@*                 表示xml中Temle_Content下所有属性节点
    ///     
    /// </summary>
    /// 
    public partial class FrmXML : DevExpress.XtraEditors.XtraForm
    {
        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmXML()
        {
            InitializeComponent();
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("Test - 副本.XML");

            //文件中含有命名空间，需要加上这两句代码
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("ab", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ac", "http://fedex.com/ws/ship/v17");

            //查找值
            string TrackingNumber = string.Empty;
            byte[] CI_Image;
            byte[] OutBound_Image;

            XmlNodeList nodelist0 = xml.SelectNodes("//ac:TrackingIds", nsmgr);
            if (nodelist0.Count > 0)
            {
                TrackingNumber = nodelist0[0].InnerText;
            }

            XmlNodeList nodelist1 = xml.SelectNodes("//ac:ShipmentDocuments", nsmgr);
            if (nodelist1.Count > 0)
            {
                CI_Image = Convert.FromBase64String(nodelist1[0].ChildNodes[5].ChildNodes[1].InnerText);
            }

            XmlNodeList nodelist2 = xml.SelectNodes("//ac:Label", nsmgr);
            if (nodelist2.Count > 0)
            {
                OutBound_Image = Convert.FromBase64String(nodelist2[0].ChildNodes[5].ChildNodes[1].InnerText);
            }
        }
    }
}
