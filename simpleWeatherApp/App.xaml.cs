using simpleWeatherApp.Core.ApiManager;
using simpleWeatherApp.Core.Base;
using simpleWeatherApp.Core.Base.VIews;
using simpleWeatherApp.Core.DependecyInjection;
using simpleWeatherApp.Core.DependecyInjection.Interfaces;
using simpleWeatherApp.Core.Navigation;
using simpleWeatherApp.Models.Enum;
using simpleWeatherApp.Modules.Weather;
using simpleWeatherApp.Modules.Weather.Base.ViewModels;
using simpleWeatherApp.Repositories;
using simpleWeatherApp.Repositories.WeatherRepository;
using simpleWeatherApp.Services;
using Xamarin.Forms;

namespace simpleWeatherApp
{
    public partial class App : Application
    {
        #region Private Fields

        private readonly ICustomContainer _container;

        #endregion

        #region Constructors

        public App()
        {
            InitializeComponent();

            _container = DependencyManager.Instance.Container;

            SetDependencies();

            InitializeNavigation();
        }

        #endregion

        #region Protected Methods

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion

        #region Private Methods

        private void InitializeNavigation()
        {
            var startingPage = DependencyManager.Instance.ServiceLocator.GetInstance<BasePage>(ViewId.WeatherPage.ToString());
            MainPage = new NavigationPage(startingPage);

            _container.RegisterInstance<INavigationService>(new NavigationService(MainPage.Navigation));
        }

        private void SetDependencies()
        {
            _container.RegisterDependency<IApiManager, ApiManager>(LifetimeCycle.SingletonInstance);

            _container.RegisterDependency<IWeatherRepository, WeatherRepository>();
            _container.RegisterDependency<IWeatherService, WeatherService>();

            //Weather Module
            _container.RegisterDependency<BasePage, WeatherPage, BaseViewModel>(ViewId.WeatherPage.ToString());
            _container.RegisterDependency<BaseViewModel, WeatherViewModel>(ViewId.WeatherPage.ToString());
        }

        #endregion
    }
}
