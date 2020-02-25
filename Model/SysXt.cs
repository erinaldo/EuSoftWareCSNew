using System;
namespace EuSoft.Model
{
	/// <summary>
	/// SysXt:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysXt
	{
		public SysXt()
		{}
		#region Model
		private int _id;
		private string _newver;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NEWVer
		{
			set{ _newver=value;}
			get{return _newver;}
		}
		#endregion Model

	}
}

