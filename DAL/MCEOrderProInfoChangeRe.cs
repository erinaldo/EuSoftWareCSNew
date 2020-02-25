using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:MCEOrderProInfoChangeRe
	/// </summary>
	public partial class MCEOrderProInfoChangeRe
	{
		public MCEOrderProInfoChangeRe()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "MCEOrderProInfoChangeRe"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MCEOrderProInfoChangeRe");
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
		public int Add(EuSoft.Model.MCEOrderProInfoChangeRe model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MCEOrderProInfoChangeRe(");
			strSql.Append("OrderNo,ProCatalogNo,ProDescription,ProSize,ProUnit,ProQuantity,ProAmount,ProCurrency,ProDunOn,ProNote,ProLibraryID,ProductStatus,ProductProcess,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons)");
			strSql.Append(" values (");
			strSql.Append("@OrderNo,@ProCatalogNo,@ProDescription,@ProSize,@ProUnit,@ProQuantity,@ProAmount,@ProCurrency,@ProDunOn,@ProNote,@ProLibraryID,@ProductStatus,@ProductProcess,@UpdateTime,@Person,@ChangeNote,@ChangePerson,@ChangeTime,@Reasons)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,40),
					new SqlParameter("@ProCatalogNo", SqlDbType.VarChar,40),
					new SqlParameter("@ProDescription", SqlDbType.VarChar,200),
					new SqlParameter("@ProSize", SqlDbType.Float,8),
					new SqlParameter("@ProUnit", SqlDbType.VarChar,10),
					new SqlParameter("@ProQuantity", SqlDbType.Int,4),
					new SqlParameter("@ProAmount", SqlDbType.Float,8),
					new SqlParameter("@ProCurrency", SqlDbType.VarChar,40),
					new SqlParameter("@ProDunOn", SqlDbType.DateTime),
					new SqlParameter("@ProNote", SqlDbType.VarChar,200),
					new SqlParameter("@ProLibraryID", SqlDbType.VarChar,40),
					new SqlParameter("@ProductStatus", SqlDbType.VarChar,40),
					new SqlParameter("@ProductProcess", SqlDbType.VarChar,40),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,20),
					new SqlParameter("@ChangeNote", SqlDbType.VarChar,200),
					new SqlParameter("@ChangePerson", SqlDbType.VarChar,200),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@Reasons", SqlDbType.VarChar,200)};
			parameters[0].Value = model.OrderNo;
			parameters[1].Value = model.ProCatalogNo;
			parameters[2].Value = model.ProDescription;
			parameters[3].Value = model.ProSize;
			parameters[4].Value = model.ProUnit;
			parameters[5].Value = model.ProQuantity;
			parameters[6].Value = model.ProAmount;
			parameters[7].Value = model.ProCurrency;
			parameters[8].Value = model.ProDunOn;
			parameters[9].Value = model.ProNote;
			parameters[10].Value = model.ProLibraryID;
			parameters[11].Value = model.ProductStatus;
			parameters[12].Value = model.ProductProcess;
			parameters[13].Value = model.UpdateTime;
			parameters[14].Value = model.Person;
			parameters[15].Value = model.ChangeNote;
			parameters[16].Value = model.ChangePerson;
			parameters[17].Value = model.ChangeTime;
			parameters[18].Value = model.Reasons;

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
		public bool Update(EuSoft.Model.MCEOrderProInfoChangeRe model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MCEOrderProInfoChangeRe set ");
			strSql.Append("OrderNo=@OrderNo,");
			strSql.Append("ProCatalogNo=@ProCatalogNo,");
			strSql.Append("ProDescription=@ProDescription,");
			strSql.Append("ProSize=@ProSize,");
			strSql.Append("ProUnit=@ProUnit,");
			strSql.Append("ProQuantity=@ProQuantity,");
			strSql.Append("ProAmount=@ProAmount,");
			strSql.Append("ProCurrency=@ProCurrency,");
			strSql.Append("ProDunOn=@ProDunOn,");
			strSql.Append("ProNote=@ProNote,");
			strSql.Append("ProLibraryID=@ProLibraryID,");
			strSql.Append("ProductStatus=@ProductStatus,");
			strSql.Append("ProductProcess=@ProductProcess,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("Person=@Person,");
			strSql.Append("ChangeNote=@ChangeNote,");
			strSql.Append("ChangePerson=@ChangePerson,");
			strSql.Append("ChangeTime=@ChangeTime,");
			strSql.Append("Reasons=@Reasons");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,40),
					new SqlParameter("@ProCatalogNo", SqlDbType.VarChar,40),
					new SqlParameter("@ProDescription", SqlDbType.VarChar,200),
					new SqlParameter("@ProSize", SqlDbType.Float,8),
					new SqlParameter("@ProUnit", SqlDbType.VarChar,10),
					new SqlParameter("@ProQuantity", SqlDbType.Int,4),
					new SqlParameter("@ProAmount", SqlDbType.Float,8),
					new SqlParameter("@ProCurrency", SqlDbType.VarChar,40),
					new SqlParameter("@ProDunOn", SqlDbType.DateTime),
					new SqlParameter("@ProNote", SqlDbType.VarChar,200),
					new SqlParameter("@ProLibraryID", SqlDbType.VarChar,40),
					new SqlParameter("@ProductStatus", SqlDbType.VarChar,40),
					new SqlParameter("@ProductProcess", SqlDbType.VarChar,40),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,20),
					new SqlParameter("@ChangeNote", SqlDbType.VarChar,200),
					new SqlParameter("@ChangePerson", SqlDbType.VarChar,200),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@Reasons", SqlDbType.VarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.OrderNo;
			parameters[1].Value = model.ProCatalogNo;
			parameters[2].Value = model.ProDescription;
			parameters[3].Value = model.ProSize;
			parameters[4].Value = model.ProUnit;
			parameters[5].Value = model.ProQuantity;
			parameters[6].Value = model.ProAmount;
			parameters[7].Value = model.ProCurrency;
			parameters[8].Value = model.ProDunOn;
			parameters[9].Value = model.ProNote;
			parameters[10].Value = model.ProLibraryID;
			parameters[11].Value = model.ProductStatus;
			parameters[12].Value = model.ProductProcess;
			parameters[13].Value = model.UpdateTime;
			parameters[14].Value = model.Person;
			parameters[15].Value = model.ChangeNote;
			parameters[16].Value = model.ChangePerson;
			parameters[17].Value = model.ChangeTime;
			parameters[18].Value = model.Reasons;
			parameters[19].Value = model.ID;

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
			strSql.Append("delete from MCEOrderProInfoChangeRe ");
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
			strSql.Append("delete from MCEOrderProInfoChangeRe ");
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
		public EuSoft.Model.MCEOrderProInfoChangeRe GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,OrderNo,ProCatalogNo,ProDescription,ProSize,ProUnit,ProQuantity,ProAmount,ProCurrency,ProDunOn,ProNote,ProLibraryID,ProductStatus,ProductProcess,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons from MCEOrderProInfoChangeRe ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			EuSoft.Model.MCEOrderProInfoChangeRe model=new EuSoft.Model.MCEOrderProInfoChangeRe();
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
		public EuSoft.Model.MCEOrderProInfoChangeRe DataRowToModel(DataRow row)
		{
			EuSoft.Model.MCEOrderProInfoChangeRe model=new EuSoft.Model.MCEOrderProInfoChangeRe();
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
				if(row["ProCatalogNo"]!=null)
				{
					model.ProCatalogNo=row["ProCatalogNo"].ToString();
				}
				if(row["ProDescription"]!=null)
				{
					model.ProDescription=row["ProDescription"].ToString();
				}
				if(row["ProSize"]!=null && row["ProSize"].ToString()!="")
				{
					model.ProSize=decimal.Parse(row["ProSize"].ToString());
				}
				if(row["ProUnit"]!=null)
				{
					model.ProUnit=row["ProUnit"].ToString();
				}
				if(row["ProQuantity"]!=null && row["ProQuantity"].ToString()!="")
				{
					model.ProQuantity=int.Parse(row["ProQuantity"].ToString());
				}
				if(row["ProAmount"]!=null && row["ProAmount"].ToString()!="")
				{
					model.ProAmount=decimal.Parse(row["ProAmount"].ToString());
				}
				if(row["ProCurrency"]!=null)
				{
					model.ProCurrency=row["ProCurrency"].ToString();
				}
				if(row["ProDunOn"]!=null && row["ProDunOn"].ToString()!="")
				{
					model.ProDunOn=DateTime.Parse(row["ProDunOn"].ToString());
				}
				if(row["ProNote"]!=null)
				{
					model.ProNote=row["ProNote"].ToString();
				}
				if(row["ProLibraryID"]!=null)
				{
					model.ProLibraryID=row["ProLibraryID"].ToString();
				}
				if(row["ProductStatus"]!=null)
				{
					model.ProductStatus=row["ProductStatus"].ToString();
				}
				if(row["ProductProcess"]!=null)
				{
					model.ProductProcess=row["ProductProcess"].ToString();
				}
				if(row["UpdateTime"]!=null && row["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(row["UpdateTime"].ToString());
				}
				if(row["Person"]!=null)
				{
					model.Person=row["Person"].ToString();
				}
				if(row["ChangeNote"]!=null)
				{
					model.ChangeNote=row["ChangeNote"].ToString();
				}
				if(row["ChangePerson"]!=null)
				{
					model.ChangePerson=row["ChangePerson"].ToString();
				}
				if(row["ChangeTime"]!=null && row["ChangeTime"].ToString()!="")
				{
					model.ChangeTime=DateTime.Parse(row["ChangeTime"].ToString());
				}
				if(row["Reasons"]!=null)
				{
					model.Reasons=row["Reasons"].ToString();
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
			strSql.Append("select ID,OrderNo,ProCatalogNo,ProDescription,ProSize,ProUnit,ProQuantity,ProAmount,ProCurrency,ProDunOn,ProNote,ProLibraryID,ProductStatus,ProductProcess,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons ");
			strSql.Append(" FROM MCEOrderProInfoChangeRe ");
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
			strSql.Append(" ID,OrderNo,ProCatalogNo,ProDescription,ProSize,ProUnit,ProQuantity,ProAmount,ProCurrency,ProDunOn,ProNote,ProLibraryID,ProductStatus,ProductProcess,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons ");
			strSql.Append(" FROM MCEOrderProInfoChangeRe ");
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
			strSql.Append("select count(1) FROM MCEOrderProInfoChangeRe ");
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
			strSql.Append(")AS Row, T.*  from MCEOrderProInfoChangeRe T ");
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
			parameters[0].Value = "MCEOrderProInfoChangeRe";
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

