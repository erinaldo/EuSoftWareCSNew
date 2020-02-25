
using System.Data;
using System.Data.SqlClient;
using System;

namespace Medical.Yottor.UI
{
    public partial class FrmDataAccess : DevExpress.XtraEditors.XtraForm
    {
        public FrmDataAccess()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取一个数据表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetDataTable_Click(object sender, System.EventArgs e)
        {
            string sql = "SELECT * FROM dbo.Login";
            DataTable login = SQLDataAccess.DataAccess.Instance.GetDataTable(sql, "Login");
            if (login != null)
            {
                MsgBox.ShowExclamation(string.Format("查询成功：{0}条记录！", login.Rows.Count));
            }
        }

        /// <summary>
        /// 获取一个数据集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetDataSet_Click(object sender, System.EventArgs e)
        {
            string sql = "SELECT * FROM dbo.Login";
            DataSet login = SQLDataAccess.DataAccess.Instance.GetDataSet(sql);
            if (login != null)
            {
                MsgBox.ShowExclamation("查询成功！");
            }
        }

        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecuteSQL_Click(object sender, System.EventArgs e)
        {
            string sql = @"INSERT INTO [dbo].[Login]([EmpNO],[LoginName],[LoginCode],[Password],[RoleID],[IsUsed],[CreateName],[CreateDatetime],[Remark])
                                             VALUES (@EmpNO,@LoginName,@LoginCode,@Password,@RoleID,@IsUsed,@CreateName,@CreateDatetime,@Remark)";
            SqlParameter[] parameter = new SqlParameter[]
            {
                  new SqlParameter("@EmpNO",SqlDbType.NVarChar,20),
                  new SqlParameter("@LoginName",SqlDbType.NVarChar,50),
                  new SqlParameter("@LoginCode",SqlDbType.NVarChar,50),
                  new SqlParameter("@Password",SqlDbType.NVarChar,100),
                  new SqlParameter("@RoleID",SqlDbType.Int),
                  new SqlParameter("@IsUsed",SqlDbType.Char,1),
                  new SqlParameter("@CreateName",SqlDbType.NVarChar,10),
                  new SqlParameter("@CreateDatetime",SqlDbType.DateTime),
                  new SqlParameter("@Remark",SqlDbType.NVarChar,1000)
            };
            parameter[0].Value = "10002";
            parameter[1].Value = "Yottor";
            parameter[2].Value = "889";
            parameter[3].Value = "123456";
            parameter[4].Value = 1;
            parameter[5].Value = "1";
            parameter[6].Value = "Admin";
            parameter[7].Value = DateTime.Now;
            parameter[8].Value = "889";

            int i = SQLDataAccess.DataAccess.Instance.ExecuteSQL(sql, parameter);
            if (i > 0)
            {
                MsgBox.ShowExclamation(string.Format("执行成功！影响行数：{0}",i));
            }
        }
    }
}
