using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.LookAndFeel;

namespace Medical.Yottor.UI
{
    public partial class FrmSplash : SplashScreen
    {
        private UserLookAndFeel lookAndFeel;

        public FrmSplash()
        {
            InitializeComponent();
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            SplashScreenCommand command = (SplashScreenCommand)cmd;
            if (command == SplashScreenCommand.SetProgress)
            {
                int pos = (int)arg;
                this.labelControl1.Text = pos.ToString();
            }
        }

        protected override UserLookAndFeel TargetLookAndFeel
        {
            get
            {
                if (lookAndFeel == null)
                {
                    lookAndFeel = new UserLookAndFeel(this);
                    lookAndFeel.UseDefaultLookAndFeel = false;
                    lookAndFeel.SkinName = "Office 2010 Black";
                }
                return lookAndFeel;
            }
        }

        public enum SplashScreenCommand
        {
            SetProgress,
            Command2,
            Command3
        }
    }
}