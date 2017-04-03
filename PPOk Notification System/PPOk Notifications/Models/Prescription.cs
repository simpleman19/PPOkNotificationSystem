using System;
using System.ComponentModel;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models
{
    public class Prescription
    {
        [Column(Name = "patient_id")]
        public long PatientId { get; set; }
        [Column(Name = "prescription_id")]
        public long PrecriptionId { get; set; }
        public int PrescriptionNumber { get; set; }
        [Column(Name = "prescription_name")]
        public string PrescriptionName { get; set; }
        [Column(Name = "prescription_datefilled")]
        public DateTime PrescriptionDateFilled { get; set; }
        [Column(Name = "prescription_supply")]
        public int PrescriptionDaysSupply { get; set; }
        [Column(Name = "prescription_refills")]
        public int PrescriptionRefills { get; set; }
        [Column(Name = "prescription_upc")]
        public string PrescriptionUpc { get; set; }

    }
}