using System;
namespace EuSoft.Model
{
	/// <summary>
	/// PDFUploadRecords:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PDFUploadRecords
	{
		public PDFUploadRecords()
		{}
		#region Model
		private int _id;
		private string _companytype;
		private string _type;
		private string _filename;
		private DateTime? _uploadtime;
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
		public string CompanyType
		{
			set{ _companytype=value;}
			get{return _companytype;}
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
		public string FileName
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UploadTime
		{
			set{ _uploadtime=value;}
			get{return _uploadtime;}
		}
		#endregion Model

	}
}

