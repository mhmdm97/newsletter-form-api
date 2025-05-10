using newsletter_form_api.Dal.Enums;
using System.ComponentModel.DataAnnotations;

namespace newsletter_form_api.Models.Dtos
{
    public class CreateSubscriberDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Type is required")]
        public SubscriberType Type { get; set; }
        
        [Required(ErrorMessage = "At least one interest is required")]
        [MinLength(1, ErrorMessage = "At least one interest must be selected")]
        public List<int> InterestIds { get; set; } = [];
        
        [Required(ErrorMessage = "At least one communication preference is required")]
        [MinLength(1, ErrorMessage = "At least one communication method must be selected")]
        public List<int> CommunicationPreferencesIds { get; set; } = [];
    }
}