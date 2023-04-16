using System.Collections.Generic;
using System.Collections.Generic;
using Domain.Domains.Roles.Enums;

namespace Persistence
{
    public static class SeedData
    {
        public static IDictionary<PredefinedRoles, HashSet<PermissionTypes>> RolesPermissions =>
            new Dictionary<PredefinedRoles, HashSet<PermissionTypes>>
            {
                {
                    PredefinedRoles.SuperAdmin, new HashSet<PermissionTypes>
                    {
                        PermissionTypes.SuperAdminAccess,
                    }
                },
            };
    }
}