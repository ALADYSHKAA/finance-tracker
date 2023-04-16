using System;
using System.Collections.Generic;
using System.Linq;
using Application._Common.Extensions;

namespace Application._Common.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(Domain.Domains.Roles.Enums.PermissionTypes name)
        : base($"Разрешение \"{name.GetDescription()}\" необходимо для выполнения данного действия")
    {
    }

    public ForbiddenException(ICollection<Domain.Domains.Roles.Enums.PermissionTypes> names)
        : base(
            $"Одно из разрешений из списка \"{string.Join(", ", names.Select(x => x.GetDescription()))}\" необходимо для выполнения данного действия")
    {
    }

    public ForbiddenException(string msg)
        : base(msg)
    {
    }
}