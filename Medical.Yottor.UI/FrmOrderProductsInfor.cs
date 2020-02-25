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
    public partial class FrmOrderProductsInfor : DevExpress.XtraEditors.XtraForm
    {
        List<string> listId = new List<string>();
        DataTable dtid = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtAddid = new DataTable();
        string sqlStr, sqlwhere, sqlwhereAdd;
        public FrmOrderProductsInfor()
        {
            InitializeComponent();
            dtid.Columns.Add(new DataColumn("ID", typeof(int)));
            dtAddid.Columns.Add(new DataColumn("ID", typeof(int)));
        }
       // DataTable dt = new DataTable();

        private void butSearch_Click(object sender, EventArgs e)
        {
            sqlStr = "";
            sqlStr = " select convert(NVarChar(20),D.[OrderDate],23) as [OrderDate],A.[OrderNo],d.invioceno,A.[ProCatalogNo],A.[ProDescription],A.[ProSize],A.[ProUnit],A.[ProQuantity],A.[ProAmount],A.[ProCurrency],convert(NVarChar(20),A.[ProDunOn],23) as [ProDunOn],A.[ProNote],A.[ProLibraryID],A.[ProductStatus],a.StockStatus,";
            sqlStr += " cast(isnull(B.[WeightG],0) as decimal(18,4)) as 'Shipped(g)',cast(isnull(C.[WeightG],0) as decimal(18,4)) as 'In Stock(g)'  ,convert(NVarChar(20),A.[TaskTime],23) as [TaskTime],A.[ID] ";
            //    sqlStr += "  ,sumOrderG,case when sumOrderG>cast(isnull(B.[WeightG],0) as decimal(18,4))+cast(isnull(C.[WeightG],0) as decimal(18,4))    then  1 else 0   end Bz ";
            sqlStr += " ,case when  charindex('mm' , [ProUnit])>0 then '3'  when   case when [ProUnit] = 'ug' then 0.000001 when [ProUnit] = 'mg' then 0.001 when [ProUnit] = 'g' then 1 when [ProUnit] = 'kg' then 1000 else 1 end*[ProSize]*[ProQuantity]>cast(isnull(B.[WeightG],0) as decimal(18,4))+cast(isnull(C.[WeightG],0) as decimal(18,4)) then '1'   else '0' end Bz,a.[ProNote],e.CAS";
            sqlStr += " from [MCEOrderProInfo] A left join  [MCEUSA].[dbo].[MCEProductsBasicinfo] e on a.ProCatalogNo=e.CSNo  Left Join (   select [OrginalID],sum([WeightG]) as [WeightG]   From  (";
            sqlStr += "         select [ID],[OrginalID],[ShipSize],[ShipUnit],";
            sqlStr += "         (case when [ShipUnit] = 'ug' then 0.000001 when [ShipUnit] = 'mg' then 0.001 when [ShipUnit] = 'g' then 1 when [ShipUnit] = 'kg' then 1000 else 1 end)*[ShipSize] as [WeightG]";
            sqlStr += "          From [CSShipProInfo]   )M";
            sqlStr += "     group by m.[OrginalID] )B on A.[ID] = B.[OrginalID] Left Join (";
            sqlStr += "     select [CSNo],stocksize,StockUnit,sum((case when [StockUnit] = 'ug' then 0.000001 when [StockUnit] = 'mg' then 0.001 when [StockUnit] = 'g' then 1 when [StockUnit] = 'kg' then 1000 else 1 end)*cast(stocksize as float)) as [WeightG]";
            sqlStr += "     From [CSStock]";
            sqlStr += "     group by [CSNo],stocksize,StockUnit";
            sqlStr += " )C on A.[ProCatalogNo] = C.[CSNo] and A.prosize = C.stocksize  and  a.ProUnit=c.StockUnit Left Join (";
            sqlStr += "     select [OrderDate],[OrderNo],[ShipStatus],invioceno";
            sqlStr += "     From [MCEOrderInfo]";
            sqlStr += " )D on A.[OrderNo] = D.[OrderNo]";
            //   sqlStr += "  left join (select  case when [ProUnit] = 'ug' then 0.000001 when [ProUnit] = 'mg' then 0.001 when [ProUnit] = 'g' then 1 when [ProUnit] = 'kg' then 1000 else 1 end*[ProSize]*[ProQuantity]";
            //    sqlStr += "  as sumOrderG,OrderNo from [MCEOrderProInfo] ) mm on A.[OrderNo] = mm.[OrderNo]";
            sqlStr += " where 1 = 1";
            sqlStr += " and [ShipStatus] = 'Unshipped' and [ProductStatus] = 'OK'";
            if (txtNote.Text != string.Empty)
            {
                sqlStr += string.Format(" and A.[ProNote] like '%{0}%'", txtNote.Text);
            }
            if (txtCaNo.Text != string.Empty)
            {
                sqlStr += string.Format(" and A.[ProCatalogNo] like '%{0}%'", txtCaNo.Text);
            }
            if (txtOrderNo.Text != "")
            {
                sqlStr += string.Format(" and d.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr += string.Format(" and  d.PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr += string.Format(" and d.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }
            if (txtDate1.Text != "")
            {
                sqlStr += string.Format(" and  CONVERT(nvarchar(20),OrderDate,23)>='{0}' ", Convert.ToDateTime(txtDate1.Text).ToString("yyyy-MM-dd"));
            }

            if (txtDate2.Text != "")
            {
                sqlStr += string.Format(" and  CONVERT(nvarchar(20),OrderDate,23)<='{0}'", Convert.ToDateTime(txtDate2.Text).ToString("yyyy-MM-dd"));
            }
            sqlStr += "order by A.[ID] desc";

            dt = Maticsoft.DBUtility.DbHelperSQL.Query(sqlStr).Tables[0];
           // labTotal.Text = "Total: " + dt.Compute("sum(ProAmount)", "").ToString();
            labCount.Text = dt.Rows.Count.ToString();

            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
            dtid.Clear();
        }

        private void butAll_Click(object sender, EventArgs e)
        {
            sqlStr = "";
            sqlStr = " select convert(NVarChar(20),D.[OrderDate],23) as [OrderDate],A.[OrderNo],d.invioceno,A.[ProCatalogNo],A.[ProDescription],A.[ProSize],A.[ProUnit],A.[ProQuantity],A.[ProAmount],A.[ProCurrency],convert(NVarChar(20),A.[ProDunOn],23) as [ProDunOn],A.[ProNote],A.[ProLibraryID],A.[ProductStatus],a.StockStatus,";
            sqlStr += " cast(isnull(B.[WeightG],0) as decimal(18,4)) as 'Shipped(g)',cast(isnull(C.[WeightG],0) as decimal(18,4)) as 'In Stock(g)'  ,convert(NVarChar(20),A.[TaskTime],23) as [TaskTime],A.[ID] ";
            //   sqlStr += " ,sumOrderG,case when sumOrderG>cast(isnull(B.[WeightG],0) as decimal(18,4))+cast(isnull(C.[WeightG],0) as decimal(18,4))    then  1 else 0   end Bz ";
            sqlStr += " ,case when  charindex('mm' , [ProUnit])>0 then '3'  when   case when [ProUnit] = 'ug' then 0.000001 when [ProUnit] = 'mg' then 0.001 when [ProUnit] = 'g' then 1 when [ProUnit] = 'kg' then 1000 else 1 end*[ProSize]*[ProQuantity]>cast(isnull(B.[WeightG],0) as decimal(18,4))+cast(isnull(C.[WeightG],0) as decimal(18,4)) then '1'   else '0' end Bz,a.[ProNote],e.CAS";
            sqlStr += " from [MCEOrderProInfo] A left join  [MCEUSA].[dbo].[MCEProductsBasicinfo] e on a.ProCatalogNo=e.CSNo  Left Join (   select [OrginalID],sum([WeightG]) as [WeightG]   From  (";
            sqlStr += "         select [ID],[OrginalID],[ShipSize],[ShipUnit],";
            sqlStr += "         (case when [ShipUnit] = 'ug' then 0.000001 when [ShipUnit] = 'mg' then 0.001 when [ShipUnit] = 'g' then 1 when [ShipUnit] = 'kg' then 1000 else 1 end)*[ShipSize] as [WeightG]";
            sqlStr += "          From [CSShipProInfo]   )M";
            sqlStr += "     group by m.[OrginalID] )B on A.[ID] = B.[OrginalID] Left Join (";
            sqlStr += "     select [CSNo],stocksize,StockUnit,sum((case when [StockUnit] = 'ug' then 0.000001 when [StockUnit] = 'mg' then 0.001 when [StockUnit] = 'g' then 1 when [StockUnit] = 'kg' then 1000 else 1 end)*cast(stocksize as float)) as [WeightG]";
            sqlStr += "     From [CSStock]";
            sqlStr += "     group by [CSNo],stocksize,StockUnit";
            sqlStr += " )C on A.[ProCatalogNo] = C.[CSNo] and A.prosize = C.stocksize  and  a.ProUnit=c.StockUnit Left Join (";
            sqlStr += "     select [OrderDate],[OrderNo],[ShipStatus],invioceno";
            sqlStr += "     From [MCEOrderInfo]";
            sqlStr += " )D on A.[OrderNo] = D.[OrderNo]";
            //  sqlStr += "  left join (select  case when [ProUnit] = 'ug' then 0.000001 when [ProUnit] = 'mg' then 0.001 when [ProUnit] = 'g' then 1 when [ProUnit] = 'kg' then 1000 else 1 end*[ProSize]*[ProQuantity]";
            //  sqlStr += "  as sumOrderG,OrderNo from [MCEOrderProInfo] ) mm on A.[OrderNo] = mm.[OrderNo]";
            sqlStr += " where 1 = 1";

            if (txtNote.Text != string.Empty)
            {
                sqlStr += string.Format(" and A.[ProNote] like '%{0}%'", txtNote.Text);
            }
            if (txtCaNo.Text != string.Empty)
            {
                sqlStr += string.Format(" and A.[ProCatalogNo] like '%{0}%'", txtCaNo.Text);
            }
            if (txtOrderNo.Text != "")
            {
                sqlStr += string.Format(" and d.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr += string.Format(" and  d.PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr += string.Format(" and d.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }
            if (txtDate1.Text != "")
            {
                sqlStr += string.Format(" and  CONVERT(nvarchar(20),OrderDate,23)>='{0}' ", Convert.ToDateTime(txtDate1.Text).ToString("yyyy-MM-dd"));
            }

            if (txtDate2.Text != "")
            {
                sqlStr += string.Format(" and  CONVERT(nvarchar(20),OrderDate,23)<='{0}'", Convert.ToDateTime(txtDate2.Text).ToString("yyyy-MM-dd"));
            }
            sqlStr += "order by A.[ID] desc";

            dt = Maticsoft.DBUtility.DbHelperSQL.Query(sqlStr).Tables[0];
           // labTotal.Text = "Total: " + dt.Compute("sum(ProAmount)", "").ToString();
            labCount.Text = dt.Rows.Count.ToString();

            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
            dtid.Clear();
        }

        private void btuTask_Click(object sender, EventArgs e)
        {
            sqlStr = "";
            sqlStr = " select convert(NVarChar(20),D.[OrderDate],23) as [OrderDate],A.[OrderNo],d.invioceno,A.[ProCatalogNo],A.[ProDescription],A.[ProSize],A.[ProUnit],A.[ProQuantity],A.[ProAmount],A.[ProCurrency],convert(NVarChar(20),A.[ProDunOn],23) as [ProDunOn],A.[ProNote],A.[ProLibraryID],A.[ProductStatus],a.StockStatus,";
            sqlStr += " cast(isnull(B.[WeightG],0) as decimal(18,4)) as 'Shipped(g)',cast(isnull(C.[WeightG],0) as decimal(18,4)) as 'In Stock(g)'  ,convert(NVarChar(20),A.[TaskTime],23) as [TaskTime],A.[ID] ";
            //  sqlStr += "  ,sumOrderG,case when sumOrderG>cast(isnull(B.[WeightG],0) as decimal(18,4))+cast(isnull(C.[WeightG],0) as decimal(18,4))    then  1 else 0   end Bz ";
            sqlStr += " ,case when  charindex('mm' , [ProUnit])>0 then '3'  when   case when [ProUnit] = 'ug' then 0.000001 when [ProUnit] = 'mg' then 0.001 when [ProUnit] = 'g' then 1 when [ProUnit] = 'kg' then 1000 else 1 end*[ProSize]*[ProQuantity]>cast(isnull(B.[WeightG],0) as decimal(18,4))+cast(isnull(C.[WeightG],0) as decimal(18,4)) then '1'   else '0' end Bz,a.[ProNote]";
            sqlStr += " from [MCEOrderProInfo] A  Left Join (   select [OrginalID],sum([WeightG]) as [WeightG]   From  (";
            sqlStr += "         select [ID],[OrginalID],[ShipSize],[ShipUnit],";
            sqlStr += "         (case when [ShipUnit] = 'ug' then 0.000001 when [ShipUnit] = 'mg' then 0.001 when [ShipUnit] = 'g' then 1 when [ShipUnit] = 'kg' then 1000 else 1 end)*[ShipSize] as [WeightG]";
            sqlStr += "          From [CSShipProInfo]   )M";
            sqlStr += "     group by m.[OrginalID] )B on A.[ID] = B.[OrginalID] Left Join (";
            sqlStr += "     select [CSNo],stocksize,StockUnit,sum((case when [StockUnit] = 'ug' then 0.000001 when [StockUnit] = 'mg' then 0.001 when [StockUnit] = 'g' then 1 when [StockUnit] = 'kg' then 1000 else 1 end)*cast(stocksize as float)) as [WeightG]";
            sqlStr += "     From [CSStock]";
            sqlStr += "     group by [CSNo],stocksize,StockUnit";
            sqlStr += " )C on A.[ProCatalogNo] = C.[CSNo] and A.prosize = C.stocksize  and  a.ProUnit=c.StockUnit  Left Join (";
            sqlStr += "     select [OrderDate],[OrderNo],[ShipStatus],invioceno";
            sqlStr += "     From [MCEOrderInfo]";
            sqlStr += " )D on A.[OrderNo] = D.[OrderNo]";
            // sqlStr += "  left join (select  case when [ProUnit] = 'ug' then 0.000001 when [ProUnit] = 'mg' then 0.001 when [ProUnit] = 'g' then 1 when [ProUnit] = 'kg' then 1000 else 1 end*[ProSize]*[ProQuantity]";
            //sqlStr += "  as sumOrderG,OrderNo from [MCEOrderProInfo] ) mm on A.[OrderNo] = mm.[OrderNo]";
            sqlStr += " where 1 = 1";
            sqlStr += " and [ShipStatus] = 'Unshipped' and [ProductStatus] = 'OK' and isnull(A.[TaskTime],'') <> ''";


            if (txtNote.Text != string.Empty)
            {
                sqlStr += string.Format(" and A.[ProNote] like '%{0}%'", txtNote.Text);
            }
            if (txtCaNo.Text != string.Empty)
            {
                sqlStr += string.Format(" and A.[ProCatalogNo] like '%{0}%'", txtCaNo.Text);
            }
            if (txtOrderNo.Text != "")
            {
                sqlStr += string.Format(" and d.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr += string.Format(" and  d.PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr += string.Format(" and d.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }
            if (txtDate1.Text != "")
            {
                sqlStr += string.Format(" and  CONVERT(nvarchar(20),OrderDate,23)>='{0}' ", Convert.ToDateTime(txtDate1.Text).ToString("yyyy-MM-dd"));
            }

            if (txtDate2.Text != "")
            {
                sqlStr += string.Format(" and  CONVERT(nvarchar(20),OrderDate,23)<='{0}'", Convert.ToDateTime(txtDate2.Text).ToString("yyyy-MM-dd"));
            }
            sqlStr += "order by A.[ID] desc";

            dt = Maticsoft.DBUtility.DbHelperSQL.Query(sqlStr).Tables[0];
           // labTotal.Text = "Total: " + dt.Compute("sum(ProAmount)", "").ToString();
            labCount.Text = dt.Rows.Count.ToString();

            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
            dtid.Clear();
        }

        private void btuTaskAll_Click(object sender, EventArgs e)
        {
            sqlStr = "";
            sqlStr = " select convert(NVarChar(20),D.[OrderDate],23) as [OrderDate],A.[OrderNo],d.invioceno,A.[ProCatalogNo],A.[ProDescription],A.[ProSize],A.[ProUnit],A.[ProQuantity],A.[ProAmount],A.[ProCurrency],convert(NVarChar(20),A.[ProDunOn],23) as [ProDunOn],A.[ProNote],A.[ProLibraryID],A.[ProductStatus],a.StockStatus,";
            sqlStr += " cast(isnull(B.[WeightG],0) as decimal(18,4)) as 'Shipped(g)',cast(isnull(C.[WeightG],0) as decimal(18,4)) as 'In Stock(g)'  ,convert(NVarChar(20),A.[TaskTime],23) as [TaskTime],A.[ID] ";
            // sqlStr += " ,sumOrderG,case when sumOrderG>cast(isnull(B.[WeightG],0) as decimal(18,4))+cast(isnull(C.[WeightG],0) as decimal(18,4))    then  1 else 0   end Bz ";
            sqlStr += " ,case when  charindex('mm' , [ProUnit])>0 then '3'  when   case when [ProUnit] = 'ug' then 0.000001 when [ProUnit] = 'mg' then 0.001 when [ProUnit] = 'g' then 1 when [ProUnit] = 'kg' then 1000 else 1 end*[ProSize]*[ProQuantity]>cast(isnull(B.[WeightG],0) as decimal(18,4))+cast(isnull(C.[WeightG],0) as decimal(18,4)) then '1' else '0'  end Bz,a.[ProNote],e.CAS";
            sqlStr += " from [MCEOrderProInfo] A left join  [MCEUSA].[dbo].[MCEProductsBasicinfo] e on a.ProCatalogNo=e.CSNo  Left Join (   select [OrginalID],sum([WeightG]) as [WeightG]   From  (";
            sqlStr += "         select [ID],[OrginalID],[ShipSize],[ShipUnit],";
            sqlStr += "         (case when [ShipUnit] = 'ug' then 0.000001 when [ShipUnit] = 'mg' then 0.001 when [ShipUnit] = 'g' then 1 when [ShipUnit] = 'kg' then 1000 else 1 end)*[ShipSize] as [WeightG]";
            sqlStr += "          From [CSShipProInfo]   )M";
            sqlStr += "     group by m.[OrginalID] )B on A.[ID] = B.[OrginalID] Left Join (";
            sqlStr += "     select [CSNo],stocksize,StockUnit,sum((case when [StockUnit] = 'ug' then 0.000001 when [StockUnit] = 'mg' then 0.001 when [StockUnit] = 'g' then 1 when [StockUnit] = 'kg' then 1000 else 1 end)*cast(stocksize as float)) as [WeightG]";
            sqlStr += "     From [CSStock]";
            sqlStr += "     group by [CSNo],stocksize,StockUnit";
            sqlStr += " )C on A.ProCatalogNo = C.[CSNo] and A.prosize = C.stocksize  and  a.ProUnit=c.StockUnit Left Join (";
            sqlStr += "     select [OrderDate],[OrderNo],[ShipStatus],invioceno";
            sqlStr += "     From [MCEOrderInfo]";
            sqlStr += " )D on A.[OrderNo] = D.[OrderNo]";
            //  sqlStr += "  left join (select  case when [ProUnit] = 'ug' then 0.000001 when [ProUnit] = 'mg' then 0.001 when [ProUnit] = 'g' then 1 when [ProUnit] = 'kg' then 1000 else 1 end*[ProSize]*[ProQuantity]";
            //  sqlStr += "  as sumOrderG,OrderNo from [MCEOrderProInfo] ) mm on A.[OrderNo] = mm.[OrderNo]";
            sqlStr += " where 1 = 1";
            sqlStr += "  and isnull(A.[TaskTime],'') <> ''";


            if (txtNote.Text != string.Empty)
            {
                sqlStr += string.Format(" and A.[ProNote] like '%{0}%'", txtNote.Text);
            }
            if (txtCaNo.Text != string.Empty)
            {
                sqlStr += string.Format(" and A.[ProCatalogNo] like '%{0}%'", txtCaNo.Text);
            }
            if (txtOrderNo.Text != "")
            {
                sqlStr += string.Format(" and d.orderno like '%{0}%' ", txtOrderNo.Text);
            }
            if (txtPONo.Text != "")
            {
                sqlStr += string.Format(" and  d.PONumber like '%{0}%' ", txtPONo.Text);
            }
            if (txtInvoiceNo.Text != "")
            {
                sqlStr += string.Format(" and d.InvioceNo  like '%{0}%'  ", txtInvoiceNo.Text);
            }
            if (txtDate1.Text != "")
            {
                sqlStr += string.Format(" and  CONVERT(nvarchar(20),OrderDate,23)>='{0}' ", Convert.ToDateTime(txtDate1.Text).ToString("yyyy-MM-dd"));
            }

            if (txtDate2.Text != "")
            {
                sqlStr += string.Format(" and  CONVERT(nvarchar(20),OrderDate,23)<='{0}'", Convert.ToDateTime(txtDate2.Text).ToString("yyyy-MM-dd"));
            }
            sqlStr += "order by A.[ID] desc";

            dt = Maticsoft.DBUtility.DbHelperSQL.Query(sqlStr).Tables[0];
          //  labTotal.Text = "Total: " + dt.Compute("sum(ProAmount)", "").ToString();
            labCount.Text = dt.Rows.Count.ToString();

            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
            dtid.Clear();
        }

        private double getUnit(string LType)
        {
            double unitS = 0.00;
            switch (LType.ToLower())
            {
                case "ug":
                    unitS = 0.000001;
                    break;
                case "mg":
                    unitS = 0.001;
                    break;
                case "g":
                    unitS = 1;
                    break;
                case "kg":
                    unitS = 1000;
                    break;
            }
            return unitS;
        }


        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            string status;
            string unitS = "ugmggkg";
            if (e.RowHandle > -1)
            {
                status = gridView1.GetRowCellValue(e.RowHandle, "ProductStatus").ToString();
                double sumOrderG = Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "In Stock(g)"));
                double ShippedG = Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "Shipped(g)"));

                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(e.RowHandle, "ID"));

                DataRow[] selectedAddRows = dtAddid.Select("ID=" + id + "");
                DataRow[] selectedRows = dtid.Select("ID=" + id + "");
                if (getUnit(gridView1.GetRowCellValue(e.RowHandle, "ProUnit").ToString()) * Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "ProSize")) * Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "ProQuantity")) <= ShippedG)
                {
                    e.Appearance.BackColor = Color.Lime;
                }
                //if (getUnit(gridView1.GetRowCellValue(e.RowHandle, "ProUnit").ToString()) * Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "ProSize")) * Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "ProQuantity")) > sumOrderG + ShippedG)
                //{


                //    if (selectedRows == null || selectedRows.Length <= 0)
                //    {
                //        e.Appearance.BackColor = Color.Goldenrod;
                //    }
                //    else
                //    {
                //        e.Appearance.BackColor = Color.White;
                //    }

                //}


                if (gridView1.GetRowCellValue(e.RowHandle, "Bz").ToString() == "1")
                {


                    if (selectedRows == null || selectedRows.Length <= 0)
                    {
                        e.Appearance.BackColor = Color.Goldenrod;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.White;
                    }

                }
                if (gridView1.GetRowCellValue(e.RowHandle, "Bz").ToString() == "3")
                {
                    e.Appearance.BackColor = Color.LightCyan;
                }

                //if (unitS.IndexOf(gridView1.GetRowCellValue(e.RowHandle, "ProUnit").ToString()) < 0)
                //{
                //    e.Appearance.BackColor = Color.LightCyan;
                //}

                if (status == "Cancel")
                {
                    e.Appearance.BackColor = Color.LightSlateGray;
                }
                if (gridView1.GetRowCellValue(e.RowHandle, "ProDunOn").ToString() != "")
                {
                    if (Convert.ToDateTime(gridView1.GetRowCellValue(e.RowHandle, "ProDunOn")) < DateTime.Now)
                    {
                        //e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.Red;
                    }
                }


                if (selectedAddRows.Length > 0)
                {
                    e.Appearance.BackColor = Color.Goldenrod;
                }

                //if (gridView1.GetRowCellValue(e.RowHandle, "TaskTime").ToString() != "")
                //{
                //    gridView1.Columns["OrderDate"].DefaultCellStyle.BackColor = Color.Yellow;
                //    e.Appearance.BackColor = Color.MediumSlateBlue;

                //}


            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            if (e.RowHandle > -1)
            {

                if (gridView1.GetRowCellValue(e.RowHandle, "TaskTime").ToString() != "")
                {
                    if (e.Column.ColumnHandle == 0)
                    {
                        e.Appearance.BackColor = Color.MediumSlateBlue;
                    }

                }
                if (gridView1.GetRowCellValue(e.RowHandle, "StockStatus") != null)
                {
                    if (gridView1.GetRowCellValue(e.RowHandle, "StockStatus").ToString() != string.Empty)
                    {
                        if (e.Column.ColumnHandle == 1)
                        {
                            e.Appearance.BackColor = Color.Aquamarine;
                        }
                    }

                }

                //double sumOrderG = Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "In Stock(g)"));
                //double ShippedG = Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "Shipped(g)"));



                //if (getUnit(gridView1.GetRowCellValue(e.RowHandle, "ProUnit").ToString()) * Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "ProSize")) * Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "ProQuantity")) > sumOrderG + ShippedG)
                //{
                //    if (e.Column.ColumnHandle == 0)
                //    {

                //        int id = Convert.ToInt32(this.gridView1.GetRowCellValue(e.RowHandle, "ID"));

                //        DataRow[] selectedRows = dt.Select("id=" + id + "");
                //        if (selectedRows != null && selectedRows.Length > 0)
                //        {


                //            selectedRows[0]["Bz"] = "1";
                //            // dt.AcceptChanges();

                //        }
                //    }

                //}
            }
        }

        private void btuExcel_Click(object sender, EventArgs e)
        {


            //DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
            //gridControl1.ExportToExcelOld(saveFileDialog.FileName);
          //  dt.Columns.Remove("Bz");
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.FileName = "Order Products List.xlsx";
            fileDialog.Title = "Excel";
            fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                string error = "";
                WHC.Framework.ControlUtil.AsposeExcelTools.DataTableToExcel2(dt, fileDialog.FileName, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    MessageDxUtil.ShowError(string.Format("error：{0}", error));
                }
                else
                {
                    if (MessageDxUtil.ShowYesNoAndTips("success,open？") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(fileDialog.FileName);
                    }
                }
            }

        }

        private void btuExcelY_Click(object sender, EventArgs e)
        {
            sqlwhere = "";
            sqlwhereAdd = "";
            for (int i = 0; i < dtid.Rows.Count; i++)
            {

                sqlwhere = sqlwhere + dtid.Rows[i]["ID"].ToString() + ",";


            }
            if (sqlwhere != "")
            {
                sqlwhere = " and id not in ( " + sqlwhere.Substring(0, sqlwhere.Length - 1) + " )";
            }

            for (int i = 0; i < dtAddid.Rows.Count; i++)
            {

                sqlwhereAdd = sqlwhereAdd + dtAddid.Rows[i]["ID"].ToString() + ",";


            }
            if (sqlwhereAdd != "")
            {
                sqlwhereAdd = " or id  in ( " + sqlwhereAdd.Substring(0, sqlwhereAdd.Length - 1) + " )";
            }


            sqlStr = sqlStr.Replace("order by A.[ID] desc", "");
            sqlStr = sqlStr + " and [ProUnit] in ('ug','mg','kg','g') and  case when [ProUnit] = 'ug' then 0.000001*[ProQuantity]*[ProSize] when [ProUnit] = 'mg' then 0.001*[ProQuantity]*[ProSize] when [ProUnit] = 'g' ";
            sqlStr = sqlStr + " then 1*[ProQuantity]*[ProSize] when [ProUnit] = 'kg' then 1000*[ProQuantity]*[ProSize]  end >cast(isnull(B.[WeightG],0) as decimal(18,4))+cast(isnull(C.[WeightG],0) as decimal(18,4))";
            // sqlStr += " order by A.[ID] desc";
            DataTable dtNew = Maticsoft.DBUtility.DbHelperSQL.Query(sqlStr + " " + sqlwhere + " " + sqlwhereAdd).Tables[0];
            if (checkTask.Checked)
            {
                var strsql = " update  MCEOrderProInfo set tasktime=getdate()  where id in ( select id from  ( " + sqlStr + "  ) a ) " + sqlwhere + " " + sqlwhereAdd;
                if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(strsql) > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dtNew.Select("id=" + dt.Rows[i]["id"] + "").Length > 0)
                        {
                            dt.Rows[i]["tasktime"] = DateTime.Now.ToShortDateString();
                        }

                    }
                    dt.AcceptChanges();
                }

            }





         //   dtNew.Columns.Remove("Bz");


            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.FileName = "Order Products List.xlsx";
            fileDialog.Title = "Excel";
            fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                string error = "";
                WHC.Framework.ControlUtil.AsposeExcelTools.DataTableToExcel2(dtNew, fileDialog.FileName, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    MessageDxUtil.ShowError(string.Format("error：{0}", error));
                }
                else
                {
                    if (MessageDxUtil.ShowYesNoAndTips("success,open？") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(fileDialog.FileName);
                    }
                }
            }

            //sqlwhere = "";
            //for (int i = 0; i < dtid.Rows.Count; i++)
            //{

            //    sqlwhere = sqlwhere + dtid.Rows[i]["ID"].ToString() + ",";


            //}
            //if (sqlwhere != "")
            //{
            //    sqlwhere = " and id not in ( " + sqlwhere.Substring(0, sqlwhere.Length - 1) + " )";
            //}



            //sqlStr = sqlStr.Replace("order by A.[ID] desc", "");
            //sqlStr = sqlStr + " and [ProUnit] in ('ug','mg','kg','g') and  case when [ProUnit] = 'ug' then 0.000001*[ProQuantity]*[ProSize] when [ProUnit] = 'mg' then 0.001*[ProQuantity]*[ProSize] when [ProUnit] = 'g' ";
            //sqlStr = sqlStr + " then 1*[ProQuantity]*[ProSize] when [ProUnit] = 'kg' then 1000*[ProQuantity]*[ProSize]  end >cast(isnull(B.[WeightG],0) as decimal(18,4))+cast(isnull(C.[WeightG],0) as decimal(18,4))";
            //// sqlStr += " order by A.[ID] desc";
            //DataTable dtNew = Maticsoft.DBUtility.DbHelperSQL.Query(sqlStr + " " + sqlwhere).Tables[0];
            //if (checkTask.Checked)
            //{
            //    var strsql = " update  MCEOrderProInfo set tasktime=getdate()  where id in ( select id from  ( " + sqlStr + "  ) a ) " + sqlwhere;
            //    if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(strsql) > 0)
            //    {

            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            if (dtNew.Select("id=" + dt.Rows[i]["id"] + "").Length > 0)
            //            {
            //                dt.Rows[i]["tasktime"] = DateTime.Now.ToShortDateString();
            //            }

            //        }
            //        dt.AcceptChanges();
            //    }

            //}


            //string filename = NPOIHelper.ExportToExcel(dtNew, "Order Products List");
            //if (!string.IsNullOrEmpty(filename))
            //    MessageDxUtil.ShowTips("success");
        }

        private void FrmOrderProductsInfor_Load(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ID"));

                //   var taskTime = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "TaskTime") == "" ? DateTime.Now.ToShortDateString() : null;

                DataRow[] selectedRows = dt.Select("id=" + id + "");
                if (selectedRows != null && selectedRows.Length > 0)
                {
                    string sqlStr;
                    if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "TaskTime").ToString() == "")
                        sqlStr = "update MCEOrderProInfo set TaskTime=getdate() where id=" + id + " ";
                    else
                        sqlStr = "update MCEOrderProInfo set TaskTime=null where id=" + id + " ";
                    if (Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(sqlStr) > 0)
                    {
                        // selectedRows[0]["Bz"] = "1";
                        if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "TaskTime").ToString() == "")
                            selectedRows[0]["TaskTime"] = DateTime.Now.ToString("yyyy-MM-dd");
                        else
                            selectedRows[0]["TaskTime"] = "";


                        dt.AcceptChanges();
                        //gridControl1.DataSource = dt;
                        //gridControl1.RefreshDataSource();
                    }



                }
            }
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            txtNote.Text = "";
            txtCaNo.Text = "";
            txtOrderNo.Text = "";
            txtPONo.Text = "";
            txtInvoiceNo.Text = "";
            txtDate1.Text = "";
            txtDate2.Text = "";
        }

        private void FrmOrderProductsInfor_KeyDown(object sender, KeyEventArgs e)
        {
          
                if (e.KeyCode == Keys.F2)
                {
                    if (this.gridView1.FocusedRowHandle >= 0)
                    {




                        int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ID"));
                        DataRow[] selectedRowsID = null;
                        if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "Bz").ToString() == "1")
                        {
                            selectedRowsID = dtid.Select("id=" + id + "");
                            if (selectedRowsID != null && selectedRowsID.Length > 0)
                            {

                                foreach (DataRow row in selectedRowsID) { dtid.Rows.Remove(row); }

                                // dt.AcceptChanges();

                            }
                            else
                            {
                                DataRow _drNewid = dtid.NewRow();
                                _drNewid["ID"] = id;
                                dtid.Rows.Add(_drNewid);
                            }

                        }
                        else
                        {

                            selectedRowsID = dtAddid.Select("id=" + id + "");
                            if (selectedRowsID != null && selectedRowsID.Length > 0)
                            {
                                foreach (DataRow row in selectedRowsID) { dtAddid.Rows.Remove(row); }
                            }
                            else
                            {

                                DataRow _drNewid = dtAddid.NewRow();
                                _drNewid["ID"] = id;
                                dtAddid.Rows.Add(_drNewid);

                            }

                        }

                        DataRow[] selectedRows = dt.Select("id=" + id + "");
                        if (selectedRows != null && selectedRows.Length > 0)
                        {


                            selectedRows[0]["tasktime"] = selectedRows[0]["tasktime"];
                            // dt.AcceptChanges();

                        }






                    }
                }
                //if (this.gridView1.FocusedRowHandle >= 0)
                //{
                //    int id = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ID"));

                //    DataRow _drNewid = dtid.NewRow();
                //    _drNewid["ID"] = id;
                //    dtid.Rows.Add(_drNewid);


                //    DataRow[] selectedRows = dt.Select("id=" + id + "");
                //    if (selectedRows != null && selectedRows.Length > 0)
                //    {


                //        selectedRows[0]["tasktime"] = selectedRows[0]["tasktime"];
                //        // dt.AcceptChanges();

                //    }
                //}
            
        }


    }
}
