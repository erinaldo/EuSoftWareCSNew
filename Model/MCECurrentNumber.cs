using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCECurrentNumber:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCECurrentNumber
	{
		public MCECurrentNumber()
		{}
		#region Model
		private int _id;
		private string _item;
		private string _number;
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
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
		}
		#endregion Model

	}
}

