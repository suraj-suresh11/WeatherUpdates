// src/components/Weather.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { ClipLoader } from 'react-spinners';

const Weather = () => {
  const [latitude, setLatitude] = useState('12.9716');
  const [longitude, setLongitude] = useState('77.5946');
  const [weatherData, setWeatherData] = useState(null);
  const [error, setError] = useState('');
  const [convertedTemperature, setConvertedTemperature] = useState(null);
  const [unit, setUnit] = useState('C'); 
  const [loading, setLoading] = useState(false);

  // Function to fetch weather data from the backend API
  const fetchWeather = async () => {
    setLoading(true);
    try {
      const response = await axios.get(`${process.env.REACT_APP_API_URL}/weather`, {
        params: {
          latitude: latitude,
          longitude: longitude,
        },
      });
      setWeatherData(response.data);
      setError('');

      // Fetch the converted temperature based on the selected unit
      if (unit !== 'C') {
        const conversionResponse = await axios.get(`${process.env.REACT_APP_API_URL}/weather/convert-temperature`, {
          params: {
            latitude: latitude,
            longitude: longitude,
            toUnit: unit,
          },
        });
        setConvertedTemperature(conversionResponse.data.convertedTemperature.toFixed(2));
      } else {
        setConvertedTemperature(response.data.temperature.toFixed(2)); 
      }
    } catch (error) {
      setWeatherData(null);
      setError('Error fetching weather data. Please check your inputs or try again later.');
    }
    setLoading(false); 
  };

  // useEffect to fetch converted temperature whenever the unit changes
  useEffect(() => {
    const fetchConvertedTemperature = async () => {
      if (weatherData && unit !== 'C') {
        try {
          const conversionResponse = await axios.get(`${process.env.REACT_APP_API_URL}/weather/convert-temperature`, {
            params: {
              latitude: latitude,
              longitude: longitude,
              toUnit: unit,
            },
          });
          setConvertedTemperature(conversionResponse.data.convertedTemperature.toFixed(2));
        } catch (error) {
          setError('Error converting temperature. Please try again later.');
        }
      } else if (weatherData) {
        setConvertedTemperature(weatherData.temperature.toFixed(2));
      }
    };

    fetchConvertedTemperature();
  }, [unit, weatherData, latitude, longitude]);

  return (
    <div className="container weather-container">
      <h2 className="title">Weather Information</h2>
      <div className="input-section">
        <input
          type="text"
          className="input"
          placeholder="Enter Latitude"
          value={latitude}
          onChange={(e) => setLatitude(e.target.value)}
        />
        <input
          type="text"
          className="input"
          placeholder="Enter Longitude"
          value={longitude}
          onChange={(e) => setLongitude(e.target.value)}
        />
      </div>

      {/* Radio buttons */}
      <div className="radio-section">
        <label>
          <input
            type="radio"
            value="C"
            checked={unit === 'C'}
            onChange={() => setUnit('C')}
          />
          Celsius (°C)
        </label>
        <label>
          <input
            type="radio"
            value="F"
            checked={unit === 'F'}
            onChange={() => setUnit('F')}
          />
          Fahrenheit (°F)
        </label>
        <label>
          <input
            type="radio"
            value="K"
            checked={unit === 'K'}
            onChange={() => setUnit('K')}
          />
          Kelvin (K)
        </label>
      </div>

      <button className="button" onClick={fetchWeather}>Get Weather</button>
      {loading && <ClipLoader color="#36d7b7" size={50} />} 

      {error && <p style={{ color: 'red' }}>{error}</p>}

      {weatherData && (
        <div className="data-card">
          <h3>Weather Data: {weatherData.cityName}</h3>
          <p>Temperature: {convertedTemperature} {unit}</p> 
          <p>Humidity: {weatherData.humidity} %</p>
          <p>Wind Speed: {weatherData.windSpeed} m/s</p>
          <p>Description: {weatherData.weatherDescription}</p>
        </div>
      )}
    </div>
  );
};

export default Weather;
