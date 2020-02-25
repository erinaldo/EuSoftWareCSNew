using System;
namespace EuSoft.Model
{
	/// <summary>
	/// SysZyBzj:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysZyBzj
	{
		public SysZyBzj()
		{}
		#region Model
		private int _id;
		private int? _xtid;
		private int? _zypid;
		private string _systemcdname="";
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
		public int? xtId
		{
			set{ _xtid=value;}
			get{return _xtid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? zyPid
		{
			set{ _zypid=value;}
			get{return _zypid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SystemCdName
		{
			set{ _systemcdname=value;}
			get{return _systemcdname;}
		}
		#endregion Model

	}
}

