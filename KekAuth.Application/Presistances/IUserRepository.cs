using KekAuth.Domain.Entities;

namespace KekAuth.Application.Presistances;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}