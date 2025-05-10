using newsletter_form_api.Dal.Enums;
using newsletter_form_api.Models.Dtos;

namespace newsletter_form_api.Services.Interfaces
{
    public interface ICommunicationPreferenceService
    {
        Task<List<CommunicationPreferenceDto>> GetAllCommunicationPreferencesAsync();
    }
}