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
    public partial class FrmLayoutControl : DevExpress.XtraEditors.XtraForm
    {
        public FrmLayoutControl()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (btnShow.Text == "显示")
            {
                btnShow.Text = "隐藏";
                layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if (btnShow.Text == "隐藏")
            {
                btnShow.Text = "显示";
                layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
    }
}