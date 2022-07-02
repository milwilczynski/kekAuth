using KekAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KekAuth.Infrastructure.DBConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(r => r.Id);
        builder
            .Property(u => u.Id)
            .UseSerialColumn();
        builder
            .Property(u => u.RoleId)
            .HasColumnName("role_id");
        builder
            .Property(u => u.Login)
            .HasColumnName("login")
            .IsRequired();
        builder
            .HasIndex(u => u.Login)
            .IsUnique();
        builder
            .Property(u => u.UserToken)
            .IsRequired();
        builder
            .HasIndex(u => u.UserToken)
            .IsUnique();
        builder
            .Property(u => u.Password)
            .HasColumnName("password");
        builder
            .Property(u => u.Email)
            .HasColumnName("email");
        builder
            .HasIndex(u => u.Email)
            .IsUnique();
        builder
            .Property(u => u.FirstName)
            .HasColumnName("first_name");
        builder
            .Property(u => u.LastName)
            .HasColumnName("last_name");
        builder
            .Property(u => u.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("now()")
            .ValueGeneratedOnAddOrUpdate();
        builder
            .Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("now()")
            .ValueGeneratedOnAdd();
        builder
            .HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(user => user.RoleId);
    }
}