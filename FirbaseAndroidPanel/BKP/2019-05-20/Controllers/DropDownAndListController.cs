using FirbaseAndroidPanel.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace FirbaseAndroidPanel.Controllers
{

    public class DropDownAndListController : ApiController
    {
        private WapPortal_CMSEntities _context;
        private FirebaseEntities _contextFirebaseEntities;
        private HoiChoiEntities _hoiChoiEntities;

        public DropDownAndListController()
        {
            _context = new WapPortal_CMSEntities();
            _contextFirebaseEntities = new FirebaseEntities();
            _hoiChoiEntities = new HoiChoiEntities();


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
            var ddlForPortalName = _context.tbl_PortalMaster.Where(x => x.ParentCode == (string.IsNullOrEmpty(SubCatCode) ? CatCode : SubCatCode)).Select(x => new
            {
                Id = x.PortalCode,
                Name = x.Title
            }).ToList();
            return Ok(ddlForPortalName);
        }

        [HttpPost]
        public object ContentListSongster(ContentListSongster contentList)
        {
            var uid = contentList.Uid;
            var caption = contentList.Caption;
            var listOfContents = MySqlList(uid, caption);
            return Ok(listOfContents);

            return Ok();

        }

        [HttpPost]
        public object ContentList(ContentListSongster contentList)
        {

            if (contentList.PortalCode == "stk")
            {
                var listOfContents = _context.Database.SqlQuery<sp_GetContentList_Result>("sp_GetContentList_stk").ToList();
                return Ok(listOfContents);
            }
            else if (contentList.PortalCode == "sb")
            {
                var listOfContents = _context.Database.SqlQuery<sp_GetContentList_Sb1_Result>("EXEC sp_GetContentList_Sb").ToList();
                return Ok(listOfContents);
            }
            else if (contentList.PortalCode == "lp")
            {
                var listOfContents = _hoiChoiEntities.Database.SqlQuery<sp_GetGameData_Result>("EXEC sp_GetGameData").ToList();
                return Ok(listOfContents);
            }
            else if (contentList.PortalCode == "song")
            {
               // var listOfContents = MySqlList();
                return Ok();
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

        public List<sp_GetContentList_MySql> MySqlList(string userid, string caption)
        {
            string connStr = "server=35.247.130.118;user=song;database=vumate;port=3306;password=VuS@ngstar;";
            MySqlConnection conn = new MySqlConnection(connStr);
            var raw = new List<sp_GetContentList_MySql>();

            try
            {
                //Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"SELECT id AS ContentCode,
                            CASE 
                            WHEN `DuetThumbnail` = '' THEN `ThumbnailUrl` 
                            ELSE  DuetThumbnail 
                            END AS imageUrl , 
                            Description AS ContentTitle
                            FROM tbl_video
                            WHERE (LCASE(Description) LIKE '%" + caption + "%')"+
                            "AND UserId LIKE '%"+ userid + "%'"+
                            "ORDER BY VIEW DESC LIMIT 20";
                var ds = new DataSet();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow VARIABLE in ds.Tables[0].Rows)
                {
                    var a = 
                        new sp_GetContentList_MySql()
                        {
                            ContentCode = VARIABLE[0].ToString(),
                            imageUrl = VARIABLE[1].ToString(),
                            ContentTitle = VARIABLE[2].ToString()
                        };
                    raw.Add(a);
                }

                return raw;
                // cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return raw;

        }
        public  class sp_GetContentList_MySql
        {
            public string ContentCode { get; set; }
            public string ContentTitle { get; set; }
            public string imageUrl { get; set; }
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


