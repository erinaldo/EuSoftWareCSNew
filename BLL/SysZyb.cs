using System;
using System.Data;
using System.Collections.Generic;

using EuSoft.Model;
using System.Text;
using System.Data.SqlClient;
namespace EuSoft.BLL
{
	/// <summary>
	/// SysZyb
	/// </summary>
	public partial class SysZyb
	{
		private readonly EuSoft.DAL.SysZyb dal=new EuSoft.DAL.SysZyb();
		public SysZyb()
		{}
		#region  BasicMethod


      
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(EuSoft.Model.SysZyb model)
		{
			return dal.Add(model);
		}

        public DataTable GetXTZyById(string id)
        {
            return dal.GetXTZyById(id);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EuSoft.Model.SysZyb model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(EuSoft.Common.PageValidate.SafeLongFilter(IDlist,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EuSoft.Model.SysZyb GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EuSoft.Model.SysZyb GetModelByCache(int ID)
		{
			
			string CacheKey = "SysZybModel-" + ID;
			object objModel = EuSoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = EuSoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						EuSoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EuSoft.Model.SysZyb)objModel;
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
		public List<EuSoft.Model.SysZyb> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EuSoft.Model.SysZyb> DataTableToList(DataTable dt)
		{
			List<EuSoft.Model.SysZyb> modelList = new List<EuSoft.Model.SysZyb>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EuSoft.Model.SysZyb model;
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
            return GetList(" xtid is null ");
		}

        /// <summary>
        /// 获得权限列表
        /// </summary>
        public DataTable  GetQXList(string strWhere)
        {
            return dal.GetQxList(strWhere).Tables[0];
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

