using System;
namespace EuSoft.Model
{
	/// <summary>
	/// SysZyb:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysZyb
	{
		public SysZyb()
		{}
		#region Model
		private int _id;
		private int? _pid;
		private string _xsname;
		private string _anname;
		private string _frmname;
		private int? _imageindex;
		private int? _handno;
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
		public int? PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XsName
		{
			set{ _xsname=value;}
			get{return _xsname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AnName
		{
			set{ _anname=value;}
			get{return _anname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FrmName
		{
			set{ _frmname=value;}
			get{return _frmname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ImageIndex
		{
			set{ _imageindex=value;}
			get{return _imageindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? HandNo
		{
			set{ _handno=value;}
			get{return _handno;}
		}
		#endregion Model

	}
}

