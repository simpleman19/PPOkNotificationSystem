using System;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class EmailService {
		public static bool Send(Notification notification) {
			SQLService db = new SQLService();

			//Get necessary data to build and format the email
			Patient patient = db.GetPatientById(notification.PatientId);
			User user = db.GetUserById(patient.UserId);
			Pharmacy pharmacy = db.GetPharmacyById(patient.PharmacyId);

			var message = new MailMessage();
			message.To.Add(new MailAddress(user.Email));
			message.From = new MailAddress(ConfigurationManager.AppSettings["SendEmailAddress"]);

			//Emails always get style sheet and header
			var body = EmailHtmlLoader.TemplateHtml;
			var content = "";

			//Set email subject and body based on type of email
			if (notification.Type == Notification.NotificationType.Birthday) {
				message.Subject = "Happy Birthday, from " + pharmacy.PharmacyName + "!";
				content += EmailHtmlLoader.BirthdayHtml;

			} else if (notification.Type == Notification.NotificationType.Ready) {
				message.Subject = "Your Refill is ready to be picked up!";
				content += EmailHtmlLoader.ReadyHtml;

			} else if (notification.Type == Notification.NotificationType.Recall) {
				message.Subject = "Important! A Prescription has been recalled!";
				message.Priority = MailPriority.High;
				content += EmailHtmlLoader.RecallHtml;

			} else if (notification.Type == Notification.NotificationType.Refill) {
				message.Subject = "Would you like to refill your prescription?";
				content += EmailHtmlLoader.RefillHtml;

			} else {
				message.Subject = "Test Email";
			}

			//Replace html template placeholder with renderbody
			body = body.Replace("{{HtmlBody}}", content);

			body = body.Replace("{{MessageText}}", notification.NotificationMessage);

			//Replace sentinels in email with personalized data
			body = body.Replace("{{PharmacyName}}", pharmacy.PharmacyName);
			body = body.Replace("{{PharmacyPhone}}", pharmacy.PharmacyPhone);
			body = body.Replace("{{PharmacyAddress}}", pharmacy.PharmacyAddress);
			body = body.Replace("{{PatientName}}", patient.GetFullName());
			body = body.Replace("{{PatientFirstName}}", patient.FirstName);
			body = body.Replace("{{PatientLastName}}", patient.LastName);
			body = body.Replace("{{PatientPhone}}", patient.Phone);
			body = body.Replace("{{PatientPhone}}", patient.Phone);
			body = body.Replace("{{PatientEmail}}", patient.Email);
			body = body.Replace("{{PatientDOBShort}}", patient.DateOfBirth.ToShortDateString());
			body = body.Replace("{{PatientDOBLong}}", patient.DateOfBirth.ToLongDateString());
			body = body.Replace("{{PatientContactTimeShort}}", patient.PreferedContactTime.ToShortTimeString());
			body = body.Replace("{{PatientContactTimeLong}}", patient.PreferedContactTime.ToLongTimeString());

			message.Body = body;
			message.IsBodyHtml = true;

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
			}
			catch (Exception e) {
				return false;
			}
			return true;
		}
	}

	public class EmailHtmlLoader {

		public static String TemplateHtml { get; private set; }

		public static String BirthdayHtml { get; private set; }
		public static String ReadyHtml { get; private set; }
		public static String RefillHtml { get; private set; }
		public static String RecallHtml { get; private set; }
		

		public static void Init() {
			TemplateHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+@"\Service\EmailHTML\template.html");
			BirthdayHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+@"\Service\EmailHTML\birthday.html");
			ReadyHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+@"\Service\EmailHTML\ready.html");
			RefillHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+@"\Service\EmailHTML\refill.html");
			RecallHtml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+@"\Service\EmailHTML\recall.html");
		}
	}
}