namespace WeatherUpdates.Models
{
    /// <summary>
    /// Represents the weather data model for storing weather-related information
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// Gets or sets the temperature value.
        /// </summary>
        public double Temperature { get; set; }
        /// <summary>
        /// Gets or sets the humidity percentage.
        /// </summary>
        public int Humidity { get; set; }
        /// <summary>
        /// Gets or sets the wind speed value.
        /// </summary>
        public double WindSpeed { get; set; }
        /// <summary>
        /// Gets or sets a brief description of the weather (e.g., "Sunny", "Cloudy", "Rainy").
        /// </summary>
        public string WeatherDescription { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
    }
}
