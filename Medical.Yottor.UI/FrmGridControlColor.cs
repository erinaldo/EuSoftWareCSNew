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
    public partial class FrmGridControlColor : DevExpress.XtraEditors.XtraForm
    {
        public FrmGridControlColor()
        {
            InitializeComponent();
        }

        private void FrmGridControlColor_Load(object sender, EventArgs e)
        {
            List<Student> list = new List<Student>();
            list.Add(new Student() { Name = "小龙", Age = 3 });
            list.Add(new Student() { Name = "小虎", Age = 9 });
            list.Add(new Student() { Name = "小兔", Age = 26 });
            list.Add(new Student() { Name = "小猫", Age = 52 });
            list.Add(new Student() { Name = "小狗", Age = 83 });
            this.gridControl1.DataSource = list;
          
            
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
