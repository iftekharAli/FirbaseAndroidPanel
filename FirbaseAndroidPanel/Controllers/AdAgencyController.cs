using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirbaseAndroidPanel.Models;

namespace FirbaseAndroidPanel.Controllers
{
    public class AdAgencyController : ApiController
    {
        private FirebaseEntities _contextFirebaseEntities;

        public AdAgencyController()
        {
            _contextFirebaseEntities = new FirebaseEntities();
        }

        
        [HttpPost]
        public object AdAgencyLog([FromBody] AddLog addLog)
        {
            _contextFirebaseEntities.tbl_AdAgencyLog.Add(new tbl_AdAgencyLog()
            {
                AddUrl = addLog.LogUrl,
                ServiecName = addLog.ServiecName,
                TimeStamp = DateTime.Now
            });
            _contextFirebaseEntities.SaveChanges();

            return Ok(new
            {
                result = "Success"
            });
        }
    }
}
