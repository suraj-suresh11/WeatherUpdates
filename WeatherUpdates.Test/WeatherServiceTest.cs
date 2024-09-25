using Xunit;
using Moq;
using System.Net;
using Moq.Protected;
using WeatherUpdates.Services;
using WeatherUpdates.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace WeatherUpdates.Tests
{
    public class WeatherServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly HttpClient _httpClient;
        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfiguration.Setup(config => config["WeatherApi:ApiKey"]).Returns("dummy-api-key");

            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _weatherService = new WeatherService(_httpClient, _mockMemoryCache.Object, _mockConfiguration.Object);
        }

                [Fact]
        public async Task GetWeatherAsync_ReturnsNull_WhenWeatherDataNotFound()
        {
            // Arrange
            double latitude = 0;
            double longitude = 0;

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await _weatherService.GetWeatherAsync(latitude, longitude);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ConvertTemperature_ShouldConvertCelsiusToFahrenheit()
        {
            // Arrange
            double temperatureCelsius = 25.0;
            string fromUnit = "C";
            string toUnit = "F";

            // Act
            var result = _weatherService.ConvertTemperature(temperatureCelsius, fromUnit, toUnit);

            // Assert
            Assert.Equal(77.0, result);
        }
    }
}
