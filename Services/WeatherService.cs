using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherUpdate.Models;
using WeatherUpdate.Utilities;
using System;

namespace WeatherUpdate.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "5716e457c83745228d058ba39923148b";  

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherData?> GetWeatherAsync(double latitude, double longitude)
        {
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
            return (averageTemperature, highestTemperature, lowestTemperature);
        }
    }
}
