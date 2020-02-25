using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:MCEScreeningLibraries
	/// </summary>
	public partial class MCEScreeningLibraries
	{
		public MCEScreeningLibraries()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EuSoft.Model.MCEScreeningLibraries model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MCEScreeningLibraries(");
			strSql.Append("ID,OrderNo,LibraryID,CatalogNO,SizeUnit,Note,UpdateTime,Person)");
			strSql.Append(" values (");
			strSql.Append("@ID,@OrderNo,@LibraryID,@CatalogNO,@SizeUnit,@Note,@UpdateTime,@Person)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@OrderNo", SqlDbType.VarChar,200),
					new SqlParameter("@LibraryID", SqlDbType.VarChar,50),
					new SqlParameter("@CatalogNO", SqlDbType.VarChar,100),
					new SqlParameter("@SizeUnit", SqlDbType.VarChar,100),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,20)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.OrderNo;
			parameters[2].Value = model.LibraryID;
			parameters[3].Value = model.CatalogNO;
			parameters[4].Value = model.SizeUnit;
			parameters[5].Value = model.Note;
			parameters[6].Value = model.UpdateTime;
			parameters[7].Value = model.Person;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(EuSoft.Model.MCEScreeningLibraries model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MCEScreeningLibraries set ");
			strSql.Append("ID=@ID,");
			strSql.Append("OrderNo=@OrderNo,");
			strSql.Append("LibraryID=@LibraryID,");
			strSql.Append("CatalogNO=@CatalogNO,");
			strSql.Append("SizeUnit=@SizeUnit,");
			strSql.Append("Note=@Note,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("Person=@Person");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@OrderNo", SqlDbType.VarChar,200),
					new SqlParameter("@LibraryID", SqlDbType.VarChar,50),
					new SqlParameter("@CatalogNO", SqlDbType.VarChar,100),
					new SqlParameter("@SizeUnit", SqlDbType.VarChar,100),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,20)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.OrderNo;
			parameters[2].Value = model.LibraryID;
			parameters[3].Value = model.CatalogNO;
			parameters[4].Value = model.SizeUnit;
			parameters[5].Value = model.Note;
			parameters[6].Value = model.UpdateTime;
			parameters[7].Value = model.Person;

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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MCEScreeningLibraries ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

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
		/// 得到一个对象实体
		/// </summary>
		public EuSoft.Model.MCEScreeningLibraries GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,OrderNo,LibraryID,CatalogNO,SizeUnit,Note,UpdateTime,Person from MCEScreeningLibraries ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			EuSoft.Model.MCEScreeningLibraries model=new EuSoft.Model.MCEScreeningLibraries();
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
		public EuSoft.Model.MCEScreeningLibraries DataRowToModel(DataRow row)
		{
			EuSoft.Model.MCEScreeningLibraries model=new EuSoft.Model.MCEScreeningLibraries();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["OrderNo"]!=null)
				{
					model.OrderNo=row["OrderNo"].ToString();
				}
				if(row["LibraryID"]!=null)
				{
					model.LibraryID=row["LibraryID"].ToString();
				}
				if(row["CatalogNO"]!=null)
				{
					model.CatalogNO=row["CatalogNO"].ToString();
				}
				if(row["SizeUnit"]!=null)
				{
					model.SizeUnit=row["SizeUnit"].ToString();
				}
				if(row["Note"]!=null)
				{
					model.Note=row["Note"].ToString();
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
            strSql.Append("select distinct [LibraryID]  ");
			strSql.Append(" FROM MCEScreeningLibraries ");
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
			strSql.Append(" ID,OrderNo,LibraryID,CatalogNO,SizeUnit,Note,UpdateTime,Person ");
			strSql.Append(" FROM MCEScreeningLibraries ");
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
			strSql.Append("select count(1) FROM MCEScreeningLibraries ");
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
			strSql.Append(")AS Row, T.*  from MCEScreeningLibraries T ");
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
			parameters[0].Value = "MCEScreeningLibraries";
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

