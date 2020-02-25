using System;
namespace EuSoft.Model
{
	/// <summary>
	/// SysUser:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysUser
	{
		public SysUser()
		{}
		#region Model
		private int _id;
		private int? _xtid;
		private string _userid="";
		private string _username="";
		private string _userpwd="";
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
		public string userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userpwd
		{
			set{ _userpwd=value;}
			get{return _userpwd;}
		}
		#endregion Model

	}
}

