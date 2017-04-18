using System;
using System.Web.Mvc;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers {

	/**
	 * This controller receives callbacks from email links to perform an 
	 * action or redirect to another portion of the application. It also
	 * validates and expires OTP codes as well as returns all email based
	 * views.
	 */
    public class EmailController : Controller {

		/**
		 * Sets a notification as responded to. Since the only response requried
		 * by an email, other than ignoring it, is YES it simply starts a refill
		 * 
		 * @receives - refill response link from sent emails
		 */
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

		/**
		 * Modifies a patients contact preferences to request not to be
		 * contacted again.
		 * 
		 * @receives - unsubscribe request from the bottom of notification based emails
		 */
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

		/**
		 * Receives a password reset token sent from email in order to redirect to
		 * the proper password reset page.
		 * 
		 * @receives - request link from email with embedded one time password
		 */
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

		/**
		 * View pages for any handled errors and or success messages
		 * =========================================================
		 */
		/**
		 * Returned if a user is disabled
		 */
	    public ActionResult UnsubscribeFailure () {
		    return View("UnsubscribeFailure");
	    }

		/**
		 * Returned if a patients contact preferences have been set to opt out
		 */
	    public ActionResult UnsubscribeSuccess () {
		    return View("UnsubscribeSuccess");
	    }

		/**
		 * Returned if the patient attempting to submit a refill is disabled
		 */
		public ActionResult RefillFailure() {
			return View("RefillFailure");
		}

		/**
		 * Returned if a refill was successfully set
		 */
		public ActionResult RefillSuccess () {
		    return View("RefillSuccess");
	    }

		/**
		 * Returned after a password has been successfully reset
		 */
		public ActionResult ResetSuccess() {
			return View("ResetSuccess");
		}

		/**
		 * Returned if a user is disabled
		 */
		public ActionResult ResetFailure() {
			return View("ResetFailure");
		}

		/**
		 * Returned if the link contains bad information, or records could not be found
		 * based on the submitted request.
		 */
		public ActionResult BadLink() {
			return View("BadLink");
		}

		/**
		 * Returned if the OTP code is disabled, signaling that is has been expired.
		 */
		public ActionResult ExpiredOtp() {
			return View("ExpiredOtp");
		}

		/**
		 * Currently not used
		 * Intended for use if an otp record could not be found, deprecated for security reasons
		 */
		public ActionResult UnknownOtp() {
			return View("UnknownOtp");
		}
	}
}