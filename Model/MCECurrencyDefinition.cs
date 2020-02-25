using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCECurrencyDefinition:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCECurrencyDefinition
	{
		public MCECurrencyDefinition()
		{}
		#region Model
		private int _id;
		private string _currency;
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
		public string Currency
		{
			set{ _currency=value;}
			get{return _currency;}
		}
		#endregion Model

	}
}

