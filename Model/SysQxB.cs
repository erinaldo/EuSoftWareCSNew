using System;
namespace EuSoft.Model
{
	/// <summary>
	/// SysQxB:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysQxB
	{
		public SysQxB()
		{}
		#region Model
		private int _id;
		private string _userid="";
		private int? _xtid;
		private int? _zyid;
		private bool _isqx= false;
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
		public string userid
		{
			set{ _userid=value;}
			get{return _userid;}
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
		public int? ZyId
		{
			set{ _zyid=value;}
			get{return _zyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsQx
		{
			set{ _isqx=value;}
			get{return _isqx;}
		}
		#endregion Model

	}
}

