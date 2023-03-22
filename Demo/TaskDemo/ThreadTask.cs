using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDemo
{
    internal  class ThreadTask
    {

        public static void TeskTask()
        {
            Console.WriteLine("程序开始");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var task = Task.Run(() =>
            {
                using(token.Register((Thread.CurrentThread.Abort)))
                {

                }
            });
        }
    }
}
