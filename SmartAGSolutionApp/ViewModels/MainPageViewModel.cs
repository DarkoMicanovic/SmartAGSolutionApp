using Prism.Commands;
using Prism.Navigation;
using SmartAGSolutionApp.Data;
using SmartAGSolutionApp.Model;
using System;
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
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

            this.Title = string.Empty;
            this.GetLatestMeasurementCommand = new DelegateCommand(() => GetLatestMeasurements());
            this.ListLatestMeasurementsCommand = new DelegateCommand(() => ListLatestMeasurements());
            this.GreenhouseProfilesCommand = new DelegateCommand(() => GreenhouseProfiles());
            this.SettingsCommand = new DelegateCommand(() => Settings());
            this.SetApplicationLanguage();
        }

        #region Properties

        public string ButtonGetLatestMeasurements { get; set; }

        public DelegateCommand GreenhouseProfilesCommand { get; set; }

        public DelegateCommand GetLatestMeasurementCommand { get; set; }

        public DelegateCommand ListLatestMeasurementsCommand { get; set; }

        public DelegateCommand SettingsCommand { get; set; }

        #endregion

        async void GetLatestMeasurements()
        {
            if (this.DataProvider.ActiveGreenhouse.ID == default)
            {
                this.GreenhouseProfileIsNotSelectedDisplayWarning();
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

        private void SetApplicationLanguage()
        {
            if (this.DataProvider.SelectedLanguage == LanguageSelectionType.Serbian)
                LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("sr");
        }

        private void ListLatestMeasurements()
        {
            if (this.DataProvider.ActiveGreenhouse.ID == default)
            {
                this.GreenhouseProfileIsNotSelectedDisplayWarning();
                return;
            }

            this.navigationService.NavigateAsync("HistoryPage");
        }

        private void GreenhouseProfileIsNotSelectedDisplayWarning()
        {
            string titleMessage = LocalizationResourceManager.Current.GetValue("Information");
            string message = LocalizationResourceManager.Current.GetValue("Greenhouse_profile_is_not_selected");
            string cancelMessage = LocalizationResourceManager.Current.GetValue("OK");
            Application.Current.MainPage?.DisplayAlert(titleMessage, message, cancelMessage);
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
