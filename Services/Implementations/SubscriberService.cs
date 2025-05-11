using newsletter_form_api.Dal.Entities;
using newsletter_form_api.Dal.Repositories.Interfaces;
using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Services.Interfaces;
using newsletter_form_api.Helpers;
using newsletter_form_api.Models.Results;

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

        public async Task<Result<SubscriberDto>> CreateSubscriberAsync(CreateSubscriberDto createDto)
        {
            // Validate email uniqueness
            if (await _subscriberRepository.EmailExistsAsync(createDto.Email))
                return Result.Conflict<SubscriberDto>($"Subscriber with email {createDto.Email} already exists.");

            // Validate phone number uniqueness
            if (await _subscriberRepository.PhoneNumberExistsAsync(createDto.PhoneNumber))
                return Result.Conflict<SubscriberDto>($"Subscriber with phone number {createDto.PhoneNumber} already exists.");

            // Get interests from repository
            var interests = await _interestRepository.GetInterestsByIdsAsync(createDto.InterestIds);

            if (interests.Count != createDto.InterestIds.Count)
                return Result.ValidationError<SubscriberDto>("One or more interest IDs are invalid.");

            // Get communication preferences from repository
            var communicationPreferences = await _communicationPreferenceRepository.GetByIdsAsync(createDto.CommunicationPreferencesIds);

            if (communicationPreferences.Count != createDto.CommunicationPreferencesIds.Count)
                return Result.ValidationError<SubscriberDto>("One or more communication methods are invalid.");

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
            var success = await _subscriberRepository.SaveChangesAsync();

            if (!success)
                return Result.Failure<SubscriberDto>("Failed to save subscriber to database.");

            return Result.Success(EntityMapper.ToDto(subscriber));
        }

        public async Task<Result<SubscriberDto>> GetSubscriberByIdAsync(int id)
        {
            var subscriber = await _subscriberRepository.GetSubscriberWithDetailsAsync(id);
            if (subscriber == null)
                return Result.NotFound<SubscriberDto>($"Subscriber with ID {id} not found.");

            return Result.Success(EntityMapper.ToDto(subscriber));
        }

        public async Task<Result<List<SubscriberDto>>> GetAllSubscribersAsync()
        {
            var subscribers = await _subscriberRepository.GetAllSubscribersWithDetailsAsync();
            return Result.Success(EntityMapper.ToDto(subscribers.ToList()));
        }

        public async Task<Result> DeleteSubscriberAsync(int id)
        {
            var subscriber = await _subscriberRepository.GetByIdAsync(id);
            if (subscriber == null)
                return Result.NotFound($"Subscriber with ID {id} not found.");

            _subscriberRepository.Delete(subscriber);
            var success = await _subscriberRepository.SaveChangesAsync();

            return success
                ? Result.Success()
                : Result.Failure("Failed to delete subscriber.");
        }
    }
}