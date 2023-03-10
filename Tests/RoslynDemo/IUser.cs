using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoslynDemo
{
    internal interface IUser
    {
        string getName(string name);
    }
    public class Intercept
    {
        public static void Before(string name)
        {
            Console.WriteLine($"拦截成功，参数：{name}");
        }


        /// <summary>
        /// 生成静态脚本
        /// </summary>
        /// <typeparam name="Tinteface"></typeparam>
        /// <returns></returns>
        public static string GeneratorScript<Tinteface>(string typeName)
        {
            var t = typeof(Tinteface);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("using System;");
            stringBuilder.Append($"using {t.Namespace};");
            stringBuilder.Append($"namespace {typeName}_namespace");
            stringBuilder.Append("{");
            stringBuilder.Append($"public class {typeName}:{t.Name}");
            stringBuilder.Append(" {");
            MethodInfo[] targetMethods = t.GetMethods();
            foreach (MethodInfo targetMethod in targetMethods)
            {
                if (targetMethod.IsPublic)
                {
                    var returnType = targetMethod.ReturnType;
                    var parameters = targetMethod.GetParameters();
                    string pStr = string.Empty;
                    List<string> parametersName = new List<string>();
                    foreach (ParameterInfo parameterInfo in parameters)
                    {
                        var pType = parameterInfo.ParameterType;
                        pStr += $"{pType.Name} _{pType.Name},";
                        parametersName.Add($"_{pType.Name}");
                    }

                    stringBuilder.Append($"public {returnType.Name} {targetMethod.Name}({pStr.TrimEnd(',')})");
                    stringBuilder.Append(" {");
                    foreach (var pName in parametersName)
                    {
                        stringBuilder.Append($"Intercept.Before({pName});");
                    }
                    stringBuilder.Append($"return \"执行成功。\";");
                    stringBuilder.Append(" }");
                }
            }
            stringBuilder.Append(" }");
            stringBuilder.Append(" }");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 构建类对象
        /// </summary>
        /// <typeparam name="Tinteface"></typeparam>
        /// <returns></returns>
        public static Type BuildType<Tinteface>()
        {
            var typeName = "_" + typeof(Tinteface).Name;
            var text = GeneratorScript<Tinteface>(typeName);

            // 将代码解析成语法树
            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(text);

            var objRefe = MetadataReference.CreateFromFile(typeof(Object).Assembly.Location);
            var consoleRefe = MetadataReference.CreateFromFile(typeof(IUser).Assembly.Location);

            var compilation = CSharpCompilation.Create(
                syntaxTrees: new[] { tree },
                assemblyName: $"assembly{typeName}.dll",
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
                references: AppDomain.CurrentDomain.GetAssemblies().Select(x => MetadataReference.CreateFromFile(x.Location)));

            Assembly compiledAssembly;
            using (var stream = new MemoryStream())
            {
                // 检测脚本代码是否有误
                var compileResult = compilation.Emit(stream);
                compiledAssembly = Assembly.Load(stream.GetBuffer());
            }
            return compiledAssembly.GetTypes().FirstOrDefault(c => c.Name == typeName);
        }
    }

}
