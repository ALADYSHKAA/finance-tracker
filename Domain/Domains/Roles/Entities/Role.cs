using System.Collections.Generic;
using Domain.Common;
using Domain.Domains.Roles.Enums;

namespace Domain.Domains.Roles.Entities;

public class Role : AudibleEntity
{
    public Role()
    {
        RolePermissions = new HashSet<RolePermission>();
    }

    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}