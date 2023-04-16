using System.ComponentModel;
using Domain.Common;

namespace Domain.Domains.Roles.Enums;

[GenerateToClient]
public enum PermissionTypes : long
{
    // Superadmin
    [Description("Доступ суперадмина")] 
    SuperAdminAccess = 0x1000_0000,

    // Админка
    [Description("Доступ в админку")]
    AdminAccess = 0x1000_0001,

    
    // Роли
    [Description("Доступ в админку ролей")] 
    RolesAdminAccess = 0x2000_0001,  
    [Description("Создание роли")] 
    RoleCreate = 0x2000_0002,
    [Description("Удаление роли")] 
    RoleRemove = 0x2000_0003,


}
