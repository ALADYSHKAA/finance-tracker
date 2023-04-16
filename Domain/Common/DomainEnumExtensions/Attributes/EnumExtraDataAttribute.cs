using System;

namespace Domain.Common.DomainEnumExtensions.Attributes;

public class EnumExtraDataAttribute : Attribute
{

    public readonly string EnumExtraData;

    public EnumExtraDataAttribute(string enumExtraData)
    {
        EnumExtraData = enumExtraData;
    }
}