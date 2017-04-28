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
            return View();
        }

        public ActionResult Pharmacy()
        {
            return View();
        }



        /// //////////////////////////////////////////////////////////
        /// Pharmacists
        /// //////////////////////////////////////////////////////////



        //[HttpPost]
        public ActionResult PharmacistListView()
        {
            IEnumerable<Pharmacist> param = new List<Pharmacist>();
            // FIXME sql to load in etc
            // ((List<PPOk_Notifications.Models.PharmacyUser>)param).AddRange(serv.GetPharmacists());

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
        public ActionResult SavePharmacist(Pharmacist m, int page = 0)
        {
            // if id's are default, get actual id's for the (new) pharmacist
            // use sql to save pharmacist to db

            // TODO (Incomplete)

            return Redirect("/Pharmacy/PhamacistListView");
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
            return Redirect("Pharmacy/AddPharmacist" + id);
        }

        public ActionResult DeletePharmacist(long id)
        {
            DatabasePharmacistService.Disable((int)id);
            return Redirect("/Pharmacy/PhamacistListView");
        }



        /// //////////////////////////////////////////////////////////
        /// Refills
        /// //////////////////////////////////////////////////////////


        //[HttpPost]
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
            DatabaseRefillService.GetById((int)id).SetFilled();
            return Redirect("/Pharmacy/RefillListView");
        }

        // TODO     public ActionResult DeleteRefill(long id)

        /// //////////////////////////////////////////////////////////
        /// Patients
        /// //////////////////////////////////////////////////////////

        //[HttpPost]
        public ActionResult PatientListView()
        {
            var patients = DatabasePatientService.GetAll();
            foreach (var p in patients)
            {
                p.LoadUserData();
            }
            return View(patients);
        }

        [HttpPost]
        public ActionResult SavePatient(Patient m, int page = 0)
        {
            // if id's are default, get actual id's for the (new) patient
            // use sql to save patient to db

            // TODO (Incomplete)

            return Redirect("/Pharmacy/PatientListView");
        }
//        TODO      public ActionResult DeletePatient() { }
//        TODO      public ActionResult EditPatient() { }

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
            DatabasePatientService.GetById(id).ContactMethod = DatabasePatientService.GetById(id).ContactMethod == Patient.PrimaryContactMethod.Call ?
                Patient.PrimaryContactMethod.Email : DatabasePatientService.GetById(id).ContactMethod == Patient.PrimaryContactMethod.Email ?
                Patient.PrimaryContactMethod.Text : Patient.PrimaryContactMethod.Call;
            return Redirect("/Pharmacy/PatientListView");
        }
        
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
                        foreach (DataRow row in csvTable.Rows)
                        {
                            
                            string id = row["PersonCode"].ToString();
                            if (DatabasePatientService.GetByPersonCode(id) == null)
                            {
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
                                patient.PharmacyId = 1;
                                patient.UserId = DatabaseUserService.Insert(patient);
                                patient.PatientId = DatabasePatientService.Insert(patient);

                                var prescription = new Prescription();
                                prescription.PrescriptionName = row["GPIGenericName"].ToString();
                                prescription.PrescriptionDateFilled = DateTime.ParseExact(row["DateFilled"].ToString(), "yyyyMMdd", null);
                                System.Diagnostics.Debug.WriteLine(prescription.PrescriptionDateFilled.ToLongDateString());
                                prescription.PrescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                                prescription.PrescriptionRefills = int.Parse(row["NumberOfRefills"].ToString());
                                prescription.PrescriptionUpc = row["NDCUPCHRI"].ToString();
                                prescription.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());

                                prescription.PatientId = patient.PatientId;
                                var preid = DatabasePrescriptionService.Insert(prescription);
                                prescription.PrecriptionId = preid;
                                var refill = new Refill(prescription);
                                refill.RefillDate = prescription.PrescriptionDateFilled.AddDays(prescription.PrescriptionDaysSupply - 2);
                                DatabaseRefillService.Insert(refill);

                            }
                            else if (DatabasePatientService.GetByPersonCode(id) != null)
                            {
                                Patient patient = null;
                                Prescription prescription = new Prescription();
                                prescription.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());

                                if (DatabasePatientService.GetByPersonCode(id) == null)
                                {
                                    prescription.PrescriptionName = row["GPIGenericName"].ToString();
                                    prescription.PrescriptionDateFilled = DateTime.ParseExact(row["DateFilled"].ToString(), "yyyyMMdd", null);
                                    prescription.PrescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                                    prescription.PrescriptionRefills = int.Parse(row["NumerOfRefills"].ToString());
                                    prescription.PrescriptionUpc = row["NDCUPCHRI"].ToString();
                                    prescription.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());
                                    Refill refill = new Refill(prescription);
                                    DatabasePrescriptionService.Insert(prescription);
                                    DatabaseRefillService.Insert(refill);
                                }
                                if ((patient = DatabasePatientService.GetByPersonCode(id)) != null)
                                {
                                        prescription = DatabasePrescriptionService.GetByPatientId(patient.PatientId);
                                    if (prescription != null && prescription.PrescriptionNumber != int.Parse(row["PrescriptionNumber"].ToString()))
                                    {
                                        prescription.PrescriptionName = row["GPIGenericName"].ToString();
                                        prescription.PrescriptionDateFilled = DateTime.ParseExact(row["DateFilled"].ToString(), "yyyyMMdd", null);
                                        prescription.PrescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                                        prescription.PrescriptionRefills = int.Parse(row["NumberOfRefills"].ToString());
                                        prescription.PrescriptionUpc = row["NDCUPCHRI"].ToString();
                                        prescription.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());
                                        Refill refill = new Refill(prescription);
                                        DatabasePrescriptionService.Insert(prescription);
                                        DatabaseRefillService.Insert(refill);
                                    }

                                    if (prescription.PrescriptionNumber == int.Parse(row["PrescriptionNumber"].ToString()))
                                    {
                                        int refills1 = int.Parse(row["NumberOfRefills"].ToString());
                                        if (refills1 > 0)
                                        {

                                            Refill refill = new Refill(prescription);
                                            DatabaseRefillService.Insert(refill);
                                        }
                                    }
                                }

                            }

                        }
                        var allPatients = DatabasePatientService.GetAll();
                        foreach (var p in allPatients)
                        {
                            var personCode = p.PersonCode;
                            var count = 0;
                       
                            foreach (DataRow row in csvTable.Rows)
                            {
                                if(personCode == row["PersonCode"].ToString())
                                {
                                    count++;
                                }
                            }
                            if (count > 0)
                            {
                                DatabasePatientService.Enable(p.PatientId);
                            }
                            else if (count == 0)
                            {
                                DatabasePatientService.Disable(p.PatientId);
                            }
                        }
                        var allrefills = DatabaseRefillService.GetAllActive();
                        var ready1 = new List<Refill>();
                        foreach (var r in allrefills)
                        {
                            if (r.RefillIt)
                            {
                                ready1.Add(r);
                            }
                        }
                        return View("~/Views/Pharmacy/RefillListView.cshtml", ready1);
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
            var refills = DatabaseRefillService.GetAllActive();
            var ready = new List<Refill>();
            foreach (var r in refills)
            {
                if (r.RefillIt)
                {
                    ready.Add(r);
                }
            }
            return View("~/Views/Pharmacy/RefillListView.cshtml", ready);
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
    }
}