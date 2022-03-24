using Microcharts;
using SmartAGSolutionApp.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SmartAGSolutionApp.Data

{
    public interface IDataProvider
    {
        Measurement GetMeasurement();

        void UpdateMeasurement(Measurement measurement);

        ObservableCollection<Greenhouse> GetGreenhouseCollection();

        Greenhouse ActiveGreenhouse { get; set; }

        void SetActiveGreenhouse(Greenhouse greenhouse);

        void AddGreenhouse(Greenhouse greenhouse);

        void RemoveGreenhouse(Greenhouse greenhouse);

        IEnumerable<ChartEntry> GetTemperatureChartEntries();

        IEnumerable<ChartEntry> GetHumidityChartEntries();

        IEnumerable<ChartEntry> GetAirTemperatureChartEntries();

        IEnumerable<ChartEntry> GetAirHumidityChartEntries();

        IEnumerable<ChartEntry> GetIlluminanceChartEntries();

        IEnumerable<ChartEntry> GetLumenChartEntries();

        void Serialize();

        void Deserialize();
    }
}
