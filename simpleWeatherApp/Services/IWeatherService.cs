using System.Threading.Tasks;
using simpleWeatherApp.Models.Enum;
using simpleWeatherApp.Models.Models.Weather;

namespace simpleWeatherApp.Services
{
    public interface IWeatherService
    {
        Task<Forecast> LoadWeatherForecastAsync(Metrics unit);
    }
}
