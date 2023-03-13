using System.Text;
using AutoMapper;
namespace TaskDemo
{
    public partial class Program
    {
       public static async Task Main()
        {
            //var task = Task.Run(() =>
            //{
            //    string fileText = File.ReadAllText(@"C:\12232.txt");
            //    return fileText;
            //});
            //Task continuation = task.ContinueWith(antecedent =>
            //{
            //    var fileNotFound = antecedent.Exception?.InnerExceptions?.FirstOrDefault(e => e is FileNotFoundException) as FileNotFoundException;
            //    if (fileNotFound != null)
            //    {
            //        Console.WriteLine(fileNotFound.Message);
            //    }
            //}, TaskContinuationOptions.OnlyOnFaulted);//仅当前面的任务中发生异常时才会执行延续

            //await continuation;


            //HandleThree();

            //Test();
            HelloFrom("122");
            Console.ReadLine();
        }
     
        //异常处理

        public static void HandleThree()
        {
            var task = Task.Run(() =>
            {
                throw new Exception("This exception is expected!");
            });
            try
            {
                task.Wait();
            }
            catch(AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    throw ex;
                    //// Handle the custom exception.
                    //if (ex is CustomException)
                    //{
                    //    Console.WriteLine(ex.Message);
                    //}
                    //// Rethrow any other exception.
                    //else
                    //{
                    //    throw ex;
                    //}
                }
            }
        }
        public static void Test()
        {
            var words = new string[] { "山", "飞", "千", "鸟", "绝" };
            var words2 = new string[] { "人", "灭", "径", "万", "踪" };
            var solution = "千山鸟飞绝，万径人踪灭";
            bool success = false;

            var barrier = new Barrier(2, b =>
            {
                var sb = new StringBuilder();
                sb.Append(string.Concat(words));
                sb.Append('，');
                sb.Append(string.Concat(words2));

                Console.WriteLine(sb.ToString());
                //Thread.Sleep(1000);
                if (string.CompareOrdinal(solution, sb.ToString()) == 0)
                {
                    success = true;
                    Console.WriteLine($"已完成");
                }
                Console.WriteLine($"当前阶段数：{b.CurrentPhaseNumber}");

            });

            var t = Task.Run(() => DoWork(words));
            //var t2 = Task.Run(() => DoWork(words2));

            Console.ReadLine();

            void DoWork(string[] words)
            {
                while (!success)
                {
                    var r = new Random();
                    for (int i = 0; i < words.Length; i++)
                    {
                        var swapIndex = r.Next(i, words.Length);
                        (words[swapIndex], words[i]) = (words[i], words[swapIndex]);
                    }

                    barrier.SignalAndWait();
                }
            }
        }

    }
}