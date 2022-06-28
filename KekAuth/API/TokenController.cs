using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace kekAuth.API;

public class TokenController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Token()
    {
        return Ok("token");
    }
}