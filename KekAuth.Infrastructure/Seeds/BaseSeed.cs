using Isopoh.Cryptography.Argon2;
using KekAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KekAuth.Infrastructure.Seeds;

public static class BaseSeed
{
    public static void GenerateSeed(this ModelBuilder modelBuilder)
    {
        var now = DateTime.Now;
        var date = new DateTime(now.Year, now.Month, now.Day,
            now.Hour, now.Minute, now.Second, now.Millisecond);
        modelBuilder.Entity<Role>()
            .HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "Most valuable user",
                    CreatedAt = date,
                    UpdatedAt = date
                },
                new Role
                {
                    Id = 2,
                    Name = "User",
                    Description = "Normal user",
                    CreatedAt = date,
                    UpdatedAt = date
                }
            );
        var password = Argon2.Hash("admin");
        modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    Id = 1,
                    Login = "admin",
                    Password = password,
                    RoleId = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "milwilczynski@gmail.com",
                    UserToken = Guid.NewGuid(),
                    CreatedAt = date,
                    UpdatedAt = date
                }
            );
    }
}