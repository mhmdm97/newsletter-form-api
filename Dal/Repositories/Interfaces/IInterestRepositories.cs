using newsletter_form_api.Dal.Entities;

namespace newsletter_form_api.Dal.Repositories.Interfaces
{
    public interface IInterestRepository : IRepository<Interest>
    {
        Task<List<Interest>> GetInterestsByIdsAsync(List<int> ids);
    }
}