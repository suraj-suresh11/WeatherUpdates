using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherUpdates.Models;
using WeatherUpdates.Utilities;
using Microsoft.Extensions.Caching.Memory; 
using System;

namespace WeatherUpdates.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string _apiKey = "5716e457c83745228d058ba39923148b";  
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

        public WeatherService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _cache = memoryCache;
        }

        public async Task<WeatherData?> GetWeatherAsync(double latitude, double longitude)
        {
            string cacheKey = $"WeatherData_{latitude}_{longitude}";
            if (_cache.TryGetValue(cacheKey, out WeatherData cachedWeatherData))
            {
                return cachedWeatherData;
            }
            var url = $"https://api.weatherbit.io/v2.0/current?lat={latitude}&lon={longitude}&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var weatherApiResponse = JsonConvert.DeserializeObject<WeatherApiResponse>(responseData);

                if (weatherApiResponse?.Data != null && weatherApiResponse.Data.Count > 0)
                {
                    var apiData = weatherApiResponse.Data[0];

                    var weatherData = new WeatherData
                    {
                        Temperature = apiData.Temp,
                        Humidity = apiData.Rh,
                        WindSpeed = apiData.Wind_spd,
                        WeatherDescription = apiData.Weather.Description
                    };

                    _cache.Set(cacheKey, weatherData, _cacheDuration);

                    return weatherData;
                }
            }

            return null;
        }

        public double ConvertTemperature(double temperature, string fromUnit, string toUnit)
        {
            return TemperatureConverter.ConvertTemperature(temperature, fromUnit, toUnit);
        }

        public async Task<(double AverageTemperature, double HighestTemperature, double LowestTemperature)> GetTemperatureStatisticsAsync(double latitude, double longitude, int days)
        {
            string cacheKey = $"TemperatureStatistics_{latitude}_{longitude}_{days}";

            if (_cache.TryGetValue(cacheKey, out (double AverageTemperature, double HighestTemperature, double LowestTemperature) cachedStatistics))
            {
                return cachedStatistics;
            }
            double totalTemperature = 0;
            int count = 0;
            double highestTemperature = double.MinValue;
            double lowestTemperature = double.MaxValue;

            for (int i = 0; i < days; i++)
            {
                var url = $"https://api.weatherbit.io/v2.0/history/daily?lat={latitude}&lon={longitude}&key={_apiKey}&start_date={DateTime.Now.AddDays(-i - 1):yyyy-MM-dd}&end_date={DateTime.Now.AddDays(-i):yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var weatherApiResponse = JsonConvert.DeserializeObject<WeatherApiResponse>(responseData);

                    if (weatherApiResponse?.Data != null && weatherApiResponse.Data.Count > 0)
                    {
                        var dailyTemp = weatherApiResponse.Data[0].Temp;
                        totalTemperature += dailyTemp;
                        count++;

                        if (dailyTemp > highestTemperature) highestTemperature = dailyTemp;
                        if (dailyTemp < lowestTemperature) lowestTemperature = dailyTemp;
                    }
                }
            }

            double averageTemperature = count > 0 ? totalTemperature / count : 0;
            var statistics = (AverageTemperature: averageTemperature, HighestTemperature: highestTemperature, LowestTemperature: lowestTemperature);
            _cache.Set(cacheKey, statistics, _cacheDuration);

            return statistics;
        }
    }
}
