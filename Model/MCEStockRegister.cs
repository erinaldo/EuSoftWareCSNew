using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEStockRegister:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEStockRegister
	{
		public MCEStockRegister()
		{}
		#region Model
		private int _id;
		private string _stockcatalogno;
		private string _stockcsno;
		private string _stocksize;
		private string _stockunit;
		private string _stockvalcode;
		private string _stockbatchno;
		private string _stocknote;
		private string _stocklibraryid;
		private string _sysnote;
		private string _reason;
		private string _stocklocation;
		private DateTime? _updatetime;
		private string _person;
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
		public string StockCatalogNo
		{
			set{ _stockcatalogno=value;}
			get{return _stockcatalogno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StockCSNo
		{
			set{ _stockcsno=value;}
			get{return _stockcsno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StockSize
		{
			set{ _stocksize=value;}
			get{return _stocksize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StockUnit
		{
			set{ _stockunit=value;}
			get{return _stockunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StockValCode
		{
			set{ _stockvalcode=value;}
			get{return _stockvalcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StockBatchNo
		{
			set{ _stockbatchno=value;}
			get{return _stockbatchno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StockNote
		{
			set{ _stocknote=value;}
			get{return _stocknote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StockLibraryID
		{
			set{ _stocklibraryid=value;}
			get{return _stocklibraryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SysNote
		{
			set{ _sysnote=value;}
			get{return _sysnote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Reason
		{
			set{ _reason=value;}
			get{return _reason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StockLocation
		{
			set{ _stocklocation=value;}
			get{return _stocklocation;}
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
		#endregion Model

	}
}

