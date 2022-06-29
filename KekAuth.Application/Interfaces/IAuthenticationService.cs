using KekAuth.Application.Models;

namespace KekAuth.Application.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticationResult> Register(string login, string password, string email, string? firstName,
        string? lastName);
}