﻿using System;
using System.Data;
using System.Collections.Generic;

using EuSoft.Model;
namespace EuSoft.BLL
{
    /// <summary>
    /// MCEOrderInfo
    /// </summary>
    public partial class MCEOrderInfo
    {
        private readonly EuSoft.DAL.MCEOrderInfo dal = new EuSoft.DAL.MCEOrderInfo();
        public MCEOrderInfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据 存储过程
        /// </summary>
        public int AddProc_CreateDD(EuSoft.Model.MCEOrderInfo model)
        {
            return dal.AddProc_CreateDD(model);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(EuSoft.Model.MCEOrderInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EuSoft.Model.MCEOrderInfo model)
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
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(EuSoft.Common.PageValidate.SafeLongFilter(IDlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EuSoft.Model.MCEOrderInfo GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public EuSoft.Model.MCEOrderInfo GetModelByCache(int ID)
        {

            string CacheKey = "MCEOrderInfoModel-" + ID;
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
                catch { }
            }
            return (EuSoft.Model.MCEOrderInfo)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得唯一数据列表
        /// </summary>
        public DataSet GetDistList(string strWhere)
        {
            return dal.GetDistList(strWhere);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<EuSoft.Model.MCEOrderInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<EuSoft.Model.MCEOrderInfo> DataTableToList(DataTable dt)
        {
            List<EuSoft.Model.MCEOrderInfo> modelList = new List<EuSoft.Model.MCEOrderInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                EuSoft.Model.MCEOrderInfo model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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

