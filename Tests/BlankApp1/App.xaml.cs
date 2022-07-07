using BlankApp1.Views;
using Coravel;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;

namespace BlankApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            //var provider = new ServiceCollection()
            // .AddServices()
            //  .BuildServiceProvider();

        }
        //protected override IContainerExtension CreateContainerExtension()
        //{

        //    return base.CreateContainerExtension();
        //}

        protected override IContainerExtension CreateContainerExtension()
        {
            //return base.CreateContainerExtension();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScheduler();
            serviceCollection.AddQueue();
            serviceCollection.AddSingleton(typeof(aa));

            ConfigurationBuilder configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("json1.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configroot = configuration.Build();
            serviceCollection.AddOptions().Configure<Config>(e => configroot.Bind(e));
            Container c = new Container(CreateContainerRules());
            var extension = base.CreateContainerExtension() as DryIocContainerExtension;
            return new Prism.DryIoc.DryIocContainerExtension(new Container(CreateContainerRules())
                .WithDependencyInjectionAdapter(serviceCollection));
        }
    }
    public class aa
    {
        public aa()
        {
            name = 1;
        }
        public int name { get; set; }
    }
    public class Config
    {
        public int name { get; set; }
    }
}
