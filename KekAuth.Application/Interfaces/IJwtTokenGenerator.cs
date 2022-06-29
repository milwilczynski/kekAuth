namespace KekAuth.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string lastName, string firstName, string email, string login);
}