using LumenWorks.Framework.IO.Csv;
using PPOk_Notifications.Models;
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

        // TODO additional actions and such need to be supported for this view.
        [HttpPost]
        public ActionResult PharmacistListView()
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.PharmacyUser> param = new List<PPOk_Notifications.Models.PharmacyUser>();
            // FIXME sql to load in etc
            // ((List<PPOk_Notifications.Models.PharmacyUser>)param).AddRange(serv.GetPharmacists());
            // pharmacy users are not pharmacists !?  Something is off within the models...
            return View(param);
        }
        [HttpGet]
        public ActionResult PharmacistListView(string searchString)
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.PharmacyUser> param = new List<PPOk_Notifications.Models.PharmacyUser>();
            List<PPOk_Notifications.Models.PharmacyUser> filtered = new List<PPOk_Notifications.Models.PharmacyUser>();
            // FIXME sql to load in etc
            // ((List<PPOk_Notifications.Models.PharmacyUser>)param).AddRange(serv.GetPharmacists());
            // pharmacy users are not pharmacists !?  Something is off within the models...
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
            return View(filtered);
        }
        // TODO additional actions and such need to be supported for this view.
        [HttpPost]
        public ActionResult RefillListView()
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.Refill> param = new List<PPOk_Notifications.Models.Refill>();
            // FIXME: key not found exception in SQL services    ((List<PPOk_Notifications.Models.Refill>)param).AddRange(serv.GetRefills());
            return View(param);
        }
        [HttpGet]
        public ActionResult RefillListView(string searchString)
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.Refill> param = new List<PPOk_Notifications.Models.Refill>();
            List<PPOk_Notifications.Models.Refill> filtered = new List<PPOk_Notifications.Models.Refill>();
            // FIXME: key not found exception in SQL services    ((List<PPOk_Notifications.Models.Refill>)param).AddRange(serv.GetRefills());
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
            return View(filtered);
        }
        // TODO additional actions and such need to be supported for this view.
        [HttpPost]
        public ActionResult PatientListView()
        {
            IEnumerable<PPOk_Notifications.Models.Patient> param = new List<PPOk_Notifications.Models.Patient>();
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            ((List<PPOk_Notifications.Models.Patient>)param).AddRange(serv.GetPatients());
            return View(new Tuple<IEnumerable<PPOk_Notifications.Models.Patient>, PPOk_Notifications.Service.SQLService> (param,serv));
        }
        [HttpGet]
        public ActionResult PatientListView(string searchString)
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.Patient> param = new List<PPOk_Notifications.Models.Patient>();
            List<PPOk_Notifications.Models.Patient> filtered = new List<PPOk_Notifications.Models.Patient>();
            // FIXME: key not found exception in SQL services    ((List<PPOk_Notifications.Models.Refill>)param).AddRange(serv.GetRefills());
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
            return View(filtered);
        }
        // TODO additional actions and such need to be supported for this view.
        [HttpPost]
        public ActionResult NotificationListView()
        {
            IEnumerable<PPOk_Notifications.Models.Notification> param = new List<PPOk_Notifications.Models.Notification>();
            // test data
            // NOTE BENE! Test will cause crash bc patient with id 3 does not exist (need to add w/sql services properly first)
            /*
            Prescription perscript = new Prescription();
            perscript.PatientId = 3;
            perscript.PrecriptionId = 2;
            perscript.PrescriptionDaysSupply = 5;
            perscript.PrescriptionDateFilled = DateTime.UtcNow;
            perscript.PrescriptionName = "mururezol";
            perscript.PrescriptionNumber = 444;
            perscript.PrescriptionRefills = 5;
            perscript.PrescriptionUpc = "OAEUAOEUR";
            Notification notif = new Notification(new Refill(perscript), Notification.NotificationType.Refill);
            notif.Sent = true;
            ((List<PPOk_Notifications.Models.Notification>)param).Add(notif);
            */
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
           ((List<PPOk_Notifications.Models.Notification>)param).AddRange(serv.GetNotifications());
            // return view
            return View(new Tuple<IEnumerable<PPOk_Notifications.Models.Notification>,
                PPOk_Notifications.Service.SQLService>
                (param, serv));
        }
        [HttpGet]
        public ActionResult NotificationListView(string searchString)
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.Notification> param = new List<PPOk_Notifications.Models.Notification>();
            List<PPOk_Notifications.Models.Notification> filtered = new List<PPOk_Notifications.Models.Notification>();
            // FIXME: key not found exception in SQL services    ((List<PPOk_Notifications.Models.Refill>)param).AddRange(serv.GetRefills());
            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var item in param)
                {
                    if (item.NotificationId.ToString().Contains(searchString) ||
                        item.Type.ToString().Contains(searchString) ||
                        item.PatientId.ToString().Contains(searchString) ||
                        serv.GetPatientById((int)item.PatientId).FirstName.Contains(searchString) ||
                        serv.GetPatientById((int)item.PatientId).LastName.Contains(searchString) )
                    {
                        filtered.Add(item);
                    }
                }
            }
            return View(filtered);
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