using System;
using Acr.UserDialogs;
using simpleWeatherApp.Models.Constants;

namespace simpleWeatherApp.Models.Models
{
    public class OperationResult<TResult>
    {
        #region Constructors

        OperationResult() { }

        #endregion

        #region Properties

        public TResult Result { get; private set; }

        public string ErrorMessage { get; private set; }

        public Exception Exception { get; private set; }

        #endregion

        #region Public  Methods

        public static OperationResult<TResult> CreateSuccessResult(TResult result) => new OperationResult<TResult> { Result = result };

        public static OperationResult<TResult> CreateFailure(string nonSuccessMessage, Exception ex = null) => new OperationResult<TResult> { ErrorMessage = nonSuccessMessage, Exception = ex };

        public bool ValidateResponse(bool displayAlert = true)
        {
            var success = string.IsNullOrEmpty(ErrorMessage) && Exception == null;

            if (!success && displayAlert)
            {
                UserDialogs.Instance.Alert(ErrorMessage);
            }
            return success;
        }

        #endregion
    }
}
