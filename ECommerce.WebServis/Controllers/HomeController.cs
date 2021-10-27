using Ecommerce.BLL;
using ECommerce.WebServis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.WebServis.Controllers
{
    public class HomeController : Controller
    {
        public UserManager usermanager = new UserManager();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //SendNotification();
            return View();
        }


        public void SendNotification()
        {
            var users = usermanager.db.Users.Where(t => !string.IsNullOrEmpty(t.UserNotificationToken)).ToList();

            foreach (var item in users)
            {
                ExpoNotificationModel model = new ExpoNotificationModel()
                {
                    body = item.EMail,
                    priority = "high",
                    sound = "default",
                    title = "Toplu Bildirim Atıyoruz",
                    to = item.UserNotificationToken
                };

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://exp.host/--/api/v2/push/send");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";


      

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();


                using (var steramReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = steramReader.ReadToEnd();
                }


                if (httpResponse != null)
                {
                    var statusCode = httpResponse.StatusCode.ToString();
                }

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    //Loglama yapılabilir
                }
            }
        }
    }
}
