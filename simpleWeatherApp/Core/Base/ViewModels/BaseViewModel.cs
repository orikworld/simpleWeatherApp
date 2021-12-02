using System.Windows.Input;
using Xamarin.Forms;

namespace simpleWeatherApp.Core.Base
{
    public abstract class BaseViewModel : NotifyPropertyChangedImplementation
    {
        #region Commands

        public ICommand OnAppearingCommand => new Command(OnAppearingExecute);

        public ICommand OnDisappearingCommand => new Command(OnDisappearingExecute);

        #endregion

        #region Protected Methods

        protected virtual void OnAppearingExecute()
        {
        }

        protected virtual void OnDisappearingExecute()
        {
        }

        #endregion
    }
}

