using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirbaseAndroidPanel.Controllers
{
    public class TokenManageController : ApiController
    {
        private FirebaseEntities _contextFirebaseEntities;

        public TokenManageController()
        {
            _contextFirebaseEntities = new FirebaseEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _contextFirebaseEntities.Dispose();
        }

        [HttpPost]
        public IHttpActionResult SaveToken(Firebase_TokenInfo_ForAllApps allApps)
        {
            // var 
            var result = _contextFirebaseEntities.Database.ExecuteSqlCommand("sp_InsertToken_ForAllApps @msisdn,@serviceName,@token",
                new SqlParameter("@msisdn", allApps.MSISDN),
                new SqlParameter("@serviceName", allApps.ServiceName),
                new SqlParameter("@token", allApps.Token)
                );
            return Ok(new
            {
                result = "Success"
            });
        }

        [HttpPost]
        public IHttpActionResult SavePushMessage(tbl_PushMessage_ForAllApps allApps)
        {
            //var id =  _contextFirebaseEntities.tbl_PushMessage_ForAllApps.Add(new tbl_PushMessage_ForAllApps()
            // {
            //    ServiceName = allApps.ServiceName,
            //     Title = allApps.Title,
            //     Body = allApps.Body,
            //     ContentCode = allApps.ContentCode,
            //     ImageUrl = allApps.ImageUrl,
            //     ProcessTime = allApps.ProcessTime,
            //     TimeStamp = DateTime.Now

            // });
            allApps.TimeStamp = DateTime.Now;
            _contextFirebaseEntities.tbl_PushMessage_ForAllApps.Add(allApps);
            _contextFirebaseEntities.SaveChanges();
            var aa = allApps.Id;
            _contextFirebaseEntities.Database.ExecuteSqlCommand("sp_InsertIntoDesktopTableRenew_ForAllApps @id",
                new SqlParameter("@id", aa));
            return Ok();
        }

        [HttpPost]
        public object UrlClickLog(UrlClickLogs_ForAllApps allApps)
        {
            allApps.TimeStamp=DateTime.Now;
            _contextFirebaseEntities.UrlClickLogs_ForAllApps.Add(allApps);
            _contextFirebaseEntities.SaveChanges();
            return Ok(new
            {
                result = "success"
            });
        }
    }
}
