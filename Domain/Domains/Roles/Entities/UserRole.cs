using Domain.Domains.Users.Entities;

namespace Domain.Domains.Roles.Entities;

public class UserRole
{
    public long? RoleId { get; set; }
    public Role Role { get; set; }

    public long? UserId { get; set; }
    public User User { get; set; }
}