using System;
using System.Collections.Generic;

namespace PPOk_Notifications.Models
{
    public class Pharmacy
    {
        public long PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public string PharmacyPhone { get; set; }
        public string PharmacyAddress { get; set; }
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