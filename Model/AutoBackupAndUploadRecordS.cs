using System;
namespace EuSoft.Model
{
	/// <summary>
	/// AutoBackupAndUploadRecordS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AutoBackupAndUploadRecordS
	{
		public AutoBackupAndUploadRecordS()
		{}
		#region Model
		private int _id;
		private string _filepath;
		private DateTime? _createtime;
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
		public string FilePath
		{
			set{ _filepath=value;}
			get{return _filepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
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

