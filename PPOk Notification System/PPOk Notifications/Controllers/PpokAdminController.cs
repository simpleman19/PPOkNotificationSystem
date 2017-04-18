using PPOk_Notifications.Service;
using System.Collections.Generic;
using System.Web.Mvc;
using PPOk_Notifications.Filters;
using PPOk_Notifications.Models;


namespace PPOk_Notifications.Controllers
{
    [Authenticate(Group.PPOkAdmin)]
    public class PpokAdminController : Controller
    {
        // returned view for seeing a list of pharmacies
        // can add to list
        // can select edit/view/delete from list
        //[HttpPost]
        public ActionResult PharmacyListView()
        {
            var pharamcies = DatabasePharmacyService.GetAllActive();
            return View(pharamcies);
        }

        public ActionResult AddorEditPharmacy(long id = 0)
        {
            var pharmacy = DatabasePharmacyService.GetById(id);

            if (pharmacy == null)
            {
                pharmacy = new Pharmacy();
                pharmacy.InsertDefaultTemplateData();
            }
            else
            {
                pharmacy.GetTemplates();
            }

            return View("~/Views/Pharmacy/Admin.cshtml", pharmacy);
        }

        [HttpPost]
        public ActionResult AddorEditPharmacy(
            string refillTextTemplate, string refillPhoneTemplate, string refillEmailTemplate,
            string pickupTextTemplate, string pickupPhoneTemplate, string pickupEmailTemplate,
            string recallTextTemplate, string recallPhoneTemplate, string recallEmailTemplate,
            string birthdayTextTemplate, string birthdayPhoneTemplate, string birthdayEmailTemplate,
            string notificationDisabledTextTemplate, string notificationDisabledPhoneTemplate, string notificationDisabledEmailTemplate,
            string pharmacyName, string pharmacyPhone, string pharmacyAddress, long pharmacyId )
        {
            Pharmacy pharmacy;

            if (pharmacyId != 0)
            {
                pharmacy = DatabasePharmacyService.GetById(pharmacyId);
                pharmacy.GetTemplates();
            }
            else
            {
                pharmacy = new Pharmacy();
                pharmacy.Fill();
                pharmacy.PharmacyId = DatabasePharmacyService.Insert(pharmacy);
                pharmacy.InsertDefaultTemplateData();
                pharmacy.SaveNewTemplates();
            }

            pharmacy.PharmacyName = pharmacyName;
            pharmacy.PharmacyPhone = pharmacyPhone;
            pharmacy.PharmacyAddress = pharmacyAddress;

            pharmacy.TemplateRefill.TemplateText = refillTextTemplate;
            pharmacy.TemplateRefill.TemplatePhone = refillPhoneTemplate;
            pharmacy.TemplateRefill.TemplateEmail = refillEmailTemplate;

            pharmacy.TemplateReady.TemplateText = pickupTextTemplate;
            pharmacy.TemplateReady.TemplatePhone = pickupPhoneTemplate;
            pharmacy.TemplateReady.TemplateEmail = pickupEmailTemplate;

            pharmacy.TemplateRecall.TemplateText = recallTextTemplate;
            pharmacy.TemplateRecall.TemplatePhone = recallPhoneTemplate;
            pharmacy.TemplateRecall.TemplateEmail = recallEmailTemplate;

            pharmacy.TemplateBirthday.TemplateText = birthdayTextTemplate;
            pharmacy.TemplateBirthday.TemplatePhone = birthdayPhoneTemplate;
            pharmacy.TemplateBirthday.TemplateEmail = birthdayEmailTemplate;

            DatabasePharmacyService.Update(pharmacy);
            pharmacy.SaveTemplates();

            return Redirect("/PpokAdmin/PharmacyListView");
        }


        public ActionResult PharmacyDisplay(long id)
        {
            Pharmacy pharmacy = DatabasePharmacyService.GetById(id);
            List<Pharmacist> pharmacists = DatabasePharmacistService.GetAll();
            Pharmacist pharmacist = new Pharmacist();
            foreach (Pharmacist p in pharmacists) {
                if (p.Enabled && p.IsAdmin) { pharmacist = p; }
            }
            return View( new System.Tuple<Pharmacy, Pharmacist>(pharmacy,pharmacist) );
        }

        public ActionResult DeletePharmacy(long id)
        {
            DatabasePharmacyService.Disable(id);
            return Redirect("/PpokAdmin/PharmacyListView");
        }
    }
}