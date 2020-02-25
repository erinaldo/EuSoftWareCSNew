
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UpSoft
{
    public partial class frmUpSoft : Form
    {
        delegate void AsynUpdateUI(int step);
        public frmUpSoft()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 结束程序
        /// </summary>
        public void KillProgram(string exeName)
        {
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList)
            {
                //如果程序启动了，则杀死
                if (process.ProcessName.IndexOf(exeName)>-1)
                {
                    process.Kill();
                }
            }
        }



        private void butNext_Click(object sender, EventArgs e)
        {
            KillProgram("CSUI");
            FTPOperater ftp = new FTPOperater();
            ftp.Server = "34.207.148.81";
            ftp.User = "HaoYuanFTP43";
            ftp.Pass = "Lmh20160929";
            ftp.Port = 2143;
            string[] fileList = ftp.GetFileList("/CSUSA/", "*");
            progressBar1.Maximum = fileList.Length;
            progressBar1.Value = 0;
            foreach (var item in fileList)
            {
                if (item != "")
                {
                    //int taskCount = 100000000; //任务量为10000
                    //this.progressBar1.Maximum = taskCount;
                    //this.progressBar1.Value = 0;
                    FTPHelper ftpHelper = new FTPHelper("34.207.148.81:2143", "CSUSA", "HaoYuanFTP43", "Lmh20160929");
                    //ftpHelper.filePath = @"D:\MCEBate\";
                    //ftpHelper.UpdateUIDelegate += UpdataUIStatus;//绑定更新任务状态的委托
                    //ftpHelper.TaskCallBack += Accomplish;//绑定完成任务要调用的委托
                    Application.DoEvents();
                    groupBox2.Text = item.Replace("\r", "") + " Downloading";

                    ftpHelper.Download(Application.StartupPath, item.Replace("\r", ""));
                    progressBar1.Value += 1;
                    //Thread thread = new Thread(new ParameterizedThreadStart(ftpHelper.Download));
                    //thread.IsBackground = true;
                    //thread.Start(item.Replace("\r","").ToString());
                }
                

            }
            System.Diagnostics.Process.Start(Application.StartupPath + @"\CSUI.exe");

            butCancel_Click(null,null);
        }
        //更新UI
    //private void UpdataUIStatus(int step, string count)
    //    {

    //        if (InvokeRequired)
    //        {
                
    //            this.Invoke(new AsynUpdateUI(delegate(int s)
    //            {
    //                this.progressBar1.Value += s;
    //            }), step);
    //            groupBox2.Text = count;
    //        }
    //        else
    //        {
    //            this.progressBar1.Value += step;
    //            groupBox2.Text = count;
    //        }
    //    }

        //完成任务时需要调用
        private void Accomplish()
        {
            //还可以进行其他的一些完任务完成之后的逻辑处理
           // MessageBox.Show("任务完成");
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
