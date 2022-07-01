namespace KekAuth.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string login, string email, string firstName, string lastName);
}