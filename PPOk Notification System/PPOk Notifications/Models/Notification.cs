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
            Recall,
            Birthday            
        };

        public long notificationID;
        public long patientID;
        public NotificationType notificationType;
        public DateTime scheduledTime;
        public DateTime sentTime;
        public String notificationResponse;
        public String notificationMessage;
    }
}