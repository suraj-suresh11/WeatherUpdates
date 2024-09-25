# Weather Updates Application

A full-stack weather updates application that provides weather data, temperature statistics, and data visualization for a given location. This application consists of a .NET backend and a React frontend, which communicate to fetch and display weather data.

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
  - [Backend Setup](#backend-setup)
  - [Frontend Setup](#frontend-setup)
- [API Endpoints](#api-endpoints)
- [Environment Variables](#environment-variables)
- [Running the Project](#running-the-project)
- [Project Structure](#project-structure)
- [Common Issues and Troubleshooting](#common-issues-and-troubleshooting)
- [Additional Information](#additional-information)

## Features
- Fetches current weather data for any location based on latitude and longitude.
- Converts temperatures between Celsius, Fahrenheit, and Kelvin.
- Displays average, highest, and lowest temperatures over a specified number of days.
- Visualizes temperature statistics using a line chart.

## Technologies Used
**Backend:** .NET 6 / .NET 8, ASP.NET Core, C#, Weatherbit API, Memory Caching, AspNetCoreRateLimit  
**Frontend:** React, Axios, Chart.js, react-chartjs-2  
**Unit Testing:** xUnit and Moq

## Prerequisites

### Required Software
- Node.js (version 14.x or later)
- .NET SDK (version 6.x or 8.x)
- Visual Studio Code or another IDE of your choice
- Git (for cloning the repository)

## Setup Instructions

### Backend Setup
1. **Clone the Repository**
    ```bash
    git clone https://github.com/your-repo/weather-updates.git
    cd WeatherUpdates
    ```
2. **Install Dependencies**
    ```bash
    dotnet restore
    ```
3. **Set Environment Variables**

   The project uses an API key for the Weatherbit API.

   Create a `.env` file in the `WeatherUpdates` directory (backend project root) and add the following line:
    ```makefile
    WEATHER_API_KEY=your_api_key_here
    ```
   Alternatively, you can set the environment variable directly in your terminal:
    ```bash
    export WEATHER_API_KEY=your_api_key_here
    ```
   > **Note:** Replace `your_api_key_here` with your actual Weatherbit API key.

4. **Run the Backend**
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
   The frontend will start running at `http://localhost:3000` (or another port if 3000 is already in use).

## API Endpoints
The backend provides the following endpoints:

- `GET /weather`: Get current weather data for a given latitude and longitude.
- `GET /weather/convert-temperature`: Convert the current temperature to a specified unit.
- `GET /weather/temperature-statistics`: Get the average, highest, and lowest temperatures over a specified number of days.
- `GET /weather/temperature-chart-data`: Fetch data for visualizing temperature statistics.

## Environment Variables
### Backend
- `WEATHER_API_KEY`: Your Weatherbit API key (required)

### Frontend
- `REACT_APP_API_URL`: The URL of the backend API (default is `http://localhost:5092`)

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

You should now be able to access the application at `http://localhost:3000`.

## Project Structure

### Backend
- **Controllers**: Contains API controller classes
- **DTOs**: Data Transfer Objects for transferring data between client and server
- **Models**: Classes representing data structures
- **Services**: Business logic services that interact with external APIs
- **Utilities**: Contains utility classes such as the temperature converter

### Frontend
- **src/components**: Contains React components for Weather and TemperatureStatistics
- **public**: Contains static assets like `index.html`
- `.env`: Environment variable configurations for the frontend

## Common Issues and Troubleshooting
- **Port Conflicts**: If either the backend or frontend ports are in use, modify the ports in your `.env` files or launch settings.
- **API Key Missing**: Ensure you have set the `WEATHER_API_KEY` environment variable.
- **CORS Issues**: Ensure CORS is correctly configured in the backend to allow requests from the frontend URL.
- **Build Errors**: Make sure all dependencies are installed and up to date.

## Additional Information
- **API Key**: Do NOT hardcode your API key. Follow the steps mentioned above to manage it using environment variables.
- **Unit Testing**: You can run backend unit tests by navigating to the `WeatherUpdates.Tests` directory and running `dotnet test`.
