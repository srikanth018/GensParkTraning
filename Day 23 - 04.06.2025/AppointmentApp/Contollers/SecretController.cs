using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SecretController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetSecret()
    {
        return Ok("You accessed a protected resource!");
    }
}
