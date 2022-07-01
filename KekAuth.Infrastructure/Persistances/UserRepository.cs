using KekAuth.Application.Presistances;
using KekAuth.Domain.Entities;

namespace KekAuth.Infrastructure.Persistances;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(user => user.Email == email);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}