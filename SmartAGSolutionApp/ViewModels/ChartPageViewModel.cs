using Microcharts;
using Prism.Navigation;
using SmartAGSolutionApp.Data;

namespace SmartAGSolutionApp.ViewModels
{
    class ChartPageViewModel : ViewModelBase
    {
        public ChartPageViewModel(INavigationService navigationService, IDataProvider dataProvider)
            : base(navigationService, dataProvider)
        {
            Title = string.Empty;

            TemperatureChart = new LineChart()
            {
                Entries = dataProvider.GetTemperatureChartEntries(),
                LineSize = 6,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Circle,
            };

            HumidityChart = new LineChart()
            {
                Entries = dataProvider.GetHumidityChartEntries(),
                LineSize = 6,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Circle,
            };

            AirTemperatureChart = new LineChart()
            {
                Entries = dataProvider.GetAirTemperatureChartEntries(),
                LineSize = 6,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Circle,
            };

            AirHumidityChart = new LineChart()
            {
                Entries = dataProvider.GetAirHumidityChartEntries(),
                LineSize = 6,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Circle,
            };

            IlluminanceChart = new LineChart()
            {
                Entries = dataProvider.GetIlluminanceChartEntries(),
                LineSize = 6,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Circle,
            };

            CO2Chart = new LineChart()
            {
                Entries = dataProvider.GetCO2ChartEntries(),
                LineSize = 6,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Circle,
            };
        }

        #region Properties

        public Chart TemperatureChart { get; set; }

        public Chart HumidityChart { get; set; }

        public Chart AirTemperatureChart { get; set; }

        public Chart AirHumidityChart { get; set; }

        public Chart IlluminanceChart { get; set; }

        public Chart CO2Chart { get; set; }

        #endregion
    }
}
