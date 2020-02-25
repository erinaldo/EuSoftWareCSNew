using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace EuSoft.Common
{
    public class MailSender
    {


        /// <summary>  
        /// 发送邮件  
        /// </summary>  
        /// <param name="mailSubjct"></param>  
        /// <param name="mailBody"></param>  
        /// <param name="mailFrom"></param>  
        /// <param name="mailAddress"></param>  
        /// <param name="HostIP"></param>  
        /// <param name="port"></param>  
        /// <param name="username"></param>  
        /// <param name="password"></param>  
        /// <param name="ssl"></param>  
        /// <param name="replyTo"></param>  
        /// <param name="sendOK"></param>  
        /// <returns></returns>  
        public static string sendMail(string mailSubjct, string mailBody, string mailFrom, List<string> mailAddress, string HostIP, int port, string username, string password, bool ssl, string Bcc, string ImgPath, string sfile, out bool sendOK)
        {
            sendOK = true;
            string str = "";
            try
            {
                MailMessage message = new MailMessage
                {
                    IsBodyHtml = true,
                    Subject = mailSubjct,
                    Body = mailBody,
                    From = new MailAddress(mailFrom)
                };
                if (Bcc != string.Empty)
                {
                    //MailAddress address = new MailAddress(replyTo);
                    //message.ReplyTo = address;
                    message.Bcc.Add(Bcc);
                }
                
                
                Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                for (int i = 0; i < mailAddress.Count; i++)
                {
                    if (regex.IsMatch(mailAddress[i]))
                    {
                        message.To.Add(mailAddress[i]);
                    }
                }
                if (message.To.Count == 0)
                {
                    return string.Empty;
                }
                SmtpClient client = new SmtpClient
                {
                    EnableSsl = ssl,
                    UseDefaultCredentials = false
                };
                message.Attachments.Clear();  
                if (ImgPath!=string.Empty )
                {
                    message.Attachments.Add(new Attachment(ImgPath));
                    message.Attachments[0].ContentType.Name = "image/gif";
                    message.Attachments[0].ContentId = "ewen";
                    message.Attachments[0].ContentDisposition.Inline = true;
                    message.Attachments[0].TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                }
                if (sfile != string.Empty)
                {
                    Attachment attach = new Attachment(sfile);
                    message.Attachments.Add(attach);
                }





                NetworkCredential credential = new NetworkCredential(username, password);
                client.Credentials = credential;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = HostIP;
                client.Port = port;
                client.Send(message);
            }
            catch (Exception exception)
            {
                str = exception.Message;
                sendOK = false;
            }
            return str;
        }


        public static void Send(string server, string sender, string recipient, string subject,
    string body, bool isBodyHtml, Encoding encoding, bool isAuthentication, params string[] files)
        {
            SmtpClient smtpClient = new SmtpClient(server);
            MailMessage message = new MailMessage(sender, recipient);
            message.IsBodyHtml = isBodyHtml;

            message.SubjectEncoding = encoding;
            message.BodyEncoding = encoding;

            message.Subject = subject;
            message.Body = body;

            message.Attachments.Clear();
            if (files != null && files.Length != 0)
            {
                for (int i = 0; i < files.Length; ++i)
                {
                    Attachment attach = new Attachment(files[i]);
                    message.Attachments.Add(attach);
                }
            }

            if (isAuthentication == true)
            {
                smtpClient.Credentials = new NetworkCredential(SmtpConfig.Create().SmtpSetting.User,
                    SmtpConfig.Create().SmtpSetting.Password);
            }
            smtpClient.Send(message);


        }

        public static void Send(string recipient, string subject, string body)
        {
            Send(SmtpConfig.Create().SmtpSetting.Server, SmtpConfig.Create().SmtpSetting.Sender, recipient, subject, body, true, Encoding.Default, true, null);
        }

        public static void Send(string Recipient, string Sender, string Subject, string Body)
        {
            Send(SmtpConfig.Create().SmtpSetting.Server, Sender, Recipient, Subject, Body, true, Encoding.UTF8, true, null);
        }

        static readonly string smtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
        static readonly string userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
        static readonly string pwd = System.Configuration.ConfigurationManager.AppSettings["Pwd"];
        static readonly int smtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SmtpPort"]);
        static readonly string authorName = System.Configuration.ConfigurationManager.AppSettings["AuthorName"];
        static readonly string to = System.Configuration.ConfigurationManager.AppSettings["To"];


        public void Send(string subject, string body)
        {

            List<string> toList = StringPlus.GetSubStringList(StringPlus.ToDBC(to), ',');
            OpenSmtp.Mail.Smtp smtp = new OpenSmtp.Mail.Smtp(smtpServer, userName, pwd, smtpPort);
            foreach (string s in toList)
            {
                OpenSmtp.Mail.MailMessage msg = new OpenSmtp.Mail.MailMessage();
                msg.From = new OpenSmtp.Mail.EmailAddress(userName, authorName);

                msg.AddRecipient(s, OpenSmtp.Mail.AddressType.To);

                //设置邮件正文,并指定格式为 html 格式
                msg.HtmlBody = body;
                //设置邮件标题
                msg.Subject = subject;
                //指定邮件正文的编码
                msg.Charset = "gb2312";
                //发送邮件
                smtp.SendMail(msg);
            }
        }
    }

    public class SmtpSetting
    {
        private string _server;

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        private bool _authentication;

        public bool Authentication
        {
            get { return _authentication; }
            set { _authentication = value; }
        }
        private string _user;

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }
        private string _sender;

        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }

    public class SmtpConfig
    {
        private static SmtpConfig _smtpConfig;
        private string ConfigFile
        {
            get
            {
                string configPath = ConfigurationManager.AppSettings["SmtpConfigPath"];
                if (string.IsNullOrEmpty(configPath) || configPath.Trim().Length == 0)
                {
                    configPath = HttpContext.Current.Request.MapPath("/Config/SmtpSetting.config");
                }
                else
                {
                    if (!Path.IsPathRooted(configPath))
                        configPath = HttpContext.Current.Request.MapPath(Path.Combine(configPath, "SmtpSetting.config"));
                    else
                        configPath = Path.Combine(configPath, "SmtpSetting.config");
                }
                return configPath;
            }
        }
        public SmtpSetting SmtpSetting
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.ConfigFile);
                SmtpSetting smtpSetting = new SmtpSetting();
                smtpSetting.Server = doc.DocumentElement.SelectSingleNode("Server").InnerText;
                smtpSetting.Authentication = Convert.ToBoolean(doc.DocumentElement.SelectSingleNode("Authentication").InnerText);
                smtpSetting.User = doc.DocumentElement.SelectSingleNode("User").InnerText;
                smtpSetting.Password = doc.DocumentElement.SelectSingleNode("Password").InnerText;
                smtpSetting.Sender = doc.DocumentElement.SelectSingleNode("Sender").InnerText;

                return smtpSetting;
            }
        }
        private SmtpConfig()
        {

        }
        public static SmtpConfig Create()
        {
            if (_smtpConfig == null)
            {
                _smtpConfig = new SmtpConfig();
            }
            return _smtpConfig;
        }
    }
}
