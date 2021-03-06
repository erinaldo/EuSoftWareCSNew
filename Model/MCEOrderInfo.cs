﻿using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEOrderInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEOrderInfo
	{
		public MCEOrderInfo()
		{}
		#region Model
		private int _id;
		private string _orderno;
		private string _salescompany;
		private string _salescontactname;
		private string _salesstreet;
		private string _salescity;
		private string _salesstate;
		private string _saleszip;
		private string _salescountry;
		private string _salestel;
		private string _salesfax;
		private string _salesemail;
		private string _billcompany;
		private string _billcontactname;
		private string _billstreet;
		private string _billcity;
		private string _billstate;
		private string _billzip;
		private string _billcountry;
		private string _billtel;
		private string _billfax;
		private string _billemail;
		private string _shipcompany;
		private string _shipcontactname;
		private string _shipstreet;
		private string _shipcity;
		private string _shipstate;
		private string _shipzip;
		private string _shipcountry;
		private string _shiptel;
		private string _shipfax;
		private string _shipemail;
		private string _invioceno;
		private string _vendorcode;
		private string _terms;
		private string _paymethod;
		private DateTime? _orderdate;
		private string _ponumber;
		private string _customerrefno;
		private int? _sh;
		private string _comments;
		private string _note;
		private string _orderstatus;
		private string _orderprocess;
		private string _customerid;
		private DateTime? _invoicedate;
		private decimal? _invoicetotal;
		private string _paystatus;
		private string _shipstatus;
		private string _carrier;
		private string _accountno;
		private DateTime? _updatetime;
		private DateTime? _sendconfirmdate;
		private DateTime? _sendnewdate;
		private string _person;
	    private string _StockStatus;
        private decimal _Tax;
        private decimal _Taxation;

        public decimal Tax
        {
            get { return _Tax; }
            set { _Tax = value; }
        }

        public decimal Taxation
        {
            get { return _Taxation; }
            set { _Taxation = value; }
        }

        public string StockStatus
	    {
            set { _StockStatus = value; }
            get { return _StockStatus; }
	    }
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderNo
		{
			set{ _orderno=value;}
			get{return _orderno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesCompany
		{
			set{ _salescompany=value;}
			get{return _salescompany;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesContactName
		{
			set{ _salescontactname=value;}
			get{return _salescontactname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesStreet
		{
			set{ _salesstreet=value;}
			get{return _salesstreet;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesCity
		{
			set{ _salescity=value;}
			get{return _salescity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesState
		{
			set{ _salesstate=value;}
			get{return _salesstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesZip
		{
			set{ _saleszip=value;}
			get{return _saleszip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesCountry
		{
			set{ _salescountry=value;}
			get{return _salescountry;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesTel
		{
			set{ _salestel=value;}
			get{return _salestel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesFax
		{
			set{ _salesfax=value;}
			get{return _salesfax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SaleseMail
		{
			set{ _salesemail=value;}
			get{return _salesemail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillCompany
		{
			set{ _billcompany=value;}
			get{return _billcompany;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillContactName
		{
			set{ _billcontactname=value;}
			get{return _billcontactname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillStreet
		{
			set{ _billstreet=value;}
			get{return _billstreet;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillCity
		{
			set{ _billcity=value;}
			get{return _billcity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillState
		{
			set{ _billstate=value;}
			get{return _billstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillZip
		{
			set{ _billzip=value;}
			get{return _billzip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillCountry
		{
			set{ _billcountry=value;}
			get{return _billcountry;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillTel
		{
			set{ _billtel=value;}
			get{return _billtel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillFax
		{
			set{ _billfax=value;}
			get{return _billfax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BilleMail
		{
			set{ _billemail=value;}
			get{return _billemail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipCompany
		{
			set{ _shipcompany=value;}
			get{return _shipcompany;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipContactName
		{
			set{ _shipcontactname=value;}
			get{return _shipcontactname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipStreet
		{
			set{ _shipstreet=value;}
			get{return _shipstreet;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipCity
		{
			set{ _shipcity=value;}
			get{return _shipcity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipState
		{
			set{ _shipstate=value;}
			get{return _shipstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipZip
		{
			set{ _shipzip=value;}
			get{return _shipzip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipCountry
		{
			set{ _shipcountry=value;}
			get{return _shipcountry;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipTel
		{
			set{ _shiptel=value;}
			get{return _shiptel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipFax
		{
			set{ _shipfax=value;}
			get{return _shipfax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipeMail
		{
			set{ _shipemail=value;}
			get{return _shipemail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InvioceNo
		{
			set{ _invioceno=value;}
			get{return _invioceno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VendorCode
		{
			set{ _vendorcode=value;}
			get{return _vendorcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Terms
		{
			set{ _terms=value;}
			get{return _terms;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayMethod
		{
			set{ _paymethod=value;}
			get{return _paymethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? OrderDate
		{
			set{ _orderdate=value;}
			get{return _orderdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PONumber
		{
			set{ _ponumber=value;}
			get{return _ponumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomerRefNo
		{
			set{ _customerrefno=value;}
			get{return _customerrefno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SH
		{
			set{ _sh=value;}
			get{return _sh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Comments
		{
			set{ _comments=value;}
			get{return _comments;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Note
		{
			set{ _note=value;}
			get{return _note;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderStatus
		{
			set{ _orderstatus=value;}
			get{return _orderstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderProcess
		{
			set{ _orderprocess=value;}
			get{return _orderprocess;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomerID
		{
			set{ _customerid=value;}
			get{return _customerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? InvoiceDate
		{
			set{ _invoicedate=value;}
			get{return _invoicedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? InvoiceTotal
		{
			set{ _invoicetotal=value;}
			get{return _invoicetotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayStatus
		{
			set{ _paystatus=value;}
			get{return _paystatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipStatus
		{
			set{ _shipstatus=value;}
			get{return _shipstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Carrier
		{
			set{ _carrier=value;}
			get{return _carrier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountNo
		{
			set{ _accountno=value;}
			get{return _accountno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SendConfirmDate
		{
			set{ _sendconfirmdate=value;}
			get{return _sendconfirmdate;}
		}
	
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SendNewDate
		{
			set{ _sendnewdate=value;}
			get{return _sendnewdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Person
		{
			set{ _person=value;}
			get{return _person;}
		}
		#endregion Model

	}
}

