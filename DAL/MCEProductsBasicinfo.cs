using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:MCEProductsBasicinfo
	/// </summary>
	public partial class MCEProductsBasicinfo
	{
		public MCEProductsBasicinfo()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EuSoft.Model.MCEProductsBasicinfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MCEProductsBasicinfo(");
			strSql.Append("ID,CSNo,CatalogNo,DrugNames,AlternativeNames,CAS,Formula,Mwt)");
			strSql.Append(" values (");
			strSql.Append("@ID,@CSNo,@CatalogNo,@DrugNames,@AlternativeNames,@CAS,@Formula,@Mwt)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CSNo", SqlDbType.VarChar,40),
					new SqlParameter("@CatalogNo", SqlDbType.VarChar,40),
					new SqlParameter("@DrugNames", SqlDbType.VarChar,800),
					new SqlParameter("@AlternativeNames", SqlDbType.VarChar,800),
					new SqlParameter("@CAS", SqlDbType.VarChar,40),
					new SqlParameter("@Formula", SqlDbType.NVarChar,400),
					new SqlParameter("@Mwt", SqlDbType.VarChar,40)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CSNo;
			parameters[2].Value = model.CatalogNo;
			parameters[3].Value = model.DrugNames;
			parameters[4].Value = model.AlternativeNames;
			parameters[5].Value = model.CAS;
			parameters[6].Value = model.Formula;
			parameters[7].Value = model.Mwt;

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
		public bool Update(EuSoft.Model.MCEProductsBasicinfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MCEProductsBasicinfo set ");
			strSql.Append("ID=@ID,");
			strSql.Append("CSNo=@CSNo,");
			strSql.Append("CatalogNo=@CatalogNo,");
			strSql.Append("DrugNames=@DrugNames,");
			strSql.Append("AlternativeNames=@AlternativeNames,");
			strSql.Append("CAS=@CAS,");
			strSql.Append("Formula=@Formula,");
			strSql.Append("Mwt=@Mwt");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CSNo", SqlDbType.VarChar,40),
					new SqlParameter("@CatalogNo", SqlDbType.VarChar,40),
					new SqlParameter("@DrugNames", SqlDbType.VarChar,800),
					new SqlParameter("@AlternativeNames", SqlDbType.VarChar,800),
					new SqlParameter("@CAS", SqlDbType.VarChar,40),
					new SqlParameter("@Formula", SqlDbType.NVarChar,400),
					new SqlParameter("@Mwt", SqlDbType.VarChar,40)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CSNo;
			parameters[2].Value = model.CatalogNo;
			parameters[3].Value = model.DrugNames;
			parameters[4].Value = model.AlternativeNames;
			parameters[5].Value = model.CAS;
			parameters[6].Value = model.Formula;
			parameters[7].Value = model.Mwt;

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
			strSql.Append("delete from MCEProductsBasicinfo ");
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
		public EuSoft.Model.MCEProductsBasicinfo GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CSNo,CatalogNo,DrugNames,AlternativeNames,CAS,Formula,Mwt from MCEProductsBasicinfo ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			EuSoft.Model.MCEProductsBasicinfo model=new EuSoft.Model.MCEProductsBasicinfo();
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
		public EuSoft.Model.MCEProductsBasicinfo DataRowToModel(DataRow row)
		{
			EuSoft.Model.MCEProductsBasicinfo model=new EuSoft.Model.MCEProductsBasicinfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["CSNo"]!=null)
				{
					model.CSNo=row["CSNo"].ToString();
				}
				if(row["CatalogNo"]!=null)
				{
					model.CatalogNo=row["CatalogNo"].ToString();
				}
				if(row["DrugNames"]!=null)
				{
					model.DrugNames=row["DrugNames"].ToString();
				}
				if(row["AlternativeNames"]!=null)
				{
					model.AlternativeNames=row["AlternativeNames"].ToString();
				}
				if(row["CAS"]!=null)
				{
					model.CAS=row["CAS"].ToString();
				}
				if(row["Formula"]!=null)
				{
					model.Formula=row["Formula"].ToString();
				}
				if(row["Mwt"]!=null)
				{
					model.Mwt=row["Mwt"].ToString();
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
			strSql.Append("select ID,CSNo,CatalogNo,DrugNames,AlternativeNames,CAS,Formula,Mwt ");
			strSql.Append(" FROM MCEProductsBasicinfo ");
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
			strSql.Append(" ID,CSNo,CatalogNo,DrugNames,AlternativeNames,CAS,Formula,Mwt ");
			strSql.Append(" FROM MCEProductsBasicinfo ");
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
			strSql.Append("select count(1) FROM MCEProductsBasicinfo ");
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
			strSql.Append(")AS Row, T.*  from MCEProductsBasicinfo T ");
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
			parameters[0].Value = "MCEProductsBasicinfo";
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

