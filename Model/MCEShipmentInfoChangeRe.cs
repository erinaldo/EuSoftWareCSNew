using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEShipmentInfoChangeRe:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEShipmentInfoChangeRe
	{
		public MCEShipmentInfoChangeRe()
		{}
		#region Model
		private int _id;
		private string _orderno;
		private DateTime? _shipdate;
		private string _shipvia;
		private string _trackingno;
		private string _note;
		private decimal? _cost;
		private string _currency;
		private string _shipmentstatus;
		private DateTime? _updatetime;
		private string _person;
		private string _changenote;
		private string _changeperson;
		private DateTime? _changetime;
		private string _reasons;
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
		public string OrderNo
		{
			set{ _orderno=value;}
			get{return _orderno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ShipDate
		{
			set{ _shipdate=value;}
			get{return _shipdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipVia
		{
			set{ _shipvia=value;}
			get{return _shipvia;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TrackingNo
		{
			set{ _trackingno=value;}
			get{return _trackingno;}
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
		public decimal? Cost
		{
			set{ _cost=value;}
			get{return _cost;}
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
		public string ShipmentStatus
		{
			set{ _shipmentstatus=value;}
			get{return _shipmentstatus;}
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
		#endregion Model

	}
}

