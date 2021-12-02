using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using simpleWeatherApp.Core.Base;
using simpleWeatherApp.Models.Constants;
using simpleWeatherApp.Models.Enum;
using simpleWeatherApp.Services;
using Xamarin.Forms;

namespace simpleWeatherApp.Modules.Weather.Base.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        #region Private Fields

        private readonly IWeatherService _service;

        private string _location;

        private ImageSource _weatherIcon;

        private float _temperature;

        private DateTime _sunrise;

        private DateTime _sunset;

        private float _humidity;

        private float _pressure;

        private string _cloudiness;

        private double _wind;


        #endregion

        #region Constuctors

        public WeatherViewModel(IWeatherService service)
        {
            _service = service;
        }

        #endregion

        #region Properties

        public ICommand UpdateCommand => new Command(async () => await UpdateCommandExecute());

        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public ImageSource WeatherIcon
        {
            get => _weatherIcon;
            set => SetProperty(ref _weatherIcon, value);
        }

        public float Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }

        public float Humidity
        {
            get => _humidity;
            set => SetProperty(ref _humidity, value);
        }

        public float Pressure
        {
            get => _pressure;
            set => SetProperty(ref _pressure, value);
        }

        public string Cloudiness
        {
            get => _cloudiness;
            set => SetProperty(ref _cloudiness, value);
        }

        public double Wind
        {
            get => _wind;
            set => SetProperty(ref _wind, value);
        }

        public DateTime Sunrise
        {
            get => _sunrise;
            set => SetProperty(ref _sunrise, value);
        }

        public DateTime Sunset
        {
            get => _sunset;
            set => SetProperty(ref _sunset, value);
        }

        #endregion

        #region Overrides

        protected override async void OnAppearingExecute()
        {
            await LoadWeatherData();
        }

        #endregion

        #region Private Methods

        private async Task UpdateCommandExecute()
        {
           await LoadWeatherData();
        }

        private async Task LoadWeatherData()
        {
            var weatherData = await _service.LoadWeatherForecastAsync(Metrics.metric);

            if(weatherData != null)
            {
                Location = $"{weatherData.name}, {weatherData.sys.country}";

                WeatherIcon = ImageSource.FromUri(new Uri(string.Format(ApiUrls.WeatherImageUri, weatherData.weather.First().icon)));

                Temperature = weatherData.main.temp;

                Humidity = weatherData.main.humidity;

                Pressure = weatherData.main.pressure;

                Cloudiness = weatherData.weather.FirstOrDefault().description;

                Sunrise = new DateTime(1970, 1, 1).AddSeconds(weatherData.sys.sunrise).ToLocalTime();

                Sunset = new DateTime(1970, 1, 1).AddSeconds(weatherData.sys.sunset).ToLocalTime();

                Wind = weatherData.wind.deg;
            }
        }

        #endregion
    }
}
