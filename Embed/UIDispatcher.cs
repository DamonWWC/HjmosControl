using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Embed
{
    /// <summary>
    /// 创建新UI线程
    /// </summary>
    public static class UIDispatcher
    {
        public static DispatcherAsyncOperation<Dispatcher> RunNewAsync(string name = null)
        {
            var awaiter = DispatcherAsyncOperation<Dispatcher>.Create(out var reportResult);

            var originDispatcher = Dispatcher.CurrentDispatcher;

            var thread = new Thread(() =>
            {
                try
                {
                    var dispatcher = Dispatcher.CurrentDispatcher;
                    SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(dispatcher));
                }
                catch (Exception ex)
                {
                    reportResult(null, ex);
                }

                try
                {
                    Dispatcher.Run();
                }
                catch (Exception ex)
                {
                    originDispatcher.InvokeAsync(() => ExceptionDispatchInfo.Capture(ex).Throw());
                }
            })
            {
                Name = name ?? "BackgroundUI",
                IsBackground = true,
            };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return awaiter;
        }

        public static Dispatcher RunNew(string name = null)
        {
            var resetEvent = new AutoResetEvent(false);

            var originDispatcher = Dispatcher.CurrentDispatcher;

            Exception innerException = null;
            Dispatcher dispatcher = null;

            var thread = new Thread(() =>
            {
                try
                {
                    dispatcher = Dispatcher.CurrentDispatcher;
                    SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(dispatcher));
                }
                catch (Exception ex)
                {
                    innerException = ex;

                }
                finally
                {
                    resetEvent.Set();
                }

                try
                {
                    Dispatcher.Run();
                }
                catch (Exception ex)
                {
                    originDispatcher.InvokeAsync(() => ExceptionDispatchInfo.Capture(ex).Throw());
                }
            })
            {
                Name = name ?? "BackgroundUI",
                IsBackground = true,
            };

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            resetEvent.WaitOne();
            resetEvent.Dispose();
            resetEvent = null;
            if (innerException != null)
            {
                ExceptionDispatchInfo.Capture(innerException).Throw();
            }
            return dispatcher;
        }
    }
}
