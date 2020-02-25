using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Domain;
using System.IO;
using System.Net;

namespace Medical.Yottor.UI
{
    public partial class FrmFTP : DevExpress.XtraEditors.XtraForm
    {
        public FrmFTP()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
           /* txtTest.Text = "文件正在上传... ...";
            FTP ftp = new FTP(new Uri("ftp://171.16.0.21/"), "business47@hy.com", "wap460");
            DirectoryInfo Folder = new DirectoryInfo(@"D:\uploadTest\");
            FileInfo[] fileInfo = Folder.GetFiles();
            foreach (FileInfo NextFile in fileInfo)
            {
                ftp.UploadProgressChanged += new FTP.De_UploadProgressChanged(ftp_UploadProgressChanged);
                ftp.UploadFileCompleted += new FTP.De_UploadFileCompleted(ftp_UploadFileCompleted);
                ftp.UploadFileAsync(NextFile.FullName, true);
            }*/
        }

        private void ftp_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            string status = (int)((e.BytesSent / 1024) / (e.TotalBytesToSend / 1024)) == 0 ? "上传中... ..." : "上传完成";
            string message = string.Format("\r\n文件大小:{0}KB,已经上传:{1}KB,上传进度:{2}", e.TotalBytesToSend / 1024, e.BytesSent / 1024, status);
            if (this.txtTest.InvokeRequired)
            {
                Action<string> actionDelegate = (x) => 
                { 
                    this.txtTest.Text += Environment.NewLine + x; 
                };
              
                this.txtTest.Invoke(actionDelegate, message);
            }
            else
            {
                this.txtTest.Text += Environment.NewLine + message;
            }
        }

        private void ftp_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            try
            {
                string message = Environment.NewLine + "文件上传成功！";
                if (this.txtTest.InvokeRequired)
                {
                    Action<string> actionDelegate = (x) =>
                    {
                        this.txtTest.Text += Environment.NewLine + x;
                    };
                    this.txtTest.Invoke(actionDelegate, message);
                }
                else
                {
                    this.txtTest.Text += Environment.NewLine + message;
                }

            }
            catch
            {
                this.txtTest.Text += Environment.NewLine + "无法连接到服务器，或者用户登陆失败！";
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
             //txtDownload.Text = "文件正在下载... ...";
             //FTP ftp = new FTP(new Uri("ftp://171.16.0.21/"), "business47@hy.com", "wap460");
             //ftp.DownloadProgressChanged += new FTP.De_DownloadProgressChanged(ftp_DownloadProgressChanged);
             //ftp.DownloadDataCompleted += new FTP.De_DownloadDataCompleted(ftp_DownloadDataCompleted);
             //ftp.DownloadFileAsync("YT测试上传.zip", "D:\\downloadTest\\YT测试上传.zip"); 
        }

        private void ftp_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string status = e.TotalBytesToReceive == -1 ? "下载中... ..." : "下载完成";
            string message = string.Format("\r\n文件大小:{0}KB,已经下载:{1}KB,下载进度:{2}", e.TotalBytesToReceive / 1024, e.BytesReceived / 1024, status);
            if (this.txtDownload.InvokeRequired)
            {
                Action<string> actionDelegate = (x) =>
                {
                    this.txtDownload.Text += Environment.NewLine + x;
                };

                this.txtDownload.Invoke(actionDelegate, message);
            }
            else
            {
                this.txtDownload.Text += Environment.NewLine + message;
            }
        }

        private void ftp_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string message = Environment.NewLine + "文件下载成功！";
                if (this.txtDownload.InvokeRequired)
                {
                    Action<string> actionDelegate = (x) =>
                    {
                        this.txtDownload.Text += Environment.NewLine + x;
                    };

                    this.txtDownload.Invoke(actionDelegate, message);
                }
                else
                {
                    this.txtDownload.Text += Environment.NewLine + message;
                }

            }
            catch
            {
                this.txtDownload.Text += Environment.NewLine + "无法连接到服务器，或者用户登陆失败！";
            }
        }
    }
}
