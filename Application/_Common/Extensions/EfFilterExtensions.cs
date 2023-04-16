using System.Reflection;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Application._Common.Extensions;

public static class EfFilterExtensions
{
    private static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(EfFilterExtensions)
        .GetMethods(BindingFlags.Public | BindingFlags.Static)
        .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

    public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
    {
        SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
            .Invoke(null, new object[] {modelBuilder});
    }

    public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
        where TEntity : AudibleEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(x => x.Deleted == null);
        //modelBuilder.Entity<Department>().HasQueryFilter(x => x.HiddenFromPortal == false);
    }
}