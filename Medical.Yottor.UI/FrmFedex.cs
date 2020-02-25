
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.XPath;
using HtmlAgilityPack;
using EuSoft.Common;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
namespace Medical.Yottor.UI
{

    public partial class FrmFedex : DevExpress.XtraEditors.XtraForm
    {
        EuSoft.BLL.MCEOrderInfo bMCEOrderInfo = new EuSoft.BLL.MCEOrderInfo();
        EuSoft.Model.MCEOrderInfo mMCEOrderInfo = new EuSoft.Model.MCEOrderInfo();
        EuSoft.Common.FedexStr cFedexStr = new EuSoft.Common.FedexStr();
        EuSoft.BLL.ProFedexInfor bProFedexInfor = new EuSoft.BLL.ProFedexInfor();
        EuSoft.Model.ProFedexInfor mProFedexInfor = new EuSoft.Model.ProFedexInfor();
        public string traNo;
        public string Yf;
        DataTable dtMCEOrderProInfo = new DataTable();
        public FrmFedex(string OrderNo, string rowCount, string ShipCompany, string ShipContactName, string ShipStreet, string ShipCity, string ShipState, string ShipZip, string ShipCountry, string ShipTel, string ShipEmail, string InvoiceNo, string PONumber, string CuNo, string reference, decimal Customs)
        {
            InitializeComponent();
            getDate(OrderNo, rowCount, ShipCompany, ShipContactName, ShipStreet, ShipCity, ShipState, ShipZip, ShipCountry, ShipTel, ShipEmail, InvoiceNo, PONumber, CuNo, reference, Customs);
        }

        private void getDate(string OrderNo, string rowCount, string ShipCompany, string ShipContactName, string ShipStreet, string ShipCity, string ShipState, string ShipZip, string ShipCountry, string ShipTel, string ShipEmail, string InvoiceNo, string PONumber, string CuNo, string reference, decimal Customs)
        {
            DataTable dt = EuSoft.Common.XMLProcess.GetDataSetByXml(Application.StartupPath + @"\gsxx.xml").Tables[0];
            txtSenders.Properties.DataSource = dt;
            txtSenders.Properties.ValueMember = "ID";
            txtSenders.Properties.DisplayMember = "senders";

            txtSenders.EditValue = "2";
            txtShipCompany.Text = ShipCompany;
            txtShipContactName.Text = ShipContactName;
            txtShipCountry.Text = ShipCountry;

            txtShipAddress1.Text = ShipStreet.Length > 35 ? ShipStreet.Substring(0, 35) : ShipStreet;
            txtShipAddress2.Text = ShipStreet.Length > 35 ? ShipStreet.Substring(35, ShipStreet.Length - 35) : "";
            txtShipAddress1.ToolTip = ShipStreet;
            txtShipAddress2.ToolTip = txtShipAddress2.Text;
            txtShipCity.Text = ShipCity;
            txtShipCity.ToolTip = ShipCity;
            txtShipZip.Text = ShipZip;
            txtShipZip.ToolTip = ShipZip;
            txtShipPhone.Text = ShipTel.Split('*')[0].ToString();
            txtShipPhoneFj.Text = ShipTel.IndexOf('*') > 0 ? ShipTel.Split('*')[1].ToString() : "";
            txtShipEmial.Text = ShipEmail;

            txtReference.Text = reference;
            txtPoNo.Text = PONumber;
            txtInvoiceNo.Text = InvoiceNo;
            txtShipDate.Text = DateTime.Now.ToShortDateString();

            txtServiceType.Properties.Items.Clear();
            string ServiceType = "EUROPE_FIRST_INTERNATIONAL_PRIORITY,FEDEX_1_DAY_FREIGHT,FEDEX_2_DAY,FEDEX_2_DAY_AM,FEDEX_2_DAY_FREIGHT,FEDEX_3_DAY_FREIGHT,FEDEX_DISTANCE_DEFERRED,FEDEX_EXPRESS_SAVER,FEDEX_FIRST_FREIGHT,FEDEX_FREIGHT_ECONOMY,FEDEX_FREIGHT_PRIORITY,FEDEX_GROUND,FEDEX_NEXT_DAY_AFTERNOON,FEDEX_NEXT_DAY_EARLY_MORNING,FEDEX_NEXT_DAY_END_OF_DAY,FEDEX_NEXT_DAY_FREIGHT,FEDEX_NEXT_DAY_MID_MORNING,FIRST_OVERNIGHT,GROUND_HOME_DELIVERY,INTERNATIONAL_ECONOMY,INTERNATIONAL_ECONOMY_FREIGHT,INTERNATIONAL_FIRST,INTERNATIONAL_PRIORITY,INTERNATIONAL_PRIORITY_FREIGHT,PRIORITY_OVERNIGHT,SAME_DAY,SAME_DAY_CITY,SMART_POST,STANDARD_OVERNIGHT";
            string[] sArray = ServiceType.Split(',');
            foreach (string i in sArray)
                txtServiceType.Properties.Items.Add(i.ToString());
            string PackageType = "FEDEX_BOX,FEDEX_ENVELOPE,FEDEX_PAK,FEDEX_TUBE,YOUR_PACKAGING";
            string[] sArray1 = PackageType.Split(',');
            foreach (string i in sArray1)
                txtPackageType.Properties.Items.Add(i.ToString());
            string Purpose = "SOLD,GIFT,NOT_SOLD,PERSONAL_EFFECTS,REPAIR_AND_RETURN,SAMPLE";
            string[] sArray2 = Purpose.Split(',');
            foreach (string i in sArray2)
                txtPurpose.Properties.Items.Add(i.ToString());

            string Duties = "SENDER,RECIPIENT,RECIPIENTTHIRD_PARTY,THIRD_PARTY";
            string[] sArray3 = Duties.Split(',');
            foreach (string i in sArray3)
            {
                txtDuties.Properties.Items.Add(i.ToString());
                txtTraTo.Properties.Items.Add(i.ToString());
            }

            string Tema = "FOB,CFR,CIF,DDP,DDU,DAP,DAT,EXW";
            string[] sArray4 = Tema.Split(',');
            foreach (string i in sArray4)
            {
                txtTema.Properties.Items.Add(i.ToString());
            }


            txtMCountry.Properties.Items.Add("CN");
            txtMCountry.Properties.Items.Add("US");


            if (CuNo == string.Empty)
            {
                txtTraTo.SelectedIndex = 0;
                txtAccountNo.Text = "144535430";

            }
            else
            {
                txtTraTo.SelectedIndex = 1;
                txtAccountNo.Text = CuNo.Replace(" ", "").Trim();

            }

            txtCustoms.Text = Customs.ToString();
            if (txtShipCountry.Text == "U.S.A." || txtShipCountry.Text == "United States")
            {
                txtWeight.Text = "1";
                txtServiceType.Text = "PRIORITY_OVERNIGHT";
                txtPackageType.Text = "FEDEX_ENVELOPE";
                txtDuties.Enabled = false;
                txtAccountNo1.Enabled = false;
                btuShip.Text = "Ship";

                layoutControlItem33.ContentVisible = false;
                layoutControlItem34.ContentVisible = false;
                layoutControlItem35.ContentVisible = false;
                layoutControlItem36.ContentVisible = false;
                layoutControlItem37.ContentVisible = false;
                layoutControlItem38.ContentVisible = false;
                layoutControlItem39.ContentVisible = false;

            }
            else
            {
                txtServiceType.Text = "INTERNATIONAL_PRIORITY";
                txtPackageType.Text = "FEDEX_ENVELOPE";
                txtDuties.Enabled = true;
                txtAccountNo1.Enabled = true;
                btuShip.Text = "Continue";
                txtWeight.Text = "0.5";
                txtFreight.Text = "85";
                txtTema.Text = "EXW";
                txtDuties.SelectedIndex = 1;
                string sql = " select  ROW_NUMBER() OVER (ORDER BY a.id) AS ID,a.ProCatalogNo, b.PDescription,b.Hcode ,b.Country as manuf,ProSize ProQuantity,  convert(decimal(10,2)," + txtWeight.Text + "/" + rowCount + ") as ProSize ,a.prounit [Unit], ProAmount, isnull(b.Customs,0) as  Customs from MCEOrderProInfo a left join  ProFedexInfor  b on a.ProCatalogNo=b.ProCataloNo where a.OrderNo = '" + OrderNo + "' and  a.stockstatus=''";


                dtMCEOrderProInfo = Maticsoft.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                gridControl2.DataSource = dtMCEOrderProInfo;

                if (CuNo != string.Empty)
                {
                    txtTema.Text = "FOB";
                    txtFreight.Text = "0";
                }
                txtShipWeight.Text = txtWeight.Text;
                txtTotalValues.Text = Convert.ToString(Convert.ToDecimal(txtFreight.Text) + Convert.ToDecimal(txtCustoms.Text));
            }
            layoutControlItem63.ContentVisible = false;
            layoutControlItem68.ContentVisible = false;
            layoutControlItem54.ContentVisible = false;

        }

        private void txtSenders_EditValueChanged(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                //string displayName = this.txtSenders.Properties.DisplayMember;
                //string valueName = "ID";
                string display = txtSenders.EditValue.ToString();

                DataTable dtTemp = this.txtSenders.Properties.DataSource as DataTable;

                if (dtTemp != null)
                {
                    DataRow[] selectedRows = dtTemp.Select(string.Format("{0}='{1}'", "ID", display.Replace("'", "‘")));
                    if (selectedRows != null && selectedRows.Length > 0)
                    {
                        txtCountry.Text = selectedRows[0]["Country"].ToString();
                        txtCompany.Text = selectedRows[0]["Company"].ToString();
                        txtContactName.Text = selectedRows[0]["ContactName"].ToString();
                        txtAddress1.Text = selectedRows[0]["Address1"].ToString();
                        txtAddress2.Text = selectedRows[0]["Address2"].ToString();
                        txtCity.Text = selectedRows[0]["City"].ToString();
                        txtZip.Text = selectedRows[0]["Zip"].ToString();
                        txtMyEmial.Text = selectedRows[0]["Emial"].ToString();
                        txtPhone.Text = selectedRows[0]["FirstPhone"].ToString();

                    }


                }


            }
        }

        private void txtPackageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            layoutControlItem33.ContentVisible = false;
            layoutControlItem34.ContentVisible = false;
            layoutControlItem35.ContentVisible = false;

            if (txtPackageType.Text == "FEDEX_BOX" || txtPackageType.Text == "FEDEX_PAK")
            {
                txtWeight.Text = "1";
            }
            if (txtPackageType.Text == "FEDEX_ENVELOPE")
            {
                txtWeight.Text = "0.5";
            }
            if (txtPackageType.Text == "YOUR_PACKAGING")
            {
                txtWeight.Text = "0.5";
                layoutControlItem33.ContentVisible = true;
                layoutControlItem34.ContentVisible = true;
                layoutControlItem35.ContentVisible = true;


            }






        }

        private void txtServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtServiceType.Text == "INTERNATIONAL_PRIORITY")
            {
                btuShip.Text = "Continue";


                layoutControlItem36.ContentVisible = true;
                layoutControlItem37.ContentVisible = true;
                layoutControlItem38.ContentVisible = true;
                layoutControlItem39.ContentVisible = true;

            }
        }

        private void btuGetRete_Click(object sender, EventArgs e)
        {
            if (txtAccountNo.Text != "144535430")
            {
                txtRate.Text = "0";
                return;
            }

            string CountryCode = cFedexStr.GetCountryCode(txtShipCountry.Text);
            if (txtShipCountry.Text == "U.S.A." || txtShipCountry.Text.ToUpper() == "CANADA" || txtShipCountry.Text == "United States")
            {

                string err = cFedexStr.GetStateOrProvinceCode(cFedexStr.GetCountryCode(txtShipCountry.Text), txtShipZip.Text);
                if (err.IndexOf("失败") > 0)
                {
                    MessageDxUtil.ShowWarning("City Or ZIP  error!");
                    txtShipZip.Focus();
                }
            }
            string SoapStr = "";


            SoapStr = SoapStr + "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:v20='http://fedex.com/ws/rate/v20'>";
            SoapStr = SoapStr + "\r\n" + "<soapenv:Header/>";
            SoapStr = SoapStr + "\r\n" + "<soapenv:Body>";
            SoapStr = SoapStr + "\r\n" + "<v20:RateRequest>";
            SoapStr = SoapStr + "\r\n" + "<v20:WebAuthenticationDetail>";
            SoapStr = SoapStr + "\r\n" + "<v20:UserCredential>";
            SoapStr = SoapStr + "\r\n" + "<v20:Key>ZHydhhp6w2EKJCGQ</v20:Key>";
            SoapStr = SoapStr + "\r\n" + "<v20:Password>fCpUUGcr7dqkAeKWZrOMincU1</v20:Password>";
            SoapStr = SoapStr + "\r\n" + "</v20:UserCredential>";
            SoapStr = SoapStr + "\r\n" + "</v20:WebAuthenticationDetail>";
            SoapStr = SoapStr + "\r\n" + "<v20:ClientDetail>";
            SoapStr = SoapStr + "\r\n" + "<v20:AccountNumber>144535430</v20:AccountNumber>";
            SoapStr = SoapStr + "\r\n" + "<v20:MeterNumber>110632288</v20:MeterNumber>";
            SoapStr = SoapStr + "\r\n" + "</v20:ClientDetail>";
            SoapStr = SoapStr + "\r\n" + "<v20:TransactionDetail>";
            SoapStr = SoapStr + "\r\n" + "<v20:CustomerTransactionId>***Rate Request using VC#***</v20:CustomerTransactionId>";
            SoapStr = SoapStr + "\r\n" + "</v20:TransactionDetail>";
            SoapStr = SoapStr + "\r\n" + "<v20:Version>";
            SoapStr = SoapStr + "\r\n" + "<v20:ServiceId>crs</v20:ServiceId>";
            SoapStr = SoapStr + "\r\n" + "<v20:Major>20</v20:Major>";
            SoapStr = SoapStr + "\r\n" + "<v20:Intermediate>0</v20:Intermediate>";
            SoapStr = SoapStr + "\r\n" + "<v20:Minor>0</v20:Minor>";
            SoapStr = SoapStr + "\r\n" + "</v20:Version>";
            SoapStr = SoapStr + "\r\n" + "<v20:RequestedShipment>";
            SoapStr = SoapStr + "\r\n" + "<v20:ShipTimestamp>" + DateTime.Now.ToString("s") + "</v20:ShipTimestamp>";
            SoapStr = SoapStr + "\r\n" + "<v20:DropoffType>REGULAR_PICKUP</v20:DropoffType>";
            SoapStr = SoapStr + "\r\n" + "<v20:ServiceType>" + txtServiceType.Text + "</v20:ServiceType>";
            SoapStr = SoapStr + "\r\n" + "<v20:PackagingType>" + txtPackageType.Text + "</v20:PackagingType>";
            SoapStr = SoapStr + "\r\n" + "<v20:Shipper>";
            SoapStr = SoapStr + "\r\n" + "<v20:Contact>";


            SoapStr = SoapStr + "\r\n" + "</v20:Contact>";
            SoapStr = SoapStr + "\r\n" + "<v20:Address>";
            SoapStr = SoapStr + "\r\n" + "<v20:StreetLines>" + cFedexStr.GetXmlStr(txtAddress1.Text) + "</v20:StreetLines>";
            SoapStr = SoapStr + "\r\n" + "<v20:StreetLines>" + cFedexStr.GetXmlStr(txtAddress2.Text) + "</v20:StreetLines>";
            SoapStr = SoapStr + "\r\n" + "<v20:City>" + txtCity.Text + "</v20:City>";

            SoapStr = SoapStr + "\r\n" + "<v20:StateOrProvinceCode>NJ</v20:StateOrProvinceCode>";

            SoapStr = SoapStr + "\r\n" + "<v20:PostalCode>" + txtZip.Text + "</v20:PostalCode>";
            SoapStr = SoapStr + "\r\n" + "<v20:CountryCode>US</v20:CountryCode>";
            SoapStr = SoapStr + "\r\n" + "</v20:Address>";
            SoapStr = SoapStr + "\r\n" + "</v20:Shipper>";
            SoapStr = SoapStr + "\r\n" + "<v20:Recipient>";
            SoapStr = SoapStr + "\r\n" + "<v20:Contact>";

            SoapStr = SoapStr + "\r\n" + "</v20:Contact>";
            SoapStr = SoapStr + "\r\n" + "<v20:Address>";
            SoapStr = SoapStr + "\r\n" + "<v20:StreetLines>" + cFedexStr.GetXmlStr(txtShipAddress1.Text) + "</v20:StreetLines>";
            SoapStr = SoapStr + "\r\n" + "<v20:StreetLines>" + cFedexStr.GetXmlStr(txtShipAddress2.Text) + "</v20:StreetLines>";
            SoapStr = SoapStr + "\r\n" + "<v20:City>" + txtShipCity.Text + "</v20:City>";

            if (txtShipCountry.Text == "U.S.A." || txtShipCountry.Text.ToUpper() == "CANADA" || txtShipCountry.Text == "United States")
            {

                // SoapStr = SoapStr + "\r\n" + "<v20:StateOrProvinceCode>" + cFedexStr.sendPost("http://34.207.148.81:4545/ShimentService.asmx/GetStateOrProvinceCode", "CountryCode=" + CountryCode + "&PostalCode=" + txtShipZip.Text + "") + "</v20:StateOrProvinceCode>";
                // SoapStr = SoapStr + "<v20:StateOrProvinceCode>" + cFedexStr.sendPost("http://34.207.148.81:4545/ShimentService.asmx/GetStateOrProvinceCode", "CountryCode=" + cFedexStr.sendPost("http://34.207.148.81:4545/ShimentService.asmx/GetCountryCode", "CountryName=" + txtShipCountry.Text + "") + " & PostalCode= " + txtShipZip.Text + "") + "</v20:StateOrProvinceCode>";
                SoapStr = SoapStr + "<v20:StateOrProvinceCode>" + cFedexStr.GetStateOrProvinceCode(cFedexStr.GetCountryCode(txtShipCountry.Text), txtShipZip.Text) + "</v20:StateOrProvinceCode>";

            }
            else
            { SoapStr = SoapStr + "\r\n" + "<v20:StateOrProvinceCode></v20:StateOrProvinceCode>"; }

            SoapStr = SoapStr + "\r\n" + "<v20:PostalCode>" + txtShipZip.Text + "</v20:PostalCode>";
            SoapStr = SoapStr + "\r\n" + "<v20:CountryCode>" + CountryCode + "</v20:CountryCode>";
            // SoapStr = SoapStr + "<v20:CountryCode> " + cFedexStr.sendPost("Http://34.207.148.81:4545/ShimentService.asmx/GetCountryCode", "CountryName=" + txtShipCountry.Text + "") + "</v20:CountryCode>";
            SoapStr = SoapStr + "\r\n" + "</v20:Address>";
            SoapStr = SoapStr + "\r\n" + "</v20:Recipient>";
            SoapStr = SoapStr + "\r\n" + "<v20:RateRequestTypes>PREFERRED</v20:RateRequestTypes>";
            SoapStr = SoapStr + "\r\n" + "<v20:PackageCount>" + txtPackagesNo.Text + "</v20:PackageCount>";
            SoapStr = SoapStr + "\r\n" + "<v20:RequestedPackageLineItems>";
            SoapStr = SoapStr + "\r\n" + "<v20:SequenceNumber>1</v20:SequenceNumber>";
            SoapStr = SoapStr + "\r\n" + "<v20:GroupNumber>1</v20:GroupNumber>";
            SoapStr = SoapStr + "\r\n" + "<v20:GroupPackageCount>1</v20:GroupPackageCount>";
            SoapStr = SoapStr + "\r\n" + "<v20:Weight>";
            SoapStr = SoapStr + "\r\n" + "<v20:Units>LB</v20:Units>";
            SoapStr = SoapStr + "\r\n" + "<v20:Value>" + txtWeight.Text + "</v20:Value>";
            SoapStr = SoapStr + "\r\n" + "</v20:Weight>";
            SoapStr = SoapStr + "\r\n" + "<v20:Dimensions>";
            if (txtPackageType.Text == "YOUR_PACKAGING")
            {
                SoapStr = SoapStr + "\r\n" + "<v20:Length>" + txtL.Text + "</v20:Length>";
                SoapStr = SoapStr + "\r\n" + "<v20:Width>" + txtW.Text + "</v20:Width>";
                SoapStr = SoapStr + "\r\n" + "<v20:Height>" + txtH.Text + "</v20:Height>";
            }
            else
            {
                SoapStr = SoapStr + "\r\n" + "<v20:Length>0</v20:Length>";
                SoapStr = SoapStr + "\r\n" + "<v20:Width>0</v20:Width>";
                SoapStr = SoapStr + "\r\n" + "<v20:Height>0</v20:Height>";
            }
            SoapStr = SoapStr + "\r\n" + "<v20:Units>IN</v20:Units>";
            SoapStr = SoapStr + "\r\n" + "</v20:Dimensions>";
            SoapStr = SoapStr + "\r\n" + "</v20:RequestedPackageLineItems>";
            SoapStr = SoapStr + "\r\n" + "</v20:RequestedShipment>";
            SoapStr = SoapStr + "\r\n" + "</v20:RateRequest>";
            SoapStr = SoapStr + "\r\n" + "</soapenv:Body>";
            SoapStr = SoapStr + "\r\n" + "</soapenv:Envelope>";

            txtRate.Text = cFedexStr.GetRataInfo(SoapStr);
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            layoutControlItem54.ContentVisible = true;
            string SoapStr = "";

            SoapStr = SoapStr + "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:v4='http://fedex.com/ws/addressvalidation/v4'>";
            SoapStr = SoapStr + "<soapenv:Header/>";
            SoapStr = SoapStr + "<soapenv:Body>";
            SoapStr = SoapStr + "<v4:AddressValidationRequest>";
            SoapStr = SoapStr + "<v4:WebAuthenticationDetail>";
            SoapStr = SoapStr + "<v4:UserCredential>";
            SoapStr = SoapStr + "<v4:Key>ZHydhhp6w2EKJCGQ</v4:Key>";
            SoapStr = SoapStr + "<v4:Password>fCpUUGcr7dqkAeKWZrOMincU1</v4:Password>";
            SoapStr = SoapStr + "</v4:UserCredential>";
            SoapStr = SoapStr + "</v4:WebAuthenticationDetail>";
            SoapStr = SoapStr + "<v4:ClientDetail>";
            SoapStr = SoapStr + "<v4:AccountNumber>144535430</v4:AccountNumber>";
            SoapStr = SoapStr + "<v4:MeterNumber>110632288</v4:MeterNumber>";
            SoapStr = SoapStr + "<v4:Localization>";
            SoapStr = SoapStr + "<v4:LanguageCode>EN</v4:LanguageCode>";
            SoapStr = SoapStr + "<v4:LocaleCode>US</v4:LocaleCode>";
            SoapStr = SoapStr + "</v4:Localization>";
            SoapStr = SoapStr + "</v4:ClientDetail>";
            SoapStr = SoapStr + "<v4:TransactionDetail>";
            SoapStr = SoapStr + "<v4:CustomerTransactionId>AddressValidationRequest_v4</v4:CustomerTransactionId>";
            SoapStr = SoapStr + "<v4:Localization>";
            SoapStr = SoapStr + "<v4:LanguageCode>EN</v4:LanguageCode>";
            SoapStr = SoapStr + "<v4:LocaleCode>US</v4:LocaleCode>";
            SoapStr = SoapStr + "</v4:Localization>";
            SoapStr = SoapStr + "</v4:TransactionDetail>";
            SoapStr = SoapStr + "<v4:Version>";
            SoapStr = SoapStr + "<v4:ServiceId>aval</v4:ServiceId>";
            SoapStr = SoapStr + "<v4:Major>4</v4:Major>";
            SoapStr = SoapStr + "<v4:Intermediate>0</v4:Intermediate>";
            SoapStr = SoapStr + "<v4:Minor>0</v4:Minor>";
            SoapStr = SoapStr + "</v4:Version>";
            SoapStr = SoapStr + "<v4:InEffectAsOfTimestamp>" + DateTime.Now.ToString("s") + "</v4:InEffectAsOfTimestamp>";
            SoapStr = SoapStr + "<v4:AddressesToValidate>";
            SoapStr = SoapStr + "<v4:Address>";
            SoapStr = SoapStr + "<v4:StreetLines>" + cFedexStr.GetXmlStr(txtShipAddress1.Text) + "</v4:StreetLines>";
            SoapStr = SoapStr + "<v4:StreetLines>" + cFedexStr.GetXmlStr(txtShipAddress2.Text) + "</v4:StreetLines>";
            SoapStr = SoapStr + "<v4:City>" + txtShipCity.Text + "</v4:City>";
            string CountryCode = cFedexStr.GetCountryCode(txtShipCountry.Text);
            if (txtShipCountry.Text == "U.S.A." || txtShipCountry.Text.ToUpper() == "CANADA" || txtShipCountry.Text == "United States")
            {
                SoapStr = SoapStr + "<v4:StateOrProvinceCode>" + cFedexStr.GetStateOrProvinceCode(CountryCode, txtShipZip.Text) + "</v4:StateOrProvinceCode>";
            }
            else
            { SoapStr = SoapStr + "<v4:StateOrProvinceCode></v4:StateOrProvinceCode>"; }
            SoapStr = SoapStr + "<v4:PostalCode>" + txtShipZip.Text + "</v4:PostalCode>";
            SoapStr = SoapStr + "<v4:CountryCode> " + CountryCode + "</v4:CountryCode>";
            SoapStr = SoapStr + "<v4:Residential>0</v4:Residential>";
            SoapStr = SoapStr + "</v4:Address>";
            SoapStr = SoapStr + "</v4:AddressesToValidate>";
            SoapStr = SoapStr + "</v4:AddressValidationRequest>";
            SoapStr = SoapStr + "</soapenv:Body>";
            SoapStr = SoapStr + "</soapenv:Envelope>";
            checkAddress.Text = cFedexStr.Getaddressvalidation(SoapStr);
            checkAddress.Checked = true;
        }

        private void btuShip_Click(object sender, EventArgs e)
        {
            // System.Diagnostics.Process.Start(@"I:\欧洲\EuSoftWare\bin\PDF\786327400835.pdf"); 
            btuGetRete_Click(null, null);
            if (btuShip.Text == "Ship")
            {
                string SoapStr = "";

                SoapStr = SoapStr + "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:v17='http://fedex.com/ws/ship/v17'>";
                SoapStr = SoapStr + "\r\n" + "<soapenv:Header/>";
                SoapStr = SoapStr + "\r\n" + "<soapenv:Body>";
                SoapStr = SoapStr + "\r\n" + "<v17:ProcessShipmentRequest><!-- Marked by Lily -->";
                SoapStr = SoapStr + "\r\n" + "<v17:WebAuthenticationDetail>";
                SoapStr = SoapStr + "\r\n" + "<v17:UserCredential>";
                SoapStr = SoapStr + "\r\n" + "<v17:Key>ZHydhhp6w2EKJCGQ</v17:Key>";
                SoapStr = SoapStr + "\r\n" + "<v17:Password>fCpUUGcr7dqkAeKWZrOMincU1</v17:Password>";
                SoapStr = SoapStr + "\r\n" + "</v17:UserCredential>";
                SoapStr = SoapStr + "\r\n" + "</v17:WebAuthenticationDetail>";
                SoapStr = SoapStr + "\r\n" + "<v17:ClientDetail>";
                SoapStr = SoapStr + "\r\n" + "<v17:AccountNumber>144535430</v17:AccountNumber>";
                SoapStr = SoapStr + "\r\n" + "<v17:MeterNumber>110632288</v17:MeterNumber>";
                SoapStr = SoapStr + "\r\n" + "<v17:Localization>";
                SoapStr = SoapStr + "\r\n" + "<v17:LanguageCode>EN</v17:LanguageCode>";
                SoapStr = SoapStr + "\r\n" + "<v17:LocaleCode>US</v17:LocaleCode>";
                SoapStr = SoapStr + "\r\n" + "</v17:Localization>";
                SoapStr = SoapStr + "\r\n" + "</v17:ClientDetail>";
                SoapStr = SoapStr + "\r\n" + "<v17:TransactionDetail>";
                SoapStr = SoapStr + "\r\n" + "<v17:CustomerTransactionId>Commodity Shipment Sample - Single Package</v17:CustomerTransactionId>";
                SoapStr = SoapStr + "\r\n" + "<v17:Localization>";
                SoapStr = SoapStr + "\r\n" + "<v17:LanguageCode>EN</v17:LanguageCode>";
                SoapStr = SoapStr + "\r\n" + "<v17:LocaleCode>US</v17:LocaleCode>";
                SoapStr = SoapStr + "\r\n" + "</v17:Localization>";
                SoapStr = SoapStr + "\r\n" + "</v17:TransactionDetail>";
                SoapStr = SoapStr + "\r\n" + "<v17:Version>";
                SoapStr = SoapStr + "\r\n" + "<v17:ServiceId>ship</v17:ServiceId>";
                SoapStr = SoapStr + "\r\n" + "<v17:Major>17</v17:Major>";
                SoapStr = SoapStr + "\r\n" + "<v17:Intermediate>0</v17:Intermediate>";
                SoapStr = SoapStr + "\r\n" + "<v17:Minor>0</v17:Minor>";
                SoapStr = SoapStr + "\r\n" + "</v17:Version>";
                SoapStr = SoapStr + "\r\n" + "<v17:RequestedShipment>";
                SoapStr = SoapStr + "\r\n" + "<v17:ShipTimestamp>" + DateTime.Now.ToString("s") + "</v17:ShipTimestamp>";
                SoapStr = SoapStr + "\r\n" + "<v17:DropoffType>REGULAR_PICKUP</v17:DropoffType>";
                SoapStr = SoapStr + "\r\n" + "<v17:ServiceType>" + txtServiceType.Text + "</v17:ServiceType>";
                SoapStr = SoapStr + "\r\n" + "<v17:PackagingType>" + txtPackageType.Text + "</v17:PackagingType>";
                SoapStr = SoapStr + "\r\n" + "<v17:TotalWeight>";
                SoapStr = SoapStr + "\r\n" + "<v17:Units>LB</v17:Units>";
                SoapStr = SoapStr + "\r\n" + "<v17:Value>" + txtWeight.Text + "</v17:Value>";
                SoapStr = SoapStr + "\r\n" + "</v17:TotalWeight>";
                SoapStr = SoapStr + "\r\n" + "<v17:TotalInsuredValue>";
                SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";
                SoapStr = SoapStr + "\r\n" + "<v17:Amount>0.00</v17:Amount>";
                SoapStr = SoapStr + "\r\n" + "</v17:TotalInsuredValue>";
                SoapStr = SoapStr + "\r\n" + "<v17:Shipper>";
                SoapStr = SoapStr + "\r\n" + "<v17:Contact>";
                SoapStr = SoapStr + "\r\n" + "<v17:PersonName>" + txtSenders.Text + "</v17:PersonName>";
                SoapStr = SoapStr + "\r\n" + "<v17:CompanyName>" + txtCompany.Text + "</v17:CompanyName>";
                SoapStr = SoapStr + "\r\n" + "<v17:PhoneNumber>" + txtPhone.Text + "</v17:PhoneNumber>";
                SoapStr = SoapStr + "\r\n" + "<v17:TollFreePhoneNumber>?</v17:TollFreePhoneNumber>";
                SoapStr = SoapStr + "\r\n" + "</v17:Contact>";
                SoapStr = SoapStr + "\r\n" + "<v17:Address>";
                SoapStr = SoapStr + "\r\n" + "<v17:StreetLines>" + txtAddress1.Text + "</v17:StreetLines>";
                SoapStr = SoapStr + "\r\n" + "<v17:StreetLines>" + txtAddress2.Text + "</v17:StreetLines>";
                SoapStr = SoapStr + "\r\n" + "<v17:City>" + txtCity.Text + "</v17:City>";
                SoapStr = SoapStr + "\r\n" + "<v17:StateOrProvinceCode>NJ</v17:StateOrProvinceCode>";
                SoapStr = SoapStr + "\r\n" + "<v17:PostalCode>" + txtZip.Text + "</v17:PostalCode>";
                SoapStr = SoapStr + "\r\n" + "<v17:CountryCode>US</v17:CountryCode>";
                SoapStr = SoapStr + "\r\n" + "</v17:Address>";
                SoapStr = SoapStr + "\r\n" + "</v17:Shipper>";
                SoapStr = SoapStr + "\r\n" + "<v17:Recipient>";
                SoapStr = SoapStr + "\r\n" + "<v17:Contact>";
                SoapStr = SoapStr + "\r\n" + "<v17:PersonName>" + txtShipContactName.Text + "</v17:PersonName>";
                SoapStr = SoapStr + "\r\n" + "<v17:Title> </v17:Title>";
                SoapStr = SoapStr + "\r\n" + "<v17:CompanyName>" + cFedexStr.GetXmlStr(txtShipCompany.Text) + "</v17:CompanyName>";
                SoapStr = SoapStr + "\r\n" + "<v17:PhoneNumber>" + txtShipPhone.Text + "</v17:PhoneNumber>";
                SoapStr = SoapStr + "\r\n" + "<v17:PhoneExtension></v17:PhoneExtension>";
                SoapStr = SoapStr + "\r\n" + "<v17:TollFreePhoneNumber>?</v17:TollFreePhoneNumber>";
                SoapStr = SoapStr + "\r\n" + "</v17:Contact>";
                SoapStr = SoapStr + "\r\n" + "<v17:Address>";
                SoapStr = SoapStr + "\r\n" + "<v17:StreetLines>" + cFedexStr.GetXmlStr(txtShipAddress1.Text) + "</v17:StreetLines>";
                SoapStr = SoapStr + "\r\n" + "<v17:StreetLines>" + cFedexStr.GetXmlStr(txtShipAddress2.Text) + "</v17:StreetLines>";
                SoapStr = SoapStr + "\r\n" + "<v17:City>" + txtShipCity.Text + "</v17:City>";
                string CountryCode = cFedexStr.GetCountryCode(txtShipCountry.Text);
                if (txtShipCountry.Text == "U.S.A." || txtShipCountry.Text.ToUpper() == "CANADA" || txtShipCountry.Text == "United States")
                {
                    SoapStr = SoapStr + "<v17:StateOrProvinceCode>" + cFedexStr.GetStateOrProvinceCode(CountryCode, txtShipZip.Text) + "</v17:StateOrProvinceCode>";
                }
                else
                { SoapStr = SoapStr + "<v17:StateOrProvinceCode></v17:StateOrProvinceCode>"; }

                // SoapStr = SoapStr + "\r\n" + "<v17:StateOrProvinceCode>MD</v17:StateOrProvinceCode>";
                SoapStr = SoapStr + "\r\n" + "<v17:PostalCode>" + txtShipZip.Text + "</v17:PostalCode>";
                SoapStr = SoapStr + "\r\n" + "<v17:CountryCode>" + CountryCode + "</v17:CountryCode>";
                SoapStr = SoapStr + "\r\n" + "<v17:Residential>false</v17:Residential>";
                SoapStr = SoapStr + "\r\n" + "</v17:Address>";
                SoapStr = SoapStr + "\r\n" + "</v17:Recipient>";
                SoapStr = SoapStr + "\r\n" + "<v17:ShippingChargesPayment>";
                SoapStr = SoapStr + "\r\n" + "<v17:PaymentType>" + txtTraTo.Text + "</v17:PaymentType>";
                SoapStr = SoapStr + "\r\n" + "<v17:Payor>";
                SoapStr = SoapStr + "\r\n" + "<v17:ResponsibleParty>";
                SoapStr = SoapStr + "\r\n" + "<v17:AccountNumber>" + txtAccountNo.Text + "</v17:AccountNumber>";
                SoapStr = SoapStr + "\r\n" + "</v17:ResponsibleParty>";
                SoapStr = SoapStr + "\r\n" + "</v17:Payor>";
                SoapStr = SoapStr + "\r\n" + "</v17:ShippingChargesPayment>";
                if (txtMyEmial.Text != "" || txtShipEmial.Text != "" || txtShipEmial1.Text != "")
                {
                    SoapStr = SoapStr + "\r\n" + "<v17:SpecialServicesRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:SpecialServiceTypes>EMAIL_NOTIFICATION</v17:SpecialServiceTypes>";
                    SoapStr = SoapStr + "\r\n" + "<v17:EMailNotificationDetail>";
                    SoapStr = SoapStr + "\r\n" + "<v17:PersonalMessage>" + txtNote.Text + "</v17:PersonalMessage>";
                    if (txtMyEmial.Text != "")
                    {
                        SoapStr = SoapStr + "\r\n" + "<v17:Recipients>";
                        SoapStr = SoapStr + "\r\n" + "<v17:EMailNotificationRecipientType>SHIPPER</v17:EMailNotificationRecipientType>";
                        SoapStr = SoapStr + "\r\n" + "<v17:EMailAddress>sales@medchemexpress.com</v17:EMailAddress>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_DELIVERY</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_EXCEPTION</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_SHIPMENT</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_TENDER</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:Format>HTML</v17:Format>";
                        SoapStr = SoapStr + "\r\n" + " <v17:Localization>";
                        SoapStr = SoapStr + "\r\n" + "<v17:LanguageCode>EN</v17:LanguageCode>";
                        SoapStr = SoapStr + "\r\n" + "</v17:Localization>";
                        SoapStr = SoapStr + "\r\n" + "</v17:Recipients>";
                    }

                    if (txtShipEmial.Text != "")
                    {
                        SoapStr = SoapStr + "\r\n" + "<v17:Recipients>";
                        SoapStr = SoapStr + "\r\n" + "<v17:EMailNotificationRecipientType>RECIPIENT</v17:EMailNotificationRecipientType>";
                        SoapStr = SoapStr + "\r\n" + "<v17:EMailAddress>" + txtShipEmial.Text + "</v17:EMailAddress>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_DELIVERY</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_EXCEPTION</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_SHIPMENT</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_TENDER</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:Format>HTML</v17:Format>";
                        SoapStr = SoapStr + "\r\n" + "<v17:Localization>";
                        SoapStr = SoapStr + "\r\n" + "<v17:LanguageCode>EN</v17:LanguageCode>";
                        SoapStr = SoapStr + "\r\n" + "</v17:Localization>";
                        SoapStr = SoapStr + "\r\n" + "</v17:Recipients>";

                    }
                    if (txtShipEmial1.Text != "")
                    {
                        SoapStr = SoapStr + "\r\n" + "<v17:Recipients>";
                        SoapStr = SoapStr + "\r\n" + "<v17:EMailNotificationRecipientType>RECIPIENT</v17:EMailNotificationRecipientType>";
                        SoapStr = SoapStr + "\r\n" + "<v17:EMailAddress>" + txtShipEmial1.Text + "</v17:EMailAddress>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_DELIVERY</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_EXCEPTION</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_SHIPMENT</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_TENDER</v17:NotificationEventsRequested>";
                        SoapStr = SoapStr + "\r\n" + "<v17:Format>HTML</v17:Format>";
                        SoapStr = SoapStr + "\r\n" + "<v17:Localization>";
                        SoapStr = SoapStr + "\r\n" + "<v17:LanguageCode>EN</v17:LanguageCode>";
                        SoapStr = SoapStr + "\r\n" + "</v17:Localization>";
                        SoapStr = SoapStr + "\r\n" + "</v17:Recipients>";
                    }


                    SoapStr = SoapStr + "\r\n" + "</v17:EMailNotificationDetail>";
                    SoapStr = SoapStr + "\r\n" + "</v17:SpecialServicesRequested>";
                }

                SoapStr = SoapStr + "\r\n" + "<v17:LabelSpecification>";
                SoapStr = SoapStr + "\r\n" + "<v17:LabelFormatType>COMMON2D</v17:LabelFormatType>";
                SoapStr = SoapStr + "\r\n" + "<v17:ImageType>PDF</v17:ImageType>";
                SoapStr = SoapStr + "\r\n" + "<v17:LabelStockType>PAPER_8.5X11_TOP_HALF_LABEL</v17:LabelStockType>";
                SoapStr = SoapStr + "\r\n" + "<v17:LabelPrintingOrientation>TOP_EDGE_OF_TEXT_FIRST</v17:LabelPrintingOrientation>";
                SoapStr = SoapStr + "\r\n" + "</v17:LabelSpecification>";
                SoapStr = SoapStr + "\r\n" + "<v17:RateRequestTypes>ACCOUNT</v17:RateRequestTypes>";


                SoapStr = SoapStr + "\r\n" + "<v17:PackageCount>1</v17:PackageCount>";
                SoapStr = SoapStr + "\r\n" + "<v17:RequestedPackageLineItems>";
                SoapStr = SoapStr + "\r\n" + "<v17:SequenceNumber>1</v17:SequenceNumber>";
                SoapStr = SoapStr + "\r\n" + "<v17:InsuredValue>";
                SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";
                SoapStr = SoapStr + "\r\n" + "<v17:Amount>0</v17:Amount>";
                SoapStr = SoapStr + "\r\n" + "</v17:InsuredValue>";
                SoapStr = SoapStr + "\r\n" + "<v17:Weight>";
                SoapStr = SoapStr + "\r\n" + "<v17:Units>LB</v17:Units>";
                SoapStr = SoapStr + "\r\n" + "<v17:Value>" + txtWeight.Text + "</v17:Value>";
                SoapStr = SoapStr + "\r\n" + "</v17:Weight>";
                if (txtPackageType.Text == "YOUR_PACKAGING")
                {
                    SoapStr = SoapStr + "\r\n" + " <v17:Dimensions>";
                    SoapStr = SoapStr + "\r\n" + "<v17:Length>" + txtL.Text + "</v17:Length>";
                    SoapStr = SoapStr + "\r\n" + "<v17:Width>" + txtW.Text + "</v17:Width>";
                    SoapStr = SoapStr + "\r\n" + "<v17:Height>" + txtH.Text + "</v17:Height>";
                    SoapStr = SoapStr + "\r\n" + "<v17:Units>IN</v17:Units>";
                    SoapStr = SoapStr + "\r\n" + "</v17:Dimensions>";
                }

                SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferences>";
                SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferenceType>CUSTOMER_REFERENCE</v17:CustomerReferenceType>";
                SoapStr = SoapStr + "\r\n" + "<v17:Value>" + txtReference.Text + "</v17:Value>";
                SoapStr = SoapStr + "\r\n" + "</v17:CustomerReferences>";
                SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferences>";
                SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferenceType>P_O_NUMBER</v17:CustomerReferenceType>";
                SoapStr = SoapStr + "\r\n" + "<v17:Value>" + cFedexStr.GetXmlStr(txtPoNo.Text) + "</v17:Value>";
                SoapStr = SoapStr + "\r\n" + "</v17:CustomerReferences>";
                SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferences>";
                SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferenceType>INVOICE_NUMBER</v17:CustomerReferenceType>";
                SoapStr = SoapStr + "\r\n" + "<v17:Value>" + txtInvoiceNo.Text + "</v17:Value>";
                SoapStr = SoapStr + "\r\n" + "</v17:CustomerReferences>";
                SoapStr = SoapStr + "\r\n" + "</v17:RequestedPackageLineItems>";
                SoapStr = SoapStr + "\r\n" + "</v17:RequestedShipment>";
                SoapStr = SoapStr + "\r\n" + "</v17:ProcessShipmentRequest>";
                SoapStr = SoapStr + "\r\n" + "</soapenv:Body>";
                SoapStr = SoapStr + "\r\n" + "</soapenv:Envelope>";

                string labPath;
                string inovicePath;
                string trkNO;
                string result = cFedexStr.GenerateShipment(SoapStr, out labPath, out inovicePath, out trkNO);
                if (result == "成功")
                {
                    traNo = trkNO;
                    Yf = txtRate.Text;
                    if (labPath != "")
                    {
                        System.Diagnostics.Process.Start(@labPath);
                    }
                    if (inovicePath != "")
                    {
                        System.Diagnostics.Process.Start(@inovicePath);
                    }
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageDxUtil.ShowTips(result);
                }
            }
            else
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
                txtShipWeight.Text = txtWeight.Text;
                txtTotalValues.Text = Convert.ToString(Convert.ToDecimal(txtFreight.Text) + Convert.ToDecimal(txtCustoms.Text));
            }


        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            if (this.gridView2.FocusedRowHandle > -1)
            {
                //  int id = Convert.ToInt32(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, this.gridView2.Columns[0]));
                labCommodity.Text = "Commodity" + this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID");
                txtCommoditydescription.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "PDescription").ToString();
                txtPName.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ProCatalogNo").ToString();
                txtUnit.Text = "LB";
                txtQuantity.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ProQuantity").ToString();
                txtCWeight.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ProSize").ToString();
                txtHz.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Hcode").ToString();
                txtMCountry.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "manuf").ToString();
                txtCus.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ProAmount").ToString();

                //txtCWeightUnit.Text = "As totals";
                //txtCusUnit.Text = "As totals";
            }
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            if (this.gridView2.FocusedRowHandle > -1)
            {
                int id = Convert.ToInt32(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID"));

                DataRow[] selectedRows = dtMCEOrderProInfo.Select("id=" + id + "");
                if (selectedRows != null && selectedRows.Length > 0)
                {
                    foreach (DataRow row in selectedRows) { dtMCEOrderProInfo.Rows.Remove(row); }
                }
                txtCustoms.Text = dtMCEOrderProInfo.Compute("sum(ProAmount)", "TRUE").ToString();
                txtTotalValues.Text = Convert.ToString(Convert.ToDecimal(txtFreight.Text) + Convert.ToDecimal(txtCustoms.Text));
            }
        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            if (this.gridView2.FocusedRowHandle > -1)
            {
                int id = Convert.ToInt32(this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID"));

                DataRow[] selectedRows = dtMCEOrderProInfo.Select("id=" + id + "");
                if (selectedRows != null && selectedRows.Length > 0)
                {

                    selectedRows[0]["PDescription"] = txtCommoditydescription.Text;
                    selectedRows[0]["ProCatalogNo"] = txtPName.Text;
                    selectedRows[0]["Unit"] = txtUnit.Text;
                    selectedRows[0]["ProQuantity"] = txtQuantity.Text;
                    selectedRows[0]["ProSize"] = txtCWeight.Text;
                    selectedRows[0]["Hcode"] = txtHz.Text;
                    selectedRows[0]["manuf"] = txtMCountry.Text;
                    selectedRows[0]["ProAmount"] = txtCus.Text;
                    mProFedexInfor.id = id;
                    mProFedexInfor.Hcode = txtHz.Text;
                    mProFedexInfor.PDescription = txtCommoditydescription.Text;
                    mProFedexInfor.Country = txtMCountry.Text;
                    mProFedexInfor.ProCataloNo = txtPName.Text;
                    mProFedexInfor.Unit = txtUnit.Text;
                    if (bProFedexInfor.GetList("ProCataloNo='" + txtPName.Text + "' ").Tables[0].Rows.Count > 0)
                    {
                        if (bProFedexInfor.Update(mProFedexInfor))
                        {
                            txtCustoms.Text = dtMCEOrderProInfo.Compute("sum(ProAmount)", "TRUE").ToString();
                        }
                    }
                    else
                    {
                        if (bProFedexInfor.Add(mProFedexInfor) > 0)
                        {
                            txtCustoms.Text = dtMCEOrderProInfo.Compute("sum(ProAmount)", "TRUE").ToString();
                        }
                    }

                    txtTotalValues.Text = Convert.ToString(Convert.ToDecimal(txtFreight.Text) + Convert.ToDecimal(txtCustoms.Text));

                    //labCommodity.Text = "Commodity" + this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID");
                    //txtCommoditydescription.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "PDescription").ToString();
                    //txtPName.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ProCatalogNo").ToString();
                    //txtUnit.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Unit").ToString();
                    //txtQuantity.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ProQuantity").ToString();
                    //txtCWeight.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ProSize").ToString();
                    //txtHz.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Hcode").ToString();
                    //txtMCountry.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "manuf").ToString();
                    //txtCus.Text = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ProAmount").ToString();
                    //dtMCEOrderProInfo.AcceptChanges();

                }
            }
        }

        private void txtTema_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFreight.Text = "0";
            if (txtTema.Text == "EXW")
                txtFreight.Text = "85";
            if (txtTema.Text == "EXW" && txtShipCountry.Text.ToUpper() == "CANADA")
                txtFreight.Text = "60";
        }

        private void txtFreight_EditValueChanged(object sender, EventArgs e)
        {
            txtTotalValues.Text = Convert.ToString(Convert.ToDecimal(txtFreight.Text) + Convert.ToDecimal(txtCustoms.Text));
        }

        private void butShipInvoice_Click(object sender, EventArgs e)
        {
            string SoapStr;
            SoapStr = "";




            SoapStr = SoapStr + "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:v17='http://fedex.com/ws/ship/v17'>";
            SoapStr = SoapStr + "\r\n" + "<soapenv:Header/>";
            SoapStr = SoapStr + "\r\n" + "<soapenv:Body>";
            SoapStr = SoapStr + "\r\n" + "<v17:ProcessShipmentRequest><!-- Marked by Lily -->";
            SoapStr = SoapStr + "\r\n" + "<v17:WebAuthenticationDetail>";
            SoapStr = SoapStr + "\r\n" + "<v17:UserCredential>";
            SoapStr = SoapStr + "\r\n" + "<v17:Key>ZHydhhp6w2EKJCGQ</v17:Key>";
            SoapStr = SoapStr + "\r\n" + "<v17:Password>fCpUUGcr7dqkAeKWZrOMincU1</v17:Password>";
            SoapStr = SoapStr + "\r\n" + "</v17:UserCredential>";
            SoapStr = SoapStr + "\r\n" + "</v17:WebAuthenticationDetail>";
            SoapStr = SoapStr + "\r\n" + "<v17:ClientDetail>";
            SoapStr = SoapStr + "\r\n" + "<v17:AccountNumber>144535430</v17:AccountNumber>";
            SoapStr = SoapStr + "\r\n" + "<v17:MeterNumber>110632288</v17:MeterNumber>";
            SoapStr = SoapStr + "\r\n" + "<v17:Localization>";
            SoapStr = SoapStr + "\r\n" + "<v17:LanguageCode>EN</v17:LanguageCode>";
            SoapStr = SoapStr + "\r\n" + "<v17:LocaleCode>US</v17:LocaleCode>";
            SoapStr = SoapStr + "\r\n" + "</v17:Localization>";
            SoapStr = SoapStr + "\r\n" + "</v17:ClientDetail>";
            SoapStr = SoapStr + "\r\n" + "<v17:TransactionDetail>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomerTransactionId>Commodity Shipment Sample - Single Package</v17:CustomerTransactionId>";
            SoapStr = SoapStr + "\r\n" + "<v17:Localization>";
            SoapStr = SoapStr + "\r\n" + "<v17:LanguageCode>EN</v17:LanguageCode>";
            SoapStr = SoapStr + "\r\n" + "<v17:LocaleCode>US</v17:LocaleCode>";
            SoapStr = SoapStr + "\r\n" + "</v17:Localization>";
            SoapStr = SoapStr + "\r\n" + "</v17:TransactionDetail>";
            SoapStr = SoapStr + "\r\n" + "<v17:Version>";
            SoapStr = SoapStr + "\r\n" + "<v17:ServiceId>ship</v17:ServiceId>";
            SoapStr = SoapStr + "\r\n" + "<v17:Major>17</v17:Major>";
            SoapStr = SoapStr + "\r\n" + "<v17:Intermediate>0</v17:Intermediate>";
            SoapStr = SoapStr + "\r\n" + "<v17:Minor>0</v17:Minor>";
            SoapStr = SoapStr + "\r\n" + "</v17:Version>";
            SoapStr = SoapStr + "\r\n" + "<v17:RequestedShipment>";
            SoapStr = SoapStr + "\r\n" + "<v17:ShipTimestamp>" + DateTime.Now.ToString("s") + "</v17:ShipTimestamp>";
            SoapStr = SoapStr + "\r\n" + "<v17:DropoffType>REGULAR_PICKUP</v17:DropoffType>";
            SoapStr = SoapStr + "\r\n" + "<v17:ServiceType>" + txtServiceType.Text + "</v17:ServiceType>";
            SoapStr = SoapStr + "\r\n" + "<v17:PackagingType>" + txtPackageType.Text + "</v17:PackagingType>";
            SoapStr = SoapStr + "\r\n" + "<v17:TotalWeight>";
            SoapStr = SoapStr + "\r\n" + "<v17:Units>LB</v17:Units>";
            SoapStr = SoapStr + "\r\n" + "<v17:Value>" + txtWeight.Text + "</v17:Value>";
            SoapStr = SoapStr + "\r\n" + "</v17:TotalWeight>";
            SoapStr = SoapStr + "\r\n" + "<v17:TotalInsuredValue>";
            SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";
            SoapStr = SoapStr + "\r\n" + "<v17:Amount>0.00</v17:Amount>";
            SoapStr = SoapStr + "\r\n" + "</v17:TotalInsuredValue>";
            SoapStr = SoapStr + "\r\n" + "<v17:Shipper>";
            SoapStr = SoapStr + "\r\n" + "<v17:Contact>";
            SoapStr = SoapStr + "\r\n" + "<v17:PersonName>" + txtSenders.Text + "</v17:PersonName>";
            SoapStr = SoapStr + "\r\n" + "<v17:CompanyName>" + txtCompany.Text + "</v17:CompanyName>";
            SoapStr = SoapStr + "\r\n" + "<v17:PhoneNumber>" + txtPhone.Text + "</v17:PhoneNumber>";
            SoapStr = SoapStr + "\r\n" + "<v17:TollFreePhoneNumber>?</v17:TollFreePhoneNumber>";
            SoapStr = SoapStr + "\r\n" + "</v17:Contact>";
            SoapStr = SoapStr + "\r\n" + "<v17:Address>";
            SoapStr = SoapStr + "\r\n" + "<v17:StreetLines>" + txtAddress1.Text + "</v17:StreetLines>";
            SoapStr = SoapStr + "\r\n" + "<v17:StreetLines>" + txtAddress2.Text + "</v17:StreetLines>";
            SoapStr = SoapStr + "\r\n" + "<v17:City>" + txtCity.Text + "</v17:City>";
            SoapStr = SoapStr + "\r\n" + "<v17:StateOrProvinceCode>NJ</v17:StateOrProvinceCode>";
            SoapStr = SoapStr + "\r\n" + "<v17:PostalCode>" + txtZip.Text + "</v17:PostalCode>";
            SoapStr = SoapStr + "\r\n" + "<v17:CountryCode>US</v17:CountryCode>";
            SoapStr = SoapStr + "\r\n" + "</v17:Address>";
            SoapStr = SoapStr + "\r\n" + "</v17:Shipper>";
            SoapStr = SoapStr + "\r\n" + "<v17:Recipient>";
            SoapStr = SoapStr + "\r\n" + "<v17:Contact>";
            SoapStr = SoapStr + "\r\n" + "<v17:PersonName>" + txtShipContactName.Text + "</v17:PersonName>";
            SoapStr = SoapStr + "\r\n" + "<v17:Title> </v17:Title>";
            SoapStr = SoapStr + "\r\n" + "<v17:CompanyName>" + cFedexStr.GetXmlStr(txtShipCompany.Text) + "</v17:CompanyName>";
            SoapStr = SoapStr + "\r\n" + "<v17:PhoneNumber>" + txtShipPhone.Text + "</v17:PhoneNumber>";
            SoapStr = SoapStr + "\r\n" + "<v17:PhoneExtension></v17:PhoneExtension>";
            SoapStr = SoapStr + "\r\n" + "<v17:TollFreePhoneNumber>?</v17:TollFreePhoneNumber>";
            SoapStr = SoapStr + "\r\n" + "</v17:Contact>";
            SoapStr = SoapStr + "\r\n" + "<v17:Address>";
            SoapStr = SoapStr + "\r\n" + "<v17:StreetLines>" + cFedexStr.GetXmlStr(txtShipAddress1.Text) + "</v17:StreetLines>";
            SoapStr = SoapStr + "\r\n" + "<v17:StreetLines>" + cFedexStr.GetXmlStr(txtShipAddress2.Text) + "</v17:StreetLines>";
            SoapStr = SoapStr + "\r\n" + "<v17:City>" + txtShipCity.Text + "</v17:City>";
            string CountryCode = cFedexStr.GetCountryCode(txtShipCountry.Text);
            if (txtShipCountry.Text == "U.S.A." || txtShipCountry.Text.ToUpper() == "CANADA" || txtShipCountry.Text == "United States")
            {
                SoapStr = SoapStr + "<v17:StateOrProvinceCode>" + cFedexStr.GetStateOrProvinceCode(CountryCode, txtShipZip.Text) + "</v17:StateOrProvinceCode>";
            }
            else
            { SoapStr = SoapStr + "<v17:StateOrProvinceCode></v17:StateOrProvinceCode>"; }

            // SoapStr = SoapStr + "\r\n" + "<v17:StateOrProvinceCode>MD</v17:StateOrProvinceCode>";
            SoapStr = SoapStr + "\r\n" + "<v17:PostalCode>" + txtShipZip.Text + "</v17:PostalCode>";
            SoapStr = SoapStr + "\r\n" + "<v17:CountryCode>" + CountryCode + "</v17:CountryCode>";
            SoapStr = SoapStr + "\r\n" + "<v17:Residential>false</v17:Residential>";
            SoapStr = SoapStr + "\r\n" + "</v17:Address>";
            SoapStr = SoapStr + "\r\n" + "</v17:Recipient>";
            SoapStr = SoapStr + "\r\n" + "<v17:ShippingChargesPayment>";
            SoapStr = SoapStr + "\r\n" + "<v17:PaymentType>" + txtTraTo.Text + "</v17:PaymentType>";
            SoapStr = SoapStr + "\r\n" + "<v17:Payor>";
            SoapStr = SoapStr + "\r\n" + "<v17:ResponsibleParty>";
            SoapStr = SoapStr + "\r\n" + "<v17:AccountNumber>" + txtAccountNo.Text + "</v17:AccountNumber>";
            SoapStr = SoapStr + "\r\n" + "</v17:ResponsibleParty>";
            SoapStr = SoapStr + "\r\n" + "</v17:Payor>";
            SoapStr = SoapStr + "\r\n" + "</v17:ShippingChargesPayment>";
            if (txtMyEmial.Text != "" || txtShipEmial.Text != "" || txtShipEmial1.Text != "")
            {
                SoapStr = SoapStr + "\r\n" + "<v17:SpecialServicesRequested>";
                SoapStr = SoapStr + "\r\n" + "<v17:SpecialServiceTypes>EMAIL_NOTIFICATION</v17:SpecialServiceTypes>";
                SoapStr = SoapStr + "\r\n" + "<v17:EMailNotificationDetail>";
                SoapStr = SoapStr + "\r\n" + "<v17:PersonalMessage>" + txtNote.Text + "</v17:PersonalMessage>";
                if (txtMyEmial.Text != "")
                {
                    SoapStr = SoapStr + "\r\n" + "<v17:Recipients>";
                    SoapStr = SoapStr + "\r\n" + "<v17:EMailNotificationRecipientType>SHIPPER</v17:EMailNotificationRecipientType>";
                    SoapStr = SoapStr + "\r\n" + "<v17:EMailAddress>sales@medchemexpress.com</v17:EMailAddress>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_DELIVERY</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_EXCEPTION</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_SHIPMENT</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_TENDER</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:Format>HTML</v17:Format>";
                    SoapStr = SoapStr + "\r\n" + " <v17:Localization>";
                    SoapStr = SoapStr + "\r\n" + "<v17:LanguageCode>EN</v17:LanguageCode>";
                    SoapStr = SoapStr + "\r\n" + "</v17:Localization>";
                    SoapStr = SoapStr + "\r\n" + "</v17:Recipients>";
                }

                if (txtShipEmial.Text != "")
                {
                    SoapStr = SoapStr + "\r\n" + "<v17:Recipients>";
                    SoapStr = SoapStr + "\r\n" + "<v17:EMailNotificationRecipientType>RECIPIENT</v17:EMailNotificationRecipientType>";
                    SoapStr = SoapStr + "\r\n" + "<v17:EMailAddress>" + txtShipEmial.Text + "</v17:EMailAddress>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_DELIVERY</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_EXCEPTION</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_SHIPMENT</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_TENDER</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:Format>HTML</v17:Format>";
                    SoapStr = SoapStr + "\r\n" + "<v17:Localization>";
                    SoapStr = SoapStr + "\r\n" + "<v17:LanguageCode>EN</v17:LanguageCode>";
                    SoapStr = SoapStr + "\r\n" + "</v17:Localization>";
                    SoapStr = SoapStr + "\r\n" + "</v17:Recipients>";

                }
                if (txtShipEmial1.Text != "")
                {
                    SoapStr = SoapStr + "\r\n" + "<v17:Recipients>";
                    SoapStr = SoapStr + "\r\n" + "<v17:EMailNotificationRecipientType>RECIPIENT</v17:EMailNotificationRecipientType>";
                    SoapStr = SoapStr + "\r\n" + "<v17:EMailAddress>" + txtShipEmial1.Text + "</v17:EMailAddress>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_DELIVERY</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_EXCEPTION</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_SHIPMENT</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:NotificationEventsRequested>ON_TENDER</v17:NotificationEventsRequested>";
                    SoapStr = SoapStr + "\r\n" + "<v17:Format>HTML</v17:Format>";
                    SoapStr = SoapStr + "\r\n" + "<v17:Localization>";
                    SoapStr = SoapStr + "\r\n" + "<v17:LanguageCode>EN</v17:LanguageCode>";
                    SoapStr = SoapStr + "\r\n" + "</v17:Localization>";
                    SoapStr = SoapStr + "\r\n" + "</v17:Recipients>";
                }


                SoapStr = SoapStr + "\r\n" + "</v17:EMailNotificationDetail>";
                SoapStr = SoapStr + "\r\n" + "</v17:SpecialServicesRequested>";
            }


            SoapStr = SoapStr + "\r\n" + "<!--Specify the Duties/Taxes charge-->";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomsClearanceDetail>";
            SoapStr = SoapStr + "\r\n" + "<v17:DutiesPayment>";
            SoapStr = SoapStr + "\r\n" + "<v17:PaymentType>" + txtDuties.Text + "</v17:PaymentType>";
            SoapStr = SoapStr + "\r\n" + "<v17:Payor>";
            SoapStr = SoapStr + "\r\n" + "<v17:ResponsibleParty>";
            SoapStr = SoapStr + "\r\n" + "<v17:AccountNumber>" + txtAccountNo1.Text + "</v17:AccountNumber>";
            SoapStr = SoapStr + "\r\n" + "</v17:ResponsibleParty>";
            SoapStr = SoapStr + "\r\n" + "</v17:Payor>";
            SoapStr = SoapStr + "\r\n" + "</v17:DutiesPayment>";
            SoapStr = SoapStr + "\r\n" + "<!--Specify Document Type, NON_DOCUMENTS, DOCUMENTS_ONLY:-->";
            SoapStr = SoapStr + "\r\n" + "<v17:DocumentContent>NON_DOCUMENTS</v17:DocumentContent>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomsValue>";
            SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";
            SoapStr = SoapStr + "\r\n" + "<v17:Amount>" + txtTotalValues.Text + "</v17:Amount>";
            SoapStr = SoapStr + "\r\n" + "</v17:CustomsValue>";
            SoapStr = SoapStr + "\r\n" + "<!--Specify the information for Commercial Invoice -->";
            SoapStr = SoapStr + "\r\n" + "<!--Comments, Freight Charge, TaxesOrMiscellaneous Charge, Packing Cost, Handling Cost, SpecialInstructions, DeclarationStatement all in optional :-->";

            SoapStr = SoapStr + "\r\n" + "<v17:CommercialInvoice>";
            SoapStr = SoapStr + "\r\n" + "<v17:Comments>CI Comments</v17:Comments>";
            SoapStr = SoapStr + "\r\n" + "<v17:FreightCharge>";
            SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";
            SoapStr = SoapStr + "\r\n" + "<v17:Amount>" + txtFreight.Text + "</v17:Amount>";
            SoapStr = SoapStr + "\r\n" + "</v17:FreightCharge>";
            SoapStr = SoapStr + "\r\n" + "<v17:TaxesOrMiscellaneousCharge>";
            SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";
            SoapStr = SoapStr + "\r\n" + "<v17:Amount>0.00</v17:Amount>";
            SoapStr = SoapStr + "\r\n" + "</v17:TaxesOrMiscellaneousCharge>";
            SoapStr = SoapStr + "\r\n" + "<v17:TaxesOrMiscellaneousChargeType>TAXES</v17:TaxesOrMiscellaneousChargeType>";
            SoapStr = SoapStr + "\r\n" + "<v17:PackingCosts>";
            SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";
            SoapStr = SoapStr + "\r\n" + "<v17:Amount>0.00</v17:Amount>";
            SoapStr = SoapStr + "\r\n" + "</v17:PackingCosts>";
            SoapStr = SoapStr + "\r\n" + "<v17:HandlingCosts>";
            SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";
            SoapStr = SoapStr + "\r\n" + "<v17:Amount>0.00</v17:Amount>";
            SoapStr = SoapStr + "\r\n" + "</v17:HandlingCosts>";
            SoapStr = SoapStr + "\r\n" + "<v17:SpecialInstructions>No Instruction.</v17:SpecialInstructions>";
            SoapStr = SoapStr + "\r\n" + "<v17:DeclarationStatement>China</v17:DeclarationStatement>";
            SoapStr = SoapStr + "\r\n" + "<!--<v17:PaymentTerms>?</v17:PaymentTerms>:-->";
            SoapStr = SoapStr + "\r\n" + "<!--Purpose, Sold, Not Sold, Gift, Sample, Repair and Return, Personal Effects:-->";
            SoapStr = SoapStr + "\r\n" + "<v17:Purpose>" + txtPurpose.Text + "</v17:Purpose>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferences>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferenceType>INVOICE_NUMBER</v17:CustomerReferenceType>";
            SoapStr = SoapStr + "\r\n" + "<v17:Value>" + txtInvoiceNo.Text + "</v17:Value>";
            SoapStr = SoapStr + "\r\n" + "</v17:CustomerReferences>";
            SoapStr = SoapStr + "\r\n" + "<!--TermsOfSale, CRF_OR_CPT, CIF_OR_CIP,DDP,DDU,DAP,DAT,EXW,FOB_OR_FCA:-->";
            SoapStr = SoapStr + "\r\n" + "<v17:TermsOfSale>" + txtTema.Text + "</v17:TermsOfSale>";
            SoapStr = SoapStr + "\r\n" + "</v17:CommercialInvoice>";

            for (int i = 0; i <= dtMCEOrderProInfo.Rows.Count - 1; i++)
            {


                SoapStr = SoapStr + "\r\n" + "<v17:Commodities>";
                SoapStr = SoapStr + "\r\n" + "<v17:NumberOfPieces>1</v17:NumberOfPieces>";
                SoapStr = SoapStr + "\r\n" + "<v17:Description>" + cFedexStr.GetXmlStr(dtMCEOrderProInfo.Rows[i]["PDescription"].ToString()) + "</v17:Description>";
                SoapStr = SoapStr + "\r\n" + "<v17:CountryOfManufacture>" + dtMCEOrderProInfo.Rows[i]["manuf"].ToString() + "</v17:CountryOfManufacture>";
                SoapStr = SoapStr + "\r\n" + "<v17:HarmonizedCode>" + dtMCEOrderProInfo.Rows[i]["Hcode"].ToString() + "</v17:HarmonizedCode>";
                SoapStr = SoapStr + "\r\n" + "<!--Specify the commoditie total weight :-->";
                SoapStr = SoapStr + "\r\n" + "<v17:Weight>";
                SoapStr = SoapStr + "\r\n" + "<v17:Units>LB</v17:Units>";
                SoapStr = SoapStr + "\r\n" + "<v17:Value>" + dtMCEOrderProInfo.Rows[i]["ProSize"].ToString() + "</v17:Value>";
                SoapStr = SoapStr + "\r\n" + "</v17:Weight>";
                SoapStr = SoapStr + "\r\n" + "<v17:Quantity>" + dtMCEOrderProInfo.Rows[i]["ProQuantity"].ToString() + "</v17:Quantity>";
                SoapStr = SoapStr + "\r\n" + "<v17:QuantityUnits>" + dtMCEOrderProInfo.Rows[i]["Unit"].ToString() + "</v17:QuantityUnits>";
                SoapStr = SoapStr + "\r\n" + "<v17:UnitPrice>";
                SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";


                SoapStr = SoapStr + "\r\n" + "<v17:Amount>" + Convert.ToDouble(dtMCEOrderProInfo.Rows[i]["ProAmount"].ToString()) / Convert.ToDouble(dtMCEOrderProInfo.Rows[i]["ProQuantity"].ToString()) + "</v17:Amount>";
                SoapStr = SoapStr + "\r\n" + "</v17:UnitPrice>";
                SoapStr = SoapStr + "\r\n" + "<v17:CustomsValue>";
                SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";
                SoapStr = SoapStr + "\r\n" + "<v17:Amount>" + dtMCEOrderProInfo.Rows[i]["ProAmount"] + "</v17:Amount>";
                SoapStr = SoapStr + "\r\n" + "</v17:CustomsValue>";
                SoapStr = SoapStr + "\r\n" + "<v17:PartNumber></v17:PartNumber>";
                SoapStr = SoapStr + "\r\n" + "</v17:Commodities>";

            }

            SoapStr = SoapStr + "\r\n" + "</v17:CustomsClearanceDetail>";





            SoapStr = SoapStr + "\r\n" + "<v17:LabelSpecification>";
            SoapStr = SoapStr + "\r\n" + "<v17:LabelFormatType>COMMON2D</v17:LabelFormatType>";
            SoapStr = SoapStr + "\r\n" + "<v17:ImageType>PDF</v17:ImageType>";
            SoapStr = SoapStr + "\r\n" + "<v17:LabelStockType>PAPER_8.5X11_TOP_HALF_LABEL</v17:LabelStockType>";
            SoapStr = SoapStr + "\r\n" + "<v17:LabelPrintingOrientation>TOP_EDGE_OF_TEXT_FIRST</v17:LabelPrintingOrientation>";
            SoapStr = SoapStr + "\r\n" + "</v17:LabelSpecification>";



            SoapStr = SoapStr + "\r\n" + "<!--Specify the printing type of Commercial Invoice.:-->";
            SoapStr = SoapStr + "\r\n" + "<v17:ShippingDocumentSpecification>";
            SoapStr = SoapStr + "\r\n" + "<v17:ShippingDocumentTypes>COMMERCIAL_INVOICE</v17:ShippingDocumentTypes>";
            SoapStr = SoapStr + "\r\n" + "<v17:CommercialInvoiceDetail>";
            SoapStr = SoapStr + "\r\n" + "<v17:Format>";
            SoapStr = SoapStr + "\r\n" + "<v17:ImageType>PDF</v17:ImageType>";
            SoapStr = SoapStr + "\r\n" + "<v17:StockType>PAPER_LETTER</v17:StockType>";
            SoapStr = SoapStr + "\r\n" + "</v17:Format>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomerImageUsages>";
            SoapStr = SoapStr + "\r\n" + "<!--Optional:-->";
            SoapStr = SoapStr + "\r\n" + "<v17:Type>LETTER_HEAD</v17:Type>";
            SoapStr = SoapStr + "\r\n" + "<!--Optional:-->";

            if (txtCompany.Text.IndexOf("Chemscene") >= 0)
                SoapStr = SoapStr + "\r\n" + "<v17:Id>IMAGE_3</v17:Id>";
            else
                SoapStr = SoapStr + "\r\n" + "<v17:Id>IMAGE_1</v17:Id>";
            //    SoapStr = SoapStr + "\r\n" + "<v17:Id>IMAGE_1</v17:Id>";
            SoapStr = SoapStr + "\r\n" + "</v17:CustomerImageUsages>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomerImageUsages>";
            SoapStr = SoapStr + "\r\n" + "<v17:Type>SIGNATURE</v17:Type>";
            SoapStr = SoapStr + "\r\n" + "<v17:Id>IMAGE_2</v17:Id>";
            SoapStr = SoapStr + "\r\n" + "</v17:CustomerImageUsages>";
            SoapStr = SoapStr + "\r\n" + "</v17:CommercialInvoiceDetail>";
            SoapStr = SoapStr + "\r\n" + "</v17:ShippingDocumentSpecification>";
            SoapStr = SoapStr + "\r\n" + "<v17:RateRequestTypes>ACCOUNT</v17:RateRequestTypes>";


            SoapStr = SoapStr + "\r\n" + "<v17:PackageCount>1</v17:PackageCount>";
            SoapStr = SoapStr + "\r\n" + "<v17:RequestedPackageLineItems>";
            SoapStr = SoapStr + "\r\n" + "<v17:SequenceNumber>1</v17:SequenceNumber>";
            SoapStr = SoapStr + "\r\n" + "<v17:InsuredValue>";
            SoapStr = SoapStr + "\r\n" + "<v17:Currency>USD</v17:Currency>";
            SoapStr = SoapStr + "\r\n" + "<v17:Amount>0</v17:Amount>";
            SoapStr = SoapStr + "\r\n" + "</v17:InsuredValue>";
            SoapStr = SoapStr + "\r\n" + "<v17:Weight>";
            SoapStr = SoapStr + "\r\n" + "<v17:Units>LB</v17:Units>";
            SoapStr = SoapStr + "\r\n" + "<v17:Value>" + txtWeight.Text + "</v17:Value>";
            SoapStr = SoapStr + "\r\n" + "</v17:Weight>";


            if (txtPackageType.Text == "YOUR_PACKAGING")
            {
                SoapStr = SoapStr + "\r\n" + " <v17:Dimensions>";
                SoapStr = SoapStr + "\r\n" + "<v17:Length>" + txtL.Text + "</v17:Length>";
                SoapStr = SoapStr + "\r\n" + "<v17:Width>" + txtW.Text + "</v17:Width>";
                SoapStr = SoapStr + "\r\n" + "<v17:Height>" + txtH.Text + "</v17:Height>";
                SoapStr = SoapStr + "\r\n" + "<v17:Units>IN</v17:Units>";
                SoapStr = SoapStr + "\r\n" + "</v17:Dimensions>";
            }


            SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferences>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferenceType>CUSTOMER_REFERENCE</v17:CustomerReferenceType>";
            SoapStr = SoapStr + "\r\n" + "<v17:Value>" + txtReference.Text + "</v17:Value>";
            SoapStr = SoapStr + "\r\n" + "</v17:CustomerReferences>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferences>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferenceType>P_O_NUMBER</v17:CustomerReferenceType>";
            SoapStr = SoapStr + "\r\n" + "<v17:Value>" + cFedexStr.GetXmlStr(txtPoNo.Text) + "</v17:Value>";
            SoapStr = SoapStr + "\r\n" + "</v17:CustomerReferences>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferences>";
            SoapStr = SoapStr + "\r\n" + "<v17:CustomerReferenceType>INVOICE_NUMBER</v17:CustomerReferenceType>";
            SoapStr = SoapStr + "\r\n" + "<v17:Value>" + txtInvoiceNo.Text + "</v17:Value>";
            SoapStr = SoapStr + "\r\n" + "</v17:CustomerReferences>";
            SoapStr = SoapStr + "\r\n" + "</v17:RequestedPackageLineItems>";
            SoapStr = SoapStr + "\r\n" + "</v17:RequestedShipment>";
            SoapStr = SoapStr + "\r\n" + "</v17:ProcessShipmentRequest>";
            SoapStr = SoapStr + "\r\n" + "</soapenv:Body>";
            SoapStr = SoapStr + "\r\n" + "</soapenv:Envelope>";



            string labPath;
            string inovicePath;
            string trkNO;
            string result = cFedexStr.GenerateShipment(SoapStr, out labPath, out inovicePath, out trkNO);
            if (result == "成功")
            {
                traNo = trkNO;
                Yf = txtRate.Text;
                if (labPath != "")
                {
                    System.Diagnostics.Process.Start(@labPath);
                }
                if (inovicePath != "")
                {
                    System.Diagnostics.Process.Start(@inovicePath);
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageDxUtil.ShowTips(result);
            }
        }

        private void butGetCode_Click(object sender, EventArgs e)
        {

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string CountryCode = cFedexStr.GetCountryCode(txtShipCountry.Text);
            WebAutoLogin.PostLogin("https://www.fedex.com/siteminderagent/forms/fdxlogin.fcc", "chemscene", "Haoyuan888");
            WebAutoLogin.GetPage("https://www.fedex.com/HSL?action=searching&clienttype=dotcom&cntry_code=us&country=" + CountryCode + "&formname=homepage&fromcountry=&hazmatFilter=All&jsaction=&lang_code=en&logout=true&returntype=both&searchtext=" + txtHz.Text + "&searchtype=2&selectedDesc=&selectedHS=&setDomain=&showCountryError=&submitType=&submit_type=&submitaction=&unlockCountry=true&validateSearchText=");
            doc.LoadHtml(WebAutoLogin.ResultHtml);


            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Harmonized Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));



            HtmlNodeCollection kwBox1 = doc.DocumentNode.SelectNodes("//input[@name='hsnum']");
            foreach (HtmlNode item in kwBox1)
            {
                DataRow _drNew = dt.NewRow();
                _drNew["Harmonized Code"] = item.ParentNode.InnerText.ToString().Replace("\r", "").Replace("\t", "").Replace("\n", "").Replace("&nbsp;", "");
                _drNew["Description"] = item.ParentNode.NextSibling.NextSibling.InnerText.ToString().Replace("\r", "").Replace("\t", "").Replace("\n", "");
                dt.Rows.Add(_drNew);
                //text1 = item.ParentNode.SelectSingleNode("./span").InnerHtml.ToString().Replace("\r", "").Replace("\t", "").Replace("\n", ""); // 2 ji Harmonized Code
                //text1 = item.ParentNode.NextSibling.NextSibling.InnerHtml.ToString().Replace("\r", "").Replace("\t", "").Replace("\n", ""); //Description
                //text1 = item.ParentNode.SelectSingleNode("./b").InnerHtml; //Harmonized Code
                //text1 = item.ParentNode.NextSibling.NextSibling.InnerHtml.ToString().Replace("\r", "").Replace("\t", "").Replace("\n", ""); //Description

            }

            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
            layoutControlItem63.ContentVisible = true;



        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle > -1)
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                string CountryCode = cFedexStr.GetCountryCode(txtShipCountry.Text);
                WebAutoLogin.PostLogin("https://www.fedex.com/siteminderagent/forms/fdxlogin.fcc", "MedChemTr", "Sju6fem4X");
                WebAutoLogin.GetPage("https://www.fedex.com/HSL?action=searching&clienttype=dotcom&cntry_code=us&country=" + CountryCode + "&formname=homepage&fromcountry=&hazmatFilter=All&jsaction=&lang_code=en&logout=true&returntype=both&searchtext=" + txtHz.Text + "&searchtype=2&selectedDesc=" + this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "Description").ToString() + "&selectedHS=" + this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "Harmonized Code").ToString() + "&setDomain=&showCountryError=&submitType=combinedsearch&submit_type=&submitaction=&unlockCountry=true&validateSearchText=");

                doc.LoadHtml(WebAutoLogin.ResultHtml);


                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Harmonized Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Description", typeof(string)));



                HtmlNodeCollection kwBox1 = doc.DocumentNode.SelectNodes("//input[@name='hsnum']");
                foreach (HtmlNode item in kwBox1)
                {
                    DataRow _drNew = dt.NewRow();
                    _drNew["Harmonized Code"] = item.ParentNode.InnerText.ToString().Replace("\r", "").Replace("\t", "").Replace("\n", "").Replace("&nbsp;", "");
                    _drNew["Description"] = item.ParentNode.NextSibling.NextSibling.InnerText.ToString().Replace("\r", "").Replace("\t", "").Replace("\n", "").Replace("&#45;", "-").Replace("&#32;", " ");
                    dt.Rows.Add(_drNew);
                    //text1 = item.ParentNode.SelectSingleNode("./span").InnerHtml.ToString().Replace("\r", "").Replace("\t", "").Replace("\n", ""); // 2 ji Harmonized Code
                    //text1 = item.ParentNode.NextSibling.NextSibling.InnerHtml.ToString().Replace("\r", "").Replace("\t", "").Replace("\n", ""); //Description
                    //text1 = item.ParentNode.SelectSingleNode("./b").InnerHtml; //Harmonized Code
                    //text1 = item.ParentNode.NextSibling.NextSibling.InnerHtml.ToString().Replace("\r", "").Replace("\t", "").Replace("\n", ""); //Description

                }

                gridControl3.DataSource = dt;
                this.gridView5.BestFitColumns();
                layoutControlItem68.ContentVisible = true;
            }
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            if (this.gridView5.FocusedRowHandle > -1)
            {

                txtHz.Text = this.gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, "Harmonized Code").ToString();
            }
        }

        private void checkAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAddress.Checked)
            {
                string addres;
                addres = checkAddress.Text;
                string[] addreszj;

                if (addres.IndexOf("|") > 0)
                {
                    addreszj = addres.Split('|');


                    txtShipAddress1.Text = EuSoft.Common.StringPlus.CutStr(addreszj[0], ":", false);
                    if (addreszj[1].IndexOf("StreetLines") > -1)

                        txtShipAddress2.Text = EuSoft.Common.StringPlus.CutStr(addreszj[1], ":", false);
                    // txtShipCity.Text = EuSoft.Common.StringPlus.CutStr(addreszj[2], ":", false);
                    if (addreszj[3].IndexOf("PostalCode") > -1)
                        txtShipZip.Text = EuSoft.Common.StringPlus.CutStr(addreszj[3], ":", false);
                    if (addreszj[4].IndexOf("PostalCode") > -1)
                        txtShipZip.Text = EuSoft.Common.StringPlus.CutStr(addreszj[4], ":", false);


                    txtShipAddress1.BackColor = Color.Yellow;
                    txtShipAddress2.BackColor = Color.Yellow;
                    //  txtShipCity.BackColor = Color.Yellow;
                    txtShipZip.BackColor = Color.Yellow;
                }

            }
            else
            {
                txtShipAddress1.Text = txtShipAddress1.ToolTip;
                txtShipAddress2.Text = txtShipAddress2.ToolTip;
                txtShipZip.Text = txtShipZip.ToolTip;
                //   txtShipCity.Text = txtShipCity.ToolTip;
                txtShipAddress1.BackColor = Color.White;
                txtShipAddress2.BackColor = Color.White;
                //  txtShipCity.BackColor = Color.White;
                txtShipZip.BackColor = Color.White;
            }
        }

        public void Base64StringToFile(string strbase64, string Tra)
        {
            byte[] data = Convert.FromBase64String(strbase64);
            //   var filename = @"c:\a.gif";

            string filename = String.Format("{0}{1}.gif", Application.StartupPath + @"\PDF\", Tra);
            //保存文件
            using (var mem = new MemoryStream(data))
            {
                using (var file = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    mem.WriteTo(file);
                }
            }
            string LabelFileName1 = String.Format("{0}{1}.pdf", Application.StartupPath + @"\PDF\", Tra);
            ConPDF(filename, LabelFileName1);
        }



        public void ConPDF(string source, string output)
        {
            iTextSharp.text.Image[] images = iTextSharp.text.Image.GetInstance(source).ToArray();
            iTextSharp.text.Image image = images[0];
            using (FileStream fs = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (Document doc = new Document(image))
                {
                    using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                    {
                        //writer.CompressionLevel = 0;
                        writer.SetFullCompression();
                        writer.SetPdfVersion(iTextSharp.text.pdf.PdfWriter.PDF_VERSION_1_7);
                        //writer.CompressionLevel = PdfStream.NO_COMPRESSION; 

                        doc.Open();
                        image.SetAbsolutePosition(0, 0);
                        doc.SetPageSize(new iTextSharp.text.Rectangle(0, 0, image.Width, image.Height * 2, 0));
                        doc.NewPage();

                        writer.DirectContent.AddImage(image, false);
                        doc.Close();
                    }
                }
            }
        }


        public static string sendPost(string postUrl, string postDataStr)
        {

            //用来存放cookie
            CookieContainer cookie = null;
            HttpWebRequest request = null;
            Stream myRequestStream = null;
            HttpWebResponse response = null;
            Stream myResponseStream = null;
            StreamReader myStreamReader = null;
            try
            {
                //转化
                byte[] byteArray = Encoding.UTF8.GetBytes(postDataStr);
                cookie = new CookieContainer();
                //发送一个POST请求
                request = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                request.CookieContainer = cookie;
                //request.Timeout = 3000;
                request.Method = "POST";
                //application/x-www-form-urlencoded
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                myRequestStream = request.GetRequestStream();
                myRequestStream.Write(byteArray, 0, byteArray.Length);
                myRequestStream.Close();
                //获取返回的内容
                response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookie.GetCookies(response.ResponseUri);
                myResponseStream = response.GetResponseStream();
                myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                return myStreamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine("postUrl = " + postUrl + "  Exception" + ex);
            }
            finally
            {
                if (myStreamReader != null)
                {
                    myStreamReader.Close();
                }
                if (myResponseStream != null)
                {
                    myResponseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }

            return "";
        }

        private void UPSRate_Click(object sender, EventArgs e)
        {
            StringBuilder upsJson = new StringBuilder();
            upsJson.Clear();
            upsJson.Append("{");
            upsJson.Append("\"UPSSecurity\": {");
            upsJson.Append("\"UsernameToken\": {");
            upsJson.Append("\"Username\": \"medchemexpress\",");
            upsJson.Append("\"Password\": \"52MedChem\"");
            upsJson.Append("},");
            upsJson.Append("\"ServiceAccessToken\": {");
            upsJson.Append("\"AccessLicenseNumber\": \"9D2BF7D7AF74C9FD\"");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"RateRequest\": {");
            upsJson.Append("\"Request\": {");
            upsJson.Append("\"RequestOption\": \"Rate\",");
            upsJson.Append("\"TransactionReference\": {");
            upsJson.Append("\"CustomerContext\": \"Your Customer Context\"");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"Shipment\": {");
            upsJson.Append("\"Shipper\": {");
            upsJson.Append("\"Name\": \"" + txtCompany.Text + "\",");
            upsJson.Append("\"ShipperNumber\": \"122339\",");
            upsJson.Append("\"Address\": {");
            upsJson.Append("\"AddressLine\": [");
            string addRessxml = "\"" + cFedexStr.GetXmlStr(txtAddress1.Text) + "\"";
            if (txtAddress2.Text != "")
            {
                addRessxml = addRessxml + "," + "\"" + cFedexStr.GetXmlStr(txtAddress2.Text) + "\"";
            }
            upsJson.Append("" + addRessxml + "");
            upsJson.Append("],");
            upsJson.Append("\"City\": \"" + txtCity.Text + "\",");
            upsJson.Append("\"StateProvinceCode\": \"NJ\",");
            upsJson.Append("\"PostalCode\": \"08852\",");
            upsJson.Append("\"CountryCode\": \"US\"");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"ShipTo\": {");
            upsJson.Append("\"Name\": \"+ cFedexStr.GetXmlStr(txtShipCompany.Text) +\",");
            upsJson.Append("\"Address\": {");
            upsJson.Append("\"AddressLine\": [");
            addRessxml = "\"" + cFedexStr.GetXmlStr(txtShipAddress1.Text) + "\"";
            if (txtShipAddress2.Text != "")
            {
                addRessxml = addRessxml + "," + "\"" + cFedexStr.GetXmlStr(txtShipAddress2.Text) + "\"";
            }
            upsJson.Append("" + addRessxml + "");
            upsJson.Append("],");
            upsJson.Append("\"City\": \"" + txtShipCity.Text + "\",");
            upsJson.Append("\"StateProvinceCode\": \"" + cFedexStr.GetStateOrProvinceCode("US", txtShipZip.Text) + "\",");
            upsJson.Append("\"PostalCode\": \"" + txtShipZip.Text + "\",");
            upsJson.Append("\"CountryCode\": \"US\"");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"ShipFrom\": {");
            upsJson.Append("\"Name\": \"" + txtCompany.Text + "\",");
            upsJson.Append("\"Address\": {");
            upsJson.Append("\"AddressLine\": [");
            addRessxml = "\"" + cFedexStr.GetXmlStr(txtAddress1.Text) + "\"";
            if (txtAddress2.Text != "")
            {
                addRessxml = addRessxml + "," + "\"" + cFedexStr.GetXmlStr(txtAddress2.Text) + "\"";
            }
            upsJson.Append("" + addRessxml + "");
            upsJson.Append("],");
            upsJson.Append("\"City\": \"" + txtCity.Text + "\",");
            upsJson.Append("\"StateProvinceCode\": \"NJ\",");
            upsJson.Append("\"PostalCode\": \"08852\",");
            upsJson.Append("\"CountryCode\": \"US\"");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"Service\": {");
            upsJson.Append("\"Code\": \"01\",");
            upsJson.Append("\"Description\": \"Express\"");
            upsJson.Append("},");
            upsJson.Append("\"Package\": {");
            if (txtPackageType.Text == "YOUR_PACKAGING")
            {

                upsJson.Append("\"PackagingType\": {\"Code\": \"02\", \"Description\": \"Rate\" }, \"Dimensions\": { \"UnitOfMeasurement\": { \"Code\": \"IN\", \"Description\": \"inches\" }, \"Length\": \"9\", \"Width\": \"6\", \"Height\": \"3\" },");

            }
            else
            {
                upsJson.Append("\"PackagingType\": {");
                upsJson.Append("\"Code\": \"01\",");
                upsJson.Append("\"Description\": \"Rate\"");
                upsJson.Append("},");
            }






            upsJson.Append("\"PackageWeight\": {");
            upsJson.Append("\"UnitOfMeasurement\": {");
            upsJson.Append("\"Code\": \"Lbs\",");
            upsJson.Append("\"Description\": \"pounds\"");
            upsJson.Append("},");
            upsJson.Append("\"Weight\": \"" + txtWeight.Text + "\"");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"ShipmentRatingOptions\": {");
            upsJson.Append("\"NegotiatedRatesIndicator\": \"\"");
            upsJson.Append("}");
            upsJson.Append("}");
            upsJson.Append("}");
            upsJson.Append("}");

            string aa = upsJson.ToString();


            string retJson = sendPost("https://onlinetools.ups.com/rest/Rate", aa);
            if (retJson!="")
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(retJson);
                string retStatues = "";
                if (retJson.IndexOf("error") > 0)
                {
                    retStatues = jo["Error"]["Description"].ToString();
                    return;
                }
                if (retJson.IndexOf("Fault") > 0)
                {
                    retStatues = jo["Fault"]["detail"]["Errors"]["ErrorDetail"]["PrimaryErrorCode"]["Description"].ToString();
                    return;
                }



                retStatues = jo["RateResponse"]["RatedShipment"]["NegotiatedRateCharges"]["TotalCharge"]["MonetaryValue"].ToString();
                Yf = retStatues;
                txtRate.Text = Yf;
            }
            else
            {
                MessageDxUtil.ShowError("UPS 连接失败");
            }    

           
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            StringBuilder upsJson = new StringBuilder();
            upsJson.Clear();

            upsJson.Append("{");
            upsJson.Append("\"UPSSecurity\": {" + "\r\n");
            upsJson.Append("\"UsernameToken\": {" + "\r\n");
            upsJson.Append("\"Username\": \"medchemexpress\"," + "\r\n");
            upsJson.Append("\"Password\": \"52MedChem\"" + "\r\n");
            upsJson.Append("}," + "\r\n");
            upsJson.Append("\"ServiceAccessToken\": {" + "\r\n");
            upsJson.Append("\"AccessLicenseNumber\": \"9D2BF7D7AF74C9FD\"" + "\r\n");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"ShipmentRequest\": {");
            upsJson.Append("\"Request\": {");
            upsJson.Append("\"RequestOption\": \"validate\",");
            upsJson.Append("\"TransactionReference\": {");
            upsJson.Append("\"CustomerContext\": \"Your Customer Context\"");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"Shipment\": {");
            upsJson.Append("\"Description\": \"" + txtReference.Text + "\",");
            upsJson.Append("\"Shipper\": {");
            upsJson.Append("\"Name\": \"" + txtSenders.Text + "\",");
            upsJson.Append("\"AttentionName\": \"" + txtCompany.Text + "\",");
            upsJson.Append("\"TaxIdentificationNumber\": \"\",");
            upsJson.Append("\"Phone\": {");
            upsJson.Append("\"Number\": \"" + txtPhone.Text + "\",");
            upsJson.Append("\"Extension\": \"\"");
            upsJson.Append("},");
            upsJson.Append("\"ShipperNumber\": \"122339\",");
            upsJson.Append("\"FaxNumber\": \"\",");
            upsJson.Append("\"Address\": {");
            upsJson.Append("\"AddressLine\":");
            upsJson.Append("[");
            string addRessxml = "\"" + cFedexStr.GetXmlStr(txtAddress1.Text) + "\"";
            if (txtAddress2.Text != "")
            {
                addRessxml = addRessxml + "," + "\"" + cFedexStr.GetXmlStr(txtAddress2.Text) + "\"";
            }
            upsJson.Append("" + addRessxml + "");
            //  upsJson.Append("\"" + txtAddress2.Text + "\"");
            upsJson.Append("],");
            upsJson.Append("\"City\": \"" + txtCity.Text + "\",");
            upsJson.Append("\"StateProvinceCode\": \"NJ\",");
            upsJson.Append("\"PostalCode\": \"08852\",");
            upsJson.Append("\"CountryCode\": \"US\"");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"ShipTo\": {");
            upsJson.Append("\"Name\": \"" + cFedexStr.GetXmlStr(txtShipCompany.Text) + "\",");
            upsJson.Append("\"AttentionName\": \"" + cFedexStr.GetXmlStr(txtShipContactName.Text) + "\",");
            upsJson.Append("\"Phone\": {");
            upsJson.Append("\"Number\": \"" + txtShipPhone.Text + "\"");
            upsJson.Append("},");
            upsJson.Append("\"Address\": {");
            upsJson.Append("\"AddressLine\":");
            upsJson.Append("[");
            addRessxml = "\"" + cFedexStr.GetXmlStr(txtShipAddress1.Text) + "\"";
            if (txtShipAddress2.Text != "")
            {
                addRessxml = addRessxml + "," + "\"" + cFedexStr.GetXmlStr(txtShipAddress2.Text) + "\"";
            }
            upsJson.Append("" + addRessxml + "");
            upsJson.Append("],");
            upsJson.Append("\"City\": \"" + txtShipCity.Text + "\",");
            upsJson.Append("\"StateProvinceCode\": \"" + cFedexStr.GetStateOrProvinceCode("US", txtShipZip.Text) + "\",");
            upsJson.Append("\"PostalCode\": \"" + txtShipZip.Text + "\",");
            upsJson.Append("\"CountryCode\": \"US\"");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"ShipFrom\": {");
            upsJson.Append("\"Name\": \"" + txtSenders.Text + "\",");
            upsJson.Append("\"AttentionName\": \"" + txtCompany.Text + "\",");
            upsJson.Append("\"Phone\": {");
            upsJson.Append("\"Number\": \"" + txtPhone.Text + "\"");
            upsJson.Append("},");
            upsJson.Append("\"FaxNumber\": \"\",");
            upsJson.Append("\"Address\": {");
            upsJson.Append("\"AddressLine\":");
            upsJson.Append("[");
            upsJson.Append("\"" + txtAddress1.Text + "\",");
            upsJson.Append("\"" + txtAddress2.Text + "\"");
            upsJson.Append("],");
            upsJson.Append("\"City\": \"" + txtCity.Text + "\",");
            upsJson.Append("\"StateProvinceCode\": \"NJ\",");
            upsJson.Append("\"PostalCode\": \"08852\",");
            upsJson.Append("\"CountryCode\": \"US\"");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"PaymentInformation\": {");
            upsJson.Append("\"ShipmentCharge\": {");
            upsJson.Append("\"Type\": \"01\",");
            upsJson.Append("\"BillShipper\": {");
            upsJson.Append("\"AccountNumber\": \"122339\"");
            upsJson.Append("}");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"Service\": {");
            upsJson.Append("\"Code\": \"01\",");
            upsJson.Append("\"Description\": \"Express\"");
            upsJson.Append("},");
            upsJson.Append("\"Package\": {");
            upsJson.Append("\"Description\": \"11111\",");
            upsJson.Append("\"ReferenceNumber\": {");
            upsJson.Append("\"Code\": \"PO\",");
            upsJson.Append("\"Value\": \"" + cFedexStr.GetXmlStr(txtPoNo.Text) + " INV:" + cFedexStr.GetXmlStr(txtInvoiceNo.Text) + "\"");
            upsJson.Append("},");
            //upsJson.Append("\"Packaging\": {");
            //upsJson.Append("\"Code\": \"01\",");
            //upsJson.Append("\"Description\": \"Description\"");
            //upsJson.Append("},");
            if (txtPackageType.Text == "YOUR_PACKAGING")
            {

                upsJson.Append("\"Packaging\": { \"Code\": \"02\", \"Description\": \"Description\" }, \"Dimensions\": { \"UnitOfMeasurement\": { \"Code\": \"IN\", \"Description\": \"Inches\" }, \"Length\": \"9\", \"Width\": \"6\", \"Height\": \"3\" },");
            }
            else
            {
                upsJson.Append("\"Packaging\": {");
                upsJson.Append("\"Code\": \"01\",");
                upsJson.Append("\"Description\": \"Description\"");
                upsJson.Append("},");
            }


            upsJson.Append("\"PackageServiceOptions\": {");
            upsJson.Append("\"Notification\":{");
            upsJson.Append("\"NotificationCode\":\"6\",");
            upsJson.Append("\"EMail\":{");
            string emialxml = "\"" + cFedexStr.GetXmlStr(txtMyEmial.Text) + "\"";
            if (txtShipEmial.Text != "")
            {
                emialxml = emialxml + "," + "\"" + cFedexStr.GetXmlStr(txtShipEmial.Text) + "\"";
            }
            if (txtShipEmial1.Text != "")
            {
                emialxml = emialxml + "," + "\"" + cFedexStr.GetXmlStr(txtShipEmial1.Text) + "\"";
            }
            upsJson.Append("\"EMailAddress\":[" + emialxml + "]");
            upsJson.Append("}");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"PackageWeight\": {");
            upsJson.Append("\"UnitOfMeasurement\": {");
            upsJson.Append("\"Code\": \"LBS\",");
            upsJson.Append("\"Description\": \"Pounds\"");
            upsJson.Append("},");
            upsJson.Append("\"Weight\": \"" + txtWeight.Text + "\"");
            upsJson.Append("}");
            upsJson.Append("}");
            upsJson.Append("},");
            upsJson.Append("\"LabelSpecification\": {");
            upsJson.Append("\"LabelImageFormat\": {");
            upsJson.Append("\"Code\": \"GIF\",");
            upsJson.Append("\"Description\": \"GIF\"");
            upsJson.Append("},");
            upsJson.Append("\"HTTPUserAgent\": \"Mozilla/4.5\"");
            upsJson.Append("}");
            upsJson.Append("}");
            upsJson.Append("}");












            string aa = upsJson.ToString();


            string retJson = sendPost("https://onlinetools.ups.com/rest/Ship", aa);
            if (retJson!="")
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(retJson);
                string retStatues = "";
                if (retJson.IndexOf("error") > 0)
                {
                    retStatues = jo["Error"]["Description"].ToString();

                }
                if (retJson.IndexOf("Fault") > 0)
                {
                    retStatues = jo["Fault"]["detail"]["Errors"]["ErrorDetail"]["PrimaryErrorCode"]["Description"].ToString();

                }
                if (retStatues != "")
                {
                    MessageDxUtil.ShowTips(retStatues);
                    return;
                }



                string upsTraNo = jo["ShipmentResponse"]["ShipmentResults"]["ShipmentIdentificationNumber"].ToString();
                //   string TotalCharges = jo["ShipmentResponse"]["ShipmentResults"]["ShipmentCharges"]["TotalCharges"]["MonetaryValue"].ToString();
                string gif = jo["ShipmentResponse"]["ShipmentResults"]["PackageResults"]["ShippingLabel"]["GraphicImage"].ToString();
                //  gif = jo["ShipmentResponse"]["ShipmentResults"]["PackageResults"]["ShippingLabel"]["HTMLImage"].ToString();

                Base64StringToFile(gif, upsTraNo);


                traNo = upsTraNo;
                //   Yf = TotalCharges;
                string LabelFileName1 = String.Format("{0}{1}.pdf", Application.StartupPath + @"\PDF\", traNo);
                System.Diagnostics.Process.Start(LabelFileName1);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageDxUtil.ShowError("UPS 连接失败");
            }

          

        }




    }
}
