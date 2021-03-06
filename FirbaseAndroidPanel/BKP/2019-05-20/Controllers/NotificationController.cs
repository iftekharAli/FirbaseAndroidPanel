﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirbaseAndroidPanel.Controllers
{
    public class NotificationController : Controller
    {
        private  WapPortal_CMSEntities _context;
        private FirebaseEntities _contextFirebaseEntities;


        public NotificationController()
        {
            _context=new  WapPortal_CMSEntities();
            _contextFirebaseEntities = new FirebaseEntities();
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            _contextFirebaseEntities.Dispose();
        }

        [HttpGet]

        // GET: Notification
        public ActionResult Index()
        {
            if (Session["Uid"] == null)
            {
                 return RedirectToAction("Index", "Login");
            }
            string[] portalCodes = new[]
            {
                "EE9D65C0-A155-464C-A41F-D6FAF01D4B88",
                "D17D271F-3947-4820-84D2-EBA9A271CC6B",
              //  "0AF10DC9-EF8E-473A-AED4-0F6FA4E89BCE",//bdtube for robi
                "884A44E2-9AD2-4677-997D-CE558EF8E522",
                "600F213C-B834-4B99-AA56-63C0487D1C92",
               // "82008548-E6DC-4352-9F9E-1794084E1791"//shabox buddy
            };
            var ddlForPortalName = _context.tbl_PortalMaster.Where(x => portalCodes.Contains(x.PortalCode)).Select(x=>new
            {
                Id=x.PortalCode,
                Name = x.Title
            }).ToList();
          
            ddlForPortalName.Insert(4, new {Id = "stk", Name = "Amar Sticker" });
            ddlForPortalName.Insert(5, new {Id = "sb", Name = "Buddy" });
            ddlForPortalName.Insert(6, new {Id = "lp", Name = "Let's Play" });
            ddlForPortalName.Insert(7, new {Id = "quizstar", Name = "QuizStar" });
            ddlForPortalName.Insert(8, new {Id = "song", Name = "Songster" });
            ViewData["PortalNames"] = ddlForPortalName;
           
            return View();
        }

        [HttpGet]
        public ActionResult Songster()
        {
            if (Session["Uid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string[] portalCodes = new[]{""};
            var ddlForPortalName = _context.tbl_PortalMaster.Where(x => portalCodes.Contains(x.PortalCode)).Select(x => new
            {
                Id = x.PortalCode,
                Name = x.Title
            }).ToList();

            //ddlForPortalName.Insert(4, new { Id = "stk", Name = "Amar Sticker" });
            //ddlForPortalName.Insert(5, new { Id = "sb", Name = "Buddy" });
            //ddlForPortalName.Insert(6, new { Id = "lp", Name = "Let's Play" });
            //ddlForPortalName.Insert(7, new { Id = "quizstar", Name = "QuizStar" });
            ddlForPortalName.Insert(0, new { Id = "song", Name = "Songster" });
            ViewData["PortalNames"] = ddlForPortalName;
            return View();
        }
    }
}