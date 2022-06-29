namespace KekAuth.Application.Models;

public class AuthenticationResult
{
    public AuthenticationResult(string firstName, string lastName, string email, string token, string login,
        string role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Token = token;
        Login = login;
        Role = role;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string Login { get; set; }
}