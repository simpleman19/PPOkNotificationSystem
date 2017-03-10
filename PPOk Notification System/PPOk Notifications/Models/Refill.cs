using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPOk_Notifications.NotificationSending;

namespace PPOk_Notifications.Models
{
    public class Refill
    {
        public long refillID { get; set; }
        public long prescriptionID { get; set; }
        public DateTime refillDate { get; set; }
        public bool refilled { get; set; }

        public Refill(Prescription prescription)
        {
            prescriptionID = prescription.precriptionID;
            refilled = false;
            Notification.createNotification(prescription.prescriptionDateFilled.AddDays(prescription.prescriptionDaysSupply - 2), prescription.patientID, Notification.NotificationType.Refill);
            
            // Save to database
        }
        
        public void setFilled()
        {
            refilled = true;
            NotificationSender.sendFilledNotification(this);
        }
    }
}