using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers
{
    public class TwilioController : Controller
    {
        // GET: Twilio
        public ActionResult Index()
        {
            var twilio = new TwilioApi(new Models.Pharmacy());
            twilio.SendTextMessage(Models.Notification.GetTestNotification());
            return View();
        }
    }
}