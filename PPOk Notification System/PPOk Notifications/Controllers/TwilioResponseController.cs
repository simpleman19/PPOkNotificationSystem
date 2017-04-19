using System.Web.Mvc;
using PPOk_Notifications.Filters;
using PPOk_Notifications.Service;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using PPOk_Notifications.Models;
using System.Xml.Linq;
using System.Xml;
using System;
using System.Text;

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

        #region Call

        #region Refill
        public ActionResult Refill(long id)
        {
            var n = DatabaseNotificationService.GetById(id);
            var pat = DatabasePatientService.GetById(n.PatientId);
            Pharmacy p = DatabasePharmacyService.GetById(pat.PharmacyId);
            p.GetTemplates();
            string message = p.GetRefillTemplate().TemplatePhone;
            var xml = new XDocument(
                new XElement("Response",
                    new XElement("Gather",
                        new XAttribute("timeout", "10"),
                        new XAttribute("numDigits", "1"),
                        new XAttribute("action", "https://ocharambe.localtunnel.me/twilioresponse/refillresponse/" + id),
                            new XElement("Say",
                                    message
                                )
                          ),
                    new XElement("Say",
                        "We didn't recieve any input, Goodbye!")
                    )
                );
            return new XmlActionResult(xml);
        }

        public ActionResult RefillResponse(long id)
        {
            string digits = Request["Digits"];
            System.Diagnostics.Debug.WriteLine(Request["To"]);
            var notification = DatabaseNotificationService.GetById(id);
            var user = DatabasePatientService.GetById(notification.PatientId);
            if (digits.Contains("1"))
            {
                XDocument xml = null;
                if (user != null)
                {
                    notification.NotificationResponse = Request["digits"];
                    DatabaseNotificationService.Update(notification);
                    var refill = DatabaseRefillService.GetByPrescriptionId(DatabasePrescriptionService.GetByPatientId(user.PatientId).PrescriptionId);
                    refill.RefillIt = true;
                    DatabaseRefillService.Update(refill);
                    xml = new XDocument(
                            new XElement("Response",
                                new XElement("Say",
                                    "Your refill will be ready shortly.")
                                    )
                        );
                } else
                {
                    xml = new XDocument(
                        new XElement("Response",
                            new XElement("Say",
                                "couldn't find refill")
                                )
                    );
                }
                return new XmlActionResult(xml);
            }
            else if (digits.Contains("9"))
            {
                var xml = new XDocument(
                        new XElement("Response",
                            new XElement("Say",
                                "Connecting you to a pharmacist."),
                        new XElement("Dial",
                        DatabasePharmacyService.GetById(user.PharmacyId).PharmacyPhone)
                        )
                    );
                return new XmlActionResult(xml);
            }
            else
            {
                var xml = new XDocument(
                        new XElement("Response",
                            new XElement("Say",
                                "Unrecognized Input, Goodbye")
                                )
                    );
                return new XmlActionResult(xml);
            }
        }
        #endregion

        #region Recall
        public ActionResult Recall(long id)
        {
            var n = DatabaseNotificationService.GetById(id);
            var xml = new XDocument(
                new XElement("Response",
                    new XElement("Gather",
                        new XAttribute("timeout", "10"),
                        new XAttribute("numDigits", "1"),
                        new XAttribute("action", "https://ocharambe.localtunnel.me/twilioresponse/recallresponse/" + id),
                            new XElement("Say",
                                    n.NotificationMessage
                                )
                          ),
                    new XElement("Say",
                        "Goodbye!")
                    )
                );
            return new XmlActionResult(xml);
        }

        public ActionResult RecallResponse(long id)
        {
            var notification = DatabaseNotificationService.GetById(id);
            var user = DatabasePatientService.GetById(notification.PatientId);
            string digits = Request["Digits"];
            System.Diagnostics.Debug.WriteLine(digits);
            if (digits.Contains("9"))
            {
                var xml = new XDocument(
                        new XElement("Response",
                            new XElement("Say",
                                "Connecting you to a pharmacist."),
                        new XElement("Dial",
                        DatabasePharmacyService.GetById(user.PharmacyId).PharmacyPhone)
                        )
                    );
                return new XmlActionResult(xml);
            }
            else
            {
                var xml = new XDocument(
                        new XElement("Response",
                            new XElement("Say",
                                "Unrecognized Input, Goodbye")
                                )
                    );
                return new XmlActionResult(xml);
            }
        }
        #endregion

        #region Ready
        public ActionResult Ready(long id)
        {
            Pharmacy p = DatabasePharmacyService.GetById(id);
            p.GetTemplates();
            string message = p.GetReadyTemplate().TemplatePhone;
            var xml = new XDocument(
                new XElement("Response",
                    new XElement("Gather",
                        new XAttribute("timeout", "10"),
                        new XAttribute("numDigits", "1"),
                        new XAttribute("action", "https://ocharambe.localtunnel.me/twilioresponse/readyresponse"),
                            new XElement("Say",
                                    message
                                )
                          ),
                    new XElement("Say",
                        "We didn't recognize that input, Goodbye!")
                    )
                );
            return new XmlActionResult(xml);
        }

        public ActionResult ReadyResponse()
        {
            string digits = Request["Digits"];
            System.Diagnostics.Debug.WriteLine(digits);
            if (digits.Contains("9"))
            {
                var xml = new XDocument(
                        new XElement("Response",
                            new XElement("Say",
                                new XAttribute("voice", "female"),
                                "Connecting you to a pharmacist."),
                        new XElement("Dial",
                        "+18065703539")
                        )
                    );
                return new XmlActionResult(xml);
            }
            else
            {
                var xml = new XDocument(
                        new XElement("Response",
                            new XElement("Say",
                                new XAttribute("voice", "female"),
                                "Unrecognized Input, Goodbye")
                                )
                    );
                return new XmlActionResult(xml);
            }
        }
        #endregion 

        #endregion


        #region SMS
        [HttpPost]
        public ActionResult SmsResponse()
        {
            var messagingResponse = new MessagingResponse();
            System.Diagnostics.Debug.WriteLine("SMS Response" + " " + Request["from"] + " " +  Request["body"]);
            if (Request["body"].ToLower() == "yes")
            {
                var users = DatabaseUserService.GetMultipleByPhone(Request["from"]);
                Patient user = null;
                Notification newest = null;
                foreach (var u in users)
                {
                    var patT = DatabasePatientService.GetByUserIdActive(u.UserId);
                    var notificationsT = DatabaseNotificationService.GetByPatientId(patT.PatientId);
                    var newestT = notificationsT[0];
                    foreach (var n in notificationsT)
                    {
                        if (newestT.SentTime > n.SentTime)
                        {
                            newestT = n;
                        }
                    }
                    if (newestT.Sent && newestT.SentTime > DateTime.Now.AddMinutes(-10))
                    {
                        user = patT;
                        newest = newestT;
                    }
                }
                user.LoadUserData();
                newest.NotificationResponse = Request["body"];
                DatabaseNotificationService.Update(newest);
                var pres = DatabasePrescriptionService.GetByPatientId(user.PatientId);
                var refill = DatabaseRefillService.GetByPrescriptionId(pres.PrescriptionId);
                refill.RefillIt = true;
                DatabaseRefillService.Update(refill);
                messagingResponse.Message("Thanks, your prescription will be ready shortly");
            } else if (Request["body"].ToLower() == "stop")
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
                if (newest.Type == Notification.NotificationType.Refill)
                {
                    pat.SendRefillMessage = false;
                    messagingResponse.Message("You have been unsubscribed from refill notifications");
                } else if (newest.Type == Notification.NotificationType.Birthday)
                {
                    pat.SendBirthdayMessage = false;
                    messagingResponse.Message("You have been unsubscribed from birthday notifications");
                } else if (newest.Type == Notification.NotificationType.Ready)
                {
                    pat.SendRefillMessage = false;
                    messagingResponse.Message("You have been unsubscribed from refill notifications");
                }
                DatabasePatientService.Update(pat);
            }
            else if (Request["body"].ToLower() == "stop all")
            {
                var user = DatabaseUserService.GetByPhoneActive(Request["from"]);
                var pat = DatabasePatientService.GetByUserIdActive(user.UserId);
                pat.ContactMethod = Patient.PrimaryContactMethod.OptOut;
            }



            return new TwiMLResult(messagingResponse);
        }
    }
    #endregion

    #region XML Action Result
    public sealed class XmlActionResult : ActionResult
    {
        private readonly XDocument _document;

        public Formatting Formatting { get; set; }
        public string MimeType { get; set; }

        public XmlActionResult(XDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            _document = document;

            // Default values
            MimeType = "text/xml";
            Formatting = Formatting.None;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = MimeType;

            using (var writer = new XmlTextWriter(context.HttpContext.Response.OutputStream, Encoding.UTF8) { Formatting = Formatting })
                _document.WriteTo(writer);
        }
    }
    #endregion

}