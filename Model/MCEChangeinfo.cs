using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEChangeinfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEChangeinfo
	{
		public MCEChangeinfo()
		{}
		#region Model
		private int _id;
		private string _item;
		private string _changeitem;
		private string _changenote;
		private string _changeperson;
		private DateTime? _changetime;
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
		public string Item
		{
			set{ _item=value;}
			get{return _item;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChangeItem
		{
			set{ _changeitem=value;}
			get{return _changeitem;}
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
		#endregion Model

	}
}

