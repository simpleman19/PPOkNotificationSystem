using System.Web.Mvc;
using PPOk_Notifications.Service;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Controllers
{
    [AllowAnonymous]
    public class TwilioResponseController : BaseController
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
            var db = new SQLService();

            System.Diagnostics.Debug.WriteLine("SMS Response" + " " + Request["from"] + " " +  Request["body"]);
            if (Request["body"] == "Yes")
            {
                User user = db.GetUserByPhoneActive(Request["from"]);
                Patient pat = db.GetPatientByUserIdActive(user.UserId);
                var notifications = db.GetNotificationsByPatientId(pat.PatientId);
                Notification newest = notifications[0];
                foreach (var n in notifications)
                {
                    if (newest.SentTime < n.SentTime)
                    {
                        newest = n;
                    }
                }
                newest.NotificationResponse = Request["body"];
                Refill refill = db.GetRefillByPrescriptionId(db.GetPrescriptionByPatientId(pat.PatientId).PrecriptionId);
                refill.RefillIt = true;
                db.RefillUpdate(refill);

            }
            var messagingResponse = new MessagingResponse();
            messagingResponse.Message("Thanks, your prescription will be ready shortly");

            return new TwiMLResult(messagingResponse);
        }
    }
}