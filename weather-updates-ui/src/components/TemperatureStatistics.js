// src/components/TemperatureStatistics.js
import React, { useState } from 'react';
import axios from 'axios';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { Line } from 'react-chartjs-2';
// Registering Chart.js components
ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend);

const TemperatureStatistics = () => {
  // State variables
  const [latitude, setLatitude] = useState('');
  const [longitude, setLongitude] = useState('');
  const [days, setDays] = useState(7);
  const [temperatureData, setTemperatureData] = useState(null);
  const [error, setError] = useState('');
  // Function to get temperature statistics
  const fetchTemperatureStatistics = async () => {
    try {
      // API call to fetch temperature statistics
      const response = await axios.get(`http://localhost:5092/weather/temperature-statistics`, {
        params: {
          latitude: latitude,
          longitude: longitude,
          days: days,
        },
      });
      setTemperatureData(response.data);
      setError('');
    } catch (error) {
      setTemperatureData(null);
      setError('Error fetching temperature statistics. Please try again later.');
    }
  };

  return (
    <div>
      <h2>Temperature Statistics</h2>
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
      <input
        type="number"
        placeholder="Enter Days"
        value={days}
        onChange={(e) => setDays(e.target.value)}
      />
      <button onClick={fetchTemperatureStatistics}>Get Statistics</button>

      {error && <p style={{ color: 'red' }}>{error}</p>}

      {temperatureData && (
        <div style={{ width: '600px', margin: '0 auto', padding: '20px', backgroundColor: '#333', borderRadius: '10px' }}>
          <h3 style={{ color: '#fff' }}>Temperature Data:</h3>
          <p style={{ color: '#fff' }}>Average Temperature: {temperatureData.averageTemperature} °C</p>
          <p style={{ color: '#fff' }}>Highest Temperature: {temperatureData.highestTemperature} °C</p>
          <p style={{ color: '#fff' }}>Lowest Temperature: {temperatureData.lowestTemperature} °C</p>

          <div style={{ position: 'relative', height: '400px', width: '600px', margin: '0 auto' }}> {/* Container to control chart size */}
            <Line
              data={{
                labels: ['Lowest', 'Average', 'Highest'],
                datasets: [
                  {
                    label: 'Temperature (°C)',
                    data: [
                      temperatureData.lowestTemperature,
                      temperatureData.averageTemperature,
                      temperatureData.highestTemperature,
                    ],
                    borderColor: 'rgba(75,192,192,1)',
                    borderWidth: 2,
                    fill: false,
                    tension: 0.4,
                  },
                ],
              }}
              options={{
                responsive: true,
                maintainAspectRatio: false, 
                plugins: {
                  legend: {
                    labels: {
                      color: '#fff', 
                    },
                  },
                },
                scales: {
                  x: {
                    ticks: {
                      color: '#fff', 
                    },
                    grid: {
                      display: false, 
                    },
                  },
                  y: {
                    ticks: {
                      color: '#fff', 
                    },
                    grid: {
                      color: 'rgba(255, 255, 255, 0.1)', 
                    },
                  },
                },
              }}
            />
          </div>
        </div>
      )}
    </div>
  );
};

export default TemperatureStatistics;