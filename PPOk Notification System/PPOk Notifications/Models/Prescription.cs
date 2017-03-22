using System;

namespace PPOk_Notifications.Models
{
    public class Prescription
    {
        public long PatientId { get; set; }
        public long PrecriptionId { get; set; }
        public int PrescriptionNumber { get; set; }
        public string PrescriptionName { get; set; }
        public DateTime PrescriptionDateFilled { get; set; }
        public int PrescriptionDaysSupply { get; set; }
        public int PrescriptionRefills { get; set; }
        public string PrescriptionUpc { get; set; }

    }
}