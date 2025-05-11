using newsletter_form_api.Dal.Repositories.Interfaces;
using newsletter_form_api.Helpers;
using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Services.Interfaces;
using newsletter_form_api.Models.Results;

namespace newsletter_form_api.Services.Implementations
{
    public class CommunicationPreferenceService(ICommunicationPreferenceRepository repository) : ICommunicationPreferenceService
    {
        private readonly ICommunicationPreferenceRepository _repository = repository;

        public async Task<Result<List<CommunicationPreferenceDto>>> GetAllCommunicationPreferencesAsync()
        {
            var communicationPreferences = await _repository.GetAllAsync();
            return Result.Success(EntityMapper.ToDto(communicationPreferences));
        }
    }
}