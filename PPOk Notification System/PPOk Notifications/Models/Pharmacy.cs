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
            // TODO ? this looks incomplete
            return refill;
        }

        public List<Notification> GetNotifications()
        {
            List<Notification> notifications = new List<Notification>();
            // TODO
            return notifications;
        }

        public Template GetRefillTemplate() => ParseTemplate(TemplateRefill);
        public Template GetRecallTemplate() => ParseTemplate(TemplateRecall);
        public Template GetReadyTemplate() => ParseTemplate(TemplateReady);
        public Template GetBirthdayTemplate() => ParseTemplate(TemplateBirthday);

        private Template ParseTemplate(Template template)
        {
            return new Template
            {
                TemplateText = Replace(template.TemplateText),
                TemplatePhone = Replace(template.TemplatePhone),
                TemplateEmail = Replace(template.TemplateEmail)
            };
        }

        private string Replace(string oldText)
        {
            return oldText
                .Replace("{{pharmacy_address}}", PharmacyAddress)
                .Replace("{{pharmacy_name}}", PharmacyName)
                .Replace("{{pharmacy_phone}}", PharmacyPhone);
        }

        public void GetTemplates()
        {
            SQLService service = new SQLService();

            TemplateRefill = service.GetTemplateById((int) TemplateRefillId);
            TemplateReady = service.GetTemplateById((int) TemplateReadyId);
            TemplateRecall = service.GetTemplateById((int) TemplateRecallId);
            TemplateBirthday = service.GetTemplateById((int) TemplateBirthdayId);
        }

        // -- TEST CODE --
        public void SaveTemplates()
        {
            SQLService service = new SQLService();

            service.TemplateInsertOrUpdate(TemplateRefill);
            service.TemplateInsertOrUpdate(TemplateRecall);
            service.TemplateInsertOrUpdate(TemplateReady);
            service.TemplateInsertOrUpdate(TemplateBirthday);
        }

        public void SaveNewTemplates()
        {
            SQLService service = new SQLService();

            TemplateRefillId = service.TemplateInsert(TemplateRefill);
            TemplateRecallId = service.TemplateInsert(TemplateRecall);
            TemplateReadyId = service.TemplateInsert(TemplateReady);
            TemplateBirthdayId = service.TemplateInsert(TemplateBirthday);
        }

        public static long FakeDataFill()
        {
            SQLService service = new SQLService();
           
            Pharmacy pharmacy = new Pharmacy();
            pharmacy.PharmacyAddress = "Some Pharmacy\nSome Address\nSome City, OK 73008";
            pharmacy.PharmacyName = "Fake Pharmacy";
            pharmacy.PharmacyPhone = "555-5555";

            pharmacy.PharmacyId = service.PharmacyInsert(pharmacy);

            pharmacy.InsertDefaultTemplateData();

            pharmacy.SaveNewTemplates();
            service.PharmacyInsertOrUpdate(pharmacy);

            return pharmacy.PharmacyId;
        }

        public void Fill()
        {
            PharmacyAddress = "";
            PharmacyName = "";
            PharmacyPhone = "";
        }

        public void InsertDefaultTemplateData()
        {

            TemplateRefill = new Template();
            TemplateBirthday = new Template();
            TemplateRecall = new Template();
            TemplateReady = new Template();

            TemplateRefill.PharmacyId = PharmacyId;
            TemplateRefill.Fill();

            TemplateBirthday.PharmacyId = PharmacyId;
            TemplateBirthday.Fill();

            TemplateRecall.PharmacyId = PharmacyId;
            TemplateRecall.Fill();

            TemplateReady.PharmacyId = PharmacyId;
            TemplateReady.Fill();

            TemplateRefill.TemplateText =
                "A prescription you have is up for refill at {{pharmacy_name}}. Would you like to refill your prescription? Text back YES";
            TemplateBirthday.TemplateText =
                "Happy Birthday from {{pharmacy_name}}! Text back STOP to disable birthday notifications, or STOPALL to disable all notifications.";
            TemplateRecall.TemplateText =
                "One of your prescriptions has been recalled, please contact your pharmacist at {{pharmacy_phone}} for more information.";
            TemplateReady.TemplateText =
                "Your prescription is ready at {{pharmacy_name}}! Please call {{pharmacy_phone}} if you have any questions.";

            TemplateRefill.TemplatePhone =
               "A prescription you have is up for refill at {{pharmacy_name}} Would you like to refill your prescription? Press 1 for Yes, Press 9 to be connected to a pharmacist";
            TemplateBirthday.TemplatePhone =
                "Happy Birthday from {{pharmacy_name}}! …(Pause)... If you would like to no longer receive these notifications press 2.";
            TemplateRecall.TemplatePhone =
                "One of your prescriptions has been recalled, please contact your pharmacist at {{pharmacy_phone}} or press 9 to be connected to a pharmacist for more information.";
            TemplateReady.TemplatePhone =
                "Your prescription is ready at {{pharmacy_name}}! Please call {{pharmacy_phone}} or press 9 to be connected to a pharmacist if you have any questions";

        }
    }
}