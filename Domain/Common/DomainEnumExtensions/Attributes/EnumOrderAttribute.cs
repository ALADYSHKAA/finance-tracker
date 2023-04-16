using System;

namespace Domain.Common.DomainEnumExtensions.Attributes;

public class EnumOrderAttribute : Attribute
{

    public readonly int Order;

    public EnumOrderAttribute(int order)
    {
        Order = order;
    }
}