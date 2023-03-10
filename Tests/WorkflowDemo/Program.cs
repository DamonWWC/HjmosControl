using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services.DefinitionStorage;

namespace WorkflowDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            IServiceProvider serviceProvider = ConfigureServices();
            var loader = serviceProvider.GetService<IDefinitionLoader>();

            var json = System.IO.File.ReadAllText("json1.json");
            loader.LoadDefinition(json, Deserializers.Json);
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.Start();
            host.StartWorkflow("HelloWorld", 1, null);
            Console.ReadLine();
            host.Stop();



            //IServiceProvider serviceProvider = ConfigureServices();
            //var host = serviceProvider.GetService<IWorkflowHost>();
            //host.RegisterWorkflow<HelloWorldWorkflow>();
            //host.Start();

            //host.StartWorkflow("HelloWorld", 1, null);
            //Console.ReadLine();
            //host.Stop();
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
            Console.WriteLine("你好");
           
            return ExecutionResult.Next();
        }
    }

    public class GoodbyeWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("再见");
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