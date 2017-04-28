using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using PPOk_Notifications.Models;
using Twilio.Rest.Trunking.V1;

namespace PPOk_Notifications.Service {

	/**
	 * Service for generating, and storing random one time passwords.
	 */
	public static class OTPService {

		/**
		 * Generates an email OTP with an attached notification for
		 * callback. Also stores the object in the database.
		 * 
		 * @param - the notification to be attached to the EmailOTP
		 * @returns - the EmailOTP generated and stored in the databse
		 */
		public static EmailOTP GenerateEmailOtp(Notification n) {
			var otp = new EmailOTP {
				NotificationId = n.NotificationId,
				Time = DateTime.Now,
				Code = RandomString(64)
			};

			otp.Id = DatabaseEmailOtpService.Insert(otp);
			return otp;
		}

		/**
		 * Generates an OTP with an attached user for callback. 
		 * Also stores the object in the database.
		 * 
		 * @param - the user for which the OTP applies
		 * @returns - the OTP generated and stored in the databse
		 */
		public static OTP GenerateOtp(User u) {
			var otp = new OTP {
				Time = DateTime.Now,
				UserId = u.UserId,
				Code = RandomString(64)
			};

			otp.Id = DatabaseOtpService.Insert(otp);
			return otp;
		}

		/**
		 * Generates a random password of a given length. Uses a specific
		 * character dictionary to prevent it from inserting non-browser compatible
		 * characters/delimiters that will eventually be part of the callback URL.
		 */
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