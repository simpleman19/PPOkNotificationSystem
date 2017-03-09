using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public long notificationID;
        public long patientID;
        public NotificationType notificationType;
        public DateTime scheduledTime;
        public DateTime sentTime;
        public bool sent;
        public String notificationResponse;
        public String notificationMessage;

        public Notification(DateTime dateTime, long patientId, NotificationType type)
        {
            sent = false;
            patientID = patientId;
            notificationType = type;
            scheduledTime = dateTime;
        }

        public Notification(Refill refill)
        {
            scheduledTime = DateTime.Now;
            notificationType = NotificationType.Refilled;
            //Make database call to get patient id from prescription id
            //patientId = database.getPrescription(prescriptionID);
            sent = false;
        }

        public static Notification createNotification(DateTime dateTime, long patientID, NotificationType type)
        {
            Notification notification = new Notification(dateTime, patientID, type);
            //Save notification to database
            return notification;
        }

        public static Notification createNotification(Refill refill)
        {
            Notification notification = new Notification(refill);
            //Save notification to database
            return notification;
        }
    }
}