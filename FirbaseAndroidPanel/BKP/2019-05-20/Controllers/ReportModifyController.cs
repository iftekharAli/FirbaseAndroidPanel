using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirbaseAndroidPanel.Models;

namespace FirbaseAndroidPanel.Controllers
{
    public class ReportModifyController : Controller
    {
        private FirebaseEntities _contextFirebaseEntities;

        public ReportModifyController()
        {
            _contextFirebaseEntities=new FirebaseEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _contextFirebaseEntities.Dispose();
        }
        // GET: ReportModify
        public ActionResult Index()
        {
            if (Session["Uid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var Data = _contextFirebaseEntities.Database.SqlQuery<DesktopNew>("EXEC Firebase.dbo.sp_getPushData_ForAllApps").ToList();
            return View(Data);
        }

        public ActionResult Delete(long id)
        {
            if (Session["Uid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            _contextFirebaseEntities.Database.ExecuteSqlCommand("EXEC Firebase.dbo.sp_DeletePushMessage_ForAllApps @id", new SqlParameter("@id", id));
            _contextFirebaseEntities.Database.ExecuteSqlCommand("EXEC Firebase.dbo.sp_DeletePushMessageFromTokenInfo_ForAllApps @id", new SqlParameter("@id", id));
            return RedirectToAction("Index");
        }
    }
}