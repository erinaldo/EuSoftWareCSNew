using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
    /// <summary>
    /// 数据访问类:MCEShipmentInfo
    /// </summary>
    public partial class MCEShipmentInfo
    {
        public MCEShipmentInfo()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID, DateTime _MASK_FROM_V2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MCEShipmentInfo");
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
        public int Add(EuSoft.Model.MCEShipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MCEShipmentInfo(");
            strSql.Append("OrderNo,ShipDate,ShipVia,TrackingNo,Note,Cost,Currency,ShipmentStatus,UpdateTime,Person,SendOrderShipDate,SendOrderReceiptDate,SendOrderCOADate,StockStatus)");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@ShipDate,@ShipVia,@TrackingNo,@Note,@Cost,@Currency,@ShipmentStatus,@UpdateTime,@Person,@SendOrderShipDate,@SendOrderReceiptDate,@SendOrderCOADate,@StockStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,80),
					new SqlParameter("@ShipDate", SqlDbType.DateTime),
					new SqlParameter("@ShipVia", SqlDbType.VarChar,80),
					new SqlParameter("@TrackingNo", SqlDbType.VarChar,80),
					new SqlParameter("@Note", SqlDbType.VarChar,400),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
					new SqlParameter("@Currency", SqlDbType.VarChar,80),
					new SqlParameter("@ShipmentStatus", SqlDbType.VarChar,80),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,40),
					new SqlParameter("@SendOrderShipDate", SqlDbType.DateTime),
					new SqlParameter("@SendOrderReceiptDate", SqlDbType.DateTime),
					new SqlParameter("@SendOrderCOADate", SqlDbType.DateTime),
					new SqlParameter("@StockStatus", SqlDbType.VarChar,50)};
            parameters[0].Value = model.OrderNo;
            parameters[1].Value = model.ShipDate;
            parameters[2].Value = model.ShipVia;
            parameters[3].Value = model.TrackingNo;
            parameters[4].Value = model.Note;
            parameters[5].Value = model.Cost;
            parameters[6].Value = model.Currency;
            parameters[7].Value = model.ShipmentStatus;
            parameters[8].Value = model.UpdateTime;
            parameters[9].Value = model.Person;
            parameters[10].Value = model.SendOrderShipDate;
            parameters[11].Value = model.SendOrderReceiptDate;
            parameters[12].Value = model.SendOrderCOADate;
            parameters[13].Value = model.StockStatus;

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
        public bool Update(EuSoft.Model.MCEShipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MCEShipmentInfo set ");
            strSql.Append("OrderNo=@OrderNo,");
            strSql.Append("ShipDate=@ShipDate,");
            strSql.Append("ShipVia=@ShipVia,");
            strSql.Append("TrackingNo=@TrackingNo,");
            strSql.Append("Note=@Note,");
            strSql.Append("Cost=@Cost,");
            strSql.Append("Currency=@Currency,");
            strSql.Append("ShipmentStatus=@ShipmentStatus,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("Person=@Person,");
            strSql.Append("SendOrderShipDate=@SendOrderShipDate,");
            strSql.Append("SendOrderReceiptDate=@SendOrderReceiptDate,");
            strSql.Append("SendOrderCOADate=@SendOrderCOADate,");
            strSql.Append("StockStatus=@StockStatus");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,80),
					new SqlParameter("@ShipDate", SqlDbType.DateTime),
					new SqlParameter("@ShipVia", SqlDbType.VarChar,80),
					new SqlParameter("@TrackingNo", SqlDbType.VarChar,80),
					new SqlParameter("@Note", SqlDbType.VarChar,400),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
					new SqlParameter("@Currency", SqlDbType.VarChar,80),
					new SqlParameter("@ShipmentStatus", SqlDbType.VarChar,80),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,40),
					new SqlParameter("@SendOrderShipDate", SqlDbType.DateTime),
					new SqlParameter("@SendOrderReceiptDate", SqlDbType.DateTime),
					new SqlParameter("@SendOrderCOADate", SqlDbType.DateTime),
					new SqlParameter("@StockStatus", SqlDbType.VarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.OrderNo;
            parameters[1].Value = model.ShipDate;
            parameters[2].Value = model.ShipVia;
            parameters[3].Value = model.TrackingNo;
            parameters[4].Value = model.Note;
            parameters[5].Value = model.Cost;
            parameters[6].Value = model.Currency;
            parameters[7].Value = model.ShipmentStatus;
            parameters[8].Value = model.UpdateTime;
            parameters[9].Value = model.Person;
            parameters[10].Value = model.SendOrderShipDate;
            parameters[11].Value = model.SendOrderReceiptDate;
            parameters[12].Value = model.SendOrderCOADate;
            parameters[13].Value = model.StockStatus;
            parameters[14].Value = model.ID;

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
            strSql.Append("delete from MCEShipmentInfo ");
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
            strSql.Append("delete from MCEShipmentInfo ");
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
            strSql.Append("delete from MCEShipmentInfo ");
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
        public EuSoft.Model.MCEShipmentInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,OrderNo,ShipDate,ShipVia,TrackingNo,Note,Cost,Currency,ShipmentStatus,UpdateTime,Person,SendOrderShipDate,SendOrderReceiptDate,SendOrderCOADate,StockStatus from MCEShipmentInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            EuSoft.Model.MCEShipmentInfo model = new EuSoft.Model.MCEShipmentInfo();
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
        public EuSoft.Model.MCEShipmentInfo DataRowToModel(DataRow row)
        {
            EuSoft.Model.MCEShipmentInfo model = new EuSoft.Model.MCEShipmentInfo();
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
                if (row["ShipDate"] != null && row["ShipDate"].ToString() != "")
                {
                    model.ShipDate = DateTime.Parse(row["ShipDate"].ToString());
                }
                if (row["ShipVia"] != null)
                {
                    model.ShipVia = row["ShipVia"].ToString();
                }
                if (row["TrackingNo"] != null)
                {
                    model.TrackingNo = row["TrackingNo"].ToString();
                }
                if (row["Note"] != null)
                {
                    model.Note = row["Note"].ToString();
                }
                if (row["Cost"] != null && row["Cost"].ToString() != "")
                {
                    model.Cost = decimal.Parse(row["Cost"].ToString());
                }
                if (row["Currency"] != null)
                {
                    model.Currency = row["Currency"].ToString();
                }
                if (row["ShipmentStatus"] != null)
                {
                    model.ShipmentStatus = row["ShipmentStatus"].ToString();
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["Person"] != null)
                {
                    model.Person = row["Person"].ToString();
                }
                if (row["SendOrderShipDate"] != null && row["SendOrderShipDate"].ToString() != "")
                {
                    model.SendOrderShipDate = DateTime.Parse(row["SendOrderShipDate"].ToString());
                }
                if (row["SendOrderReceiptDate"] != null && row["SendOrderReceiptDate"].ToString() != "")
                {
                    model.SendOrderReceiptDate = DateTime.Parse(row["SendOrderReceiptDate"].ToString());
                }
                if (row["SendOrderCOADate"] != null && row["SendOrderCOADate"].ToString() != "")
                {
                    model.SendOrderCOADate = DateTime.Parse(row["SendOrderCOADate"].ToString());
                }
                if (row["StockStatus"] != null)
                {
                    model.StockStatus = row["StockStatus"].ToString();
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
            strSql.Append("select ID,OrderNo,ShipDate,ShipVia,TrackingNo,Note,Cost,Currency,ShipmentStatus,UpdateTime,Person,SendOrderShipDate,SendOrderReceiptDate,SendOrderCOADate,StockStatus ");
            strSql.Append(" FROM MCEShipmentInfo ");
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
            strSql.Append(" ID,OrderNo,ShipDate,ShipVia,TrackingNo,Note,Cost,Currency,ShipmentStatus,UpdateTime,Person,SendOrderShipDate,SendOrderReceiptDate,SendOrderCOADate,StockStatus ");
            strSql.Append(" FROM MCEShipmentInfo ");
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
            strSql.Append("select count(1) FROM MCEShipmentInfo ");
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
            strSql.Append(")AS Row, T.*  from MCEShipmentInfo T ");
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
            parameters[0].Value = "MCEShipmentInfo";
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

