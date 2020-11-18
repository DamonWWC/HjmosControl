using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjmos.BaseControls.Interactivity
{
    public interface IEventArgsConverter
    {
        object Convert(object value, object parameter);
    }
}
