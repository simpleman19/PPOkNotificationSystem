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
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.PharmacyUser> param = new List<PPOk_Notifications.Models.PharmacyUser>();
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
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.PharmacyUser> param = new List<PPOk_Notifications.Models.PharmacyUser>();
            // FIXME sql to load in etc
            // ((List<PPOk_Notifications.Models.PharmacyUser>)param).AddRange(serv.GetPharmacists());
            List<PPOk_Notifications.Models.PharmacyUser> filtered = new List<PPOk_Notifications.Models.PharmacyUser>();

            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var item in param)
                {
                    if (item.Email.ToString().Contains(searchString) ||
                        item.FirstName.ToString().Contains(searchString) ||
                        item.LastName.ToString().Contains(searchString) ||
                        item.Phone.ToString().Contains(searchString))
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
            else
            {
                return View(filtered);
            }
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
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.Refill> param = new List<PPOk_Notifications.Models.Refill>();

            // FIXME: key not found exception in SQL services    ((List<PPOk_Notifications.Models.Refill>)param).AddRange(serv.GetRefills());

            if (Request.IsAjaxRequest())
            {
                return PartialView("RefillListView", param);
            }
            else
            {
                return View(param);
            }
        }
        [HttpGet]
        public ActionResult RefillListView(string searchString)
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.Refill> param = new List<PPOk_Notifications.Models.Refill>();
        
            // FIXME: key not found exception in SQL services    ((List<PPOk_Notifications.Models.Refill>)param).AddRange(serv.GetRefills());
        
            List<PPOk_Notifications.Models.Refill> filtered = new List<PPOk_Notifications.Models.Refill>();

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
                return PartialView("RefillListView", filtered);
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
            IEnumerable<PPOk_Notifications.Models.Patient> param = new List<PPOk_Notifications.Models.Patient>();
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            ((List<PPOk_Notifications.Models.Patient>)param).AddRange(serv.GetPatients());

            if (Request.IsAjaxRequest())
            {
                return PartialView("PatientListView", new Tuple<IEnumerable<PPOk_Notifications.Models.Patient>, PPOk_Notifications.Service.SQLService>(param, serv));
            }
            else
            {
                return View(new Tuple<IEnumerable<PPOk_Notifications.Models.Patient>, PPOk_Notifications.Service.SQLService>(param, serv));
            }
        }

        [HttpGet]
        public ActionResult PatientListView(string searchString)
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            List<PPOk_Notifications.Models.Patient> param = new List<PPOk_Notifications.Models.Patient>();
            List<PPOk_Notifications.Models.Patient> filtered = new List<PPOk_Notifications.Models.Patient>();
            param.AddRange(serv.GetPatients());
            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var item in param)
                {
                    if (item.FirstName.ToString().Contains(searchString) ||
                        item.LastName.ToString().Contains(searchString) ||
                        item.PatientId.ToString().Contains(searchString) ||
                        item.Phone.ToString().Contains(searchString))
                    {
                        filtered.Add(item);
                    }
                }
            }
            else { filtered = param; }

            if (Request.IsAjaxRequest())
            {
                return PartialView("PatientListView", filtered);
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
    }
}