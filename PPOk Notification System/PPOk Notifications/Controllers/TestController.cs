using System.Web.Mvc;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers
{
    [AllowAnonymous]
    public class TestController : BaseController
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

            return "sucess \n username: test@test.com \n password: harambe";
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
    }
}