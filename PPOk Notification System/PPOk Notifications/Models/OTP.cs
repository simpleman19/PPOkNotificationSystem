using System;

namespace PPOk_Notifications.Models
{
    public class OTP
    {
        public long OtpId { get; set; }
        public long UserId { get; set; }
        public DateTime OtpTimeStamp { get; set; }
        public string OtpCode { get; set; }
    }
}