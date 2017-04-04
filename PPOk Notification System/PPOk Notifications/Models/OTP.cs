using System;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models
{
    public class OTP
    {

		[Column(Name = "otp_id")]
		public long Id { get; set; }

		[Column(Name = "user_id")]
		public long UserId { get; set; }

		[Column(Name = "otp_time")]
		public DateTime Time { get; set; }

		[Column(Name = "otp_code")]
		public string Code { get; set; }
    }
}