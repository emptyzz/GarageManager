using Microsoft.AspNetCore.Mvc;

namespace GarageManager.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "ok", timestamp = DateTime.UtcNow });
    }

    [HttpGet("version")]
    public IActionResult GetVersion()
    {
        return Ok(new { version = "1.0.0", api = "GarageManager" });
    }
}