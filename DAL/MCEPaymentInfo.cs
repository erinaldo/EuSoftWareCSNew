using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:MCEPaymentInfo
	/// </summary>
	public partial class MCEPaymentInfo
	{
		public MCEPaymentInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "MCEPaymentInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MCEPaymentInfo");
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
		public int Add(EuSoft.Model.MCEPaymentInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MCEPaymentInfo(");
			strSql.Append("InvioceNo,ReceivedDate,ReceivedAmount,BankingCost,Balance,Currency,Note,UpdateTime,Person,Cardinfo)");
			strSql.Append(" values (");
			strSql.Append("@InvioceNo,@ReceivedDate,@ReceivedAmount,@BankingCost,@Balance,@Currency,@Note,@UpdateTime,@Person,@Cardinfo)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@InvioceNo", SqlDbType.VarChar,80),
					new SqlParameter("@ReceivedDate", SqlDbType.DateTime),
					new SqlParameter("@ReceivedAmount", SqlDbType.Float,8),
					new SqlParameter("@BankingCost", SqlDbType.Float,8),
					new SqlParameter("@Balance", SqlDbType.VarChar,40),
					new SqlParameter("@Currency", SqlDbType.VarChar,40),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,20),
					new SqlParameter("@Cardinfo", SqlDbType.VarChar,200)};
			parameters[0].Value = model.InvioceNo;
			parameters[1].Value = model.ReceivedDate;
			parameters[2].Value = model.ReceivedAmount;
			parameters[3].Value = model.BankingCost;
			parameters[4].Value = model.Balance;
			parameters[5].Value = model.Currency;
			parameters[6].Value = model.Note;
			parameters[7].Value = model.UpdateTime;
			parameters[8].Value = model.Person;
			parameters[9].Value = model.Cardinfo;

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
		public bool Update(EuSoft.Model.MCEPaymentInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MCEPaymentInfo set ");
			strSql.Append("InvioceNo=@InvioceNo,");
			strSql.Append("ReceivedDate=@ReceivedDate,");
			strSql.Append("ReceivedAmount=@ReceivedAmount,");
			strSql.Append("BankingCost=@BankingCost,");
			strSql.Append("Balance=@Balance,");
			strSql.Append("Currency=@Currency,");
			strSql.Append("Note=@Note,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("Person=@Person,");
			strSql.Append("Cardinfo=@Cardinfo");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@InvioceNo", SqlDbType.VarChar,80),
					new SqlParameter("@ReceivedDate", SqlDbType.DateTime),
					new SqlParameter("@ReceivedAmount", SqlDbType.Float,8),
					new SqlParameter("@BankingCost", SqlDbType.Float,8),
					new SqlParameter("@Balance", SqlDbType.VarChar,40),
					new SqlParameter("@Currency", SqlDbType.VarChar,40),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,20),
					new SqlParameter("@Cardinfo", SqlDbType.VarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.InvioceNo;
			parameters[1].Value = model.ReceivedDate;
			parameters[2].Value = model.ReceivedAmount;
			parameters[3].Value = model.BankingCost;
			parameters[4].Value = model.Balance;
			parameters[5].Value = model.Currency;
			parameters[6].Value = model.Note;
			parameters[7].Value = model.UpdateTime;
			parameters[8].Value = model.Person;
			parameters[9].Value = model.Cardinfo;
			parameters[10].Value = model.ID;

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
			strSql.Append("delete from MCEPaymentInfo ");
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
			strSql.Append("delete from MCEPaymentInfo ");
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
		public EuSoft.Model.MCEPaymentInfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,InvioceNo,ReceivedDate,ReceivedAmount,BankingCost,Balance,Currency,Note,UpdateTime,Person,Cardinfo from MCEPaymentInfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			EuSoft.Model.MCEPaymentInfo model=new EuSoft.Model.MCEPaymentInfo();
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
		public EuSoft.Model.MCEPaymentInfo DataRowToModel(DataRow row)
		{
			EuSoft.Model.MCEPaymentInfo model=new EuSoft.Model.MCEPaymentInfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["InvioceNo"]!=null)
				{
					model.InvioceNo=row["InvioceNo"].ToString();
				}
				if(row["ReceivedDate"]!=null && row["ReceivedDate"].ToString()!="")
				{
					model.ReceivedDate=DateTime.Parse(row["ReceivedDate"].ToString());
				}
				if(row["ReceivedAmount"]!=null && row["ReceivedAmount"].ToString()!="")
				{
					model.ReceivedAmount=decimal.Parse(row["ReceivedAmount"].ToString());
				}
				if(row["BankingCost"]!=null && row["BankingCost"].ToString()!="")
				{
					model.BankingCost=decimal.Parse(row["BankingCost"].ToString());
				}
				if(row["Balance"]!=null)
				{
					model.Balance=row["Balance"].ToString();
				}
				if(row["Currency"]!=null)
				{
					model.Currency=row["Currency"].ToString();
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
				if(row["Cardinfo"]!=null)
				{
					model.Cardinfo=row["Cardinfo"].ToString();
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
			strSql.Append("select ID,InvioceNo,ReceivedDate,ReceivedAmount,BankingCost,Balance,Currency,Note,UpdateTime,Person,Cardinfo ");
			strSql.Append(" FROM MCEPaymentInfo ");
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
			strSql.Append(" ID,InvioceNo,ReceivedDate,ReceivedAmount,BankingCost,Balance,Currency,Note,UpdateTime,Person,Cardinfo ");
			strSql.Append(" FROM MCEPaymentInfo ");
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
			strSql.Append("select count(1) FROM MCEPaymentInfo ");
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
			strSql.Append(")AS Row, T.*  from MCEPaymentInfo T ");
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
			parameters[0].Value = "MCEPaymentInfo";
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

