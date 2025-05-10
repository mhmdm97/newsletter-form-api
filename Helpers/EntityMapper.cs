using newsletter_form_api.Dal.Entities;
using newsletter_form_api.Models.Dtos;

namespace newsletter_form_api.Helpers
{
    //improvements could be done by making the entity mapper implement an interface which would allow for better testing and mocking
    public static class EntityMapper
    {
        public static SubscriberDto ToDto(Subscriber subscriber)
        {
            return new SubscriberDto
            {
                Id = subscriber.Id,
                Name = subscriber.Name,
                Email = subscriber.Email,
                Type = subscriber.Type,
                Interests = [.. subscriber.Interests.Select(i => i.Name)],
                CommunicationPreferences = [.. subscriber.CommunicationPreferences.Select(i => i.Tag)],
                CreatedAt = subscriber.CreatedAt
            };
        }

        public static CommunicationPreferenceDto ToDto(CommunicationPreference communicationPreference)
        {
            return new CommunicationPreferenceDto
            {
                Id = communicationPreference.Id,
                Tag = communicationPreference.Tag
            };
        }

        public static InterestDto ToDto(Interest interest)
        {
            return new InterestDto
            {
                Id = interest.Id,
                Name = interest.Name
            };
        }

        public static List<CommunicationPreferenceDto> ToDto(IEnumerable<CommunicationPreference> communicationPreferences)
        {
            return [.. communicationPreferences.Select(ToDto)];
        }

        public static List<InterestDto> ToDto(IEnumerable<Interest> interests)
        {
            return [.. interests.Select(ToDto)];
        }

        public static List<SubscriberDto> ToDto(IEnumerable<Subscriber> subscribers)
        {
            return [.. subscribers.Select(ToDto)];
        }
    }
}