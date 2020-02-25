using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Medical.Yottor.UI
{
    public partial class FrmBackGround : Form
    {
        public FrmBackGround()
        {
            InitializeComponent();
        }

        private void labelControl10_Click(object sender, EventArgs e)
        {
            FrmCustomer form = new FrmCustomer();
            form.MdiParent = this.MdiParent; //父窗体相同
            form.Show();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {
            FrmProductsFromSH form = new FrmProductsFromSH();
            form.MdiParent = this.MdiParent; //父窗体相同
            form.Show();
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {
            FrmInventoryMaintenance form = new FrmInventoryMaintenance();
            form.MdiParent = this.MdiParent; //父窗体相同
            form.Show();
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {
            FrmOrder form = new FrmOrder();
          //  FrmGridControlColor form = new FrmGridControlColor();
            form.MdiParent = this.MdiParent; //父窗体相同
            form.Show();
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {
            FrmShip form = new FrmShip();
            form.MdiParent = this.MdiParent; //父窗体相同
            form.Show();
        }

        private void labelControl9_Click(object sender, EventArgs e)
        {
            FrmStockForSalse form = new FrmStockForSalse();
            
            form.MdiParent = this.MdiParent; //父窗体相同
            form.Show();
        }

        private void labelControl15_Click(object sender, EventArgs e)
        {
            FrmInvoice form = new FrmInvoice();
            form.MdiParent = this.MdiParent; //父窗体相同
            form.Show();
        }

        private void labelControl16_Click(object sender, EventArgs e)
        {
            FrmPaymentInformation form = new FrmPaymentInformation();
            form.MdiParent = this.MdiParent; //父窗体相同
            form.Show();
        }

        private void labelControl17_Click(object sender, EventArgs e)
        {
            FrmCoaInfor form = new FrmCoaInfor();
            form.MdiParent = this.MdiParent; //父窗体相同
            form.Show();
        }

        private void labelControl3_Click_1(object sender, EventArgs e)
        {

            frmPrintLable form = new frmPrintLable();
            form.MdiParent = this.MdiParent; //父窗体相同
            form.Show();
        }

      
    }
}
