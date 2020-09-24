using Hjmos.BaseControls.Tools.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjmos.BaseControls.Data
{
    internal class MouseHookEventArgs : EventArgs
    {
        public MouseHookMessageType MessageType { get; set; }
        public InteropValues.POINT Point { get; set; }
    }
}
