using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.NotificationSending
{
    public class NotificationSender : IRegisteredObject
    {
        string path = HttpContext.Current.Server.MapPath("~/App_Data/lastNoficiationSent.bin");
        int minsBetweenSending = 15;

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
                prepareForSending();
            }
        }

        public bool sendFilledNotification(Refill refill)
        {
            Notification n = new Notification(refill);
            // Save notification to database
            sendNotification(n);
            return true;
        }

        private void prepareForSending()
        {
            saveDate();
            List<Notification> notifications = getNotifications();
            foreach (Notification n in notifications)
            {
                sendNotification(n);
            }
        }

        private void sendNotification(Notification n)
        {
            //Call Database for patient using n.patientID
            if (n.notificationType == Notification.NotificationType.Recall)
            {
                // Call twillio api using phone call and message saved in notification
            }
            else
            {
                // Get template from pharmacy
                // Call Twilio api using patient prefered contact method
                // Mark as sent
            }
        }

        private List<Notification> getNotifications()
        {
            //Make database call
            Notification test = new Notification();
            test.notificationID = 1111111;
            test.notificationType = Notification.NotificationType.Refill;
            test.patientID = 1;
            test.scheduledTime = DateTime.Now;

            List<Notification> list = new List<Notification>();
            list.Add(test);
            return list;
        }

        private Boolean saveDate()
        {
            DateTime? lastDTwhenSent = null;
            if (File.Exists(path))
            {
                lastDTwhenSent = readDateFromFile();
            }
            if (lastDTwhenSent != null)
            {
                TimeSpan span = DateTime.Now.Subtract((DateTime)lastDTwhenSent);
                if (span.Minutes >= minsBetweenSending)
                {
                    writeDateToFile();
                    System.Diagnostics.Debug.WriteLine("Sending");
                }
            }
            return true;
        }

        private Boolean writeDateToFile()
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    fs.Position = 0;
                    writer.Write(DateTime.Now.ToString());
                    writer.Close();
                }
                fs.Close();
            }
            return true;
        }

        private DateTime? readDateFromFile()
        {
            DateTime? parsedDateTime = null;
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    fs.Position = 0;
                    if (fs.Position < fs.Length)
                    {
                        string dtString = reader.ReadString();
                        parsedDateTime = DateTime.Parse(dtString);                    }
                }
                fs.Close();
            }
            return parsedDateTime;
        }
    }
}