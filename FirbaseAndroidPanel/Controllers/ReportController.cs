using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirbaseAndroidPanel.Models;

namespace FirbaseAndroidPanel.Controllers
{
    public class ReportController : Controller
    {
        private FirebaseEntities _context;

        public ReportController()
        {
            _context = new FirebaseEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Report
        public ActionResult Index()
        {
            if (Session["Uid"] == null)
            {
                return RedirectToAction("Index", "Login");

            }
            ViewData["ServiceName"] = _context.Firebase_TokenInfo_ForAllApps.Where(x => x.IsActive == 1).Select(x => new { Name = x.ServiceName, ID = x.ServiceName }).Distinct().ToList();
            ViewData["Type"] = cat.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult Index(Report report)
        {


            return View();
        }
        public List<ListOfService> cat = new List<ListOfService>()
        {
            new ListOfService()
            {
                Id= "Send Log",
                Name = "Send Log"
            },
            new ListOfService()
            {
                Id ="Fail Log",
                Name = "Fail Log"
            },

            new ListOfService()
            {
                Id ="Url Log",
                Name = "Url Log"
            },

        };
       
    }
}