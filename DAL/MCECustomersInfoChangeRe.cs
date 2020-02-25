using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:MCECustomersInfoChangeRe
	/// </summary>
	public partial class MCECustomersInfoChangeRe
	{
		public MCECustomersInfoChangeRe()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "MCECustomersInfoChangeRe"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MCECustomersInfoChangeRe");
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
		public int Add(EuSoft.Model.MCECustomersInfoChangeRe model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MCECustomersInfoChangeRe(");
			strSql.Append("SalesCompany,SalesContactName,SalesStreet,SalesCity,SalesState,SalesZip,SalesCountry,SalesTel,SalesFax,SaleseMail,BillCompany,BillContactName,BillStreet,BillCity,BillState,BillZip,BillCountry,BillTel,BillFax,BilleMail,ShipCompany,ShipContactName,ShipStreet,ShipCity,ShipState,ShipZip,ShipCountry,ShipTel,ShipFax,ShipeMail,Terms,PayMethod,MailPromotion,VendorCode,Carrier,AccountNo,Note,VATIDNo,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons)");
			strSql.Append(" values (");
			strSql.Append("@SalesCompany,@SalesContactName,@SalesStreet,@SalesCity,@SalesState,@SalesZip,@SalesCountry,@SalesTel,@SalesFax,@SaleseMail,@BillCompany,@BillContactName,@BillStreet,@BillCity,@BillState,@BillZip,@BillCountry,@BillTel,@BillFax,@BilleMail,@ShipCompany,@ShipContactName,@ShipStreet,@ShipCity,@ShipState,@ShipZip,@ShipCountry,@ShipTel,@ShipFax,@ShipeMail,@Terms,@PayMethod,@MailPromotion,@VendorCode,@Carrier,@AccountNo,@Note,@VATIDNo,@UpdateTime,@Person,@ChangeNote,@ChangePerson,@ChangeTime,@Reasons)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SalesCompany", SqlDbType.VarChar,300),
					new SqlParameter("@SalesContactName", SqlDbType.VarChar,200),
					new SqlParameter("@SalesStreet", SqlDbType.VarChar,200),
					new SqlParameter("@SalesCity", SqlDbType.VarChar,100),
					new SqlParameter("@SalesState", SqlDbType.VarChar,100),
					new SqlParameter("@SalesZip", SqlDbType.VarChar,100),
					new SqlParameter("@SalesCountry", SqlDbType.VarChar,100),
					new SqlParameter("@SalesTel", SqlDbType.VarChar,100),
					new SqlParameter("@SalesFax", SqlDbType.VarChar,100),
					new SqlParameter("@SaleseMail", SqlDbType.VarChar,100),
					new SqlParameter("@BillCompany", SqlDbType.VarChar,300),
					new SqlParameter("@BillContactName", SqlDbType.VarChar,200),
					new SqlParameter("@BillStreet", SqlDbType.VarChar,100),
					new SqlParameter("@BillCity", SqlDbType.VarChar,100),
					new SqlParameter("@BillState", SqlDbType.VarChar,100),
					new SqlParameter("@BillZip", SqlDbType.VarChar,100),
					new SqlParameter("@BillCountry", SqlDbType.VarChar,100),
					new SqlParameter("@BillTel", SqlDbType.VarChar,100),
					new SqlParameter("@BillFax", SqlDbType.VarChar,100),
					new SqlParameter("@BilleMail", SqlDbType.VarChar,100),
					new SqlParameter("@ShipCompany", SqlDbType.VarChar,300),
					new SqlParameter("@ShipContactName", SqlDbType.VarChar,200),
					new SqlParameter("@ShipStreet", SqlDbType.VarChar,100),
					new SqlParameter("@ShipCity", SqlDbType.VarChar,100),
					new SqlParameter("@ShipState", SqlDbType.VarChar,100),
					new SqlParameter("@ShipZip", SqlDbType.VarChar,100),
					new SqlParameter("@ShipCountry", SqlDbType.VarChar,100),
					new SqlParameter("@ShipTel", SqlDbType.VarChar,100),
					new SqlParameter("@ShipFax", SqlDbType.VarChar,100),
					new SqlParameter("@ShipeMail", SqlDbType.VarChar,100),
					new SqlParameter("@Terms", SqlDbType.VarChar,100),
					new SqlParameter("@PayMethod", SqlDbType.VarChar,100),
					new SqlParameter("@MailPromotion", SqlDbType.VarChar,100),
					new SqlParameter("@VendorCode", SqlDbType.VarChar,100),
					new SqlParameter("@Carrier", SqlDbType.VarChar,100),
					new SqlParameter("@AccountNo", SqlDbType.VarChar,100),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@VATIDNo", SqlDbType.VarChar,200),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,200),
					new SqlParameter("@ChangeNote", SqlDbType.VarChar,200),
					new SqlParameter("@ChangePerson", SqlDbType.VarChar,200),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@Reasons", SqlDbType.VarChar,20)};
			parameters[0].Value = model.SalesCompany;
			parameters[1].Value = model.SalesContactName;
			parameters[2].Value = model.SalesStreet;
			parameters[3].Value = model.SalesCity;
			parameters[4].Value = model.SalesState;
			parameters[5].Value = model.SalesZip;
			parameters[6].Value = model.SalesCountry;
			parameters[7].Value = model.SalesTel;
			parameters[8].Value = model.SalesFax;
			parameters[9].Value = model.SaleseMail;
			parameters[10].Value = model.BillCompany;
			parameters[11].Value = model.BillContactName;
			parameters[12].Value = model.BillStreet;
			parameters[13].Value = model.BillCity;
			parameters[14].Value = model.BillState;
			parameters[15].Value = model.BillZip;
			parameters[16].Value = model.BillCountry;
			parameters[17].Value = model.BillTel;
			parameters[18].Value = model.BillFax;
			parameters[19].Value = model.BilleMail;
			parameters[20].Value = model.ShipCompany;
			parameters[21].Value = model.ShipContactName;
			parameters[22].Value = model.ShipStreet;
			parameters[23].Value = model.ShipCity;
			parameters[24].Value = model.ShipState;
			parameters[25].Value = model.ShipZip;
			parameters[26].Value = model.ShipCountry;
			parameters[27].Value = model.ShipTel;
			parameters[28].Value = model.ShipFax;
			parameters[29].Value = model.ShipeMail;
			parameters[30].Value = model.Terms;
			parameters[31].Value = model.PayMethod;
			parameters[32].Value = model.MailPromotion;
			parameters[33].Value = model.VendorCode;
			parameters[34].Value = model.Carrier;
			parameters[35].Value = model.AccountNo;
			parameters[36].Value = model.Note;
			parameters[37].Value = model.VATIDNo;
			parameters[38].Value = model.UpdateTime;
			parameters[39].Value = model.Person;
			parameters[40].Value = model.ChangeNote;
			parameters[41].Value = model.ChangePerson;
			parameters[42].Value = model.ChangeTime;
			parameters[43].Value = model.Reasons;

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
		public bool Update(EuSoft.Model.MCECustomersInfoChangeRe model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MCECustomersInfoChangeRe set ");
			strSql.Append("SalesCompany=@SalesCompany,");
			strSql.Append("SalesContactName=@SalesContactName,");
			strSql.Append("SalesStreet=@SalesStreet,");
			strSql.Append("SalesCity=@SalesCity,");
			strSql.Append("SalesState=@SalesState,");
			strSql.Append("SalesZip=@SalesZip,");
			strSql.Append("SalesCountry=@SalesCountry,");
			strSql.Append("SalesTel=@SalesTel,");
			strSql.Append("SalesFax=@SalesFax,");
			strSql.Append("SaleseMail=@SaleseMail,");
			strSql.Append("BillCompany=@BillCompany,");
			strSql.Append("BillContactName=@BillContactName,");
			strSql.Append("BillStreet=@BillStreet,");
			strSql.Append("BillCity=@BillCity,");
			strSql.Append("BillState=@BillState,");
			strSql.Append("BillZip=@BillZip,");
			strSql.Append("BillCountry=@BillCountry,");
			strSql.Append("BillTel=@BillTel,");
			strSql.Append("BillFax=@BillFax,");
			strSql.Append("BilleMail=@BilleMail,");
			strSql.Append("ShipCompany=@ShipCompany,");
			strSql.Append("ShipContactName=@ShipContactName,");
			strSql.Append("ShipStreet=@ShipStreet,");
			strSql.Append("ShipCity=@ShipCity,");
			strSql.Append("ShipState=@ShipState,");
			strSql.Append("ShipZip=@ShipZip,");
			strSql.Append("ShipCountry=@ShipCountry,");
			strSql.Append("ShipTel=@ShipTel,");
			strSql.Append("ShipFax=@ShipFax,");
			strSql.Append("ShipeMail=@ShipeMail,");
			strSql.Append("Terms=@Terms,");
			strSql.Append("PayMethod=@PayMethod,");
			strSql.Append("MailPromotion=@MailPromotion,");
			strSql.Append("VendorCode=@VendorCode,");
			strSql.Append("Carrier=@Carrier,");
			strSql.Append("AccountNo=@AccountNo,");
			strSql.Append("Note=@Note,");
			strSql.Append("VATIDNo=@VATIDNo,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("Person=@Person,");
			strSql.Append("ChangeNote=@ChangeNote,");
			strSql.Append("ChangePerson=@ChangePerson,");
			strSql.Append("ChangeTime=@ChangeTime,");
			strSql.Append("Reasons=@Reasons");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@SalesCompany", SqlDbType.VarChar,300),
					new SqlParameter("@SalesContactName", SqlDbType.VarChar,200),
					new SqlParameter("@SalesStreet", SqlDbType.VarChar,200),
					new SqlParameter("@SalesCity", SqlDbType.VarChar,100),
					new SqlParameter("@SalesState", SqlDbType.VarChar,100),
					new SqlParameter("@SalesZip", SqlDbType.VarChar,100),
					new SqlParameter("@SalesCountry", SqlDbType.VarChar,100),
					new SqlParameter("@SalesTel", SqlDbType.VarChar,100),
					new SqlParameter("@SalesFax", SqlDbType.VarChar,100),
					new SqlParameter("@SaleseMail", SqlDbType.VarChar,100),
					new SqlParameter("@BillCompany", SqlDbType.VarChar,300),
					new SqlParameter("@BillContactName", SqlDbType.VarChar,200),
					new SqlParameter("@BillStreet", SqlDbType.VarChar,100),
					new SqlParameter("@BillCity", SqlDbType.VarChar,100),
					new SqlParameter("@BillState", SqlDbType.VarChar,100),
					new SqlParameter("@BillZip", SqlDbType.VarChar,100),
					new SqlParameter("@BillCountry", SqlDbType.VarChar,100),
					new SqlParameter("@BillTel", SqlDbType.VarChar,100),
					new SqlParameter("@BillFax", SqlDbType.VarChar,100),
					new SqlParameter("@BilleMail", SqlDbType.VarChar,100),
					new SqlParameter("@ShipCompany", SqlDbType.VarChar,300),
					new SqlParameter("@ShipContactName", SqlDbType.VarChar,200),
					new SqlParameter("@ShipStreet", SqlDbType.VarChar,100),
					new SqlParameter("@ShipCity", SqlDbType.VarChar,100),
					new SqlParameter("@ShipState", SqlDbType.VarChar,100),
					new SqlParameter("@ShipZip", SqlDbType.VarChar,100),
					new SqlParameter("@ShipCountry", SqlDbType.VarChar,100),
					new SqlParameter("@ShipTel", SqlDbType.VarChar,100),
					new SqlParameter("@ShipFax", SqlDbType.VarChar,100),
					new SqlParameter("@ShipeMail", SqlDbType.VarChar,100),
					new SqlParameter("@Terms", SqlDbType.VarChar,100),
					new SqlParameter("@PayMethod", SqlDbType.VarChar,100),
					new SqlParameter("@MailPromotion", SqlDbType.VarChar,100),
					new SqlParameter("@VendorCode", SqlDbType.VarChar,100),
					new SqlParameter("@Carrier", SqlDbType.VarChar,100),
					new SqlParameter("@AccountNo", SqlDbType.VarChar,100),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@VATIDNo", SqlDbType.VarChar,200),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,200),
					new SqlParameter("@ChangeNote", SqlDbType.VarChar,200),
					new SqlParameter("@ChangePerson", SqlDbType.VarChar,200),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@Reasons", SqlDbType.VarChar,20),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.SalesCompany;
			parameters[1].Value = model.SalesContactName;
			parameters[2].Value = model.SalesStreet;
			parameters[3].Value = model.SalesCity;
			parameters[4].Value = model.SalesState;
			parameters[5].Value = model.SalesZip;
			parameters[6].Value = model.SalesCountry;
			parameters[7].Value = model.SalesTel;
			parameters[8].Value = model.SalesFax;
			parameters[9].Value = model.SaleseMail;
			parameters[10].Value = model.BillCompany;
			parameters[11].Value = model.BillContactName;
			parameters[12].Value = model.BillStreet;
			parameters[13].Value = model.BillCity;
			parameters[14].Value = model.BillState;
			parameters[15].Value = model.BillZip;
			parameters[16].Value = model.BillCountry;
			parameters[17].Value = model.BillTel;
			parameters[18].Value = model.BillFax;
			parameters[19].Value = model.BilleMail;
			parameters[20].Value = model.ShipCompany;
			parameters[21].Value = model.ShipContactName;
			parameters[22].Value = model.ShipStreet;
			parameters[23].Value = model.ShipCity;
			parameters[24].Value = model.ShipState;
			parameters[25].Value = model.ShipZip;
			parameters[26].Value = model.ShipCountry;
			parameters[27].Value = model.ShipTel;
			parameters[28].Value = model.ShipFax;
			parameters[29].Value = model.ShipeMail;
			parameters[30].Value = model.Terms;
			parameters[31].Value = model.PayMethod;
			parameters[32].Value = model.MailPromotion;
			parameters[33].Value = model.VendorCode;
			parameters[34].Value = model.Carrier;
			parameters[35].Value = model.AccountNo;
			parameters[36].Value = model.Note;
			parameters[37].Value = model.VATIDNo;
			parameters[38].Value = model.UpdateTime;
			parameters[39].Value = model.Person;
			parameters[40].Value = model.ChangeNote;
			parameters[41].Value = model.ChangePerson;
			parameters[42].Value = model.ChangeTime;
			parameters[43].Value = model.Reasons;
			parameters[44].Value = model.ID;

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
			strSql.Append("delete from MCECustomersInfoChangeRe ");
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
			strSql.Append("delete from MCECustomersInfoChangeRe ");
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
		public EuSoft.Model.MCECustomersInfoChangeRe GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,SalesCompany,SalesContactName,SalesStreet,SalesCity,SalesState,SalesZip,SalesCountry,SalesTel,SalesFax,SaleseMail,BillCompany,BillContactName,BillStreet,BillCity,BillState,BillZip,BillCountry,BillTel,BillFax,BilleMail,ShipCompany,ShipContactName,ShipStreet,ShipCity,ShipState,ShipZip,ShipCountry,ShipTel,ShipFax,ShipeMail,Terms,PayMethod,MailPromotion,VendorCode,Carrier,AccountNo,Note,VATIDNo,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons from MCECustomersInfoChangeRe ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			EuSoft.Model.MCECustomersInfoChangeRe model=new EuSoft.Model.MCECustomersInfoChangeRe();
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
		public EuSoft.Model.MCECustomersInfoChangeRe DataRowToModel(DataRow row)
		{
			EuSoft.Model.MCECustomersInfoChangeRe model=new EuSoft.Model.MCECustomersInfoChangeRe();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["SalesCompany"]!=null)
				{
					model.SalesCompany=row["SalesCompany"].ToString();
				}
				if(row["SalesContactName"]!=null)
				{
					model.SalesContactName=row["SalesContactName"].ToString();
				}
				if(row["SalesStreet"]!=null)
				{
					model.SalesStreet=row["SalesStreet"].ToString();
				}
				if(row["SalesCity"]!=null)
				{
					model.SalesCity=row["SalesCity"].ToString();
				}
				if(row["SalesState"]!=null)
				{
					model.SalesState=row["SalesState"].ToString();
				}
				if(row["SalesZip"]!=null)
				{
					model.SalesZip=row["SalesZip"].ToString();
				}
				if(row["SalesCountry"]!=null)
				{
					model.SalesCountry=row["SalesCountry"].ToString();
				}
				if(row["SalesTel"]!=null)
				{
					model.SalesTel=row["SalesTel"].ToString();
				}
				if(row["SalesFax"]!=null)
				{
					model.SalesFax=row["SalesFax"].ToString();
				}
				if(row["SaleseMail"]!=null)
				{
					model.SaleseMail=row["SaleseMail"].ToString();
				}
				if(row["BillCompany"]!=null)
				{
					model.BillCompany=row["BillCompany"].ToString();
				}
				if(row["BillContactName"]!=null)
				{
					model.BillContactName=row["BillContactName"].ToString();
				}
				if(row["BillStreet"]!=null)
				{
					model.BillStreet=row["BillStreet"].ToString();
				}
				if(row["BillCity"]!=null)
				{
					model.BillCity=row["BillCity"].ToString();
				}
				if(row["BillState"]!=null)
				{
					model.BillState=row["BillState"].ToString();
				}
				if(row["BillZip"]!=null)
				{
					model.BillZip=row["BillZip"].ToString();
				}
				if(row["BillCountry"]!=null)
				{
					model.BillCountry=row["BillCountry"].ToString();
				}
				if(row["BillTel"]!=null)
				{
					model.BillTel=row["BillTel"].ToString();
				}
				if(row["BillFax"]!=null)
				{
					model.BillFax=row["BillFax"].ToString();
				}
				if(row["BilleMail"]!=null)
				{
					model.BilleMail=row["BilleMail"].ToString();
				}
				if(row["ShipCompany"]!=null)
				{
					model.ShipCompany=row["ShipCompany"].ToString();
				}
				if(row["ShipContactName"]!=null)
				{
					model.ShipContactName=row["ShipContactName"].ToString();
				}
				if(row["ShipStreet"]!=null)
				{
					model.ShipStreet=row["ShipStreet"].ToString();
				}
				if(row["ShipCity"]!=null)
				{
					model.ShipCity=row["ShipCity"].ToString();
				}
				if(row["ShipState"]!=null)
				{
					model.ShipState=row["ShipState"].ToString();
				}
				if(row["ShipZip"]!=null)
				{
					model.ShipZip=row["ShipZip"].ToString();
				}
				if(row["ShipCountry"]!=null)
				{
					model.ShipCountry=row["ShipCountry"].ToString();
				}
				if(row["ShipTel"]!=null)
				{
					model.ShipTel=row["ShipTel"].ToString();
				}
				if(row["ShipFax"]!=null)
				{
					model.ShipFax=row["ShipFax"].ToString();
				}
				if(row["ShipeMail"]!=null)
				{
					model.ShipeMail=row["ShipeMail"].ToString();
				}
				if(row["Terms"]!=null)
				{
					model.Terms=row["Terms"].ToString();
				}
				if(row["PayMethod"]!=null)
				{
					model.PayMethod=row["PayMethod"].ToString();
				}
				if(row["MailPromotion"]!=null)
				{
					model.MailPromotion=row["MailPromotion"].ToString();
				}
				if(row["VendorCode"]!=null)
				{
					model.VendorCode=row["VendorCode"].ToString();
				}
				if(row["Carrier"]!=null)
				{
					model.Carrier=row["Carrier"].ToString();
				}
				if(row["AccountNo"]!=null)
				{
					model.AccountNo=row["AccountNo"].ToString();
				}
				if(row["Note"]!=null)
				{
					model.Note=row["Note"].ToString();
				}
				if(row["VATIDNo"]!=null)
				{
					model.VATIDNo=row["VATIDNo"].ToString();
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
			strSql.Append("select ID,SalesCompany,SalesContactName,SalesStreet,SalesCity,SalesState,SalesZip,SalesCountry,SalesTel,SalesFax,SaleseMail,BillCompany,BillContactName,BillStreet,BillCity,BillState,BillZip,BillCountry,BillTel,BillFax,BilleMail,ShipCompany,ShipContactName,ShipStreet,ShipCity,ShipState,ShipZip,ShipCountry,ShipTel,ShipFax,ShipeMail,Terms,PayMethod,MailPromotion,VendorCode,Carrier,AccountNo,Note,VATIDNo,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons ");
			strSql.Append(" FROM MCECustomersInfoChangeRe ");
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
			strSql.Append(" ID,SalesCompany,SalesContactName,SalesStreet,SalesCity,SalesState,SalesZip,SalesCountry,SalesTel,SalesFax,SaleseMail,BillCompany,BillContactName,BillStreet,BillCity,BillState,BillZip,BillCountry,BillTel,BillFax,BilleMail,ShipCompany,ShipContactName,ShipStreet,ShipCity,ShipState,ShipZip,ShipCountry,ShipTel,ShipFax,ShipeMail,Terms,PayMethod,MailPromotion,VendorCode,Carrier,AccountNo,Note,VATIDNo,UpdateTime,Person,ChangeNote,ChangePerson,ChangeTime,Reasons ");
			strSql.Append(" FROM MCECustomersInfoChangeRe ");
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
			strSql.Append("select count(1) FROM MCECustomersInfoChangeRe ");
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
			strSql.Append(")AS Row, T.*  from MCECustomersInfoChangeRe T ");
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
			parameters[0].Value = "MCECustomersInfoChangeRe";
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

