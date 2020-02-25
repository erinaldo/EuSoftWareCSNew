using Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using System.Net;
using NPOI.SS.Util;


namespace EuSoft.Common
{
    public class ExcelHelper
    {




        //private IWorkbook workbook = null;
        //private FileStream fs = null;




        //#region XZH 2011.3

        //#region HandleExcel


        /// <summary>  
        /// 设置单元格内容  
        /// </summary>  
        /// <param name="row"></param>  
        /// <param name="col"></param>  
        /// <param name="value"></param>  
        public static void SetCellText(ISheet se, int row, int col, string value)
        {


            IRow dataRow = se.GetRow(row);
            ICell newCell = dataRow.GetCell(col);
            newCell.SetCellValue(value);

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
        #region Excel复制行

        /// <summary>
        /// Excel复制行
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="sheet"></param>
        /// <param name="starRow"></param>
        /// <param name="rows"></param>
        private static void insertRow(XSSFWorkbook wb, ISheet sheet, int starRow, int rows, List<int> sCol,List<int> eCol)
        {
            /*
             * ShiftRows(int startRow, int endRow, int n, bool copyRowHeight, bool resetOriginalRowHeight); 
             * 
             * startRow 开始行
             * endRow 结束行
             * n 移动行数
             * copyRowHeight 复制的行是否高度在移
             * resetOriginalRowHeight 是否设置为默认的原始行的高度
             * 
             */

            sheet.ShiftRows(starRow + 1, sheet.LastRowNum, rows, true, true);

            starRow = starRow - 1;

            for (int i = 0; i < rows; i++)
            {

                IRow sourceRow = null;
                IRow targetRow = null;
                ICell sourceCell = null;
                ICell targetCell = null;

                short m;

                starRow = starRow + 1;
                sourceRow = (IRow)sheet.GetRow(starRow);
                targetRow = (IRow)sheet.CreateRow(starRow + 1);

             
                targetRow.HeightInPoints = sourceRow.HeightInPoints;

                for (m = (short)sourceRow.FirstCellNum; m < sourceRow.LastCellNum; m++)
                {

                    sourceCell = (ICell)sourceRow.GetCell(m);
                    targetCell = (ICell)targetRow.CreateCell(m);

                    //ICellStyle newCellStyle = wb.CreateCellStyle();
                    //newCellStyle.CloneStyleFrom(sourceCell.CellStyle);
                    
               //     targetCell.Encoding = sourceCell.Encoding;
                    if (sourceCell!=null)
                    {
                        targetCell.CellStyle = sourceCell.CellStyle;
                        targetCell.SetCellType(sourceCell.CellType);
                    }
                   
                   


                }

                for (int j = 0; j < sCol.Count; j++) ///合并单元格
                {
                    sheet.AddMergedRegion(new CellRangeAddress(starRow + 1, starRow + 1, sCol[j], eCol[j]));
                }
                    
             
                  
              
               
            }

        }

        #endregion

        /// 
        /// HSSFRow Copy Command
        ///
        /// Description:  Inserts a existing row into a new row, will automatically push down
        ///               any existing rows.  Copy is done cell by cell and supports, and the
        ///               command tries to copy all properties available (style, merged cells, values, etc...)
        /// 
        private static void CopyRow(XSSFWorkbook workbook, ISheet worksheet, int sourceRowNum, int destinationRowNum)
        {
            // Get the source / new row
            IRow newRow = worksheet.GetRow(destinationRowNum);
            IRow sourceRow = worksheet.GetRow(sourceRowNum);

            // If the row exist in destination, push down all rows by 1 else create a new row
            if (newRow != null)
            {
                worksheet.ShiftRows(destinationRowNum, worksheet.LastRowNum, 2);
            }
            else
            {
                newRow = worksheet.CreateRow(destinationRowNum);
            }

            // Loop through source columns to add to new row
            for (int i = 0; i < sourceRow.LastCellNum; i++)
            {
                // Grab a copy of the old/new cell
                ICell oldCell = sourceRow.GetCell(i);
                ICell newCell = newRow.CreateCell(i);

                // If the old cell is null jump to next cell
                if (oldCell == null)
                {
                    newCell = null;
                    continue;
                }

                // Copy style from old cell and apply to new cell

                ICellStyle newCellStyle = workbook.CreateCellStyle();
                newCellStyle.CloneStyleFrom(oldCell.CellStyle); ;
                newCell.CellStyle = newCellStyle;

                // If there is a cell comment, copy
                if (newCell.CellComment != null) newCell.CellComment = oldCell.CellComment;

                // If there is a cell hyperlink, copy
                if (oldCell.Hyperlink != null) newCell.Hyperlink = oldCell.Hyperlink;

                // Set the cell data type
                newCell.SetCellType(oldCell.CellType);

                // Set the cell data value
                switch (oldCell.CellType)
                {

                    case CellType.Blank:
                        newCell.SetCellValue(oldCell.StringCellValue);
                        break;
                    case CellType.Boolean:
                        newCell.SetCellValue(oldCell.BooleanCellValue);
                        break;
                    case CellType.Error:
                        newCell.SetCellErrorValue(oldCell.ErrorCellValue);
                        break;
                    case CellType.Formula:
                        newCell.SetCellFormula(oldCell.CellFormula);
                        break;
                    case CellType.Numeric:
                        newCell.SetCellValue(oldCell.NumericCellValue);
                        break;
                    case CellType.String:
                        newCell.SetCellValue(oldCell.RichStringCellValue);
                        break;
                    case CellType.Unknown:
                        newCell.SetCellValue(oldCell.StringCellValue);
                        break;
                }
            }

            // If there are are any merged regions in the source row, copy to new row
            for (int i = 0; i < worksheet.NumMergedRegions; i++)
            {


          
           CellRangeAddress cellRangeAddress = worksheet.GetMergedRegion(i);
                if (cellRangeAddress.FirstRow == sourceRow.RowNum)
                {
                    CellRangeAddress newCellRangeAddress = new CellRangeAddress(newRow.RowNum,
                                                                                (newRow.RowNum +
                                                                                 (cellRangeAddress.FirstRow -
                                                                                  cellRangeAddress.LastRow)),
                                                                                cellRangeAddress.FirstColumn,
                                                                                cellRangeAddress.LastColumn);
                    worksheet.AddMergedRegion(newCellRangeAddress);
                }
            }

        }



        ///// <summary>
        ///// 利用模板，DataTable导出到Excel（单个类别）
        ///// </summary>
        ///// <param name="dtSource">DataTable</param>
        ///// <param name="strTemplateFileName">模板的文件路径、名称</param>
        ///// <param name="flg">文件标识--sheet名（1：经营贸易情况/2：生产经营情况/3：项目投资情况/4：房产销售情况/其他：总表）</param>
        ///// <param name="titleName">表头名称</param>
        ///// <returns></returns>
        public static void ExportExcelForDtByNPOI(System.Data.DataTable dtOrder, System.Data.DataTable dtShip, System.Data.DataTable dtPr, System.Data.DataTable dtPay, string strFileName, string strTemplateFileName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = 20;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            // int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            XSSFWorkbook workbook = new XSSFWorkbook(file);

            ISheet sheet = workbook.GetSheet("MSDS");
            if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
            {
                sheet = workbook.GetSheetAt(0);
            }



        //    //第一步：读取图片到byte数组  
        ////    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Template\usa_email_logo.png");

        //    byte[] bytes = System.IO.File.ReadAllBytes(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Template\usa_email_logo.png");
 
 
           

        //    //第二步：将图片添加到workbook中  指定图片格式 返回图片所在workbook->Picture数组中的索引地址（从1开始）  
        //    int pictureIdx = workbook.AddPicture(bytes, PictureType.JPEG);

        //    //第三步：在sheet中创建画部  
        //    NPOI.SS.UserModel.IDrawing patriarch = sheet.CreateDrawingPatriarch();

        //    //第四步：设置锚点 （在起始单元格的X坐标0-1023，Y的坐标0-255，在终止单元格的X坐标0-1023，Y的坐标0-255，起始单元格行数，列数，终止单元格行数，列数）  
        //    IClientAnchor anchor = patriarch.CreateAnchor(0, 0, 0, 0, 0, 0, 2, 2);


        //    //第五步：创建图片  
        //    NPOI.SS.UserModel.IPicture pict = patriarch.CreatePicture(anchor, pictureIdx);  






            SetCellText(sheet, 6, 9, dtOrder.Rows[0]["InvioceNo"].ToString());
          //  SetCellText(sheet, 6, 12, dtOrder.Rows[0]["PONumber"].ToString());
            SetCellText(sheet, 8, 9, DateConvert(Convert.ToDateTime(dtOrder.Rows[0]["OrderDate"].ToString())));

            if (dtShip.Rows.Count > 0)
            {
                SetCellText(sheet, 8, 9, DateConvert(Convert.ToDateTime(dtShip.Rows[0]["ShipDate"].ToString())));
            }



            SetCellText(sheet, 8, 12, "");

            if (dtOrder.Rows[0]["PayMethod"].ToString() == "Credit Card")
            {
                SetCellText(sheet, 10, 9, "CC");
                SetCellText(sheet, 12, 9, "Paid " + DateTime.Now.Month.ToString("dd"));
                if (dtPay.Rows.Count > 0)
                {
                    SetCellText(sheet, 12, 9, "Paid " + DateConvert(Convert.ToDateTime(dtPay.Rows[0]["ReceivedDate"].ToString())));
                }
                else
                {
                    if (dtShip.Rows.Count > 0)
                    {
                        SetCellText(sheet, 12, 9, DateConvert(Convert.ToDateTime(dtShip.Rows[0]["ShipDate"].ToString()).AddDays(Convert.ToDouble(dtOrder.Rows[0]["Terms"]))));

                    }
                }


            }
            else
            {
                SetCellText(sheet, 10, 9, "NET " + dtOrder.Rows[0]["Terms"].ToString() + "Days");
                SetCellText(sheet, 12, 9, DateConvert(Convert.ToDateTime(dtOrder.Rows[0]["OrderDate"].ToString()).AddDays(Convert.ToDouble(dtOrder.Rows[0]["Terms"]))));
                if (dtPay.Rows.Count > 0)
                {
                    SetCellText(sheet, 12, 9, "Paid " + DateConvert(Convert.ToDateTime(dtPay.Rows[0]["ReceivedDate"].ToString())));
                }
                else
                {
                    if (dtShip.Rows.Count > 0)
                    {
                        SetCellText(sheet, 12, 9, DateConvert(Convert.ToDateTime(dtShip.Rows[0]["ShipDate"].ToString()).AddDays(Convert.ToDouble(dtOrder.Rows[0]["Terms"]))));

                    }
                }
            }

            //Purchase PO No.:
            SetCellText(sheet, 6, 14, dtOrder.Rows[0]["PONumber"].ToString());
            //Customer-Ref No.:
            SetCellText(sheet, 8, 14, dtOrder.Rows[0]["CustomerRefNo"].ToString());
            //Vendor No.:
            SetCellText(sheet, 10, 14, dtOrder.Rows[0]["VendorCode"].ToString());

            SetCellText(sheet, 15, 0, dtOrder.Rows[0]["BillCompany"].ToString());
            SetCellText(sheet, 16, 1, dtOrder.Rows[0]["BillContactName"].ToString());
            SetCellText(sheet, 17, 0, dtOrder.Rows[0]["BillStreet"].ToString());
            SetCellText(sheet, 18, 0, dtOrder.Rows[0]["BillCity"].ToString() + " " + dtOrder.Rows[0]["BillState"].ToString() + " " + dtOrder.Rows[0]["BillZip"].ToString());
            SetCellText(sheet, 19, 0, dtOrder.Rows[0]["BillCountry"].ToString());
            SetCellText(sheet, 20, 1, dtOrder.Rows[0]["BilleMail"].ToString());

            SetCellText(sheet, 15, 9, dtOrder.Rows[0]["ShipCompany"].ToString());
            SetCellText(sheet, 16, 10, dtOrder.Rows[0]["ShipContactName"].ToString());
            SetCellText(sheet, 17, 9, dtOrder.Rows[0]["ShipStreet"].ToString());
            SetCellText(sheet, 18, 9, dtOrder.Rows[0]["ShipCity"].ToString() + " " + dtOrder.Rows[0]["ShipState"].ToString() + " " + dtOrder.Rows[0]["ShipZip"].ToString());
            SetCellText(sheet, 19, 9, dtOrder.Rows[0]["ShipCountry"].ToString());
            SetCellText(sheet, 20, 10, dtOrder.Rows[0]["ShipTel"].ToString());
            int nLine = 0;
            List<int> sCol = new List<int>();
            List<int> eCol = new List<int>();
            sCol.Add(0);
            eCol.Add(3);
            sCol.Add(5);
            eCol.Add(7);
            sCol.Add(9);
            eCol.Add(13);
            sheet.CopyRow(26, 27);
            if (dtShip.Rows.Count >1)
            {
                nLine = dtShip.Rows.Count - 1;
                
                insertRow(workbook, sheet, 23, dtShip.Rows.Count - 1, sCol, eCol);
            }
            
            for (int i = 0; i < dtShip.Rows.Count; i++)
            {
                
                SetCellText(sheet, 23+i, 0, DateConvert(Convert.ToDateTime(dtShip.Rows[0]["ShipDate"].ToString())));
                SetCellText(sheet, 23+i, 5, dtShip.Rows[0]["ShipVia"].ToString());
                SetCellText(sheet, 23+i, 9, dtShip.Rows[0]["TrackingNo"].ToString());

            }
            double sumTotal = 0;
            sCol.Clear();
            eCol.Clear();
            sCol.Add(0);
            eCol.Add(1);
            sCol.Add(2);
            eCol.Add(10);
            sCol.Add(11);
            eCol.Add(13);
            sCol.Add(15);
            eCol.Add(16);

            if (dtPr.Rows.Count<=9)
            {
               // sheet.CopyRow(25,26);
                insertRow(workbook, sheet, 26 + nLine, 9 - nLine, sCol, eCol);
                nLine+=9;
            }
            else
            {
                insertRow(workbook, sheet, 26 + nLine, dtPr.Rows.Count, sCol, eCol);
                nLine += dtPr.Rows.Count;
            }
 
          //insertRow(workbook, sheet, 27, 8);
          //  CopyRow(workbook, sheet, 26, 9);
            for (int i = 0; i < dtPr.Rows.Count; i++)
            {
               


               
                sumTotal += Convert.ToDouble(dtPr.Rows[0]["ProAmount"].ToString());

                SetCellText(sheet, 26+i, 0, dtPr.Rows[0]["ProCatalogNo"].ToString());
                SetCellText(sheet, 26 + i, 2, dtPr.Rows[0]["ProDescription"].ToString());
                SetCellText(sheet, 26 + i, 11, dtPr.Rows[0]["ProSize"].ToString() + " " + dtPr.Rows[0]["ProUnit"].ToString());
                SetCellText(sheet, 26 + i, 14, dtPr.Rows[0]["ProQuantity"].ToString());
                SetCellText(sheet, 26 + i, 15, dtPr.Rows[0]["ProAmount"].ToString());
             

            }
            if (dtOrder.Rows[0]["BillCountry"].ToString() == "United States")
                SetCellText(sheet, 36, 0, dtOrder.Rows[0]["Comments"].ToString() + "For laboratory use only --- Not for drug, household or other use.");
            else
                SetCellText(sheet, 36, 0, dtOrder.Rows[0]["Comments"].ToString() + "For laboratory use only --- Not for drug, household or other use. Incoterms: DDU.");


            SetCellText(sheet, 38, 15, sumTotal.ToString("0.00"));
            SetCellText(sheet, 39, 15, "0.00");
            SetCellText(sheet, 40, 15, dtOrder.Rows[0]["SH"].ToString());
            SetCellText(sheet, 41, 15, Convert.ToString(sumTotal + Convert.ToDouble(dtOrder.Rows[0]["SH"].ToString())));
            SetCellText(sheet, 42, 15, "0.00");
            SetCellText(sheet, 43, 15, dtPr.Rows[0]["ProAmount"].ToString());

            double payT=0;

            if (dtPay.Rows.Count > 0)
            {
                if (dtOrder.Rows[0]["PayMethod"].ToString() == "Credit Card")
                {

                    SetCellText(sheet, 38, 0, "");
                    SetCellText(sheet, 39, 0, "");
                    SetCellText(sheet, 40, 0, "");
                    SetCellText(sheet, 41, 0, "");
                    SetCellText(sheet, 43, 0, "");
                    SetCellText(sheet, 42, 0, "Paid with card ending " + " " + dtPay.Rows[0]["Cardinfo"].ToString().Substring(dtPay.Rows[0]["Cardinfo"].ToString().Length - 4, 4));
                    SetCellText(sheet, 42, 15, dtPay.Rows[0]["PID"].ToString());
                    payT=Convert.ToDouble(dtPay.Rows[0]["PID"].ToString());
                    SetCellText(sheet, 43, 15, "0.00");
                }
                else
                {
                    if (dtOrder.Rows[0]["BillCountry"].ToString() == "United States" || dtOrder.Rows[0]["BillCountry"].ToString() == "U.S.A.")
                    {

                        SetCellText(sheet, 38, 0, "");
                        SetCellText(sheet, 39, 0, "");
                        SetCellText(sheet, 40, 0, "");
                        SetCellText(sheet, 41, 0, "");
                        SetCellText(sheet, 43, 0, "");
                        SetCellText(sheet, 42, 0, "");

                    }
                }
            }
            SetCellText(sheet, 43, 15,Convert.ToString(sumTotal - payT));

            using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);

                sheet = null;
                workbook = null;
            }

            //    // 隐藏多余行
            //    for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            //    {
            //        IRow dataRowD = sheet.GetRow(i);
            //        dataRowD.Height = 0;
            //        dataRowD.ZeroHeight = true;
            //        //sheet.RemoveRow(dataRowD);
            //    }

            //    foreach (DataRow row in dtSource.Rows)
            //    {
            //        #region 填充内容
            //        IRow dataRow = sheet.GetRow(rowIndex);

            //        int columnIndex = 1;        // 开始列（0为标题列，从1开始）
            //        foreach (DataColumn column in dtSource.Columns)
            //        {
            //            // 列序号赋值
            //            if (columnIndex >= dtSource.Columns.Count)
            //                break;

            //            ICell newCell = dataRow.GetCell(columnIndex);
            //            if (newCell == null)
            //                newCell = dataRow.CreateCell(columnIndex);

            //            string drValue = row[column].ToString();

            //            switch (column.DataType.ToString())
            //            {
            //                case "System.String"://字符串类型
            //                    newCell.SetCellValue(drValue);
            //                    break;
            //                case "System.DateTime"://日期类型
            //                    DateTime dateV;
            //                    DateTime.TryParse(drValue, out dateV);
            //                    newCell.SetCellValue(dateV);

            //                    break;
            //                case "System.Boolean"://布尔型
            //                    bool boolV = false;
            //                    bool.TryParse(drValue, out boolV);
            //                    newCell.SetCellValue(boolV);
            //                    break;
            //                case "System.Int16"://整型
            //                case "System.Int32":
            //                case "System.Int64":
            //                case "System.Byte":
            //                    int intV = 0;
            //                    int.TryParse(drValue, out intV);
            //                    newCell.SetCellValue(intV);
            //                    break;
            //                case "System.Decimal"://浮点型
            //                case "System.Double":
            //                    double doubV = 0;
            //                    double.TryParse(drValue, out doubV);
            //                    newCell.SetCellValue(doubV);
            //                    break;
            //                case "System.DBNull"://空值处理
            //                    newCell.SetCellValue("");
            //                    break;
            //                default:
            //                    newCell.SetCellValue("");
            //                    break;
            //            }
            //            columnIndex++;
            //        }
            //        #endregion

            //        rowIndex++;
            //    }


            //    // 格式化当前sheet，用于数据total计算
            //    sheet.ForceFormulaRecalculation = true;

            ////    #region Clear "0"
            //    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //    int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case   HSSFCellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(HSSFCellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case HSSFCellType.BLANK:

            //                    case HSSFCellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            //#endregion



            //using (MemoryStream ms = new MemoryStream())
            //{
            //    workbook.Write(ms);
            //    ms.Flush();
            //    ms.Position = 0;
            //    sheet = null;
            //    workbook = null;


            //    //sheet.Dispose();
            //    //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
            //    return ms;
            //}
        }


    }
}
