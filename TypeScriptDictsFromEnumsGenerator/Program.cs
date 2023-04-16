using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Domain.Common;
using NSwag;
using NSwag.CodeGeneration.OperationNameGenerators;
using NSwag.CodeGeneration.TypeScript;
using TypeScriptDictsFromEnumsGenerator.EnumsGenerators;
using TypeScriptDictsFromEnumsGenerator.Resources;

namespace TypeScriptDictsFromEnumsGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            {
                var outputFilePath = args.Length == 1 ? args[0] : "enumDicts.ts";

                var assemblyList = new List<Assembly>();
                var typeList = new List<Type>();
                var appAssembly = Assembly.Load("Application");
                assemblyList.Add(appAssembly);
                var domainAssembly = Assembly.Load("Domain");
                assemblyList.Add(domainAssembly);
                foreach (var assembly in assemblyList)
                {
                    var collection = assembly.GetTypes().Where(t => t.IsEnum && t.GetCustomAttribute<GenerateToClientAttribute>() != null);
                    typeList.AddRange(collection);
                }
         
                var template = new StringBuilder();

                template.Append(CodeSnippets.TypeScriptSnippets["DeclaredTypes"]);
                template.Append(IdTitleDescEnumGenerator.GenerateIdTitleDescEnum(typeList));

                using var streamWriter = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputFilePath));
                streamWriter.Write(template.ToString());
            }
        }
    }
}
