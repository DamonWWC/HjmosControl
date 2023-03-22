using HarmonyLib;
using System.Reflection;

namespace HarmonyDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var student = new Student();
            Console.WriteLine(student.GetDetail("wangwenchao"));

            var harmony = new Harmony("harmonydemo");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Console.WriteLine(student.GetDetail("wangwen"));


            Console.ReadLine();
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
        public static bool Prefix(ref string name,ref string __result)
        {
            if("wang".Equals(name))
            {
                __result = "这是我的姓";
                return false;
            }

            if(!"wangwenchao".Equals(name))
            {
                name = "不是我的名字";
            }
            FileLog.Debug("11");

            return true;
        }

        public static void Postfix(ref string __result)
        {
            Console.WriteLine("Postfix");
        }
        public static void Finalizer()
        {
            Console.WriteLine("Finalizer");
        }
    }
}