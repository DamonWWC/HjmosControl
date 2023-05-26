using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace CefSharpDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

            AppDomain.CurrentDomain.AssemblyResolve += (s, a) =>
            {
                AssemblyName assemblyName = new AssemblyName(a.Name);

                // 尝试从x86或x64子目录加载缺少的程序集
                // 在使用AnyCPU运行时，CefSharp要求加载非托管依赖项
                if (assemblyName.Name.StartsWith("CefSharp"))
                {
                    string name = assemblyName.Name + ".dll";
                    string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Environment.Is64BitProcess ? "x64" : "x86", name);

                    return File.Exists(archSpecificPath) ? Assembly.LoadFile(archSpecificPath) : null;
                }
                return null;
            };

        }
    }
}
