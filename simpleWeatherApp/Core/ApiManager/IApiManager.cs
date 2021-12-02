using System.Threading.Tasks;
using simpleWeatherApp.Models.Models;

namespace simpleWeatherApp.Core.ApiManager
{
    public interface IApiManager
    {
        Task<OperationResult<T>> PostAsync<T>(string requestUri, object model);

        Task<OperationResult<T>> GetAsync<T>(string requestUri);
    }
}
