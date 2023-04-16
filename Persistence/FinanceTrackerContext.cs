using System.Reflection;
using Application._Common.Extensions;
using Application._Common.Interfaces.Infrastructure.Services;
using Application._Common.Interfaces.Persistence;
using Domain.Common;
using Domain.Domains.Roles.Entities;
using Domain.Domains.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Persistence;

public partial class FinanceTrackerContext : DbContext, IFinanceTrackerContext
{
    private readonly IHostEnvironment _env;
    private readonly ILogger<FinanceTrackerContext> _logger;
    private readonly IUserIdService _userIdService;

    public FinanceTrackerContext(DbContextOptions<FinanceTrackerContext> options,
                                 IHostEnvironment environment,
                                 IUserIdService userIdService,
                                 ILogger<FinanceTrackerContext> logger) :
        base(options)
    {
        _env = environment;
        _logger = logger;
        _userIdService = userIdService;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    public void DetachAllEntities()
    {
        var changedEntriesCopy = ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .ToList();

        foreach (var entry in changedEntriesCopy) entry.State = EntityState.Detached;
    }

    public async Task<int> SaveChangesDefaultAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var userId = _userIdService.GetImposerId() ?? _userIdService.GetUserId();

        var ts = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AudibleEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedById ??= userId;
                    entry.Entity.UpdatedById = userId;
                    entry.Entity.Created = entry.Entity.Created != default ? entry.Entity.Created : ts;
                    entry.Entity.Updated = ts;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedById = userId;
                    entry.Entity.Updated = ts;
                    break;
                case EntityState.Deleted:
                    entry.Entity.UpdatedById = userId;
                    entry.Entity.Updated = ts;
                    entry.Entity.Deleted = ts;
                    entry.State = EntityState.Modified;
                    break;
                case EntityState.Unchanged:
                    continue;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IAudibleDates>()
                     .Where(x => x.Entity is not AudibleEntity _))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = entry.Entity.Created != default ? entry.Entity.Created : ts;
                    entry.Entity.Updated = ts;
                    break;
                case EntityState.Modified:
                    entry.Entity.Updated = ts;
                    break;
                case EntityState.Deleted:
                    entry.Entity.Updated = ts;
                    entry.State = EntityState.Modified;
                    break;
                case EntityState.Unchanged:
                    continue;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    /*public async Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken)
    {
        return await Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(AudibleEntity).IsAssignableFrom(entityType.ClrType))
                modelBuilder.SetSoftDeleteFilter(entityType.ClrType);
        }

        //Вынес настройки в папку конфиграции.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(FinanceTrackerContext)));

        modelBuilder.SeedRoles();
        modelBuilder.SeedUsers();

        if (!_env.IsProduction()) modelBuilder.SeedTestUsers();
    }
}