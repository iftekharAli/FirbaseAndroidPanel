using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirbaseAndroidPanel.Models;

namespace FirbaseAndroidPanel.Controllers
{
    public class ReportAngularController : ApiController
    {

        private FirebaseEntities _context;

        public ReportAngularController()
        {
            _context = new FirebaseEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        public object DataByService([FromBody] Report report)
        {
            var Result = _context.Database.SqlQuery<RsultOfSp>("EXEC sp_GetReportData_Android @servicename,@sdate,@edate ",

                    new SqlParameter("@servicename", report.ServiceName ?? "All"),
                    new SqlParameter("@sdate", report.FromDate),
                    new SqlParameter("@edate", report.EndDate)

                ).
                ToList<RsultOfSp>();
            return Result;
        }
    }
}
