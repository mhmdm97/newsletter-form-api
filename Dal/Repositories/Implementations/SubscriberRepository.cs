using Microsoft.EntityFrameworkCore;
using newsletter_form_api.Dal;
using newsletter_form_api.Dal.Entities;
using newsletter_form_api.Dal.Repositories.Interfaces;

namespace newsletter_form_api.Dal.Repositories.Implementations
{
    public class SubscriberRepository(NewsletterDbContext context) : Repository<Subscriber>(context), ISubscriberRepository
    {
        public async Task<Subscriber?> GetSubscriberWithDetailsAsync(int id)
        {
            return await _context.Subscribers
                .Include(s => s.Interests)
                .Include(s => s.CommunicationPreferences)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Subscriber>> GetAllSubscribersWithDetailsAsync()
        {
            return await _context.Subscribers
                .Include(s => s.Interests)
                .Include(s => s.CommunicationPreferences)
                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Subscribers
                .AnyAsync(s => s.Email.ToLower() == email.ToLower());
        }
        public async Task<bool> PhoneNumberExistsAsync(string phoneNumber)
        {
            return await _context.Subscribers
                .AnyAsync(s => s.PhoneNumber == phoneNumber);
        }
    }
}