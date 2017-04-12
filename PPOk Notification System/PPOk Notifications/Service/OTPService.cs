using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using PPOk_Notifications.Models;
using Twilio.Rest.Trunking.V1;

namespace PPOk_Notifications.Service {

	public static class OTPService {

		public static EmailOTP GenerateEmailOtp(Notification n) {
			var otp = new EmailOTP {
				NotificationId = n.NotificationId,
				Time = DateTime.Now,
				Code = RandomString(64)
			};

			otp.Id = DatabaseEmailOtpService.Insert(otp);
			return otp;
		}

		public static OTP GenerateOtp(User u) {
			var otp = new OTP {
				Time = DateTime.Now,
				UserId = u.UserId,
				Code = RandomString(64)
			};

			otp.Id = DatabaseOtpService.Insert(otp);
			return otp;
		}

		public static string RandomString(int size) {
			const string allowedChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			var random = new Random();
			var otp = "";
			for (var i = 1; i < size + 1; i++) {
				otp  += allowedChars[random.Next(0, allowedChars.Length)];
			}
			return otp;
		}
	}
}