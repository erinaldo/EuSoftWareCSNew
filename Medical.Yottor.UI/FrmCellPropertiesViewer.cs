using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Spreadsheet;
using System.IO;
using Maticsoft.DBUtility;
namespace Medical.Yottor.UI
{
    public partial class FrmCellPropertiesViewer : Form
    {
        public string savePath = "";
        public int saveType;
        public string saverOrderNo = "";
        public string saverinvioceNO = "";
        public FrmCellPropertiesViewer(string sPath, string orderNo, string trNo, int type, string invioceNO, bool IsB = true)
        {



            InitializeComponent();
            labelControl1.Text = sPath;
            saveType = type;
            InitializeWorkbook(sPath, orderNo, trNo, type, invioceNO, IsB);
            saverOrderNo = orderNo;
            saverinvioceNO = invioceNO;

        }

        private void FrmCellPropertiesViewer_Load(object sender, EventArgs e)
        {


        }
        void InitializeWorkbook(string sPath, string orderNo, string trNo, int type, string invioceNO, bool IsB = true)
        {
            IWorkbook workbook = spreadsheetControl1.Document;
            //sPath = @"D:\temp\Invoice#MCE172599_Karolinska Institutet_PO#123456.xlsx";
            workbook.LoadDocument(@sPath);
            //int wCount = workbook.Worksheets.Count;
            //for (int i = 0; i < wCount; i++)
            //{
            //    if (i > 0)
            //    {
            //        workbook.Worksheets.RemoveAt(i);
            //    }

            //}

            //workbook.Worksheets.RemoveAt(1);
            //workbook.Worksheets.Remove(workbook.Worksheets["Sheet3"]);
            if (type == 0) //发票
            {
                Print(workbook, orderNo, trNo, invioceNO, IsB);
            }
            else if (type == 1)
            {
                PrintZxd(workbook, orderNo);
            }



            //workbook.Unit = DevExpress.Office.DocumentUnit.Inch;
            //// Access page margins.  
            Margins pageMargins = workbook.Worksheets[0].ActiveView.Margins;
            //// Specify page margins.  
          //  pageMargins.Left = 0.8f;

        }

        private void PrintZxd(IWorkbook workbook, string orderNo)
        {
            //#region WorksheetPrintOptions
            Worksheet worksheet = workbook.Worksheets[0];
            DataTable dtMCEOrderProInfo = new DataTable();
            string sql = "select isnull(a.OrderNo,'') as OrderNo,a.TrackingNo,a.ShipCatalogNo,a.ShipSize,a.ShipUnit,c.ProCatalogNo,c.ProDescription,c.ProSize,c.ProUnit,c.ProQuantity,d.PONumber from MCEShipProInfo a inner join MCEOrderProInfo c on a.OrginalID =c.id inner join MCEOrderInfo d on c.OrderNo=d.OrderNo where a.TrackingNo = '" + orderNo + "' and c.StockStatus='' order by a.id";
            savePath = "PackingList#MCE" + orderNo + ".xlsx";

            dtMCEOrderProInfo = DbHelperSQL.Query(sql).Tables[0];
            int N_id = 0;
            int dd = 0;
            int Y_EXCEL = 17;                                                              //0 起点
            double Subtotal = 0.00;
            if (dtMCEOrderProInfo.Rows.Count > 0)
            {
                for (int i = 0; i < dtMCEOrderProInfo.Rows.Count; i++)
                {


                    if (Y_EXCEL + N_id + dd > 17)
                    {
                        worksheet.Rows.Insert(Y_EXCEL + N_id + dd);
                        worksheet.Rows[Y_EXCEL + N_id + dd].CopyFrom(worksheet.Rows[Y_EXCEL + N_id + dd - 1], PasteSpecial.Formats);
                        worksheet.Rows[Y_EXCEL + N_id + dd].Borders.RemoveBorders();
                    }
                    worksheet.Rows[Y_EXCEL + N_id + dd][0].Value = N_id + 1;
                    worksheet.Rows[Y_EXCEL + N_id + dd][1].Value = dtMCEOrderProInfo.Rows[i]["ShipCatalogNo"].ToString();
                    worksheet.Rows[Y_EXCEL + N_id + dd][3].Value = dtMCEOrderProInfo.Rows[i]["ProDescription"].ToString();
                    worksheet.Rows[Y_EXCEL + N_id + dd][16].Value = "1";
                    worksheet.Rows[Y_EXCEL + N_id + dd][11].Value = dtMCEOrderProInfo.Rows[i]["ShipSize"].ToString() + " " + dtMCEOrderProInfo.Rows[i]["ShipUnit"].ToString();


                    dd = dd + cellCut(worksheet, Y_EXCEL + N_id + dd, 1, 25, true);

                    N_id = N_id + 1;

                }
                N_id = N_id + dd;
            }

            int Snum = 0;
            if (N_id > 1)
                Snum = Snum + N_id - 1;
            worksheet.Rows[18 + Snum][0].Value = N_id + 1;
            worksheet.Rows[19 + Snum][16].Value = (N_id + 1).ToString();


            DataTable dt = new DataTable();




            sql = "select * from MCEOrderInfo a left join MCEShipmentInfo b on a.OrderNo=b.OrderNo where a.OrderNo = '" + dtMCEOrderProInfo.Rows[0]["OrderNo"].ToString() + "'";
            dt = DbHelperSQL.Query(sql).Tables[0];

            dd = 0;
            int Snums = 0;
            worksheet.Rows[6][7].Value = dt.Rows[0]["ShipCompany"].ToString();
            dd = cellCut(worksheet, 6, 7, 45, true);
            Snums = Snums + dd;
            worksheet.Rows[7 + Snums][8].Value = dt.Rows[0]["ShipContactName"].ToString();
            dd = cellCut(worksheet, 7 + Snums, 8, 24, true);
            Snums = Snums + dd;
            worksheet.Rows[8 + Snums][7].Value = dt.Rows[0]["ShipStreet"].ToString();
            dd = cellCut(worksheet, 8 + Snums, 7, 40, true);
            Snums = Snums + dd;

            worksheet.Rows[9 + Snums][7].Value = dt.Rows[0]["ShipCity"] + " " + dt.Rows[0]["ShipState"] + " " + dt.Rows[0]["ShipZip"];
            worksheet.Rows[10 + Snums][7].Value = dt.Rows[0]["ShipCountry"].ToString();




            //Cell sourceCell = worksheet.Cells["G6"];

            //worksheet.Cells["G8"].CopyFrom(sourceCell);



            //worksheet.Rows[7].CopyFrom(worksheet.Rows[5], PasteSpecial.Formats);
            //worksheet.Rows[7].Borders.RemoveBorders();
            //worksheet.Rows[8].CopyFrom(worksheet.Rows[6], PasteSpecial.Formats);
            //worksheet.Rows[8].Borders.RemoveBorders();


            worksheet.Clear(worksheet.Range["N6:Q6"]);
            worksheet.MergeCells(worksheet.Range["n6:Q6"]);
            worksheet.Cells["n6"].Font.Bold = true;


            worksheet.Clear(worksheet.Range["n7:Q7"]);
            worksheet.MergeCells(worksheet.Range["n7:Q7"]);


            worksheet.Clear(worksheet.Range["N8:Q8"]);
            worksheet.MergeCells(worksheet.Range["n8:Q8"]);
            worksheet.Cells["n8"].Font.Bold = true;


            worksheet.Clear(worksheet.Range["n9:Q9"]);
            worksheet.MergeCells(worksheet.Range["n9:Q9"]);


            worksheet.Clear(worksheet.Range["n10:Q10"]);
            worksheet.MergeCells(worksheet.Range["n10:Q10"]);
            worksheet.Cells["n10"].Font.Bold = true;


            worksheet.Clear(worksheet.Range["n11:Q11"]);
            worksheet.MergeCells(worksheet.Range["n11:Q11"]);


            worksheet.Clear(worksheet.Range["n12:Q12"]);
            worksheet.MergeCells(worksheet.Range["n12:Q12"]);
            worksheet.Cells["n12"].Font.Bold = true;


            worksheet.Clear(worksheet.Range["n13:Q13"]);
            worksheet.MergeCells(worksheet.Range["n13:Q13"]);


            worksheet.Clear(worksheet.Range["n14:Q14"]);
            worksheet.MergeCells(worksheet.Range["n14:Q14"]);
            worksheet.Cells["n14"].Font.Bold = true;


            worksheet.Clear(worksheet.Range["n15:Q15"]);
            worksheet.MergeCells(worksheet.Range["n15:Q15"]);




            worksheet.Rows[5][13].Value = "Purchase PO No.:";
            worksheet.Rows[6][13].Value = dt.Rows[0]["PONumber"].ToString();

            worksheet.Rows[7][13].Value = "Customer-Ref No.:";
            worksheet.Rows[8][13].Value = dt.Rows[0]["CustomerRefNo"].ToString();
            worksheet.Rows[9][13].Value = "Invoice No.:  ";
            worksheet.Rows[10][13].Value = dt.Rows[0]["InvioceNo"].ToString();
            worksheet.Rows[11][13].Value = "Date of Issue:";
            worksheet.Rows[12][13].Value = DateConvert(Convert.ToDateTime(dt.Rows[0]["OrderDate"].ToString()));
            worksheet.Rows[13][13].Value = "Ship Date:";
            worksheet.Rows[14][13].Value = DateConvert(Convert.ToDateTime(dt.Rows[0]["ShipDate"].ToString()));

            worksheet.Rows[6][0].Value = "Chemscene LLC";
            worksheet.Rows[7][0].Value = "1 Deer Park Dr,Suite Q,";
            worksheet.Rows[8][0].Value = "Monmouth Junction,";
            worksheet.Rows[9][0].Value = "NJ 08852, USA.";

            worksheet.Clear(worksheet.Range["A11:E11"]);
            worksheet.MergeCells(worksheet.Range["B11:E11"]);
            worksheet.Cells["A11"].Font.Bold = true;
            worksheet.Rows[10][0].Value = "Tel: ";
            worksheet.Rows[10][1].Value = "732-484-9848";
            worksheet.Clear(worksheet.Range["A12:E12"]);
            worksheet.MergeCells(worksheet.Range["B12:E12"]);
            worksheet.Cells["A12"].Font.Bold = true;

            worksheet.Rows[11][0].Value = "Fax:";
            worksheet.Rows[11][1].Value = "888-484-5008";

            worksheet.Clear(worksheet.Range["A13:E13"]);
            worksheet.MergeCells(worksheet.Range["B13:E13"]);
            worksheet.Cells["A13"].Font.Bold = true;

            worksheet.Rows[12][0].Value = "E-mail:";
            worksheet.Rows[12][1].Value = "sales@chemscene.com";

            double MSDSHeight = 0.00;
            for (int i = 0; i < 24 + Snum + Snums; i++)
            {
                MSDSHeight = MSDSHeight + worksheet.Rows[i][0].RowHeight;
            }

            double pageHeight = 3229.1667633056641;
            MSDSHeight = MSDSHeight % pageHeight;
            if (MSDSHeight != 0)
            {
                for (int i = 0; i < Convert.ToInt32((pageHeight - MSDSHeight) / 66.458335876464844); i++)
                {
                    worksheet.Rows.Insert(18 + N_id + Snums + i);
                    worksheet.Rows[18 + N_id + Snums + i].CopyFrom(worksheet.Rows[18 + N_id + Snums + i - 1], PasteSpecial.Formats);
                    worksheet.Rows[18 + N_id + Snums + i].Borders.RemoveBorders();
                }
            }




            //#endregion PrintWorksheet
        }


        private void butExcel_Click(object sender, EventArgs e)
        {

            SaveFileDialog SaveFile = new SaveFileDialog();

            SaveFile.FileName = savePath;
            SaveFile.Filter = "EXCEL|*.xlsx";
            SaveFile.RestoreDirectory = true;
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {

                IWorkbook workbook = spreadsheetControl1.Document;

                workbook.SaveDocument(@SaveFile.FileName, DocumentFormat.OpenXml);
                labExcelPath.Text = @SaveFile.FileName;
                savePath = @SaveFile.FileName;
                System.Diagnostics.Process.Start(@SaveFile.FileName);
                this.DialogResult = DialogResult.OK;
            }

        }




        private void butPDF_Click(object sender, EventArgs e)
        {


            SaveFileDialog SaveFile = new SaveFileDialog();
            savePath = savePath.Replace(".xlsx", ".pdf");
            //if (saveType == 0) //发票
            //    savePath = "Invoice#MCE" + saverOrderNo + " " + saverinvioceNO + ".pdf";
            //else
            //    savePath = "PackingList#MCE" + saverOrderNo + ".pdf";
            SaveFile.FileName = savePath;
            SaveFile.Filter = "PDF|*.PDF";
            SaveFile.RestoreDirectory = true;
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {

                IWorkbook workbook = spreadsheetControl1.Document;

                using (FileStream pdfFileStream = new FileStream(@SaveFile.FileName, FileMode.Create))
                {
                    workbook.ExportToPdf(pdfFileStream);
                }


                //workbook.SaveDocument(@SaveFile.FileName, DocumentFormat.OpenXml);
                labExcelPath.Text = @SaveFile.FileName;
                savePath = @SaveFile.FileName;
                System.Diagnostics.Process.Start(@SaveFile.FileName);
                this.DialogResult = DialogResult.OK;
            }


        }

        private void butPrint_Click(object sender, EventArgs e)
        {
            IWorkbook workbook = spreadsheetControl1.Document;
            // Print(workbook);
        }

        private string getDwHs(string beforDw, double beforSz, string afterDw, Double afterSZ, Double zje)
        {
            if (beforDw != afterDw)
            {
                Double beforzjSz = 0;
                Double afterzjSz = 0;
                switch (beforDw)
                {
                    case "g":
                        beforzjSz = beforSz;
                        break;
                    case "kg":
                        beforzjSz = beforSz * 1000;
                        break;
                    case "μg":
                        beforzjSz = beforSz * 0.000001;
                        break;
                    case "mg":
                        beforzjSz = beforSz * 0.001;
                        break;
                    default:
                        beforzjSz = beforSz;
                        break;
                }


                switch (afterDw)
                {
                    case "g":
                        afterzjSz = afterSZ;
                        break;
                    case "kg":
                        afterzjSz = afterSZ * 1000;
                        break;
                    case "μg":
                        afterzjSz = afterSZ * 0.000001;
                        break;
                    case "mg":
                        afterzjSz = afterSZ * 0.001;
                        break;
                    default:
                        afterzjSz = afterSZ;
                        break;
                }

                return (beforzjSz / afterzjSz * zje).ToString("0.00");
            }
            else
            {
                return (beforSz / afterSZ * zje).ToString("0.00");
            }

        }
        public static string DateConvert(DateTime dTime)
        {
            string retTime = "";
            switch (dTime.Month)
            {
                case 1:
                    retTime = "Jan-" + dTime.ToString("dd-yyyy");
                    break;
                case 2:
                    retTime = "Feb-" + dTime.ToString("dd-yyyy");
                    break;
                case 3:
                    retTime = "Mar-" + dTime.ToString("dd-yyyy");
                    break;
                case 4:
                    retTime = "Apr-" + dTime.ToString("dd-yyyy");
                    break;
                case 5:
                    retTime = "May-" + dTime.ToString("dd-yyyy");
                    break;
                case 6:
                    retTime = "June-" + dTime.ToString("dd-yyyy");
                    break;
                case 7:
                    retTime = "July-" + dTime.ToString("dd-yyyy");
                    break;
                case 8:
                    retTime = "Aug-" + dTime.ToString("dd-yyyy");
                    break;
                case 9:
                    retTime = "Sept-" + dTime.ToString("dd-yyyy");
                    break;
                case 10:
                    retTime = "Oct-" + dTime.ToString("dd-yyyy");
                    break;
                case 11:
                    retTime = "Nov-" + dTime.ToString("dd-yyyy");
                    break;
                case 12:
                    retTime = "Dec-" + dTime.ToString("dd-yyyy");
                    break;

            }
            return retTime;
        }
        private void Print(IWorkbook workbook, string orderNo, string trNo, string invioceNO, bool IsB = true)
        {
            //#region WorksheetPrintOptions
            Worksheet worksheet = workbook.Worksheets[0];
            DataTable dtOrder = new DataTable();

            worksheet.Rows[0][8].Value = "Bioactive Molecules, Building Blocks, Intermediates";
            worksheet.Rows[1][8].Value = "www.ChemScene.com";

            Range range = worksheet.Range["I1:Q1"];
            range.Merge();
            //  Cell cell = firstSheet.Cells["B2"];

            range.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range.Alignment.Horizontal = DevExpress.Spreadsheet.SpreadsheetHorizontalAlignment.Right;
            range = worksheet.Range["I2:Q2"];
            range.Merge();

            range.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range.Alignment.Horizontal = DevExpress.Spreadsheet.SpreadsheetHorizontalAlignment.Right;

            string sql = "select MCEOrderInfo.*,MCECustomersInfo.VATIDNo from MCEOrderInfo left join  MCECustomersInfo on  MCEOrderInfo.[SalesCompany]=MCECustomersInfo.[SalesCompany] where OrderNo = '" + orderNo + "'";

            dtOrder = DbHelperSQL.Query(sql).Tables[0];
            int dd, dd1;
            worksheet.Rows[6][9].Value = invioceNO;
            worksheet.Rows[8][9].Value = DateConvert(Convert.ToDateTime(dtOrder.Rows[0]["OrderDate"].ToString()));
            sql = "select *  from CSShipmentInfo where OrderNo = '" + orderNo + "' order by id  desc";
            DataTable dtShip = DbHelperSQL.Query(sql).Tables[0];

            sql = "select * from MCEPaymentInfo where invioceno='" + invioceNO + "'  order by id desc";
            DataTable dtPay = DbHelperSQL.Query(sql).Tables[0];

            if (checkEdit1.Checked)
                savePath = " Invoice#CS" + invioceNO + " " + orderNo + ".xlsx";
            else
                savePath = " Invoice#CS" + invioceNO + "_" + dtOrder.Rows[0]["BillContactName"] + "_" + dtOrder.Rows[0]["PONumber"] + ".xlsx";

            if (dtShip.Rows.Count > 0)
            {
                worksheet.Rows[8][9].Value = DateConvert(Convert.ToDateTime(dtShip.Rows[0]["ShipDate"].ToString()));
            }

            if (dtOrder.Rows[0]["PayMethod"].ToString() == "Credit Card")
            {
                worksheet.Rows[10][9].Value = "CC";
                worksheet.Rows[12][9].Value = "Paid " + DateTime.Now.ToString("MM");
                if (dtPay.Rows.Count > 0)
                {
                    worksheet.Rows[12][9].Value = "Paid " + DateConvert(Convert.ToDateTime(dtPay.Rows[0]["ReceivedDate"].ToString()));
                }
                else
                {
                    if (dtShip.Rows.Count > 0)
                    {
                        worksheet.Rows[12][9].Value = DateConvert(Convert.ToDateTime(dtShip.Rows[0]["ShipDate"].ToString()).AddDays(Convert.ToDouble(dtOrder.Rows[0]["Terms"])));

                    }
                }

            }
            else
            {
                worksheet.Rows[10][9].Value = "NET " + dtOrder.Rows[0]["Terms"].ToString() + "Days";
                worksheet.Rows[12][9].Value = DateConvert(Convert.ToDateTime(dtOrder.Rows[0]["OrderDate"].ToString()).AddDays(Convert.ToDouble(dtOrder.Rows[0]["Terms"])));
                if (dtPay.Rows.Count > 0)
                {
                    worksheet.Rows[12][9].Value = "Paid " + DateConvert(Convert.ToDateTime(dtPay.Rows[0]["ReceivedDate"].ToString()));
                }
                else
                {
                    if (dtShip.Rows.Count > 0)
                    {
                        worksheet.Rows[12][9].Value = DateConvert(Convert.ToDateTime(dtShip.Rows[0]["ShipDate"].ToString()).AddDays(Convert.ToDouble(dtOrder.Rows[0]["Terms"])));

                    }
                }
            }


            //Purchase PO No.:
            worksheet.Rows[6][14].Value = dtOrder.Rows[0]["PONumber"].ToString();
            //Customer-Ref No.:
            worksheet.Rows[8][14].Value = dtOrder.Rows[0]["CustomerRefNo"].ToString();
            //Vendor No.:
            worksheet.Rows[10][14].Value = dtOrder.Rows[0]["VendorCode"].ToString();


            worksheet.Rows[15][0].Value = dtOrder.Rows[0]["BillCompany"].ToString();
            worksheet.Rows[16][1].Value = dtOrder.Rows[0]["BillContactName"].ToString();
            if (dtOrder.Rows[0]["BillStreet"].ToString().Length > dtOrder.Rows[0]["ShipStreet"].ToString().Length)
            {
                worksheet.Rows[17][0].Value = dtOrder.Rows[0]["BillStreet"].ToString();

                dd = cellCut(worksheet, 17, 0, 55, true);
                worksheet.Rows[17][9].Value = dtOrder.Rows[0]["ShipStreet"].ToString();

                dd1 = cellCut(worksheet, 17, 9, 55, false);
            }
            else
            {
                worksheet.Rows[17][9].Value = dtOrder.Rows[0]["ShipStreet"].ToString();

                dd = cellCut(worksheet, 17, 9, 55, true);
                worksheet.Rows[17][0].Value = dtOrder.Rows[0]["BillStreet"].ToString();

                dd1 = cellCut(worksheet, 17, 0, 55, false);
            }



            worksheet.Rows[18 + dd][0].Value = dtOrder.Rows[0]["BillCity"].ToString() + " " + dtOrder.Rows[0]["BillState"].ToString() + " " + dtOrder.Rows[0]["BillZip"].ToString();
            worksheet.Rows[19 + dd][0].Value = dtOrder.Rows[0]["BillCountry"].ToString();
            worksheet.Rows[20 + dd][1].Value = dtOrder.Rows[0]["BilleMail"].ToString();

            worksheet.Rows[15][9].Value = dtOrder.Rows[0]["ShipCompany"].ToString();
            worksheet.Rows[16][10].Value = dtOrder.Rows[0]["ShipContactName"].ToString();


            worksheet.Rows[18 + dd][9].Value = dtOrder.Rows[0]["ShipCity"].ToString() + " " + dtOrder.Rows[0]["ShipState"].ToString() + " " + dtOrder.Rows[0]["ShipZip"].ToString();
            worksheet.Rows[19 + dd][9].Value = dtOrder.Rows[0]["ShipCountry"].ToString();
            worksheet.Rows[20 + dd][10].Value = dtOrder.Rows[0]["ShipTel"].ToString();
            int Y_EXCEL = 23 + dd;
            int N_id = 0;
            int ddzj = 0;
            if (dtShip.Rows.Count >= 0)
            {
                for (int i = 0; i < dtShip.Rows.Count; i++)
                {


                    if (Y_EXCEL + N_id + ddzj > Y_EXCEL)
                    {
                        worksheet.Rows.Insert(Y_EXCEL + N_id + ddzj);
                        worksheet.Rows[Y_EXCEL + N_id + ddzj].CopyFrom(worksheet.Rows[Y_EXCEL + N_id + ddzj - 1], PasteSpecial.Formats);
                        worksheet.Rows[Y_EXCEL + N_id + ddzj].Borders.RemoveBorders();
                    }
                    worksheet.Rows[Y_EXCEL + N_id + ddzj][0].Value = DateConvert(Convert.ToDateTime(dtShip.Rows[i]["ShipDate"].ToString())); ;
                    worksheet.Rows[Y_EXCEL + N_id + ddzj][5].Value = dtShip.Rows[i]["ShipVia"].ToString();
                    worksheet.Rows[Y_EXCEL + N_id + ddzj][9].Value = dtShip.Rows[i]["TrackingNo"].ToString();
                    N_id = N_id + 1;
                }
                N_id = N_id + ddzj;
            }

            int Snum = dd;
            if (N_id > 1)
                Snum = Snum + N_id - 1;

            Y_EXCEL = 26 + Snum;
            // Snum = 0;
            N_id = 0;
            ddzj = 0;
            double Subtotal = 0.00;

            sql = "select ProCatalogNo,ProDescription,ProSize,ProUnit,ProQuantity,ProAmount,ProCurrency,convert(varchar(20),ProDunOn,101) as ProDunOn,ProNote,ProLibraryID, ID from  MCEOrderProInfo where OrderNo = '" + orderNo + "'";

            DataTable dtPr = DbHelperSQL.Query(sql).Tables[0];

            if (dtPr.Rows.Count > 0)
            {
                for (int i = 0; i < dtPr.Rows.Count; i++)
                {


                    if (Y_EXCEL + N_id + ddzj > Y_EXCEL)
                    {
                        worksheet.Rows.Insert(Y_EXCEL + N_id + ddzj);
                        worksheet.Rows[Y_EXCEL + N_id + ddzj].CopyFrom(worksheet.Rows[Y_EXCEL + N_id + ddzj - 1], PasteSpecial.Formats);
                        worksheet.Rows[Y_EXCEL + N_id + ddzj].Borders.RemoveBorders();
                    }
                    worksheet.Rows[Y_EXCEL + N_id + ddzj][0].Value = dtPr.Rows[i]["ProCatalogNo"].ToString(); ;
                    worksheet.Rows[Y_EXCEL + N_id + ddzj][2].Value = dtPr.Rows[i]["ProDescription"].ToString();
                    worksheet.Rows[Y_EXCEL + N_id + ddzj][11].Value = dtPr.Rows[i]["ProSize"].ToString() + " " + dtPr.Rows[i]["ProUnit"].ToString();
                    worksheet.Rows[Y_EXCEL + N_id + ddzj][14].Value = dtPr.Rows[i]["ProQuantity"].ToString();
                    worksheet.Rows[Y_EXCEL + N_id + ddzj][15].Value = Convert.ToDouble(dtPr.Rows[i]["ProAmount"].ToString()).ToString("0.00");
                    ddzj = ddzj + cellCut(worksheet, Y_EXCEL + N_id + ddzj, 2, 56, true);
                    Subtotal = Subtotal + Convert.ToDouble(dtPr.Rows[i]["ProAmount"].ToString());
                    N_id = N_id + 1;
                }
                N_id = N_id + ddzj;
            }

            if (N_id > 1)
                Snum = Snum + N_id - 1;
            if (dtOrder.Rows[0]["BillCountry"].ToString() == "United States")
                worksheet.Rows[27 + Snum][0].Value = dtOrder.Rows[0]["Comments"].ToString() + "For laboratory use only --- Not for drug, household or other use.";
            else
                worksheet.Rows[27 + Snum][0].Value = dtOrder.Rows[0]["Comments"].ToString() + "For laboratory use only --- Not for drug, household or other use. Incoterms: DDU.";

            dd = cellCut(worksheet, 27 + Snum, 0, 140, true);
            Snum = Snum + dd;
            worksheet.Rows[29 + Snum][15].Value = "$" + Subtotal.ToString("0.00");
            //worksheet.Rows[30 + Snum][15].Value = "$" + "0.00";
            worksheet.Rows[30 + Snum][11].Value = "Sales & Use Tax(" + dtOrder.Rows[0]["Tax"].ToString() + "%)";
            worksheet.Rows[30 + Snum][15].Value = "$" + dtOrder.Rows[0]["Taxation"].ToString();
            worksheet.Rows[31 + Snum][15].Value = "$" + Convert.ToDouble(dtOrder.Rows[0]["SH"].ToString()).ToString("0.00");
            worksheet.Rows[32 + Snum][15].Value = "$" + (Subtotal + Convert.ToDouble(dtOrder.Rows[0]["SH"].ToString()) + Convert.ToDouble(dtOrder.Rows[0]["Taxation"].ToString())).ToString("0.00");
            worksheet.Rows[33 + Snum][15].Value = "$" + "0.00";
           // worksheet.Rows[34 + Snum][15].Value = "$" + Convert.ToDouble(dtPr.Rows[0]["ProAmount"].ToString()).ToString("0.00");

            double payT = 0;

            string cardInfo = "";

            if (dtPay.Rows.Count > 0)
            {
                if (dtPay.Rows[0]["Cardinfo"].ToString()!="")
                {
                    cardInfo = "Paid with card ending " + " " + dtPay.Rows[0]["Cardinfo"].ToString().Substring(dtPay.Rows[0]["Cardinfo"].ToString().Length - 4, 4);
                }
                worksheet.Rows[35 + Snum][0].Value = "";
            }
            if (dtOrder.Rows[0]["PayMethod"].ToString() == "Credit Card")
            {

                worksheet.Rows[29 + Snum][0].Value = "";
                worksheet.Rows[30 + Snum][0].Value = "";
                worksheet.Rows[31 + Snum][0].Value = "";
                worksheet.Rows[32 + Snum][0].Value = cardInfo;
                worksheet.Rows[33 + Snum][0].Value = "";
                worksheet.Rows[34 + Snum][0].Value = "";                    //  worksheet.Rows[33 + Snum][15].Value = dtPay.Rows[0]["PID"].ToString();
                //   payT = Convert.ToDouble(dtPay.Rows[0]["PID"].ToString());
                //  worksheet.Rows[34 + Snum][15].Value = "$0.00";
            }
            else
            {
                if (dtOrder.Rows[0]["BillCountry"].ToString() == "United States" || dtOrder.Rows[0]["BillCountry"].ToString() == "U.S.A.")
                {

                    worksheet.Rows[29 + Snum][0].Value = "";
                    worksheet.Rows[30 + Snum][0].Value = "";
                    worksheet.Rows[31 + Snum][0].Value = "";
                    worksheet.Rows[32 + Snum][0].Value = "";
                    worksheet.Rows[33 + Snum][0].Value = "";
                    worksheet.Rows[34 + Snum][0].Value = "";

                }
            }

            //  'Paid :
            worksheet.Rows[33 + Snum][15].Value = "$" + "0.00";

            double paiSum = 0.00;
            sql = "select isnull(sum(receivedamount),0) as pid from  MCEPaymentInfo  where InvioceNo='" + invioceNO + "' ";
            DataTable dtPaySum = DbHelperSQL.Query(sql).Tables[0];
            if (dtPaySum.Rows.Count > 0)
            {
                paiSum = Convert.ToDouble(dtPaySum.Rows[0]["PID"].ToString());
                worksheet.Rows[33 + Snum][15].Value = "$" + Convert.ToDouble(dtPaySum.Rows[0]["PID"].ToString()).ToString("0.00");
            }



            //  'Balance DUE:
            worksheet.Rows[34 + Snum][15].Value = "$" + (Subtotal + Convert.ToDouble(dtOrder.Rows[0]["Taxation"].ToString()) + Convert.ToDouble(dtOrder.Rows[0]["SH"].ToString()) - paiSum).ToString("0.00");
            double MSDSHeight = 0.00;
            for (int i = 0; i < 36 + Snum; i++)
            {
                MSDSHeight = MSDSHeight + worksheet.Rows[i][0].RowHeight;
            }

            double pageHeight = 3229.1667633056641;
            MSDSHeight = MSDSHeight % pageHeight;
            if (MSDSHeight != 0)
            {
                for (int i = 0; i < Convert.ToInt32((pageHeight - MSDSHeight) / 66.458335876464844); i++)
                {
                    worksheet.Rows.Insert(27 + Snum + i);
                    worksheet.Rows[27 + Snum + i].CopyFrom(worksheet.Rows[27 + Snum + i - 1], PasteSpecial.Formats);
                    worksheet.Rows[27 + Snum + i].Borders.RemoveBorders();
                }
            }
        }


        private int cellCut(Worksheet worksheet, int mRow, int mCol, int JqS, bool IsCx = true)
        {
            int dd;
            string yStr = worksheet.Rows[mRow][mCol].Value.ToString() + " ";
            string qStr;
            dd = 0;
            int i = 0;
            do
            {
                qStr = EuSoft.Common.StringPlus.CutEn(yStr, JqS);
                if (i == 0)
                {
                    worksheet.Rows[mRow][mCol].Value = qStr;
                }
                else
                {
                    if (IsCx)
                    {
                        worksheet.Rows.Insert(mRow + i);
                        worksheet.Rows[mRow + i].CopyFrom(worksheet.Rows[mRow + i - 1], PasteSpecial.Formats);
                        worksheet.Rows[mRow + i].Borders.RemoveBorders();
                        dd = dd + 1;
                    }
                    worksheet.Rows[mRow + i][mCol].Value = qStr;
                }
                yStr = yStr.Substring(qStr.Length, yStr.Length - qStr.Length);
                i = i + 1;
            } while (yStr.Length > 0);

            return dd;
        }

        private void butPdf_Click_1(object sender, EventArgs e)
        {


            SaveFileDialog SaveFile = new SaveFileDialog();
            if (saveType == 0) //发票
                savePath = "Invoice#MCT" + saverOrderNo + " " + saverinvioceNO + ".pdf";
            else
                savePath = "PackingList#MCT" + saverOrderNo + ".pdf";
            SaveFile.FileName = savePath;
            SaveFile.Filter = "PDF|*.PDF";
            SaveFile.RestoreDirectory = true;
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {

                IWorkbook workbook = spreadsheetControl1.Document;


                using (FileStream pdfFileStream = new FileStream(@SaveFile.FileName, FileMode.Create))
                {
                    workbook.ExportToPdf(pdfFileStream);
                }


                //workbook.SaveDocument(@SaveFile.FileName, DocumentFormat.OpenXml);
                labExcelPath.Text = @SaveFile.FileName;
                savePath = @SaveFile.FileName;
                System.Diagnostics.Process.Start(@SaveFile.FileName);
                this.DialogResult = DialogResult.OK;
            }
        }



    }
}