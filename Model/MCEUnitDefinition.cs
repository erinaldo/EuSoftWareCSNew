using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEUnitDefinition:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEUnitDefinition
	{
		public MCEUnitDefinition()
		{}
		#region Model
		private int _id;
		private string _unit;
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
		public string Unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		#endregion Model

	}
}

