using Coravel;
using Coravel.Queuing.Interfaces;
using Coravel.Scheduling.Schedule;
using Coravel.Scheduling.Schedule.Interfaces;
using Microsoft.Extensions.Options;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Windows;

namespace BlankApp1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IScheduler scheduler,IQueue queue, IServiceProvider serviceProvider,IOptionsSnapshot<Config> options)
        {
            Debug.WriteLine("222");
            serviceProvider.UseScheduler(scheduler=>
            {
                scheduler.Schedule(() => Debug.WriteLine("Every second during the week."))

                  .EverySecond()
                  .Weekday();
            });
            var aa = options.Value.name;
            scheduler.Schedule(() => MessageBox.Show("11"))
                  .EverySecond();

            //var aa = scheduler.Schedule(() => Debug.WriteLine("Every second during the week."))
            //      //工作日每隔1秒输出
            //      .EverySecond()
            //      .Weekday();
        }
    }
}
