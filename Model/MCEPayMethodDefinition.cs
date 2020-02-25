using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEPayMethodDefinition:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEPayMethodDefinition
	{
		public MCEPayMethodDefinition()
		{}
		#region Model
		private int _id;
		private string _paymethod;
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
		public string PayMethod
		{
			set{ _paymethod=value;}
			get{return _paymethod;}
		}
		#endregion Model

	}
}

