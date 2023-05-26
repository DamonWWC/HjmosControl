using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace CefSharpDemoPrism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            //Address = "https://www.baidu.com/";
            //Cookies = new List<Cookie>
            //{
            //    new Cookie("11","aa")
            //};

            List<strudent> one = new List<strudent>();
            one.Add(new strudent() { age = 2, name = "aaa" });
            one.Add(new strudent() { age = 1, name = "bbb" });
            one.Add(new strudent() { age = 3, name = "ddd" });
            one.Add(new strudent() { age = 55, name = "fff" });
            one.Add(new strudent() { age = 2, name = "ggg" });
            one.Add(new strudent() { age = 12, name = "eee" });
            List<strudent> two = new List<strudent>();
            two.Add(new strudent() { age = 11, name = "aaa" });
            two.Add(new strudent() { age = 22, name = "bbb" });
            two.Add(new strudent() { age = 33, name = "ddd" });
            two.Add(new strudent() { age = 55, name = "fff" });
            two.Add(new strudent() { age = 44, name = "ggg" });
            two.Add(new strudent() { age = 22, name = "eee" });
            two.Add(new strudent() { age = 22, name = "aaaaa" });
            var tt = one.Union(two).ToList();
            var res = from p in tt
                      group p by new { p.name } into g
                      select new { name = g.Key.name, accp = g.Sum(p => p.age)};


        }


        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { SetProperty(ref _Address, value); }
        }

        private List<Cookie> _Cookies;
        public List<Cookie> Cookies
        {
            get { return _Cookies; }
            set { SetProperty(ref _Cookies, value); }
        }

        //Address="https://www.baidu.com/"

        private object _BrowserDataContext;
        public object BrowserDataContext
        {
            get { return _BrowserDataContext; }
            set { SetProperty(ref _BrowserDataContext, value); }
        }
    }
    public class strudent
    {
        public decimal age { get; set; }
        public string name { get; set; }
    }
}
