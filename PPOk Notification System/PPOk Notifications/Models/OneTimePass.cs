using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPOk_Notifications.Models
{
    public class OneTimePass
    {
        public long otpID { get; set; }
        public long userID { get; set; }
        public DateTime otpTimeStamp { get; set; }
        public String otpCode { get; set; }
    }
}