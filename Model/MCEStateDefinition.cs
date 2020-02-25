using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEStateDefinition:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEStateDefinition
	{
		public MCEStateDefinition()
		{}
		#region Model
		private int _id;
		private string _statecode;
		private string _state;
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
		public string StateCode
		{
			set{ _statecode=value;}
			get{return _statecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string State
		{
			set{ _state=value;}
			get{return _state;}
		}
		#endregion Model

	}
}

