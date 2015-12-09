using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using DeepMiningInc.Rendering;
using Microsoft.Practices.ServiceLocation;
using Template10.Common;

namespace Sample
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : BootStrapper
    {
        private static IContainer Container { get; set; }

        public App()
        {
            InitializeComponent();
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            await ConfigureContainerAsync();
            NavigationService.Navigate(typeof(Views.MainPage));
            await Task.Yield();
        }

        private Task ConfigureContainerAsync()
        {
            var builder = new ContainerBuilder();
            

            Container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));
            return Task.CompletedTask;
        }
    }
}

