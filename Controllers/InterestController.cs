using Microsoft.AspNetCore.Mvc;
using newsletter_form_api.Models.Responses;
using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Services.Interfaces;

namespace newsletter_form_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestController(IInterestService interestService) : ControllerBase
    {
        private readonly IInterestService _interestService = interestService;

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<InterestDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllInterests()
        {
            try
            {
                var interests = await _interestService.GetAllInterestsAsync();
                return Ok(ApiResponse<List<InterestDto>>.Ok(interests));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<string>.Error("An error occurred while retrieving interests"));
            }
        }
    }
}