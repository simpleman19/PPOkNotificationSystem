using System.Web.Mvc;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers {
	public class TestController : Controller {
		// GET: Debug
		public ActionResult Index() {
			return View();
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

	    public string InsertFake()
	    {
            Pharmacy.FakeDataFill();
	        return "success";
	    }
	}
}