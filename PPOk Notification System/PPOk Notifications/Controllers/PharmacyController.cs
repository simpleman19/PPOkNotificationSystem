using LumenWorks.Framework.IO.Csv;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPOk_Notifications.Filters;

namespace PPOk_Notifications.Controllers
{
    public class PharmacyController : Controller
    {

        #region Pharmacists
        /// //////////////////////////////////////////////////////////
        /// Pharmacists
        /// //////////////////////////////////////////////////////////

        [HttpPost]
        [Authenticate(Group.PharmacyAdmin, Group.PPOkAdmin)]
        public ActionResult SavePharmacist(Pharmacist m, String command)
        {
            // if id's are default, get actual id's for the (new) pharmacist
            // use sql to save pharmacist to db
            if (m.PharmacistId == 0)
            {
                m.Type = Models.User.UserType.Pharmacist;
                var phid = DatabaseUserService.Insert(m);
                m.UserId = phid;
                m.PharmacistId = DatabasePharmacistService.Insert(m);
                var login = new Login();
                login.LoginToken = "";
                login.UserId = m.UserId;
                login.SetPassword(Login.GetUniqueKey(32));
                DatabaseLoginService.Insert(login);
                EmailService.SendReset(m);
            }
            else
            {
                DatabaseUserService.Update(m);
                DatabasePharmacistService.Update(m);
            }

            if (DatabaseUserService.GetById((long)Session["user_id"]).Type == Models.User.UserType.PPOkAdmin)
            {
                return RedirectToAction("AddorEditPharmacy", "PpokAdmin", new { id = m.PharmacyId });
            }
            return RedirectToAction("Admin", "Pharmacy");
        }

        [Authenticate(Group.PharmacyAdmin, Group.PPOkAdmin)]
        public ActionResult AddorEditPharmacist(long id = 0, long pharm_id = 0)
        {
            var pharmy = DatabasePharmacistService.GetById(id);
            if (pharmy == null)
            {
                pharmy = new Pharmacist();
                pharmy.PharmacyId = pharm_id;
            } else
            {
                pharmy.LoadUserData();
            }

            return View(pharmy);
            
        }

        [Authenticate(Group.PharmacyAdmin, Group.PPOkAdmin)]
        public ActionResult DeletePharmacist(long id)
        {
            long pharmacyId = DatabasePharmacistService.GetById(id).PharmacyId;
            DatabasePharmacistService.Disable((int)id);
            if (DatabaseUserService.GetById((long)Session["user_id"]).Type == Models.User.UserType.PPOkAdmin)
            {
                return RedirectToAction("AddorEditPharmacy", "PpokAdmin", new { id = pharmacyId });
            }
            return RedirectToAction("Admin", "Pharmacy");
        }
        #endregion

        #region Refills
        /// //////////////////////////////////////////////////////////
        /// Refills
        /// //////////////////////////////////////////////////////////

        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult RefillListView()
        {
            var refills = DatabasePrescriptionService.GetAllWithRefill((long)Session["pharm_id"]);
            var ready = from e in refills
                        where e.Key.RefillIt
                        select e;

            return View("RefillListView", Tuple.Create(ready, refills));
        }

        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult SetFilled(long id)
        {
            Refill r = DatabaseRefillService.GetById((int)id);
            r.SetFilled();
            DatabaseRefillService.Update(r);

            return RefillListView();
        }

        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult DeleteRefill(long id)
        {
            Refill r = DatabaseRefillService.GetById((int)id);
            r.RefillIt = false;
            DatabaseRefillService.Update(r);

            return RefillListView();
        }

        // TODO     public ActionResult DeleteRefill(long id)
        #endregion

        #region Patients
        /// //////////////////////////////////////////////////////////
        /// Patients
        /// //////////////////////////////////////////////////////////

        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult PatientListView()
        {
            var patients = DatabasePatientService.GetAllActive((long)Session["pharm_id"]);
            foreach (var p in patients)
            {
                p.LoadUserData();
            }
            return View("PatientListView", patients);
        }

        [HttpPost]
        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult SavePatient(Patient m, String command)
        {
            // if id's are default, get actual id's for the (new) patient
            // use sql to save patient to db

            if (m.PatientId == 0)
            {
                m.PharmacyId = (long)Session["pharm_id"];
                var pid = DatabaseUserService.Insert((User)m);
                m.UserId = pid;
                DatabasePatientService.Insert(m);
            }
            else
            {
                DatabaseUserService.Update(m);
                DatabasePatientService.Update(m);
            }

            return PatientListView();
        }

        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult DeletePatient(long id)
        {
            DatabasePatientService.UpdateInactive(DatabasePatientService.GetById((int)id));
            return PatientListView();
        }

        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult AddorEditPatient(long id = 0)
        {
            Patient patient = new Patient();

            if (id != 0)
            {
                patient = DatabasePatientService.GetById((int)id);
                patient.LoadUserData();
            }
                
            return View(patient);
        }

        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult CycleMethod(long id)
        {
            Patient thisGuy = DatabasePatientService.GetById(id);
            thisGuy.ContactMethod = DatabasePatientService.GetById(id).ContactMethod == Patient.PrimaryContactMethod.Call ?
                Patient.PrimaryContactMethod.Email : DatabasePatientService.GetById(id).ContactMethod == Patient.PrimaryContactMethod.Email ?
                Patient.PrimaryContactMethod.Text : Patient.PrimaryContactMethod.Call;
            DatabasePatientService.Update(thisGuy);
            return RedirectToAction("PatientListView");
        }
        #endregion

        #region Upload
        // pharmacy uploading patients
        // from csv
        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid) {
	            if (!CsvService.ParseCsv(upload, (long) Session["pharm_id"])) {
					ModelState.AddModelError("File", "Error parsing file");
				}
            }
            return RefillListView();
        }

        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult UploadRecalls()
        {
            var pharm = DatabasePharmacyService.GetById((long)Session["pharm_id"]);
            pharm.GetTemplates();
            return View(pharm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult UploadRecalls(HttpPostedFileBase upload, string recallMessage)
        {
            var pharm = DatabasePharmacyService.GetById((long)Session["pharm_id"]);
            pharm.GetTemplates();
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        var stream = upload.InputStream;
                        var csvTable = new DataTable();
                        using (var csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        foreach (DataRow row in csvTable.Rows)
                        {
	                        var patient = new Patient {
		                        FirstName = row["PatientFirstName"].ToString(),
		                        LastName = row["PatientLastName"].ToString(),
		                        Phone = row["Phone"].ToString(),
		                        PharmacyId = 1,
		                        DateOfBirth = DateTime.Now,
		                        Email = "ante@ante.com",
		                        ContactMethod = Patient.PrimaryContactMethod.Call,
		                        PreferedContactTime = DateTime.Now,
		                        PersonCode = row["PersonCode"].ToString()
	                        };
	                        var id =  DatabaseUserService.Insert(patient);
                            patient.UserId = id;
                            patient.PatientId = DatabasePatientService.Insert(patient);
                            var notification = new Notification(DateTime.Now, patient.PatientId, Notification.NotificationType.Recall, recallMessage);
                            DatabasePatientService.Disable(patient.PatientId);
                            DatabaseNotificationService.Insert(notification);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View(pharm);
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View(pharm);
        }
        #endregion

        #region Admin
        /// //////////////////////////////////////////////////////////
        /// Administrators
        /// //////////////////////////////////////////////////////////

        [Authenticate(Group.PharmacyAdmin, Group.PPOkAdmin)]
        public ActionResult Admin()
        {
            var id = DatabasePharmacistService.GetByUserId((long)Session[Login.UserIdSession]).PharmacyId;
            var pharmacy = DatabasePharmacyService.GetById(id);
            pharmacy.GetTemplates();

            return View(pharmacy);
        }

        // This is kinda bad, but it's a controller method, so it's never called manually.
        // Just didn't quite get around to moving this to a POCO :(
        [HttpPost]
        [Authenticate(Group.PharmacyAdmin, Group.PPOkAdmin)]
        public ActionResult Admin(
            string refillTextTemplate, string refillPhoneTemplate, string refillEmailTemplate,
            string pickupTextTemplate, string pickupPhoneTemplate, string pickupEmailTemplate,
            string recallTextTemplate, string recallPhoneTemplate, string recallEmailTemplate,
            string birthdayTextTemplate, string birthdayPhoneTemplate, string birthdayEmailTemplate,
            string notificationDisabledTextTemplate, string notificationDisabledPhoneTemplate, string notificationDisabledEmailTemplate,
            string pharmacyName, string pharmacyPhone, string pharmacyAddress, long pharmacyId)
        {
            var pharmacy = DatabasePharmacyService.GetById(pharmacyId);
            pharmacy.GetTemplates();

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


            return View(pharmacy);
        }
        #endregion

    }
}