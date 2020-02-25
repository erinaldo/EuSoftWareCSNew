
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace EuSoft.Common
{
    public class WebAutoLogin
    {
        #region 属性
        /// <summary>
        /// 登陆后返回的Html
        /// </summary>
        public static string ResultHtml
        {
            get;
            set;
        }
        /// <summary>
        /// 下一次请求的Url
        /// </summary>
        public static string NextRequestUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 若要从远程调用中获取COOKIE一定要为request设定一个CookieContainer用来装载返回的cookies
        /// </summary>
        public static CookieContainer CookieContainer
        {
            get;
            set;
        }
        /// <summary>
        /// Cookies 字符创
        /// </summary>
        public static string CookiesString
        {
            get;
            set;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 用户登陆指定的网站
        /// </summary>
        /// <param name="loginUrl"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        public static void PostLogin(string loginUrl, string account, string password)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
//                string postdata = "SMENC=UTF-8&TARGET=HTTPS%3A%2F%2Fwww.fedex.com%2Flogin%2Fredirect%3FTARGET%3Dhttps%253A%252F%252Fwww.fedex.com%252FHSL&SMAUTHREASON=0&SMAGENTNAME=-SM-cHRx8NDkuPhVMZoOiDl6YXu9vZBhuA6yi2SYGTDA8Zh%2BJTE84 " +
//"%2BPYQB5cW3BmWGnc&fclqrs=https%3A%2F%2Fwww.fedex.com%2Flogin%2Fweb%2Fjsp%2Flogon.jsp%3FTYPE%3D33554432" +
//"%26REALMOID%3D06-00047ebe-e235-1f50-a0ab-0e650affb0c4%26GUID%3D1%26SMAUTHREASON%3D0%26METHOD%3DGET%26SMAGENTNAME" +
//"%3D-SM-cHRx8NDkuPhVMZoOiDl6YXu9vZBhuA6yi2SYGTDA8Zh%252bJTE84%252bPYQB5cW3BmWGnc%26TARGET%3D-SM-HTTPS" +
//"%253a%252f%252fwww%252efedex%252ecom%252flogin%252fredirect%253fTARGET%253dhttps-%253A-%252F-%252Fwww" +
//"%252efedex%252ecom-%252FHSL&invitationError=&USER=chemscene&PASSWORD=Haoyuan888&remusrid=yes&login=Login";//模拟请求数据，数据样式可以用FireBug插件得到。
                string postdata = "SMENC=UTF-8&TARGET=HTTPS%3A%2F%2Fwww.fedex.com%2Flogin%2Fredirect%3FTARGET%3Dhttps%253A%252F%252Fwww.fedex.com%252FHSL&SMAUTHREASON=0" +
 "&fclqrs=https%3A%2F%2Fwww.fedex.com%2Flogin%2Fweb%2Fjsp%2Flogon.jsp%3FTYPE%3D33554432%26REALMOID%3D06-00047ebe-e235-1f50-a0ab-0e650affb0c4" +
 "%26GUID%3D1%26SMAUTHREASON%3D0%26METHOD%3DGET%26SMAGENTNAME%3Dujuw2I74EbiHkb58Hi6DXFHGGMrZ4YgOED8i1j9CYLB7OkuXBqf2VmPmsoBuuFZI" +
 "%26TARGET%3D-SM-HTTPS%253a%252f%252fwww%252efedex%252ecom%252flogin%252fredirect%253fTARGET%253dhttps-" +
 "%253A-%252F-%252Fwww%252efedex%252ecom-%252FHSL&invitationError=&USER=chemscene&PASSWORD=Haoyuan888&remusrid=yes&login=Login";
                
                
                
                //string LoginUrl = "https://www.fedex.com/login/web/jsp/logon.jsp";
                request = (HttpWebRequest)WebRequest.Create(loginUrl);//实例化web访问类  
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "POST";//数据提交方式为POST  
                request.ContentType = "application/x-www-form-urlencoded";    //模拟头  
                request.AllowAutoRedirect = false;   // 不用需自动跳转
                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
                request.KeepAlive = true;
                //提交请求  
                byte[] postdatabytes = Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = postdatabytes.Length;
                Stream stream;
                stream = request.GetRequestStream();
                //设置POST 数据
                stream.Write(postdatabytes, 0, postdatabytes.Length);
                stream.Close();
                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                CookieCollection cook = response.Cookies;
                string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;
                //取下一次GET跳转地址  
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string content = sr.ReadToEnd();
                sr.Close();
                request.Abort();
                response.Close();
                //依据登陆成功后返回的Page信息，求出下次请求的url
                //每个网站登陆后加载的Url和顺序不尽相同，以下两步需根据实际情况做特殊处理，从而得到下次请求的URL
              //  string[] substr = content.Split(new char[] { '"' });
              //  NextRequestUrl = substr[1];
            }
            catch (WebException ex)
            {
                throw ex;

              //  MessageBox.Show(string.Format("登陆时出错，详细信息：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 获取用户登陆后下一次请求返回的内容
        /// </summary>
        public static void GetPage(string url)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "GET";
                request.KeepAlive = true;
                request.Headers.Add("Cookie:" + CookiesString);
                request.CookieContainer = CookieContainer;
                request.AllowAutoRedirect = false;
                response = (HttpWebResponse)request.GetResponse();
                //设置cookie  
                CookiesString = request.CookieContainer.GetCookieHeader(request.RequestUri);
                //取再次跳转链接  
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string ss = sr.ReadToEnd();
                sr.Close();
                request.Abort();
                response.Close();
                //依据登陆成功后返回的Page信息，求出下次请求的url
                //每个网站登陆后加载的Url和顺序不尽相同，以下两步需根据实际情况做特殊处理，从而得到下次请求的URL
                //string[] substr = ss.Split(new char[] { '"' });
                //NextRequestUrl = substr[1];
                ResultHtml = ss;
            }
            catch (WebException ex)
            {
                //MessageBox.Show(string.Format("获取页面HTML信息出错，详细信息：{0}", ex.Message));
            }
        }
        #endregion

    }
}
