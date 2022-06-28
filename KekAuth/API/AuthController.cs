using System.Threading.Tasks;
using KekAuth.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace kekAuth.API;

public class AuthController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Login) && string.IsNullOrEmpty(request.Password))
        {
            return BadRequest();
        }

        return Ok("user");
    }
}