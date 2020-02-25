using Medical.Yottor.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Medical.Yottor.UI
{

    public partial class FrmAllOrderInfor : DevExpress.XtraEditors.XtraForm
    {

        public FrmAllOrderInfor()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void butSearch_Click(object sender, EventArgs e)
        {

            var strWhere = string.Empty;
            if (txtDate1.Text != string.Empty)
            {
                strWhere += " and orderdate>='" + txtDate1.Text + "' ";
            }
            if (txtDate2.Text != string.Empty)
            {
                strWhere += " and orderdate<='" + txtDate2.Text + "' ";
            }
            if (txtOrderNo.Text != string.Empty)
            {
                strWhere += " and orderno like '%" + txtOrderNo.Text + "%' ";
            }
            if (txtPO.Text != string.Empty)
            {
                strWhere += " and [ponumber] like '%" + txtDate1.Text + "%' ";
            }
            if (txtCountry.Text != string.Empty)
            {
                strWhere += " and ShipCountry like '%" + txtCountry.Text + "%' ";
            }


            var sqlStr = @"select distinct a.orderno, a.orderdate,a.InvioceNo,a.ponumber , a.InvoiceTotal,a.invoicetotalusd,a.SalesCompany,a.SalesCountry,[ShipCompany]
      ,[ShipContactName]
      ,[ShipStreet]
      ,[ShipCity]
      ,[ShipState]
      ,[ShipZip]
      ,[ShipCountry]
      ,[ShipTel]
      ,[ShipFax]
      ,[ShipeMail],ffff.ProCount,b.[Catalog ID],
b.[Quantity sold], b.proamount as[Selling Price],a.SH as [S&H]
from MCEOrderInfo a  left join (
SELECT OrderNo, sum(proamount) as proamount, [Catalog ID]=stuff((select '\' + ProCatalogNo  from MCEOrderProInfo WHERE OrderNo=x.OrderNo for xml path('')),1,1,''),
[Quantity sold]=stuff((select '\' + CAST(ProSize AS VARCHAR(20))+ProUnit + case CAST(ProQuantity AS VARCHAR(20)) when '1' then '' else 'x'+CAST(ProQuantity AS VARCHAR(20)) end from MCEOrderProInfo WHERE OrderNo=x.OrderNo for xml path('')),1,1,''),
[Product Name]=stuff((select '\' + ProDescription  from MCEOrderProInfo WHERE OrderNo=x.OrderNo for xml path('')),1,1,'')
FROM MCEOrderProInfo x
GROUP BY OrderNo) b on a.orderno=b.OrderNo left join ( 
select InvioceNo,sum(Bankingcost) Bankingcost,sum(CAST(Balance as float)) balance, 
receiveddate=stuff((select '\' + convert(varchar(20),receiveddate,120)  from MCEPaymentInfo WHERE InvioceNo=kk.InvioceNo
 for xml path('')),1,1,'') from MCEPaymentInfo kk group by  InvioceNo )  c on  a.InvioceNo=c.InvioceNo
left join
(
select  OrderNo,shipvia=
stuff((select '\' + shipvia  from (select d.OrderNo,d.Cost,d.TrackingNo,d.ShipDate,h.ChangeTime,d.shipvia from  MCEShipmentInfo d left join  (select top 1 ChangeTime,ChangeItem  from  MCEChangeinfo   where item='Tracking' and ChangeNote  ='Delivered'  order by ChangeTime desc) h
on d.TrackingNo=h.ChangeItem ) mm  WHERE OrderNo=m.OrderNo for xml path('')),1,1,''),sum(Cost) as cost,TrackingNo=stuff((select '\' + [trackingno]  from (select d.OrderNo,d.Cost,d.TrackingNo,d.ShipDate,h.ChangeTime,d.shipvia from  MCEShipmentInfo d left join  (select top 1 ChangeTime,ChangeItem  from  MCEChangeinfo   where item='Tracking' and ChangeNote  ='Delivered'  order by ChangeTime desc) h
on d.TrackingNo=h.ChangeItem ) mm  WHERE OrderNo=m.OrderNo for xml path('')),1,1,''),
ShipDate=stuff((select '\' + convert(varchar(20),[shipdate],120)  from (select d.OrderNo,d.Cost,d.TrackingNo,d.ShipDate,h.ChangeTime,d.shipvia from  MCEShipmentInfo d left join  (select top 1 ChangeTime,ChangeItem  from  MCEChangeinfo   where item='Tracking' and ChangeNote  ='Delivered' order by ChangeTime desc) h
on d.TrackingNo=h.ChangeItem ) mm WHERE OrderNo=m.OrderNo for xml path('')),1,1,''),
ChangeTime=stuff((select '\' + convert(varchar(20),ChangeTime,120)  from (select d.OrderNo,d.Cost,d.TrackingNo,d.ShipDate,h.ChangeTime,d.shipvia from  MCEShipmentInfo d left join  (select top 1 ChangeTime,ChangeItem  from  MCEChangeinfo   where item='Tracking' and ChangeNote  ='Delivered'  order by ChangeTime desc) h
on d.TrackingNo=h.ChangeItem ) mm WHERE OrderNo=m.OrderNo for xml path('')),1,1,'') from 
 (select d.OrderNo,d.Cost,d.TrackingNo,d.ShipDate,h.ChangeTime,d.shipvia from  MCEShipmentInfo d left join  (select top 1 ChangeTime,ChangeItem from  MCEChangeinfo   where item='Tracking' and ChangeNote  ='Delivered'  order by ChangeTime desc) h
on d.TrackingNo=h.ChangeItem  )m  GROUP BY OrderNo )   s on a.OrderNo=s.OrderNo

left join (
 select OrderNo,COUNT(*) ProCount from MCEOrderProInfo GROUP BY OrderNo
) ffff on  a.orderno=ffff.OrderNo
 where  a.orderprocess='Finalize'
  " + strWhere;




            dt = Maticsoft.DBUtility.DbHelperSQL.Query(sqlStr, 300).Tables[0];


            gridControl1.DataSource = dt;
            gridView1.BestFitColumns();

            //string strWhere = "";
            //if (txtDate1.Text != string.Empty)
            //{

            //    strWhere += " and date>='" + txtDate1.Text + "' ";
            //}
            //if (txtDate2.Text != string.Empty)
            //{
            //    strWhere += " and date<='" + txtDate2.Text + "' ";
            //}
            //if (txtOrderNo.Text != string.Empty)
            //{
            //    strWhere += " and orderno like '%" + txtOrderNo.Text + "%' ";
            //}
            //if (txtPO.Text != string.Empty)
            //{
            //    strWhere += " and [PO#] like '%" + txtDate1.Text + "%' ";
            //}
            //if (txtCountry.Text != string.Empty)
            //{
            //    strWhere += " and Country like '%" + txtCountry.Text + "%' ";
            //}


            //string sqlStr = " select * from ( ";
            //sqlStr = sqlStr + " select [orderno],[date],[Sold to],[Country],[PO#],[Catalog ID],[Product Name],[Quantity sold],[Selling Price] SellingPrice ,[S&H] SH,[S&H Cost] SHCost,[Invoice#],[Invoice Amount] InvoiceAmount,[Payment],[payment method],[Banking cost] Bankingcost,[Net sales] Netsales,[Pay due],[Paid-day],[Carrier],[Tracking#],[Ship date],[Deliver date],[OrderStatus],[PayStatus],[ShipStatus] from sall  where 1=1 " + strWhere;
            //sqlStr = sqlStr + "     union all select '总计 '+cast(COUNT(*) as varchar(10)) [orderno],null,'' [Sold to],'' [Country],'' [PO#],'' [Catalog ID],'' [Product Name],'' [Quantity sold],sum([Selling Price]) SellingPrice ,sum([S&H]) SH,sum([S&H Cost]) SHCost,''[Invoice#],sum([Invoice Amount]) InvoiceAmount,'' [Payment],'' [payment method],sum([Banking cost]) Bankingcost,sum([Net sales]) Netsales,null,null,''[Carrier],''[Tracking#],''[Ship date],''[Deliver date],''[OrderStatus],''[PayStatus],''[ShipStatus] from sall where 1=1  " + strWhere;


          
            //sqlStr += " ) mm order by [date] desc";
            //dt = Maticsoft.DBUtility.DbHelperSQL.Query(sqlStr).Tables[0];


            //gridControl1.DataSource = dt;
            //this.gridView1.BestFitColumns();
            //DevExpress.XtraGrid.Columns.GridColumn col_Profit = gridView1.Columns[0];

            //gridView1.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //gridView1.Columns[0].SummaryItem.DisplayFormat = dt.Rows.Count.ToString("F0");

            //DevExpress.XtraGrid.Columns.GridColumn col_Profit11 = gridView1.Columns[8];

            //gridView1.Columns[8].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns[8].SummaryItem.DisplayFormat = "Selling Price Total:" + dt.Compute("sum(SellingPrice)", "TRUE").ToString();


            //DevExpress.XtraGrid.Columns.GridColumn col_Profit1 = gridView1.Columns[9];

            //gridView1.Columns[9].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //gridView1.Columns[9].SummaryItem.DisplayFormat = "SH Total:" + dt.Compute("sum(SH)", "TRUE").ToString();
            //DevExpress.XtraGrid.Columns.GridColumn col_Profit2 = gridView1.Columns[10];

            //gridView1.Columns[10].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //gridView1.Columns[10].SummaryItem.DisplayFormat = "S H Cost Total::" + dt.Compute("sum(SHCost)", "TRUE").ToString();
            //DevExpress.XtraGrid.Columns.GridColumn col_Profit3 = gridView1.Columns[12];

            //gridView1.Columns[12].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //gridView1.Columns[12].SummaryItem.DisplayFormat = "Payment Total:" + dt.Compute("sum(InvoiceAmount)", "TRUE").ToString();
            //DevExpress.XtraGrid.Columns.GridColumn col_Profit4 = gridView1.Columns[15];

            //gridView1.Columns[15].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //gridView1.Columns[15].SummaryItem.DisplayFormat = "Products Total:" + dt.Compute("sum(Bankingcost)", "TRUE").ToString();

            //DevExpress.XtraGrid.Columns.GridColumn col_Profit5 = gridView1.Columns[16];

            //gridView1.Columns[16].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //gridView1.Columns[16].SummaryItem.DisplayFormat = "Shipment Cost Total:" + dt.Compute("sum(Netsales)", "TRUE").ToString();

        }




        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {


        }





        private void FrmOrderProductsInfor_Load(object sender, EventArgs e)
        {

        }



        private void butClear_Click(object sender, EventArgs e)
        {

            txtCatalogID.Text = "";
            txtPO.Text = "";
            txtDate2.Text = "";
            txtDate1.Text = "";
            txtCountry.Text = "";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //string filename = NPOIHelper.ExportToExcel(dt, "Order SalesStatistics");
            //if (!string.IsNullOrEmpty(filename))
            //    MessageDxUtil.ShowTips("success");




            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.FileName = "CSSales.xlsx";
            fileDialog.Title = "Excel";
            fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                // options.RawDataMode = true;
                // gridControl1.ExportToXls(fileDialog.FileName);
                // gridControl1.ExportToExcelOld(fileDialog.FileName);
                gridControl1.ExportToXlsx(fileDialog.FileName);
                if (MessageDxUtil.ShowYesNoAndTips("success,open？") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(fileDialog.FileName);
                }
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }


    }
}
