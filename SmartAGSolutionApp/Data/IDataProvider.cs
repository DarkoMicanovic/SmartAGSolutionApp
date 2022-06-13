using Microcharts;
using SmartAGSolutionApp.Model;
using System;
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

        LanguageSelectionType SelectedLanguage { get; set; }

        Guid FindGreenhouse(Guid id);

        void SetActiveGreenhouse(Greenhouse greenhouse);

        void AddGreenhouse(Greenhouse greenhouse);

        void RemoveGreenhouse(string greenhouseName);

        void ApplyModify(Greenhouse greenhouse);

        void ClearCache();

        IEnumerable<ChartEntry> GetTemperatureChartEntries();

        IEnumerable<ChartEntry> GetHumidityChartEntries();

        IEnumerable<ChartEntry> GetAirTemperatureChartEntries();

        IEnumerable<ChartEntry> GetAirHumidityChartEntries();

        IEnumerable<ChartEntry> GetIlluminanceChartEntries();

        IEnumerable<ChartEntry> GetCO2ChartEntries();

        void Serialize();

        void Deserialize();
    }
}
