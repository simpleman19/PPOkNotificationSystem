using System.Web.Mvc;
using PPOk_Notifications.Filters;
using PPOk_Notifications.Service;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using PPOk_Notifications.Models;

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
            if (Request["body"].ToLower() == "yes")
            {
                var user = DatabaseUserService.GetByPhoneActive(Request["from"]);
                var pat = DatabasePatientService.GetByUserIdActive(user.UserId);
                var notifications = DatabaseNotificationService.GetByPatientId(pat.PatientId);
                var newest = notifications[0];
                foreach (var n in notifications)
                {
                    if (newest.SentTime < n.SentTime)
                    {
                        newest = n;
                    }
                }
                newest.NotificationResponse = Request["body"];
                var refill = DatabaseRefillService.GetByPrescriptionId(DatabasePrescriptionService.GetByPatientId(pat.PatientId).PrecriptionId);
                refill.RefillIt = true;
                DatabaseRefillService.Update(refill);

            }
            var messagingResponse = new MessagingResponse();
            messagingResponse.Message("Thanks, your prescription will be ready shortly");

            return new TwiMLResult(messagingResponse);
        }
    }
}