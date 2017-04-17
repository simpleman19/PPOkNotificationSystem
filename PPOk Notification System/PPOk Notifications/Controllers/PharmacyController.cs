using LumenWorks.Framework.IO.Csv;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using PPOk_Notifications.Filters;

namespace PPOk_Notifications.Controllers
{
    [Authenticate]
    public class PharmacyController : Controller
    {
        // GET: Pharmacy
        public ActionResult Index()
        {
            return RedirectToAction("RefillListView");
        }

        #region Pharmacists
        /// //////////////////////////////////////////////////////////
        /// Pharmacists
        /// //////////////////////////////////////////////////////////


        //[HttpPost]
        public ActionResult PharmacistListView()
        {
            List<Pharmacist> param = new List<Pharmacist>();

            param.AddRange(DatabasePharmacistService.GetAll());

            foreach (var p in param)
            {
                p.LoadUserData();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("PharmacistListView", param);
            }
            else
            {
                return View(param);
            }
        }

        [HttpPost]
        public ActionResult SavePharmacist(Pharmacist m, String command)
        {
            // if id's are default, get actual id's for the (new) pharmacist
            // use sql to save pharmacist to db
            if (m.PharmacyId == 0)
            {
                var phid = DatabaseUserService.Insert((User)m);
                m.UserId = phid;
                DatabasePharmacistService.Insert(m);
            }
            else
            {
                DatabaseUserService.Update((User)m);
                DatabasePharmacistService.Update(m);
            }

            return RedirectToAction("PharmacistListView");
        }

        public ActionResult AddPharmacist(long id = 0)
        {
            var pharmy = new Pharmacist();

            if (id != 0)
                pharmy = DatabasePharmacistService.GetById((int)id);

            if (Request.IsAjaxRequest())
            {
                return PartialView("AddPharmacist", pharmy);
            }
            else
            {
                return View(pharmy);
            }
        }

        public ActionResult EditPharmacist(long id)
        {
            return RedirectToAction("AddPharmacist", new { id });
        }

        public ActionResult DeletePharmacist(long id)
        {
            DatabasePharmacistService.Disable((int)id);
            return RedirectToAction("PhamacistListView");
        }
        #endregion


        #region Refills
        /// //////////////////////////////////////////////////////////
        /// Refills
        /// //////////////////////////////////////////////////////////

        public ActionResult RefillListView()
        {
            var refills = DatabaseRefillService.GetAllActive();
            var ready = new List<Refill>();
            foreach (var r in refills)
            {
                if (r.RefillIt)
                {
                    ready.Add(r);
                }
            }
            return View(ready);
        }

        public ActionResult SetFilled(long id)
        {
            Refill r = DatabaseRefillService.GetById((int)id);
            r.SetFilled();
            DatabaseRefillService.Update(r);

            return RedirectToAction("RefillListView");
        }


        public ActionResult DeleteRefill(long id)
        {
            Refill r = DatabaseRefillService.GetById((int)id);
            r.RefillIt = false;
            DatabaseRefillService.Update(r);

            return RedirectToAction("RefillListView");
        }

        // TODO     public ActionResult DeleteRefill(long id)
        #endregion

        #region Patients
        /// //////////////////////////////////////////////////////////
        /// Patients
        /// //////////////////////////////////////////////////////////

        public ActionResult PatientListView()
        {
            var patients = DatabasePatientService.GetAllActive();
            foreach (var p in patients)
            {
                p.LoadUserData();
            }
            return View(patients);
        }

        [HttpPost]
        public ActionResult SavePatient(Patient m, String command)
        {
            // if id's are default, get actual id's for the (new) patient
            // use sql to save patient to db

            if (m.PatientId == 0)
            {
                var pid = DatabaseUserService.Insert((User)m);
                m.UserId = pid;
                DatabasePatientService.Insert(m);
            }
            else
            {
                DatabaseUserService.Update((User)m);
                DatabasePatientService.Update(m);
            }

            return RedirectToAction("PatientListView");
        }

        public ActionResult EditPatient(long id)
        {
            return RedirectToAction("AddPatient", new { id });
        }

        public ActionResult DeletePatient(long id)
        {
            DatabasePatientService.UpdateInactive(DatabasePatientService.GetById((int)id));
            return RedirectToAction("PatientListView");
        }

        public ActionResult AddPatient(long id = 0)
        {
            Patient patient = new Patient();

            if (id != 0)
                patient = DatabasePatientService.GetById((int)id);

            if (Request.IsAjaxRequest())
            {
                return PartialView("AddPatient", patient);
            }
            else
            {
                return View(patient);
            }
        }

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
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
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
                        // return View(csvTable);
                        foreach (DataRow row in csvTable.Rows)
                        {
                            //int id = int.Parse(row["PersonCode"].ToString());

                           // if (ser.GetPatientById(id) == null)
                            //{
                                var patient = new Patient();
                                patient.PersonCode = row["PersonCode"].ToString();
                                patient.FirstName = row["PatientFirstName"].ToString();
                                patient.LastName = row["PatientLastName"].ToString();
                                patient.Phone = row["Phone"].ToString();
                                patient.Email = row["Email"].ToString();
                                patient.DateOfBirth = DateTime.ParseExact(row["DOB"].ToString(), "yyyyMMdd", null);
                                var dateNow = DateTime.Now;
                                patient.PreferedContactTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 4, 5, 6);
                                patient.ContactMethod = Patient.PrimaryContactMethod.Call;
                                patient.PharmacyId = 1; //FIXME: shouldn't this get the current pharmacy's ID?

                                var prescription = new Prescription();
                                prescription.PrescriptionName = row["GPIGenericName"].ToString();
                                prescription.PrescriptionDateFilled = DateTime.ParseExact(row["DateFilled"].ToString(), "yyyyMMdd", null);
                                System.Diagnostics.Debug.WriteLine(prescription.PrescriptionDateFilled.ToLongDateString());
                                prescription.PrescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                                prescription.PrescriptionRefills = int.Parse(row["NumberOfRefills"].ToString());
                                prescription.PrescriptionUpc = row["NDCUPCHRI"].ToString();
                                prescription.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());

                                var ID = DatabaseUserService.Insert(patient);
                                patient.UserId = ID;
                                patient.PatientId = DatabasePatientService.Insert(patient);
                                DatabasePatientService.Insert(patient);		//FIXME: Was this intended? You're inserting the patient twice?
                                prescription.PatientId = patient.PatientId;
                                var preid = DatabasePrescriptionService.Insert(prescription);
                                prescription.PrecriptionId = preid;
                                DatabasePrescriptionService.Insert(prescription);
                                var refill = new Refill(prescription);
                                refill.RefillDate = prescription.PrescriptionDateFilled.AddDays(prescription.PrescriptionDaysSupply - 2);
                                DatabaseRefillService.Insert(refill);

                            //}
                           /* else if (ser.GetPatientById(id) != null)
                            {
                                Prescription prescription = new Prescription();
                                prescription.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());

                                if (ser.GetPrescriptionByPatientId(id) == null)
                                {
                                    Prescription prescription1 = new Prescription();
                                    prescription1.PrescriptionName = row["GPIGenericName"].ToString();
                                    prescription1.PrescriptionDateFilled = DateTime.ParseExact(row["DateFilled"].ToString(), "yyyyMMdd", null);
                                    prescription1.PrescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                                    prescription1.PrescriptionRefills = int.Parse(row["NumerOfRefills"].ToString());
                                    prescription1.PrescriptionUpc = row["NDCUPCHRI"].ToString();
                                    prescription1.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());
                                    Refill refill = new Refill(prescription1);
                                    ser.PrescriptionInsert(prescription1);
                                    ser.RefillInsert(refill);
                                }
                                if (ser.GetPrescriptionByPatientId(id) != null)
                                {
                                    Prescription prescription1 = ser.GetPrescriptionByPatientId(id);
                                    if (prescription1.PrescriptionNumber != int.Parse(row["PrescriptionNumber"].ToString()))
                                    {
                                        Prescription prescription2 = new Prescription();
                                        prescription2.PrescriptionName = row["GPIGenericName"].ToString();
                                        prescription2.PrescriptionDateFilled = DateTime.ParseExact(row["DateFilled"].ToString(), "yyyyMMdd", null);
                                        prescription2.PrescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                                        prescription2.PrescriptionRefills = int.Parse(row["NumberOfRefills"].ToString());
                                        prescription2.PrescriptionUpc = row["NDCUPCHRI"].ToString();
                                        prescription2.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());
                                        Refill refill = new Refill(prescription);
                                        ser.PrescriptionInsert(prescription2);
                                        ser.RefillInsert(refill);
                                    }

                                    if (prescription1.PrescriptionNumber == int.Parse(row["PrescriptionNumber"].ToString()))
                                    {
                                        int refills = int.Parse(row["NumberOfRefills"].ToString());
                                        if (refills > 0)
                                        {

                                            Refill refill = new Refill(prescription1);
                                        }
                                    }
                                }

                            }*/

                        }
                        RedirectToAction("Upload", "Pharmacy");
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }

        public ActionResult UploadRecalls()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadRecalls(HttpPostedFileBase upload, string recallMessage)
        {
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
                            DatabaseNotificationService.Insert(notification);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }
        #endregion

        #region Admin
        /// //////////////////////////////////////////////////////////
        /// Administrators
        /// //////////////////////////////////////////////////////////

        public ActionResult Admin()
        {
            var id = DatabasePharmacistService.GetByUserId((long)Session[Login.UserIdSession]).PharmacyId;
            var pharmacy = DatabasePharmacyService.GetById(id);
            pharmacy.GetTemplates();

            return View(pharmacy);
        }

        [HttpPost]
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