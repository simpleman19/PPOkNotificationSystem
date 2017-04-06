using System;
using System.Web.Mvc;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers
{
    public class TestController : Controller
    {
        // GET: Debug
        public ActionResult Index()
        {
            return View();
        }

        public string AddFakeLogin(long pid)
        {
            var db = new SQLService();
            var pharmAdmin = new Pharmacist
            {
                FirstName = "Pharma",
                LastName = "cist",
                Phone = "+19999999993",
                Email = "test@test.com",
                PharmacyId = pid,
                UserId = 1,
                IsAdmin = true,
                Type = Models.User.UserType.Pharmacist
            };
            pharmAdmin.UserId = db.UserInsert(pharmAdmin);
            var login = new Login
            {
                LoginId = 1,
                UserId = pharmAdmin.UserId,
                LoginToken = ""
            };
            login.SetPassword("harambe");
            db.LoginInsert(login);

            db.PharmacistInsert(pharmAdmin);

            User ppokAdmin = new User();
            ppokAdmin.LastName = "dmin";
            ppokAdmin.FirstName = "PPOk A";
            ppokAdmin.Type = Models.User.UserType.PPOkAdmin;
            ppokAdmin.Phone = "+19999999998";
            ppokAdmin.Email = "admin@test.com";
            ppokAdmin.UserId = db.UserInsert(ppokAdmin);

            var login2 = new Login
            {
                UserId = ppokAdmin.UserId,
                LoginToken = ""
            };
            login2.SetPassword("harambe");

            db.LoginInsert(login2);

            return "sucess <br/> Pharm: username: test@test.com password: harambe <br/> Admin: username: admin@test.com password: harambe";
        }

        public string AddFakePresRefillNotif(long pid)
        {
            var db = new SQLService();
            var pres = new Prescription();
            pres.PatientId = pid;
            pres.PrescriptionName = "Test Prescription";
            pres.PrescriptionNumber = 12345;
            pres.PrescriptionRefills = 3;
            pres.PrescriptionDateFilled = System.DateTime.Now.AddDays(-27);
            pres.PrescriptionDaysSupply = 30;
            pres.PrescriptionUpc = "123456789";
            pres.PrecriptionId = db.PrescriptionInsert(pres);
            var refill = new Refill(pres);
            refill.RefillId = db.RefillInsert(refill);
            return "Sucesss";
        }
        public string AddFakePatient(long pid)
        {
            var db = new SQLService();

            var pat = new Patient();
            pat.ContactMethod = Patient.PrimaryContactMethod.Text;
            pat.FirstName = "John";
            pat.LastName = "Doe";
            pat.PersonCode = "1";
            pat.DateOfBirth = System.DateTime.Now;
            pat.Phone = "+18065703539";
            pat.PharmacyId = pid;
            pat.PreferedContactTime = System.DateTime.Now;
            long id = db.UserInsert(pat);
            pat.UserId = id;
            var patId = db.PatientInsert(pat);
            this.AddFakePresRefillNotif(patId);
            return "success";
        }
        public string Reset()
        {
            SQLService sql = new SQLService();
            string result = sql.Rebuild();
            return result;
        }

        public string SqlScripts()
        {
            string debug = "";
            foreach (var key in ScriptService.Scripts.Keys)
            {
                debug += key + ": <br/>" + ScriptService.Scripts[key] + "<br/><br/>";
            }
            if (ScriptService.Scripts.Count == 0)
            {
                debug = "No Scripts Found!";
            }
            return debug;
        }

        public string InsertFake()
        {
            string output = "";
            var pharmID = Pharmacy.FakeDataFill();
            output += "\n" + this.AddFakeLogin(pharmID);
            output += "\n" + this.AddFakePatient(pharmID);
            return output;
        }

        public string ResetAndInsert()
        {
            string output = "";
            output += "\n" + this.Reset();
            output += "\n" + this.InsertFake();
            return output;
        }

		public string GetRandomOTP() {
			return OTPService.RandomString(64);
		}

	    public string SendTestEmail() {

			var db = new SQLService();

			var u = new User();
			var p = new Patient();
			var n = new Notification();
			var pr = new Prescription();
			var r = new Refill();

		    u.Email = "test@test.com"; // PUT YOUR EMAIL HERE TO TEST
		    u.FirstName = "Test";
		    u.LastName = "User";
		    u.Phone = "+14055555555";
		    u.UserId = db.UserInsert(u);

		    p.UserId = u.UserId;
		    p.PharmacyId = 1;
			p.DateOfBirth = DateTime.Now;
			p.PreferedContactTime = DateTime.Now;
			p.ContactMethod = Patient.PrimaryContactMethod.Email;
		    p.PersonCode = "0";
		    p.SendBirthdayMessage = true;
		    p.SendRefillMessage = true;
			p.PatientId = db.PatientInsert(p);

		    pr.PatientId = p.PatientId;
		    pr.PrescriptionDaysSupply = 30;
		    pr.PrescriptionRefills = 3;
		    pr.PrescriptionName = "Tylenol";
		    pr.PrescriptionNumber = 1;
		    pr.PrescriptionUpc = "ABC123";
			pr.PrescriptionDateFilled = DateTime.Now;
		    pr.PrecriptionId = db.PrescriptionInsert(pr);

		    r.RefillIt = false;
		    r.PrescriptionId = pr.PrecriptionId;
		    r.Refilled = false;
			r.RefillDate = DateTime.Now;
		    r.RefillId = db.RefillInsert(r);

			n.PatientId = p.PatientId;
			n.Type = Notification.NotificationType.Refill;
			n.NotificationMessage = "This is a test email for a refill";
			n.ScheduledTime = DateTime.Now;
		    n.SentTime = null;
		    n.Sent = false;
		    n.NotificationId = db.NotificationInsert(n);


		    EmailService.SendNotification(n);
		    EmailService.SendReset(u);

		    return ("Sent an notification and reset email to test account");
	    }
	}
}