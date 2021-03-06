﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
    /// <summary>
    /// 数据访问类:MCEShipProInfo
    /// </summary>
    public partial class MCEShipProInfo
    {
        public MCEShipProInfo()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID, DateTime _MASK_FROM_V2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MCEShipProInfo");
            strSql.Append(" where ID=@ID and _MASK_FROM_V2=@_MASK_FROM_V2 ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@_MASK_FROM_V2", SqlDbType.Timestamp,8)			};
            parameters[0].Value = ID;
            parameters[1].Value = _MASK_FROM_V2;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(EuSoft.Model.MCEShipProInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MCEShipProInfo(");
            strSql.Append("OrderNo,TrackingNo,ShipCatalogNo,ShipCSNo,ShipSize,ShipUnit,ShipValCode,ShipBatchNo,ShipNote,ShipLibraryID,OrginalID,Flag,UpdateTime,Person,StockStatus,rkrq)");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@TrackingNo,@ShipCatalogNo,@ShipCSNo,@ShipSize,@ShipUnit,@ShipValCode,@ShipBatchNo,@ShipNote,@ShipLibraryID,@OrginalID,@Flag,@UpdateTime,@Person,@StockStatus,@rkrq)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,80),
					new SqlParameter("@TrackingNo", SqlDbType.VarChar,80),
					new SqlParameter("@ShipCatalogNo", SqlDbType.VarChar,80),
					new SqlParameter("@ShipCSNo", SqlDbType.VarChar,80),
					new SqlParameter("@ShipSize", SqlDbType.Float,8),
					new SqlParameter("@ShipUnit", SqlDbType.VarChar,80),
					new SqlParameter("@ShipValCode", SqlDbType.VarChar,80),
					new SqlParameter("@ShipBatchNo", SqlDbType.VarChar,80),
					new SqlParameter("@ShipNote", SqlDbType.VarChar,400),
					new SqlParameter("@ShipLibraryID", SqlDbType.VarChar,80),
					new SqlParameter("@OrginalID", SqlDbType.VarChar,80),
					new SqlParameter("@Flag", SqlDbType.VarChar,80),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,40),
					new SqlParameter("@StockStatus", SqlDbType.VarChar,50),
                     new SqlParameter("@rkrq", SqlDbType.DateTime)                   
                                        
                                        };
            parameters[0].Value = model.OrderNo;
            parameters[1].Value = model.TrackingNo;
            parameters[2].Value = model.ShipCatalogNo;
            parameters[3].Value = model.ShipCSNo;
            parameters[4].Value = model.ShipSize;
            parameters[5].Value = model.ShipUnit;
            parameters[6].Value = model.ShipValCode;
            parameters[7].Value = model.ShipBatchNo;
            parameters[8].Value = model.ShipNote;
            parameters[9].Value = model.ShipLibraryID;
            parameters[10].Value = model.OrginalID;
            parameters[11].Value = model.Flag;
            parameters[12].Value = model.UpdateTime;
            parameters[13].Value = model.Person;
            parameters[14].Value = model.StockStatus;
            parameters[15].Value = model.Rkrq;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EuSoft.Model.MCEShipProInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MCEShipProInfo set ");
            strSql.Append("OrderNo=@OrderNo,");
            strSql.Append("TrackingNo=@TrackingNo,");
            strSql.Append("ShipCatalogNo=@ShipCatalogNo,");
            strSql.Append("ShipCSNo=@ShipCSNo,");
            strSql.Append("ShipSize=@ShipSize,");
            strSql.Append("ShipUnit=@ShipUnit,");
            strSql.Append("ShipValCode=@ShipValCode,");
            strSql.Append("ShipBatchNo=@ShipBatchNo,");
            strSql.Append("ShipNote=@ShipNote,");
            strSql.Append("ShipLibraryID=@ShipLibraryID,");
            strSql.Append("OrginalID=@OrginalID,");
            strSql.Append("Flag=@Flag,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("Person=@Person,");
            strSql.Append("StockStatus=@StockStatus");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,80),
					new SqlParameter("@TrackingNo", SqlDbType.VarChar,80),
					new SqlParameter("@ShipCatalogNo", SqlDbType.VarChar,80),
					new SqlParameter("@ShipCSNo", SqlDbType.VarChar,80),
					new SqlParameter("@ShipSize", SqlDbType.Float,8),
					new SqlParameter("@ShipUnit", SqlDbType.VarChar,80),
					new SqlParameter("@ShipValCode", SqlDbType.VarChar,80),
					new SqlParameter("@ShipBatchNo", SqlDbType.VarChar,80),
					new SqlParameter("@ShipNote", SqlDbType.VarChar,400),
					new SqlParameter("@ShipLibraryID", SqlDbType.VarChar,80),
					new SqlParameter("@OrginalID", SqlDbType.VarChar,80),
					new SqlParameter("@Flag", SqlDbType.VarChar,80),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,40),
					new SqlParameter("@StockStatus", SqlDbType.VarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.OrderNo;
            parameters[1].Value = model.TrackingNo;
            parameters[2].Value = model.ShipCatalogNo;
            parameters[3].Value = model.ShipCSNo;
            parameters[4].Value = model.ShipSize;
            parameters[5].Value = model.ShipUnit;
            parameters[6].Value = model.ShipValCode;
            parameters[7].Value = model.ShipBatchNo;
            parameters[8].Value = model.ShipNote;
            parameters[9].Value = model.ShipLibraryID;
            parameters[10].Value = model.OrginalID;
            parameters[11].Value = model.Flag;
            parameters[12].Value = model.UpdateTime;
            parameters[13].Value = model.Person;
            parameters[14].Value = model.StockStatus;
            parameters[15].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MCEShipProInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID, DateTime _MASK_FROM_V2)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MCEShipProInfo ");
            strSql.Append(" where ID=@ID and _MASK_FROM_V2=@_MASK_FROM_V2 ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@_MASK_FROM_V2", SqlDbType.Timestamp,8)			};
            parameters[0].Value = ID;
            parameters[1].Value = _MASK_FROM_V2;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MCEShipProInfo ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EuSoft.Model.MCEShipProInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,OrderNo,TrackingNo,ShipCatalogNo,ShipCSNo,ShipSize,ShipUnit,ShipValCode,ShipBatchNo,ShipNote,ShipLibraryID,OrginalID,Flag,UpdateTime,Person,StockStatus,rkrq from MCEShipProInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            EuSoft.Model.MCEShipProInfo model = new EuSoft.Model.MCEShipProInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EuSoft.Model.MCEShipProInfo DataRowToModel(DataRow row)
        {
            EuSoft.Model.MCEShipProInfo model = new EuSoft.Model.MCEShipProInfo();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["OrderNo"] != null)
                {
                    model.OrderNo = row["OrderNo"].ToString();
                }
                if (row["TrackingNo"] != null)
                {
                    model.TrackingNo = row["TrackingNo"].ToString();
                }
                if (row["ShipCatalogNo"] != null)
                {
                    model.ShipCatalogNo = row["ShipCatalogNo"].ToString();
                }
                if (row["ShipCSNo"] != null)
                {
                    model.ShipCSNo = row["ShipCSNo"].ToString();
                }
                if (row["ShipSize"] != null && row["ShipSize"].ToString() != "")
                {
                    model.ShipSize = decimal.Parse(row["ShipSize"].ToString());
                }
                if (row["ShipUnit"] != null)
                {
                    model.ShipUnit = row["ShipUnit"].ToString();
                }
                if (row["ShipValCode"] != null)
                {
                    model.ShipValCode = row["ShipValCode"].ToString();
                }
                if (row["ShipBatchNo"] != null)
                {
                    model.ShipBatchNo = row["ShipBatchNo"].ToString();
                }
                if (row["ShipNote"] != null)
                {
                    model.ShipNote = row["ShipNote"].ToString();
                }
                if (row["ShipLibraryID"] != null)
                {
                    model.ShipLibraryID = row["ShipLibraryID"].ToString();
                }
                if (row["OrginalID"] != null)
                {
                    model.OrginalID = row["OrginalID"].ToString();
                }
                if (row["Flag"] != null)
                {
                    model.Flag = row["Flag"].ToString();
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["Person"] != null)
                {
                    model.Person = row["Person"].ToString();
                }
                if (row["StockStatus"] != null)
                {
                    model.StockStatus = row["StockStatus"].ToString();
                }
                if (row["rkrq"] != null && row["rkrq"].ToString() != "")
                {
                    model.Rkrq = DateTime.Parse(row["rkrq"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,OrderNo,TrackingNo,ShipCatalogNo,ShipCSNo,ShipSize,ShipUnit,ShipValCode,ShipBatchNo,ShipNote,ShipLibraryID,OrginalID,Flag,UpdateTime,Person,StockStatus ");
            strSql.Append(" FROM MCEShipProInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,OrderNo,TrackingNo,ShipCatalogNo,ShipCSNo,ShipSize,ShipUnit,ShipValCode,ShipBatchNo,ShipNote,ShipLibraryID,OrginalID,Flag,UpdateTime,Person,StockStatus ");
            strSql.Append(" FROM MCEShipProInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM MCEShipProInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from MCEShipProInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "MCEShipProInfo";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

