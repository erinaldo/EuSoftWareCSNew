using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEPaymentInfoChangeRe:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEPaymentInfoChangeRe
	{
		public MCEPaymentInfoChangeRe()
		{}
		#region Model
		private int _id;
		private string _invioceno;
		private DateTime? _receiveddate;
		private decimal? _receivedamount;
		private decimal? _bankingcost;
		private string _balance;
		private string _currency;
		private string _note;
		private DateTime? _updatetime;
		private string _person;
		private string _changenote;
		private string _changeperson;
		private DateTime? _changetime;
		private string _reasons;
		private string _cardinfo;
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
		public string InvioceNo
		{
			set{ _invioceno=value;}
			get{return _invioceno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReceivedDate
		{
			set{ _receiveddate=value;}
			get{return _receiveddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ReceivedAmount
		{
			set{ _receivedamount=value;}
			get{return _receivedamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BankingCost
		{
			set{ _bankingcost=value;}
			get{return _bankingcost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Balance
		{
			set{ _balance=value;}
			get{return _balance;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Currency
		{
			set{ _currency=value;}
			get{return _currency;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Note
		{
			set{ _note=value;}
			get{return _note;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Person
		{
			set{ _person=value;}
			get{return _person;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChangeNote
		{
			set{ _changenote=value;}
			get{return _changenote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChangePerson
		{
			set{ _changeperson=value;}
			get{return _changeperson;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ChangeTime
		{
			set{ _changetime=value;}
			get{return _changetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Reasons
		{
			set{ _reasons=value;}
			get{return _reasons;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Cardinfo
		{
			set{ _cardinfo=value;}
			get{return _cardinfo;}
		}
		#endregion Model

	}
}

