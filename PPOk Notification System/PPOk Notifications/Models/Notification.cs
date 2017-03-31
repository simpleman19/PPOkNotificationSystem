using System;
using System.Collections.Generic;
using System.ComponentModel;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models
{
    [Serializable, DisplayName("Notification")]
    public class Notification
    {
        public enum NotificationType
        {
            Refill,
            Ready,
            Recall,
            Birthday            
        };

        [DisplayName("Notification ID")]
        [Column(Name = "notification_id")]
        public long NotificationId { get; set; }
        [DisplayName("Patient ID")]
        [Column(Name = "patient_id")]
        public long PatientId { get; set; }
        [DisplayName("Notification Type")]
        [Column(Name = "notification_type")]
        public NotificationType Type { get; set; }
        [DisplayName("Scheduled Time")]
        [Column(Name = "scheduled_time")]
        public DateTime ScheduledTime { get; set; }
        [DisplayName("Sent Time")]
        [Column(Name = "sent_time")]
        public DateTime SentTime { get; set; }
        [DisplayName("Send Status")]
        [Column(Name = "send_status")]
        public bool Sent { get; set; }
        [DisplayName("Notification Response")]
        [Column(Name = "notification_response")]
        public String NotificationResponse { get; set; }
        [DisplayName("Notification Message")]
        [Column(Name = "notification_message")]
        public String NotificationMessage { get; set; }

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
            if (type == NotificationType.Ready)
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