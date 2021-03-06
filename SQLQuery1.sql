
GO
/****** Object:  StoredProcedure [dbo].[proc_CreateDD]    Script Date: 04/12/2017 14:44:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
ALTER proc [dbo].[proc_CreateDD]  --创建名字为Proc_bb的存储过程 带3个参数
(
   @orderno varchar(40),
   @SalesCompany varchar(300),
   @SalesContactName varchar(200),
   @SalesStreet varchar(100),
   @SalesCity varchar(100),
   @SalesState varchar(100),
   @SalesZip varchar(100),
   @SalesCountry varchar(100),
   @SalesTel varchar(100),
   @SalesFax varchar(100),
   @SaleseMail varchar(100),
   @BillCompany varchar(300),
   @BillContactName varchar(200),
   @BillStreet varchar(100),
   @BillCity varchar(100),
   @BillState varchar(100),
   @BillZip varchar(100),
   @BillCountry varchar(100),
   @BillTel varchar(100),
   @BillFax varchar(100),
   @BilleMail varchar(100),
   @ShipCompany varchar(300),
   @ShipContactName varchar(200),
   @ShipStreet varchar(100),
   @ShipCity varchar(100),
   @ShipState varchar(100),
   @ShipZip varchar(100),
   @ShipCountry varchar(100),
   @ShipTel varchar(100),
   @ShipFax varchar(100),
   @ShipeMail varchar(100),
   @InvioceNo varchar(40),
   @VendorCode varchar(100),
   @Terms varchar(100),
   @PayMethod varchar(100),
   @OrderDate varchar(100),
   @PONumber varchar(100),
   @CustomerRefNo varchar(100),
   @SH varchar(100),
   @Comments varchar(200),
   @Note varchar(200),
   @OrderStatus varchar(100),
   @OrderProcess varchar(100),
   @CustomerID varchar(100),
   @InvoiceDate varchar(100),
   @InvoiceTotal varchar(100),
   @PayStatus varchar(100),
   @ShipStatus varchar(100),
   @Carrier varchar(100),
   @AccountNo varchar(100),
   @UpdateTime varchar(100),
   @SendConfirmDate varchar(100),
   @SendInhibitorDate varchar(100),
   @SendOtherDate varchar(100),
   @SendNewDate varchar(100),
   @Person varchar(100)
)
as
begin  tran --开始执行事务
 declare @OrderNo1 varchar(50),@InvoiceNo1 varchar(50)
 
select @InvoiceNo1=Number+1 from dbo.MCECurrentNumber where Item='Invoice'
select @OrderNo1=left(Number,5)+right('00000000'+cast(RIGHT(Number,9)+1 as varchar),9) from dbo.MCECurrentNumber where Item='OrderNo'

INSERT INTO [dbo].[MCEOrderInfo]
           ([OrderNo],[SalesCompany] ,[SalesContactName],[SalesStreet],[SalesCity],[SalesState],[SalesZip],[SalesCountry]
           ,[SalesTel],[SalesFax],[SaleseMail],[BillCompany],[BillContactName],[BillStreet],[BillCity],[BillState],[BillZip]
           ,[BillCountry],[BillTel],[BillFax],[BilleMail],[ShipCompany],[ShipContactName]
           ,[ShipStreet],[ShipCity],[ShipState],[ShipZip],[ShipCountry],[ShipTel],[ShipFax]
           ,[ShipeMail] ,[InvioceNo] ,[VendorCode] ,[Terms],[PayMethod],[OrderDate],[PONumber],[CustomerRefNo]
           ,[SH],[Comments],[Note],[OrderStatus],[OrderProcess],[CustomerID],[InvoiceDate],[InvoiceTotal],[PayStatus]
           ,[ShipStatus],[Carrier],[AccountNo],[UpdateTime],[SendConfirmDate],[SendInhibitorDate],[SendOtherDate],[SendNewDate],[Person])
     VALUES
           (@OrderNo1,@SalesCompany,@SalesContactName,@SalesStreet,@SalesCity,@SalesState,@SalesZip,@SalesCountry
           ,@SalesTel,@SalesFax,@SaleseMail,@BillCompany,@BillContactName,@BillStreet ,@BillCity,@BillState,@BillZip
           ,@BillCountry,@BillTel,@BillFax,@BilleMail,@ShipCompany,@ShipContactName
           ,@ShipStreet,@ShipCity,@ShipState,@ShipZip,@ShipCountry,@ShipTel,@ShipFax
           ,@ShipeMail,@InvoiceNo1,@VendorCode,@Terms,@PayMethod,@OrderDate,@PONumber,@CustomerRefNo
           ,@SH,@Comments,@Note,@OrderStatus,@OrderProcess,@CustomerID,@InvoiceDate,@InvoiceTotal,@PayStatus
           ,@ShipStatus,@Carrier,@AccountNo,@UpdateTime,@SendConfirmDate,@SendInhibitorDate,@SendOtherDate,@SendNewDate,@Person)
           
        update dbo.MCECurrentNumber set Number=@InvoiceNo1 where Item='Invoice'
        update dbo.MCECurrentNumber set Number=@OrderNo1 where Item='OrderNo'

     


if @@ERROR<>0 --判断  如果两条语句有任何一条出现错误。(如果前面的SQL 语句执行没有错误，则返回0)lect *
begin
rollback tran --开始执行事务的回滚，恢复转账开始之前的状态
return 0
end
 
else  --如果两个语句都执行成功
begin
commit tran --执行这个事务的操作
end
