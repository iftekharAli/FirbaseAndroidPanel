using System;
using System.Collections.Generic;
using System.Data;
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
            DataSet ds = new CDA().GetDataSet("exec [WapPortal_CMS].[dbo].spUserLogin '" + lg.UserName + "','" + lg.Password + "'", "HR");
            if (ds != null)
            {
                Session["Uid"] = lg.UserName;

                return RedirectToAction("Index", "Report");
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