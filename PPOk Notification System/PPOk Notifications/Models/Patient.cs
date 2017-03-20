using System;

namespace PPOk_Notifications.Models
{
    public class Patient : User
    {
        public long PatientId { get; set; }
        public int PersonCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime PreferedContactTime { get; set; }
        public bool SendBirthdayMessage { get; set; }
        public bool SendRefillMessage { get; set; }
        public OneTimePass OneTimePass { get; set; }
        public PrimaryContactMethod ContactMethod { get; set; }

        public enum PrimaryContactMethod
        {
            Text,
            Call,
            Email
        }
    }
}