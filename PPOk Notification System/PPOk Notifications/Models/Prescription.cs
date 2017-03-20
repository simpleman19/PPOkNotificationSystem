using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPOk_Notifications.Models
{
    public class Prescription
    {
        public long patientID { get; set; }
        public long precriptionID { get; set; }
        public int prescriptionNumber { get; set; }
        public String prescriptionName { get; set; }
        public DateTime prescriptionDateFilled { get; set; }
        public int prescriptionDaysSupply { get; set; }
        public int prescriptionRefills { get; set; }
        public String prescriptionUPC { get; set; }

    }
}