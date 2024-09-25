# Weather Updates Application

A full-stack weather updates application that provides weather data, temperature statistics, and data visualization for a given location. This application consists of a **.NET backend** and a **React frontend**, which communicate to fetch and display weather data.

## Table of Contents
1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Prerequisites](#prerequisites)
4. [Setup Instructions](#setup-instructions)
   - [Backend Setup](#backend-setup)
   - [Frontend Setup](#frontend-setup)
5. [API Endpoints](#api-endpoints)
6. [Environment Variables](#environment-variables)
7. [Running the Project](#running-the-project)
8. [Project Structure](#project-structure)
9. [Common Issues and Troubleshooting](#common-issues-and-troubleshooting)

---

## Features

- Fetches current weather data for any location based on latitude and longitude.
- Converts temperatures between Celsius, Fahrenheit, and Kelvin.
- Displays average, highest, and lowest temperatures over a specified number of days.
- Visualizes temperature statistics using a line chart.

## Technologies Used

- **Backend**: .NET 6 / .NET 8, ASP.NET Core, C#, Weatherbit API, Memory Caching, AspNetCoreRateLimit
- **Frontend**: React, Axios, Chart.js, react-chartjs-2
- **Unit Testing**: xUnit and Moq

## Prerequisites

### Required Software
- [Node.js](https://nodejs.org/en/download/) (version 14.x or later)
- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.x or 8.x)
- [Visual Studio Code](https://code.visualstudio.com/) or another IDE of your choice
- Git (for cloning the repository)

## Setup Instructions

### Backend Setup

#### 1. Clone the Repository
```bash
git clone https://github.com/your-repo/weather-updates.git
cd WeatherUpdates
