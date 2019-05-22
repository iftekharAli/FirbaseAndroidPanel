using FirbaseAndroidPanel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirbaseAndroidPanel.Controllers
{
    public class SearchController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Songster(Search search)
        {
            //if (Session["Uid"] == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            MysqlConnector conn = new MysqlConnector();
            var data = conn.GetData(search.ownerName, search.songName);



            var d = data;

            //foreach (DataRow item in d.Rows)
            //{
            //    string name = item["SongName"].ToString();
            // }
            //     List<GetStudentSearchModel> dtList = data.AsEnumerable()
            //.Select(row => new GetStudentSearchModel
            //{
            //    Id = row["Id"].ToString()

            //}).ToList();



            return Ok(d);

        }


        [HttpPost]
        public IHttpActionResult pushSave(pushTemp push )
        {
            FirebaseEntities db = new FirebaseEntities();
            tbl_PushMessage_ForAllApps pushMessage = new tbl_PushMessage_ForAllApps();
            tbl_FirebaseSendDesktopNotification_ForAllApps sendPushMsg = new tbl_FirebaseSendDesktopNotification_ForAllApps();

            pushMessage.Title = push.title;
            pushMessage.Body = push.body;
            pushMessage.ImageUrl = push.imgUrl;
            pushMessage.TimeStamp = DateTime.Now;
            pushMessage.ProcessTime = push.sendTime;
            pushMessage.ContentCode = push.id;
            pushMessage.ServiceName = "Songster-Audio-Push";

            db.tbl_PushMessage_ForAllApps.Add(pushMessage);
            db.SaveChanges();
            int refId = (int)pushMessage.Id;
            sendPushMsg.ImageUrl = push.imgUrl;
            sendPushMsg.isSent = 0;
            sendPushMsg.AppType = "Songster_Service_Name";
            sendPushMsg.ServiceName = "Songster_Service_Name";
            sendPushMsg.Refid = refId;
            sendPushMsg.Title = push.title;
            sendPushMsg.Body = push.body;
            sendPushMsg.ProcessTime = push.sendTime;
            sendPushMsg.SendTime = push.sendTime;
            sendPushMsg.TimeStamp = DateTime.Now;
            sendPushMsg.ContentCode = push.id;
            sendPushMsg.Token = "";
            sendPushMsg.MSISDN = "";
            sendPushMsg.PushTblId = refId;
            sendPushMsg.RedirectUrl = "";
            db.tbl_FirebaseSendDesktopNotification_ForAllApps.Add(sendPushMsg);
            db.SaveChanges();

            return Ok();
        }
    }
}
