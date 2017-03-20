using System;
using PPOk_Notifications.NotificationSending;

namespace PPOk_Notifications.Models
{
    public class Refill
    {
        public long RefillId { get; set; }
        public long PrescriptionId { get; set; }
        public DateTime RefillDate { get; set; }
        public bool Refilled { get; set; }

        public Refill(Prescription prescription)
        {
            PrescriptionId = prescription.PrecriptionId;
            Refilled = false;
            Notification.CreateNotification(prescription.PrescriptionDateFilled.AddDays(prescription.PrescriptionDaysSupply - 2), prescription.PatientId, Notification.NotificationType.Refill);
            
            // Save to database
        }
        
        public void SetFilled()
        {
            Refilled = true;
            NotificationSender.SendFilledNotification(this);
        }
    }
}