using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEValcode:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEValcode
	{
		public MCEValcode()
		{}
		#region Model
		private int _id;
		private string _catalogno;
		private string _batchno;
		private string _valcode;
		private string _size;
		private string _unit;
		private string _note;
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
		public string CatalogNo
		{
			set{ _catalogno=value;}
			get{return _catalogno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BatchNo
		{
			set{ _batchno=value;}
			get{return _batchno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ValCode
		{
			set{ _valcode=value;}
			get{return _valcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Size
		{
			set{ _size=value;}
			get{return _size;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Unit
		{
			set{ _unit=value;}
			get{return _unit;}
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

