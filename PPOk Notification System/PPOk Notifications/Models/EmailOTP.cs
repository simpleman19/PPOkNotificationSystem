using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models {
	public class EmailOTP {

		[Column(Name = "emailotp_id")]
		public long Id { get; set; }

		[Column(Name = "notification_id")]
		public long NotificationId { get; set; }

		[Column(Name = "emailtop_time")]
		public DateTime Time { get; set; }

		[Column(Name = "emailotp_code")]
		public string Code { get; set; }

		[Column(Name = "object_active")]
		public bool object_active { get; set; }

		public bool IsActive() {
			if (!object_active) return false;
			if ((Time - DateTime.Now).TotalDays < 7) {
				return true;
			} else {
				DatabaseEmailOtpService.Disable(Id);
			}
			return false;
		}
	}
}