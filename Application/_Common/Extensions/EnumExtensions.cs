using System.ComponentModel;
using Domain.Common.DomainEnumExtensions.Attributes;

namespace Application._Common.Extensions;

public static class EnumExtensions
{
    public static T GetValueFromDescription<T>(this string description)
    {
        var type = typeof(T);
        if (!type.IsEnum) throw new InvalidOperationException();
        foreach (var field in type.GetFields())
        {
            if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description)
                    return (T) field.GetValue(null);
            }
            else
            {
                if (field.Name == description)
                    return (T) field.GetValue(null);
            }
        }

        //throw new ArgumentException("Not found.", "description");
        return default;
    }

    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return null;

        return !(Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute
            )
            ? value.ToString()
            : attribute.Description;
    }


    public static string GetOrder(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return null;
        return !(Attribute.GetCustomAttribute(field, typeof(EnumOrderAttribute)) is EnumOrderAttribute attribute
            )
            ? value.ToString()
            : attribute.Order.ToString();
    }
}