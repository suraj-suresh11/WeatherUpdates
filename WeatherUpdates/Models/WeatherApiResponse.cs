using System.Collections.Generic;

namespace WeatherUpdates.Models
{
    /// <summary>
    /// Represents the response from the weather API.
    /// </summary>
    public class WeatherApiResponse
    {
        /// <summary>
        /// Returns list of weather data objects containing weather information.
        /// </summary>
        public List<WeatherApiData> Data { get; set; } = new List<WeatherApiData>();
    }
    /// <summary>
    /// Represents individual weather data for a specific time or location.
    /// </summary>
    public class WeatherApiData
    {
        /// <summary>
        /// The temperature at the given time/location in Celsius.
        /// </summary>
        public double Temp { get; set; }
        /// <summary>
        /// The relative humidity 
        /// </summary>
        public int Rh { get; set; }
        /// <summary>
        /// The wind speed in meters per second.
        /// </summary>
        public double Wind_spd { get; set; }
        /// <summary>
        /// Contains a description of the weather.
        /// </summary>
        public WeatherDescription Weather { get; set; } = new WeatherDescription();

        public string city_name { get; set; }
    }
    /// <summary>
    /// Represents a description of the weather condition.
    /// </summary>
    public class WeatherDescription
    {
        public string Description { get; set; } = string.Empty;
    }
}
