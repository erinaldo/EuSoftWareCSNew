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

    public partial class FrmMoreEmail : DevExpress.XtraEditors.XtraForm
    {
        EuSoft.BLL.MCEOrderPerson bMCEOrderPerson = new EuSoft.BLL.MCEOrderPerson();
        EuSoft.Model.MCEOrderPerson mMCEOrderPerson = new EuSoft.Model.MCEOrderPerson();
        public FrmMoreEmail(string orderNO)
        {
            InitializeComponent();
            labOrderNo.Text = orderNO;
            getDate(labOrderNo.Text);
        }

        public void getDate(string orderNO)
        {
            gridControl1.DataSource = bMCEOrderPerson.GetList("orderno='" + orderNO + "' ").Tables[0];
            cobType.Properties.Items.Clear();
            cobType.Properties.Items.Add("Contact Persons");
            cobType.Properties.Items.Add("Related Persons");
            cobType.SelectedIndex = 0;
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            mMCEOrderPerson.ContactName = txtContactName.Text;
            mMCEOrderPerson.email = txtEmail.Text;
            mMCEOrderPerson.Note = txtEmail.Text+" "+cobType.Text;
            mMCEOrderPerson.OrderNo = labOrderNo.Text;
            if (bMCEOrderPerson.Add(mMCEOrderPerson)>0)
            {
                 MessageDxUtil.ShowTips("success");
                 getDate(labOrderNo.Text);
            }

        }

        private void ButDel_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle>-1)
            {
                    int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]));
                    if (bMCEOrderPerson.Delete(id))
                    {
                        MessageDxUtil.ShowTips("success");
                        getDate(labOrderNo.Text);
                    }
            }
        }
    }
}
