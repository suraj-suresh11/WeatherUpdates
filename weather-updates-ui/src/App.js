// src/App.js
import React from 'react';
import Weather from './components/Weather';
import TemperatureStatistics from './components/TemperatureStatistics';
import './App.css';

const App = () => {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Weather Updates</h1>
        <Weather />
        <TemperatureStatistics />
      </header>
    </div>
  );
};

export default App;
