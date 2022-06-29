using KekAuth.Application.Interfaces;
using KekAuth.Application.Models;

namespace KekAuth.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    //register user
    public async Task<AuthenticationResult> Register(string login, string password, string email, string? firstName,
        string? lastName)
    {
        //check if user exists in database

        //create user

        //create jwt
        var userId = Guid.NewGuid();
        //var token = _jwtTokenGenerator.GenerateToken();
        return new AuthenticationResult("name", "lastname", "role", "token", "username", "admin");
    }
}