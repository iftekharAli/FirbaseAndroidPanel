using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirbaseAndroidPanel.Models
{
    public class DesktopNew
    {
        public long Id { get; set; }
        public string ServiceName { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public DateTime ProcessTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}