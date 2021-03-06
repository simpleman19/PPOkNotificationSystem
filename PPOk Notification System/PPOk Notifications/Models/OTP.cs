﻿using System;
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

		[Column(Name = "object_active")]
		public bool object_active { get; set; }

		public bool IsActive() {
			if (!object_active) return false;
			if ((Time - DateTime.Now).TotalDays < 1) {
				return true;
			} else {
				DatabaseOtpService.Disable(Id);
			}
			return false;
		}

	}
}