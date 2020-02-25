using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEOrderProInfoChangeRe:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEOrderProInfoChangeRe
	{
		public MCEOrderProInfoChangeRe()
		{}
		#region Model
		private int _id;
		private string _orderno;
		private string _procatalogno;
		private string _prodescription;
		private decimal? _prosize;
		private string _prounit;
		private int? _proquantity;
		private decimal? _proamount;
		private string _procurrency;
		private DateTime? _produnon;
		private string _pronote;
		private string _prolibraryid;
		private string _productstatus;
		private string _productprocess;
		private DateTime? _updatetime;
		private string _person;
		private string _changenote;
		private string _changeperson;
		private DateTime? _changetime;
		private string _reasons;
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
		public string ProCatalogNo
		{
			set{ _procatalogno=value;}
			get{return _procatalogno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProDescription
		{
			set{ _prodescription=value;}
			get{return _prodescription;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ProSize
		{
			set{ _prosize=value;}
			get{return _prosize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProUnit
		{
			set{ _prounit=value;}
			get{return _prounit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ProQuantity
		{
			set{ _proquantity=value;}
			get{return _proquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ProAmount
		{
			set{ _proamount=value;}
			get{return _proamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProCurrency
		{
			set{ _procurrency=value;}
			get{return _procurrency;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ProDunOn
		{
			set{ _produnon=value;}
			get{return _produnon;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProNote
		{
			set{ _pronote=value;}
			get{return _pronote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProLibraryID
		{
			set{ _prolibraryid=value;}
			get{return _prolibraryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductStatus
		{
			set{ _productstatus=value;}
			get{return _productstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductProcess
		{
			set{ _productprocess=value;}
			get{return _productprocess;}
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
		public string Person
		{
			set{ _person=value;}
			get{return _person;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChangeNote
		{
			set{ _changenote=value;}
			get{return _changenote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChangePerson
		{
			set{ _changeperson=value;}
			get{return _changeperson;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ChangeTime
		{
			set{ _changetime=value;}
			get{return _changetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Reasons
		{
			set{ _reasons=value;}
			get{return _reasons;}
		}
		#endregion Model

	}
}

