using Application._Common.Extensions;
using Domain.Domains.Roles.Enums;

namespace Application._Common.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(PermissionTypes name)
        : base($"Разрешение \"{name.GetDescription()}\" необходимо для выполнения данного действия")
    {
    }

    public ForbiddenException(ICollection<PermissionTypes> names)
        : base(
            $"Одно из разрешений из списка \"{string.Join(", ", names.Select(x => x.GetDescription()))}\" необходимо для выполнения данного действия")
    {
    }

    public ForbiddenException(string msg)
        : base(msg)
    {
    }
}