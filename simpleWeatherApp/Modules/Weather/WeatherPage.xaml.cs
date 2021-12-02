using simpleWeatherApp.Core.Base;
using simpleWeatherApp.Core.Base.VIews;
using Xamarin.Forms;

namespace simpleWeatherApp.Modules.Weather
{
    public partial class WeatherPage : BasePage
    {
        public WeatherPage(BaseViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
