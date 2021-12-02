using System.Threading.Tasks;
using simpleWeatherApp.Core.ApiManager;
using simpleWeatherApp.Models.Constants;
using simpleWeatherApp.Models.Enum;
using simpleWeatherApp.Models.Models;
using simpleWeatherApp.Models.Models.Weather;

namespace simpleWeatherApp.Repositories.WeatherRepository
{
    public class WeatherRepository : IWeatherRepository
    {
        #region Private Fields

        private readonly IApiManager _apiManager;

        #endregion

        #region Constructors

        public WeatherRepository(IApiManager apiManager)
        {
            _apiManager = apiManager;
        }

        #endregion

        #region Public Methods

        public async Task<OperationResult<Forecast>> GetWeatherForecastAsync(float latitude, float longitude, Metrics unit)
          => await _apiManager.GetAsync<Forecast>(string.Format(ApiUrls.WeatherForecastUri, latitude, longitude, unit));

        #endregion
    }
}
