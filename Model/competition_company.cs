using System;
namespace EuSoft.Model
{
	/// <summary>
	/// competition_company:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class competition_company
	{
		public competition_company()
		{}
		#region Model
		private string _id;
		private string _company_name;
		private string _company_url;
		private string _notes;
		private DateTime? _add_date;
		/// <summary>
		/// 
		/// </summary>
		public string id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string company_name
		{
			set{ _company_name=value;}
			get{return _company_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string company_url
		{
			set{ _company_url=value;}
			get{return _company_url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string notes
		{
			set{ _notes=value;}
			get{return _notes;}
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

