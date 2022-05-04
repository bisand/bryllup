using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly ILogger<ContactController> _logger;

    public ContactController(ILogger<ContactController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] ContactForm model)
    {
        _logger.LogInformation(0, null, (object)model);
        return Ok(new { text = "Takk for tilbakemeldingen!" });
    }
}