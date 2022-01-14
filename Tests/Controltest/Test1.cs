using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controltest
{
    internal class Test1 : Interface2
    {
        public Test1()
        {
        }

        public int Sum([Required] int a, [Range(1, 10)] int b)
        {
            var stackTrace = EnhancedStackTrace.Current();
            var aa = stackTrace.FirstOrDefault(u => u.GetMethod().DeclaringType.Namespace != typeof(Test1).Namespace);
            return a + b;
        }
    }
}