using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Medical.Yottor.UI
{
    public static class MsgBox
    {
        // 消息
        public static void ShowInformation(string text, string caption = "提示")
        {
            XtraMessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 提醒
        public static void ShowExclamation(string text, string caption = "提示")
        {
            XtraMessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // 错误
        public static void ShowError(string text, string caption = "提示")
        {
            XtraMessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // 是OR否
        public static DialogResult ShowYesNo(string text, string caption = "请确认选择")
        {
            return XtraMessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
