using KekAuth.Application.Interfaces;
using KekAuth.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace kekAuth.API;

public class AuthController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Password))
        {
            return BadRequest();
        }

        return Ok(await _authenticationService.Login(request.Email, request.Password));
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) &&
            string.IsNullOrEmpty(request.Password) && string.IsNullOrEmpty(request.Email))
        {
            return BadRequest();
        }

        var user = await _authenticationService.Register(request.Username, request.Password, request.Email,
            request.FirstName, request.LastName);

        return Ok(user);
    }
}