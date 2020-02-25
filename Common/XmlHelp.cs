using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace EuSoft.Common
{
    public class XmlHelp
    {
        /// <summary>
        /// 获取节点值
        /// </summary>
        /// <param name="list"></param>
        /// <param name="nodename"></param>
        /// <returns></returns>
        public static string GetValue(XmlNodeList list, string nodename)
        {
            string result = string.Empty;
            if (list != null)
            {
                switch (nodename)
                {
                    case "TrackingNumber":
                        result = GetTrackingNumber(list);
                        break;
                    case "CommercialInvoice":
                        result = GetCommercial_Invoice(list);
                        break;
                    case "OutBoundLabel":
                        result = GetOutBound_Label(list);
                        break;
                    case "Code":
                        result = GetCode(list);
                        break;
                }

            }

            return result;
        }

        #region 保存PDF文件
        /// <summary>
        /// 保存PDF文件
        /// </summary>
        /// <param name="labelFileName"></param>
        /// <param name="labelBuffer"></param>
        public static void SaveLabel(string labelFileName, byte[] labelBuffer)
        {
            FileStream LabelFile = new FileStream(labelFileName, FileMode.Create);
            LabelFile.Write(labelBuffer, 0, labelBuffer.Length);
            LabelFile.Close();
        }
        #endregion

        #region 解析追踪码
        /// <summary>
        /// 解析追踪码
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static string GetTrackingNumber(XmlNodeList list)
        {
            //ProcessShipmentReply 节点
            XmlNode n1 = list[0];

            //CompletedShipmentDetail 节点
            XmlNode n2 = n1.ChildNodes[5];

            //CompletedPackageDetails 节点
            XmlNode n3 = n2.ChildNodes[7];

            //TrackingIds 节点
            XmlNode n4 = n3.ChildNodes[1];

            //TrackingNumber 节点
            XmlNode n5 = n4.ChildNodes[2];

            return n5.InnerText;
        }
        #endregion

        #region 解析代码
        /// <summary>
        /// 解析代码
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static string GetCode(XmlNodeList list)
        {
            StringBuilder sb = new StringBuilder();

            //ProcessShipmentReply 节点
            XmlNode n1 = list[0];

            //Notifications 节点
            XmlNode n2 = n1.ChildNodes[1];

            //Severity 节点
            XmlNode n3 = n2.ChildNodes[0];
            XmlNode n4 = n2.ChildNodes[1];
            XmlNode n5 = n2.ChildNodes[2];
            XmlNode n6 = n2.ChildNodes[3];
            XmlNode n7 = n2.ChildNodes[4];
            sb.Append(String.Format("Severity：{0}     ", n3.InnerText));
            sb.Append(String.Format("Source：{0}     ", n4.InnerText));
            sb.Append(String.Format("Code：{0}     ", n5.InnerText));
            sb.Append(String.Format("Message：{0}     ", n6.InnerText));
            sb.Append(String.Format("LocalizedMessage：{0}     ", n7.InnerText));

            return sb.ToString();
        }
        #endregion

        #region 解析发票图片
        /// <summary>
        /// 解析发票图片
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static string GetCommercial_Invoice(XmlNodeList list)
        {
            //ProcessShipmentReply 节点
            XmlNode n1 = list[0];

            //CompletedShipmentDetail 节点
            XmlNode n2 = n1.ChildNodes[5];

            //ShipmentDocuments 节点
            XmlNode n3 = n2.ChildNodes[6];

            //Parts 节点
            XmlNode n4 = n3.ChildNodes[5];

            //Image 节点
            XmlNode n5 = n4.ChildNodes[1];

            return n5.InnerText;
        }
        #endregion

        #region 解析面单图片
        /// <summary>
        /// 解析面单图片
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static string GetOutBound_Label(XmlNodeList list)
        {
            //ProcessShipmentReply 节点
            XmlNode n1 = list[0];

            //CompletedShipmentDetail 节点
            XmlNode n2 = n1.ChildNodes[5];

            //CompletedPackageDetails 节点
            XmlNode n3 = n2.ChildNodes[7];

            //Label 节点
            XmlNode n4 = n3.ChildNodes[4];

            //Parts 节点
            XmlNode n5 = n4.ChildNodes[5];

            //Image 节点
            XmlNode n6 = n5.ChildNodes[1];

            return n6.InnerText;
        }
        #endregion

        public static string GetTrackRequest(string tracknumber, string key, string password, string accountNumber, string meterNumber, string trackPath)
        {
            string request = string.Empty;
            FileStream fs = new FileStream(trackPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            request = sr.ReadToEnd();
            sr.Close();
            //ParentCredential
            request = request.Replace("@P1", key);
            request = request.Replace("@P2", password);
            //UserCredential
            request = request.Replace("@P3", key);
            request = request.Replace("@P4", password);
            //ClientDetail
            request = request.Replace("@P5", accountNumber);
            request = request.Replace("@P6", meterNumber);
            //Tracknumber
            request = request.Replace("@P7", tracknumber);

            return request;
        }
    }
}
