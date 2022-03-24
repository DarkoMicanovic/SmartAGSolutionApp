using Microcharts;
using Prism.Navigation;
using SkiaSharp;
using SmartAGSolutionApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartAGSolutionApp.ViewModels
{
    class ChartPageViewModel : ViewModelBase
    {
        public ChartPageViewModel(INavigationService navigationService, IDataProvider dataProvider)
            : base(navigationService, dataProvider)
        {
            Title = "Measurements overview";

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

            LumenChart = new LineChart()
            {
                Entries = dataProvider.GetLumenChartEntries(),
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

        public Chart LumenChart { get; set; }

        #endregion
    }
}
