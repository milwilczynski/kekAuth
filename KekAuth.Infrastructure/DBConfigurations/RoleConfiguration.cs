using KekAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KekAuth.Infrastructure.DBConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasKey(r => r.Id);
        builder
            .Property(u => u.Id)
            .UseSerialColumn();
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .HasIndex(r => r.Name)
            .IsUnique();
        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(100);
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
            .HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);
    }
}