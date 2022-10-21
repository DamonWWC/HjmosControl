

using GeneratorTest;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
        static void SomeMethodIHave()
        {
            HelloWorldGenerator.HelloWorld.SayHello(); 
        }
    }
}