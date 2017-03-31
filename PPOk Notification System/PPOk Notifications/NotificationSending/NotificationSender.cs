﻿using System;
using System.Collections.Generic;
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
            Notification n = new Notification(refill, Notification.NotificationType.Ready);
            var db = new SQLService();
            db.NotificationInsert(n);
            SendNotification(n);
            return true;
        }

        private void PrepareForSending()
        {
            List<Notification> notifications = getNotifications();
            foreach (Notification n in notifications)
            {
                SendNotification(n);
            }
        }

        private static void SendNotification(Notification n)
        {
            System.Diagnostics.Debug.WriteLine("Sending Notification: " + n.NotificationId);
            var db = new SQLService();
            Patient p = db.GetPatientById(n.PatientId);

            if (n.Type == Notification.NotificationType.Recall)
            {
                // TODO Call twillio api using phone call and use message saved in notification
            }
            else
            {
                // TODO Get template from pharmacy
                switch(p.ContactMethod)
                {
                    case Patient.PrimaryContactMethod.Call:
                        break;
                    case Patient.PrimaryContactMethod.Email:
                        break;
                    case Patient.PrimaryContactMethod.Text:
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
            var db = new SQLService();
            List<Notification> list = db.GetNotificationsActive();
            // TODO add birthdays and get not sent but before current datetime
            return list;
        }

        private bool CheckIfSend()
        {
            DateTime? lastDTwhenSent = null;
            if (File.Exists(_path))
            {
                lastDTwhenSent = ReadDateFromFile();
                if (lastDTwhenSent == null)
                {
                    WriteDateToFile();
                    PrepareForSending();
                }
                else
                {
                    TimeSpan span = DateTime.Now.Subtract((DateTime)lastDTwhenSent);
                    if (span.Minutes >= MinsBetweenSending)
                    {
                        WriteDateToFile();
                        PrepareForSending();
                    }
                }
            } 
            else
            {
                FileStream fs = new FileStream(_path, FileMode.Create);
                fs.Close();
            }
            return true;
        }

        private bool WriteDateToFile()
        {
            using (FileStream fs = new FileStream(_path, FileMode.OpenOrCreate))
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

        private DateTime? ReadDateFromFile()
        {
            DateTime? parsedDateTime = null;
            using (FileStream fs = new FileStream(_path, FileMode.OpenOrCreate))
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