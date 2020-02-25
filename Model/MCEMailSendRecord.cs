using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEMailSendRecord:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEMailSendRecord
	{
		public MCEMailSendRecord()
		{}
		#region Model
		private int _id;
		private string _orderno;
		private string _emailaddress;
		private string _type;
		private DateTime? _senddate;
		private string _remark;
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
		public string OrderNO
		{
			set{ _orderno=value;}
			get{return _orderno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmailAddress
		{
			set{ _emailaddress=value;}
			get{return _emailaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SendDate
		{
			set{ _senddate=value;}
			get{return _senddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

