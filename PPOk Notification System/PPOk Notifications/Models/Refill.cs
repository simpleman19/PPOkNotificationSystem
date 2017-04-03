using System;
using System.Collections.Generic;
using System.ComponentModel;
using PPOk_Notifications.Service;
using PPOk_Notifications.NotificationSending;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models
{
    [Serializable, DisplayName("Pharmacy")]
    public class Refill
    {
        [DisplayName("Refill ID")]
        [Column(Name = "refill_id")]
        public long RefillId { get; set; }
        [DisplayName("Prescription ID")]
        [Column(Name = "prescription_id")]
        public long PrescriptionId { get; set; }
        [DisplayName("Refill Date")]
        [Column(Name = "refill_date")]
        public DateTime? RefillDate { get; set; }
        [Column(Name = "refill_filled")]
        public bool Refilled { get; set; }
        [DisplayName("Ready to Refill")]
        [Column(Name = "refill_it")]
        public bool RefillIt { get; set; }

        public Refill()
        {

        }

        public Refill(Prescription prescription)
        {
            PrescriptionId = prescription.PrecriptionId;
            Refilled = false;
            RefillIt = false;
            var notification = Notification.CreateNotification(prescription.PrescriptionDateFilled.AddDays(prescription.PrescriptionDaysSupply - 2), prescription.PatientId, Notification.NotificationType.Refill);

            var db = new SQLService();
            db.NotificationInsert(notification);
        }
        
        public void SetFilled()
        {
            Refilled = true;
            RefillDate = DateTime.Now;
            NotificationSender.SendFilledNotification(this);
            var db = new SQLService();
            db.RefillUpdate(this);
        }

        public static Refill getTestRefill()
        {
            var refill = new Refill();
            refill.PrescriptionId = 1123;
            refill.RefillDate = DateTime.Now;
            refill.Refilled = false;
            return refill;
        }
    }
}