using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class CsvService {

		public static bool ParseCsv(HttpPostedFileBase upload, long pharmacyId) {

			//Check if valid file was uploaded
			if (upload != null && upload.ContentLength > 0 && upload.FileName.EndsWith(".csv")) {

				//Convert binary file received from request into text
				var textdata = string.Empty;
				using (BinaryReader b = new BinaryReader(upload.InputStream)) {
					var binData = b.ReadBytes(upload.ContentLength);
					textdata = Encoding.UTF8.GetString(binData);
				}

				//Get list of patients from database for comparison
				var patients = new Dictionary<string, Patient>();
				var patientlist = DatabasePatientService.GetAll(pharmacyId);
				foreach (var p in patientlist) {
					p.LoadUserData();
					patients.Add(p.PersonCode, p);
				}

				var prescriptionlist = DatabasePrescriptionService.GetAll();
				var prescriptions = prescriptionlist.ToDictionary(p => p.PrecriptionId);

				//Interate over each line of text in the file
				var text = new StringReader(textdata);
				var line = string.Empty;

				//Remove headers from file
				text.ReadLine(); 

				while ((line = text.ReadLine()) != null) {
					var row = line.Split(',');

					//Check if patient exists
					try {
						var dateNow = DateTime.Now;
						
						Patient patient = new Patient() {
							PersonCode = row[0],
							FirstName = row[1],
							LastName = row[2],
							DateOfBirth = DateTime.ParseExact(row[3], "yyyyMMdd", null),
							Phone = row[5],
							Email = row[6],
							PharmacyId = pharmacyId
							};
						if (patients.ContainsKey(row[0])) {
							//Update patient

							var oldPatient = patients[row[0]];

							patient.UserId = oldPatient.UserId;
							patient.PatientId = patients[row[0]].PatientId;
							patient.Type = oldPatient.Type;
							patient.UserLogin = oldPatient.UserLogin;
							patient.ContactMethod = oldPatient.ContactMethod;
							patient.PreferedContactTime = oldPatient.PreferedContactTime;
							patient.SendBirthdayMessage = oldPatient.SendBirthdayMessage;
							patient.SendRefillMessage = oldPatient.SendRefillMessage;
							DatabaseUserService.Update(patient);
							DatabasePatientService.Update(patient);
							DatabaseUserService.Enable(patient.PatientId);
							DatabasePatientService.Enable(patient.PatientId);
						} else {
							//Create patient
							patient.PreferedContactTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 15, 0, 0);
							patient.UserId = DatabaseUserService.Insert(patient);
							patient.PatientId = DatabasePatientService.Insert(patient);
						}

						//Check if prescription exists
						try {
							var prescriptionId = Convert.ToInt32(row[8]);
							var prescription = new Prescription() {
								PrescriptionDateFilled = DateTime.ParseExact(row[7], "yyyyMMdd", null),
								PrescriptionNumber = prescriptionId,
								PrescriptionDaysSupply = Convert.ToInt32(row[9]),
								PrescriptionRefills = Convert.ToInt32(row[10]),
								PrescriptionUpc = row[11],
								PrescriptionName = row[12],
								PatientId = patient.PatientId
							};
							if (prescriptions.ContainsKey(prescriptionId)) {
								//Check and update previous prescription
								prescription.PrecriptionId = prescriptionId;
								DatabasePrescriptionService.Update(prescription);
							} else {
								//Add new prescription
								prescription.PrecriptionId = DatabasePrescriptionService.Insert(prescription);
							}

							var refill = new Refill(prescription) {
								RefillDate = prescription.PrescriptionDateFilled.AddDays(prescription.PrescriptionDaysSupply - 2)
							};
							DatabaseRefillService.Insert(refill);

						} catch (Exception e) {
							//Ignore prescriptions that fail the model building
						}

					} catch (Exception e) {
						//Do not add patients which fail the model building
					}
				}
				return true;
			}
			return false;
		}
	}
}