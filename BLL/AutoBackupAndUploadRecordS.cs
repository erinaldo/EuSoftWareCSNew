﻿using System;
using System.Data;
using System.Collections.Generic;

using EuSoft.Model;
namespace EuSoft.BLL
{
	/// <summary>
	/// AutoBackupAndUploadRecordS
	/// </summary>
	public partial class AutoBackupAndUploadRecordS
	{
		private readonly EuSoft.DAL.AutoBackupAndUploadRecordS dal=new EuSoft.DAL.AutoBackupAndUploadRecordS();
		public AutoBackupAndUploadRecordS()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EuSoft.Model.AutoBackupAndUploadRecordS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EuSoft.Model.AutoBackupAndUploadRecordS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.Delete();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EuSoft.Model.AutoBackupAndUploadRecordS GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EuSoft.Model.AutoBackupAndUploadRecordS GetModelByCache()
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "AutoBackupAndUploadRecordSModel-" ;
			object objModel = EuSoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel();
					if (objModel != null)
					{
						int ModelCache = EuSoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						EuSoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EuSoft.Model.AutoBackupAndUploadRecordS)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EuSoft.Model.AutoBackupAndUploadRecordS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EuSoft.Model.AutoBackupAndUploadRecordS> DataTableToList(DataTable dt)
		{
			List<EuSoft.Model.AutoBackupAndUploadRecordS> modelList = new List<EuSoft.Model.AutoBackupAndUploadRecordS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EuSoft.Model.AutoBackupAndUploadRecordS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

