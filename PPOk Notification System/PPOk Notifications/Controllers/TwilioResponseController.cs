﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPOk_Notifications.Service;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;

namespace PPOk_Notifications.Controllers
{
    public class TwilioResponseController : Controller
    {
        // GET: Twilio
        public ActionResult Index()
        {
            var twilio = new TwilioApi(new Models.Pharmacy());
            twilio.SendTextMessage(Models.Notification.GetTestNotification());
            return View();
        }

        [HttpPost]
        public ActionResult SmsResponse()
        {
            System.Diagnostics.Debug.WriteLine("SMS Response" + " " + Request["from"] + " " +  Request["body"]);
            var messagingResponse = new MessagingResponse();
            messagingResponse.Message("Thanks, your prescription will be ready shortly");

            return new TwiMLResult(messagingResponse);
        }
    }
}