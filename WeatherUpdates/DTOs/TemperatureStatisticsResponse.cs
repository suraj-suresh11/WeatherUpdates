namespace WeatherUpdates.DTOs
{
    public class TemperatureStatisticsResponse
    {
        /// <summary>
        /// Gets or sets the average temperature over a specified period.
        /// </summary>
        public double AverageTemperature { get; set; }

        /// <summary>
        /// Gets or sets the highest temperature recorded over a specified period.
        /// </summary>
        public double HighestTemperature { get; set; }
        
        /// <summary>
        /// Gets or sets the lowest temperature recorded over a specified period.
        /// </summary>
        public double LowestTemperature { get; set; }
    }
}
