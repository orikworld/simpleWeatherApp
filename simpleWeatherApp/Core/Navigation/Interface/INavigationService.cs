using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simpleWeatherApp.Models.Enum;
using Xamarin.Forms;

namespace simpleWeatherApp.Core.Navigation
{
    public interface INavigationService : INavigation
    {
        #region Methods

        Task NavigateTo(ViewId viewId, bool animated = false);

        #endregion
    }
}

