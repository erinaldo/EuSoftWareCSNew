using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:PDFDownloadRecord
	/// </summary>
	public partial class PDFDownloadRecord
	{
		public PDFDownloadRecord()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EuSoft.Model.PDFDownloadRecord model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PDFDownloadRecord(");
			strSql.Append("ID,CatalogNO,TrackingNO,TestDate,RetesDate,MergePDFName,DownloadTime)");
			strSql.Append(" values (");
			strSql.Append("@ID,@CatalogNO,@TrackingNO,@TestDate,@RetesDate,@MergePDFName,@DownloadTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CatalogNO", SqlDbType.NVarChar,20),
					new SqlParameter("@TrackingNO", SqlDbType.Int,4),
					new SqlParameter("@TestDate", SqlDbType.DateTime),
					new SqlParameter("@RetesDate", SqlDbType.DateTime),
					new SqlParameter("@MergePDFName", SqlDbType.NVarChar,100),
					new SqlParameter("@DownloadTime", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CatalogNO;
			parameters[2].Value = model.TrackingNO;
			parameters[3].Value = model.TestDate;
			parameters[4].Value = model.RetesDate;
			parameters[5].Value = model.MergePDFName;
			parameters[6].Value = model.DownloadTime;

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
		public bool Update(EuSoft.Model.PDFDownloadRecord model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PDFDownloadRecord set ");
			strSql.Append("ID=@ID,");
			strSql.Append("CatalogNO=@CatalogNO,");
			strSql.Append("TrackingNO=@TrackingNO,");
			strSql.Append("TestDate=@TestDate,");
			strSql.Append("RetesDate=@RetesDate,");
			strSql.Append("MergePDFName=@MergePDFName,");
			strSql.Append("DownloadTime=@DownloadTime");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CatalogNO", SqlDbType.NVarChar,20),
					new SqlParameter("@TrackingNO", SqlDbType.Int,4),
					new SqlParameter("@TestDate", SqlDbType.DateTime),
					new SqlParameter("@RetesDate", SqlDbType.DateTime),
					new SqlParameter("@MergePDFName", SqlDbType.NVarChar,100),
					new SqlParameter("@DownloadTime", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CatalogNO;
			parameters[2].Value = model.TrackingNO;
			parameters[3].Value = model.TestDate;
			parameters[4].Value = model.RetesDate;
			parameters[5].Value = model.MergePDFName;
			parameters[6].Value = model.DownloadTime;

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
			strSql.Append("delete from PDFDownloadRecord ");
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
		public EuSoft.Model.PDFDownloadRecord GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CatalogNO,TrackingNO,TestDate,RetesDate,MergePDFName,DownloadTime from PDFDownloadRecord ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			EuSoft.Model.PDFDownloadRecord model=new EuSoft.Model.PDFDownloadRecord();
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
		public EuSoft.Model.PDFDownloadRecord DataRowToModel(DataRow row)
		{
			EuSoft.Model.PDFDownloadRecord model=new EuSoft.Model.PDFDownloadRecord();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["CatalogNO"]!=null)
				{
					model.CatalogNO=row["CatalogNO"].ToString();
				}
				if(row["TrackingNO"]!=null && row["TrackingNO"].ToString()!="")
				{
					model.TrackingNO=int.Parse(row["TrackingNO"].ToString());
				}
				if(row["TestDate"]!=null && row["TestDate"].ToString()!="")
				{
					model.TestDate=DateTime.Parse(row["TestDate"].ToString());
				}
				if(row["RetesDate"]!=null && row["RetesDate"].ToString()!="")
				{
					model.RetesDate=DateTime.Parse(row["RetesDate"].ToString());
				}
				if(row["MergePDFName"]!=null)
				{
					model.MergePDFName=row["MergePDFName"].ToString();
				}
				if(row["DownloadTime"]!=null && row["DownloadTime"].ToString()!="")
				{
					model.DownloadTime=DateTime.Parse(row["DownloadTime"].ToString());
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
			strSql.Append("select ID,CatalogNO,TrackingNO,TestDate,RetesDate,MergePDFName,DownloadTime ");
			strSql.Append(" FROM PDFDownloadRecord ");
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
			strSql.Append(" ID,CatalogNO,TrackingNO,TestDate,RetesDate,MergePDFName,DownloadTime ");
			strSql.Append(" FROM PDFDownloadRecord ");
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
			strSql.Append("select count(1) FROM PDFDownloadRecord ");
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
			strSql.Append(")AS Row, T.*  from PDFDownloadRecord T ");
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
			parameters[0].Value = "PDFDownloadRecord";
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

