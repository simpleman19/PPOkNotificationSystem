using System;
using System.Collections.Generic;
using System.ComponentModel;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models
{
    [Serializable, DisplayName("Pharmacy")]
    public class Pharmacy
    {
        [DisplayName("Pharmacy ID")] [Column(Name = "pharmacy_id")] public long PharmacyId { get; set; }
        [DisplayName("Pharmacy Name")] [Column(Name = "pharmacy_name")] public string PharmacyName { get; set; }
        [DisplayName("Pharmacy Phone")] [Column(Name = "pharmacy_phone")] public string PharmacyPhone { get; set; }
        [DisplayName("Pharmacy Address")] [Column(Name = "pharmacy_address")] public string PharmacyAddress { get; set; }
        public Template TemplateRefill;
        public Template TemplateRefilled;
        public Template TemplateReady;
        public Template TemplateRecall;
        public Template TemplateBirthday;

        public Refill CreateRefill(Prescription prescription, Patient patient)
        {
            Refill refill = new Refill(prescription);

            return refill;
        }

        public List<Notification> GetNotifications()
        {
            List<Notification> notifications = new List<Notification>();

            return notifications;
        }

        public Template GetRefillTemplate()
        {
            return parseTemplate(TemplateRefill);
        }

        public Template GetRecallTemplate()
        {
            return parseTemplate(TemplateRecall);
        }

        public Template GetRefilledTemplate()
        {
            return parseTemplate(TemplateRefilled);
        }

        public Template GetBirthdayTemplate()
        {
            return parseTemplate(TemplateBirthday);
        }

        private Template parseTemplate(Template template)
        {
            //TODO parsing
            return template;
        }
    }
}