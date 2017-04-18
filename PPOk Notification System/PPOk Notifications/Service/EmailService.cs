using System;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class EmailService {

		/**
		 * Send a compiled MailMessage from the configured credentials
		 * 
		 * @param - MailMessage - compiled message to send
		 * @returns - bool - result of sending the email
		 */
		public static bool SendEmail(MailMessage message) {
			try {
				using (var smtp = new SmtpClient()) {
					var credential = new NetworkCredential {
						UserName = ConfigurationManager.AppSettings["SendEmailAddress"],
						Password = ConfigurationManager.AppSettings["SendEmailPassword"]
					};
					smtp.Credentials = credential;
					smtp.Host = ConfigurationManager.AppSettings["SmtpHost"];
					smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
					smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpEnableSsl"]);

					smtp.Send(message);
				}
			} catch (Exception) {
				return false;
			}
			return true;
		}

		/**
		 * Send a notification via email
		 * 
		 * @param - Notification - the notification to be compiled and sent
		 * @returns - bool - result of sending the email
		 */
		public static bool SendNotification(Notification notification) {
			return SendEmail(Build(notification));
		}

		/**
		 * Send password reset email
		 * 
		 * @param - User - a user for which to request a password reset
		 * @returns - bool - result of sending the email
		 */
		public static bool SendReset(User user) {
			var message = new MailMessage();
			message.To.Add(new MailAddress(user.Email));
			message.From = new MailAddress(ConfigurationManager.AppSettings["SendEmailAddress"]);
			var body = EmailHtmlLoader.ResetHtml;

			//Generate OTP code
			var otp = OTPService.GenerateOtp(user);

			//Replace sentinels in email with personalized data
			message.Subject = "PPOK notifcications: Password reset";
			body = body.Replace("{{Email}}", user.Email);
			body = body.Replace("{{FirstName}}", user.FirstName);

			//Set up links
			body = body.Replace("{{OtpCode}}", otp.Code);
			body = body.Replace("{{UserId}}", user.UserId.ToString());
			body = body.Replace("{{ResetLink}}", "http://localhost:50082/email/reset");

			message.Body = body;
			message.IsBodyHtml = true;

			return SendEmail(message);
		}

		/**
		 * Compiles a notification into an email. Fills template placeholders
		 * with actual data, sets proper email parameters, and renders the body
		 * of the email based on notification type.
		 * 
		 * @param - Notifiation - the notification to compile
		 * @returns - MailMessage - the compiled email with rendered body
		 */
		public static MailMessage Build(Notification notification) {

			//Get necessary data to build and format the email
			var patient = DatabasePatientService.GetById(notification.PatientId);
			var user = DatabaseUserService.GetById(patient.UserId);
			var pharmacy = DatabasePharmacyService.GetById(patient.PharmacyId);

			var message = new MailMessage();
			message.To.Add(new MailAddress(user.Email));
			message.From = new MailAddress(ConfigurationManager.AppSettings["SendEmailAddress"]);

			//Emails always get style sheet and header
			var body = EmailHtmlLoader.TemplateHtml;
			var content = "";
			var emailtitle = "";

			//Set email subject and body based on type of email
			switch (notification.Type) {
				case Notification.NotificationType.Birthday:
					message.Subject = "Happy Birthday, from " + pharmacy.PharmacyName + "!";
					content += EmailHtmlLoader.BirthdayHtml;
					emailtitle = "Happy Birthday!";
					break;
				case Notification.NotificationType.Ready:
					message.Subject = "Your Refill is ready to be picked up";
					content += EmailHtmlLoader.ReadyHtml;
					emailtitle = "You have a refill ready to be picked up";
					break;
				case Notification.NotificationType.Recall:
					message.Subject = "A Prescription you received has been recalled!";
					message.Priority = MailPriority.High;
					content += EmailHtmlLoader.RecallHtml;
					emailtitle = "There has been a recall on a prescription you received";
					break;
				case Notification.NotificationType.Refill:
					message.Subject = "Your medication is up for refill";
					content += EmailHtmlLoader.RefillHtml;
					emailtitle = "Would you like to refill your medication with us?";
					break;
				case Notification.NotificationType.Reset:
					break;
				default:
					message.Subject = "Unknown Notification Type";
					break;
			}

			//Set contact reason message
			var reason = "You are receiving this email because ";
			if (notification.Type == Notification.NotificationType.Recall) {
				reason += "this is a mandatory email from your pharmacy. " +
				          "If you have any questions please call" + pharmacy.PharmacyPhone+
						  " to speak with your pharmacist.";
			} else {
				reason += "of your personal contact preferences. If you wish to unsubscribe" +
				          " from all future emails, please click the button below or contact" +
				          " your pharmacist at "+pharmacy.PharmacyPhone+".";
			}
			body = body.Replace("{{ContactReason}}", reason);

			//Replace html template placeholder with renderbody
			body = body.Replace("{{EmailBody}}", content);
			body = body.Replace("{{EmailTitle}}", emailtitle);
			body = body.Replace("{{MessageText}}", notification.NotificationMessage);

			//Replace sentinels in email with personalized data
			body = body.Replace("{{PharmacyName}}", pharmacy.PharmacyName);
			body = body.Replace("{{PharmacyPhone}}", pharmacy.PharmacyPhone);
			body = body.Replace("{{PharmacyAddress}}", pharmacy.PharmacyAddress);
			body = body.Replace("{{Name}}", patient.GetFullName());
			body = body.Replace("{{FirstName}}", patient.FirstName);
			body = body.Replace("{{LastName}}", patient.LastName);
			body = body.Replace("{{Phone}}", patient.Phone);
			body = body.Replace("{{Email}}", patient.Email);
			body = body.Replace("{{DOBShort}}", patient.DateOfBirth.ToShortDateString());
			body = body.Replace("{{DOBLong}}", patient.DateOfBirth.ToLongDateString());
			body = body.Replace("{{ContactTimeShort}}", patient.PreferedContactTime.ToShortTimeString());
			body = body.Replace("{{ContactTimeLong}}", patient.PreferedContactTime.ToLongTimeString());

			//Set up links
			body = body.Replace("{{OtpCode}}", OTPService.GenerateEmailOtp(notification).Code);
			body = body.Replace("{{PatientId}}", patient.PatientId.ToString());
			body = body.Replace("{{RespondLink}}", "http://localhost:50082/email/respond");
			body = body.Replace("{{UnsubscribeLink}}", "http://localhost:50082/email/unsubscribe");

			message.Body = body;
			message.IsBodyHtml = true;

			return message;
		}
	}

	/**
	 * This class specifies the various HTML template files and loads them
	 * at startup in order to serve them to the email compiling service
	 */
	public class EmailHtmlLoader {

		public static string TemplateHtml { get; private set; }

		public static string BirthdayHtml { get; private set; }
		public static string ReadyHtml { get; private set; }
		public static string RefillHtml { get; private set; }
		public static string RecallHtml { get; private set; }
		public static string ResetHtml { get; private set; }
		

		/**
		 * Loads emails from the emailHTML directory. This emthod is run
		 * at startup in order to load dynamic templates.
		 */
		public static void Init() {
			TemplateHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+@"\Service\EmailHTML\template.html");
			BirthdayHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+@"\Service\EmailHTML\birthday.html");
			ReadyHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+@"\Service\EmailHTML\ready.html");
			RefillHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+@"\Service\EmailHTML\refill.html");
			RecallHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+@"\Service\EmailHTML\recall.html");
			ResetHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Service\EmailHTML\reset.html");
		}
	}
}