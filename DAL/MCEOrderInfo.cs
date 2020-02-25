using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace EuSoft.DAL
{
	/// <summary>
	/// 数据访问类:MCEOrderInfo
	/// </summary>
	public partial class MCEOrderInfo
	{
		public MCEOrderInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "MCEOrderInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MCEOrderInfo");
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
        public int AddProc_CreateDD(EuSoft.Model.MCEOrderInfo model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,40),
					new SqlParameter("@SalesCompany", SqlDbType.VarChar,300),
					new SqlParameter("@SalesContactName", SqlDbType.VarChar,200),
					new SqlParameter("@SalesStreet", SqlDbType.VarChar,100),
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
					new SqlParameter("@InvioceNo", SqlDbType.VarChar,100),
					new SqlParameter("@VendorCode", SqlDbType.VarChar,100),
					new SqlParameter("@Terms", SqlDbType.VarChar,100),
					new SqlParameter("@PayMethod", SqlDbType.VarChar,100),
					new SqlParameter("@OrderDate", SqlDbType.DateTime),
					new SqlParameter("@PONumber", SqlDbType.VarChar,100),
					new SqlParameter("@CustomerRefNo", SqlDbType.VarChar,100),
					new SqlParameter("@SH", SqlDbType.Int,4),
					new SqlParameter("@Comments", SqlDbType.VarChar,200),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@OrderStatus", SqlDbType.VarChar,100),
					new SqlParameter("@OrderProcess", SqlDbType.VarChar,100),
					new SqlParameter("@CustomerID", SqlDbType.VarChar,100),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@InvoiceTotal", SqlDbType.Float,8),
					new SqlParameter("@PayStatus", SqlDbType.VarChar,40),
					new SqlParameter("@ShipStatus", SqlDbType.VarChar,40),
					new SqlParameter("@Carrier", SqlDbType.VarChar,40),
					new SqlParameter("@AccountNo", SqlDbType.VarChar,80),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@SendConfirmDate", SqlDbType.DateTime),		
					new SqlParameter("@SendNewDate", SqlDbType.DateTime),
                    new SqlParameter("@StockStatus", SqlDbType.VarChar,20),
					new SqlParameter("@Person", SqlDbType.VarChar,20),
                    new SqlParameter("@Tax", SqlDbType.Decimal,4),
                    new SqlParameter("@Taxation", SqlDbType.Decimal,4)};
            parameters[0].Value = model.OrderNo;
            parameters[1].Value = model.SalesCompany;
            parameters[2].Value = model.SalesContactName;
            parameters[3].Value = model.SalesStreet;
            parameters[4].Value = model.SalesCity;
            parameters[5].Value = model.SalesState;
            parameters[6].Value = model.SalesZip;
            parameters[7].Value = model.SalesCountry;
            parameters[8].Value = model.SalesTel;
            parameters[9].Value = model.SalesFax;
            parameters[10].Value = model.SaleseMail;
            parameters[11].Value = model.BillCompany;
            parameters[12].Value = model.BillContactName;
            parameters[13].Value = model.BillStreet;
            parameters[14].Value = model.BillCity;
            parameters[15].Value = model.BillState;
            parameters[16].Value = model.BillZip;
            parameters[17].Value = model.BillCountry;
            parameters[18].Value = model.BillTel;
            parameters[19].Value = model.BillFax;
            parameters[20].Value = model.BilleMail;
            parameters[21].Value = model.ShipCompany;
            parameters[22].Value = model.ShipContactName;
            parameters[23].Value = model.ShipStreet;
            parameters[24].Value = model.ShipCity;
            parameters[25].Value = model.ShipState;
            parameters[26].Value = model.ShipZip;
            parameters[27].Value = model.ShipCountry;
            parameters[28].Value = model.ShipTel;
            parameters[29].Value = model.ShipFax;
            parameters[30].Value = model.ShipeMail;
            parameters[31].Value = model.InvioceNo;
            parameters[32].Value = model.VendorCode;
            parameters[33].Value = model.Terms;
            parameters[34].Value = model.PayMethod;
            parameters[35].Value = model.OrderDate;
            parameters[36].Value = model.PONumber;
            parameters[37].Value = model.CustomerRefNo;
            parameters[38].Value = model.SH;
            parameters[39].Value = model.Comments;
            parameters[40].Value = model.Note;
            parameters[41].Value = model.OrderStatus;
            parameters[42].Value = model.OrderProcess;
            parameters[43].Value = model.CustomerID;
            parameters[44].Value = model.InvoiceDate;
            parameters[45].Value = model.InvoiceTotal;
            parameters[46].Value = model.PayStatus;
            parameters[47].Value = model.ShipStatus;
            parameters[48].Value = model.Carrier;
            parameters[49].Value = model.AccountNo;
            parameters[50].Value = model.UpdateTime;
            parameters[51].Value = model.SendConfirmDate;
            parameters[52].Value = model.SendNewDate;
            parameters[53].Value = model.StockStatus;
            parameters[54].Value = model.Person;
            parameters[55].Value = model.Tax;
            parameters[56].Value = 0.00;
            int rowsAffected;
            object obj = DbHelperSQL.RunProcedure("proc_CreateDD", parameters,out  rowsAffected);
            if (rowsAffected == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(rowsAffected);
            }
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(EuSoft.Model.MCEOrderInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MCEOrderInfo(");
			strSql.Append("OrderNo,SalesCompany,SalesContactName,SalesStreet,SalesCity,SalesState,SalesZip,SalesCountry,SalesTel,SalesFax,SaleseMail,BillCompany,BillContactName,BillStreet,BillCity,BillState,BillZip,BillCountry,BillTel,BillFax,BilleMail,ShipCompany,ShipContactName,ShipStreet,ShipCity,ShipState,ShipZip,ShipCountry,ShipTel,ShipFax,ShipeMail,InvioceNo,VendorCode,Terms,PayMethod,OrderDate,PONumber,CustomerRefNo,SH,Comments,Note,OrderStatus,OrderProcess,CustomerID,InvoiceDate,InvoiceTotal,PayStatus,ShipStatus,Carrier,AccountNo,UpdateTime,SendConfirmDate,SendNewDate,Person)");
			strSql.Append(" values (");
			strSql.Append("@OrderNo,@SalesCompany,@SalesContactName,@SalesStreet,@SalesCity,@SalesState,@SalesZip,@SalesCountry,@SalesTel,@SalesFax,@SaleseMail,@BillCompany,@BillContactName,@BillStreet,@BillCity,@BillState,@BillZip,@BillCountry,@BillTel,@BillFax,@BilleMail,@ShipCompany,@ShipContactName,@ShipStreet,@ShipCity,@ShipState,@ShipZip,@ShipCountry,@ShipTel,@ShipFax,@ShipeMail,@InvioceNo,@VendorCode,@Terms,@PayMethod,@OrderDate,@PONumber,@CustomerRefNo,@SH,@Comments,@Note,@OrderStatus,@OrderProcess,@CustomerID,@InvoiceDate,@InvoiceTotal,@PayStatus,@ShipStatus,@Carrier,@AccountNo,@UpdateTime,@SendConfirmDate,@SendNewDate,@Person)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderNo", SqlDbType.VarChar,40),
					new SqlParameter("@SalesCompany", SqlDbType.VarChar,300),
					new SqlParameter("@SalesContactName", SqlDbType.VarChar,200),
					new SqlParameter("@SalesStreet", SqlDbType.VarChar,100),
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
					new SqlParameter("@InvioceNo", SqlDbType.VarChar,100),
					new SqlParameter("@VendorCode", SqlDbType.VarChar,100),
					new SqlParameter("@Terms", SqlDbType.VarChar,100),
					new SqlParameter("@PayMethod", SqlDbType.VarChar,100),
					new SqlParameter("@OrderDate", SqlDbType.DateTime),
					new SqlParameter("@PONumber", SqlDbType.VarChar,100),
					new SqlParameter("@CustomerRefNo", SqlDbType.VarChar,100),
					new SqlParameter("@SH", SqlDbType.Int,4),
					new SqlParameter("@Comments", SqlDbType.VarChar,200),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@OrderStatus", SqlDbType.VarChar,100),
					new SqlParameter("@OrderProcess", SqlDbType.VarChar,100),
					new SqlParameter("@CustomerID", SqlDbType.VarChar,100),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@InvoiceTotal", SqlDbType.Float,8),
					new SqlParameter("@PayStatus", SqlDbType.VarChar,40),
					new SqlParameter("@ShipStatus", SqlDbType.VarChar,40),
					new SqlParameter("@Carrier", SqlDbType.VarChar,40),
					new SqlParameter("@AccountNo", SqlDbType.VarChar,80),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@SendConfirmDate", SqlDbType.DateTime),
					new SqlParameter("@SendNewDate", SqlDbType.DateTime),
					new SqlParameter("@Person", SqlDbType.VarChar,20)};
			parameters[0].Value = model.OrderNo;
			parameters[1].Value = model.SalesCompany;
			parameters[2].Value = model.SalesContactName;
			parameters[3].Value = model.SalesStreet;
			parameters[4].Value = model.SalesCity;
			parameters[5].Value = model.SalesState;
			parameters[6].Value = model.SalesZip;
			parameters[7].Value = model.SalesCountry;
			parameters[8].Value = model.SalesTel;
			parameters[9].Value = model.SalesFax;
			parameters[10].Value = model.SaleseMail;
			parameters[11].Value = model.BillCompany;
			parameters[12].Value = model.BillContactName;
			parameters[13].Value = model.BillStreet;
			parameters[14].Value = model.BillCity;
			parameters[15].Value = model.BillState;
			parameters[16].Value = model.BillZip;
			parameters[17].Value = model.BillCountry;
			parameters[18].Value = model.BillTel;
			parameters[19].Value = model.BillFax;
			parameters[20].Value = model.BilleMail;
			parameters[21].Value = model.ShipCompany;
			parameters[22].Value = model.ShipContactName;
			parameters[23].Value = model.ShipStreet;
			parameters[24].Value = model.ShipCity;
			parameters[25].Value = model.ShipState;
			parameters[26].Value = model.ShipZip;
			parameters[27].Value = model.ShipCountry;
			parameters[28].Value = model.ShipTel;
			parameters[29].Value = model.ShipFax;
			parameters[30].Value = model.ShipeMail;
			parameters[31].Value = model.InvioceNo;
			parameters[32].Value = model.VendorCode;
			parameters[33].Value = model.Terms;
			parameters[34].Value = model.PayMethod;
			parameters[35].Value = model.OrderDate;
			parameters[36].Value = model.PONumber;
			parameters[37].Value = model.CustomerRefNo;
			parameters[38].Value = model.SH;
			parameters[39].Value = model.Comments;
			parameters[40].Value = model.Note;
			parameters[41].Value = model.OrderStatus;
			parameters[42].Value = model.OrderProcess;
			parameters[43].Value = model.CustomerID;
			parameters[44].Value = model.InvoiceDate;
			parameters[45].Value = model.InvoiceTotal;
			parameters[46].Value = model.PayStatus;
			parameters[47].Value = model.ShipStatus;
			parameters[48].Value = model.Carrier;
			parameters[49].Value = model.AccountNo;
			parameters[50].Value = model.UpdateTime;
			parameters[51].Value = model.SendConfirmDate;
			parameters[52].Value = model.SendNewDate;
			parameters[53].Value = model.Person;

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
		public bool Update(EuSoft.Model.MCEOrderInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MCEOrderInfo set ");
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
			strSql.Append("VendorCode=@VendorCode,");
			strSql.Append("Terms=@Terms,");
			strSql.Append("PayMethod=@PayMethod,");
			strSql.Append("OrderDate=@OrderDate,");
			strSql.Append("PONumber=@PONumber,");
			strSql.Append("CustomerRefNo=@CustomerRefNo,");
			strSql.Append("SH=@SH,");
			strSql.Append("Comments=@Comments,");
			strSql.Append("Note=@Note,");
			strSql.Append("OrderStatus=@OrderStatus,");
			strSql.Append("OrderProcess=@OrderProcess,");
			strSql.Append("CustomerID=@CustomerID,");
			strSql.Append("InvoiceDate=@InvoiceDate,");
			strSql.Append("InvoiceTotal=@InvoiceTotal,");
			strSql.Append("PayStatus=@PayStatus,");
			strSql.Append("ShipStatus=@ShipStatus,");
			strSql.Append("Carrier=@Carrier,");
			strSql.Append("AccountNo=@AccountNo,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("SendConfirmDate=@SendConfirmDate,");
			strSql.Append("SendNewDate=@SendNewDate,");
            strSql.Append("StockStatus=@StockStatus,");
			strSql.Append("Person=@Person,");
            strSql.Append("Tax=@Tax,");
            strSql.Append("Taxation=@Taxation");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@SalesCompany", SqlDbType.VarChar,300),
					new SqlParameter("@SalesContactName", SqlDbType.VarChar,200),
					new SqlParameter("@SalesStreet", SqlDbType.VarChar,100),
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
					new SqlParameter("@VendorCode", SqlDbType.VarChar,100),
					new SqlParameter("@Terms", SqlDbType.VarChar,100),
					new SqlParameter("@PayMethod", SqlDbType.VarChar,100),
					new SqlParameter("@OrderDate", SqlDbType.DateTime),
					new SqlParameter("@PONumber", SqlDbType.VarChar,100),
					new SqlParameter("@CustomerRefNo", SqlDbType.VarChar,100),
					new SqlParameter("@SH", SqlDbType.Int,4),
					new SqlParameter("@Comments", SqlDbType.VarChar,200),
					new SqlParameter("@Note", SqlDbType.VarChar,200),
					new SqlParameter("@OrderStatus", SqlDbType.VarChar,100),
					new SqlParameter("@OrderProcess", SqlDbType.VarChar,100),
					new SqlParameter("@CustomerID", SqlDbType.VarChar,100),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@InvoiceTotal", SqlDbType.Float,8),
					new SqlParameter("@PayStatus", SqlDbType.VarChar,40),
					new SqlParameter("@ShipStatus", SqlDbType.VarChar,40),
					new SqlParameter("@Carrier", SqlDbType.VarChar,40),
					new SqlParameter("@AccountNo", SqlDbType.VarChar,80),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@SendConfirmDate", SqlDbType.DateTime),
					new SqlParameter("@SendNewDate", SqlDbType.DateTime),
			        new SqlParameter("@StockStatus", SqlDbType.VarChar,20),
					new SqlParameter("@Person", SqlDbType.VarChar,20),
                    new SqlParameter("@Tax", SqlDbType.Decimal),
                    new SqlParameter("@Taxation", SqlDbType.Decimal),
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
			parameters[30].Value = model.VendorCode;
			parameters[31].Value = model.Terms;
			parameters[32].Value = model.PayMethod;
			parameters[33].Value = model.OrderDate;
			parameters[34].Value = model.PONumber;
			parameters[35].Value = model.CustomerRefNo;
			parameters[36].Value = model.SH;
			parameters[37].Value = model.Comments;
			parameters[38].Value = model.Note;
			parameters[39].Value = model.OrderStatus;
			parameters[40].Value = model.OrderProcess;
			parameters[41].Value = model.CustomerID;
			parameters[42].Value = model.InvoiceDate;
			parameters[43].Value = model.InvoiceTotal;
			parameters[44].Value = model.PayStatus;
			parameters[45].Value = model.ShipStatus;
			parameters[46].Value = model.Carrier;
			parameters[47].Value = model.AccountNo;
			parameters[48].Value = model.UpdateTime;
			parameters[49].Value = model.SendConfirmDate;
			parameters[50].Value = model.SendNewDate;
		    parameters[51].Value = model.StockStatus;
			parameters[52].Value = model.Person;
            parameters[53].Value = model.Tax;
            parameters[54].Value = model.Taxation;
			parameters[55].Value = model.ID;

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
			strSql.Append("delete from MCEOrderInfo ");
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
			strSql.Append("delete from MCEOrderInfo ");
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
		public EuSoft.Model.MCEOrderInfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ID,OrderNo,SalesCompany,SalesContactName,SalesStreet,SalesCity,SalesState,SalesZip,SalesCountry,SalesTel,SalesFax,SaleseMail,BillCompany,BillContactName,BillStreet,BillCity,BillState,BillZip,BillCountry,BillTel,BillFax,BilleMail,ShipCompany,ShipContactName,ShipStreet,ShipCity,ShipState,ShipZip,ShipCountry,ShipTel,ShipFax,ShipeMail,InvioceNo,VendorCode,Terms,PayMethod,OrderDate,PONumber,CustomerRefNo,SH,Comments,Note,OrderStatus,OrderProcess,CustomerID,InvoiceDate,InvoiceTotal,PayStatus,ShipStatus,Carrier,AccountNo,UpdateTime,SendConfirmDate,SendNewDate,StockStatus,Person,Tax,Taxation from MCEOrderInfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			EuSoft.Model.MCEOrderInfo model=new EuSoft.Model.MCEOrderInfo();
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
		public EuSoft.Model.MCEOrderInfo DataRowToModel(DataRow row)
		{
			EuSoft.Model.MCEOrderInfo model=new EuSoft.Model.MCEOrderInfo();
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
				if(row["InvioceNo"]!=null)
				{
					model.InvioceNo=row["InvioceNo"].ToString();
				}
				if(row["VendorCode"]!=null)
				{
					model.VendorCode=row["VendorCode"].ToString();
				}
				if(row["Terms"]!=null)
				{
					model.Terms=row["Terms"].ToString();
				}
				if(row["PayMethod"]!=null)
				{
					model.PayMethod=row["PayMethod"].ToString();
				}
				if(row["OrderDate"]!=null && row["OrderDate"].ToString()!="")
				{
					model.OrderDate=DateTime.Parse(row["OrderDate"].ToString());
				}
				if(row["PONumber"]!=null)
				{
					model.PONumber=row["PONumber"].ToString();
				}
				if(row["CustomerRefNo"]!=null)
				{
					model.CustomerRefNo=row["CustomerRefNo"].ToString();
				}
				if(row["SH"]!=null && row["SH"].ToString()!="")
				{
					model.SH=int.Parse(row["SH"].ToString());
				}
				if(row["Comments"]!=null)
				{
					model.Comments=row["Comments"].ToString();
				}
				if(row["Note"]!=null)
				{
					model.Note=row["Note"].ToString();
				}
				if(row["OrderStatus"]!=null)
				{
					model.OrderStatus=row["OrderStatus"].ToString();
				}
				if(row["OrderProcess"]!=null)
				{
					model.OrderProcess=row["OrderProcess"].ToString();
				}
				if(row["CustomerID"]!=null)
				{
					model.CustomerID=row["CustomerID"].ToString();
				}
				if(row["InvoiceDate"]!=null && row["InvoiceDate"].ToString()!="")
				{
					model.InvoiceDate=DateTime.Parse(row["InvoiceDate"].ToString());
				}
				if(row["InvoiceTotal"]!=null && row["InvoiceTotal"].ToString()!="")
				{
					model.InvoiceTotal=decimal.Parse(row["InvoiceTotal"].ToString());
				}
				if(row["PayStatus"]!=null)
				{
					model.PayStatus=row["PayStatus"].ToString();
				}
				if(row["ShipStatus"]!=null)
				{
					model.ShipStatus=row["ShipStatus"].ToString();
				}
				if(row["Carrier"]!=null)
				{
					model.Carrier=row["Carrier"].ToString();
				}
				if(row["AccountNo"]!=null)
				{
					model.AccountNo=row["AccountNo"].ToString();
				}
				if(row["UpdateTime"]!=null && row["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(row["UpdateTime"].ToString());
				}
				if(row["SendConfirmDate"]!=null && row["SendConfirmDate"].ToString()!="")
				{
					model.SendConfirmDate=DateTime.Parse(row["SendConfirmDate"].ToString());
				}			
				if(row["SendNewDate"]!=null && row["SendNewDate"].ToString()!="")
				{
					model.SendNewDate=DateTime.Parse(row["SendNewDate"].ToString());
				}
                if (row["StockStatus"] != null)
			    {
                    model.StockStatus = row["StockStatus"].ToString();
			    }
				if(row["Person"]!=null)
				{
					model.Person=row["Person"].ToString();
				}
                if (row["Tax"] != null && row["Tax"].ToString() != "")
				{
                    model.Tax = Convert.ToDecimal(row["Tax"]);
				}
                if (row["Taxation"] != null && row["Taxation"].ToString() != "")
				{
                    model.Taxation =  Convert.ToDecimal(row["Taxation"]);
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
            strSql.Append("select ID,OrderNo,SalesCompany,SalesContactName,SalesStreet,SalesCity,SalesState,SalesZip,SalesCountry,SalesTel,SalesFax,SaleseMail,BillCompany,BillContactName,BillStreet,BillCity,BillState,BillZip,BillCountry,BillTel,BillFax,BilleMail,ShipCompany,ShipContactName,ShipStreet,ShipCity,ShipState,ShipZip,ShipCountry,ShipTel,ShipFax,ShipeMail,InvioceNo,VendorCode,Terms,PayMethod,OrderDate,PONumber,CustomerRefNo,SH,Comments,Note,OrderStatus,OrderProcess,CustomerID,InvoiceDate,InvoiceTotal,PayStatus,ShipStatus,Carrier,AccountNo,UpdateTime,SendConfirmDate,SendNewDate,StockStatus,Person ");
			strSql.Append(" FROM MCEOrderInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        public DataSet GetDistList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select distinct BillCompany,BillContactName,BillStreet,BillCity,BillState,BillZip,BillCountry,BillTel,BillFax,BilleMail,ShipCompany,ShipContactName,ShipStreet,ShipCity,ShipState,ShipZip,ShipCountry,ShipTel,ShipFax,ShipeMail ");
            strSql.Append(" FROM MCEOrderInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            strSql.Append(" ID,OrderNo,SalesCompany,SalesContactName,SalesStreet,SalesCity,SalesState,SalesZip,SalesCountry,SalesTel,SalesFax,SaleseMail,BillCompany,BillContactName,BillStreet,BillCity,BillState,BillZip,BillCountry,BillTel,BillFax,BilleMail,ShipCompany,ShipContactName,ShipStreet,ShipCity,ShipState,ShipZip,ShipCountry,ShipTel,ShipFax,ShipeMail,InvioceNo,VendorCode,Terms,PayMethod,OrderDate,PONumber,CustomerRefNo,SH,Comments,Note,OrderStatus,OrderProcess,CustomerID,InvoiceDate,InvoiceTotal,PayStatus,ShipStatus,Carrier,AccountNo,UpdateTime,SendConfirmDate,SendNewDate,StockStatus,Person ");
			strSql.Append(" FROM MCEOrderInfo ");
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
			strSql.Append("select count(1) FROM MCEOrderInfo ");
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
			strSql.Append(")AS Row, T.*  from MCEOrderInfo T ");
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
			parameters[0].Value = "MCEOrderInfo";
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

