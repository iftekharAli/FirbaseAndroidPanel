using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirbaseAndroidPanel.Models;

namespace FirbaseAndroidPanel.Controllers
{
    public class SongsterReportController : Controller
    {
        // GET: SongsterReport
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetResult(string fromDate, string toDate)
        {
            DataSet dataSet = null;

            var clickQuery = "SELECT CAST(TimeStamp as date) date, COUNT(*) click " +
                        " from UrlClickLogs_ForAllApps" +
                        " where CAST(TimeStamp as date) between '"+fromDate+"' and '"+toDate+"' " +
                        " group by CAST(TimeStamp as date) " +
                        " order by CAST(TimeStamp as date) desc";

            var sendQuery = " SELECT CAST(TimeStamp as date) date, COUNT(Id) Send " +
                        " FROM SendLogTables_ForAllApps " +
                        " where CAST(TimeStamp as date) between '"+fromDate+"' and '"+toDate+"' " +
                        " group by CAST(TimeStamp as date) " +
                        " order by CAST(TimeStamp as date) desc";

            List<Click> clicks = new List<Click>();
            List<Send> sends = new List<Send>();

            string db = "fb";

            dataSet = new CDA().GetReportData(clickQuery, db);

            if (dataSet != null)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Click click = new Click();
                    click.Id = clicks.Count+1;
                    click.TimeStamp = row["date"].ToString();
                    click.Count = Int32.Parse(row["click"].ToString());
                    clicks.Add(click);
                }
            }

            dataSet = null;

            dataSet = new CDA().GetReportData(sendQuery, db);
             
            if (dataSet != null)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Send send = new Send();
                    send.Id = clicks.Count + 1;
                    send.TimeStamp = row["date"].ToString();
                    send.Count = Int32.Parse(row["Send"].ToString());
                    sends.Add(send);
                }
            }

            var q =
                from c in clicks
                join s in sends on c.TimeStamp equals s.TimeStamp into ps
                from p in ps.DefaultIfEmpty()  
                select new { TimeStamp = c , Send = p == null ? "(No send)" : p.Count.ToString()};

            return Json( q, JsonRequestBehavior.AllowGet);
        }

    }
}