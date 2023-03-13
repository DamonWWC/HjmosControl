using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;

namespace Controltest
{
    public class WinformToWpfTest
    {
        public WinformToWpfTest()
        {
        }

        public int AddValue(int a)
        {
            int b = 0;
            if (a < 0)
            {
                b = 1;
            }
            if (a == 0)
            {
                b = 0;
            }
            else
            {
                b = 2;
            }
            return b;
        }
    }
}