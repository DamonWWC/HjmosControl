using CefSharpDemoPrism.Views;
using Prism.Ioc;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace CefSharpDemoPrism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
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
