using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEScreeningLibraries:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEScreeningLibraries
	{
		public MCEScreeningLibraries()
		{}
		#region Model
		private int? _id;
		private string _orderno;
		private string _libraryid;
		private string _catalogno;
		private string _sizeunit;
		private string _note;
		private DateTime? _updatetime;
		private string _person;
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
		public string OrderNo
		{
			set{ _orderno=value;}
			get{return _orderno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LibraryID
		{
			set{ _libraryid=value;}
			get{return _libraryid;}
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
		public string SizeUnit
		{
			set{ _sizeunit=value;}
			get{return _sizeunit;}
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

