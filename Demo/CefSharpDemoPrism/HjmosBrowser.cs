using CefSharp.Wpf;
using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Text;
using System.Linq;

namespace CefSharpDemoPrism
{
    public class HjmosBrowser : ChromiumWebBrowser
    {

        public HjmosBrowser()
        {
            KeyboardHandler = new KeyBoardHander();     
            DownloadHandler =CefSharp.Fluent.DownloadHandler.AskUser();
        }

      

        static HjmosBrowser()
        {
            CookieProperty = DependencyProperty.Register("Cookie", typeof(IEnumerable<Cookie>), typeof(HjmosBrowser), new PropertyMetadata(default(Dictionary<string, string>), new PropertyChangedCallback(CookieChanged)));
            JsObjectRespositoryProperty =DependencyProperty.Register("JsObjectRespository", typeof(IEnumerable<JsObjectRespository>), typeof(HjmosBrowser), new PropertyMetadata(default,RespositoryChanged));
            MsgToH5Property =DependencyProperty.Register("MsgToH5", typeof(string), typeof(HjmosBrowser), new PropertyMetadata(default, new PropertyChangedCallback(SendMsgToH5)));

            Init();
        }

       
        public IEnumerable<Cookie> Cookie
        {
            get { return (IEnumerable<Cookie>)GetValue(CookieProperty); }
            set { SetValue(CookieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cookie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CookieProperty;


        private static async void CookieChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is IEnumerable<Cookie> value &&value != null)
            {
                var cookieManager = Cef.GetGlobalCookieManager();
                if (o is HjmosBrowser browser && browser != null)
                {
                    var uri = new Uri(browser.Address);
                    foreach (var item in value)
                    {
                        var re = await cookieManager.SetCookieAsync(browser.Address, new CefSharp.Cookie()
                        {
                            Name = item.Name,
                            Value = item.Value,
                            Domain=uri.Host,
                            Path="/",
                            Expires=DateTime.MinValue
                        });

                    }
                }
            }
        }


        private static void RespositoryChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if(args.NewValue is IEnumerable<JsObjectRespository> respository && respository!=null)
            {
                if(o is HjmosBrowser browser &&browser !=null)
                {
                    browser.InitJavascriptObjects(respository);
                }
            }
        }


        public IEnumerable<JsObjectRespository> JsObjectRespository
        {
            get { return (IEnumerable<JsObjectRespository>)GetValue(JsObjectRespositoryProperty); }
            set { SetValue(JsObjectRespositoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for JsObjectRespository.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JsObjectRespositoryProperty;




        /// <summary>
        /// 注册Javascript对象集合
        /// </summary>
        /// <param name="oldJavascriptObjects"></param>
        /// <param name="newJavascriptObjects"></param>
        private void InitJavascriptObjects(IEnumerable<JsObjectRespository> newJavascriptObjects)
        {             
            JavascriptObjectRepository.UnRegisterAll();

            if (newJavascriptObjects.Count() > 0)
            {
                foreach (var newJavascriptObject in newJavascriptObjects)
                {
#if NET472

                    JavascriptObjectRepository.Register(newJavascriptObject.Name, newJavascriptObject.ObjectToBind, true, BindingOptions.DefaultBinder);
#endif
#if NET6_0
                    JavascriptObjectRepository.Register(newJavascriptObject.Name, newJavascriptObject.ObjectToBind, BindingOptions.DefaultBinder);
#endif
                }
            }
        }


        public string MsgToH5
        {
            get { return (string)GetValue(MsgToH5Property); }
            set { SetValue(MsgToH5Property, value); }
        }

        // Using a DependencyProperty as the backing store for MsgToH5.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MsgToH5Property;

       
        private static void SendMsgToH5(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is string msg && msg != null)
            {
                if (o is HjmosBrowser browser && browser != null)
                {
                    browser.ExecuteScriptAsync(msg);
                }
            }           
        }


        #region init

        private static readonly object _lock = new object();
        public static void Init()
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
        #endregion
    }

    internal class KeyBoardHander : IKeyboardHandler
    {
        public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            return false;
        }

        public bool OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            //按键事件会触发2次KeyType类型：RawKeyDown、KeyUp，为避免触发两次，需进行判断
            if (type != KeyType.KeyUp)
            {
                return false;
            }

            const int VK_F5 = 0x74;
            const int VK_F9 = 0x78;
            const int VK_F12 = 0x7B;

            if (windowsKeyCode == VK_F5)
            {
                browser.Reload();
            }
            else if (windowsKeyCode == VK_F12)
            {
                browser.ShowDevTools();
            }
            else if (windowsKeyCode == VK_F9)
            {
            }
            return false;
        }
    }


    public class JavascriptCallbackBoundObject
    {
        private IJavascriptCallback callback;
        private IWebBrowser webBrowser;

        public JavascriptCallbackBoundObject(IWebBrowser webBrowser)
        {
            this.webBrowser = webBrowser;
        }

        [JavascriptIgnore]
        public void RunCallback()
        {
            if (callback != null && callback.CanExecute)
            {
                callback.ExecuteAsync("Hello from c#").ContinueWith(t =>
                {
                    var javascriptResponse = t.Result;
                    var builder = new StringBuilder();

                    if (javascriptResponse.Success)
                    {
                        builder.AppendLine("Response From Callback: " + javascriptResponse.Result.ToString());
                    }
                    else
                    {
                        var mainFrame = webBrowser.GetMainFrame();

                        builder.AppendLine("Javascript callback failed with " + javascriptResponse.Message);
                        builder.AppendLine("<br/>");
                        builder.AppendLine("Current MainFrame Id:" + mainFrame.Identifier);
                    }

                    webBrowser.LoadHtml(builder.ToString());
                });
            }
            else
            {
                webBrowser.LoadHtml("Callback CanExecute is now false");
            }
        }

        public void SetCallBack(IJavascriptCallback callback)
        {
            this.callback = callback;
        }
    }

    public struct JsObjectRespository
    {
        public JsObjectRespository(string name, object objectToBind)
        {
            this.Name = name;
            this.ObjectToBind = objectToBind;
        }
        /// <summary>
        /// 绑定对象名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 绑定对象
        /// </summary>
        public object ObjectToBind { get; set; }
    }
    public struct Cookie
    {
        public Cookie(string name,string value)
        {
            this.Name = name;
            this.Value = value;
        }
        /// <summary>
        /// cookie名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// cookie值
        /// </summary>
        public string Value { get; set; }
    }

}
