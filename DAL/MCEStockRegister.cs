﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:MCEStockRegister
	/// </summary>
	public partial class MCEStockRegister
	{
		public MCEStockRegister()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "MCEStockRegister"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MCEStockRegister");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(EuSoft.Model.MCEStockRegister model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MCEStockRegister(");
			strSql.Append("StockCatalogNo,StockCSNo,StockSize,StockUnit,StockValCode,StockBatchNo,StockNote,StockLibraryID,SysNote,Reason,StockLocation,UpdateTime,Person)");
			strSql.Append(" values (");
			strSql.Append("@StockCatalogNo,@StockCSNo,@StockSize,@StockUnit,@StockValCode,@StockBatchNo,@StockNote,@StockLibraryID,@SysNote,@Reason,@StockLocation,@UpdateTime,@Person)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@StockCatalogNo", SqlDbType.VarChar,50),
					new SqlParameter("@StockCSNo", SqlDbType.VarChar,40),
					new SqlParameter("@StockSize", SqlDbType.VarChar,40),
					new SqlParameter("@StockUnit", SqlDbType.VarChar,40),
					new SqlParameter("@StockValCode", SqlDbType.VarChar,40),
					new SqlParameter("@StockBatchNo", SqlDbType.VarChar,40),
					new SqlParameter("@StockNote", SqlDbType.VarChar,800),
					new SqlParameter("@StockLibraryID", SqlDbType.VarChar,40),
					new SqlParameter("@SysNote", SqlDbType.VarChar,200),
					new SqlParameter("@Reason", SqlDbType.VarChar,200),
					new SqlParameter("@StockLocation", SqlDbType.VarChar,40),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,200)};
			parameters[0].Value = model.StockCatalogNo;
			parameters[1].Value = model.StockCSNo;
			parameters[2].Value = model.StockSize;
			parameters[3].Value = model.StockUnit;
			parameters[4].Value = model.StockValCode;
			parameters[5].Value = model.StockBatchNo;
			parameters[6].Value = model.StockNote;
			parameters[7].Value = model.StockLibraryID;
			parameters[8].Value = model.SysNote;
			parameters[9].Value = model.Reason;
			parameters[10].Value = model.StockLocation;
			parameters[11].Value = model.UpdateTime;
			parameters[12].Value = model.Person;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(EuSoft.Model.MCEStockRegister model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MCEStockRegister set ");
			strSql.Append("StockCatalogNo=@StockCatalogNo,");
			strSql.Append("StockCSNo=@StockCSNo,");
			strSql.Append("StockSize=@StockSize,");
			strSql.Append("StockUnit=@StockUnit,");
			strSql.Append("StockValCode=@StockValCode,");
			strSql.Append("StockBatchNo=@StockBatchNo,");
			strSql.Append("StockNote=@StockNote,");
			strSql.Append("StockLibraryID=@StockLibraryID,");
			strSql.Append("SysNote=@SysNote,");
			strSql.Append("Reason=@Reason,");
			strSql.Append("StockLocation=@StockLocation,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("Person=@Person");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@StockCatalogNo", SqlDbType.VarChar,50),
					new SqlParameter("@StockCSNo", SqlDbType.VarChar,40),
					new SqlParameter("@StockSize", SqlDbType.VarChar,40),
					new SqlParameter("@StockUnit", SqlDbType.VarChar,40),
					new SqlParameter("@StockValCode", SqlDbType.VarChar,40),
					new SqlParameter("@StockBatchNo", SqlDbType.VarChar,40),
					new SqlParameter("@StockNote", SqlDbType.VarChar,800),
					new SqlParameter("@StockLibraryID", SqlDbType.VarChar,40),
					new SqlParameter("@SysNote", SqlDbType.VarChar,200),
					new SqlParameter("@Reason", SqlDbType.VarChar,200),
					new SqlParameter("@StockLocation", SqlDbType.VarChar,40),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.StockCatalogNo;
			parameters[1].Value = model.StockCSNo;
			parameters[2].Value = model.StockSize;
			parameters[3].Value = model.StockUnit;
			parameters[4].Value = model.StockValCode;
			parameters[5].Value = model.StockBatchNo;
			parameters[6].Value = model.StockNote;
			parameters[7].Value = model.StockLibraryID;
			parameters[8].Value = model.SysNote;
			parameters[9].Value = model.Reason;
			parameters[10].Value = model.StockLocation;
			parameters[11].Value = model.UpdateTime;
			parameters[12].Value = model.Person;
			parameters[13].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MCEStockRegister ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MCEStockRegister ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public EuSoft.Model.MCEStockRegister GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,StockCatalogNo,StockCSNo,StockSize,StockUnit,StockValCode,StockBatchNo,StockNote,StockLibraryID,SysNote,Reason,StockLocation,UpdateTime,Person from MCEStockRegister ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			EuSoft.Model.MCEStockRegister model=new EuSoft.Model.MCEStockRegister();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public EuSoft.Model.MCEStockRegister DataRowToModel(DataRow row)
		{
			EuSoft.Model.MCEStockRegister model=new EuSoft.Model.MCEStockRegister();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["StockCatalogNo"]!=null)
				{
					model.StockCatalogNo=row["StockCatalogNo"].ToString();
				}
				if(row["StockCSNo"]!=null)
				{
					model.StockCSNo=row["StockCSNo"].ToString();
				}
				if(row["StockSize"]!=null)
				{
					model.StockSize=row["StockSize"].ToString();
				}
				if(row["StockUnit"]!=null)
				{
					model.StockUnit=row["StockUnit"].ToString();
				}
				if(row["StockValCode"]!=null)
				{
					model.StockValCode=row["StockValCode"].ToString();
				}
				if(row["StockBatchNo"]!=null)
				{
					model.StockBatchNo=row["StockBatchNo"].ToString();
				}
				if(row["StockNote"]!=null)
				{
					model.StockNote=row["StockNote"].ToString();
				}
				if(row["StockLibraryID"]!=null)
				{
					model.StockLibraryID=row["StockLibraryID"].ToString();
				}
				if(row["SysNote"]!=null)
				{
					model.SysNote=row["SysNote"].ToString();
				}
				if(row["Reason"]!=null)
				{
					model.Reason=row["Reason"].ToString();
				}
				if(row["StockLocation"]!=null)
				{
					model.StockLocation=row["StockLocation"].ToString();
				}
				if(row["UpdateTime"]!=null && row["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(row["UpdateTime"].ToString());
				}
				if(row["Person"]!=null)
				{
					model.Person=row["Person"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,StockCatalogNo,StockCSNo,StockSize,StockUnit,StockValCode,StockBatchNo,StockNote,StockLibraryID,SysNote,Reason,StockLocation,UpdateTime,Person ");
			strSql.Append(" FROM MCEStockRegister ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,StockCatalogNo,StockCSNo,StockSize,StockUnit,StockValCode,StockBatchNo,StockNote,StockLibraryID,SysNote,Reason,StockLocation,UpdateTime,Person ");
			strSql.Append(" FROM MCEStockRegister ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM MCEStockRegister ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from MCEStockRegister T ");
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
			parameters[0].Value = "MCEStockRegister";
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

