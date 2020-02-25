using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEProductsBasicinfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEProductsBasicinfo
	{
		public MCEProductsBasicinfo()
		{}
		#region Model
		private int? _id;
		private string _csno;
		private string _catalogno;
		private string _drugnames;
		private string _alternativenames;
		private string _cas;
		private string _formula;
		private string _mwt;
		/// <summary>
		/// 
		/// </summary>
		public int? ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CSNo
		{
			set{ _csno=value;}
			get{return _csno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CatalogNo
		{
			set{ _catalogno=value;}
			get{return _catalogno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DrugNames
		{
			set{ _drugnames=value;}
			get{return _drugnames;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AlternativeNames
		{
			set{ _alternativenames=value;}
			get{return _alternativenames;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CAS
		{
			set{ _cas=value;}
			get{return _cas;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Formula
		{
			set{ _formula=value;}
			get{return _formula;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mwt
		{
			set{ _mwt=value;}
			get{return _mwt;}
		}
		#endregion Model

	}
}

