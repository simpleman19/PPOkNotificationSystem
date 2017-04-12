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
		    try {
			    var otp = DatabaseEmailOtpService.GetByCode(RouteData.Values["otp"].ToString());
			    var notification = DatabaseNotificationService.GetById(otp.NotificationId);
			    var patient = DatabasePatientService.GetById(notification.PatientId);

				if (otp.IsActive()) {
					if (patient.object_active) {

						notification.NotificationResponse = "Refill";
						DatabaseNotificationService.Update(notification);

						var refill = DatabaseRefillService.GetByPrescriptionId(DatabasePrescriptionService.GetByPatientId(patient.PatientId).PrecriptionId);
						refill.RefillIt = true;
						DatabaseRefillService.Update(refill);

						DatabaseEmailOtpService.Disable(otp.Id);

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
			try {
				var otp = DatabaseEmailOtpService.GetByCode(RouteData.Values["otp"].ToString());
				var notification = DatabaseNotificationService.GetById(otp.NotificationId);
				var patient = DatabasePatientService.GetById(notification.PatientId);
				

				if (otp.object_active) {
					if (patient.object_active) {
						patient.ContactMethod = Patient.PrimaryContactMethod.OptOut;
						DatabasePatientService.Update(patient);

						notification.NotificationResponse = "Unsubscribe";
						DatabaseNotificationService.Update(notification);

						DatabaseEmailOtpService.Disable(otp.Id);

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