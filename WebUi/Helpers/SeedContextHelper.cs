using Domain.Domains.Roles.Entities;
using Domain.Domains.Roles.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebUi.Helpers;

public static class SeedContextHelper
{
    public static async Task Seed(FinanceTrackerContext context)
    {
        var roles = await context.Roles.OrderBy(x => x.Id).ToListAsync();
        var allRolePermissions = await context.RolePermissions.ToListAsync();
        foreach (var role in roles)
        {
            var currentRolePermissions = allRolePermissions
                .Where(x => x.RoleId == role.Id);
                
            var id = (PredefinedRoles) role.Id;
            SeedData.RolesPermissions.TryGetValue(id, out var predefinedPermissions);
            if (predefinedPermissions is not null)
            {
                var rolesPermissions = predefinedPermissions.Select(x => new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = x
                    }).Concat(currentRolePermissions)
                    .DistinctBy(x => x.PermissionId)
                    .ToList();
                    
                role.RolePermissions = rolesPermissions;
            }
        }
        await context.SaveChangesAsync(CancellationToken.None);
    }
}