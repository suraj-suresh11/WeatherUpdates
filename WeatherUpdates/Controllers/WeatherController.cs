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

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] double latitude, [FromQuery] double longitude)
        {
            if (!IsValidLatitude(latitude) || !IsValidLongitude(longitude))
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
                var weatherData = await _weatherService.GetWeatherAsync(latitude, longitude);

                if (weatherData == null)
                {
                    return NotFound("Weather data not found for the specified location.");
                }

                double convertedTemperature = _weatherService.ConvertTemperature(weatherData.Temperature, "C", toUnit);

                return Ok(new TemperatureResponse { ConvertedTemperature = convertedTemperature, Unit = toUnit });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

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

        private bool IsValidLatitude(double latitude)
        {
            return latitude >= -90 && latitude <= 90;
        }

        private bool IsValidLongitude(double longitude)
        {
            return longitude >= -180 && longitude <= 180;
        }

        private bool IsValidTemperatureUnit(string unit)
        {
            return unit == "C" || unit == "F" || unit == "K";
        }
    }
}
