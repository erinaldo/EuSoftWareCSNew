using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace EuSoft.Common
{
    public class FedexStr
    {

        public FedexStr() { }

        #region 系统变量参数

        /// <summary>
        /// Account Number
        /// </summary>
        public static string AccountNumber = "144535430";
        /// <summary>
        /// Meter Number
        /// </summary>
        public static string MeterNumber = "110632288";
        /// <summary>
        /// Key
        /// </summary>
        public static string Key = "ZHydhhp6w2EKJCGQ";
        /// <summary>
        /// Password
        /// </summary>
        public static string Password = "fCpUUGcr7dqkAeKWZrOMincU1";
        /// <summary>
        /// 文件保存路径
        /// </summary>
        // string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        public static string FilePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"PDF\";
        /// <summary>
        /// 合并文件中间保存路径
        /// </summary>
        public static string FilePathHbZj = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"PDF\PDFHbZj\";
        /// <summary>
        /// 合并文件保存路径
        /// </summary>
        public static string FilePathHb = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"PDF\PDFHb\";
        /// <summary>
        /// 请求模板路径
        /// </summary>
        public static string TrackPath= System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"TrackRequest.txt";
        /// <summary>
        /// 追踪请求模板
        /// </summary>
        public static string TempPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"log\";
        /// <summary>
        /// 获取洲代码请求模板
        /// </summary>
        public static string StateOrProvinceCodePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"StateOrProvinceCodeRequest.xml";
        /// <summary>
        /// 日志记录
        /// </summary>
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger("ShimentService");

        #endregion

        public string GetXmlStr(string xml)
        {
            string XmlStr;

            return XmlStr = xml.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;");
        }
        #region 生成 Shipment 信息


        /// <summary>
        /// 生成 Shipment 信息
        /// </summary>
        /// <returns></returns>

        public string GenerateShipment(string Request, out string labPath, out string inovicePath,out string trkNO)
        {
            string result = string.Empty;
            string code = string.Empty;
            labPath = "";
            inovicePath = "";
            trkNO = "";
            try
            {
                //01 请求接口服务器,获取返回值
                Uri uri = new Uri("https://ws.fedex.com:443/web-services/ship");
                //Uri uri = new Uri("https://wsbeta.fedex.com:443/web-services");
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(Request);
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }
                //响应
                WebResponse webResponse = webRequest.GetResponse();
                StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);

                //02 解析返回值
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(myStreamReader.ReadToEnd());

                //添加命名空间(此返回SOAP文档带有命名空间,一定要加上下面两句，否则，会解析失败)
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("ab", "http://schemas.xmlsoap.org/soap/envelope/");
                nsmgr.AddNamespace("ac", "http://fedex.com/ws/ship/v17");

                //获取追踪码(如果没有获取到追踪码，则返回错误信息！)
                string trackingNumber = string.Empty;
                XmlNodeList nodelist0 = xml.SelectNodes("//ac:TrackingNumber", nsmgr);
                if (nodelist0.Count > 0)
                {
                    trackingNumber = nodelist0[0].InnerText;
                }
                if (!string.IsNullOrEmpty(trackingNumber))
                {
                    //获取面单标签
                    byte[] outBoundLabel = null;
                    XmlNodeList nodelist1 = xml.SelectNodes("//ac:Label", nsmgr);
                    if (nodelist1.Count > 0)
                    {
                        if (nodelist1[0].HasChildNodes && nodelist1[0].ChildNodes.Count >= 5)
                        {
                            if (nodelist1[0].ChildNodes[5].HasChildNodes && nodelist1[0].ChildNodes[5].ChildNodes.Count >= 1)
                            {
                                outBoundLabel = Convert.FromBase64String(nodelist1[0].ChildNodes[5].ChildNodes[1].InnerText);
                            }
                        }
                    }

                    //获取发票标签
                    byte[] commercialInvoice = null;
                    XmlNodeList nodelist2 = xml.SelectNodes("//ac:ShipmentDocuments", nsmgr);
                    if (nodelist2.Count > 0)
                    {
                        if (nodelist2[0].HasChildNodes && nodelist2[0].ChildNodes.Count >= 5)
                        {
                            if (nodelist2[0].ChildNodes[5].HasChildNodes && nodelist2[0].ChildNodes[5].ChildNodes.Count >= 1)
                            {
                                commercialInvoice = Convert.FromBase64String(nodelist2[0].ChildNodes[5].ChildNodes[1].InnerText);
                            }
                        }
                    }

                    string LabelFileName1 = String.Format("{0}{1}.pdf", FilePath, trackingNumber);
                    string LabelFileName2 = String.Format("{0}{1}-CI.pdf", FilePath, trackingNumber);
                    trkNO = trackingNumber;
                    //保存费率信息
                    //XmlNodeList nodelist3 = xml.SelectNodes("//ac:TotalNetFreight", nsmgr);
                    //if (nodelist3.Count > 0)
                    //{
                    //    foreach (XmlNode node in nodelist3)
                    //    {
                    //        string amount = node["Amount"].InnerText.ToString();
                    //        string unit = node["Currency"].InnerText.ToString();

                    //        rate += (amount + " " + unit + "|");
                    //    }
                    //}
                    //标签保存
                    if (outBoundLabel != null)
                        XmlHelp.SaveLabel(LabelFileName1, outBoundLabel);
                    if (commercialInvoice != null)
                        XmlHelp.SaveLabel(LabelFileName2, commercialInvoice);

                    if (outBoundLabel != null && commercialInvoice != null)
                    {
                        PDF p = new PDF();
                        p.hbPdfPath(LabelFileName1, LabelFileName2, FilePathHbZj, trackingNumber, FilePathHb);
                        labPath = LabelFileName1;
                        result = "成功";

                        //  result = String.Format("生成成功！ 文件信息：{0}|", LabelFileName1);
                    }
                    else if (outBoundLabel != null)
                    {
                        labPath = LabelFileName1;
                        result = "成功";
                    }
                    //result = String.Format("生成成功！ 文件信息：{0}|", LabelFileName1);
                    else if (commercialInvoice != null)
                    {
                        labPath = LabelFileName2;
                        result = "成功";
                    }
                    // result = String.Format("生成成功！ 文件信息：{0}|", LabelFileName2);
                }
                else
                {
                    result += "生成失败！错误信息：";
                    XmlNodeList messagenode = xml.SelectNodes("//ac:Notifications", nsmgr);
                    if (messagenode.Count > 0)
                    {
                        foreach (XmlNode node in messagenode)
                        {
                            if (node.HasChildNodes)
                            {
                                result += string.Format(" [Code:{0} , [Message:{1}]", node.ChildNodes[2].InnerText, node.ChildNodes[3].InnerText);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result = String.Format("生成失败！ 错误信息：{0} ", ex.Message);
            }

            //日志记录
            logger.Info(String.Format("{0}\r\n{1}", result, code));

            return result;
        }

        #endregion

        #region 获取追踪物件状态信息

        /// <summary>
        /// 获取追踪物件状态信息
        /// </summary>
        /// <param name="tracknumber"></param>
        /// <returns></returns>

        public string GetTrackStatusInfomation(string TrackNumber)
        {
            string result = string.Empty;

            try
            {
                //01 请求接口服务器,获取返回值
                Uri uri = new Uri("https://ws.fedex.com:443/web-services/track");
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";
                string request = XmlHelp.GetTrackRequest(TrackNumber, Key, Password, AccountNumber, MeterNumber, TrackPath);
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(request);
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }
                //响应
                WebResponse webResponse = webRequest.GetResponse();
                StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);

                //02 解析返回值
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(myStreamReader.ReadToEnd());

                //添加命名空间(此返回SOAP文档带有命名空间,一定要加上下面两句，否则，会解析失败)
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("ab", "http://schemas.xmlsoap.org/soap/envelope/");
                nsmgr.AddNamespace("ac", "http://fedex.com/ws/track/v10");


                //获取code
                string code = string.Empty;
                XmlNodeList nodelist = xml.SelectNodes("//ac:StatusDetail", nsmgr);
                if (nodelist.Count > 0)
                {
                    if (nodelist[0].ChildNodes[1] != null)
                    {
                        code = nodelist[0].ChildNodes[1].InnerText;
                    }
                    else
                    {
                        string error = string.Empty;
                        error += "获取状态信息失败！错误信息：";
                        XmlNodeList messageerror = xml.SelectNodes("//ac:Notification", nsmgr);
                        if (messageerror.Count > 0)
                        {
                            foreach (XmlNode node in messageerror)
                            {
                                if (node.HasChildNodes)
                                {
                                    error += string.Format(" [Code:{0} , [Message:{1}]", node.ChildNodes[2].InnerText, node.ChildNodes[3].InnerText);
                                }
                            }
                        }

                        //日志记录
                        logger.Info(String.Format("{0}", error));
                    }
                }

                result = code;
            }
            catch (Exception ex)
            {
                result = String.Format("查询失败！ 错误信息：{0} ", ex.Message);
                //日志记录
                logger.Info(String.Format("{0}", result));
            }

            return result;
        }

        #endregion

        #region 获取追踪物件详细信息


        public DataTable GetTrackDetailInfomation(string TrackNumber)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("date");
            dt.Columns.Add("description");
            dt.Columns.Add("city");
            dt.Columns.Add("countrycode");
            try
            {
                //01 请求接口服务器,获取返回值
                Uri uri = new Uri("https://ws.fedex.com:443/web-services/track");
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";
                string request = XmlHelp.GetTrackRequest(TrackNumber, Key, Password, AccountNumber, MeterNumber, TrackPath);
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(request);
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }
                //响应
                WebResponse webResponse = webRequest.GetResponse();
                StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);

                //02 解析返回值
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(myStreamReader.ReadToEnd());

                //添加命名空间(此返回SOAP文档带有命名空间,一定要加上下面两句，否则，会解析失败)
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("ab", "http://schemas.xmlsoap.org/soap/envelope/");
                nsmgr.AddNamespace("ac", "http://fedex.com/ws/track/v10");

                XmlNodeList nodelist = xml.SelectNodes("//ac:Events", nsmgr);
             
                if (nodelist.Count > 0)
                {
                    foreach (XmlNode node in nodelist)
                    {
                        string date = node.ChildNodes[0].InnerText;
                        string description = node.ChildNodes[2].InnerText;
                        string city = string.Empty;
                        string countrycode = string.Empty;
                        if (node.ChildNodes[node.ChildNodes.Count - 2].ChildNodes.Count > 2)
                        {
                            city = node.ChildNodes[node.ChildNodes.Count - 2].ChildNodes[0].InnerText;
                            countrycode = node.ChildNodes[node.ChildNodes.Count - 2].ChildNodes[2].InnerText;
                        }

                        DataRow _drNew = dt.NewRow();
                        _drNew["date"] = node.ChildNodes[0].InnerText;
                        _drNew["description"] = node.ChildNodes[2].InnerText;
                        _drNew["city"] = city;
                        _drNew["countrycode"] = countrycode;
                        dt.Rows.Add(_drNew);

                       // result += string.Format("{0},{1},{2}  {3},|", date, description, city, countrycode);
                    }
                }
              
            }
            catch (Exception ex)
            {
                result = String.Format("查询失败！ 错误信息：{0} ", ex.Message);
                //日志记录
                logger.Info(String.Format("{0}", result));
            }

            return dt;
        }


        #endregion

        #region 根据国家名称获取国家代码

        /// <summary>
        /// 根据国家名称获取国家代码
        /// </summary>
        /// <param name="CountryName"></param>
        /// <returns></returns>

        public string GetCountryCode(string CountryName)
        {
            return CountryCode.GetCountryCodeByName(CountryName);
        }

        #endregion

        #region 获取洲代码(仅限美国和加拿大)

        /// <summary>
        /// 获取洲代码
        /// </summary>
        /// <param name="CountryCode">国家代码</param>
        /// <param name="PostalCode">邮编</param>
        /// <returns></returns>

        public string GetStateOrProvinceCode(string CountryCode, string PostalCode)
        {
            string result = string.Empty;

            try
            {
                //01 请求接口服务器,获取返回值
                //Uri uri = new Uri("https://wsbeta.fedex.com:443/web-services/cnty");
                Uri uri = new Uri("https://ws.fedex.com:443/web-services/cnty");
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";

                string request = string.Empty;
                //请求参数
                if (File.Exists(StateOrProvinceCodePath))
                {
                    FileStream fs = new FileStream(StateOrProvinceCodePath, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    request = sr.ReadToEnd();
                    sr.Close();
                }
                //Key
                request = request.Replace("@P1", Key);
                //Password
                request = request.Replace("@P2", Password);
                //AccountNumber
                request = request.Replace("@P3", AccountNumber);
                //MeterNumber
                request = request.Replace("@P4", MeterNumber);
                //PostalCode
                request = request.Replace("@P5", PostalCode);
                //CountryCode
                request = request.Replace("@P6", CountryCode);
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(request);
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }
                //响应
                WebResponse webResponse = webRequest.GetResponse();
                StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);

                //02 解析返回值
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(myStreamReader.ReadToEnd());

                //添加命名空间(此返回SOAP文档带有命名空间,一定要加上下面两句，否则，会解析失败)
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("ab", "http://schemas.xmlsoap.org/soap/envelope/");
                nsmgr.AddNamespace("ac", "http://fedex.com/ws/cnty/v2");

                //获取洲代码
                XmlNodeList nodelist0 = xml.SelectNodes("//ac:ExpressDescription", nsmgr);
                if (nodelist0.Count > 0)
                {
                    result = nodelist0[0].ChildNodes[1].InnerText;
                }
            }
            catch (Exception ex)
            {
                result = String.Format("获取失败！错误信息:{0}", ex.Message);
            }

            return result;
        }

        #endregion

        #region 根据请求获取运费信息
        /// <summary>
        /// 根据请求获取运费信息
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>

        public string GetRataInfo(string Request)
        {
            string rate = string.Empty;
            try
            {
                //01 请求接口服务器,获取返回值
                //Uri uri = new Uri("https://wsbeta.fedex.com:443/web-services/rate");
                Uri uri = new Uri("https://ws.fedex.com:443/web-services/rate");
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(Request);
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }
                //响应
                WebResponse webResponse = webRequest.GetResponse();
                StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);

                //02 解析返回值
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(myStreamReader.ReadToEnd());

                //添加命名空间(此返回SOAP文档带有命名空间,一定要加上下面两句，否则，会解析失败)
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("ab", "http://schemas.xmlsoap.org/soap/envelope/");
                nsmgr.AddNamespace("ac", "http://fedex.com/ws/rate/v20");

                //保存费率信息
                XmlNodeList nodelist3 = xml.SelectNodes("//ac:TotalNetFedExCharge", nsmgr);
                if (nodelist3.Count > 0)
                {
                    foreach (XmlNode node in nodelist3)
                    {
                        string amount = node["Amount"].InnerText.ToString();
                        string unit = node["Currency"].InnerText.ToString();
                        rate = amount;
                       // rate += (amount + " " + unit);
                    }
                }
            }
            catch (Exception ex)
            {
                rate = String.Format("获取运费失败！ 错误信息：{0} ", ex.Message);
            }
            return rate;
        }
        #endregion

        #region 检验地址服务
        /// <summary>
        /// 检验地址服务
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>

        public string Getaddressvalidation(string Request)
        {
            string addressvalidation = string.Empty;
            try
            {
                //01 请求接口服务器,获取返回值
                Uri uri = new Uri("https://ws.fedex.com:443/web-services/addressvalidation");
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(Request);
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }
                //响应
                WebResponse webResponse = webRequest.GetResponse();
                StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);

                //02 解析返回值
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(myStreamReader.ReadToEnd());

                //添加命名空间(此返回SOAP文档带有命名空间,一定要加上下面两句，否则，会解析失败)
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("ab", "http://schemas.xmlsoap.org/soap/envelope/");
                nsmgr.AddNamespace("ac", "http://fedex.com/ws/addressvalidation/v4");

                //保存地址信息
                XmlNodeList nodelist = xml.SelectNodes("//ac:EffectiveAddress", nsmgr);
                if (nodelist.Count > 0)
                {
                    foreach (XmlNode n in nodelist)
                    {  // n 遍历所有doc和ctrl节点
                        foreach (XmlNode item in n.ChildNodes)
                        {
                            string stype = item.Name;   //节点名  v4:HouseNumber
                            string s = item.InnerText;  // 节点的值 
                            addressvalidation += item.Name.Replace("v4:", "") + ":" + item.InnerText + "|";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                addressvalidation = String.Format("检验地址失败！ 错误信息：{0} ", ex.Message);
            }
            return addressvalidation;
        }
        #endregion


        public string sendPost(string postUrl, string postDataStr)
        {


            ////  string postUrl = "Http://34.207.148.81:4545/ShimentService.asmx?WSDL";
            //  //用来存放cookie
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
                request.CookieContainer = cookie;
                //request.Timeout = 3000;
                request.Method = "POST";
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
                // return myStreamReader.ReadToEnd();

                //string eStr=@"</string>";
                //  string sStr=@"'>";
                System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
                xd.LoadXml(myStreamReader.ReadToEnd());
                return xd.InnerText;
                //  return Regex.Match(myStreamReader.ReadToEnd(), "(?<="+sStr+").*?(?="+eStr+")").Value;
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

    }


}
