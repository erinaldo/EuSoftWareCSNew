namespace Medical.Yottor.UI
{
    partial class FrmConsole
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsole));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSkin1 = new System.Windows.Forms.ToolStripButton();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.mdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.pmTabbed = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.btnCloseAllExcept = new DevExpress.XtraBars.BarButtonItem();
            this.btnCloseAll = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bsiSys = new DevExpress.XtraBars.BarSubItem();
            this.biClose = new DevExpress.XtraBars.BarButtonItem();
            this.biCloseAll = new DevExpress.XtraBars.BarButtonItem();
            this.biCloseAllExcept = new DevExpress.XtraBars.BarButtonItem();
            this.bsiSkin = new DevExpress.XtraBars.BarSubItem();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.pmSkin = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnSkin = new System.Windows.Forms.ToolStripMenuItem();
            this.btnQuanX = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTest = new DevExpress.XtraNavBar.NavBarItem();
            this.btnTest1 = new DevExpress.XtraNavBar.NavBarItem();
            this.btnCharts = new DevExpress.XtraNavBar.NavBarItem();
            this.btnTreeList = new DevExpress.XtraNavBar.NavBarItem();
            this.btnLayoutControl = new DevExpress.XtraNavBar.NavBarItem();
            this.btnLookUpEdit = new DevExpress.XtraNavBar.NavBarItem();
            this.btnPopupMenu = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem3 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem4 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem5 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem6 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem7 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem8 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem9 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem10 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem11 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmTabbed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmSkin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(214)))), ((int)(((byte)(251)))));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSkin1,
            this.btnHelp,
            this.btnAbout});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1042, 40);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSkin1
            // 
            this.btnSkin1.Image = ((System.Drawing.Image)(resources.GetObject("btnSkin1.Image")));
            this.btnSkin1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSkin1.Name = "btnSkin1";
            this.btnSkin1.Size = new System.Drawing.Size(91, 37);
            this.btnSkin1.Text = "更换皮肤(&S)";
            this.btnSkin1.Click += new System.EventHandler(this.btnSkin_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(69, 37);
            this.btnHelp.Text = "帮助(&H)";
            // 
            // btnAbout
            // 
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(52, 37);
            this.btnAbout.Text = "关于";
            // 
            // mdiManager1
            // 
            this.mdiManager1.MdiParent = this;
            this.mdiManager1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mdiManager1_MouseUp);
            // 
            // pmTabbed
            // 
            this.pmTabbed.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnClose),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCloseAllExcept),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCloseAll)});
            this.pmTabbed.Manager = this.barManager1;
            this.pmTabbed.Name = "pmTabbed";
            // 
            // btnClose
            // 
            this.btnClose.Caption = "关闭";
            this.btnClose.Id = 5;
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // btnCloseAllExcept
            // 
            this.btnCloseAllExcept.Caption = "除此之外关闭所有";
            this.btnCloseAllExcept.Id = 6;
            this.btnCloseAllExcept.Name = "btnCloseAllExcept";
            this.btnCloseAllExcept.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCloseAllExcept_ItemClick);
            // 
            // btnCloseAll
            // 
            this.btnCloseAll.Caption = "关闭所有文档";
            this.btnCloseAll.Id = 7;
            this.btnCloseAll.Name = "btnCloseAll";
            this.btnCloseAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCloseAll_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsiSys,
            this.biClose,
            this.biCloseAll,
            this.biCloseAllExcept,
            this.bsiSkin,
            this.btnClose,
            this.btnCloseAllExcept,
            this.btnCloseAll});
            this.barManager1.MaxItemId = 8;
            this.barManager1.StatusBar = this.bar3;
            this.barManager1.Merge += new DevExpress.XtraBars.BarManagerMergeEventHandler(this.barManager1_Merge);
            this.barManager1.UnMerge += new DevExpress.XtraBars.BarManagerMergeEventHandler(this.barManager1_UnMerge);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "状态栏";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1042, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 569);
            this.barDockControlBottom.Size = new System.Drawing.Size(1042, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 569);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1042, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 569);
            // 
            // bsiSys
            // 
            this.bsiSys.Caption = "系统(&S)";
            this.bsiSys.Id = 0;
            this.bsiSys.Name = "bsiSys";
            // 
            // biClose
            // 
            this.biClose.Caption = "关闭";
            this.biClose.Id = 1;
            this.biClose.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4));
            this.biClose.Name = "biClose";
            // 
            // biCloseAll
            // 
            this.biCloseAll.Caption = "关闭所有文档(&L)";
            this.biCloseAll.Id = 2;
            this.biCloseAll.Name = "biCloseAll";
            // 
            // biCloseAllExcept
            // 
            this.biCloseAllExcept.Caption = "除此之外全部关闭(&A)";
            this.biCloseAllExcept.Id = 3;
            this.biCloseAllExcept.Name = "biCloseAllExcept";
            // 
            // bsiSkin
            // 
            this.bsiSkin.Caption = "样式(&V)";
            this.bsiSkin.Id = 4;
            this.bsiSkin.Name = "bsiSkin";
            // 
            // barManager2
            // 
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.MaxItemId = 0;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(1042, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 592);
            this.barDockControl2.Size = new System.Drawing.Size(1042, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Size = new System.Drawing.Size(0, 592);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(1042, 0);
            this.barDockControl4.Size = new System.Drawing.Size(0, 592);
            // 
            // pmSkin
            // 
            this.pmSkin.Manager = this.barManager1;
            this.pmSkin.Name = "pmSkin";
            // 
            // btnSkin
            // 
            this.btnSkin.Name = "btnSkin";
            this.btnSkin.Size = new System.Drawing.Size(152, 22);
            this.btnSkin.Text = "皮肤";
            this.btnSkin.Click += new System.EventHandler(this.btnSkin_Click);
            // 
            // btnQuanX
            // 
            this.btnQuanX.Name = "btnQuanX";
            this.btnQuanX.Size = new System.Drawing.Size(152, 22);
            this.btnQuanX.Text = "权限设置";
            this.btnQuanX.Click += new System.EventHandler(this.btnQuanX_Click);
            // 
            // btnTest
            // 
            this.btnTest.Caption = "人员信息";
            this.btnTest.Name = "btnTest";
            this.btnTest.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnTest.SmallImage")));
            this.btnTest.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btnTest_LinkClicked);
            // 
            // btnTest1
            // 
            this.btnTest1.Caption = "权限设置";
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnTest1.SmallImage")));
            this.btnTest1.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btnTest1_LinkClicked);
            // 
            // btnCharts
            // 
            this.btnCharts.Caption = "ChartControl 控件";
            this.btnCharts.Name = "btnCharts";
            this.btnCharts.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnCharts.SmallImage")));
            this.btnCharts.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btnCharts_LinkClicked);
            // 
            // btnTreeList
            // 
            this.btnTreeList.Caption = "TreeList 控件";
            this.btnTreeList.Name = "btnTreeList";
            this.btnTreeList.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnTreeList.SmallImage")));
            this.btnTreeList.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btnTreeList_LinkClicked);
            // 
            // btnLayoutControl
            // 
            this.btnLayoutControl.Caption = "LayoutControl 控件";
            this.btnLayoutControl.Name = "btnLayoutControl";
            this.btnLayoutControl.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnLayoutControl.SmallImage")));
            this.btnLayoutControl.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btnLayoutControl_LinkClicked);
            // 
            // btnLookUpEdit
            // 
            this.btnLookUpEdit.Caption = "LookUpEdit 控件";
            this.btnLookUpEdit.Name = "btnLookUpEdit";
            this.btnLookUpEdit.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnLookUpEdit.SmallImage")));
            this.btnLookUpEdit.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btnLookUpEdit_LinkClicked);
            // 
            // btnPopupMenu
            // 
            this.btnPopupMenu.Caption = "PopupMenu 控件";
            this.btnPopupMenu.Name = "btnPopupMenu";
            this.btnPopupMenu.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnPopupMenu.SmallImage")));
            this.btnPopupMenu.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btnPopupMenu_LinkClicked);
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "XtraGrid 控件分组";
            this.navBarItem1.Name = "navBarItem1";
            this.navBarItem1.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem1.SmallImage")));
            this.navBarItem1.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem1_LinkClicked);
            // 
            // navBarItem2
            // 
            this.navBarItem2.Caption = "NPOI 控件";
            this.navBarItem2.Name = "navBarItem2";
            this.navBarItem2.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem2.SmallImage")));
            this.navBarItem2.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem2_LinkClicked);
            // 
            // navBarItem3
            // 
            this.navBarItem3.Caption = "Html / PDF 操作";
            this.navBarItem3.Name = "navBarItem3";
            this.navBarItem3.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem3.SmallImage")));
            this.navBarItem3.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem3_LinkClicked);
            // 
            // navBarItem4
            // 
            this.navBarItem4.Caption = "DataAccess";
            this.navBarItem4.Name = "navBarItem4";
            this.navBarItem4.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem4.SmallImage")));
            this.navBarItem4.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem4_LinkClicked);
            // 
            // navBarItem5
            // 
            this.navBarItem5.Caption = "FTP 工具类";
            this.navBarItem5.Name = "navBarItem5";
            this.navBarItem5.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem5.SmallImage")));
            this.navBarItem5.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem5_LinkClicked);
            // 
            // navBarItem6
            // 
            this.navBarItem6.Caption = "API 接口服务请求";
            this.navBarItem6.Name = "navBarItem6";
            this.navBarItem6.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem6.SmallImage")));
            this.navBarItem6.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem6_LinkClicked);
            // 
            // navBarItem7
            // 
            this.navBarItem7.Caption = "XML 操作";
            this.navBarItem7.Name = "navBarItem7";
            this.navBarItem7.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem7.SmallImage")));
            this.navBarItem7.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem7_LinkClicked);
            // 
            // navBarItem8
            // 
            this.navBarItem8.Caption = "Date 公共类";
            this.navBarItem8.Name = "navBarItem8";
            this.navBarItem8.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem8.SmallImage")));
            this.navBarItem8.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem8_LinkClicked);
            // 
            // navBarItem9
            // 
            this.navBarItem9.Caption = "XtraScheduler 控件";
            this.navBarItem9.Name = "navBarItem9";
            this.navBarItem9.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem9.SmallImage")));
            this.navBarItem9.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem9_LinkClicked);
            // 
            // navBarItem10
            // 
            this.navBarItem10.Caption = "GridControl单元格Color";
            this.navBarItem10.Name = "navBarItem10";
            this.navBarItem10.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem10.SmallImage")));
            this.navBarItem10.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem10_LinkClicked);
            // 
            // navBarItem11
            // 
            this.navBarItem11.Caption = "FastReport";
            this.navBarItem11.Name = "navBarItem11";
            this.navBarItem11.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem11.SmallImage")));
            this.navBarItem11.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem11_LinkClicked);
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "工具箱";
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.btnCharts),
            new DevExpress.XtraNavBar.NavBarItemLink(this.btnTreeList),
            new DevExpress.XtraNavBar.NavBarItemLink(this.btnLayoutControl),
            new DevExpress.XtraNavBar.NavBarItemLink(this.btnLookUpEdit),
            new DevExpress.XtraNavBar.NavBarItemLink(this.btnPopupMenu),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem1),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem2),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem3),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem4),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem5),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem6),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem7),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem8),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem9),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem10),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem11)});
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup3;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup3});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.btnTest,
            this.btnTest1,
            this.btnCharts,
            this.btnTreeList,
            this.btnLayoutControl,
            this.btnLookUpEdit,
            this.btnPopupMenu,
            this.navBarItem1,
            this.navBarItem2,
            this.navBarItem3,
            this.navBarItem4,
            this.navBarItem5,
            this.navBarItem6,
            this.navBarItem7,
            this.navBarItem8,
            this.navBarItem9,
            this.navBarItem10,
            this.navBarItem11});
            this.navBarControl1.Location = new System.Drawing.Point(0, 40);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 197;
            this.navBarControl1.Size = new System.Drawing.Size(197, 529);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.Click += new System.EventHandler(this.navBarControl1_Click);
            // 
            // FrmConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 592);
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.IsMdiContainer = true;
            this.Name = "FrmConsole";
            this.Text = "主控台";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmTabbed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmSkin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager mdiManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu pmTabbed;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarSubItem bsiSys;
        private DevExpress.XtraBars.BarButtonItem biClose;
        private DevExpress.XtraBars.BarButtonItem biCloseAll;
        private DevExpress.XtraBars.BarButtonItem biCloseAllExcept;
        private DevExpress.XtraBars.BarSubItem bsiSkin;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.BarButtonItem btnCloseAllExcept;
        private DevExpress.XtraBars.BarButtonItem btnCloseAll;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.PopupMenu pmSkin;
        private System.Windows.Forms.ToolStripButton btnSkin1;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.ToolStripMenuItem btnSkin;
        private System.Windows.Forms.ToolStripMenuItem btnQuanX;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarItem btnCharts;
        private DevExpress.XtraNavBar.NavBarItem btnTreeList;
        private DevExpress.XtraNavBar.NavBarItem btnLayoutControl;
        private DevExpress.XtraNavBar.NavBarItem btnLookUpEdit;
        private DevExpress.XtraNavBar.NavBarItem btnPopupMenu;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;
        private DevExpress.XtraNavBar.NavBarItem navBarItem3;
        private DevExpress.XtraNavBar.NavBarItem navBarItem4;
        private DevExpress.XtraNavBar.NavBarItem navBarItem5;
        private DevExpress.XtraNavBar.NavBarItem navBarItem6;
        private DevExpress.XtraNavBar.NavBarItem navBarItem7;
        private DevExpress.XtraNavBar.NavBarItem navBarItem8;
        private DevExpress.XtraNavBar.NavBarItem navBarItem9;
        private DevExpress.XtraNavBar.NavBarItem navBarItem10;
        private DevExpress.XtraNavBar.NavBarItem navBarItem11;
        private DevExpress.XtraNavBar.NavBarItem btnTest;
        private DevExpress.XtraNavBar.NavBarItem btnTest1;
        internal DevExpress.XtraBars.Bar bar3;
    }
}