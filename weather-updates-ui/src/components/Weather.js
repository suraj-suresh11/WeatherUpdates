// src/components/Weather.js
import React, { useState } from 'react';
import axios from 'axios';

const Weather = () => {
  // State variables
  const [latitude, setLatitude] = useState('');
  const [longitude, setLongitude] = useState('');
  const [weatherData, setWeatherData] = useState(null);
  const [error, setError] = useState('');
  const [convertedTemperature, setConvertedTemperature] = useState(null);
  const [unit, setUnit] = useState('C'); 

  // Function to fetch weather data from the backend API
  const fetchWeather = async () => {
    try {
      const response = await axios.get(`${process.env.REACT_APP_API_URL}/weather`, {
        params: {
          latitude: latitude,
          longitude: longitude,
        },
      });
      setWeatherData(response.data);
      setError('');
      setConvertedTemperature(null); 
    } catch (error) {
      // Handle errors 
      setWeatherData(null);
      setError('Error fetching weather data. Please check your inputs or try again later.');
    }
  };

  // Function to convert the temperature
  const convertTemperature = async (targetUnit) => {
    if (!weatherData) return;
    try {
      const response = await axios.get(`${process.env.REACT_APP_API_URL}/weather/convert-temperature`, {
        params: {
          latitude: latitude,
          longitude: longitude,
          toUnit: targetUnit,
        },
      });
      setConvertedTemperature(response.data.convertedTemperature);
      setUnit(response.data.unit);
    } catch (error) {
      setError('Error converting temperature. Please try again later.');
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
        <div style={{ backgroundColor: '#333', padding: '20px', borderRadius: '10px', color: '#fff', marginTop: '20px' }}>
          <h3>Weather Data:</h3>
          <p>Temperature: {convertedTemperature !== null ? `${convertedTemperature} ${unit}` : `${weatherData.temperature} °C`}</p>
          <p>Humidity: {weatherData.humidity} %</p>
          <p>Wind Speed: {weatherData.windSpeed} m/s</p>
          <p>Description: {weatherData.weatherDescription}</p>

          {/* Temperature conversion buttons */}
          <button onClick={() => convertTemperature('C')}>Convert to Celsius</button>
          <button onClick={() => convertTemperature('F')}>Convert to Fahrenheit</button>
          <button onClick={() => convertTemperature('K')}>Convert to Kelvin</button>
        </div>
      )}
    </div>
  );
};

export default Weather;
