using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCECountryDefinition:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCECountryDefinition
	{
		public MCECountryDefinition()
		{}
		#region Model
		private int _id;
		private string _countryname;
		private string _countrycode;
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
		public string CountryName
		{
			set{ _countryname=value;}
			get{return _countryname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CountryCode
		{
			set{ _countrycode=value;}
			get{return _countrycode;}
		}
		#endregion Model

	}
}

