using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Models.Results;

namespace newsletter_form_api.Services.Interfaces
{
    public interface ICommunicationPreferenceService
    {
        Task<Result<List<CommunicationPreferenceDto>>> GetAllCommunicationPreferencesAsync();
    }
}