using System;
using System.Web.Mvc;
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

						return RefillSuccess();
					} else {
						return RefillFailure();
					}
				} else {
					return ExpiredOtp();
				}

			} catch (Exception) {
			    return BadLink();
		    }
			
	    }

		public ActionResult Unsubscribe() {
			try {
				var otp = DatabaseEmailOtpService.GetByCode(RouteData.Values["otp"].ToString());
				var notification = DatabaseNotificationService.GetById(otp.NotificationId);
				var patient = DatabasePatientService.GetById(notification.PatientId);
				
				if (otp.IsActive()) {
					if (patient.object_active) {
						patient.ContactMethod = Patient.PrimaryContactMethod.OptOut;
						DatabasePatientService.Update(patient);

						notification.NotificationResponse = "Unsubscribe";
						DatabaseNotificationService.Update(notification);

						DatabaseEmailOtpService.Disable(otp.Id);

						return UnsubscribeSuccess();
					} else {
						return UnsubscribeFailure();
					}
				} else {
					return ExpiredOtp();
				}
			} catch (Exception) {
				return BadLink();
			}
		}

		public ActionResult Reset() {
			try {
				var userOtp = DatabaseOtpService.GetByCode(RouteData.Values["otp"].ToString());
				var user = DatabaseUserService.GetById(userOtp.UserId);

				if (userOtp.IsActive()) {
					if (user.Enabled) {
						return View("../Login/Reset", new LoginController.ResetData { Email = user.Email, OTP = userOtp.Code });
					} else {
						return ResetFailure();
					}
				} else {
					return ExpiredOtp();
				}
			} catch (Exception) {
				return BadLink();
			}
		}

		//View Returns
	    public ActionResult UnsubscribeFailure () {
		    return View("UnsubscribeFailure");
	    }

	    public ActionResult UnsubscribeSuccess () {
		    return View("UnsubscribeSuccess");
	    }

		public ActionResult RefillFailure() {
			return View("RefillFailure");
		}

		public ActionResult RefillSuccess () {
		    return View("RefillSuccess");
	    }

		public ActionResult ResetSuccess() {
			return View("ResetSuccess");
		}

		public ActionResult ResetFailure() {
			return View("ResetFailure");
		}

		public ActionResult BadLink() {
			return View("BadLink");
		}

		public ActionResult ExpiredOtp() {
			return View("ExpiredOtp");
		}

		public ActionResult UnknownOtp() {
			return View("UnknownOtp");
		}
	}
}