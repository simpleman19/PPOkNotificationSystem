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
	}
}