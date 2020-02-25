using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using Maticsoft.DBUtility;

namespace Medical.Yottor.UI
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        private string exeConfigFile;
        
     //   private readonly string UserCenterDbConnection = BaseSystemInfo.UserCenterDbConnection;

        EuSoft.BLL.MCEUser MCEUser = new EuSoft.BLL.MCEUser();
        EuSoft.BLL.SysXt bSysXt = new EuSoft.BLL.SysXt();
        public FrmLogin()
        {
            InitializeComponent();
           
        }
        public string Read()
        {
            string strXml = "";
            try
            {

                strXml = File.ReadAllText(Application.StartupPath + @"\sp.sql", Encoding.Default);


            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return strXml;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\sp.sql"))
            {

                string sql = Read();
                if (DbHelperSQL.ExecuteSqlByTimeLocal(sql.Replace("\n", " ").Replace("\t", " ").Replace("\r", " "), 30) > 0)
                {

                }
            }   
            
            txtUserName.Text = txtUserName.Text.TrimEnd();
            txtPwd.Text = txtPwd.Text.TrimEnd();
            bool isValid = vpMain.Validate();
            if (!isValid) return;
            try
            {
                DataSet ds = new DataSet();
                StringBuilder strWhere = new StringBuilder();
                if (txtUserName.Text.Trim() != "")
                {

                    strWhere.AppendFormat("username='{0}'", txtUserName.Text.Trim());
                }
                if (txtPwd.Text.Trim() != "")
                {

                    strWhere.AppendFormat(" and  password ='{0}'", txtPwd.Text.Trim());
                }
                ds = MCEUser.GetList(strWhere.ToString());
               



                if (ds.Tables[0].Rows.Count >0)
                {
                    Properties.Settings.Default.LastUser = txtUserName.Text;
                    Properties.Settings.Default.LastPwd = txtPwd.Text;
                    Properties.Settings.Default.SalesCompany = "";
                }
                else
                {
                    string msg = "User name and password error!";
                    MsgBox.ShowExclamation(msg);
                    throw new Exception(msg);
                }

                Properties.Settings.Default.IsSaveUser = cbxRemember.Checked;
                Properties.Settings.Default.Save();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            
            catch(Exception)
            {
            //  throw ex;
                this.DialogResult = DialogResult.None;
                return;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            FrmSqlConnect f = new FrmSqlConnect();
            f.StartPosition = FormStartPosition.Manual;
            f.Location = this.Location;
            this.Hide();
            f.ShowDialog();
            this.Location = f.ShareLocation;
            f.Dispose();
            this.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            this.Text = string.Format("{0} V{1}.{2}", fvi.FileDescription, fvi.ProductMajorPart, fvi.ProductMinorPart);

            if (Properties.Settings.Default.IsSaveUser)
            {
                txtUserName.Text = Properties.Settings.Default.LastUser;
                txtPwd.Text = Properties.Settings.Default.LastPwd;
                cbxRemember.Checked = true;
            }

            exeConfigFile = Application.ExecutablePath + ".config";
            if (!File.Exists(exeConfigFile))
            {
                MsgBox.ShowError(exeConfigFile, "提醒");
                Application.Exit();
                return;
            }
            else
            {
                FileVersionInfo file1 = System.Diagnostics.FileVersionInfo.GetVersionInfo(exeConfigFile.Replace(".config", ""));
                //版本号显示为“主版本号.次版本号.内部版本号.专用部件号”。
                string FileVersions = String.Format("{0}.{1}.{2}.{3}", file1.FileMajorPart, file1.FileMinorPart, file1.FileBuildPart, file1.FilePrivatePart);

                DataTable dt = bSysXt.GetAllList().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["newver"].ToString().CompareTo(FileVersions) > 0)
                    {
                        if (MessageDxUtil.ShowYesNoAndWarning("The server has the latest version and is upgraded?") == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Application.StartupPath + @"\UpSoft1.exe");
                            // Application.Exit();

                        }
                    }
                }
            }
        }

       
    }
}