using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Models.Results;

namespace newsletter_form_api.Services.Interfaces
{
    public interface ISubscriberService
    {
        Task<Result<SubscriberDto>> CreateSubscriberAsync(CreateSubscriberDto createDto);
        Task<Result<SubscriberDto>> GetSubscriberByIdAsync(int id);
        Task<Result<List<SubscriberDto>>> GetAllSubscribersAsync();
        Task<Result> DeleteSubscriberAsync(int id);
    }
}