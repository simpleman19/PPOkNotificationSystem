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

        [HttpPost]
        public ActionResult PharmacistListView()
        {
            SQLService serv = new SQLService();
            IEnumerable<PharmacyUser> param = new List<PharmacyUser>();
            // FIXME sql to load in etc
            // ((List<PPOk_Notifications.Models.PharmacyUser>)param).AddRange(serv.GetPharmacists());

            if (Request.IsAjaxRequest())
            {
                return PartialView("PharmacistListView", param);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult PharmacistListView(string searchString)
        {
            SQLService serv = new SQLService();
            List<PharmacyUser> param = new List<PharmacyUser>();
            // FIXME sql to load in etc
            // ((List<PPOk_Notifications.Models.PharmacyUser>)param).AddRange(serv.GetPharmacists());
            List<PharmacyUser> filtered = new List<PharmacyUser>();

            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var item in param)
                {
                    if (item.Email.Contains(searchString) ||
                        item.FirstName.Contains(searchString) ||
                        item.LastName.Contains(searchString) ||
                        item.Phone.Contains(searchString))
                    {
                        filtered.Add(item);
                    }
                }
            }
            else { filtered = param; }

            if (Request.IsAjaxRequest())
            {
                return PartialView("PharmacistListView", filtered);
            }

            return View(filtered);
        }
        public ActionResult AddPharmacist(long id)
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
            return Redirect("Pharmacy/AddPharmacist"  + id);
        }
        public ActionResult DeletePharmacist(long id)
        {
            var db = new SQLService();
            db.Pharmacist_Disable((int)id);
            return Redirect("/Pharmacy/PhamacistListView");
        }
        
        [HttpPost]
        public ActionResult RefillListView()
        {
            SQLService serv = new SQLService();
            IEnumerable<Refill> param = new List<Refill>();

            // FIXME: key not found exception in SQL services    ((List<PPOk_Notifications.Models.Refill>)param).AddRange(serv.GetRefills());

            if (Request.IsAjaxRequest())
            {
                return PartialView("RefillListView", Tuple.Create(param, serv));
            }
            else
            {
                return View(param);
            }
        }
        [HttpGet]
        public ActionResult RefillListView(string searchString)
        {
            SQLService serv = new SQLService();
            List<Refill> param = new List<Refill>();

            // FIXME: key not found exception in SQL services    ((List<PPOk_Notifications.Models.Refill>)param).AddRange(serv.GetRefills());

            List<Refill> filtered = new List<Refill>();

            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var item in param)
                {
                    if (item.PrescriptionId.ToString().Contains(searchString) ||
                        item.RefillId.ToString().Contains(searchString)) {
                        filtered.Add(item);
                    }
                }
            }
            else { filtered = param; }

            if (Request.IsAjaxRequest())
            {
                return PartialView("RefillListView", Tuple.Create((IEnumerable<Refill>) filtered, serv));
            }
            else
            {
                return View(filtered);
            }

        }

        public ActionResult ToggleComplete(long id)
        {
            var db = new SQLService();
            db.GetRefillById((int)id).Refilled = !db.GetRefillById((int)id).Refilled;
            return Redirect("/Pharmacy/RefillListView");
        }

        public ActionResult SendNotification(long id)
        {
            // TODO
            return View();
        }
        

        [HttpPost]
        public ActionResult PatientListView()
        {
            IEnumerable<Patient> param = new List<Patient>();
            SQLService serv = new SQLService();
            ((List<Patient>)param).AddRange(serv.GetPatients());

            if (Request.IsAjaxRequest())
            {
                return PartialView("PatientListView", new Tuple<IEnumerable<Patient>, SQLService>(param, serv));
            }
            else
            {
                return View(new Tuple<IEnumerable<Patient>, SQLService>(param, serv));
            }
        }

        [HttpGet]
        public ActionResult PatientListView(string searchString)
        {
            SQLService serv = new SQLService();
            List<Patient> param = new List<Patient>();
            List<Patient> filtered = new List<Patient>();
            param.AddRange(serv.GetPatients());
            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var item in param)
                {
                    if (item.FirstName.Contains(searchString) ||
                        item.LastName.Contains(searchString) ||
                        item.PatientId.ToString().Contains(searchString) ||
                        item.Phone.Contains(searchString))
                    {
                        filtered.Add(item);
                    }
                }
            }
            else { filtered = param; }

            if (Request.IsAjaxRequest())
            {
                return PartialView("PatientListView", Tuple.Create((IEnumerable<Patient>) filtered, serv));
            }
            else
            {
                return View(filtered);
            }
        }
        public ActionResult AddPatient()
        {
            // TODO: Will need a view or modal for this
            return View();
        }
        public ActionResult CycleMethod(long id)
        {
            var db = new SQLService();
            db.GetPatientById(id).ContactMethod = db.GetPatientById(id).ContactMethod==Patient.PrimaryContactMethod.Call?
                Patient.PrimaryContactMethod.Email : db.GetPatientById(id).ContactMethod == Patient.PrimaryContactMethod.Email?
                Patient.PrimaryContactMethod.Text : Patient.PrimaryContactMethod.Call;
            return Redirect("/Pharmacy/PatientListView");
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
                            //SqlService.AddPatient(patient);
                            //SqlService.AddPrescription(prescription);
                            //SqlService.AddRefill(refill);
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