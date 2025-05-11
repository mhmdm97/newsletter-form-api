using Microsoft.AspNetCore.Mvc;
using newsletter_form_api.Models.Responses;
using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Services.Interfaces;
using newsletter_form_api.Models.Results;

namespace newsletter_form_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriberController(ISubscriberService subscriberService, ILogger<SubscriberController> logger) : ControllerBase
    {
        private readonly ISubscriberService _subscriberService = subscriberService;
        private readonly ILogger<SubscriberController> _logger = logger;

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<SubscriberDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSubscriber([FromBody] CreateSubscriberDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<string>.Error("Invalid input data"));

                var result = await _subscriberService.CreateSubscriberAsync(createDto);
                
                if (result.IsFailure)
                {
                    return result.ErrorType switch
                    {
                        ErrorType.Validation => BadRequest(ApiResponse<string>.Error(result.Error)),
                        ErrorType.Conflict => Conflict(ApiResponse<string>.Error(result.Error)),
                        ErrorType.NotFound => NotFound(ApiResponse<string>.Error(result.Error)),
                        _ => StatusCode(500, ApiResponse<string>.Error(result.Error))
                    };
                }

                return CreatedAtAction(nameof(GetSubscriber), new { id = result.Value.Id },
                    ApiResponse<SubscriberDto>.Ok(result.Value, "Subscriber created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating subscriber");
                return StatusCode(500, ApiResponse<string>.Error("An unexpected error occurred while creating subscriber."));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<SubscriberDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubscriber(int id)
        {
            try
            {
                var result = await _subscriberService.GetSubscriberByIdAsync(id);
                
                if (result.IsFailure)
                {
                    return result.ErrorType switch
                    {
                        ErrorType.NotFound => NotFound(ApiResponse<string>.Error(result.Error)),
                        _ => StatusCode(500, ApiResponse<string>.Error(result.Error))
                    };
                }

                return Ok(ApiResponse<SubscriberDto>.Ok(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving subscriber with ID {Id}", id);
                return StatusCode(500, ApiResponse<string>.Error("An unexpected error occurred while retrieving subscriber."));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<SubscriberDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSubscribers()
        {
            try
            {
                var result = await _subscriberService.GetAllSubscribersAsync();
                
                if (result.IsFailure)
                {
                    return StatusCode(500, ApiResponse<string>.Error(result.Error));
                }

                return Ok(ApiResponse<List<SubscriberDto>>.Ok(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all subscribers");
                return StatusCode(500, ApiResponse<string>.Error("An unexpected error occurred while retrieving subscribers."));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            try
            {
                var result = await _subscriberService.DeleteSubscriberAsync(id);
                
                if (result.IsFailure)
                {
                    return result.ErrorType switch
                    {
                        ErrorType.NotFound => NotFound(ApiResponse<string>.Error(result.Error)),
                        _ => StatusCode(500, ApiResponse<string>.Error(result.Error))
                    };
                }

                return Ok(ApiResponse<bool>.Ok(true, "Subscriber deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting subscriber with ID {Id}", id);
                return StatusCode(500, ApiResponse<string>.Error("An unexpected error occurred while deleting subscriber."));
            }
        }
    }
}