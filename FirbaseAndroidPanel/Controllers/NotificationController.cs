using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirbaseAndroidPanel.Controllers
{
    public class NotificationController : Controller
    {
        private WapPortal_CMSEntities _context;

        public NotificationController(WapPortal_CMSEntities context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
          _context.Dispose(); 
        }

        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }
    }
}