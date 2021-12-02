using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using simpleWeatherApp.Models.Enum;
using simpleWeatherApp.Models.Models.Weather;
using simpleWeatherApp.Repositories;

namespace simpleWeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        #region Private Fields

        public readonly Position defaultPosition = new Position(49.8397, 24.0297);

        private readonly IWeatherRepository _repository;


        #endregion

        #region Constructors

        public WeatherService(IWeatherRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public async Task<Forecast> LoadWeatherForecastAsync(Metrics unit)
        {
            var userPosition = await GetCurrentPosition();

            var result = await _repository.GetWeatherForecastAsync(
                (float)userPosition.Latitude, (float)userPosition.Longitude, unit);

            if(result.ValidateResponse())
            {
                return result.Result;
            }

            return null;
        }

        #endregion

        #region Private Methods

        private async Task<Position> GetCurrentPosition()
        {
            if (!CrossGeolocator.Current.IsGeolocationEnabled)
            {
              
                return defaultPosition;
            }

            try
            {
                var lastKnown = await CrossGeolocator.Current?.GetLastKnownLocationAsync();
                if (lastKnown != null
                    && lastKnown != default(Position)
                    && (lastKnown.Timestamp - DateTimeOffset.Now) < TimeSpan.FromMinutes(10))
                    return lastKnown;

                return await CrossGeolocator.Current?.GetPositionAsync(TimeSpan.FromMilliseconds(300));
            }
            catch { }

            return defaultPosition;
        }
        
        #endregion

    }
}
