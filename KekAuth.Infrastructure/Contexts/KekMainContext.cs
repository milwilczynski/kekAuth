using KekAuth.Application.Interfaces;
using KekAuth.Domain.Entities;
using KekAuth.Infrastructure.DBConfigurations;
using KekAuth.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;

namespace KekAuth.Infrastructure.Contexts;

public class KekMainContext : DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public KekMainContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
        ;
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserConfiguration().Configure(modelBuilder.Entity<User>());
        new RoleConfiguration().Configure(modelBuilder.Entity<Role>());

        modelBuilder.GenerateSeed();
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        var now = _dateTimeProvider.Now;
        var date = new DateTime(now.Year, now.Month, now.Day,
            now.Hour, now.Minute, now.Second, now.Millisecond);
        foreach (var entityEntry in entries)
        {
            ((BaseEntity) entityEntry.Entity).UpdatedAt = date;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity) entityEntry.Entity).CreatedAt = date;
            }
        }

        return base.SaveChanges();
    }
}