using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SmartAGSolutionApp.Data;
using SmartAGSolutionApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartAGSolutionApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService, IDataProvider dataProvider)
            : base(navigationService, dataProvider)
        {
            this.navigationService = navigationService;

            Title = "Smart AG Solution";
            GetLatestMeasurementCommand = new DelegateCommand(() => GetLatestMeasurements());
            ListLatestMeasurementsCommand = new DelegateCommand(() => ListLatestMeasurements());
            GreenhouseProfilesCommand = new DelegateCommand(() => GreenhouseProfiles());
        }

        #region Properties

        public DelegateCommand GreenhouseProfilesCommand { get; set; }

        public DelegateCommand GetLatestMeasurementCommand { get; set; }

        public DelegateCommand ListLatestMeasurementsCommand { get; set; }
        
        #endregion

        async void GetLatestMeasurements()
        {
            var parameters = new NavigationParameters();
            var rnd = new Random();
            double temperature = Math.Round(rnd.NextDouble() * 3 + 4, 2);
            double humidity = Math.Round(rnd.NextDouble() * 60 + 30  , 2);
            double airTemperature = Math.Round(rnd.NextDouble() * 3 + 4, 2);
            double airHumidity = Math.Round(rnd.NextDouble() * 60 + 30, 2);
            double illuminance = Math.Round(rnd.NextDouble() * 50 + 300, 2);
            double lumen = Math.Round(rnd.NextDouble() * 80 + 100, 2);
            parameters.Add(nameof(Measurement), new Measurement(temperature, humidity, airHumidity, airTemperature, illuminance, lumen));

            await navigationService.NavigateAsync("HistoryPage", parameters);
        }

        private void ListLatestMeasurements()
        {
            if (this.DataProvider.ActiveGreenhouse.Name == string.Empty)
            {
                Application.Current.MainPage.DisplayAlert("Information", "Greenhouse profile is not selected.", "Ok");
                return;
            }
            navigationService.NavigateAsync("HistoryPage");
        }

        private void GreenhouseProfiles()
        {
            navigationService.NavigateAsync("GreenhouseProfilesPage");
        }
    }
}
