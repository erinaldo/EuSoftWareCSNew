using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using FileHelper;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace EuSoft.Common
{
    public class PDF
    {
        List<string> jnPdfName = new List<string>();
        Operater op = new Operater();
        /// <summary>
        /// pdf合并
        /// </summary>
        /// <param name="filePath">面单pdf位置</param>
        /// <param name="fileInviocePath">发票pdf位置</param>
        /// <param name="FilePathHbZj">合并中间位置</param>
        /// <param name="trkNO">快递单号</param>
        /// <param name="outPath">合并后位置</param>
        /// <returns></returns>
        public string hbPdfPath(string filePath, string fileInviocePath, string FilePathHbZj, string trkNO, string outPath)
        {
            string path = "";


            ConvertPDFToPDF(filePath, String.Format("{0}{1}.pdf", FilePathHbZj, trkNO));

            ConvertPDFToPDF(fileInviocePath, String.Format("{0}{1}-CI.pdf", FilePathHbZj, trkNO));

            //string[] sPdf = new string[] { @"c:\785898988517.pdf", @"c:\785898988517-CI.pdf" };
            //MergePDFFiles(sPdf, @"c:\785898988518.pdf");

            jnPdfName.Add(String.Format("{0}{1}.pdf", FilePathHbZj, trkNO));
            jnPdfName.Add(String.Format("{0}{1}-CI.pdf", FilePathHbZj, trkNO));


            op.MergerFile(String.Format("{0}{1}.pdf", outPath, trkNO), "pdf", jnPdfName.ToArray(), null);
            path = String.Format("{0}{1}.pdf", outPath, trkNO);

            File.Copy(path, filePath, true);
            return filePath;
        }


        /// <summary>
        /// 实现PDF复制
        /// </summary>
        /// <param name="filePath">源PDF文件</param>
        /// <param name="toPath">目标PDF文件</param>

        public void ConvertPDFToPDF(string filePath, string toPath)
        {
            PdfReader reader = new PdfReader(filePath);
            Document document = new Document(reader.GetPageSizeWithRotation(1));
            int n = reader.NumberOfPages;
            FileStream baos = new FileStream(toPath, FileMode.Create, FileAccess.Write);
            PdfCopy copy = new PdfCopy(document, baos);
            copy.ViewerPreferences = PdfWriter.HideToolbar | PdfWriter.HideMenubar;
            //往pdf中写入内容   
            document.Open();
            if (filePath.IndexOf("CI") > 0)
            {
                for (int j = 0; j < 3; j++)
                {
                    PdfImportedPage page = copy.GetImportedPage(reader, 1);
                    copy.AddPage(page);
                }
            }
            else
            {
                for (int i = 1; i <= n; i++)
                {
                    if (i == 1)
                    {
                        PdfImportedPage page = copy.GetImportedPage(reader, i);
                        copy.AddPage(page);
                    }
                    else
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            PdfImportedPage page = copy.GetImportedPage(reader, 2);
                            copy.AddPage(page);
                        }

                    }

                }
            }


            document.Close();
            reader.Close();
        }
    }
}
