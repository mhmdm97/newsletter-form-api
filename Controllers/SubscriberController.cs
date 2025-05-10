using Microsoft.AspNetCore.Mvc;
using newsletter_form_api.Models.Responses;
using newsletter_form_api.Models.Dtos;
using newsletter_form_api.Services.Interfaces;

namespace newsletter_form_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriberController(ISubscriberService subscriberService) : ControllerBase
    {
        private readonly ISubscriberService _subscriberService = subscriberService;

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<SubscriberDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSubscriber([FromBody] CreateSubscriberDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<string>.Error("Invalid input data"));

                var subscriber = await _subscriberService.CreateSubscriberAsync(createDto);
                return CreatedAtAction(nameof(GetSubscriber), new { id = subscriber.Id },
                    ApiResponse<SubscriberDto>.Ok(subscriber, "Subscriber created successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<string>.Error(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<string>.Error(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<string>.Error("An error occurred while creating the subscriber"));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<SubscriberDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubscriber(int id)
        {
            try
            {
                var subscriber = await _subscriberService.GetSubscriberByIdAsync(id);
                if (subscriber == null)
                    return NotFound(ApiResponse<string>.Error($"Subscriber with ID {id} not found"));

                return Ok(ApiResponse<SubscriberDto>.Ok(subscriber));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<string>.Error("An error occurred while retrieving the subscriber"));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<SubscriberDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSubscribers()
        {
            try
            {
                var subscribers = await _subscriberService.GetAllSubscribersAsync();
                return Ok(ApiResponse<List<SubscriberDto>>.Ok(subscribers));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<string>.Error("An error occurred while retrieving subscribers"));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            try
            {
                var result = await _subscriberService.DeleteSubscriberAsync(id);
                if (!result)
                    return NotFound(ApiResponse<string>.Error($"Subscriber with ID {id} not found"));

                return Ok(ApiResponse<bool>.Ok(true, "Subscriber deleted successfully"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<string>.Error("An error occurred while retrieving the subscriber"));
            }
        }
    }
}