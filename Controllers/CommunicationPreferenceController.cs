using Microsoft.AspNetCore.Mvc;
using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Models.Responses;
using newsletter_form_api.Services.Interfaces;

namespace newsletter_form_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommunicationPreferenceController(
        ICommunicationPreferenceService communicationPreferenceService,
        ILogger<CommunicationPreferenceController> logger) : ControllerBase
    {
        private readonly ICommunicationPreferenceService _communicationPreferenceService = communicationPreferenceService;
        private readonly ILogger<CommunicationPreferenceController> _logger = logger;

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CommunicationPreferenceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCommunicationPreferences()
        {
            _logger.LogInformation("Starting GetAllCommunicationPreferences request");
            
            try
            {
                var result = await _communicationPreferenceService.GetAllCommunicationPreferencesAsync();
                
                if (result.IsFailure)
                {
                    _logger.LogWarning("GetAllCommunicationPreferences request failed: {ErrorMessage}", result.Error);
                    return StatusCode(500, ApiResponse<string>.Error(result.Error));
                }

                _logger.LogInformation("GetAllCommunicationPreferences request completed successfully, returned {PreferenceCount} preferences", 
                    result.Value.Count);
                    
                return Ok(ApiResponse<List<CommunicationPreferenceDto>>.Ok(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving communication preferences");
                return StatusCode(500, ApiResponse<string>.Error("An unexpected error occurred while retrieving communication preferences."));
            }
        }
    }
}