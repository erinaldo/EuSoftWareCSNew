using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEEmailSendOr:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEEmailSendOr
	{
		public MCEEmailSendOr()
		{}
		#region Model
		private int _id;
		private string _emailaddress;
		private int? _send_or;
		private string _email_type;
		private string _source;
		private DateTime? _add_date;
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
		public string emailAddress
		{
			set{ _emailaddress=value;}
			get{return _emailaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? send_or
		{
			set{ _send_or=value;}
			get{return _send_or;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string email_type
		{
			set{ _email_type=value;}
			get{return _email_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string source
		{
			set{ _source=value;}
			get{return _source;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? add_date
		{
			set{ _add_date=value;}
			get{return _add_date;}
		}
		#endregion Model

	}
}

