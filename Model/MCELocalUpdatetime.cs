using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCELocalUpdatetime:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCELocalUpdatetime
	{
		public MCELocalUpdatetime()
		{}
		#region Model
		private int _id;
		private string _item;
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
		public string Item
		{
			set{ _item=value;}
			get{return _item;}
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

