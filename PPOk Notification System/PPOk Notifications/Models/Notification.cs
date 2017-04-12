using System;
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
            Birthday,
			Reset
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
        [Column(Name = "notification_time")]
        public DateTime ScheduledTime { get; set; }

        [DisplayName("Sent Time")]
        [Column(Name = "notification_senttime")]
        public DateTime? SentTime { get; set; }

        [DisplayName("Send Status")]
        [Column(Name = "notification_sent")]
        public bool Sent { get; set; }

        [DisplayName("Notification Response")]
        [Column(Name = "notification_response")]
        public string NotificationResponse { get; set; }

        [DisplayName("Notification Message")]
        [Column(Name = "notification_message")]
        public string NotificationMessage { get; set; }

        public Notification() {}

        public Notification(DateTime dateTime, long patientId, NotificationType type)
        {
            Sent = false;
            PatientId = patientId;
            Type = type;
            ScheduledTime = dateTime;
            SentTime = null;
        }

        public Notification(DateTime dateTime, long patientId, NotificationType type, string message)
        {
            Sent = false;
            PatientId = patientId;
            Type = type;
            ScheduledTime = dateTime;
            NotificationMessage = message;
            SentTime = null;
        }

        public Notification(Refill refill, NotificationType type)
        {
            switch (type) {
	            case NotificationType.Ready:
		            ScheduledTime = DateTime.Now;
		            break;
	            case NotificationType.Refill:
		            break;
	            case NotificationType.Recall:
		            break;
	            case NotificationType.Birthday:
		            break;
	            case NotificationType.Reset:
		            break;
	            default:
		            throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            Type = type;
            //Make database call to get patient id from prescription id
            //patientId = database.getPrescription(prescriptionID);
            Sent = false;
            SentTime = null;
        }

        public static Notification CreateNotification(DateTime dateTime, long patientID, NotificationType type)
        {
            var notification = new Notification(dateTime, patientID, type);
            //Save notification to database
            return notification;
        }

        public static Notification CreateNotification(DateTime dateTime, long patientID, NotificationType type, string message)
        {
            var notification = new Notification(dateTime, patientID, type, message);
            //Save notification to database
            return notification;
        }

        public static Notification CreateNotification(Refill refill, NotificationType type)
        {
            var notification = new Notification(refill, type);
            //Save notification to database
            return notification;
        }

        public static Notification MarkSent(Notification notification)
        {
            notification.Sent = true;
            notification.SentTime = DateTime.Now;
            DatabaseNotificationService.Update(notification);
            return notification;
        }

        public static Notification MarkSent(Notification notification, DateTime time)
        {
            notification.Sent = true;
            notification.SentTime = time;
            DatabaseNotificationService.Update(notification);
            return notification;
        }

        public static Notification GetTestNotification()
        {
            var test = new Notification(DateTime.Now, 1, Notification.NotificationType.Refill);
            var rand = new Random();
            test.NotificationId = rand.Next(1000, 10000000);
            return test;
        }

        public static Notification GetTestNotification(Random rand)
        {
	        var test = new Notification(DateTime.Now, 1, Notification.NotificationType.Refill) {
		        SentTime = DateTime.Now,
		        NotificationId = rand.Next(1000, 10000000)
	        };
	        return test;
        }
    }
}