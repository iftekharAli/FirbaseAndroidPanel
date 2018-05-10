using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirbaseAndroidPanel.Models;

namespace FirbaseAndroidPanel.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Logins lg)
        {
            if (lg.UserName.ToUpper() == "ADMIN" && lg.Password == "vuadmin")
            {
                Session["Uid"] = lg.UserName;

                return RedirectToAction("Index", "Notification");
            }
            else
            {
                ModelState.AddModelError("", @"Invalid user Name/Password");
                return View(lg);

            }


        }
        public ActionResult Logout()
        {
            Session["Uid"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}