using System;
using System.Linq;
using System.Web.Mvc;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers
{
	/**
	 * Debugging purposes only, this inserts random data, resets the entire system
	 * and can cause serious damage to a system in use.
	 */
    public class TestController : Controller {

        // GET: Debug
        public ActionResult Index() {
            return View();
        }

        public string MakePhoneCall()
        {
            TwilioApi twilio = new TwilioApi(DatabasePharmacyService.GetById(1));
            twilio.MakePhoneCall(new Notification());
            return "Calling";
        }

        public string AddFakeLogin(long pid) {
            var pharmAdmin = new Pharmacist {
                FirstName = "Pharma",
                LastName = "cist",
                Phone = "+19999999993",
                Email = "pharm@test.com",
                PharmacyId = pid,
                UserId = 1,
                IsAdmin = true,
                Type = Models.User.UserType.Pharmacist
            };
            pharmAdmin.UserId = DatabaseUserService.Insert(pharmAdmin);
            var login = new Login {
                LoginId = 1,
                UserId = pharmAdmin.UserId,
                LoginToken = ""
            };
            login.SetPassword("harambe");
            DatabaseLoginService.Insert(login);

            DatabasePharmacistService.Insert(pharmAdmin);

	        var ppokAdmin = new User {
		        LastName = "dmin",
		        FirstName = "PPOk A",
		        Type = Models.User.UserType.PPOkAdmin,
		        Phone = "+19999999998",
		        Email = "admin@test.com"
	        };
	        ppokAdmin.UserId = DatabaseUserService.Insert(ppokAdmin);

            var login2 = new Login {
                UserId = ppokAdmin.UserId,
                LoginToken = ""
            };
            login2.SetPassword("harambe");

            DatabaseLoginService.Insert(login2);

            return "sucess <br/> Pharm: username: test@test.com password: harambe <br/> Admin: username: admin@test.com password: harambe";
        }

        public string AddFakePresRefillNotif(long pid) {
	        var pres = new Prescription {
		        PatientId = pid,
		        PrescriptionName = "Test Prescription",
		        PrescriptionNumber = 12345,
		        PrescriptionRefills = 3,
		        PrescriptionDateFilled = System.DateTime.Now.AddDays(-27),
		        PrescriptionDaysSupply = 30,
		        PrescriptionUpc = "123456789"
	        };
	        pres.PrecriptionId = DatabasePrescriptionService.Insert(pres);
            var refill = new Refill(pres);
            refill.RefillIt = false;
            refill.RefillId = DatabaseRefillService.Insert(refill);
            return "Sucesss";
        }

        public string AddFakePatient(long pid) {
            var pat = new Patient {
                ContactMethod = Patient.PrimaryContactMethod.Text,
                FirstName = "John",
                LastName = "Doe",
                PersonCode = "1",
                DateOfBirth = System.DateTime.Now,
                Phone = "+18065703539",
                PharmacyId = pid,
                PreferedContactTime = System.DateTime.Now,
                SendRefillMessage = true,
                SendBirthdayMessage = true

	        };
	        var id = DatabaseUserService.Insert(pat);
            pat.UserId = id;
            var patId = DatabasePatientService.Insert(pat);
            this.AddFakePresRefillNotif(patId);
            return "success";
        }

		//Resets the entire databse
        public string Reset() {
            var result = DatabaseService.Rebuild();
	        return result ? "Success" : "Failure";
        }

		//Prints out a text list of all the SQL scripts loaded by the system
        public string SqlScripts() {
            var debug = ScriptService.Scripts.Keys.Aggregate("", (current, key) => current + (key + ": <br/>" + ScriptService.Scripts[key] + "<br/><br/>"));
	        if (ScriptService.Scripts.Count == 0) {
                debug = "No Scripts Found!";
            }
            return debug;
        }

		//Inserts default information for testing
        public string InsertFake() {
            var output = "";
            var pharmID = Pharmacy.FakeDataFill();
            output += "\n" + this.AddFakeLogin(pharmID);
            output += "\n" + this.AddFakePatient(pharmID);
            return output;
        }

		//Resets the database and inserts default data
        public string ResetAndInsert() {
            var output = "";
            output += "\n" + this.Reset();
            output += "\n" + this.InsertFake();
            return output;
        }

		//Prints a random OTP string without creating the object
		public string GetRandomOTP() {
			return OTPService.RandomString(64);
		}

		//Sends test emails with working callbacks to the email specified
	    public string SendTestEmail() {

			var u = new User();
			var p = new Patient();
			var n = new Notification();
			var pr = new Prescription();
			var r = new Refill();

		    u.Email = "test@test.com"; // PUT YOUR EMAIL HERE TO TEST
		    u.FirstName = "Test";
		    u.LastName = "User";
		    u.Phone = "+14055555555";
		    u.UserId = DatabaseUserService.Insert(u);

		    p.UserId = u.UserId;
		    p.PharmacyId = 1;
			p.DateOfBirth = DateTime.Now;
			p.PreferedContactTime = DateTime.Now;
			p.ContactMethod = Patient.PrimaryContactMethod.Email;
		    p.PersonCode = "0";
		    p.SendBirthdayMessage = true;
		    p.SendRefillMessage = true;
			p.PatientId = DatabasePatientService.Insert(p);

		    pr.PatientId = p.PatientId;
		    pr.PrescriptionDaysSupply = 30;
		    pr.PrescriptionRefills = 3;
		    pr.PrescriptionName = "Tylenol";
		    pr.PrescriptionNumber = 1;
		    pr.PrescriptionUpc = "ABC123";
			pr.PrescriptionDateFilled = DateTime.Now;
		    pr.PrecriptionId = DatabasePrescriptionService.Insert(pr);

		    r.RefillIt = false;
		    r.PrescriptionId = pr.PrecriptionId;
		    r.Refilled = false;
			r.RefillDate = DateTime.Now;
		    r.RefillId = DatabaseRefillService.Insert(r);

			n.PatientId = p.PatientId;
			n.Type = Notification.NotificationType.Refill;
			n.NotificationMessage = "This is a test email for a refill";
			n.ScheduledTime = DateTime.Now;
		    n.SentTime = null;
		    n.Sent = false;
		    n.NotificationId = DatabaseNotificationService.Insert(n);


		    EmailService.SendNotification(n);
		    EmailService.SendReset(u);

		    return ("Sent an notification and reset email to test account");
	    }
	}
}