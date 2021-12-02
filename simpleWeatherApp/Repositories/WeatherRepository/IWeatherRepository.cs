using System.Threading.Tasks;
using simpleWeatherApp.Models.Enum;
using simpleWeatherApp.Models.Models;
using simpleWeatherApp.Models.Models.Weather;

namespace simpleWeatherApp.Repositories
{
    public interface IWeatherRepository
    {
        Task<OperationResult<Forecast>> GetWeatherForecastAsync(float latitude, float longitude, Metrics unit);
    }
}
