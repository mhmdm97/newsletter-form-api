using Microsoft.AspNetCore.Mvc;

namespace newsletter_form_api.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsletterController(ILogger<NewsletterController> logger) : ControllerBase
{
    private readonly ILogger<NewsletterController> _logger = logger;

    [HttpGet]
    public IActionResult GetNewsletters()
    {
        _logger.LogInformation("GetNewsletters endpoint called.");
        return Ok("GetNewsletters endpoint is working!");
    }
}
