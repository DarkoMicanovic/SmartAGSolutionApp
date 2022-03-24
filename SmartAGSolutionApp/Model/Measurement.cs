using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartAGSolutionApp.Model
{
    public class Measurement
    {
        public Measurement()
        {
            this.Temperature = this.Humidity = this.AirHumidity = this.AirTemperature = this.Illuminance = this.Lumen = 0;
        }

        public Measurement(double temperature, double humidity, double airHumidity, double airTemperature, double illuminance, double lumen)
        {
            this.Temperature = temperature;
            this.Humidity = humidity;
            this.AirHumidity = airHumidity;
            this.AirTemperature = airTemperature;
            this.Illuminance = illuminance;
            this.Lumen = lumen;
        }

        #region Properties

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public double AirTemperature { get; set; }

        public double AirHumidity { get; set; }

        public double Illuminance { get; set; }

        public double Lumen { get; set; }

        #endregion
    }
}