using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Domain;

namespace Medical.Yottor.UI
{
    public partial class FrmDate : DevExpress.XtraEditors.XtraForm
    {
        public FrmDate()
        {
            InitializeComponent();
        }

        private void btnTEST_Click(object sender, EventArgs e)
        {
          /*  StringBuilder sb = new StringBuilder();

            //01 本年的天数
            int days = DateUtil.GetDaysOfYear(2016);
            sb.Append(String.Format("2016 年有 {0}天。\r\n", days));

            //02 本月的天数
            int monthdays = DateUtil.GetDaysOfMonth(2016, 7);
            sb.Append(String.Format("2016 年 7 月 有 {0}天。\r\n", monthdays));

            //03 返回当前日期的星期名称
            DateTime dt = new DateTime(2016, 3, 15);
            string strweek = DateUtil.GetWeekNameOfDay(dt);
            sb.Append(String.Format("2016 年 3 月 15 日 是 {0}。\r\n", strweek));

            //04 获取某一年有多少周
            int weekamount = DateUtil.GetWeekAmount(2016);
            sb.Append(String.Format("2016 年 总共有 {0} 周。\r\n", weekamount));

            //05 获取某一日期是该年中的第几周
             int weekyear = DateUtil.GetWeekOfYear(dt);
             sb.Append(String.Format("2016 年 3 月 15 日 是 第 {0} 周。\r\n", weekyear));

            //06 根据某年的第几周获取这周的起止日期
             DateTime firstDate = DateTime.Now , lastDate = DateTime.Now;
             DateUtil.WeekRange(2016, 18,ref firstDate, ref lastDate);
             sb.Append(String.Format("2016 年 第 18 周的日期区间是：{0} - {1}。\r\n", firstDate.ToString("yyyy-MM-dd"), lastDate.ToString("yyyy-MM-dd")));

            //07 返回两个日期之间相差的天数
             int daysyear = DateUtil.DiffDays(new DateTime(2013, 5, 28), new DateTime(2016, 2, 10));
             sb.Append(String.Format("2013 年 5 月 28日 至 ：2016 年 2 月 10 日 相差 {0} 天。\r\n", daysyear));

             this.memoEdit1.Text = sb.ToString(); */
        }
    }
}
