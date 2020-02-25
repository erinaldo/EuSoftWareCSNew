using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Collections;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using DevExpress.Utils;
namespace Medical.Yottor.UI
{
    public class MyGridLookupDataSourceHelper
    {

        MyObject _MyObject = new MyObject();
        private MyDataSourceWrapper _DataSourceWrapper;
        GridLookUpEdit edit;
        bool popupOpened = false;
        public MyGridLookupDataSourceHelper(GridLookUpEdit edit, ITypedList dataSource, string displayMember, string valueMember)
        {
            this.edit = edit;
            _DataSourceWrapper = new MyDataSourceWrapper(dataSource, _MyObject, valueMember, displayMember);
            edit.Properties.DisplayMember = displayMember;
            edit.Properties.ValueMember = valueMember;
            edit.Properties.DataSource = _DataSourceWrapper;
            edit.Properties.View.CustomRowFilter += View_CustomRowFilter;
            edit.ProcessNewValue += edit_ProcessNewValue;
            edit.Properties.View.RefreshData();
            edit.Properties.QueryPopUp += new CancelEventHandler(Properties_QueryPopUp);
        }

        void Properties_QueryPopUp(object sender, CancelEventArgs e)
        {
            this.popupOpened = true;
            edit.Properties.View.DataController.DoRefresh();
        }

        public static void SetupGridLookUpEdit(GridLookUpEdit edit, ITypedList dataSource, string displayMember, string valueMember)
        {
            new MyGridLookupDataSourceHelper(edit, dataSource, displayMember, valueMember);
        }

        void View_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if (!popupOpened) return;
            if (_DataSourceWrapper[e.ListSourceRow] is MyObject)
            {
                e.Visible = false;
                e.Handled = true;
            }
        }

        void edit_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            _MyObject.Value = e.DisplayValue;
            this.popupOpened = false;
            edit.Properties.View.DataController.DoRefresh();
            e.Handled = true;
        }
    }
}
