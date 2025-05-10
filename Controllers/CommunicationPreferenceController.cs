using Microsoft.AspNetCore.Mvc;
using newsletter_form_api.Dal.Enums;
using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Models.Responses;
using newsletter_form_api.Services.Interfaces;

namespace newsletter_form_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommunicationPreferenceController(ICommunicationPreferenceService communicationPreferenceService) : ControllerBase
    {
        private readonly ICommunicationPreferenceService _communicationPreferenceService = communicationPreferenceService;

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CommunicationPreferenceDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCommunicationPreferences()
        {
            try
            {
                var communicationPreferences = await _communicationPreferenceService.GetAllCommunicationPreferencesAsync();
                return Ok(ApiResponse<List<CommunicationPreferenceDto>>.Ok(communicationPreferences));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<string>.Error("An error occurred while retrieving communication preferences"));
            }
        }
    }
}