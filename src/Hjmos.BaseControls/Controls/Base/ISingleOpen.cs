using System;

namespace Hjmos.BaseControls.Controls
{
    public interface ISingleOpen:IDisposable
    {
        bool CanDispose { get; }
    }
}
