using KekAuth.Domain.Entities;

namespace KekAuth.Application.Presistances;

public interface IUserRepository
{
    Task<bool> Add(User user);
    Task<User?> GetUserByEmail(string email);
}