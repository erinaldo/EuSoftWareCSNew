using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCECarrierDefinition:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCECarrierDefinition
	{
		public MCECarrierDefinition()
		{}
		#region Model
		private int _id;
		private string _carrier;
		private string _web;
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
		public string Carrier
		{
			set{ _carrier=value;}
			get{return _carrier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Web
		{
			set{ _web=value;}
			get{return _web;}
		}
		#endregion Model

	}
}

