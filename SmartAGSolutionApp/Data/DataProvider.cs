using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System;
using Microcharts;
using Prism.Mvvm;
using SmartAGSolutionApp.Model;
using SkiaSharp;
using System.Linq;

namespace SmartAGSolutionApp.Data
{
    public class DataProvider : BindableBase, IDataProvider
    {
        private ObservableCollection<Greenhouse> greenhouseCollection;
        private Greenhouse activeGreenhouse;
        private Measurement measurement;
        private List<ChartEntry> temperatureChartEntries;
        private List<ChartEntry> humidityChartEntries;
        private List<ChartEntry> airTemperatureChartEntries;
        private List<ChartEntry> airHumidityChartEntries;
        private List<ChartEntry> illuminanceChartEntries;
        private List<ChartEntry> co2ChartEntries;
        private Queue<Measurement> measurementQueue;
        private LanguageSelectionType languageSelectionType;

        public DataProvider()
        {
            this.measurementQueue = new Queue<Measurement>();
            this.greenhouseCollection = new ObservableCollection<Greenhouse>();
            this.temperatureChartEntries = new List<ChartEntry>();
            this.humidityChartEntries = new List<ChartEntry>();
            this.airTemperatureChartEntries = new List<ChartEntry>();
            this.airHumidityChartEntries = new List<ChartEntry>();
            this.illuminanceChartEntries = new List<ChartEntry>();
            this.co2ChartEntries = new List<ChartEntry>();

            this.Deserialize();
        }

        private void UpdateCharts()
        {
            this.temperatureChartEntries.Clear();
            this.humidityChartEntries.Clear();
            this.airTemperatureChartEntries.Clear();
            this.airHumidityChartEntries.Clear();
            this.illuminanceChartEntries.Clear();
            this.co2ChartEntries.Clear();

            foreach (Measurement item in this.measurementQueue)
            {
                this.temperatureChartEntries.Add(new ChartEntry((float)item.Temperature) { Color = new SKColor(0xd5b400) });
                this.humidityChartEntries.Add(new ChartEntry((float)item.Humidity) { Color = new SKColor(0xd5b400) });
                this.airTemperatureChartEntries.Add(new ChartEntry((float)item.AirTemperature) { Color = new SKColor(0xd5b400) });
                this.airHumidityChartEntries.Add(new ChartEntry((float)item.AirHumidity) { Color = new SKColor(0xd5b400) });
                this.illuminanceChartEntries.Add(new ChartEntry((float)item.Illuminance) { Color = new SKColor(0xd5b400) });
                this.co2ChartEntries.Add(new ChartEntry((float)item.CO2) { Color = new SKColor(0xd5b400) });
            }
        }

        public void SerializeMeasurements()
        {
            if (this.activeGreenhouse.Name == string.Empty)
                return;

            string fileName = $"{this.activeGreenhouse.Name}.msnt";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

            using (FileStream writerFileStream = File.Create(filePath))
            {
                using (StreamWriter writer = new StreamWriter(writerFileStream))
                {
                    foreach (Measurement item in this.measurementQueue)
                    {
                        string line = $"{item.Temperature}:{item.Humidity}:{item.AirTemperature}:{item.AirHumidity}:{item.Illuminance}:{item.CO2}";
                        writer.WriteLine(line);
                    }
                }
            }
        }

        public void SerializeGreenhouses()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "greenhouse.list");

            using (FileStream writerFileStream = File.Create(filePath))
            {
                using (StreamWriter writer = new StreamWriter(writerFileStream))
                {
                    foreach (Greenhouse item in this.greenhouseCollection)
                    {
                        string line = $"{item.ID}:{item.PhoneNumber}:{item.Name}:{item.Description}";
                        writer.WriteLine(line);
                    }
                }
            }
        }

        public void SerializeAppInformation()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "app.info");

            using (FileStream writerFileStream = File.Create(filePath))
            {
                using (StreamWriter writer = new StreamWriter(writerFileStream))
                {
                    string activeGreenhouse = $"active_greenhouse:{this.activeGreenhouse.Name}";
                    writer.WriteLine(activeGreenhouse);
                    string selectedLanguage = $"selected_language:{this.languageSelectionType}";
                    writer.WriteLine(selectedLanguage);
                }
            }
        }

        public void DeserializeMeasurements()
        {
            if (this.activeGreenhouse.Name == string.Empty)
                return;

            if (this.measurementQueue.Count != 0)
                this.measurementQueue.Clear();

            string fileName = $"{this.activeGreenhouse.Name}.msnt";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

            if (!File.Exists(filePath))
            {
                this.measurement = new Measurement();
                this.UpdateCharts();
                return;
            }

            using (FileStream readerFileStream = File.OpenRead(filePath))
            {
                using (StreamReader reader = new StreamReader(readerFileStream))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] args = line.Split(':');
                        Measurement measuremnt = new Measurement(double.Parse(args[0]), double.Parse(args[1]), double.Parse(args[3]), double.Parse(args[2]), double.Parse(args[4]), double.Parse(args[5]));
                        this.measurementQueue.Enqueue(measuremnt);
                    }
                }
            }

            this.measurement = this.measurementQueue.Count != 0 ? this.measurementQueue.Last() : new Measurement();
            this.UpdateCharts();
        }

        public void DeserializeGreenhouses()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "greenhouse.list");

            if (!File.Exists(filePath))
                return;

            using (FileStream readerFileStream = File.OpenRead(filePath))
            {
                using (StreamReader reader = new StreamReader(readerFileStream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] args = line.Split(':');
                        this.greenhouseCollection.Add(new Greenhouse(args[1], args[2], args[3]) { ID = new Guid(args[0]) });
                    }
                }
            }
        }

        public void DeserializeAppInformation()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "app.info");

            if (!File.Exists(filePath))
            {
                this.activeGreenhouse = new Greenhouse();
                this.languageSelectionType = LanguageSelectionType.English;
                return;
            }

            using (FileStream readerFileStream = File.OpenRead(filePath))
            {
                using (StreamReader reader = new StreamReader(readerFileStream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] args = line.Split(':');

                        if (string.Equals(args[0], "active_greenhouse") && this.greenhouseCollection.Any(item => string.Equals(item.Name, args[1])))
                            this.activeGreenhouse = this.greenhouseCollection.Where(item => string.Equals(item.Name, args[1])).FirstOrDefault();
                        else if (string.Equals(args[0], "active_greenhouse") && string.Equals(args[1], string.Empty))
                            this.activeGreenhouse = new Greenhouse();
                        if (string.Equals(args[0], "selected_language"))
                            this.SetLanguageSelectionType(args[1]);
                    }
                }
            }
        }

        #region IDataProvider Members

        public Greenhouse ActiveGreenhouse
        {
            get { return this.activeGreenhouse; }
            set { SetProperty(ref this.activeGreenhouse, value); }
        }

        public LanguageSelectionType SelectedLanguage
        {
            get { return this.languageSelectionType; }
            set 
            {
                SetProperty(ref this.languageSelectionType, value);
                this.SerializeAppInformation();
            }
        }

        public Measurement GetMeasurement()
        {
            return this.measurement;
        }

        public void UpdateMeasurement(Measurement measurement)
        {
            this.measurement.Temperature = measurement.Temperature;
            this.measurement.Humidity = measurement.Humidity;
            this.measurement.AirTemperature = measurement.AirTemperature;
            this.measurement.AirHumidity = measurement.AirHumidity;
            this.measurement.Illuminance = measurement.Illuminance;
            this.measurement.CO2 = measurement.CO2;

            this.measurementQueue.Enqueue(measurement);
            if (this.measurementQueue.Count > 5)
                this.measurementQueue.Dequeue();

            this.SerializeMeasurements();
            this.UpdateCharts();
        }

        public void AddGreenhouse(Greenhouse greenhouse)
        {
            this.greenhouseCollection.Add(greenhouse);
            this.SetActiveGreenhouse(greenhouse);
        }

        public ObservableCollection<Greenhouse> GetGreenhouseCollection()
        {
            return this.greenhouseCollection;
        }

        public Guid FindGreenhouse(Guid id)
        {
            return this.greenhouseCollection.FirstOrDefault(greenhouse => greenhouse.ID == id).ID;
        }

        public void SetActiveGreenhouse(Greenhouse greenhouse)
        {
            if (this.greenhouseCollection.Contains(greenhouse) || greenhouse.ID == default)
                this.ActiveGreenhouse = greenhouse;
            this.DeserializeMeasurements();
            this.SerializeGreenhouses();
            this.SerializeAppInformation();
        }

        public void RemoveGreenhouse(string greenhouseName)
        {
            Greenhouse currentGreenhouse = this.greenhouseCollection.FirstOrDefault(item => item.Name == greenhouseName);
            if (currentGreenhouse != null)
            {
                this.greenhouseCollection.Remove(currentGreenhouse);
                string fileName = $"{this.activeGreenhouse.Name}.msnt";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
                File.Delete(filePath);
            }

            if (this.greenhouseCollection.Count == 0)
                this.SetActiveGreenhouse(new Greenhouse());
            else if (!this.greenhouseCollection.Contains(this.activeGreenhouse))
                this.SetActiveGreenhouse(this.greenhouseCollection[0]);

            this.SerializeGreenhouses();
        }

        public void ApplyModify(Greenhouse greenhouseUpdate)
        {
            foreach (Greenhouse greenhouse in this.greenhouseCollection.Where(greenhouse => greenhouse.ID == greenhouseUpdate.ID))
                greenhouse.Update(greenhouseUpdate.Name, greenhouseUpdate.PhoneNumber, greenhouseUpdate.Description);

            this.SerializeGreenhouses();
        }

        public void ClearCache()
        {
            string[] cachedFiles = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            foreach (var file in cachedFiles)
                File.Delete(file);

            this.measurementQueue.Clear();
            this.greenhouseCollection.Clear();
            this.temperatureChartEntries.Clear();
            this.humidityChartEntries.Clear();
            this.airTemperatureChartEntries.Clear();
            this.airHumidityChartEntries.Clear();
            this.illuminanceChartEntries.Clear();
            this.co2ChartEntries.Clear();

            this.Deserialize();
        }

        public IEnumerable<ChartEntry> GetTemperatureChartEntries()
        {
            return this.temperatureChartEntries;
        }

        public IEnumerable<ChartEntry> GetHumidityChartEntries()
        {
            return this.humidityChartEntries;
        }

        public IEnumerable<ChartEntry> GetAirTemperatureChartEntries()
        {
            return this.airTemperatureChartEntries;
        }

        public IEnumerable<ChartEntry> GetAirHumidityChartEntries()
        {
            return this.airHumidityChartEntries;
        }

        public IEnumerable<ChartEntry> GetIlluminanceChartEntries()
        {
            return this.illuminanceChartEntries;
        }

        public IEnumerable<ChartEntry> GetCO2ChartEntries()
        {
            return this.co2ChartEntries;
        }

        public void Serialize()
        {
            //GREENHOUSE COLLECTION:
            this.SerializeGreenhouses();

            //APP INFORMATION:
            this.SerializeAppInformation();

            //MEASUREMENT LIST:
            this.SerializeMeasurements();
        }

        public void Deserialize()
        {
            //GREENHOUSE COLLECTION:
            this.DeserializeGreenhouses();

            //APP INFORMATION:
            this.DeserializeAppInformation();

            //MEASUREMENT LIST:
            this.DeserializeMeasurements();
        }

        #endregion

        void SetLanguageSelectionType(string selectedLanguage)
        {
            if (selectedLanguage == "Serbian")
                this.languageSelectionType = LanguageSelectionType.Serbian;
            else if (selectedLanguage == "English")
                this.languageSelectionType = LanguageSelectionType.English;
        }
        }
    public enum LanguageSelectionType
    {
        Serbian = 0,
        English = 1
    }
}
