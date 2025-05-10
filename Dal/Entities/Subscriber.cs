using newsletter_form_api.Dal.Enums;

namespace newsletter_form_api.Dal.Entities
{
    public class Subscriber : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public SubscriberType Type { get; set; }
        public ICollection<CommunicationPreference> CommunicationPreferences { get; set; } = [];
        public ICollection<Interest> Interests { get; set; } = [];
    }
}