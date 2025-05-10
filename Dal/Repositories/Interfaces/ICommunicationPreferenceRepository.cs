using newsletter_form_api.Dal.Entities;
using newsletter_form_api.Dal.Enums;

namespace newsletter_form_api.Dal.Repositories.Interfaces
{
    public interface ICommunicationPreferenceRepository : IRepository<CommunicationPreference>
    {
        Task<List<CommunicationPreference>> GetByIdsAsync(List<int> ids);
    }
}