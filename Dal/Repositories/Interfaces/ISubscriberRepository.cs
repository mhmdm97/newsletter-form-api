using newsletter_form_api.Dal.Entities;

namespace newsletter_form_api.Dal.Repositories.Interfaces
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        Task<Subscriber?> GetSubscriberWithDetailsAsync(int id);
        Task<IEnumerable<Subscriber>> GetAllSubscribersWithDetailsAsync();
        Task<bool> EmailExistsAsync(string email);
        Task<bool> PhoneNumberExistsAsync(string phoneNumber);
        
    }
}