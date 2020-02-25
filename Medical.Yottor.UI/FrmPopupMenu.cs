using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Medical.Yottor.UI
{
    public partial class FrmPopupMenu : DevExpress.XtraEditors.XtraForm
    {
        public FrmPopupMenu()
        {
            InitializeComponent();
        }

        private void FrmPopupMenu_Load(object sender, EventArgs e)
        {
            DataTable dt = GetTestData();
            if (dt != null)
            {
                this.gridView1.UpdateCurrentRow();
                this.gridControl1.DataSource = dt;
                this.gridView1.PopulateColumns();

                this.gridView2.UpdateCurrentRow();
                this.gridControl2.DataSource = dt;
                this.gridView2.PopulateColumns();
            }
        }

        private DataTable GetTestData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("MC", typeof(string)));
            dt.Columns.Add(new DataColumn("Note", typeof(string)));

            Random _rm = new Random();
            for (int i = 0; i < 50; i++)
            {
                DataRow _drNew = dt.NewRow();
                _drNew["ID"] = i;
                _drNew["MC"] = "手机" + " - A00" + i;
                _drNew["Note"] = "测试" + _rm.Next(100, 200);
                dt.Rows.Add(_drNew);
            }
            return dt;
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            GridControl grid = sender as GridControl;
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
            {
                GridHitInfo hitInfo = gridView1.CalcHitInfo(e.Location);
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);

                if (hitInfo.InRowCell && hitInfo.Column != null)
                {
                    popupMenu1.ShowPopup(p);
                }
            }
        }
    }
}