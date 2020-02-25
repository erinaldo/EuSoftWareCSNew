using Agile.Report;
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
    public partial class frmPrintLable : Form
    {
        EuSoft.BLL.MCEStock bMCEStock = new EuSoft.BLL.MCEStock();
        public frmPrintLable()
        {
            InitializeComponent();
        }

        public void printsmall(DataTable dt, string reportPath)
        {
            //StockCSNo	StockSize	StockUnit	StockValCode	StockBatchNo storage
            ReportEx report = new ReportEx();
            var dtRow = dt.AsEnumerable().Where<DataRow>(x => x["StockBatchNo"].ToString() == txtLot.Text);
            foreach (DataRow item in dtRow)
            {

                report.AddParameter("valcode", item["StockValCode"].ToString());
                report.AddParameter("CS_No", item["StockCSNo"].ToString());
                report.AddParameter("size", item["StockSize"].ToString());
                report.AddParameter("name", item["DrugNames"].ToString());
                report.AddParameter("cas", item["cas"].ToString());
                report.AddParameter("mwt", item["mwt"].ToString());
                report.AddParameter("lot", item["StockBatchNo"].ToString());
                report.AddParameter("pruity", item["pruity"].ToString());
                report.AddParameter("Storage", item["Storage"].ToString());
                report.LoadFrom(System.IO.Path.Combine(reportPath, "lysmall.frx"));
                report.Print(false, "");
            }


        }
        private void txtValCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtValCode.Text != "" && e.KeyChar == 13)
            {


                DataTable dt = new DataTable();
                dt = bMCEStock.GetList("StockValCode =  '" + txtValCode.Text + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {


                    txtSize.Text = dt.Rows[0]["StockSize"].ToString() + dt.Rows[0]["StockUnit"].ToString();
                    txtLot.Text = dt.Rows[0]["StockBatchNo"].ToString();

                    var dtRow = dt.AsEnumerable().Where<DataRow>(x => x["StockBatchNo"].ToString() == txtLot.Text);
                    foreach (DataRow item in dtRow)
                    {
                        txtCs_No.Text = item["StockCSNo"].ToString();
                        txtPruty.Text = item["pruity"].ToString();
                        txtStorage.Text = item["Storage"].ToString();
                        txtDrug_Names.Text = item["DrugNames"].ToString();
                        txtCAS.Text = item["cas"].ToString();
                        txtMwt.Text = item["mwt"].ToString();
                    }




                }

            }


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ReportEx report = new ReportEx();



            report.AddParameter("valcode", txtValCode.Text);
            report.AddParameter("CS_No", txtCs_No.Text);
            report.AddParameter("size", txtSize.Text);
            report.AddParameter("name", txtDrug_Names.Text);
            report.AddParameter("cas", txtCAS.Text);
            report.AddParameter("mwt", txtMwt.Text);
            report.AddParameter("lot", txtLot.Text);
            report.AddParameter("pruity", txtPruty.Text);
            report.AddParameter("Storage", txtStorage.Text);
            report.LoadFrom(System.IO.Path.Combine(Application.StartupPath, "lysmall.frx"));
            report.Print(false, "");


        }
    }
}
