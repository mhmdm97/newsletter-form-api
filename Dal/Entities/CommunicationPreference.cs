using System.ComponentModel.DataAnnotations;
using newsletter_form_api.Dal.Enums;

namespace newsletter_form_api.Dal.Entities
{
    public class CommunicationPreference : BaseEntity
    {
        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;
        public ICollection<Subscriber> Subscribers { get; set; } = [];
    }
}