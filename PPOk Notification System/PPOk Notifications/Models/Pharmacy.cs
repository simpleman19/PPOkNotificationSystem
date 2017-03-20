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
        public Template templateRefill;
        public Template templateRefilled;
        public Template templateReady;
        public Template templateRecall;
        public Template templateBirthday;

        public Refill createRefill(Prescription prescription, Patient patient)
        {
            Refill refill = new Refill(prescription);

            return refill;
        }

        public List<Notification> getNotifications()
        {
            List<Notification> notifications = new List<Notification>();

            return notifications;
        }

        public Template getRefillTemplate()
        {
            return parseTemplate(templateRefill);
        }

        public Template getRecallTemplate()
        {
            return parseTemplate(templateRecall);
        }

        public Template getRefilledTemplate()
        {
            return parseTemplate(templateRefilled);
        }

        public Template getBirthdayTemplate()
        {
            return parseTemplate(templateBirthday);
        }

        private Template parseTemplate(Template template)
        {
            //TODO parsing
            return template;
        }
    }
}