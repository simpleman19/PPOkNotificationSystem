using System;
using System.Net;
using System.Net.Mail;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public class EmailService {
		public static bool Send(Notification notification) {
			SQLService db = new SQLService();

			Patient patient = db.GetPatientById(notification.PatientId);
			User user = db.GetUserById(patient.UserId);
			Pharmacy pharmacy = db.GetPharmacyById(patient.PharmacyId);

			var message = new MailMessage();
			message.To.Add(new MailAddress(user.Email));
			message.From = new MailAddress("ppoknotifier@gmail.com");

			var body = "";

			if (notification.Type == Notification.NotificationType.Birthday) {
				message.Subject = "Happy Birthday, from " + pharmacy.PharmacyName + "!";
			} else if (notification.Type == Notification.NotificationType.Ready) {
				message.Subject = "Your Refill is ready to be picked up!";
			} else if (notification.Type == Notification.NotificationType.Recall) {
				message.Subject = "Important! A Prescription has been recalled!";
				message.Priority = MailPriority.High;
			} else if (notification.Type == Notification.NotificationType.Refill) {
				message.Subject = "Would you like to refill your prescription?";
			} else {
				message.Subject = "Test Email";
			}

			//message.Body = string.Format(body, senduser.user_name);
			message.IsBodyHtml = true;

			try {
				using (var smtp = new SmtpClient()) {
					var credential = new NetworkCredential {
						UserName = "ppoknotifier@gmail.com", //TESTING ACCOUNT
						Password = "L7BQ2aOYpEO9lXiAVW9uEA2Rju9X7scFLN7M7RUhpuRQ9fAcllk4be0bThlQfj1eqgj4YzII6Ve10gBbKrmtv5T9JYlRWmHq5s9w"
					};
					smtp.Credentials = credential;
					smtp.Host = "smtp.gmail.com";
					smtp.Port = 587;
					smtp.EnableSsl = true;

					smtp.Send(message);
				}
			}
			catch (Exception e) {
				return false;
			}
			return true;
		}
	}
}