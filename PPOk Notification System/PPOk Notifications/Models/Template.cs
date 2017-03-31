using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models
{
    public class Template
    {
        [Column(Name = "template_id")] public long TemplateId { get; set; }
        [Column(Name = "pharmacy_id")] public long PharmacyId { get; set; }
        [Column(Name = "template_email")]  public string TemplateEmail { get; set; }
        [Column(Name = "template_text")] public string TemplateText { get; set; }
        [Column(Name = "template_phone")] public string TemplatePhone { get; set; }

        public void Fill()
        {
            TemplateEmail = "";
            TemplateText = "";
            TemplatePhone = "";
        }
    }
}
