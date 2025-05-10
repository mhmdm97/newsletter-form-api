using newsletter_form_api.Models.Dtos;

namespace newsletter_form_api.Services.Interfaces
{
    public interface ISubscriberService
    {
        Task<SubscriberDto> CreateSubscriberAsync(CreateSubscriberDto createDto);
        Task<SubscriberDto?> GetSubscriberByIdAsync(int id);
        Task<List<SubscriberDto>> GetAllSubscribersAsync();
        Task<bool> DeleteSubscriberAsync(int id);
        Task<bool> SubscriberExistsAsync(string email);
    }
}