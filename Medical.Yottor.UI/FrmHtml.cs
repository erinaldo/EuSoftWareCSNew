using System;
using System.Windows.Forms;
using Medical.Yottor.Domain;
using ExpertPdf.HtmlToPdf;
using System.IO;
using System.Drawing.Imaging;

namespace Medical.Yottor.UI
{
    public partial class FrmHtml : DevExpress.XtraEditors.XtraForm
    {
        public FrmHtml()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 根据 Html 模板，发送邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmail_Click(object sender, EventArgs e)
        {
            //原模板内容
            string html = GenerateHtmlHelper.ReadHtml(Application.StartupPath + @"\chemense.html");

            //需要替换的内容
            string newText = string.Empty;
            const string top = "top";
            const string height = "25";
            const string style = "font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333;";
            for (int i = 0; i < 5; i++)
            {
                newText += "<tr>";
                newText += string.Format(@"<td valign=""{0}""  width=""{1}"" height=""{2}"" style=""{3}"">" + "{4}" + "</td>", top, 100, height, style, "hy-10003");
                newText += string.Format(@"<td valign=""{0}""  width=""{1}"" height=""{2}"" style=""{3}"">" + "{4}" + "</td>", top, 320, height, style, "Alfacalcidol");
                newText += string.Format(@"<td valign=""{0}""  width=""{1}"" height=""{2}"" style=""{3}"">" + "{4}" + "</td>", top, 90, height, style, "1 mg");
                newText += string.Format(@"<td valign=""{0}"" align=""center""  width=""{1}"" height=""{2}"" style=""{3}"">" + "{4}" + "</td>", top, 85, height, style, "1");
                newText += string.Format(@"<td valign=""{0}""  width=""{1}"" height=""{2}"" style=""{3}"">" + "{4}" + "</td>", top, 75, height, style, "$100.00");
                newText += "</tr>";
            }

            //替换操作
            html = html.Replace("{#ORDER}", newText);

            //写入操作
            string savepath = Application.StartupPath + @"\chemenseTest.html";
            bool success = GenerateHtmlHelper.WriteHtml(html, savepath);
            if (success)
            {
                string content = GenerateHtmlHelper.ReadHtml(savepath);
                bool isSend = EMailHelper.Send("测试邮件", content, "2154799377@qq.com", true);
                if (isSend)
                    MsgBox.ShowExclamation("发送成功！");
            }
        }

        /// <summary>
        /// 根据 Html 模板,生成 PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPDF_Click(object sender, EventArgs e)
        {
            string savepath = Application.StartupPath + @"\chemenseTest.html";
            bool success = File.Exists(savepath);
            if (success)
            {
                try
                {
                    PdfConverter pdfConverter = new PdfConverter();
                    //pdfConverter.LicenseKey = "Yu Tao";
                    pdfConverter.PdfDocumentOptions.EmbedFonts = false;
                    pdfConverter.PdfDocumentOptions.ShowFooter = false;
                    pdfConverter.PdfDocumentOptions.ShowHeader = false;
                    pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;
                    string outfile = Application.StartupPath + "\\PDF\\HtmlToPdf.pdf";
                    try
                    {
                        pdfConverter.SavePdfFromUrlToFile(savepath, outfile);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.ShowExclamation(ex.Message);
                        return;
                    }

                    MsgBox.ShowExclamation("生成成功！\r\n\r\n生成路径：" + outfile);
                }
                catch(Exception ex)
                {
                    MsgBox.ShowExclamation(ex.Message);
                    return;
                }
            }
        }

        /// <summary>
        /// 根据 Html 模板，生成图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImage_Click(object sender, EventArgs e)
        {
            string savepath = Application.StartupPath + @"\chemenseTest.html";
            bool success = File.Exists(savepath);
            if (success)
            {
                ImgConverter imgConverter = new ImgConverter();
                //imgConverter.LicenseKey = "Yu Tao";
                string outfile = Application.StartupPath + "\\Image\\HtmlToImage.ipeg";
                try
                {
                    imgConverter.SaveImageFromUrlToFile(savepath, ImageFormat.Jpeg, outfile);
                }
                catch (Exception ex)
                {
                    MsgBox.ShowExclamation(ex.Message);
                    return;
                }

                MsgBox.ShowExclamation("生成成功！\r\n\r\n生成路径：" + outfile);
            }
        }
    }
}
