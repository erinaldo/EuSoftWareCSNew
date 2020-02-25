using System;
namespace EuSoft.Model
{
	/// <summary>
	/// MCEProductsMols:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MCEProductsMols
	{
		public MCEProductsMols()
		{}
		#region Model
		private int _id;
		private string _catalogno;
		private string _mol;
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
		public string CatalogNo
		{
			set{ _catalogno=value;}
			get{return _catalogno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mol
		{
			set{ _mol=value;}
			get{return _mol;}
		}
		#endregion Model

	}
}

