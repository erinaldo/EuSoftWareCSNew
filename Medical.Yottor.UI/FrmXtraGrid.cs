using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Medical.Yottor.UI
{
    public partial class FrmXtraGrid : DevExpress.XtraEditors.XtraForm
    {
        public FrmXtraGrid()
        {
            InitializeComponent();
        }

        public DataTable GetTestData()
        {
            DataTable dt = new DataTable("table1");
            dt.Columns.Add("classID", typeof(int));
            dt.Columns.Add("className", typeof(String));
            dt.Columns.Add("stuNum", typeof(int));
            dt.Columns.Add("stuName", typeof(String));
            dt.Columns.Add("courseName", typeof(String));
            dt.Columns.Add("hours", typeof(String));
            dt.Columns.Add("grade", typeof(String));

            dt.Rows.Add(new object[] { 1, "计算机101班", 2014001, "李强", "数据库", "64", "90" });
            dt.Rows.Add(new object[] { 1, "计算机101班", 2014001, "李强", "操作系统", "64", "100" });
            dt.Rows.Add(new object[] { 1, "计算机101班", 2014001, "李强", "软件工程", "64", "80" });
            dt.Rows.Add(new object[] { 1, "计算机101班", 2014002, "王伟", "数据库", "64", "90" });
            dt.Rows.Add(new object[] { 1, "计算机101班", 2014002, "王伟", "数据库", "64", "90" });
            dt.Rows.Add(new object[] { 1, "计算机101班", 2014002, "王伟", "数据库", "64", "90" });

            dt.Rows.Add(new object[] { 2, "计算机102班", 2014003, "孙明", "数据库", "64", "90" });
            dt.Rows.Add(new object[] { 2, "计算机102班", 2014003, "孙明", "操作系统", "64", "100" });
            dt.Rows.Add(new object[] { 2, "计算机102班", 2014003, "孙明", "软件工程", "64", "80" });
            dt.Rows.Add(new object[] { 2, "计算机102班", 2014004, "赵敏", "数据库", "64", "100" });
            dt.Rows.Add(new object[] { 2, "计算机102班", 2014004, "赵敏", "数据库", "64", "90" });
            dt.Rows.Add(new object[] { 2, "计算机102班", 2014004, "赵敏", "数据库", "64", "70" });

            dt.Rows.Add(new object[] { 3, "计算机103班", 2014005, "李磊", "数据库", "64", "90" });
            dt.Rows.Add(new object[] { 3, "计算机103班", 2014005, "李磊", "操作系统", "64", "100" });
            dt.Rows.Add(new object[] { 3, "计算机103班", 2014005, "李磊", "软件工程", "64", "80" });
            dt.Rows.Add(new object[] { 3, "计算机103班", 2014006, "马超", "数据库", "64", "100" });
            dt.Rows.Add(new object[] { 3, "计算机103班", 2014006, "马超", "数据库", "64", "90" });
            dt.Rows.Add(new object[] { 3, "计算机103班", 2014006, "马超", "数据库", "64", "70" });

            return dt;
        }

        private void FrmXtraGrid_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = GetTestData();
        }
    }
}