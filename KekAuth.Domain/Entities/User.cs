namespace KekAuth.Domain.Entities;

public class User : BaseEntity
{
    public Guid UserToken { get; set; } = Guid.NewGuid();
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
}