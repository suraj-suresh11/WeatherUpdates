import React, { useState } from 'react';
import axios from 'axios';

const Weather = () => {
  // State variables
  const [latitude, setLatitude] = useState('');
  const [longitude, setLongitude] = useState('');
  const [weatherData, setWeatherData] = useState(null);
  const [error, setError] = useState('');

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
    } catch (error) {
      // Handle errors 
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
        <div style={{ width: '600px', margin: '0 auto', padding: '20px', backgroundColor: '#333', borderRadius: '10px', color: '#fff' }}>
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
