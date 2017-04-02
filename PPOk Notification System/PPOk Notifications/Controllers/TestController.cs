
using System.Web.Mvc;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Controllers
{
    public class TestController : Controller
    {
        // GET: Debug
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddFakeLogin()
        {
            var db = new SQLService();

            var pharmacies = db.GetPharmacies();
            var pharmAdmin = new Pharmacist
            {
                FirstName = "Pharma",
                LastName = "cist",
                Phone = "+19999999993",
                Email = "test@test.com",
                PharmacyId = pharmacies[0].PharmacyId,
                UserId = 1
            };
            db.UserInsert(pharmAdmin);

            var login = new Login
            {
                LoginId = 1,
                UserId = pharmAdmin.UserId,
                LoginToken = ""
            };
            login.SetPassword("harambe");
            db.LoginInsert(login);

            db.PharmacistInsert(pharmAdmin);

            return Redirect("/");
        }

        public string AddFakePatient()
        {
            var db = new SQLService();

            var pat = new Patient();
            pat.ContactMethod = Patient.PrimaryContactMethod.Text;
            pat.FirstName = "John";
            pat.LastName = "Doe";
            pat.PersonCode = "1";
            pat.DateOfBirth = System.DateTime.Now;
            pat.Phone = "+19999999999";
            pat.PharmacyId = 1;
            pat.PreferedContactTime = System.DateTime.Now;
            long id = db.UserInsert(pat);
            pat.UserId = id;
            db.PatientInsert(pat);

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
            Pharmacy.FakeDataFill();
            return "success";
        }
    }
}