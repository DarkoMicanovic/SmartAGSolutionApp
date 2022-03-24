using Prism.Commands;
using Prism.Navigation;
using SmartAGSolutionApp.Data;
using SmartAGSolutionApp.Model;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace SmartAGSolutionApp.ViewModels
{
    public class HistoryPageViewModel : ViewModelBase, INavigatedAware, IInitialize
    {
        private IDataProvider dataProvider;
        private INavigationService navigationService;
        private string temperatureString, humidityString, airTemperatureString, airHumidityString, illuminanceString, lumenString;

        public HistoryPageViewModel(INavigationService navigationService, IDataProvider dataProvider)
            : base(navigationService, dataProvider)
        {
            Title = "History";
            this.navigationService = navigationService;
            this.dataProvider = dataProvider;

            OpenChartCommand = new DelegateCommand(() => OpenChart());
            this.SetProperties();
        }

        #region Properties

        public DelegateCommand OpenChartCommand { get; set; }

        public string TemperatureString
        {
            get { return this.temperatureString; }
            set { SetProperty(ref this.temperatureString, value); }
        }

        public string HumitidyString
        {
            get { return this.humidityString; }
            set { SetProperty(ref this.humidityString, value); }
        }

        public string AirHumitidyString
        {
            get { return this.airHumidityString; }
            set { SetProperty(ref this.airHumidityString, value); }
        }

        public string AirTemperatureString
        {
            get { return this.airTemperatureString; }
            set { SetProperty(ref this.airTemperatureString, value); }
        }

        public string IlluminanceString
        {
            get { return this.illuminanceString; }
            set { SetProperty(ref this.illuminanceString, value); }
        }

        public string LumenString
        {
            get { return this.lumenString; }
            set { SetProperty(ref this.lumenString, value); }
        }

        public string ActiveGreenhouseProfileName
        {
            get { return this.dataProvider.ActiveGreenhouse.Name; }
        }

        #endregion

        private void OpenChart()
        {
            navigationService.NavigateAsync("ChartPage");
        }

        private void SetProperties()
        {
            Measurement latestMeasurement = this.dataProvider.GetMeasurement();
            this.TemperatureString = $"{latestMeasurement.Temperature}\xB0 C";
            this.HumitidyString = $"{latestMeasurement.Humidity} %";
            this.AirTemperatureString = $"{latestMeasurement.AirTemperature}\xB0 C";
            this.AirHumitidyString = $"{latestMeasurement.AirHumidity} %";
            this.IlluminanceString = $"{latestMeasurement.Illuminance}";
            this.LumenString = $"{latestMeasurement.Lumen}";
        }

        #region IInitialize Members

        public void Initialize(INavigationParameters parameters)
        {
        }

        #endregion

        #region INavigationAware Members

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(Measurement)))
            {
                dataProvider.UpdateMeasurement(parameters.GetValue<Measurement>(nameof(Measurement)));
                this.SetProperties();
            }
        }

        #endregion
    }
}
