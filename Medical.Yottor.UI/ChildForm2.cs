using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System.IO;
using Agile.Report;

namespace Medical.Yottor.UI
{
    public partial class ChildForm2 : DevExpress.XtraEditors.XtraForm
    {
        public ChildForm2()
        {
            InitializeComponent();
          
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.Show(string.Format("窗体[{0}]的<{1}>按钮事件触发.", this.Name, "系统调试"));
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.Show(string.Format("窗体[{0}]的<{1}>按钮事件触发.", this.Name, "部门设置"));
        }

        private void btn_Design_ItemClick(object sender, ItemClickEventArgs e)
        {
          
        }

        private void btn_Preview_ItemClick(object sender, ItemClickEventArgs e)
        {
            var report = this.Prepare();
            report.Preview();
        }

        private void btn_Print_ItemClick(object sender, ItemClickEventArgs e)
        {
            var report = this.Prepare();
            report.Print();
        }

        private ReportEx Prepare()
        {
            ReportEx report = new ReportEx();
            report.AddDataSource(new DataTable(),"Class");
            report.AddDataSource(new DataTable(), "Student");
            report.AddParameter("参数1", "FastFrameWork 快速开发框架");
            report.AddParameter("参数2", DateTime.Now);
            report.LoadFrom(Path.Combine(Application.StartupPath, "Report", "test.frx"));
            return report;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var report = this.Prepare();
            report.Design();
        }

        private void ChildForm2_Load(object sender, EventArgs e)
        {

        }

    }
}