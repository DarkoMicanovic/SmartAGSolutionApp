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

            this.Title = "Smart AG Solution";
            this.GetLatestMeasurementCommand = new DelegateCommand(() => GetLatestMeasurements());
            this.ListLatestMeasurementsCommand = new DelegateCommand(() => ListLatestMeasurements());
            this.GreenhouseProfilesCommand = new DelegateCommand(() => GreenhouseProfiles());
            this.SettingsCommand = new DelegateCommand(() => Settings());
        }

        #region Properties

        public DelegateCommand GreenhouseProfilesCommand { get; set; }

        public DelegateCommand GetLatestMeasurementCommand { get; set; }

        public DelegateCommand ListLatestMeasurementsCommand { get; set; }

        public DelegateCommand SettingsCommand { get; set; }

        #endregion

        async void GetLatestMeasurements()
        {
            if (this.DataProvider.ActiveGreenhouse.ID == default)
            {
                Application.Current.MainPage?.DisplayAlert("Information", "Greenhouse profile is not selected.", "Ok");
                return;
            }

            var parameters = new NavigationParameters();
            var random = new Random();
            double temperature = Math.Round(random.NextDouble() * 3 + 4, 2);
            double humidity = Math.Round(random.NextDouble() * 60 + 30  , 2);
            double airTemperature = Math.Round(random.NextDouble() * 3 + 4, 2);
            double airHumidity = Math.Round(random.NextDouble() * 60 + 30, 2);
            double illuminance = Math.Round(random.NextDouble() * 50 + 300, 2);
            double lumen = Math.Round(random.NextDouble() * 80 + 100, 2);
            parameters.Add(nameof(Measurement), new Measurement(temperature, humidity, airHumidity, airTemperature, illuminance, lumen));

            await this.navigationService.NavigateAsync("HistoryPage", parameters);
        }

        private void ListLatestMeasurements()
        {
            if (this.DataProvider.ActiveGreenhouse.ID == default)
            {
                Application.Current.MainPage?.DisplayAlert("Information", "Greenhouse profile is not selected.", "Ok");
                return;
            }
            this.navigationService.NavigateAsync("HistoryPage");
        }

        private void GreenhouseProfiles()
        {
            this.navigationService.NavigateAsync("GreenhouseProfilesPage");
        }

        private void Settings()
        {
            this.navigationService.NavigateAsync("SettingsPage");
        }
    }
}
