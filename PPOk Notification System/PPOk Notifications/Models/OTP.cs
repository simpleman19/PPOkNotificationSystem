using System;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models
{
    public class OTP
    {

		[Column(Name = "otp_id")]
		public long OtpId { get; set; }

		[Column(Name = "user_id")]
		public long UserId { get; set; }

		[Column(Name = "otp_time")]
		public DateTime TimeStamp { get; set; }

		[Column(Name = "otp_code")]
		public string OtpCode { get; set; }
    }
}