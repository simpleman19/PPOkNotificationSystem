using System.Web.Mvc;
using PPOk_Notifications.Service;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Controllers {
	public class TestController : Controller {
		// GET: Debug
		public ActionResult Index() {
			return View();
		}

        public ActionResult AddFakeLogin()
        {
            var db = new SQLService();

            var pharm = new Pharmacy();
            pharm.PharmacyName = "Test Pharmacy";
            pharm.PharmacyAddress = "An address";
            pharm.PharmacyPhone = "+19999999999";
            db.PharmacyInsert(pharm);

            var list = db.GetPharmacies();
            pharm = list[0];
            var pharmAdmin = new Pharmacist();
            pharmAdmin.FirstName = "Pharma";
            Pharmacist.HashPassword(pharmAdmin, "harambe");
            pharmAdmin.Email = "admin@admin.com";
            pharmAdmin.PharmacyId = pharm.PharmacyId;
            pharmAdmin.IsAdmin = true;
            db.PharmacistInsertOrUpdate(pharmAdmin);

            return Redirect("/");
        }

		public string Reset() {
			SQLService sql = new SQLService();
			string result = sql.Rebuild();
			return result;
		}

		public string SqlScripts() {
			string debug = "";
			foreach (var key in ScriptService.Scripts.Keys) {
				debug += key + ": <br/>" + ScriptService.Scripts[key] + "<br/><br/>";
			}
			if (ScriptService.Scripts.Count == 0) {
				debug = "No Scripts Found!";
			}
			return debug;
		}
	}
}