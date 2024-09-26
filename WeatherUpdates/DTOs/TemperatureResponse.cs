namespace WeatherUpdates.DTOs
{
    public class TemperatureResponse
    {
        /// <summary>
        /// Gets or sets the converted temperature value.
        /// </summary>
        public double ConvertedTemperature { get; set; }
        /// <summary>
        /// Gets or sets the unit of the converted temperature (e.g., Celsius, Fahrenheit, Kelvin).
        /// </summary>
        public string Unit { get; set; }
    }
}
