using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CefSharpDemo
{
    public class HjmosBrowser : ChromiumWebBrowser
    {

        public HjmosBrowser()
        {
           
        }

        static HjmosBrowser()
        {           
            CookieProperty= DependencyProperty.Register("Cookie", typeof(Dictionary<string, string>), typeof(HjmosBrowser), new PropertyMetadata(default(Dictionary<string, string>), new PropertyChangedCallback(CookieChanged)));

            Init();
        }






        public Dictionary<string,string> Cookie
        {
            get { return (Dictionary<string,string>)GetValue(CookieProperty); }
            set { SetValue(CookieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cookie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CookieProperty;
            

        private static void CookieChanged(DependencyObject o,DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null && args.NewValue is Dictionary<string, string> value)
            {
                var cookieManager = Cef.GetGlobalCookieManager();
                if (o is HjmosBrowser browser && browser != null)
                {
                    var uri = new Uri(browser.Address);
                    foreach (var item in value)
                    {
                        cookieManager.SetCookieAsync(browser.Address, new CefSharp.Cookie()
                        {
                            //Name=item.
                        });
                     
                    }
                }                
            }
        }










        private static readonly object _lock = new object();
        public static  void Init()
        {
            if (Cef.IsInitialized)
            {
                return;
            }
            lock (_lock)
            {
                if (Cef.IsInitialized)
                {
                    return;
                }

                var settings = new CefSettings();

                //设置语言
                settings.Locale = "zh-CN";

                //忽略https证书的问题
                settings.CefCommandLineArgs.Add("--ignore-urlfetcher-cert-requests", "1");
                settings.CefCommandLineArgs.Add("--ignore-certificate-errors", "1");

                //禁止启用同源策略安全限制，禁止后不会出现跨域问题
                settings.CefCommandLineArgs.Add("--disable-web-security", "1");

                //取消cefsharp打印浏览器的输出日志
                settings.LogSeverity = LogSeverity.Disable;
                //禁用gpu,防止浏览器闪烁
                settings.CefCommandLineArgs.Add("disable-gpu", "1");

                //去掉代理，增加加载网页速度
                settings.CefCommandLineArgs.Add("proxy-auto-detect", "0");
                settings.CefCommandLineArgs.Add("no-proxy-serve", "1");

                // 在运行时根据系统类型（32/64位），设置BrowserSubProcessPath
                //settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Environment.Is64BitProcess ? "x64" : "x86", "CefSharp.BrowserSubprocess.exe");

                var directory = Directory.GetCurrentDirectory();
                var cachePath = $@"{directory}\cache";
                if (!Directory.Exists(cachePath))
                {
                    Directory.CreateDirectory(cachePath);
                }
                settings.CachePath = cachePath;

                CefSharpSettings.SubprocessExitIfParentProcessClosed = true;

                // 确保设置performDependencyCheck为false
                Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
            }
        }
    }
}
