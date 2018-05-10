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

    public class DropDownAndListController : ApiController
    {
        private WapPortal_CMSEntities _context;
        private FirebaseEntities _contextFirebaseEntities;

        public DropDownAndListController()
        {
            _context = new WapPortal_CMSEntities();
            _contextFirebaseEntities =new FirebaseEntities();
                
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            _contextFirebaseEntities.Dispose();
        }


        [HttpPost]
        public object CatAndSubCat(CatSubCat cat)
        {
            var CatCode = cat.CatCode;
            var SubCatCode = cat.SubCatCode;
            var ddlForPortalName = _context.tbl_PortalMaster.Where(x => x.ParentCode== (string.IsNullOrEmpty(SubCatCode) ? CatCode : SubCatCode)).Select(x => new
            {
                Id = x.PortalCode,
                Name = x.Title
            }).ToList();
            return Ok(ddlForPortalName);
        }

        [HttpPost]
        public object ContentList(ContentList contentList)
        {
            if (contentList.PortalCode == "stk")
            {
                var listOfContents = _context.Database.SqlQuery<sp_GetContentList_Result>("sp_GetContentList_stk").ToList();
                return Ok(listOfContents);
            }
            else
            {
                var listOfContents = _context.Database.SqlQuery<sp_GetContentList_Result>("sp_GetContentList @portalcode,@catcode",
                    new SqlParameter("@portalcode", contentList.PortalCode ?? "123"),
                    new SqlParameter("@catcode", contentList.CatCode ?? "123")
                ).ToList();
                return Ok(listOfContents);
            }
           
          
        }
        [HttpGet]
        public object FirebasePortalNames()
        {

            var firbasePortalName = _contextFirebaseEntities.Firebase_TokenInfo_ForAllApps.Select(x => new
            {
                Name = x.ServiceName,
                Id = x.ServiceName
            }).Distinct().ToList();
          
         
            return Ok(firbasePortalName);
        }
    }
}
