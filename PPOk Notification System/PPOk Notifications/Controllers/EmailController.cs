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

	    public ActionResult Respond() {
			var db = new SQLService();
		    try {
			    var otp = db.GetEmailOTPByCode(RouteData.Values["otp"].ToString());
			    var notification = db.GetNotificationById(otp.NotificationId);
			    var patient = db.GetPatientById(notification.PatientId);

				if (otp.IsActive()) {
					if (patient.object_active) {

						notification.NotificationResponse = "Refill";
						db.NotificationUpdate(notification);

						Refill refill = db.GetRefillByPrescriptionId(db.GetPrescriptionByPatientId(patient.PatientId).PrecriptionId);
						refill.RefillIt = true;
						db.RefillUpdate(refill);

						db.EmailOTP_Disable(otp.Id);

						return Redirect("/Email/RefillSuccess");
					} else {
						return Redirect("/Email/RefillFailure");
					}
				} else {
					return Redirect("/Email/ExpiredOtp");
				}

			} catch (Exception) {
			    return Redirect("/Email/BadLink");
		    }
			
	    }

		public ActionResult Unsubscribe() {
			var db = new SQLService();

			try {
				var otp = db.GetEmailOTPByCode(RouteData.Values["otp"].ToString());
				var notification = db.GetNotificationById(otp.NotificationId);
				var patient = db.GetPatientById(notification.PatientId);
				

				if (otp.object_active) {
					if (patient.object_active) {
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
			var db = new SQLService();
			
			return Redirect("/Email/ResetSuccess");
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