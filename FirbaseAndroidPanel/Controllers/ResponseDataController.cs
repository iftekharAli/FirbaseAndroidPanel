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
    public class ResponseDataController : ApiController
    {
        private WapPortal_CMSEntities _context;

        public ResponseDataController()
        {
            _context = new WapPortal_CMSEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        public object BdtubeResult(HitBackModel backModel)
        {
            var Result = _context.Database.SqlQuery<sp_getFirbaseBdtubeInfo_Result>(
                "sp_getFirbaseBdtubeInfo @contentcode,@id,@refid",
                new SqlParameter("@contentcode", backModel.ContentCode),
                new SqlParameter("@id", backModel.Id),
                new SqlParameter("@refid", backModel.RefId)
            ).ToList();

            return Ok(new
            {
                NotificationResult= Result
            });
        }

        [HttpPost]
        public object ClubzResult(HitBackModel backModel)
        {
            var Result = _context.Database.SqlQuery<sp_getFirbaseClubzInfo_Result>(
                "sp_getFirbaseClubzInfo @contentcode,@id,@refid",
                new SqlParameter("@contentcode", backModel.ContentCode),
                new SqlParameter("@id", backModel.Id),
                new SqlParameter("@refid", backModel.RefId)
            ).ToList();

            return Ok(new
            {
                NotificationResult = Result
            });
        }
    }
}
