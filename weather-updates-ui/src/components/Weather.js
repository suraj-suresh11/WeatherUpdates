// src/components/Weather.js
import React, { useState } from 'react';
import axios from 'axios';

const Weather = () => {
  const [latitude, setLatitude] = useState('');
  const [longitude, setLongitude] = useState('');
  const [weatherData, setWeatherData] = useState(null);
  const [error, setError] = useState('');

  const fetchWeather = async () => {
    try {
      const response = await axios.get(`http://localhost:5092/weather`, {
        params: {
          latitude: latitude,
          longitude: longitude,
        },
      });
      setWeatherData(response.data);
      setError('');
    } catch (error) {
      setWeatherData(null);
      setError('Error fetching weather data. Please check your inputs or try again later.');
    }
  };

  return (
    <div>
      <h2>Weather Information</h2>
      <input
        type="text"
        placeholder="Enter Latitude"
        value={latitude}
        onChange={(e) => setLatitude(e.target.value)}
      />
      <input
        type="text"
        placeholder="Enter Longitude"
        value={longitude}
        onChange={(e) => setLongitude(e.target.value)}
      />
      <button onClick={fetchWeather}>Get Weather</button>

      {error && <p style={{ color: 'red' }}>{error}</p>}

      {weatherData && (
        <div>
          <h3>Weather Data:</h3>
          <p>Temperature: {weatherData.temperature} °C</p>
          <p>Humidity: {weatherData.humidity} %</p>
          <p>Wind Speed: {weatherData.windSpeed} m/s</p>
          <p>Description: {weatherData.weatherDescription}</p>
        </div>
      )}
    </div>
  );
};

export default Weather;
