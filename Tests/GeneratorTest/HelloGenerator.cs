using Microsoft.CodeAnalysis;

namespace GeneratorTest
{
    [Generator]
    public class HelloGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var code = @"namespace HelloGenerated
{
  public class HelloGenerator
  {
    public static void Test() => System.Console.WriteLine(""Hello Generator"");
  }
}";
            context.AddSource(nameof(HelloGenerator), code);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}