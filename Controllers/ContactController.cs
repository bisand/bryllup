using Microsoft.AspNetCore.Mvc;

namespace bryllup.Controllers;

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
    public IActionResult Post([FromForm] ContactForm model)
    {
        _logger.LogInformation(0, null, (object)model);
        var mailClient = new MailClient();
        try
        {
            mailClient.Send("bryllup@biseth.net", model.InputMessage);
        }
        catch (System.Exception)
        {
            return BadRequest("An error ofurred while trying to send mail");
        }
        return Ok(new { text = "Takk for tilbakemeldingen!" });
    }
}