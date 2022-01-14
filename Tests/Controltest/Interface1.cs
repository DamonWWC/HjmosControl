using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controltest
{
    public interface Interface1
    {
        void Call();
    }

    public interface Interface2
    {
        int Sum([Required] int a, [Range(1, 10)] int b);
    }
}