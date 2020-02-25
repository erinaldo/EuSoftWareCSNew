using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars;
using DevExpress.LookAndFeel;

namespace Medical.Yottor.UI
{
    public partial class FrmConsole : DevExpress.XtraEditors.XtraForm
    {
        private Dictionary<string, string> _skins = new Dictionary<string, string>
        {
            {"DevExpress Style","默认主题"},
            {"DevExpress Dark Style","默认(黑)"},
            {"Sharp","尖锐"},
            {"Sharp Plus","尖锐（增强）"},
            {"Caramel","焦糖"},
            {"Lilian","莉莲"},
            {"Money Twins","财富"},
            {"Office 2013 Dark Gray","Office 2013"},
             {"Office 2010 silver","Office 2010 silver"},
        };

        public FrmConsole()
        {
            InitializeComponent();

            InitSkins();
        }

        public void InitSkins()
        {
            barManager2.ForceInitialize();
            foreach (KeyValuePair<string, string> item in _skins)
            {
                BarCheckItem bi = new BarCheckItem(barManager2, false);
                bi.Caption = item.Value;
                bi.Hint = item.Key;
                bi.GroupIndex = 1;
                if (item.Key.Equals(UserLookAndFeel.Default.SkinName))
                    bi.Checked = true;
                pmSkin.AddItem(bi);
                bi.ItemClick += OnSkinClick;
            }
        }

        void OnSkinClick(object sender, ItemClickEventArgs e)
        {
            UserLookAndFeel.Default.SetSkinStyle(e.Item.Hint);
            barManager1.GetController().PaintStyleName = "Skin";
        }

        /// <summary>
        /// 显示MDI窗体(单例模式)
        /// </summary>
        /// <param name="type"></param>
        private void ShowOrActiveForm(Type type)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == type)
                {
                    f.Activate();
                    return;
                }
            }
            Form form = type.Assembly.CreateInstance(type.ToString()) as Form;
            form.MdiParent = this;
            form.Show();
        }

        /// <summary>
        /// 关闭文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMdiTabPage page = mdiManager1.SelectedPage;
            if (page != null)
                page.MdiChild.Close();
        }

        /// <summary>
        /// 关闭除此之外所有文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseAllExcept_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMdiTabPage page = mdiManager1.SelectedPage;
            foreach (Form f in this.MdiChildren)
            {
                if (f != page.MdiChild)
                {
                    f.Close();
                    f.Dispose();
                }
            }
        }

        /// <summary>
        /// 关闭所有文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
        }

        /// <summary>
        /// 文档关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mdiManager1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                XtraMdiTabPage page = mdiManager1.CalcHitInfo(new Point(e.X, e.Y)).Page as XtraMdiTabPage;
                if (page != null)
                {
                    if (mdiManager1.SelectedPage != page)
                        mdiManager1.SelectedPage = page;
                    pmTabbed.ShowPopup(Control.MousePosition);
                }
            }
        }

        /// <summary>
        /// 工具栏合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager1_Merge(object sender, DevExpress.XtraBars.BarManagerMergeEventArgs e)
        {
            var parent = barManager1.Bars["Tools"];
            var child = e.ChildManager.Bars["Tools"];
            if (child != null)
                parent.Merge(child);
        }

        /// <summary>
        /// 工具栏非合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager1_UnMerge(object sender, DevExpress.XtraBars.BarManagerMergeEventArgs e)
        {
            barManager1.Bars["Tools"].UnMerge();
        }

        private void btnTest_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
           // ShowOrActiveForm(typeof(FrmUser));
        }

        private void btnTest1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmAuthority));
        }

        /// <summary>
        /// 皮肤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSkin_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Point pos = new Point(10,50);
            pmSkin.ShowPopup(PointToScreen(pos));
        }

        private void btnQuanX_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmAuthority));
        }

        private void btnCharts_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmCharts));
        }

        private void btnTreeList_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmTreeList));
        }

        private void btnLayoutControl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmLayoutControl));
        }

        private void btnLookUpEdit_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
           // ShowOrActiveForm(typeof(FrmLookUpEdit));
        }

        private void btnPopupMenu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmPopupMenu));
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmXtraGrid));
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(frmNPOI));
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmHtml));
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmDataAccess));
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmFTP));
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmAPIService));
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmXML));
        }

        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmDate));
        }

        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
           // ShowOrActiveForm(typeof(FrmXtraScheduler));
        }

        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmGridControlColor));
        }

        private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(ChildForm2));
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }
    }
}