using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirbaseAndroidPanel.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class ListOfService
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class RsultOfSp
    {

        public DateTime Date { get; set; }
        public int TotalSend { get; set; }
        public int TotalFailed { get; set; }
        public int TotalClick { get; set; }
    }
}