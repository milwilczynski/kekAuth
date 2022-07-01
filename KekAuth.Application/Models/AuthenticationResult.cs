namespace KekAuth.Application.Models;

public class AuthenticationResult
{
    public AuthenticationResult(string token)
    {
        Token = token;
    }

    public string Token { get; set; }
}