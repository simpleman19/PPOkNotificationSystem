using System;

namespace PPOk_Notifications.Models
{
    public class Notification
    {
        public enum NotificationType
        {
            Refill,
            Refilled,
            Recall,
            Birthday            
        };

        public long NotificationId;
        public long PatientId;
        public NotificationType Type;
        public DateTime ScheduledTime;
        public DateTime SentTime;
        public bool Sent;
        public String NotificationResponse;
        public String NotificationMessage;

        public Notification(DateTime dateTime, long patientId, NotificationType type)
        {
            Sent = false;
            PatientId = patientId;
            Type = type;
            ScheduledTime = dateTime;
        }

        public Notification(DateTime dateTime, long patientId, NotificationType type, String message)
        {
            Sent = false;
            PatientId = patientId;
            Type = type;
            ScheduledTime = dateTime;
            NotificationMessage = message;
        }

        public Notification(Refill refill, NotificationType type)
        {
            if (type == NotificationType.Refilled)
            {
                ScheduledTime = DateTime.Now;
            } else if (type == NotificationType.Refill)
            {

            }
            Type = type;
            //Make database call to get patient id from prescription id
            //patientId = database.getPrescription(prescriptionID);
            Sent = false;
        }

        public static Notification CreateNotification(DateTime dateTime, long patientID, NotificationType type)
        {
            Notification notification = new Notification(dateTime, patientID, type);
            //Save notification to database
            return notification;
        }

        public static Notification CreateNotification(DateTime dateTime, long patientID, NotificationType type, String message)
        {
            Notification notification = new Notification(dateTime, patientID, type, message);
            //Save notification to database
            return notification;
        }

        public static Notification CreateNotification(Refill refill, NotificationType type)
        {
            Notification notification = new Notification(refill, type);
            //Save notification to database
            return notification;
        }

        public static Notification MarkSent(Notification notification)
        {
            notification.Sent = true;
            notification.SentTime = DateTime.Now;

            return notification;
        }

        public static Notification MarkSent(Notification notification, DateTime time)
        {
            notification.Sent = true;
            notification.SentTime = time;
            return notification;
        }

        public static Notification GetTestNotification()
        {
            Notification test = new Notification(DateTime.Now, 1, Notification.NotificationType.Refill);
            Random rand = new Random();
            test.NotificationId = rand.Next(1000, 10000000);
            return test;
        }

        public static Notification GetTestNotification(Random rand)
        {
            Notification test = new Notification(DateTime.Now, 1, Notification.NotificationType.Refill);
            test.NotificationId = rand.Next(1000, 10000000);
            return test;
        }
    }
}