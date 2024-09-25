# Weather Updates Application

It's a weather updates application that provides current weather data, temperature statistics and history, and data visualization for a given lattitude and longtitude. This application was developed on .NET backend and a React frontend, which communicate to fetch and display weather data.

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [API Endpoints](#api-endpoints)
- [Environment Variables](#environment-variables)
- [Setup Instructions](#setup-instructions)
  - [Backend Setup](#backend-setup)
  - [Frontend Setup](#frontend-setup)
- [Running the Project](#running-the-project)

## Features
- Displays current weather data(Temperature, Humidity, Wind Speed, Description) for any location based on latitude and longitude.
- Converts temperatures from Celsius to Fahrenheit and Kelvin.
- Displays average, highest, and lowest temperatures for specified number of days.
- Displays a chart of weather statistics

## Technologies Used
**Backend:**  .NET 8, ASP.NET Core, C#, Weatherbit API 
**Frontend:** React 
**Unit Testing:** xUnit and Moq

## Prerequisites

### Required Software
- Node.js (version 14.x or later)
- .NET SDK (version 6.x or 8.x)
- Visual Studio Code or another IDE of your choice

## API Endpoints
The backend provides the following endpoints:

- `GET /weather`: Get current weather data for a given latitude and longitude.
- `GET /weather/convert-temperature`: Used to Convert the current temperature to a specified unit(K or F).
- `GET /weather/temperature-statistics`: Used to fetch the average, highest, and lowest temperatures for specified number of days.
- `GET /weather/temperature-chart-data`: Used to fetch data for visualizing temperature statistics through a chart.

## Environment Variables

### Backend
- `WEATHER_API_KEY`: Your Weatherbit API key (required)

### Frontend
- `REACT_APP_API_URL`: The URL of the backend API (default is `http://localhost:5092`)

## Setup Instructions

### Backend Setup
1. **To Clone the Repository**
    ```bash
    git clone https://github.com/your-repo/weather-updates.git
    cd WeatherUpdates
    ```
2. **To Install Dependencies**
    ```bash
    dotnet restore
    ```
3. **To Set Environment Variables**

   I have used Weatherbit API for the API key.

   Instead of Hardcoding the APIKey I have set API key to the environment variable.
   Alternatively, you can set the environment variable directly in your terminal:
    ```bash
    export WEATHER_API_KEY=your_api_key_here
    ```
   > Replace `your_api_key_here` with your actual Weatherbit API key.(https://www.weatherbit.io/)

4. **To Run the Backend**
    ```bash
    dotnet run
    ```
   The backend will start running at `http://localhost:5092`.

### Frontend Setup
1. **Navigate to the Frontend Directory**
    ```bash
    cd weather-updates-ui
    ```
2. **Install Dependencies**
    ```bash
    npm install
    ```
3. **Create a `.env` File**

   In the `weather-updates-ui` directory, create a `.env` file and add the following:
    ```env
    REACT_APP_API_URL=http://localhost:5092
    ```
4. **Start the Frontend**
    ```bash
    npm start
    ```
   The frontend will start running at `http://localhost:3001`.



## Running the Project

### Start the Backend
1. Open a terminal in the `WeatherUpdates` directory.
2. Run:
    ```bash
    dotnet run
    ```

### Start the Frontend
1. Open another terminal in the `weather-updates-ui` directory.
2. Run:
    ```bash
    npm start
    ```

You should now be able to access the application at `http://localhost:3001`.


