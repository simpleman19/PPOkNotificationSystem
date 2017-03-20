using LumenWorks.Framework.IO.Csv;
using PPOk_Notifications.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
                            prescription.prescriptionName = row["GPIGenericName"].ToString();
                            prescription.prescriptionDateFilled = DateTime.Parse(row["DateFilled"].ToString());
                            prescription.prescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                            prescription.prescriptionRefills = int.Parse(row["NumerOfRefills"].ToString());
                            prescription.prescriptionUPC = row["NDCUPCHRI"].ToString();
                            prescription.prescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());
                            //SqlService.AddPrescription(prescription);
                            //If patient doesnt exist in the database, create a new patient and save it in the db along with their prescriptions.
                            Patient patient = new Patient();
                            Prescription prescription1 = new Prescription();
                            patient.personCode = int.Parse(row["PersonCode"].ToString());
                            patient.firstName = row["PatientFirstName"].ToString();
                            patient.lastName = row["PatientLastName"].ToString();
                            patient.phone = row["Phone"].ToString();
                            patient.email = row["Email"].ToString();
                            prescription.prescriptionName = row["GPIGenericName"].ToString();
                            prescription.prescriptionDateFilled = DateTime.Parse(row["DateFilled"].ToString());
                            prescription.prescriptionDaysSupply = int.Parse(row["DaysSupply"].ToString());
                            prescription.prescriptionRefills = int.Parse(row["NumerOfRefills"].ToString());
                            prescription.prescriptionUPC = row["NDCUPCHRI"].ToString();
                            prescription.prescriptionNumber = int.Parse(row["PrescriptionNumber"].ToString());
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