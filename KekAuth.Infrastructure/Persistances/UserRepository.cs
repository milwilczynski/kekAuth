using KekAuth.Application.Presistances;
using KekAuth.Domain.Entities;
using KekAuth.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace KekAuth.Infrastructure.Persistances;

public class UserRepository : IUserRepository
{
    private readonly KekMainContext _context;

    public UserRepository(KekMainContext context)
    {
        _context = context;
    }

    public async Task<bool> Add(User user)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
        if (role != null)
        {
            user.Role = role;
            user.RoleId = role.Id;
        }

        var addResponse = await _context.Users.AddAsync(user);
        if (addResponse.State == EntityState.Added)
        {
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }


    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}