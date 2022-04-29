using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Controltest
{
    internal class Test1 : Interface1
    {
        public Test1()
        {
        }

      

       

        public void Call(int b, int a = 1)
        {
            
        }
        public void Test<T>(T wi=null)where T:Window, new()
        {
            
        }
        //public int Sum([Required] int a, [Range(1, 10)] int b)
        //{
        //    var stackTrace = EnhancedStackTrace.Current();
        //    var aa = stackTrace.FirstOrDefault(u => u.GetMethod().DeclaringType.Namespace != typeof(Test1).Namespace);
        //    return a + b;
        //}
    }
}