using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Ribbon;
using System.Reflection;

namespace Medical.Yottor.UI
{
    public partial class MidMain : DevExpress.XtraEditors.XtraForm
    {
        List<BarItem> AllowCustomizationMenuList = new List<BarItem>();
        EuSoft.BLL.SysZyb SysZyb = new EuSoft.BLL.SysZyb();
        EuSoft.BLL.SysZyb bSysZyb = new EuSoft.BLL.SysZyb();
        public MidMain()
        {
            InitializeComponent();
            GetMenuBind();
            ribbonControl1.Minimized = !ribbonControl1.Minimized;
            ShowOrActiveForm(typeof(FrmBackGround)); 
        }



        /// <summary>
        /// 动态加载菜单
        /// </summary>
        private void GetMenuBind()
        {

        this.ribbonControl1.Pages.Clear(); //清除所有选项卡

            SkinHelper.InitSkinGallery(ribbonGalleryBarItem1); //加载皮肤
            AllowCustomizationMenuList.Add(ribbonGalleryBarItem1);//加载皮肤到左上角
            ribbonControl1.Toolbar.ItemLinks.Add(ribbonGalleryBarItem1); //单击事件
            CheckFile();//检查文件
            GetXmlSkin();//获取xml主题
            UserLookAndFeel.Default.SetSkinStyle(defaultSkinName);//设置主题样式
            ribbonGalleryBarItem1.Caption = "主题：" + defaultSkinName;

           // DataTable dt = SysZyb.GetAllList().Tables[0];
            DataTable dt = bSysZyb.GetQXList(" c.UserName='" + Properties.Settings.Default.LastUser + "' and a.IsQx=1 order by a.ID  ");
            DataRow[] kpRows = dt.Select("PID=0");
            //根据登录用户角色菜单动态创建
            //循环创建卡菜单
            foreach (var kpDr in kpRows)
            {
                //创建卡
                RibbonPage ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();//菜单卡对象定义
                ribbonPage.Text = kpDr["XsName"].ToString();
                this.ribbonControl1.Pages.Add(ribbonPage);
                //创建组
                RibbonPageGroup ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();//菜单卡分组定义
                ribbonPageGroup.Text = kpDr["XsName"].ToString(); ;//卡片组名
                ribbonPage.Groups.Add(ribbonPageGroup);
                DataRow[] rows = dt.Select("PID=" + kpDr["ID"].ToString() + "","ID");
                foreach (DataRow dr in rows)
                {
                    BarButtonItem barButtonItem = new DevExpress.XtraBars.BarButtonItem();
                    barButtonItem.Caption = dr["XsName"].ToString();
                    barButtonItem.LargeGlyph = largeImgs.Images[Convert.ToInt16(dr["ImageIndex"].ToString())];// global::MemberManager.Properties.Resources.group_key;
                    barButtonItem.Name = dr["AnName"].ToString();
                    barButtonItem.Tag = dr["FrmName"].ToString();
                    ribbonPageGroup.ItemLinks.Add(barButtonItem);
                    barButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);//注册事件
                }
                //创建皮肤组

                RibbonPageGroup rpg = new RibbonPageGroup();
                rpg.Text = "界面皮肤";
                ribbonPage.Groups.Add(rpg);

                
                //皮肤列表
                RibbonGalleryBarItem rgb=new RibbonGalleryBarItem();
                rpg.ItemLinks.Add(rgb);
                SkinHelper.InitSkinGallery(rgb); //加载皮肤
                CheckFile();//检查文件
                GetXmlSkin();//获取xml主题
                UserLookAndFeel.Default.SetSkinStyle(defaultSkinName);//设置主题样式
                rgb.Caption = "主题：" + defaultSkinName;

                rgb.GalleryItemClick += new GalleryItemClickEventHandler(ribbonGalleryBarItem1_GalleryItemClick);
              //  rgb.ItemClick += new ItemClickEventHandler(OnSkinClick);//注册事件
            
            }
       }



        /// <summary>
        ///皮肤按钮事件
        /// </summary>
        private void OnSkinClick(object sender, ItemClickEventArgs e)
        {

            string name = string.Empty;
            string caption = string.Empty;
            if (ribbonGalleryBarItem1.Gallery == null) return;
            caption = ribbonGalleryBarItem1.Gallery.GetCheckedItems()[0].Caption;//主题的描述
            caption = caption.Replace("主题：", "");
            //name = bsiPaintStyle.Manager.PressedLink.Item.Tag.ToString();//主题的名称
            ribbonGalleryBarItem1.Caption = "主题：" + caption;
            XmlDocument doc = new XmlDocument();
            doc.Load("SkinInfo.xml");
            XmlNodeList nodelist = doc.SelectSingleNode("SetSkin").ChildNodes;
            foreach (XmlNode node in nodelist)
            {
                XmlElement xe = (XmlElement)node;//将子节点类型转换为XmlElement类型 
                if (xe.Name == "Skinstring")
                {
                    xe.InnerText = caption;
                }
            }
            doc.Save("SkinInfo.xml");
        }


        /// <summary>
        ///按钮单击事件 动态调出窗体事件 注：动态调用窗体名需和数据库中名称完全一致 
        /// </summary>
        private void barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {

            string strForm = e.Item.Tag.ToString();
            if (strForm=="")//退出
            {
                  this.Close();
                  return;
            }
            
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            // 获得窗体的名空间
           foreach (Form f in this.MdiChildren)
            {
                if (f.Name == strForm)
                {
                    f.Activate();
                    return;
                }
            }
            //根据窗体名称 反射取到该窗体
          Form form = (Form)Assembly.Load("CSUI").CreateInstance("Medical.Yottor.UI." + strForm);
          // 加载窗体
          xtraTabbedMdiManager1.MdiParent = this;   //设置控件的父表单..
          form.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
          // 显示窗体
          form.Show();
          xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[form];    //使得标签的选择为当前新建的窗口
          this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;    //设置标签后面添加删除按钮 ,  多个标签只需要设置一次..
        }
 
        public string defaultSkinName;//皮肤
        #region 检查XML文件是否存在
        public void CheckFile()
        {
            try
            {
                if (System.IO.File.Exists("SkinInfo.xml") == false)
                {
                    CreateXml();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 创建XML文件
        public void CreateXml()
        {
            XmlDocument doc = new XmlDocument();
            //建立xml定义声明
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            //创建根节点
            XmlElement root = doc.CreateElement("SetSkin");
            XmlElement rootone = doc.CreateElement("Skinstring");//皮肤
            //将one，two，插入到root节点下
            doc.AppendChild(root);
            root.AppendChild(rootone);
            doc.Save("SkinInfo.xml");
        }
        #endregion
        #region 读取Xml节点内容
        public void GetXmlSkin()
        {
            try
            {
                XmlDocument mydoc = new XmlDocument();
                mydoc.Load("SkinInfo.xml");
                XmlNode ressNode = mydoc.SelectSingleNode("SetSkin");
                defaultSkinName = ressNode.SelectSingleNode("Skinstring").InnerText;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #endregion
        private void ribbonGalleryBarItem1_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            string name = string.Empty;
            string caption = string.Empty;
            if (ribbonGalleryBarItem1.Gallery == null) return;
            caption = ribbonGalleryBarItem1.Gallery.GetCheckedItems()[0].Caption;//主题的描述
            caption = caption.Replace("主题：", "");
            //name = bsiPaintStyle.Manager.PressedLink.Item.Tag.ToString();//主题的名称
            ribbonGalleryBarItem1.Caption = "主题：" + caption;
            XmlDocument doc = new XmlDocument();
            doc.Load("SkinInfo.xml");
            XmlNodeList nodelist = doc.SelectSingleNode("SetSkin").ChildNodes;
            foreach (XmlNode node in nodelist)
            {
                XmlElement xe = (XmlElement)node;//将子节点类型转换为XmlElement类型 
                if (xe.Name == "Skinstring")
                {
                    xe.InnerText = caption;
                }
            }
            doc.Save("SkinInfo.xml");
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnCd_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmOrderInfor));
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
            //form.MdiParent = this;
            //// form.FormBorderStyle = FormBorderStyle.None;//无边框
            //form.WindowState = FormWindowState.Maximized;//子窗体最大化
            //form.Parent = this.panelControl1;//指定子窗体的父容器为panelControl1
            //form.Show();

            // 获得窗体的名空间
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == type.ToString())
                {
                    f.Activate();
                    return;
                }
            }

            xtraTabbedMdiManager1.MdiParent = this;   //设置控件的父表单..
            form.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
            form.Show();
            xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[form];    //使得标签的选择为当前新建的窗口
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;    //设置标签后面添加删除按钮 ,  多个标签只需要设置一次..
      
        }

     



        private void MidMain_Load(object sender, EventArgs e)
        {

        }

        private void btnGn_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
           ShowOrActiveForm(typeof(FrmShip));
           // ShowOrActiveForm(typeof(FrmGridControlColor));
            
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
             SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmUser));
            
        }
        /// <summary>
        /// 工具栏合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager1_Merge(object sender, BarManagerMergeEventArgs e)
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
        private void barManager1_UnMerge(object sender, BarManagerMergeEventArgs e)
        {
            barManager1.Bars["Tools"].UnMerge();
        }
        /// <summary>
        /// 关闭文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMdiTabPage page = xtraTabbedMdiManager1.SelectedPage;
            if (page != null)
                page.MdiChild.Close();
        }
        /// <summary>
        /// 关闭除此之外所有文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseAllExcept_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMdiTabPage page = xtraTabbedMdiManager1.SelectedPage;
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
        private void btnCloseAll_ItemClick(object sender, ItemClickEventArgs e)
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
        private void xtraTabbedMdiManager1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                XtraMdiTabPage page = xtraTabbedMdiManager1.CalcHitInfo(new Point(e.X, e.Y)).Page as XtraMdiTabPage;
                if (page != null)
                {
                    if (xtraTabbedMdiManager1.SelectedPage != page)
                        xtraTabbedMdiManager1.SelectedPage = page;
                    pmTabbed.ShowPopup(Control.MousePosition);
                }
            }
        }

        public void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            //for (int i = 1; i <= 100; i++)
            //{
            //    SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
            //    SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
            //    System.Threading.Thread.Sleep(10);
            //}
            //SplashScreenManager.CloseForm(false);
            //FrmOrder xFrm = new FrmOrder();
            //xFrm.Show();
            ShowOrActiveForm(typeof(FrmOrder));
            
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            
           ShowOrActiveForm(typeof(FrmCustomer));
           //FrmCustomer xFrm = new FrmCustomer();
           //xFrm.Show();
        }

        private void barSubItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
             SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmOrderProductsInfor));
            
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(FrmWait), true, true, false);
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormCaption("请稍候... ...");
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                System.Threading.Thread.Sleep(10);
            }
            SplashScreenManager.CloseForm(false);
            ShowOrActiveForm(typeof(FrmOrderProductsInfor));
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmStockForSalse));
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmProductsFromSH));
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmInventoryMaintenance));
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmInventory));
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmInvoice));
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmTracking));
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmPaymentInformation));
            
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
          ShowOrActiveForm(typeof(FrmCoaInfor));
          

            //FrmCoaInfor xFrm = new FrmCoaInfor();

            //if (xFrm.ShowDialog() == DialogResult.OK)
            //{
                
            //}
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmSLibraries));
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmSalesStatistics));
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
          ShowOrActiveForm(typeof(FrmStockInformation));

        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmAllOrderInfor));
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmDateDownLoad));
            
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {

            ShowOrActiveForm(typeof(FrmCd));
            //FrmCoaInfor xFrm = new FrmCoaInfor();

            //FrmCd xFrm = new FrmCd();
            //if (xFrm.ShowDialog() == DialogResult.OK)
            //{

            //}
        }

        private void barButtonItem6_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ShowOrActiveForm(typeof(FrmBackGround)); 
        }
     
   
       

    }

}


