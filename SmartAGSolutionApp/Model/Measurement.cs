namespace SmartAGSolutionApp.Model
{
    public class Measurement
    {
        public Measurement()
        {
            this.Temperature = this.Humidity = this.AirHumidity = this.AirTemperature = this.Illuminance = this.CO2 = 0;
        }

        public Measurement(double temperature, double humidity, double airHumidity, double airTemperature, double illuminance, double co2)
        {
            this.Temperature = temperature;
            this.Humidity = humidity;
            this.AirHumidity = airHumidity;
            this.AirTemperature = airTemperature;
            this.Illuminance = illuminance;
            this.CO2 = co2;
        }

        #region Properties

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public double AirTemperature { get; set; }

        public double AirHumidity { get; set; }

        public double Illuminance { get; set; }

        public double CO2 { get; set; }

        #endregion
    }
}