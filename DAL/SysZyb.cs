using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:SysZyb
	/// </summary>
	public partial class SysZyb
	{
		public SysZyb()
		{}
		#region  BasicMethod

        public DataTable GetXTZyById(string id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select [ID],[PID] as ParentID,[XsName],[AnName],[FrmName],[ImageIndex],[HandNo] from SysZyb a  where exists(select* from SysZyb b where (a.PID=b.ID  or  a.ID=b.ID)  ");
            sql.Append(" and b.id=@id )");
            SqlParameter[] para =
            {
                new SqlParameter("@id",id)
            };
            return DbHelperSQL.Query(sql.ToString(), para).Tables[0];

        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(EuSoft.Model.SysZyb model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SysZyb(");
			strSql.Append("PID,XsName,AnName,FrmName,ImageIndex,HandNo)");
			strSql.Append(" values (");
			strSql.Append("@PID,@XsName,@AnName,@FrmName,@ImageIndex,@HandNo)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PID", SqlDbType.Int,4),
					new SqlParameter("@XsName", SqlDbType.VarChar,100),
					new SqlParameter("@AnName", SqlDbType.VarChar,100),
					new SqlParameter("@FrmName", SqlDbType.VarChar,100),
					new SqlParameter("@ImageIndex", SqlDbType.Int,4),
					new SqlParameter("@HandNo", SqlDbType.Int,4)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.XsName;
			parameters[2].Value = model.AnName;
			parameters[3].Value = model.FrmName;
			parameters[4].Value = model.ImageIndex;
			parameters[5].Value = model.HandNo;

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
		public bool Update(EuSoft.Model.SysZyb model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SysZyb set ");
			strSql.Append("PID=@PID,");
			strSql.Append("XsName=@XsName,");
			strSql.Append("AnName=@AnName,");
			strSql.Append("FrmName=@FrmName,");
			strSql.Append("ImageIndex=@ImageIndex,");
			strSql.Append("HandNo=@HandNo");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@PID", SqlDbType.Int,4),
					new SqlParameter("@XsName", SqlDbType.VarChar,100),
					new SqlParameter("@AnName", SqlDbType.VarChar,100),
					new SqlParameter("@FrmName", SqlDbType.VarChar,100),
					new SqlParameter("@ImageIndex", SqlDbType.Int,4),
					new SqlParameter("@HandNo", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.XsName;
			parameters[2].Value = model.AnName;
			parameters[3].Value = model.FrmName;
			parameters[4].Value = model.ImageIndex;
			parameters[5].Value = model.HandNo;
			parameters[6].Value = model.ID;

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
			strSql.Append("delete from SysZyb ");
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
			strSql.Append("delete from SysZyb ");
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
		public EuSoft.Model.SysZyb GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,PID,XsName,AnName,FrmName,ImageIndex,HandNo from SysZyb ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			EuSoft.Model.SysZyb model=new EuSoft.Model.SysZyb();
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
		public EuSoft.Model.SysZyb DataRowToModel(DataRow row)
		{
			EuSoft.Model.SysZyb model=new EuSoft.Model.SysZyb();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["PID"]!=null && row["PID"].ToString()!="")
				{
					model.PID=int.Parse(row["PID"].ToString());
				}
				if(row["XsName"]!=null)
				{
					model.XsName=row["XsName"].ToString();
				}
				if(row["AnName"]!=null)
				{
					model.AnName=row["AnName"].ToString();
				}
				if(row["FrmName"]!=null)
				{
					model.FrmName=row["FrmName"].ToString();
				}
				if(row["ImageIndex"]!=null && row["ImageIndex"].ToString()!="")
				{
					model.ImageIndex=int.Parse(row["ImageIndex"].ToString());
				}
				if(row["HandNo"]!=null && row["HandNo"].ToString()!="")
				{
					model.HandNo=int.Parse(row["HandNo"].ToString());
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
			strSql.Append("select ID,PID,XsName,AnName,FrmName,ImageIndex,HandNo ");
			strSql.Append(" FROM SysZyb ");
			if(strWhere.Trim()!="")
			{
                strSql.Append(" where    " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetQxList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.ID,b.PID,b.XsName,b.AnName,b.FrmName,b.ImageIndex,b.HandNo,a.IsQx,a.ID as QxId ");
            strSql.Append(" FROM  SysQxB  a right join  SysZyb b on a.ZyId=b.ID  left join MCEUser c on a.userid=c.ID  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  a.xtid is null and " + strWhere);
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
			strSql.Append(" ID,PID,XsName,AnName,FrmName,ImageIndex,HandNo ");
			strSql.Append(" FROM SysZyb ");
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
			strSql.Append("select count(1) FROM SysZyb ");
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
			strSql.Append(")AS Row, T.*  from SysZyb T ");
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
			parameters[0].Value = "SysZyb";
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

