using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WeatherUpdates.Services;
using WeatherUpdates.DTOs;

namespace WeatherUpdates.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        // DI of IWeatherService
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        // Endpoint to fetch current weather data
        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] double latitude, [FromQuery] double longitude)
        {
            // Validate latitude and longitude values
            if (!IsValidLatitude(latitude) || !IsValidLongitude(longitude))
            {
                return BadRequest("Invalid latitude or longitude values.");
            }

            try
            {
                // Fetch weather data using the weather service
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
        // Endpoint to convert the current temperature to a specified unit.
        [HttpGet("convert-temperature")]
        public async Task<IActionResult> ConvertTemperature([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] string toUnit)
        {
            if (!IsValidLatitude(latitude) || !IsValidLongitude(longitude))
            {
                return BadRequest("Invalid latitude or longitude values.");
            }

            if (!IsValidTemperatureUnit(toUnit))
            {
                return BadRequest("Invalid temperature unit. Please use 'C', 'F', or 'K'.");
            }

            try
            {
                // Fetch weather data for the given location
                var weatherData = await _weatherService.GetWeatherAsync(latitude, longitude);

                if (weatherData == null)
                {
                    return NotFound("Weather data not found for the specified location.");
                }
                // Convert the temperature from Celsius to the requested unit
                double convertedTemperature = _weatherService.ConvertTemperature(weatherData.Temperature, "C", toUnit);

                return Ok(new TemperatureResponse { ConvertedTemperature = convertedTemperature, Unit = toUnit });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // Endpoint to get temperature statistics 
        [HttpGet("temperature-statistics")]
        public async Task<IActionResult> GetTemperatureStatistics([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] int days)
        {
            if (!IsValidLatitude(latitude) || !IsValidLongitude(longitude))
            {
                return BadRequest("Invalid latitude or longitude values.");
            }

            if (days <= 0 || days > 30)
            {
                return BadRequest("Invalid number of days. Please provide a value between 1 and 30.");
            }

            try
            {
                var statistics = await _weatherService.GetTemperatureStatisticsAsync(latitude, longitude, days);

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
        // Helper methods to validate latitude and longitude values 
        private bool IsValidLatitude(double latitude)
        {
            return latitude >= -90 && latitude <= 90;
        }

        private bool IsValidLongitude(double longitude)
        {
            return longitude >= -180 && longitude <= 180;
        }
        // Helper method to validate temperature units
        private bool IsValidTemperatureUnit(string unit)
        {
            return unit == "C" || unit == "F" || unit == "K";
        }
    }
}
