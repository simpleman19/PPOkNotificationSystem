using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using PPOk_Notifications.Filters;
using PPOk_Notifications.Models;
using PPOk_Notifications.NotificationSending;
using PPOk_Notifications.Service;
using Group = PPOk_Notifications.Filters.Group;

namespace PPOk_Notifications.Controllers
{
    public class PatientController : Controller
    {
        [Authenticate(Group.Patient)]
        public ActionResult Index()
        {
            var userId = (long) Session[Models.Login.UserIdSession];

            var patient = DatabasePatientService.GetByUserId(userId);
            if (patient == null)
            {
                return RedirectToAction("Index", "Login");
            }
            patient.LoadUserData();

            return View("Index", Tuple.Create(patient, true));
        }

        [HttpPost]
        [Authenticate(Group.Patient)]
        public ActionResult Index(string contactMethod, string notificationTime, string birthdayEnabled, string refillsEnabled)
        {
            var userId = (long)Session[Models.Login.UserIdSession];
            var patient = DatabasePatientService.GetByUserId(userId);
            if (patient == null)
            {
                return RedirectToAction("Index", "Login");
            }
            patient.LoadUserData();

            SavePatient(patient, contactMethod, notificationTime, birthdayEnabled, refillsEnabled);
            return View("Index", Tuple.Create(patient, true));
        }

        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult PharmIndex(string patientId)
        {
            var patient = GetPatient(patientId);
            if (patient == null)
            {
                return RedirectToAction("Index", "Login");
            }
            patient.LoadUserData();
            return View("Index", Tuple.Create(patient, false));
        }

        [HttpPost]
        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
        public ActionResult PharmIndex(string patientId, string contactMethod, string notificationTime, string birthdayEnabled, string refillsEnabled)
        {
            var patient = GetPatient(patientId);
            if (patient == null)
            {
                return RedirectToAction("Index", "Login");
            }
            patient.LoadUserData();

            SavePatient(patient, contactMethod, notificationTime, birthdayEnabled, refillsEnabled);
            return View("Index", Tuple.Create(patient, false));
        }

        private Patient GetPatient(string patientId)
        {
            long patientIdLong;
            if (!long.TryParse(patientId, out patientIdLong))
            {
                return null;
            }

            var patient = DatabasePatientService.GetById(patientIdLong);
            if (patient == null)
            {
                return null;
            }
            patient.LoadUserData();
            return patient;
        }

        private void SavePatient(Patient patient, string contactMethod, string notificationTime, string birthdayEnabled, string refillsEnabled)
        {
            switch (contactMethod)
            {
                case "text":
                    patient.ContactMethod = Patient.PrimaryContactMethod.Text;
                    break;
                case "call":
                    patient.ContactMethod = Patient.PrimaryContactMethod.Call;
                    break;
                case "email":
                    patient.ContactMethod = Patient.PrimaryContactMethod.Email;
                    break;
                case "optout":
                    patient.ContactMethod = Patient.PrimaryContactMethod.OptOut;
                    break;
            }

            if (notificationTime.Length == 6)
            {
                notificationTime = "0" + notificationTime;
            }
            patient.PreferedContactTime = DateTime.ParseExact(notificationTime, "hh:mmtt", CultureInfo.InvariantCulture);

            patient.SendBirthdayMessage = birthdayEnabled == "on";
            patient.SendRefillMessage = refillsEnabled == "on";
           
            DatabasePatientService.Update(patient);
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(string phonenumber)
        {
            // just a bit of input cleanup
            phonenumber = new Regex("[\\(\\)\\s+\\-]").Replace(phonenumber, "");
            if (!phonenumber.StartsWith("+"))
            {
                if (phonenumber.Length == 10)
                {
                    phonenumber = "+1" + phonenumber;
                }
                else
                {
                    phonenumber = "+" + phonenumber;
                }
            }
            else
            {
                if (phonenumber.Length == 11)
                {
                    phonenumber = "+1" + phonenumber.Substring(1);
                }
            }

            // TODO Tyler - skip this step and get patient directly from phone number?
            var user = DatabaseUserService.GetByPhoneActive(phonenumber);
            if (user == null)
            {
                return Code(null);
            }

            var patient = DatabasePatientService.GetByUserIdActive(user.UserId);
            if (patient == null)
            {
                return Code(null);
            }

            var otp = new OTP()
            {
                UserId = patient.UserId,
                Time = DateTime.Now,
                Code = new Random().Next(0, 1000000).ToString("000000")
            };
            DatabaseOtpService.Insert(otp);
            NotificationSender.SendNotification(patient, "Your one-time patient login code is " + otp.Code);

            return Code(patient.UserId);
        }

        public ActionResult Code(long? userId)
        {
            return View("Code", userId);
        }

        [HttpPost]
        public ActionResult Code(string userId, string loginCode)
        {
            if (userId == null || loginCode == null)
            {
                return RedirectToAction("Index", "Login");
            }

            long userIdLong;
            if (!long.TryParse(userId, out userIdLong))
            {
                return RedirectToAction("Index", "Login");
            }

            var otp = DatabaseOtpService.GetByCode(loginCode);
            if (otp.Time.AddMinutes(10) < DateTime.Now || otp.UserId != userIdLong)
            {
                return RedirectToAction("Index", "Login");
            }
            DatabaseOtpService.Disable(otp.Id);

            Session[Models.Login.UserIdSession] = otp.UserId;
            return RedirectToAction("Index", "Patient");
        }
    }
}