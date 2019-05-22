using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirbaseAndroidPanel.Models;
using MySql.Data.MySqlClient;

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
            var result = _contextFirebaseEntities.Database.ExecuteSqlCommand("sp_InsertToken_ForAllApps @msisdn,@serviceName,@token,@DeviceManufacture,@DeviceModel",
                new SqlParameter("@msisdn", allApps.MSISDN),
                new SqlParameter("@serviceName", allApps.ServiceName),
                new SqlParameter("@token", allApps.Token),
                new SqlParameter("@DeviceManufacture", allApps.DeviceManufacture ?? ""),
                new SqlParameter("@DeviceModel", allApps.DeviceModel??"")
                );
            return Ok(new
            {
                result = "Success"
            });
        }

        [HttpPost]
        public IHttpActionResult SavePushMessage(tbl_PushMessage_ForAllApps allApps)
        {
           
            if (string.IsNullOrWhiteSpace(allApps.Body) || string.IsNullOrWhiteSpace(allApps.Title))
            {
                return Ok(new
                {
                    result="Failed"
                });
            }
            else
            {
                if (allApps.ServiceName.ToLower().Contains("song"))
                {
                   // DelPreviousInsertNewFromMYSql();
                }
               
                allApps.TimeStamp = DateTime.Now;
                allApps.ServiceName = "Songster";
                _contextFirebaseEntities.tbl_PushMessage_ForAllApps.Add(allApps);
                _contextFirebaseEntities.SaveChanges();
                var aa = allApps.Id;
                _contextFirebaseEntities.Database.ExecuteSqlCommand("sp_InsertIntoDesktopTableRenew_ForAllApps_SongSter @id",
                    new SqlParameter("@id", aa));
                return Ok(new
                {
                    result = "Success"
                });
            }
           
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


        [HttpPost]
        public object SendLog([FromBody] TokenModel tokenModel)
        {
            var SendLog = new SendLogTables_ForAllApps()
            {
                RefId = tokenModel.Id,
                Token = tokenModel.token,
                TimeStamp = DateTime.Now
            };
            _contextFirebaseEntities.SendLogTables_ForAllApps.Add(SendLog);
            _contextFirebaseEntities.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted,
                ""
            );
        }

        [HttpPost]
        public object FailedLog([FromBody] TokenModel s)
        {
            var FailedLog = new FailedLogs_ForAllApps()
            {
                RefId = s.Id,
                Token = s.token,
                TimeStamp = DateTime.Now
            };
            _contextFirebaseEntities.FailedLogs_ForAllApps.Add(FailedLog);
            _contextFirebaseEntities.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted,
                ""
            );
        }
        [HttpPost]
        public object DeactiveToken([FromBody] TokenModel s)
        {
            var dTokenInfois = _contextFirebaseEntities.Firebase_TokenInfo_ForAllApps.Where(f => s.token.Contains(f.Token)).ToList();
            dTokenInfois.ForEach(a => a.IsActive = 0);
            _contextFirebaseEntities.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted,
                ""
            );
        }
        [HttpGet]
        public void DelPreviousInsertNewFromMYSql()
        {
            //string delsong =
            //    "delete FROM [Firebase].[dbo].[Firebase_TokenInfo_ForAllApps] where ServiceName = 'Songster'";
            //new CDA().ExecuteNonQuery(delsong, "fb");
            string connStr = "server=35.247.130.118;user=song;database=vumate;port=3306;password=VuS@ngstar;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                //Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT ServiceName,token FROM tbl_tokenlog";
                var ds = new DataSet();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow VARIABLE in ds.Tables[0].Rows)
                {
                    string ssd = "exec sp_InsertMySql '" + VARIABLE.ItemArray[0] + "','" + VARIABLE.ItemArray[1] + "'";

                    //string ss =
                    //    "INSERT into Firebase_TokenInfo_ForAllApps (ServiceName,MSISDN,Token,TimeStamp,IsActive,AppType,DeviceModel,DeviceManufacture) VALUES ('Songster','','" +
                    //    VARIABLE.ItemArray[1] + "',GETDATE(),1,'"+VARIABLE.ItemArray[0]+"','','')";
                    new CDA().ExecuteNonQuery(ssd, "fb");
                }
                // cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");
            
            // return Ok();
        }
    }
}
