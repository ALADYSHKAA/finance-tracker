using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Domain.Common.DomainEnumExtensions.Attributes;

namespace TypeScriptDictsFromEnumsGenerator.EnumsGenerators;

public static class IdTitleDescEnumGenerator
{
    public static string GenerateIdTitleDescEnum(List<Type> typeList)
    {
        var template = new StringBuilder();

        foreach (var type in typeList)
        {
            template.AppendLine("export const " + type.Name +
                                "Dict: ReadOnlyDictionary<IdTitleDescEnum> = new ReadOnlyDictionary<IdTitleDescEnum> (");
            template.AppendLine("\t[");
            foreach (var fieldInfo in type.GetFields().Where((Func<FieldInfo, bool>) (w => w.IsLiteral)))
            {
                var name = fieldInfo.Name;
                var id = fieldInfo.GetRawConstantValue();
                var description =
                    !(Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is DescriptionAttribute
                        customAttribute)
                        ? ""
                        : customAttribute.Description;
                var extraAttribute =
                    !(Attribute.GetCustomAttribute(fieldInfo, typeof(EnumExtraDataAttribute)) is EnumExtraDataAttribute
                        extraDataAttribute)
                        ? ""
                        : extraDataAttribute.EnumExtraData;
                int? enumOrder =
                    !(Attribute.GetCustomAttribute(fieldInfo, typeof(EnumOrderAttribute)) is EnumOrderAttribute
                        orderAttribute)
                        ? null
                        : orderAttribute.Order;
                //new IdTitleDescEnum({{id : {id}, title : '{name}', description : '{description}'}})
                var extraData = new StringBuilder("");
                if (!string.IsNullOrWhiteSpace(description)) extraData.Append($", description: '{description}'");
                if (!string.IsNullOrWhiteSpace(extraAttribute)) extraData.Append($", extraData: '{extraAttribute}'");
                if (enumOrder is not null) extraData.Append($", enumOrder: {enumOrder}");

                var idTitleDtoText = $"new IdTitleDescEnum({{id: {id}, title: '{name}'{extraData}}})";
                template.AppendLine(
                    $"\t\t{{key : {id}, value : {idTitleDtoText}}},");
            }

            template.AppendLine("\t]");
            template.AppendLine(");");
            template.AppendLine();
        }

        return template.ToString();
    }
}