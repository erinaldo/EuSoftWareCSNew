using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace Medical.Yottor.UI
{
    public partial class FrmSqlConnect : DevExpress.XtraEditors.XtraForm
    {
        #region 变量及属性

        internal Point ShareLocation
        {
            get
            {
                return this.Location;
            }
            set
            {
                this.Location = value;
            }
        }

        private ConditionValidationRule VNotBlank;
        private string exeConfigFile;
        private const string key = "conn";
        private const string db = "Master";
        private const string _lostConfigFile = "没有发现可用的配置文件，请检查是否被误删。";
        private const string _lostConnection =
            "连接数据服务器失败，可能的原因为：\r\n"
            + "\r\n1.网络故障：请检查局域网是否正常。"
            + "\r\n2.未配置数据服务器或参数不正常。";

        #endregion

        #region 构造函数

        public FrmSqlConnect()
        {
            InitializeComponent();

            VNotBlank = new ConditionValidationRule();
            VNotBlank.ConditionOperator = ConditionOperator.IsNotBlank;
            VNotBlank.ErrorText = "远程访问模式时，必填";
            VNotBlank.ErrorType = ErrorType.Warning;
        }

        #endregion

        private void FrmSqlConnect_Load(object sender, EventArgs e)
        {
            exeConfigFile = Application.ExecutablePath + ".config";
            if (!File.Exists(exeConfigFile))
            {
                MsgBox.ShowError(_lostConfigFile);
                Application.Exit();
                return;
            }
            try
            {
                SqlConnectionStringBuilder build = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[key].ConnectionString);
                cboLoginType.SelectedIndex = build.IntegratedSecurity ? 0 : 1;
                txtSrv.Text = build.DataSource;
                txtUser.Text = build.UserID;
                txtPwd.Text = build.Password;
            }
            catch (Exception) { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
           //01 验证输入
            bool isValid = vpMain.Validate();
            if (!isValid)
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            //02 测试连接字符串
            string conString = JoinConnectionString(db);
            if (!TestConnectionString(conString, true))
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            //03 写入配置文件
            Configuration config;
            ConfigurationUserLevel level = ConfigurationUserLevel.None;
            try
            {
                if (!File.Exists(exeConfigFile))
                {
                    throw new Exception(_lostConfigFile);
                }
                config = ConfigurationManager.OpenExeConfiguration(level);
                ConnectionStringSettings node = config.ConnectionStrings.ConnectionStrings[key];
                if (node == null)
                {
                    node = new ConnectionStringSettings();
                    node.Name = key;
                    node.ConnectionString = conString;
                    node.ProviderName = "System.Data.SqlClient";

                    config.ConnectionStrings.ConnectionStrings.Add(node);
                }
                else
                {
                    node.ConnectionString = conString;
                    node.ProviderName = "System.Data.SqlClient";
                }
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
                node = config.ConnectionStrings.ConnectionStrings[key];
            }
            catch (Exception ex)
            {
                MsgBox.ShowExclamation(ex.Message);
                this.DialogResult = DialogResult.None;
            }
        }

        private string JoinConnectionString(string dbName)
        {
            string conString = string.Format("Data Source={0};Initial Catalog={1};", txtSrv.Text, dbName);
            if (cboLoginType.SelectedIndex == 0)
            {
                conString += "Integrated Security=True;";
            }
            else
            {
                conString += string.Format("Persist Security Info=True;User ID={0};Password={1};",
                    txtUser.Text.TrimEnd(),txtPwd.Text.TrimEnd());
            }
            conString += "MultipleActiveResultSets=True;App=鱼儿天下开发框架";

            return conString;
        }

        private bool TestConnectionString(string connectionString, bool showMsg)
        {
            string msg = string.Empty;
            using (SqlConnection cn = new SqlConnection())
            {
                try
                {
                    msg = "自配置文件读取参数失败。";
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        connectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                    }
                    cn.ConnectionString = connectionString;
                    msg = _lostConnection;
                    cn.Open();
                    if (showMsg)
                        MsgBox.ShowInformation("数据服务器状态正常。");
                    return true;
                }
                catch (Exception)
                {
                    MsgBox.ShowError(msg);
                    return false;
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}