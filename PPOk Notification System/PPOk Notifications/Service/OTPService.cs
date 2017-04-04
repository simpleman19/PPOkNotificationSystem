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
			EmailOTP otp = new EmailOTP();
			otp.NotificationId = n.NotificationId;
			otp.Time = DateTime.Now;
			otp.Code = RandomString(64);

			SQLService db = new SQLService();
			otp.Id = db.EmailOTPInsert(otp);
			return otp;
		}

		public static OTP GenerateOtp(User u) {
			OTP otp = new OTP();
			otp.Time = DateTime.Now;
			otp.UserId = u.UserId;
			otp.Code = RandomString(64);

			SQLService db = new SQLService();
			otp.Id = db.OTPInsert(otp);
			return otp;
		}

		private static string RandomString(int size) {
			const string allowedChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			Random random = new Random();
			string otp = "";
			for (int i = 1; i < size + 1; i++) {
				otp  += allowedChars[random.Next(0, allowedChars.Length)];
			}
			return otp;
		}
	}
}