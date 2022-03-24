using Prism;
using Prism.Ioc;
using SmartAGSolutionApp.Data;
using SmartAGSolutionApp.ViewModels;
using SmartAGSolutionApp.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace SmartAGSolutionApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<IDataProvider,DataProvider>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<HistoryPage, HistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<GreenhouseProfilesPage, GreenhouseProfilesPageViewModel>();
            containerRegistry.RegisterForNavigation<GreenhouseProfilesAddPage, GreenhouseProfilesAddPageViewModel>();
            containerRegistry.RegisterForNavigation<ChartPage, ChartPageViewModel>();
        }
    }
}
