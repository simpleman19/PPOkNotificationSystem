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
        int minsBetweenSending = 1;

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
                checkIfSend();
            }
        }

        public static bool sendFilledNotification(Refill refill)
        {
            Notification n = new Notification(refill, Notification.NotificationType.Refilled);
            // Save notification to database
            sendNotification(n);
            return true;
        }

        private void prepareForSending()
        {
            List<Notification> notifications = getNotifications();
            foreach (Notification n in notifications)
            {
                sendNotification(n);
            }
        }

        private static void sendNotification(Notification n)
        {
            System.Diagnostics.Debug.WriteLine("Sending Notification: " + n.notificationID);

            //Call Database for patient using n.patientID
            if (n.notificationType == Notification.NotificationType.Recall)
            {
                // TODO Call twillio api using phone call and use message saved in notification
            }
            else
            {
                // TODO Get template from pharmacy
                // TODO Call Twilio api using patient prefered contact method
                // Mark as sent
            }
            Notification.markSent(n);
        }

        private List<Notification> getNotifications()
        {
            List<Notification> list = new List<Notification>();
            // TODO Make database call
            // TODO Get patient preferences and filter notifications to be sent based on preferences
            // TODO Get patients whose birthday is today and  narrow down based on preferences
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                Notification test = Notification.getTestNotification(rand);
                list.Add(test);
            }

            return list;
        }

        private bool checkIfSend()
        {
            DateTime? lastDTwhenSent = null;
            if (File.Exists(path))
            {
                lastDTwhenSent = readDateFromFile();
                if (lastDTwhenSent == null)
                {
                    writeDateToFile();
                    prepareForSending();
                }
                else
                {
                    TimeSpan span = DateTime.Now.Subtract((DateTime)lastDTwhenSent);
                    if (span.Minutes >= minsBetweenSending)
                    {
                        writeDateToFile();
                        prepareForSending();
                    }
                }
            } 
            else
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Close();
            }
            return true;
        }

        private bool writeDateToFile()
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
                        parsedDateTime = DateTime.Parse(dtString);
                    }
                }
                fs.Close();
            }
            return parsedDateTime;
        }
    }
}