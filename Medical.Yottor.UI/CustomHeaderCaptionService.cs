using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraScheduler.Services;
using DevExpress.XtraScheduler.Drawing;

namespace Medical.Yottor.UI
{
    public class CustomHeaderCaptionService : HeaderCaptionServiceWrapper
    {
         public CustomHeaderCaptionService(IHeaderCaptionService service)
            : base(service)
        {
        }

        public override string GetDayColumnHeaderCaption(DayHeader header)
        {
            DateTime date = header.Interval.Start.Date;
            return string.Format("{0:M}({1})", date, date.ToString("dddd",new System.Globalization.CultureInfo("zh-cn")));
        }
    }
}
