using Microsoft.EntityFrameworkCore;
using newsletter_form_api.Dal.Entities;
using newsletter_form_api.Dal.Repositories.Interfaces;

namespace newsletter_form_api.Dal.Repositories.Implementations
{
    public class InterestRepository(NewsletterDbContext context) : Repository<Interest>(context), IInterestRepository
    {
        public async Task<List<Interest>> GetInterestsByIdsAsync(List<int> ids)
        {
            return await _dbSet
                .Where(i => ids.Contains(i.Id))
                .ToListAsync();
        }
    }
}