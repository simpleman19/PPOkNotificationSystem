using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Hosting;
using System.IO;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.NotificationSending
{
    public class NotificationSender : IRegisteredObject
    {
        private readonly string _path = HttpContext.Current.Server.MapPath("~/App_Data/lastNoficiationSent.bin");
        private const int MinsBetweenSending = 1;

        private readonly object _lock = new object();
        private bool _shuttingDown;

        public NotificationSender()
        {
            HostingEnvironment.RegisterObject(this);
        }

        public void Stop(bool immediate)
        {
            lock (_lock)
            {
                _shuttingDown = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }

        public void DoWork()
        {
            lock (_lock)
            {
                if (_shuttingDown)
                {
                    return;
                }
                CheckIfSend();
            }
        }

        public static bool SendFilledNotification(Refill refill)
        {
            var n = new Notification(refill, Notification.NotificationType.Ready);
            var p = DatabasePrescriptionService.GetById(refill.PrescriptionId);
            n.PatientId = p.PatientId;
            System.Diagnostics.Debug.WriteLine(n.PatientId);
            n.NotificationId = DatabaseNotificationService.Insert(n);
            var pat = DatabasePatientService.GetById(n.PatientId);
            var twilio = new TwilioApi(pat.getPharmacy());
            SendNotification(n, twilio);
            return true;
        }

        public static void SendRecalls(List<Notification> notifications)
        {
            foreach (var n in notifications)
            {
                var twilio = new TwilioApi(Patient.PatientTree[n.PatientId].getPharmacy());
                SendNotification(n, twilio);
            }
        }

        public static void SendNotification(Notification notification)
        {
            var twilio = new TwilioApi(Patient.PatientTree[notification.PatientId].getPharmacy());
            SendNotification(notification, twilio);
        }

        private void PrepareForSending()
        {
            var notifications = getNotifications();
            foreach (var n in notifications)
            {
                var pat = DatabasePatientService.GetById(n.PatientId);

                var twilio = new TwilioApi(pat.getPharmacy());
                SendNotification(n, twilio);
            }
        }

        private static void SendNotification(Notification n, TwilioApi twilio)
        {
            System.Diagnostics.Debug.WriteLine("Sending Notification: " + n.NotificationId);
            var p = DatabasePatientService.GetById(n.PatientId);

            if (n.Type == Notification.NotificationType.Recall)
            {
                twilio.MakeRecallPhoneCall(n);
            }
            else
            {

                // TODO Get template from pharmacy
                switch(p.ContactMethod)
                {
                    case Patient.PrimaryContactMethod.Call:
                        twilio.SendTextMessage(n);
                        break;
                    case Patient.PrimaryContactMethod.Email:

                        break;
                    case Patient.PrimaryContactMethod.Text:
                        twilio.SendTextMessage(n);
                        break;
                    case Patient.PrimaryContactMethod.OptOut:
                        // Do nothing
                        break;
                    default:
                        break;
                }
                // TODO Call Twilio api using patient prefered contact method
                // Mark as sent
            }
            Notification.MarkSent(n);
        }

        private List<Notification> getNotifications()
        {
            var list = DatabaseNotificationService.GetAllActive();
            // TODO add birthdays and get not sent but before current datetime
            return list;
        }

        private bool CheckIfSend()
        {
	        if (File.Exists(_path)) {
	            var lastDTwhenSent = ReadDateFromFile();
	            if (lastDTwhenSent == null)
                {
                    WriteDateToFile();
                    PrepareForSending();
                }
                else
                {
                    var span = DateTime.Now.Subtract((DateTime)lastDTwhenSent);
	                if (span.Minutes < MinsBetweenSending) return true;
	                WriteDateToFile();
	                PrepareForSending();
                }
            } 
            else
            {
                var fs = new FileStream(_path, FileMode.Create);
                fs.Close();
            }
            return true;
        }

        private bool WriteDateToFile()
        {
            using (var fs = new FileStream(_path, FileMode.OpenOrCreate))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    fs.Position = 0;
                    writer.Write(DateTime.Now.ToString(CultureInfo.CurrentCulture));
                    writer.Close();
                }
                fs.Close();
            }
            return true;
        }

        private DateTime? ReadDateFromFile()
        {
            DateTime? parsedDateTime = null;
            using (var fs = new FileStream(_path, FileMode.OpenOrCreate))
            {
                using (var reader = new BinaryReader(fs))
                {
                    fs.Position = 0;
                    if (fs.Position < fs.Length)
                    {
                        var dtString = reader.ReadString();
                        parsedDateTime = DateTime.Parse(dtString);
                    }
                }
                fs.Close();
            }
            return parsedDateTime;
        }
    }
}