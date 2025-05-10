using newsletter_form_api.Dal.Enums;

namespace newsletter_form_api.Models.Dtos
{
    public class SubscriberDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public SubscriberType Type { get; set; }
        public List<string> Interests { get; set; } = [];
        public List<string> CommunicationPreferences { get; set; } = [];
        public DateTime CreatedAt { get; set; }
    }
}