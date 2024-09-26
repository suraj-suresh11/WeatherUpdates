namespace WeatherUpdates.Utilities
{
    public static class TemperatureConverter
    {
        public static double ConvertTemperature(double temperature, string fromUnit, string toUnit)
        {
            // Convert both units to uppercase to ensure the method is case-insensitive.
            fromUnit = fromUnit.ToUpper();
            toUnit = toUnit.ToUpper();

            if (fromUnit == toUnit) return temperature;
            // Conversion logic for Celsius to other units
            if (fromUnit == "C")
            {
                if (toUnit == "F")
                {   
                    // Convert Celsius to Fahrenheit
                    return (temperature * 9 / 5) + 32; 
                }
                else if (toUnit == "K")
                {
                     // Convert Celsius to Kelvin
                    return temperature + 273.15; 
                }
            }
            // Conversion logic for Fahrenheit to other units
            else if (fromUnit == "F")
            {
                if (toUnit == "C")
                {
                    // Convert Fahrenheit to Celsius
                    return (temperature - 32) * 5 / 9; 
                }
                else if (toUnit == "K")
                {
                    // Convert Fahrenheit to Kelvin
                    return (temperature - 32) * 5 / 9 + 273.15; 
                }
            }
            // Conversion logic for Kelvin to other units
            else if (fromUnit == "K")
            {
                if (toUnit == "C")
                {
                    // Convert Kelvin to Celsius
                    return temperature - 273.15; 
                }
                else if (toUnit == "F")
                {
                    // Convert Kelvin to Fahrenheit
                    return (temperature - 273.15) * 9 / 5 + 32; 
                }
            }

            throw new ArgumentException($"Invalid temperature conversion from {fromUnit} to {toUnit}");
        }
    }
}
