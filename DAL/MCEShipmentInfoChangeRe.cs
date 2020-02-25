using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:MCEShipmentInfoChangeRe
	/// </summary>
	public partial class MCEShipmentInfoChangeRe
	{
		public MCEShipmentInfoChangeRe()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "MCEShipmentInfoChangeRe"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MCEShipmentInfoChangeRe");
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
		public int Add(EuSoft.Model.MCEShipmentInfoChangeRe model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MCEShipmentInfoChangeRe(");
			strSql.Append("OrderNo,ShipDate,ShipVia,TrackingNo,Note,Cost,Currency,ShipmentStatus,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons)");
			strSql.Append(" values (");
			strSql.Append("@OrderNo,@ShipDate,@ShipVia,@TrackingNo,@Note,@Cost,@Currency,@ShipmentStatus,@UpdateTime,@Person,@ChangeNote,@ChangePerson,@ChangeTime,@Reasons)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,40),
					new SqlParameter("@ShipDate", SqlDbType.DateTime),
					new SqlParameter("@ShipVia", SqlDbType.VarChar,40),
					new SqlParameter("@TrackingNo", SqlDbType.VarChar,40),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
					new SqlParameter("@Currency", SqlDbType.VarChar,40),
					new SqlParameter("@ShipmentStatus", SqlDbType.VarChar,40),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,200),
					new SqlParameter("@ChangeNote", SqlDbType.VarChar,200),
					new SqlParameter("@ChangePerson", SqlDbType.VarChar,20),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@Reasons", SqlDbType.VarChar,200)};
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
			parameters[10].Value = model.ChangeNote;
			parameters[11].Value = model.ChangePerson;
			parameters[12].Value = model.ChangeTime;
			parameters[13].Value = model.Reasons;

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
		public bool Update(EuSoft.Model.MCEShipmentInfoChangeRe model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MCEShipmentInfoChangeRe set ");
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
			strSql.Append("ChangeNote=@ChangeNote,");
			strSql.Append("ChangePerson=@ChangePerson,");
			strSql.Append("ChangeTime=@ChangeTime,");
			strSql.Append("Reasons=@Reasons");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,40),
					new SqlParameter("@ShipDate", SqlDbType.DateTime),
					new SqlParameter("@ShipVia", SqlDbType.VarChar,40),
					new SqlParameter("@TrackingNo", SqlDbType.VarChar,40),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
					new SqlParameter("@Currency", SqlDbType.VarChar,40),
					new SqlParameter("@ShipmentStatus", SqlDbType.VarChar,40),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,200),
					new SqlParameter("@ChangeNote", SqlDbType.VarChar,200),
					new SqlParameter("@ChangePerson", SqlDbType.VarChar,20),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@Reasons", SqlDbType.VarChar,200),
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
			parameters[10].Value = model.ChangeNote;
			parameters[11].Value = model.ChangePerson;
			parameters[12].Value = model.ChangeTime;
			parameters[13].Value = model.Reasons;
			parameters[14].Value = model.ID;

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
			strSql.Append("delete from MCEShipmentInfoChangeRe ");
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
			strSql.Append("delete from MCEShipmentInfoChangeRe ");
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
		public EuSoft.Model.MCEShipmentInfoChangeRe GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,OrderNo,ShipDate,ShipVia,TrackingNo,Note,Cost,Currency,ShipmentStatus,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons from MCEShipmentInfoChangeRe ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			EuSoft.Model.MCEShipmentInfoChangeRe model=new EuSoft.Model.MCEShipmentInfoChangeRe();
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
		public EuSoft.Model.MCEShipmentInfoChangeRe DataRowToModel(DataRow row)
		{
			EuSoft.Model.MCEShipmentInfoChangeRe model=new EuSoft.Model.MCEShipmentInfoChangeRe();
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
				if(row["ShipDate"]!=null && row["ShipDate"].ToString()!="")
				{
					model.ShipDate=DateTime.Parse(row["ShipDate"].ToString());
				}
				if(row["ShipVia"]!=null)
				{
					model.ShipVia=row["ShipVia"].ToString();
				}
				if(row["TrackingNo"]!=null)
				{
					model.TrackingNo=row["TrackingNo"].ToString();
				}
				if(row["Note"]!=null)
				{
					model.Note=row["Note"].ToString();
				}
				if(row["Cost"]!=null && row["Cost"].ToString()!="")
				{
					model.Cost=decimal.Parse(row["Cost"].ToString());
				}
				if(row["Currency"]!=null)
				{
					model.Currency=row["Currency"].ToString();
				}
				if(row["ShipmentStatus"]!=null)
				{
					model.ShipmentStatus=row["ShipmentStatus"].ToString();
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
			strSql.Append("select ID,OrderNo,ShipDate,ShipVia,TrackingNo,Note,Cost,Currency,ShipmentStatus,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons ");
			strSql.Append(" FROM MCEShipmentInfoChangeRe ");
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
			strSql.Append(" ID,OrderNo,ShipDate,ShipVia,TrackingNo,Note,Cost,Currency,ShipmentStatus,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons ");
			strSql.Append(" FROM MCEShipmentInfoChangeRe ");
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
			strSql.Append("select count(1) FROM MCEShipmentInfoChangeRe ");
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
			strSql.Append(")AS Row, T.*  from MCEShipmentInfoChangeRe T ");
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
			parameters[0].Value = "MCEShipmentInfoChangeRe";
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

