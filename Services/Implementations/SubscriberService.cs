using newsletter_form_api.Dal.Entities;
using newsletter_form_api.Dal.Repositories.Interfaces;
using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Services.Interfaces;
using newsletter_form_api.Helpers;

namespace newsletter_form_api.Services.Implementations
{
    public class SubscriberService(
        ISubscriberRepository subscriberRepository,
        IInterestRepository interestRepository,
        ICommunicationPreferenceRepository communicationPreferenceRepository) : ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository = subscriberRepository;
        private readonly IInterestRepository _interestRepository = interestRepository;
        private readonly ICommunicationPreferenceRepository _communicationPreferenceRepository = communicationPreferenceRepository;

        public async Task<SubscriberDto> CreateSubscriberAsync(CreateSubscriberDto createDto)
        {
            // Validate email uniqueness
            if (await _subscriberRepository.EmailExistsAsync(createDto.Email))
                throw new InvalidOperationException($"Subscriber with email {createDto.Email} already exists.");

            // Validate phone number uniqueness
            if (await _subscriberRepository.PhoneNumberExistsAsync(createDto.PhoneNumber))
                throw new InvalidOperationException($"Subscriber with phone number {createDto.PhoneNumber} already exists.");

            // Get interests from repository
            var interests = await _interestRepository.GetInterestsByIdsAsync(createDto.InterestIds);

            if (interests.Count != createDto.InterestIds.Count)
                throw new ArgumentException("One or more interest IDs are invalid.");

            // Get communication preferences from repository
            var communicationPreferences = await _communicationPreferenceRepository.GetByIdsAsync(createDto.CommunicationPreferencesIds);

            if (communicationPreferences.Count != createDto.CommunicationPreferencesIds.Count)
                throw new ArgumentException("One or more communication methods are invalid.");

            // Create new subscriber
            var subscriber = new Subscriber
            {
                Name = createDto.Name,
                Email = createDto.Email,
                PhoneNumber = createDto.PhoneNumber,
                Type = createDto.Type,
                Interests = interests,
                CommunicationPreferences = communicationPreferences
            };

            await _subscriberRepository.AddAsync(subscriber);
            await _subscriberRepository.SaveChangesAsync();

            return EntityMapper.ToDto(subscriber);
        }

        public async Task<SubscriberDto?> GetSubscriberByIdAsync(int id)
        {
            var subscriber = await _subscriberRepository.GetSubscriberWithDetailsAsync(id);
            if (subscriber == null)
                return null;
                
            return EntityMapper.ToDto(subscriber);
        }

        public async Task<List<SubscriberDto>> GetAllSubscribersAsync()
        {
            var subscribers = await _subscriberRepository.GetAllSubscribersWithDetailsAsync();
            return EntityMapper.ToDto(subscribers);
        }

        public async Task<bool> DeleteSubscriberAsync(int id)
        {
            var subscriber = await _subscriberRepository.GetByIdAsync(id);
            if (subscriber == null)
                return false;

            _subscriberRepository.Delete(subscriber);
            return await _subscriberRepository.SaveChangesAsync();
        }

        public async Task<bool> SubscriberExistsAsync(string email)
        {
            return await _subscriberRepository.EmailExistsAsync(email);
        }
    }
}