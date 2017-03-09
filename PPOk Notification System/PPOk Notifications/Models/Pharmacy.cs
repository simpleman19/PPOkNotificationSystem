using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPOk_Notifications.Models
{
    public class Pharmacy
    {
        public long pharmacyID { get; set; }
        public String pharmacyName { get; set; }
        public String pharmacyPhone { get; set; }
        public String pharmacyAddress { get; set; }
        public Template templateRefill { get; set; }
        public Template templateReady { get; set; }
        public Template templateRecall { get; set; }
        public Template templateBirthday { get; set; }

        public Refill createRefill(Prescription prescription, Patient patient)
        {
            Refill refill = new Refill(prescription);
            NotificationSending.NotificationSender.sendRefilledNotification(this);
        }

    }
}