using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraWaitForm;

namespace Medical.Yottor.UI
{
    public partial class FrmWait : WaitForm
    {
        public FrmWait()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

         #region WaitForm Demo...
         /*  SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
           
            for (int i = 1; i <= 100; i++)
            {
                //SplashScreenManager.Default.SetWaitFormCaption("请稍候");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(50);
            }
            SplashScreenManager.CloseForm(false); */
            #endregion
    }
}