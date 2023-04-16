using Domain.Domains.Roles.Entities;
using Domain.Domains.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application._Common.Interfaces.Persistence;

public interface IFinanceTrackerContext
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<RolePermission> RolePermissions { get; set; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}