using Microsoft.EntityFrameworkCore;
using newsletter_form_api.Dal.Entities;
using newsletter_form_api.Dal.Enums;
using newsletter_form_api.Dal.Repositories.Interfaces;

namespace newsletter_form_api.Dal.Repositories.Implementations
{
    public class CommunicationPreferenceRepository(NewsletterDbContext context) 
        : Repository<CommunicationPreference>(context), ICommunicationPreferenceRepository
    {
        public async Task<List<CommunicationPreference>> GetByIdsAsync(List<int> ids)
        {
            return await _dbSet
                .Where(cp => ids.Contains(cp.Id))
                .ToListAsync();
        }
    }
}