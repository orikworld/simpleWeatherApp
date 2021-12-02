using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using simpleWeatherApp.Models.Constants;
using simpleWeatherApp.Models.Models;

namespace simpleWeatherApp.Core.ApiManager
{
    public class ApiManager : IApiManager
    {
        #region Private Fields

        const string mediaType = "application/json";
 
        private readonly HttpClient client;

        #endregion

        #region Constructors

        public ApiManager()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(2);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }

        #endregion

        #region Public Methods

        public async Task<OperationResult<T>> PostAsync<T>(string requestUri, object model)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return OperationResult<T>.CreateFailure(AppConstant.NETWORK_FAILURE);
            }

            var jsonModel = model is string ? (string)model : JsonConvert.SerializeObject(model);

            HttpContent jsonContent = new StringContent(jsonModel, Encoding.UTF8, mediaType);
            HttpResponseMessage httpResponse = null;

            try
            {
                httpResponse = await client.PostAsync(requestUri, jsonContent);

                return await ParseResponseAsync<T>(httpResponse);
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
            finally
            {
                httpResponse?.Dispose();
            }
        }

        public async Task<OperationResult<T>> GetAsync<T>(string requestUri)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return OperationResult<T>.CreateFailure(AppConstant.NETWORK_FAILURE);
            }

            HttpResponseMessage httpResponse = null;
            try
            {
                httpResponse = await client.GetAsync(requestUri);

                return await ParseResponseAsync<T>(httpResponse);
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
            finally
            {
                httpResponse?.Dispose();
            }
        }

        #endregion

        #region Private Methods

        private async Task<OperationResult<T>> ParseResponseAsync<T>(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                var jsonResult = await httpResponse.Content.ReadAsStringAsync();
                return Deseralize<T>(jsonResult);
            }

            return OperationResult<T>.CreateFailure(AppConstant.END_POINT_ERROR);
        }

        private OperationResult<T> Deseralize<T>(string jsonResult)
        {
            T result;
            if (jsonResult is T)
            {
                result = (T)Convert.ChangeType(jsonResult, typeof(string));
                return OperationResult<T>.CreateSuccessResult(result);
            }

            try
            {
                if (string.IsNullOrWhiteSpace(jsonResult))
                    throw new Exception("Empty response");
                result = JsonConvert.DeserializeObject<T>(jsonResult);
            }
            catch (Exception ex)
            {
                return OperationResult<T>.CreateFailure("Error reading response value", ex);
            }
            return OperationResult<T>.CreateSuccessResult(result);
        }

        private OperationResult<T> HandleException<T>(Exception ex)
        {
            if (ex is OperationCanceledException || ex is TaskCanceledException)
                return OperationResult<T>.CreateFailure(AppConstant.CANCELLED);

            if (ex is HttpRequestException || ex is WebException)
            {

                return OperationResult<T>.CreateFailure(AppConstant.NETWORK_FAILURE);
            }

            return OperationResult<T>.CreateFailure(AppConstant.END_POINT_ERROR, ex);
        }

        #endregion
    }
}
