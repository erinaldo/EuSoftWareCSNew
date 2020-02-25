using System;
namespace EuSoft.Model
{
	/// <summary>
	/// ProFedexInfor:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProFedexInfor
	{
		public ProFedexInfor()
		{}
		#region Model
		private int _id;
		private string _procatalono="";
		private string _pdescription="";
		private string _hcode="";
		private string _country="";
		private string _unit="";
		private string _customs="";
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
		public string ProCataloNo
		{
			set{ _procatalono=value;}
			get{return _procatalono;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PDescription
		{
			set{ _pdescription=value;}
			get{return _pdescription;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Hcode
		{
			set{ _hcode=value;}
			get{return _hcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Country
		{
			set{ _country=value;}
			get{return _country;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Customs
		{
			set{ _customs=value;}
			get{return _customs;}
		}
		#endregion Model

	}
}

