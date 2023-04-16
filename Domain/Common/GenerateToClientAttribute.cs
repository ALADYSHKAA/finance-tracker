using System;

namespace Domain.Common
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class GenerateToClientAttribute : Attribute
    {
    }
}