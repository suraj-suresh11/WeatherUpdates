using Xunit;
using Moq;
using System.Net;
using Moq.Protected;
using Microsoft.AspNetCore.Mvc;
using WeatherUpdates.Controllers;
using WeatherUpdates.Services;
using WeatherUpdates.Models;
using System.Threading.Tasks;

namespace WeatherUpdates.Tests
{
    public class WeatherControllerTests
    {
        private readonly Mock<IWeatherService> _mockWeatherService;
        private readonly WeatherController _weatherController;

        public WeatherControllerTests()
        {
            _mockWeatherService = new Mock<IWeatherService>();
            _weatherController = new WeatherController(_mockWeatherService.Object);
        }

        [Fact]
        public async Task GetWeather_ReturnsBadRequest_ForInvalidCoordinates()
        {
            // Arrange
            double invalidLatitude = 100; // Invalid latitude
            double invalidLongitude = 200; // Invalid longitude

            // Act
            var result = await _weatherController.GetWeather(invalidLatitude, invalidLongitude);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetWeather_ReturnsNotFound_WhenWeatherDataNotFound()
        {
            // Arrange
            double latitude = 40.7128;
            double longitude = -74.0060;
            _mockWeatherService.Setup(service => service.GetWeatherAsync(latitude, longitude)).ReturnsAsync((WeatherData)null);

            // Act
            var result = await _weatherController.GetWeather(latitude, longitude);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetWeather_ReturnsOkResult_WithWeatherData()
        {
            // Arrange
            double latitude = 40.7128;
            double longitude = -74.0060;
            var weatherData = new WeatherData { Temperature = 25.0, Humidity = 80, WindSpeed = 5.0, WeatherDescription = "Clear" };
            _mockWeatherService.Setup(service => service.GetWeatherAsync(latitude, longitude)).ReturnsAsync(weatherData);

            // Act
            var result = await _weatherController.GetWeather(latitude, longitude) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(weatherData, result.Value);
        }

        [Fact]
        public async Task ConvertTemperature_ReturnsBadRequest_ForInvalidCoordinates()
        {
            // Arrange
            double invalidLatitude = 100;
            double invalidLongitude = 200;

            // Act
            var result = await _weatherController.ConvertTemperature(invalidLatitude, invalidLongitude, "F");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ConvertTemperature_ReturnsOkResult_WithConvertedTemperature()
        {
            // Arrange
            double latitude = 40.7128;
            double longitude = -74.0060;
            var weatherData = new WeatherData { Temperature = 25.0 }; // Celsius

            // Set up the mock service to return weather data
            _mockWeatherService
                .Setup(service => service.GetWeatherAsync(latitude, longitude))
                .ReturnsAsync(weatherData);

            // Set up the mock service to return the converted temperature
            _mockWeatherService
                .Setup(service => service.ConvertTemperature(25.0, "C", "F"))
                .Returns(77.0);

            // Act
            var result = await _weatherController.ConvertTemperature(latitude, longitude, "F");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = okResult.Value as dynamic;

            // If your controller is returning an anonymous object, you can use dynamic
            Assert.Equal(77.0, (double)resultValue.ConvertedTemperature);

            // If your controller is returning a known strongly typed object, replace the dynamic with the expected type:
            // var resultValue = Assert.IsType<ExpectedResponseType>(okResult.Value)
            // Assert.Equal(77.0, resultValue.ConvertedTemperature)
        }

        [Fact]
        public async Task GetTemperatureStatistics_ReturnsOkResult_WithStatistics()
        {
            // Arrange
            double latitude = 40.7128;
            double longitude = -74.0060;
            int days = 5;
            var statistics = (AverageTemperature: 20.0, HighestTemperature: 25.0, LowestTemperature: 15.0);
            _mockWeatherService.Setup(service => service.GetTemperatureStatisticsAsync(latitude, longitude, days)).ReturnsAsync(statistics);

            // Act
            var result = await _weatherController.GetTemperatureStatistics(latitude, longitude, days) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var resultValue = result.Value as dynamic;
            Assert.Equal(20.0, (double)resultValue.AverageTemperature);
            Assert.Equal(25.0, (double)resultValue.HighestTemperature);
            Assert.Equal(15.0, (double)resultValue.LowestTemperature);
        }
    }
}
