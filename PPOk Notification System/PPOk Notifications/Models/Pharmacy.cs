using System;
using System.Collections.Generic;
using System.ComponentModel;
using Dapper.Contrib.Extensions;
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

        [DisplayName("Template Refill Id")] [Column(Name = "template_refill")] public long TemplateRefillId { get; set; }
        [DisplayName("Template Ready Id")] [Column(Name = "template_ready")] public long TemplateReadyId { get; set; }
        [DisplayName("Template Recall Id")] [Column(Name = "template_recall")] public long TemplateRecallId { get; set; }
        [DisplayName("Template Birthday Id")] [Column(Name = "template_birthday")] public long TemplateBirthdayId { get; set; }

        [Computed] public Template TemplateRefill { get; set; }
        [Computed] public Template TemplateReady { get; set; }
        [Computed] public Template TemplateRecall { get; set; }
        [Computed] public Template TemplateBirthday { get; set; }

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

        public Template GetReadyTemplate()
        {
            return parseTemplate(TemplateReady);
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

        public void GetTemplates()
        {
            SQLService service = new SQLService();

            TemplateRefill = service.GetTemplateById((int) TemplateRefillId);
            TemplateReady = service.GetTemplateById((int) TemplateReadyId);
            TemplateRecall = service.GetTemplateById((int) TemplateRecallId);
            TemplateBirthday = service.GetTemplateById((int) TemplateBirthdayId);
        }

        public void SaveTemplates()
        {
            SQLService service = new SQLService();

            service.TemplateInsertOrUpdate(TemplateRefill);
            service.TemplateInsertOrUpdate(TemplateRecall);
            service.TemplateInsertOrUpdate(TemplateReady);
            service.TemplateInsertOrUpdate(TemplateBirthday);
        }

        public static void FakeDataFill()
        {
            SQLService service = new SQLService();
            service.Rebuild();
           
            Pharmacy pharmacy = new Pharmacy();
            pharmacy.PharmacyId = 1L;
            pharmacy.PharmacyAddress = "Some Pharmacy\nSome Address\nSome City, OK 73008";
            pharmacy.PharmacyName = "Fake Pharmacy";
            pharmacy.PharmacyPhone = "555-5555";

            pharmacy.TemplateRefill = new Template();
            pharmacy.TemplateBirthday = new Template();
            pharmacy.TemplateRecall = new Template();
            pharmacy.TemplateReady = new Template();

            pharmacy.TemplateRefill.PharmacyId = 1;
            pharmacy.TemplateRefill.TemplateId = 1;
            pharmacy.TemplateRefill.Fill();

            pharmacy.TemplateBirthday.PharmacyId = 1;
            pharmacy.TemplateBirthday.TemplateId = 2;
            pharmacy.TemplateBirthday.Fill();

            pharmacy.TemplateRecall.PharmacyId = 1;
            pharmacy.TemplateRecall.TemplateId = 3;
            pharmacy.TemplateRecall.Fill();

            pharmacy.TemplateReady.PharmacyId = 1;
            pharmacy.TemplateReady.TemplateId = 4;
            pharmacy.TemplateReady.Fill();

            pharmacy.TemplateRefill.TemplateText =
                "A prescription you have is up for refill at {{pharmacy_name}}. Would you like to refill your prescription? Text back YES";
            pharmacy.TemplateBirthday.TemplateText =
                "Happy Birthday from {{pharmacy_name}}! Text back STOP to disable birthday notifications, or STOPALL to disable all notifications.";
            pharmacy.TemplateRecall.TemplateText =
                "One of your prescriptions has been recalled, please contact your pharmacist at {{pharmacy_phone}} for more information.";
            pharmacy.TemplateReady.TemplateText =
                "Your prescription is ready at {{pharmacy_name}}! Please call {{pharmacy_phone}} if you have any questions.";

            pharmacy.TemplateRefill.TemplatePhone =
               "A prescription you have is up for refill at {{pharmacy_name}} Would you like to refill your prescription? Press 1 for Yes, Press 9 to be connected to a pharmacist";
            pharmacy.TemplateBirthday.TemplatePhone =
                "Happy Birthday from {{pharmacy_name}}! …(Pause)... If you would like to no longer receive these notifications press 2.";
            pharmacy.TemplateRecall.TemplatePhone =
                "One of your prescriptions has been recalled, please contact your pharmacist at {{pharmacy_phone}} or press 9 to be connected to a pharmacist for more information.";
            pharmacy.TemplateReady.TemplatePhone =
                "Your prescription is ready at {{pharmacy_name}}! Please call {{pharmacy_phone}} or press 9 to be connected to a pharmacist if you have any questions";

            pharmacy.TemplateRefillId = pharmacy.TemplateRefill.TemplateId;
            pharmacy.TemplateRecallId = pharmacy.TemplateRecall.TemplateId;
            pharmacy.TemplateReadyId = pharmacy.TemplateReady.TemplateId;
            pharmacy.TemplateBirthdayId = pharmacy.TemplateBirthday.TemplateId;

            service.PharmacyInsert(pharmacy);
            pharmacy.SaveTemplates();
        }
    }
}