using newsletter_form_api.Models.Dtos;

namespace newsletter_form_api.Services.Interfaces
{
    public interface IInterestService
    {
        Task<List<InterestDto>> GetAllInterestsAsync();
    }
}