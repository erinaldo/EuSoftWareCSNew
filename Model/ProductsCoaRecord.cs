using System;
namespace EuSoft.Model
{
	/// <summary>
	/// ProductsCoaRecord:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductsCoaRecord
	{
		public ProductsCoaRecord()
		{}
		#region Model
		private int? _id;
		private string _catalogno;
		private int? _trackingno;
		private DateTime? _testdate;
		private DateTime? _retesdate;
		private string _storage;
		private string _mergepdfname;
		private DateTime? _uploadtime;
		private string _remarks;
		/// <summary>
		/// 
		/// </summary>
		public int? ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CatalogNO
		{
			set{ _catalogno=value;}
			get{return _catalogno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TrackingNO
		{
			set{ _trackingno=value;}
			get{return _trackingno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TestDate
		{
			set{ _testdate=value;}
			get{return _testdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RetesDate
		{
			set{ _retesdate=value;}
			get{return _retesdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Storage
		{
			set{ _storage=value;}
			get{return _storage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MergePDFName
		{
			set{ _mergepdfname=value;}
			get{return _mergepdfname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UploadTime
		{
			set{ _uploadtime=value;}
			get{return _uploadtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		#endregion Model

	}
}

