using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
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
    public partial class FrmCustomer : Form
    {
        EuSoft.BLL.MCECustomersInfo bMcus = new EuSoft.BLL.MCECustomersInfo();
        EuSoft.Model.MCECustomersInfo mMcus = new EuSoft.Model.MCECustomersInfo();
        EuSoft.BLL.MCEEmailPromotion bMeep = new EuSoft.BLL.MCEEmailPromotion();
        EuSoft.BLL.MCECountryDefinition bMCountry = new EuSoft.BLL.MCECountryDefinition();
        EuSoft.BLL.MCEPayMethodDefinition bMPay = new EuSoft.BLL.MCEPayMethodDefinition();
        EuSoft.BLL.MCEStateDefinition bMsa = new EuSoft.BLL.MCEStateDefinition();
        EuSoft.BLL.MCECarrierDefinition bMcu = new EuSoft.BLL.MCECarrierDefinition();
        public FrmCustomer()
        {
            InitializeComponent();
            GetDate();
            cobNoteCarrier.Text = "FedEx";
            
        }

        private void GetDate()
        { 
            
            DataTable dtCountry=new DataTable();
            dtCountry=bMCountry.GetAllList().Tables[0];
            BindComboBoxEdit(cobSalesCountry, dtCountry, 1);
            BindComboBoxEdit(cobBillCountry, dtCountry, 1);
            BindComboBoxEdit(cobShipCountry, dtCountry, 1);

            DataTable dtState = new DataTable();
            dtState = bMsa.GetAllList().Tables[0];
            BindComboBoxEdit(cobShipState, dtState,2);
            BindComboBoxEdit(cobBillState, dtState, 2);
            BindComboBoxEdit(cobSalesState, dtState, 2);

            DataTable dtPayMethod = new DataTable();
            dtPayMethod = bMPay.GetAllList().Tables[0];
            BindComboBoxEdit(cobPayMethod, dtPayMethod, 1);

            DataTable dtCarrier = new DataTable();
            dtCarrier = bMcu.GetAllList().Tables[0];
            BindComboBoxEdit(cobNoteCarrier, dtCarrier, 1);

            DataTable dtEmail = new DataTable();
            dtEmail = bMeep.GetAllList().Tables[0];
            BindComboBoxEdit(cobNoteMailPromotion, dtEmail, 1);


           
            gridControl1.DataSource = bMcus.GetList(30,"","id desc").Tables[0];
    
        }

        /// <summary>
        /// 绑定ComBoBoxEdit下拉框数据  
        /// </summary>
        /// <param name="cmb">ComBoBoxEdit控件</param>
        /// <param name="dt">数据源</param>
        /// <param name="key">显示名称</param>
        /// <param name="val">对应EditValue值</param>
        public static void BindComboBoxEdit(ComboBoxEdit cmb, DataTable dt,  int val)
        {
            cmb.Properties.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmb.Properties.Items.Add(dr[val].ToString());
            }
            if (cmb.Properties.Items.Count > 0)
                cmb.SelectedIndex = 0;
        }

        private void ButSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.Append(" 1=1 ");
            if (txtCompany.Text!="")
            {
                sqlWhere.AppendFormat(" and SalesCompany like '%{0}%'",txtCompany.Text);
            }
            if (txtCountry.Text!="")
            {
                sqlWhere.AppendFormat(" and SalesCountry like '%{0}%' ",txtCountry.Text);
            }
            if (txtContactName.Text != "")
            {
                sqlWhere.AppendFormat("  and ([BillContactName] like '%{0}%' or [ShipContactName] like '%{0}%' or [SalesContactName] like '%{0}%') ",txtContactName.Text);
            }
            if (txtBillTo.Text!="")
            {
                sqlWhere.AppendFormat(" and ([BillCompany] like '%{0}%' or [BillContactName] like '%{0}%' or [BillStreet] like '%{0}%' or [BillCity] like '%{0}%' or [BillState] like '%{0}%' or [BillZip] like '%{0}%' or [BillCountry] like '%{0}%' or [BillTel] like '%{0}%' or [BillFax] like '%{0}%' or [BilleMail] like '%{0}%')",txtBillTo.Text);
            }
            if (txtShipto.Text!="")
            {
                sqlWhere.AppendFormat(" and ([ShipCompany] like '%{0}%' or [ShipContactName] like '%{0}%' or [ShipStreet] like '%{0}%' or [ShipCity] like '%{0}%' or [ShipState] like '%{0}%' or [ShipZip] like '%{0}%' or [ShipCountry] like '%{0}%' or [ShipTel] like '%{0}%' or [ShipFax] like '%{0}%' or [ShipeMail] like '%{0}%')", txtShipto.Text);
            }

            dt = bMcus.GetList(sqlWhere.ToString()).Tables[0];
            gridControl1.DataSource = dt;
        }
        /// <summary>
        /// 清空文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      

        private void btuClear_Click(object sender, EventArgs e)
        {
            txtCompany.Text = "";
            txtBillTo.Text = "";
            txtContactName.Text = "";
            txtCountry.Text = "";
            txtShipto.Text = "";
        }

      

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]).ToString());
              
                mMcus = bMcus.GetModel(id);
                txtSalesCompany.Text = mMcus.SalesCompany;
                txtSalesContactName.Text = mMcus.SalesContactName;
                txtSalesEmail.Text = mMcus.SaleseMail;
                txtSalesFax.Text = mMcus.SalesFax;
                txtSalesStreet.Text = mMcus.SalesStreet;
                txtSalesTel.Text = mMcus.SalesTel;
                txtSalesCity.Text = mMcus.SalesCity;
                cobSalesCountry.Text = mMcus.SalesCountry;
                cobSalesState.Text = mMcus.SalesState;
                txtSalesZip.Text = mMcus.SalesZip;

                txtBillCompany.Text = mMcus.BillCompany;
                txtBillContactname.Text = mMcus.BillContactName;
                txtBillEmail.Text = mMcus.BilleMail;
                txtBillFax.Text = mMcus.BillFax;
                txtBillStreet.Text = mMcus.BillStreet;
                txtBillTel.Text = mMcus.BillTel;
                cobBillCountry.Text = mMcus.BillCountry;
                cobBillState.Text = mMcus.BillState;
                txtBillZip.Text = mMcus.BillZip;
                txtBillCity.Text = mMcus.BillCity;


                txtShipCompany.Text = mMcus.ShipCompany;
                txtShipContactName.Text = mMcus.ShipContactName;
                txtShipEmail.Text = mMcus.ShipeMail;
                txtShipFax.Text = mMcus.ShipFax;
                txtShipStreet.Text = mMcus.ShipStreet;
                txtShipTel.Text = mMcus.ShipTel;
                txtShipCity.Text = mMcus.ShipCity;
                cobShipCountry.Text = mMcus.ShipCountry;
                cobShipState.Text = mMcus.ShipState;
                txtShipZip.Text = mMcus.ShipZip;

                txtNote.Text = mMcus.Note;
                txtNoteAccountNo.Text = mMcus.AccountNo;
                txtNoteTrems.Text = mMcus.Terms;
                txtNoteVatIDNo.Text = mMcus.VATIDNo;
                txtNoteVendorCode.Text = mMcus.VendorCode;
                cobNoteCarrier.Text = mMcus.Carrier;
                cobNoteMailPromotion.Text = mMcus.MailPromotion;
                cobPayMethod.Text = mMcus.PayMethod;
                

            }
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            if (txtSalesCompany.Text=="")
            {
                txtSalesCompany.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the SalesCompany.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtBillCompany.Text=="")
            {
                txtBillCompany.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the BillCompany.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtShipCompany.Text=="")
            {
                txtShipCompany.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the ShipCompany.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cobSalesCountry.Text == "")
            {
                cobSalesCountry.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the SalesCountry.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (!bMCountry.ExistscountryName(cobSalesCountry.Text))
                {
                    cobSalesCountry.Focus();
                    MessageDxUtil.ShowError("SalesCountry error!");
                    return;
                }
            }
            if (cobShipCountry.Text == "")
            {
                cobShipCountry.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the ShipCountry.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (!bMCountry.ExistscountryName(cobShipCountry.Text))
                {
                    cobShipCountry.Focus();
                    MessageDxUtil.ShowError("ShipCountry error!");
                    return;
                }
            }
            if (cobBillCountry.Text == "")
            {
                cobBillCountry.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the BillCountry.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (!bMCountry.ExistscountryName(cobBillCountry.Text))
                {
                    cobBillCountry.Focus();
                    MessageDxUtil.ShowError("BillCountry error!");
                    return;
                }

            }
            if (cobNoteMailPromotion.Text=="")
            {
                cobNoteMailPromotion.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the MailPromotion.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtShipStreet.Text=="")
            {
                txtShipStreet.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the ShipStreet.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtNoteTrems.Text=="")
            {
                txtNoteTrems.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the Trems.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }




            if (cobSalesCountry.Text == "United States")
            {
                if (cobSalesState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobSalesState.Text))
                    {
                        cobSalesState.Focus();
                        MessageDxUtil.ShowError("SalesState error!");
                        return;
                    }
                }

            }

            if (cobSalesCountry.Text == "United States")
            {
                if (cobBillState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobBillState.Text))
                    {
                        cobBillState.Focus();
                        MessageDxUtil.ShowError("BillState error!");
                        return;
                    }
                }

            }

            if (cobSalesCountry.Text == "United States")
            {
                if (cobShipState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobShipState.Text))
                    {
                        cobShipState.Focus();
                        MessageDxUtil.ShowError("ShipState error!");
                        return;
                    }
                }

            }

            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.AppendFormat(" SalesCompany='{0}'  ", txtSalesCompany.Text);
            DataTable dt = new DataTable();
            dt = bMcus.GetList(sqlWhere.ToString()).Tables[0];
            if (dt.Rows.Count==0)
            {
                mMcus.SalesCompany = txtSalesCompany.Text;
                mMcus.SalesContactName = txtSalesContactName.Text;
                mMcus.SaleseMail = txtSalesEmail.Text;
                mMcus.SalesFax = txtSalesFax.Text;
                mMcus.SalesStreet = txtSalesStreet.Text;
                mMcus.SalesTel = txtSalesTel.Text;
                mMcus.SalesCity = txtSalesCity.Text;
                mMcus.SalesCountry = cobSalesCountry.Text;
                mMcus.SalesState = cobSalesState.Text;
                mMcus.SalesZip = txtSalesZip.Text;

                mMcus.BillCompany = txtBillCompany.Text;
                mMcus.BillContactName = txtBillContactname.Text;
                mMcus.BilleMail = txtBillEmail.Text;
                mMcus.BillFax = txtBillFax.Text;
                mMcus.BillStreet = txtBillStreet.Text;
                mMcus.BillTel = txtBillTel.Text;
                mMcus.BillCountry = cobBillCountry.Text;
                mMcus.BillState = cobBillState.Text;
                mMcus.BillZip = txtBillZip.Text;
                mMcus.BillCity = txtBillCity.Text;


                mMcus.ShipCompany = txtShipCompany.Text;
                mMcus.ShipContactName = txtShipContactName.Text;
                mMcus.ShipeMail = txtShipEmail.Text;
                mMcus.ShipFax = txtShipFax.Text;
                mMcus.ShipStreet = txtShipStreet.Text;
                mMcus.ShipTel = txtShipTel.Text;
                mMcus.ShipCity = txtShipCity.Text;
                mMcus.ShipCountry = cobShipCountry.Text;
                mMcus.ShipState = cobShipState.Text;
                mMcus.ShipZip = txtShipZip.Text;

                mMcus.Note = txtNote.Text;
                mMcus.AccountNo = txtNoteAccountNo.Text;
                mMcus.Terms = txtNoteTrems.Text;
                mMcus.VATIDNo = txtNoteVatIDNo.Text;
                mMcus.VendorCode = txtNoteVendorCode.Text;
                mMcus.Carrier = cobNoteCarrier.Text;
                mMcus.MailPromotion = cobNoteMailPromotion.Text;
                mMcus.PayMethod = cobPayMethod.Text;
                mMcus.Person = Properties.Settings.Default.LastUser;
                mMcus.UpdateTime = DateTime.Now;
                mMcus.Reasons = txtNoteM.Text;
                if (bMcus.Add(mMcus) > 0)
                {
                    butClear_Click(null, null);
                    gridControl1.DataSource = bMcus.GetList(30, "", "id desc").Tables[0];
                } 
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("The company already exists in the system. Please check again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            


           
        }

        private void butClear_Click(object sender, EventArgs e)
        {
         
            txtSalesCompany.Text = "";
            txtSalesContactName.Text = "";
            txtSalesEmail.Text = "";
            txtSalesFax.Text = "";
            txtSalesStreet.Text = "";
            txtSalesTel.Text = "";
            txtSalesCity.Text = "";
            cobSalesCountry.Text = "";
            cobSalesState.Text = "";
            txtSalesZip.Text = "";
            
            txtBillCompany.Text = "";
            txtBillContactname.Text = "";
            txtBillEmail.Text = "";
            txtBillFax.Text = "";
            txtBillStreet.Text = "";
            txtBillTel.Text = "";
            cobBillCountry.Text = "";
            cobBillState.Text = "";
            txtBillZip.Text = "";
            txtBillCity.Text = "";


            txtShipCompany.Text = "";
            txtShipContactName.Text = "";
            txtShipEmail.Text = "";
            txtShipFax.Text = "";
            txtShipStreet.Text = "";
            txtShipTel.Text = "";
            txtShipCity.Text = "";
            cobShipCountry.Text = "";
            cobShipState.Text = "";
            txtShipZip.Text = "";

            txtNote.Text = "";
            txtNoteAccountNo.Text = "";
            txtNoteTrems.Text = "";
            txtNoteVatIDNo.Text = "";
            txtNoteVendorCode.Text = "";
            cobNoteCarrier.Text = "";
            cobNoteMailPromotion.Text = "";
            cobPayMethod.Text = "";
            txtNoteM.Text = "";
        }
        public void Clear_Control(Control.ControlCollection Con)
        {
            

            Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
            foreach (Control C in Con)
            { //遍历可视化组件中的所有控件
                if (C.GetType().Name == "TextBox")  //判断是否为TextBox控件
                    if (((TextBox)C).Visible == true)   //判断当前控件是否为显示状态
                        ((TextBox)C).Clear();   //清空当前控件
                if (C.GetType().Name == "DateEdit")  //判断是否为DateEdit控件
                    if (((DevExpress.XtraEditors.DateEdit)C).Visible == true)   //判断当前控件是否为显示状态
                        ((DevExpress.XtraEditors.DateEdit)C).Text = "";   //清空当前控件
                if (C.GetType().Name == "TextEdit")  //判断是否为TextEdit控件
                    if (((DevExpress.XtraEditors.TextEdit)C).Visible == true)   //判断当前控件是否为显示状态
                        ((DevExpress.XtraEditors.TextEdit)C).Text = "";   //清空当前控件
                if (C.GetType().Name == "CheckEdit")  //判断是否为CheckEdit控件
                    if (((DevExpress.XtraEditors.CheckEdit)C).Visible == true)   //判断当前控件是否为显示状态
                        ((DevExpress.XtraEditors.CheckEdit)C).Checked = false;   //清空当前控件
                if (C.GetType().Name == "ComboBox")  //判断是否为ComboBox控件
                    if (((System.Windows.Forms.ComboBox)C).Visible == true)   //判断当前控件是否为显示状态
                        ((System.Windows.Forms.ComboBox)C).Text = "";   //清空当前控件的Text属性值
                if (C.GetType().Name == "PictureBox")  //判断是否为PictureBox控件
                    if (((PictureBox)C).Visible == true)   //判断当前控件是否为显示状态
                        ((PictureBox)C).Image = null;   //清空当前控件的Image属性
            }
        }

        private void txtNoteTrems_KeyPress(object sender, KeyPressEventArgs e)
        {
            //(char)8是退格键的键值 可允许用户敲定退格进行更改
            if (!(char.IsNumber(e.KeyChar))&&e.KeyChar!=(char)8)
            {
                e.Handled = true;
            }
      
               
        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            if (txtSalesCompany.Text == "")
            {
                txtSalesCompany.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the SalesCompany.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtBillCompany.Text == "")
            {
                txtBillCompany.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the BillCompany.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtShipCompany.Text == "")
            {
                txtShipCompany.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the ShipCompany.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cobSalesCountry.Text == "")
            {
                cobSalesCountry.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the SalesCountry.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (!bMCountry.ExistscountryName(cobSalesCountry.Text))
                {
                    cobSalesCountry.Focus();
                    MessageDxUtil.ShowError("SalesCountry error!");
                    return;
                }
            }
            if (cobShipCountry.Text == "")
            {
                cobShipCountry.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the ShipCountry.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (!bMCountry.ExistscountryName(cobShipCountry.Text))
                {
                    cobShipCountry.Focus();
                    MessageDxUtil.ShowError("ShipCountry error!");
                    return;
                }
            }
            if (cobBillCountry.Text == "")
            {
                cobBillCountry.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the BillCountry.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (!bMCountry.ExistscountryName(cobBillCountry.Text))
                {
                    cobBillCountry.Focus();
                    MessageDxUtil.ShowError("BillCountry error!");
                    return;
                }

            }
            if (cobNoteMailPromotion.Text == "")
            {
                cobNoteMailPromotion.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the MailPromotion.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtShipStreet.Text == "")
            {
                txtShipStreet.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the ShipStreet.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtNoteTrems.Text == "")
            {
                txtNoteTrems.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill the Trems.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtNoteM.Text=="")
            {
                txtNoteM.Focus();
                DevExpress.XtraEditors.XtraMessageBox.Show("Please fill modification reasons .", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cobSalesCountry.Text == "United States")
            {
                if (cobSalesState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobSalesState.Text))
                    {
                        cobSalesState.Focus();
                        MessageDxUtil.ShowError("SalesState error!");
                        return;
                    }
                }

            }

            if (cobSalesCountry.Text == "United States")
            {
                if (cobBillState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobBillState.Text))
                    {
                        cobBillState.Focus();
                        MessageDxUtil.ShowError("BillState error!");
                        return;
                    }
                }

            }

            if (cobSalesCountry.Text == "United States")
            {
                if (cobShipState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobShipState.Text))
                    {
                        cobShipState.Focus();
                        MessageDxUtil.ShowError("ShipState error!");
                        return;
                    }
                }

            }



            if (cobSalesCountry.Text == "United States")
            {
                if (cobSalesState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobSalesState.Text))
                    {
                        cobSalesState.Focus();
                        MessageDxUtil.ShowError("SalesState error!");
                        return;
                    }
                }

            }

            if (cobSalesCountry.Text == "United States")
            {
                if (cobBillState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobBillState.Text))
                    {
                        cobBillState.Focus();
                        MessageDxUtil.ShowError("BillState error!");
                        return;
                    }
                }

            }

            if (cobSalesCountry.Text == "United States")
            {
                if (cobShipState.Text != "")
                {
                    if (!bMsa.ExistsStateName(cobShipState.Text))
                    {
                        cobShipState.Focus();
                        MessageDxUtil.ShowError("ShipState error!");
                        return;
                    }
                }

            }


            mMcus.ID = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]).ToString());
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.AppendFormat(" SalesCompany='{0}'  ", txtSalesCompany.Text);
            sqlWhere.AppendFormat(" and id<>{0} ", mMcus.ID);
            DataTable dt = new DataTable();
            dt = bMcus.GetList(sqlWhere.ToString()).Tables[0];
            if (dt.Rows.Count == 0)
            {
     
               
                mMcus.SalesCompany = txtSalesCompany.Text;
                mMcus.SalesContactName = txtSalesContactName.Text;
                mMcus.SaleseMail = txtSalesEmail.Text;
                mMcus.SalesFax = txtSalesFax.Text;
                mMcus.SalesStreet = txtSalesStreet.Text;
                mMcus.SalesTel = txtSalesTel.Text;
                mMcus.SalesCity = txtSalesCity.Text;
                mMcus.SalesCountry = cobSalesCountry.Text;
                mMcus.SalesState = cobSalesState.Text;
                mMcus.SalesZip = txtSalesZip.Text;

                mMcus.BillCompany = txtBillCompany.Text;
                mMcus.BillContactName = txtBillContactname.Text;
                mMcus.BilleMail = txtBillEmail.Text;
                mMcus.BillFax = txtBillFax.Text;
                mMcus.BillStreet = txtBillStreet.Text;
                mMcus.BillTel = txtBillTel.Text;
                mMcus.BillCountry = cobBillCountry.Text;
                mMcus.BillState = cobBillState.Text;
                mMcus.BillZip = txtBillZip.Text;
                mMcus.BillCity = txtBillCity.Text;


                mMcus.ShipCompany = txtShipCompany.Text;
                mMcus.ShipContactName = txtShipContactName.Text;
                mMcus.ShipeMail = txtShipEmail.Text;
                mMcus.ShipFax = txtShipFax.Text;
                mMcus.ShipStreet = txtShipStreet.Text;
                mMcus.ShipTel = txtShipTel.Text;
                mMcus.ShipCity = txtShipCity.Text;
                mMcus.ShipCountry = cobShipCountry.Text;
                mMcus.ShipState = cobShipState.Text;
                mMcus.ShipZip = txtShipZip.Text;

                mMcus.Note = txtNote.Text;
                mMcus.AccountNo = txtNoteAccountNo.Text;
                mMcus.Terms = txtNoteTrems.Text;
                mMcus.VATIDNo = txtNoteVatIDNo.Text;
                mMcus.VendorCode = txtNoteVendorCode.Text;
                mMcus.Carrier = cobNoteCarrier.Text;
                mMcus.MailPromotion = cobNoteMailPromotion.Text;
                mMcus.PayMethod = cobPayMethod.Text;
                mMcus.Person = Properties.Settings.Default.LastUser;
                mMcus.UpdateTime = DateTime.Now;
                mMcus.Reasons = txtNoteM.Text;
                if (bMcus.Update(mMcus))
                {
                    butClear_Click(null, null);
                    gridControl1.DataSource = bMcus.GetList(30, "", "id desc").Tables[0];
                }
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("The company already exists in the system. Please check again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void butToOrder_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                if (Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.Columns[0]).ToString()) > 0 && txtSalesCompany.Text!="")
                {
                    Properties.Settings.Default.SalesCompany = txtSalesCompany.Text;
                    FrmOrder form = new FrmOrder();
                   
                    form.MdiParent = this.MdiParent; //父窗体相同
                    form.Show();
                    Properties.Settings.Default.SalesCompany = "";
                }
               
            }
        }

        private void FrmCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {


                txtBillCompany.Text = txtSalesCompany.Text;
                txtBillContactname.Text = txtSalesContactName.Text;
                txtBillEmail.Text = txtSalesEmail.Text;
                txtBillFax.Text = txtSalesFax.Text;
                txtBillStreet.Text = txtSalesStreet.Text;
                txtBillTel.Text = txtSalesTel.Text;
                cobBillCountry.Text = cobSalesCountry.Text;
                cobBillState.Text = cobSalesState.Text;
                txtBillZip.Text = txtSalesZip.Text;
                txtBillCity.Text = txtSalesCity.Text;


                txtShipCompany.Text = txtSalesCompany.Text;
                txtShipContactName.Text = txtSalesContactName.Text;
                txtShipEmail.Text = txtSalesEmail.Text;
                txtShipFax.Text = txtSalesFax.Text;
                txtShipStreet.Text = txtSalesStreet.Text;
                txtShipTel.Text = txtSalesTel.Text;
                txtShipCity.Text = txtSalesCity.Text;
                cobShipCountry.Text = cobSalesCountry.Text;
                cobShipState.Text = cobSalesState.Text;
                txtShipZip.Text = txtSalesZip.Text;

            } 
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            splitContainerControl5.SplitterPosition = splitContainerControl2.Width / 2;
            splitContainerControl4.SplitterPosition = splitContainerControl2.Width / 2;
        }
       

     
    }
}
