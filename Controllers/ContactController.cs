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

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { result = "OK" });
    }

    [HttpPost]
    public IActionResult Post([FromForm] ContactForm model)
    {
        if (string.IsNullOrWhiteSpace(model.InputName) || string.IsNullOrWhiteSpace(model.InputEmail) || string.IsNullOrWhiteSpace(model.InputEvents))
            return BadRequest("Manglende felt");

        var mailClient = new MailClient();
        try
        {
            var message = string.Format("Navn: {0}\r\nE-post: {1}\r\nKommer: {2}\r\nMelding: {3}\r\n", model.InputName, model.InputEmail, model.InputEvents, model.InputMessage);
            mailClient.Send("bryllup@biseth.net", message);
        }
        catch (System.Exception)
        {
            return BadRequest("An error ocurred while trying to send mail");
        }
        return Ok(new { text = "Takk for tilbakemeldingen!" });
    }
}