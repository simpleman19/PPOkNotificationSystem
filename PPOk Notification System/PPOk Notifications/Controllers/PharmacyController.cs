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

        [HttpGet]
        public ActionResult PharmacistListView(string searchString)
        {
            SQLService db = new SQLService();
            // FIXME: Key not in dictionary var pharms = db.GetPharmacistsActive();
            var pharms = new List<PharmacyUser>();
            return View(pharms);
        }

        public ActionResult AddPharmacist(long id)
        {
            SQLService database = new SQLService();
            Pharmacist pharmy = new Pharmacist();

            //if (id != 0)
            //    pharmy = database.GetPharmacistById((int)id);

            return View(pharmy);
        }

        public ActionResult EditPharmacist(long id)
        {
            return Redirect("Pharmacy/AddPharmacist/" + id);
        }

        public ActionResult DeletePharmacist(long id)
        {
            var db = new SQLService();
            //db.Pharmacist_Disable((int)id);
            return Redirect("/Pharmacy/PhamacistListView");
        }
        
        public ActionResult RefillListView(string searchString)
        {
            var db = new SQLService();
            //var refills = db.GetRefillsActive();
            List<Refill> refills = new List<Refill>();
            for (int i = 0; i < 10; i++)
            {
                refills.Add(Refill.getTestRefill());
            }

            return View(refills);
        }

        public ActionResult SetFilled(long id)
        {
            var db = new SQLService();
           // db.GetRefillById((int)id).Refilled = !db.GetRefillById((int)id).Refilled;
            return Redirect("/Pharmacy/RefillListView");
        }

        public ActionResult DeleteRefill(long id)
        {
            var db = new SQLService();
            // db.Refill_Disable((int)id);
            return Redirect("/Pharmacy/RefillListView");
        }

        public ActionResult PatientListView(string searchString)
        {
            var db = new SQLService();
            //var patients = db.GetPatientsActive();
            List<Patient> patients = new List<Patient>();
            for (int i = 0; i < 10; i++)
            {
                patients.Add(Patient.getTestPatient());
            }

            return View(patients);
        }
        public ActionResult AddPatient(long id)
        {
            var patient = new Patient();
            if (id != 0) {
                //get patient data and put in patient.. hafta wait on db stuff
            }
            // TODO: Will need a view or modal for this
            return View(patient);
        }
        public ActionResult CycleMethod(long id)
        {
            var db = new SQLService();
            /*
            db.GetPatientById(id).ContactMethod = db.GetPatientById(id).ContactMethod==Patient.PrimaryContactMethod.Call?
                Patient.PrimaryContactMethod.Email : db.GetPatientById(id).ContactMethod == Patient.PrimaryContactMethod.Email?
                Patient.PrimaryContactMethod.Text : Patient.PrimaryContactMethod.Call;
            */
            return Redirect("/Pharmacy/PatientListView");
        }
        public ActionResult EdiPatient(long id) {
            return Redirect("/Pharmacy/AddPatient/" + id);
        }
        public ActionResult DetailsPatient(long id) {
            return Redirect("/Pharmacy/AddPatient/" + id);
        }
        public ActionResult DeletePatient(long id) {
            return Redirect("/Pharmacy/AddPatient/" + id);
        }

        // pharmacy uploading patients
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
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        return View(csvTable);
                        
                        foreach (DataRow row in csvTable.Rows)
                        {
                            var db = new SQLService();
                            //TODO check if patient exist in the databse already.
                            //If it does, then check prescriptions for that patients if they are in databse.
                            //If yes, check for new refills.
                            //If there is new refills.
                            //----prescription.prescriptionDateFilled = DateTime.Parse(row["DateFilled"].ToString());
                            //----Refill refill = new Refill (prescription);
                            //If there is a new prescription save it to the database.
                            Prescription prescription = new Prescription();
                            prescription.PrescriptionName = row["GPIGenericName"].ToString();
                            prescription.PrescriptionDateFilled = DateTime.Parse(row["DateFilled"].ToString());
                            prescription.PrescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                            prescription.PrescriptionRefills = int.Parse(row["NumerOfRefills"].ToString());
                            prescription.PrescriptionUpc = row["NDCUPCHRI"].ToString();
                            prescription.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());
                            //SqlService.AddPrescription(prescription);
                            //If patient doesnt exist in the database, create a new patient and save it in the db along with their prescriptions.
                            Patient patient = new Patient();
                            Prescription prescription1 = new Prescription();
                            patient.PersonCode = int.Parse(row["PersonCode"].ToString());
                            patient.FirstName = row["PatientFirstName"].ToString();
                            patient.LastName = row["PatientLastName"].ToString();
                            patient.Phone = row["Phone"].ToString();
                            patient.Email = row["Email"].ToString();
                            prescription.PrescriptionName = row["GPIGenericName"].ToString();
                            prescription.PrescriptionDateFilled = DateTime.Parse(row["DateFilled"].ToString());
                            prescription.PrescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                            prescription.PrescriptionRefills = int.Parse(row["NumerOfRefills"].ToString());
                            prescription.PrescriptionUpc = row["NDCUPCHRI"].ToString();
                            prescription.PrescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());
                            Refill refill = new Refill(prescription);
                            db.PatientInsert(patient);
                            db.PrescriptionInsert(prescription);
                            db.RefillInsert(refill);
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
            pharmacy.SaveTemplates();

            return Redirect("/Pharmacy/Admin");
        }
    }
}