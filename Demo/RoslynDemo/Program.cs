using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace RoslynDemo
{
    internal class Program
    {

        // <SnippetDeclareSampleCode>
        private const string sampleCode =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}";
        static void Main(string[] args)
        {

            Type t = Intercept.BuildType<IUser>();
            var method = t.GetMethod("getName");
            object obj = Activator.CreateInstance(t);
            var result = method.Invoke(obj, new object[] { "ÕÅÈý" }).ToString();
            Console.WriteLine(result);




            //// <SnippetDeclareTestCompilation>
            //var test = CreateTestCompilation();
            //// </SnippetDeclareTestCompilation>

            //// <SnippetIterateTrees>
            //foreach (SyntaxTree sourceTree in test.SyntaxTrees)
            //{
            //    var model = test.GetSemanticModel(sourceTree);

            //    var rewriter = new TypeInferenceRewriter(model);

            //    // <SnippetTransformTrees>
            //    var newSource = rewriter.Visit(sourceTree.GetRoot());

            //    if (newSource != sourceTree.GetRoot())
            //    {
            //        File.WriteAllText(sourceTree.FilePath, newSource.ToFullString());
            //    }
            //    // </SnippetTransformTrees>
            //}
            //// </SnippetIterateTrees>
        }

        private static Compilation CreateTestCompilation()
        {
            // <SnippetCreateTestCompilation>
            var programPath = @"..\..\..\Program.cs";
            var programText = File.ReadAllText(programPath);
            var programTree =
                           CSharpSyntaxTree.ParseText(programText)
                                           .WithFilePath(programPath);

            var rewriterPath = @"..\..\..\TypeInferenceRewriter.cs";
            var rewriterText = File.ReadAllText(rewriterPath);
            var rewriterTree =
                           CSharpSyntaxTree.ParseText(rewriterText)
                                           .WithFilePath(rewriterPath);

            SyntaxTree[] sourceTrees = { programTree, rewriterTree };

            MetadataReference mscorlib =
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            MetadataReference codeAnalysis =
                    MetadataReference.CreateFromFile(typeof(SyntaxTree).Assembly.Location);
            MetadataReference csharpCodeAnalysis =
                    MetadataReference.CreateFromFile(typeof(CSharpSyntaxTree).Assembly.Location);

            MetadataReference[] references = { mscorlib, codeAnalysis, csharpCodeAnalysis };

            return CSharpCompilation.Create("TransformationCS",
                sourceTrees,
                references,
                new CSharpCompilationOptions(OutputKind.ConsoleApplication));
            // </SnippetCreateTestCompilation>
        }
    }
}