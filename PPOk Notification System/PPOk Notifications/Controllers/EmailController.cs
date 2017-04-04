using System.Net;
using System.Web.Mvc;
using System.Web.WebPages;

namespace PPOk_Notifications.Controllers {
    public class EmailController : Controller {
	    public ActionResult SetResponse(string EmailOtp, string response) {
			Response.SetStatus(HttpStatusCode.OK);
		    return Redirect("/Email/RefillSuccess");
	    }
	}
}