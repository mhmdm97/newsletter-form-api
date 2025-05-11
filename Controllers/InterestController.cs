using Microsoft.AspNetCore.Mvc;
using newsletter_form_api.Models.Responses;
using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Services.Interfaces;
using System.Text.Json;

namespace newsletter_form_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestController(IInterestService interestService, ILogger<InterestController> logger) : ControllerBase
    {
        private readonly IInterestService _interestService = interestService;
        private readonly ILogger<InterestController> _logger = logger;

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<InterestDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllInterests()
        {
            _logger.LogInformation("Starting GetAllInterests request");
            
            try
            {
                var result = await _interestService.GetAllInterestsAsync();
                
                if (result.IsFailure)
                {
                    _logger.LogWarning("GetAllInterests request failed: {ErrorMessage}", result.Error);
                    return StatusCode(500, ApiResponse<string>.Error(result.Error));
                }

                _logger.LogInformation("GetAllInterests request completed successfully, returned {InterestCount} interests", 
                    result.Value.Count);
                    
                return Ok(ApiResponse<List<InterestDto>>.Ok(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving interests");
                return StatusCode(500, ApiResponse<string>.Error("An unexpected error occurred while retrieving interests."));
            }
        }
    }
}