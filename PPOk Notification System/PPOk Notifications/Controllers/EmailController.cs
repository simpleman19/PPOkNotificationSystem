using System;
using System.Linq.Expressions;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers {
    public class EmailController : Controller {

	    public JsonResult Respond() {
			SQLService db = new SQLService();
			return Json(RouteData.Values["otp"], JsonRequestBehavior.AllowGet);
	    }

		public ActionResult Unsubscribe() {
			SQLService db = new SQLService();
			EmailOTP otp = null;
			Patient patient = null;
			Notification notification = null;
			try {
				otp = db.GetEmailOTPByCode(RouteData.Values["otp"].ToString());
				patient = db.GetPatientById(Convert.ToInt64(RouteData.Values["patientid"]));
				notification = db.GetNotificationById(otp.NotificationId);

				if (otp.object_active) {
					if (patient.object_active && notification.PatientId == patient.PatientId) {
						patient.ContactMethod = Patient.PrimaryContactMethod.OptOut;
						db.PatientUpdate(patient);

						notification.NotificationResponse = "Unsubscribe";
						db.NotificationUpdate(notification);

						db.EmailOTP_Disable(otp.Id);

						return Redirect("/Email/UnsubscribeSuccess");
					} else {
						return Redirect("/Email/UnsubscribeFailure");
					}
				} else {
					return Redirect("/Email/ExpiredOtp");
				}
			} catch (Exception) {
				return Redirect("/Email/BadLink");
			}
		}

		public ActionResult Reset() { 
			SQLService db = new SQLService();
			
			return Redirect("/Email/UnsubscribeSuccess");
		}

		//View Returns
	    public ActionResult UnsubscribeFailure () {
		    return View();
	    }

	    public ActionResult UnsubscribeSuccess () {
		    return View();
	    }

		public ActionResult RefillFailure() {
			return View();
		}

		public ActionResult RefillSuccess () {
		    return View();
	    }

		public ActionResult ResetSuccess() {
			return View();
		}

		public ActionResult ResetFailure() {
			return View();
		}

		public ActionResult BadLink() {
			return View();
		}

		public ActionResult ExpiredOtp() {
			return View();
		}

		public ActionResult UnknownOtp() {
			return View();
		}
	}
}