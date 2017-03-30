using System;
using PPOk_Notifications.NotificationSending;
using PPOk_Notifications.Service;

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
            var notification = Notification.CreateNotification(prescription.PrescriptionDateFilled.AddDays(prescription.PrescriptionDaysSupply - 2), prescription.PatientId, Notification.NotificationType.Refill);

            var db = new SQLService();
            db.NotificationInsert(notification);
        }
        
        public void SetFilled()
        {
            Refilled = true;
            NotificationSender.SendFilledNotification(this);
        }
    }
}