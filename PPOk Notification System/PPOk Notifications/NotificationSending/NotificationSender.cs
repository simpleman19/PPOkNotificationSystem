using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;

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
                sendNotifications();
            }
        }

        private void sendNotifications()
        {
            DateTime? lastDTwhenSent = null;
            if (File.Exists(path)) {
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