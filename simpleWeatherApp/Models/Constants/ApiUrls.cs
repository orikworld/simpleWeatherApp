using System;
using simpleWeatherApp.Models.Enum;

namespace simpleWeatherApp.Models.Constants
{
    public class ApiUrls
    {
        public const string WeatherForecastUri
            = "https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units={2}&APPID=a2d8f09d62c1fb4176521266d8c81b4e";

        public const string WeatherImageUri = "https://openweathermap.org/img/w/{0}.png";
    }
}
