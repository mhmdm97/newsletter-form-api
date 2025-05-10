using newsletter_form_api.Dal.Enums;
using newsletter_form_api.Dal.Repositories.Interfaces;
using newsletter_form_api.Helpers;
using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Services.Interfaces;

namespace newsletter_form_api.Services.Implementations
{
    public class CommunicationPreferenceService(ICommunicationPreferenceRepository repository) : ICommunicationPreferenceService
    {
        private readonly ICommunicationPreferenceRepository _repository = repository;

        public async Task<List<CommunicationPreferenceDto>> GetAllCommunicationPreferencesAsync()
        {
            var communicationPreferences = await _repository.GetAllAsync();
            return EntityMapper.ToDto(communicationPreferences);
        }
    }
}