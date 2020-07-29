using System;

namespace HjmosControl.Controls
{
    public interface ISingleOpen:IDisposable
    {
        bool CanDispose { get; }
    }
}
