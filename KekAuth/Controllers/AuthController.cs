using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace KekAuth.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    public AuthController()
    {
        Log.Information("SIEMKA2");
        Log.Error("Siemka2");
    }

    [HttpGet]
    public IActionResult Token([FromQuery] string user)
    {
        return Ok($"User token: 1231231 for User: {user}");
    }
}