using DevExpress.Data.Filtering;
using DevExpress.DocumentServices.ServiceModel.DataContracts.Xpf.Designer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Maticsoft.DBUtility;//Please add references
namespace Medical.Yottor.UI
{
    public partial class FrmEditOrder : DevExpress.XtraEditors.XtraForm
    {
        EuSoft.BLL.MCEProductsBasicinfo bMCEProductsBasicinfo = new EuSoft.BLL.MCEProductsBasicinfo();
        EuSoft.BLL.MCEUnitDefinition bMCEUnitDefinition = new EuSoft.BLL.MCEUnitDefinition();
        EuSoft.BLL.MCEScreeningLibraries bMCEScreeningLibraries = new EuSoft.BLL.MCEScreeningLibraries();
        EuSoft.BLL.MCEOrderProInfo bMCEOrderProInfo = new EuSoft.BLL.MCEOrderProInfo();
        EuSoft.Model.MCEOrderProInfo mMCEOrderProInfo = new EuSoft.Model.MCEOrderProInfo();
        public string stockStatus = "";
        public FrmEditOrder(string id, string orderNo)
        {
            InitializeComponent();
            stockStatus = "";
            GetDate();
            labID.Text = id;
            butAdd.Text = "Add";
            if (id != "")
            {
                butAdd.Text = "Update";

                mMCEOrderProInfo = bMCEOrderProInfo.GetModel(Convert.ToInt32(id));
                labID.Text = id;
                labOrderNo.Text = mMCEOrderProInfo.OrderNo;
                txtCatalogNo.EditValue = mMCEOrderProInfo.ProCatalogNo;
                txtCatalogNo.Text = mMCEOrderProInfo.ProCatalogNo;
                txtDescription.Text = mMCEOrderProInfo.ProDescription;
                txtSize.Text = mMCEOrderProInfo.ProSize.ToString();
                cobUnit.Text = mMCEOrderProInfo.ProUnit;
                txtQuantity.Text = mMCEOrderProInfo.ProQuantity.ToString();
                txtAmount.Text = mMCEOrderProInfo.ProAmount.ToString();
                txtDunon.Text = mMCEOrderProInfo.ProDunOn.ToString();
                cobLibraryID.Text = mMCEOrderProInfo.ProLibraryID;
                txtNote.Text = mMCEOrderProInfo.ProNote;
                txtStockStatus.Text = mMCEOrderProInfo.StockStatus;
            }
            labOrderNo.Text = orderNo;
        }







        public void GetDate()
        {

         
            txtDunon.Text = DateTime.Now.ToShortDateString();
         
           // txtCatalogNo.Properties.DrugNames
            DataTable dtbMCEUnitDefinition = new DataTable();
            dtbMCEUnitDefinition = bMCEUnitDefinition.GetAllList().Tables[0];
            BindComboBoxEdit(cobUnit, dtbMCEUnitDefinition, 1);

            DataTable dtbMCEScreeningLibraries = new DataTable();
            dtbMCEScreeningLibraries = bMCEScreeningLibraries.GetAllList().Tables[0];
            BindComboBoxEdit(cobLibraryID, dtbMCEScreeningLibraries, 0);

            //this.txtCatalogNo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            //this.txtCatalogNo.Properties.View.BestFitColumns();
            //this.txtCatalogNo.Properties.ShowFooter = false;
            //this.txtCatalogNo.Properties.View.OptionsView.ShowAutoFilterRow = true; //显示不显示grid上第一个空行,也是用于检索的应用
            //this.txtCatalogNo.Properties.AutoComplete = false;
            //this.txtCatalogNo.Properties.ImmediatePopup = true;
            //this.txtCatalogNo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            //this.txtCatalogNo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard; //配置,用于像文本框那样呀,可自己录入,选择,些处是枚举,可自行设置.

        }

        /// <summary>
        /// 绑定ComBoBoxEdit下拉框数据  
        /// </summary>
        /// <param name="cmb">ComBoBoxEdit控件</param>
        /// <param name="dt">数据源</param>
        /// <param name="key">显示名称</param>
        /// <param name="val">对应EditValue值</param>
        public static void BindComboBoxEdit(ComboBoxEdit cmb, DataTable dt, int val)
        {
            cmb.Properties.Items.Clear();
            cmb.Properties.Items.Add("");
            foreach (DataRow dr in dt.Rows)
            {
                cmb.Properties.Items.Add(dr[val].ToString());
            }
            if (cmb.Properties.Items.Count > 0)
                cmb.SelectedIndex = 0;
        }
        private void getSumPrice()
        {

            if (this.txtSize.Text.Trim() != "" && this.cobUnit.Text.Trim() != "" && this.txtQuantity.Text.Trim() != "" && this.txtCatalogNo.Text.Trim() != "")
            {
                string size = this.txtSize.Text.Trim() + this.cobUnit.Text.Trim().Replace("*", "/");
                string catalog_no = this.txtCatalogNo.Text.Trim();
                string Quantity = this.txtQuantity.Text.Trim();
                string Unit = this.cobUnit.Text.Trim();
                DataTable dtSumPrice = new DataTable();
                dtSumPrice = DbHelperSQL.Query("select " + Quantity + "*cast(price as int) as  price from price where catalog_no='" + catalog_no + "' and package='" + size + "'").Tables[0];
                if (dtSumPrice.Rows.Count > 0)
                {
                    txtAmount.Text = Convert.ToDouble(dtSumPrice.Rows[0]["price"]).ToString("N");
                }
            }


        }
        private void FilterLookup(object sender)
        {


            GridLookUpEdit edit = sender as GridLookUpEdit;
            GridView gridView = edit.Properties.View as GridView;

            System.Reflection.FieldInfo fi = gridView.GetType().GetField("extraFilter", BindingFlags.NonPublic | BindingFlags.Instance);

            BinaryOperator op1 = new BinaryOperator("CatalogNo", "%" + edit.AutoSearchText + "%", BinaryOperatorType.Like);
            BinaryOperator op2 = new BinaryOperator("DrugNames", "%" + edit.AutoSearchText + "%", BinaryOperatorType.Like);
            string filterCondition = new GroupOperator(GroupOperatorType.Or, new CriteriaOperator[] { op1, op2 }).ToString();
            fi.SetValue(gridView, filterCondition);

            MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(gridView, null);
        }

        private void txtCatalogNo_EditValueChanged(object sender, EventArgs e)
        {
            //BeginInvoke(new MethodInvoker(delegate()
            //{
            //    FilterLookup(sender);

            //}));
        }

        private void txtCatalogNo_Popup(object sender, EventArgs e)
        {
            //FilterLookup(sender);
        }

        private void txtCatalogNo_EditValueChanged_1(object sender, EventArgs e)
        {
          
        }
        private void ClearTxt()
        {
            txtCatalogNo.Text = "";
            txtDescription.Text = "";
            txtDescription.Text = "";
            txtSize.Text = "";
            txtQuantity.Text = "";
            txtAmount.Text = "";
            txtDunon.Text = "";
            txtNote.Text = "";
            txtStockStatus.Text = "";
        }

        private void butAdd_Click(object sender, EventArgs e)
        {


            if (this.txtCatalogNo.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the CatalogNo.");
                this.txtCatalogNo.Focus();
                return;
            }

            if (this.txtDescription.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the Description.");
                this.txtDescription.Focus();
                return;
            }

            if (this.txtSize.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the Size.");
                this.txtSize.Focus();
                return;
            }
            if (this.cobUnit.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the Unit.");
                this.cobUnit.Focus();
                return;
            }
            if (this.txtQuantity.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the Quantity.");
                this.txtQuantity.Focus();
                return;
            }
            if (this.txtAmount.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("Please fill the Amount.");
                this.txtAmount.Focus();
                return;
            }
            stockStatus = txtStockStatus.Text;
            mMCEOrderProInfo.OrderNo = labOrderNo.Text;
            mMCEOrderProInfo.ProCatalogNo = txtCatalogNo.Text;
            mMCEOrderProInfo.ProDescription = txtDescription.Text;
            mMCEOrderProInfo.ProSize = Convert.ToDecimal(txtSize.Text);
            mMCEOrderProInfo.ProUnit = cobUnit.Text;
            mMCEOrderProInfo.ProQuantity = Convert.ToInt32(txtQuantity.Text);
            mMCEOrderProInfo.StockStatus = txtStockStatus.Text;
            mMCEOrderProInfo.ProAmount = Convert.ToDecimal(txtAmount.Text);
            if (txtDunon.Text != "")
            {
                mMCEOrderProInfo.ProDunOn = Convert.ToDateTime(txtDunon.Text);
            }
            mMCEOrderProInfo.ProLibraryID = cobLibraryID.Text;
            mMCEOrderProInfo.ProNote = txtNote.Text;
            mMCEOrderProInfo.ProCurrency = cobAmount.Text;
            mMCEOrderProInfo.ProductStatus = "OK";
            if (butAdd.Text=="Update")
            {
                 mMCEOrderProInfo.ID = Convert.ToInt32(labID.Text);

                 StringBuilder sqlStr = new StringBuilder();
                 sqlStr.Append("INSERT INTO [dbo].[MCEOrderProInfoChangeRe]([OrderNo],[ProCatalogNo],[ProDescription],[ProSize],[ProUnit],[ProQuantity],[ProAmount],[ProCurrency],");
                 sqlStr.Append(" [ProDunOn],[ProNote],[ProLibraryID],[ProductStatus],[ProductProcess],[UpdateTime],[Person],[ChangeNote])");
                 sqlStr.Append("select [OrderNo],[ProCatalogNo],[ProDescription],[ProSize],[ProUnit],[ProQuantity],[ProAmount],[ProCurrency],[ProDunOn],[ProNote],[ProLibraryID]");
                 sqlStr.AppendFormat(",[ProductStatus],[ProductProcess],[UpdateTime],Person,'{1}' FROM [dbo].[MCEOrderProInfo]", Properties.Settings.Default.LastUser, "ID " + labID.Text + " before", Properties.Settings.Default.LastUser);
                 sqlStr.AppendFormat("  where id={0}", mMCEOrderProInfo.ID);
                 if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(sqlStr.ToString()) > 0)
                 {

                 }




                 if (bMCEOrderProInfo.Update(mMCEOrderProInfo))
                 {
                     sqlStr.Append("INSERT INTO [dbo].[MCEOrderProInfoChangeRe]([OrderNo],[ProCatalogNo],[ProDescription],[ProSize],[ProUnit],[ProQuantity],[ProAmount],[ProCurrency],");
                     sqlStr.Append(" [ProDunOn],[ProNote],[ProLibraryID],[ProductStatus],[ProductProcess],[UpdateTime],[Person],[ChangeNote],[ChangePerson],[ChangeTime])");
                     sqlStr.Append("select [OrderNo],[ProCatalogNo],[ProDescription],[ProSize],[ProUnit],[ProQuantity],[ProAmount],[ProCurrency],[ProDunOn],[ProNote],[ProLibraryID]");
                     sqlStr.AppendFormat(",[ProductStatus],[ProductProcess],[UpdateTime],'{0}','{1}','{2}',getdate() FROM [dbo].[MCEOrderProInfo]", Properties.Settings.Default.LastUser, "ID " + labID.Text + " After", Properties.Settings.Default.LastUser);
                     sqlStr.AppendFormat("  where id={0}", mMCEOrderProInfo.ID);
                     if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(sqlStr.ToString()) > 0)
                     {

                     }

                      ClearTxt();
                    MessageDxUtil.ShowTips("success");
                    this.DialogResult = DialogResult.OK;
                 }
            }
            else
            {
                if (bMCEOrderProInfo.Add(mMCEOrderProInfo) > 0)
                {
                    ClearTxt();
                    MessageDxUtil.ShowTips("success");
                    this.DialogResult = DialogResult.OK;
                }
            }
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCatalogNo_Click(object sender, EventArgs e)
        {
            if (txtCatalogNo.Text != "")
            {
                DataTable dt = new DataTable();
                string sqlStr = "select  catalogNo as  StockCatalogNo,b.CSNo,b.DrugNames,StockSize,StockUnit,StockValCode,StockBatchNo,SysNote,StockLocation,StockStatus from viewMCEStock  a right join dbo.MCEProductsBasicinfo b on a.stockcatalogno=b.catalogNo where  b.csno='" + txtCatalogNo.Text + "'";

                dt = DbHelperSQL.QueryMCE(sqlStr).Tables[0];
                gridControl5.DataSource = dt;
                gridView9.BestFitColumns();
                groupControl10.Visible = true;
            }
        }

        private void gridControl5_Click(object sender, EventArgs e)
        {
            if (gridView9.FocusedRowHandle > -1)
            {
                txtDescription.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "DrugNames").ToString();
                txtStockStatus.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "StockStatus").ToString();
                txtNote.Text = gridView9.GetRowCellValue(this.gridView9.FocusedRowHandle, "SysNote").ToString();
            }
            groupControl10.Visible = false;
        }

    

        private void txtCatalogNo_Leave(object sender, EventArgs e)
        {
            if (txtCatalogNo.Text != "")
            {
                DataTable dt = new DataTable();
                string sqlStr = "select  catalogNo as  StockCatalogNo,b.CSNo,b.DrugNames,StockSize,StockUnit,StockValCode,StockBatchNo,SysNote,StockLocation,StockStatus from viewMCEStock  a right join dbo.MCEProductsBasicinfo b on a.stockcatalogno=b.catalogNo where  b.csno='" + txtCatalogNo.Text + "'";
                dt = DbHelperSQL.QueryMCE(sqlStr).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtDescription.Text = dt.Rows[0]["DrugNames"].ToString();
                   
                }


            }
        }

        private void groupControl10_Click(object sender, EventArgs e)
        {
            groupControl10.Visible = !groupControl10.Visible;
        }

        private void txtSize_EditValueChanged(object sender, EventArgs e)
        {
            getSumPrice();
        }

        private void txtQuantity_EditValueChanged(object sender, EventArgs e)
        {
            getSumPrice();
        }

        private void cobUnit_EditValueChanged(object sender, EventArgs e)
        {
            getSumPrice();
        }

        private void gridView9_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                string status = gridView9.GetRowCellValue(e.RowHandle, "StockStatus").ToString();
                if (status == "CA")
                {
                    e.Appearance.BackColor = Color.Aquamarine;
                }

            }
        }
    }
}
