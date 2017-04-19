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
            Pharmacy p = DatabasePharmacyService.GetById(id);
            p.GetTemplates();
            string message = p.GetRefillTemplate().TemplatePhone;
            var xml = new XDocument(
                new XElement("Response",
                    new XElement("Gather",
                        new XAttribute("timeout", "10"),
                        new XAttribute("numDigits", "1"),
                        new XAttribute("action", "https://ocharambe.localtunnel.me/twilioresponse/refillresponse"),
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

        public ActionResult RefillResponse()
        {
            string digits = Request["Digits"];
            System.Diagnostics.Debug.WriteLine(digits);
            if (digits.Contains("1"))
            {
                System.Diagnostics.Debug.WriteLine(Request["To"]);
                var user = DatabaseUserService.GetByPhoneActive(Request["To"]);
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
                newest.NotificationResponse = Request["digits"];
                var refill = DatabaseRefillService.GetByPrescriptionId(DatabasePrescriptionService.GetByPatientId(pat.PatientId).PrescriptionId);
                refill.RefillIt = true;
                DatabaseRefillService.Update(refill);
                var xml = new XDocument(
                        new XElement("Response",
                            new XElement("Say",
                                "Your refill will be ready shortly.")
                                )
                    );
                return new XmlActionResult(xml);
            }
            else if (digits.Contains("9"))
            {
                var xml = new XDocument(
                        new XElement("Response",
                            new XElement("Say",
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
                        new XAttribute("action", "https://ocharambe.localtunnel.me/twilioresponse/recallresponse"),
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

        public ActionResult RecallResponse()
        {
            string digits = Request["Digits"];
            System.Diagnostics.Debug.WriteLine(digits);
            if (digits.Contains("9"))
            {
                var xml = new XDocument(
                        new XElement("Response",
                            new XElement("Say",
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
                DatabaseNotificationService.Update(newest);
                var refill = DatabaseRefillService.GetByPrescriptionId(DatabasePrescriptionService.GetByPatientId(pat.PatientId).PrescriptionId);
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