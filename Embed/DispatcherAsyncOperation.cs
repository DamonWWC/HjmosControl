using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Embed
{
    public class DispatcherAsyncOperation<T> : DispatcherObject
    {
        private DispatcherAsyncOperation()
        {
        }

        public DispatcherAsyncOperation<T> GetAwaiter()
        {
            return this;
        }

        public bool IsCompleted { get; private set; }

        public T Result { get; private set; }

        public T GetResult()
        {
            if (_exception != null)
            {
                ExceptionDispatchInfo.Capture(_exception).Throw();
            }
            return Result;
        }

        public DispatcherAsyncOperation<T> ConfigurePriority(DispatcherPriority priority)
        {
            _priority = priority;
            return this;
        }

        public void OnCompleted(Action continuation)
        {
            if (IsCompleted)
            {
                continuation?.Invoke();
            }
            else
            {
                _continuation += continuation;
            }
        }

        private void ReportResult(T result, Exception exception)
        {
            Result = result;
            _exception = exception;
            IsCompleted = true;

            if (_continuation != null)
            {
                Dispatcher.InvokeAsync(_continuation, _priority);
            }
        }

        private Action _continuation;

        private Exception _exception;

        private DispatcherPriority _priority = DispatcherPriority.Normal;

        public static DispatcherAsyncOperation<T> Create(out Action<T, Exception> reportResult)
        {
            var asyncOperation = new DispatcherAsyncOperation<T>();
            reportResult = asyncOperation.ReportResult;
            return asyncOperation;
        }
    }
}

