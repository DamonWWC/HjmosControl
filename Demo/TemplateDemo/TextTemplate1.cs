using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkTo.Test.ConsoleT4
{
    class Hello
    {
        /// <summary>
        /// Hello类Show()方法
        /// </summary>
        public static void Show()
        {
            Console.WriteLine("Hello");
        }
    }
    
    class World
    {
        /// <summary>
        /// World类Show()方法
        /// </summary>
        public static void Show()
        {
            Console.WriteLine("World");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            //Hello.Show();方法调用
            Hello.Show();
            //World.Show();方法调用
            World.Show();

            Console.Read();
        }
    }
}
