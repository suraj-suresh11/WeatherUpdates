using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WeatherUpdates.Services;
using WeatherUpdates.DTOs;  // Include the namespace for the DTOs

namespace WeatherUpdates.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] double latitude, [FromQuery] double longitude)
        {
            if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
            {
                return BadRequest("Invalid latitude or longitude values.");
            }

            try
            {
                var weatherData = await _weatherService.GetWeatherAsync(latitude, longitude);

                if (weatherData == null)
                {
                    return NotFound("Weather data not found for the specified location.");
                }

                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // ConvertTemperature method
        [HttpGet("convert-temperature")]
        public async Task<IActionResult> ConvertTemperature([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] string toUnit)
        {
            if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
            {
                return BadRequest("Invalid latitude or longitude values.");
            }

            try
            {
                var weatherData = await _weatherService.GetWeatherAsync(latitude, longitude);

                if (weatherData == null)
                {
                    return NotFound("Weather data not found for the specified location.");
                }

                double convertedTemperature = _weatherService.ConvertTemperature(weatherData.Temperature, "C", toUnit);

                // Return the DTO object
                return Ok(new TemperatureResponse { ConvertedTemperature = convertedTemperature, Unit = toUnit });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GetTemperatureStatistics method
        [HttpGet("temperature-statistics")]
        public async Task<IActionResult> GetTemperatureStatistics([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] int days)
        {
            if (days <= 0) return BadRequest("The number of days must be greater than zero.");

            try
            {
                var statistics = await _weatherService.GetTemperatureStatisticsAsync(latitude, longitude, days);

                // Return the DTO object
                return Ok(new TemperatureStatisticsResponse
                {
                    AverageTemperature = statistics.AverageTemperature,
                    HighestTemperature = statistics.HighestTemperature,
                    LowestTemperature = statistics.LowestTemperature
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("temperature-chart-data")]
        public async Task<IActionResult> GetTemperatureChartData([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] int days)
        {
            if (days <= 0) return BadRequest("The number of days must be greater than zero.");

            try
            {
                var statisticsList = await _weatherService.GetTemperatureDataForChartAsync(latitude, longitude, days);
                return Ok(statisticsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
