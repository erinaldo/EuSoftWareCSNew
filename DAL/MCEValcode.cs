using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:MCEValcode
	/// </summary>
	public partial class MCEValcode
	{
		public MCEValcode()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(EuSoft.Model.MCEValcode model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MCEValcode(");
			strSql.Append("CatalogNo,BatchNo,ValCode,Size,Unit,Note,UpdateTime,Person)");
			strSql.Append(" values (");
			strSql.Append("@CatalogNo,@BatchNo,@ValCode,@Size,@Unit,@Note,@UpdateTime,@Person)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CatalogNo", SqlDbType.VarChar,50),
					new SqlParameter("@BatchNo", SqlDbType.VarChar,50),
					new SqlParameter("@ValCode", SqlDbType.VarChar,50),
					new SqlParameter("@Size", SqlDbType.VarChar,50),
					new SqlParameter("@Unit", SqlDbType.VarChar,50),
					new SqlParameter("@Note", SqlDbType.VarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,50)};
			parameters[0].Value = model.CatalogNo;
			parameters[1].Value = model.BatchNo;
			parameters[2].Value = model.ValCode;
			parameters[3].Value = model.Size;
			parameters[4].Value = model.Unit;
			parameters[5].Value = model.Note;
			parameters[6].Value = model.UpdateTime;
			parameters[7].Value = model.Person;

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
		public bool Update(EuSoft.Model.MCEValcode model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MCEValcode set ");
			strSql.Append("CatalogNo=@CatalogNo,");
			strSql.Append("BatchNo=@BatchNo,");
			strSql.Append("ValCode=@ValCode,");
			strSql.Append("Size=@Size,");
			strSql.Append("Unit=@Unit,");
			strSql.Append("Note=@Note,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("Person=@Person");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@CatalogNo", SqlDbType.VarChar,50),
					new SqlParameter("@BatchNo", SqlDbType.VarChar,50),
					new SqlParameter("@ValCode", SqlDbType.VarChar,50),
					new SqlParameter("@Size", SqlDbType.VarChar,50),
					new SqlParameter("@Unit", SqlDbType.VarChar,50),
					new SqlParameter("@Note", SqlDbType.VarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.CatalogNo;
			parameters[1].Value = model.BatchNo;
			parameters[2].Value = model.ValCode;
			parameters[3].Value = model.Size;
			parameters[4].Value = model.Unit;
			parameters[5].Value = model.Note;
			parameters[6].Value = model.UpdateTime;
			parameters[7].Value = model.Person;
			parameters[8].Value = model.ID;

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
			strSql.Append("delete from MCEValcode ");
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
			strSql.Append("delete from MCEValcode ");
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
		public EuSoft.Model.MCEValcode GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CatalogNo,BatchNo,ValCode,Size,Unit,Note,UpdateTime,Person from MCEValcode ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			EuSoft.Model.MCEValcode model=new EuSoft.Model.MCEValcode();
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
		public EuSoft.Model.MCEValcode DataRowToModel(DataRow row)
		{
			EuSoft.Model.MCEValcode model=new EuSoft.Model.MCEValcode();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["CatalogNo"]!=null)
				{
					model.CatalogNo=row["CatalogNo"].ToString();
				}
				if(row["BatchNo"]!=null)
				{
					model.BatchNo=row["BatchNo"].ToString();
				}
				if(row["ValCode"]!=null)
				{
					model.ValCode=row["ValCode"].ToString();
				}
				if(row["Size"]!=null)
				{
					model.Size=row["Size"].ToString();
				}
				if(row["Unit"]!=null)
				{
					model.Unit=row["Unit"].ToString();
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
			strSql.Append("select ID,CatalogNo,BatchNo,ValCode,Size,Unit,Note,UpdateTime,Person ");
			strSql.Append(" FROM MCEValcode ");
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
			strSql.Append(" ID,CatalogNo,BatchNo,ValCode,Size,Unit,Note,UpdateTime,Person ");
			strSql.Append(" FROM MCEValcode ");
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
			strSql.Append("select count(1) FROM MCEValcode ");
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
			strSql.Append(")AS Row, T.*  from MCEValcode T ");
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
			parameters[0].Value = "MCEValcode";
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

