using System.Text.RegularExpressions;
using System.Web.Mvc;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers
{
    public class PatientController : Controller
    {
        public ActionResult Index()
        {
            return View(DatabasePatientService.GetByPersonCode("1", 1));
        }

        public ActionResult Login()
        {
            return View();
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
                return RedirectToAction("Code"); // just act like there's no problem
            }

            var patient = DatabasePatientService.GetByUserIdActive(user.UserId);
            if (patient == null)
            {
                return RedirectToAction("Code"); // just act like there's no problem
            }

            // TODO send code, write it to DB, redirect to Code page

            return RedirectToAction("Code");
        }

        public ActionResult Code()
        {
            return View();
        }
    }
}