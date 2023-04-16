using Application._Common.Extensions;
using Domain.Domains.Roles.Entities;
using Domain.Domains.Roles.Enums;
using Domain.Domains.Users.Entities;
using Domain.Domains.Users.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public static class FinanceTrackerDataModelBuilderExtensions
{
    public static void SeedTestUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    Id = (long) WellKnownEmployeeIds.TestSuperAdminEmployee,

                    //SsoGuid = Guid.Parse("75AAA4FD-E365-45C4-BAE6-E8A8EA9A9ABC"),
                    Email = "sa@finance-tracker.ru",
                    FirstName = "Отто",
                    LastName = "Нормалвербраухер",
                },
                new User
                {
                    Id = (long) WellKnownEmployeeIds.TestEmployee1,
                    Email = "stierlitz@finance-tracker.ru",
                    FirstName = "Всеволод",
                    LastName = "Штирлиц",
                },
                new User
                {
                    Id = (long) WellKnownEmployeeIds.TestEmployee2,

                    Email = "curie@finance-tracker.ru",
                    FirstName = "Мария",
                    LastName = "Кюри",
                },
                new User
                {
                    Id = (long) WellKnownEmployeeIds.TestEmployee3,
                    Email = "einstein@finance-tracker.ru",
                    FirstName = "Альберт",
                    LastName = "Эйнштейн",
                },
                new User
                {
                    Id = (long) WellKnownEmployeeIds.TestEmployee4,
                    Email = "monroe@finance-tracker.ru",
                    FirstName = "Мерлин",
                    LastName = "Монро",
                });
    }

    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(new List<User>()
            {
                new()
                {
                    Id = (long) WellKnownEmployeeIds.FinanceTrackerPlatform,
                    Email = "set-some-email-later@finance-tracker.ru",
                    FirstName = "Link",
                    LastName = "One",
                }
            });
    }

    public static void SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .HasData(
                new Role
                {
                    Id = (long) PredefinedRoles.SuperAdmin,
                    Title = PredefinedRoles.SuperAdmin.ToString(),
                    Description = PredefinedRoles.SuperAdmin.GetDescription(),
                    Created = new DateTime(2023, 04, 16, 0,0,0,0, DateTimeKind.Utc),
                },
                new Role
                {
                    Id = (long) PredefinedRoles.RolesAdmin,
                    Title = PredefinedRoles.RolesAdmin.ToString(),
                    Description = PredefinedRoles.SuperAdmin.GetDescription(),
                    Created = new DateTime(2023, 04, 16, 0,0,0,0, DateTimeKind.Utc),
                }
            );
    }
}