using HarmonyLib;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace HarmonyDemo
{
    internal class Program
    {

        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_MOUSE_INPUT = 0x0010;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle,out uint dwMode);
        static void Main(string[] args)
        {
            //var student = new Student();
            //Console.WriteLine(student.GetDetail("wangwenchao"));

            //var harmony = new Harmony("harmonydemo");
            //harmony.PatchAll(Assembly.GetExecutingAssembly());

            //Console.WriteLine(student.GetDetail("wangwen"));

            //Example ex = new Example();
            //ex.DoSomething();




            //string input = "fcown dog";
            //string text = "The quick brown fox jumps over the lazy dog.";

            //// 根据输入内容构造正则表达式模式
            //string pattern = string.Join(".*?", input.Split(' '));

            //// 使用正则表达式进行匹配
            //var matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

            //// 使用 LINQ 过滤匹配结果
            //var results = from Match match in matches
            //              select new
            //              {
            //                  Index = match.Index,
            //                  Length = match.Length,
            //                  Value = match.Value
            //              };

            //// 输出匹配结果
            //foreach (var result in results)
            //{
            //    Console.WriteLine($"Index: {result.Index}, Length: {result.Length}, Value: {result.Value}");
            //}



            IntPtr handle = GetStdHandle(STD_INPUT_HANDLE);
            uint mode;
            if (!GetConsoleMode(handle, out mode))
            {
                Console.WriteLine("Error: " + Marshal.GetLastWin32Error());
                return;
            }
            mode &= ~ENABLE_MOUSE_INPUT;
            if (!SetConsoleMode(handle, mode))
            {
                Console.WriteLine("Error: " + Marshal.GetLastWin32Error());
                return;
            }




            Console.ReadLine();
        }
    }




    [AttributeUsage(AttributeTargets.Method)]
    public class LogAttribute : Attribute
    {
        public void OnEntry()
        {
            Console.WriteLine("Method entered.");
        }

        public void OnExit()
        {
            Console.WriteLine("Method exited.");
        }
    }

    public class Example
    {
        [Log]
        public void DoSomething()
        {
            Console.WriteLine("Doing something...");
        }
    }





    public class Student
    {
        public string GetDetail(string name)
        {
            return $"我是{name}";
        }
    }

    [HarmonyPatch(typeof(Student))]
    [HarmonyPatch(nameof(Student.GetDetail))]
    //[HarmonyPatch(typeof(String))]
    public class HookStudent
    {
      
        public static void Postfix(ref string __result)
        {
            Console.WriteLine("Postfix");
        }
        public static void Finalizer()
        {
            Console.WriteLine("Finalizer");
        }
        public static bool Prefix(ref string name, ref string __result)
        {
            if ("wang".Equals(name))
            {
                __result = "这是我的姓";
                return false;
            }

            if (!"wangwenchao".Equals(name))
            {
                name = "不是我的名字";
            }
            FileLog.Debug("11");

            return true;
        }

    }
}