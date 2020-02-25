using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medical.Yottor.UI
{
    public class MyObject
    {
        public MyObject()
        { }

        private object _Value;
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
}
