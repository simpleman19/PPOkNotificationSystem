using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPOk_Notifications.Models
{
    public class Template
    {
        public long templateID { get; set; }
        public long pharmacyID { get; set; }
        public String templateEmail { get; set; }
        public String templateText { get; set; }
        public String templatePhone { get; set; }

    }
}