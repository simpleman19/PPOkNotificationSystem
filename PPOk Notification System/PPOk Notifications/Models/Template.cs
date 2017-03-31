using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models
{
    public class Template
    {
        [Column(Name = "template_id")] public long TemplateId { get; set; }

        public long PharmacyId { get; set; }

        public string TemplateEmail { get; set; }

        public string TemplateText { get; set; }

        public string TemplatePhone { get; set; }

    }
}
