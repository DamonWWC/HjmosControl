using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharpDemoPrism.ViewModels
{
   public  class BrowserDataContext: BindableBase
    {
        public BrowserDataContext()
        {
            
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { SetProperty(ref _Address, value); }
        }
    }
}
