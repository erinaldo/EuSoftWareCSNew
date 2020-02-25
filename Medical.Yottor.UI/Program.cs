using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;
using System.Threading;

namespace Medical.Yottor.UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          //  ConfigHelper.GetConfig();
            CultureInfo ci = new CultureInfo("zh-hans");
            Application.CurrentCulture = ci;

            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.Skins.SkinManager.EnableMdiFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("Money Twins");

            SplashScreenManager.ShowForm(null, typeof(FrmSplash), true, true, false, 1000);
            for (int i = 1; i <= 200; i++)
            {
                //此处模仿初始加载
                SplashScreenManager.Default.SendCommand(FrmSplash.SplashScreenCommand.SetProgress, i);
                Thread.Sleep(25);
            }
            SplashScreenManager.CloseForm(false);

            DialogResult result = DialogResult.None;

            using (FrmLogin fl = new FrmLogin())
            {
                result = fl.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Application.Run(new MidMain());
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
