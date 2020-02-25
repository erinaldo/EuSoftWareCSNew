using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEShipProInfoChangeRe:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEShipProInfoChangeRe
	{
		public MCEShipProInfoChangeRe()
		{}
		#region Model
		private int _id;
		private string _trackingno;
		private string _shipcatalogno;
		private string _shipcsno;
		private decimal? _shipsize;
		private string _shipunit;
		private string _shipvalcode;
		private string _shipbatchno;
		private string _shipnote;
		private string _shiplibraryid;
		private string _orginalid;
		private string _flag;
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
		public string TrackingNo
		{
			set{ _trackingno=value;}
			get{return _trackingno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipCatalogNo
		{
			set{ _shipcatalogno=value;}
			get{return _shipcatalogno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipCSNo
		{
			set{ _shipcsno=value;}
			get{return _shipcsno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ShipSize
		{
			set{ _shipsize=value;}
			get{return _shipsize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipUnit
		{
			set{ _shipunit=value;}
			get{return _shipunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipValCode
		{
			set{ _shipvalcode=value;}
			get{return _shipvalcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipBatchNo
		{
			set{ _shipbatchno=value;}
			get{return _shipbatchno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipNote
		{
			set{ _shipnote=value;}
			get{return _shipnote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShipLibraryID
		{
			set{ _shiplibraryid=value;}
			get{return _shiplibraryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrginalID
		{
			set{ _orginalid=value;}
			get{return _orginalid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
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

