using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WpfWorkflowDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        IWorkflowHost host;
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IServiceProvider serviceProvider = ConfigureServices();
            host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<HelloWorldWorkflow>();
            host.Start();

           
            //Console.ReadLine();
            //host.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            host.StartWorkflow("HelloWorld", 1, null);
        }



        private static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();
            services.AddWorkflowDSL();
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }

    public class HelloWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Debug.WriteLine("你好");
           // Thread.Sleep(50000);
            var aa = MessageBox.Show("22");
          

            return ExecutionResult.Next();
        }
    }

    public class GoodbyeWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Debug.WriteLine("再见");
            var aa = MessageBox.Show("33");
            return ExecutionResult.Next();
        }
    }


    public class HelloWorldWorkflow : IWorkflow
    {
        public string Id => "HelloWorld";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder.StartWith<HelloWorld>().Then<GoodbyeWorld>();
        }
    }



}
