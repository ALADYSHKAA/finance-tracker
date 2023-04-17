using System.ComponentModel;

namespace Domain.Domains.Roles.Enums;

public enum PredefinedRoles : long
{
    [Description("Супер Админ")] SuperAdmin = -1,
    [Description("Супер Админ")] RolesAdmin = -2
}