using newsletter_form_api.Dal.Enums;

namespace newsletter_form_api.Models.Dtos
{
    public class CommunicationPreferenceDto
    {
        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;
    }
}