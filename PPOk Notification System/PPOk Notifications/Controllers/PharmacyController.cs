using LumenWorks.Framework.IO.Csv;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PPOk_Notifications.Controllers
{
    [AllowAnonymous]
    public class PharmacyController : BaseController
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
            SQLService serv = new SQLService();
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
            return Redirect("/Pharmacy/PhamacistListView");
        }
        public ActionResult AddPharmacist(long id = 0)
        {

            SQLService database = new SQLService();
            Pharmacist pharmy = new Pharmacist();

            if (id != 0)
                pharmy = database.GetPharmacistById((int)id);

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
            var db = new SQLService();
            db.Pharmacist_Disable((int)id);
            return Redirect("/Pharmacy/PhamacistListView");
        }



        /// //////////////////////////////////////////////////////////
        /// Refills
        /// //////////////////////////////////////////////////////////


        //[HttpPost]
        public ActionResult RefillListView()
        {
            SQLService serv = new SQLService();
            var refills = serv.GetRefillsActive();
            List<Refill> ready = new List<Refill>();
            foreach (var r in refills)
            {
                if (r.RefillIt)
                {
                    ready.Add(r);
                }
            }
            return View(refills);
        }
        public ActionResult SetFilled(long id)
        {
            var db = new SQLService();
            db.GetRefillById((int)id).SetFilled();
            return Redirect("/Pharmacy/RefillListView");
        }



        /// //////////////////////////////////////////////////////////
        /// Patients
        /// //////////////////////////////////////////////////////////

        //[HttpPost]
        public ActionResult PatientListView()
        {
            IEnumerable<Patient> param = new List<Patient>();
            SQLService serv = new SQLService();
            //((List<Patient>)param).AddRange(serv.GetPatients());

            return View(param);
        }
        [HttpPost]
        public ActionResult SavePatient(Patient m, int page = 0)
        {
            // if id's are default, get actual id's for the (new) patient
            // use sql to save patient to db
            return Redirect("/Pharmacy/PatientListView");
        }
//        public ActionResult DeletePatient() { }
  //      public ActionResult DetailsPatient() { }
    //    public ActionResult EditPatient() { }
        public ActionResult AddPatient(long id = 0)
        {
            SQLService database = new SQLService();
            Patient patient = new Patient();

            if (id != 0)
                patient = database.GetPatientById((int)id);

            if (Request.IsAjaxRequest())
            {
                return PartialView("AddPharmacist", patient);
            }
            else
            {
                return View(patient);
            }
        }
        public ActionResult CycleMethod(long id)
        {
            var db = new SQLService();
            db.GetPatientById(id).ContactMethod = db.GetPatientById(id).ContactMethod == Patient.PrimaryContactMethod.Call ?
                Patient.PrimaryContactMethod.Email : db.GetPatientById(id).ContactMethod == Patient.PrimaryContactMethod.Email ?
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
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        SQLService ser = new SQLService();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        // return View(csvTable);
                        foreach (DataRow row in csvTable.Rows)
                        {
                            int id = int.Parse(row["PersonCode"].ToString());

                            if (ser.GetPatientById(id) == null)
                            {
                                Patient patient = new Patient();
                                Prescription prescription = new Prescription();
                                patient.PersonCode = row["PersonCode"].ToString();
                                patient.FirstName = row["PatientFirstName"].ToString();
                                patient.LastName = row["PatientLastName"].ToString();
                                patient.Phone = row["Phone"].ToString();
                                patient.Email = row["Email"].ToString();
                                patient.DateOfBirth = DateTime.ParseExact(row["DOB"].ToString(), "yyyyMMdd",null);
                                var dateNow = DateTime.Now;
                                patient.PreferedContactTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 4, 5, 6);
                                patient.ContactMethod = Patient.PrimaryContactMethod.Call;
                                patient.PharmacyId = 1;
                                prescription.PrescriptionName = row["GPIGenericName"].ToString();
                                prescription.PrescriptionDateFilled = DateTime.ParseExact(row["DateFilled"].ToString(), "yyyyMMdd", null);
                                System.Diagnostics.Debug.WriteLine(prescription.PrescriptionDateFilled.ToLongDateString());
                                prescription.PrescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                                prescription.PrescriptionRefills = int.Parse(row["NumberOfRefills"].ToString());
                                prescription.PrescriptionUpc = row["NDCUPCHRI"].ToString();
                                prescription.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());
                                var ID = ser.UserInsert(patient);
                                patient.UserId = ID;
                                patient.PatientId = ser.PatientInsert(patient);
                                prescription.PatientId = patient.PatientId;
                                var preid = ser.PrescriptionInsert(prescription);
                                prescription.PrecriptionId = preid;
                                Refill refill = new Refill(prescription);
                                refill.RefillDate = prescription.PrescriptionDateFilled.AddDays(prescription.PrescriptionDaysSupply - 2);
                                ser.RefillInsert(refill);

                            }
                            else if (ser.GetPatientById(id) != null)
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

                            }

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
        public ActionResult UploadRecalls(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        foreach (DataRow row in csvTable.Rows)
                        {
                            SQLService ser = new SQLService();
                            Patient patient = new Patient();
                            patient.FirstName = row["PatientFirstName"].ToString();
                            patient.LastName = row["PatientLastName"].ToString();
                            patient.Phone = row["Phone"].ToString();
                            var id =  ser.UserInsert(patient);
                            patient.UserId = id;
                            patient.PatientId = ser.PatientInsert(patient);
                            Notification notification = new Notification(DateTime.Now, patient.PatientId, Notification.NotificationType.Recall,"");
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

        public ActionResult Admin()
        {
            Pharmacy pharmacy = new SQLService().GetPharmacyById(1);
            pharmacy.GetTemplates();
            return View(pharmacy);
        }

        [HttpPost]
        public ActionResult Admin(
            string refillTextTemplate, string refillPhoneTemplate, string refillEmailTemplate,
            string pickupTextTemplate, string pickupPhoneTemplate, string pickupEmailTemplate,
            string recallTextTemplate, string recallPhoneTemplate, string recallEmailTemplate,
            string birthdayTextTemplate, string birthdayPhoneTemplate, string birthdayEmailTemplate,
            string notificationDisabledTextTemplate, string notificationDisabledPhoneTemplate, string notificationDisabledEmailTemplate)
        {
            SQLService service = new SQLService();
            Pharmacy pharmacy = service.GetPharmacyById(1);
            pharmacy.GetTemplates();

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

            service.PharmacyUpdate(pharmacy);

            return View();
        }
    }
}