using Domain.Domains.Roles.Enums;

namespace Domain.Domains.Roles.Entities;

public class RolePermission
{
    public long RoleId { get; set; }
    public PermissionTypes PermissionId { get; set; }
    public Role Role { get; set; }
}